using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;


namespace DBSproject
{
    public partial class Form9 : Form
    {
        OracleConnection? con;

        public Form9()
        {
            InitializeComponent();
        }
        void connect()
        {
            if (con == null)
            {
                con = new OracleConnection("User Id=C##myuser; Password=DBMS123; Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)))");
            }

            if (con != null)
            {
                con.Open();
                MessageBox.Show("Connected to Database!");
            }
            else
            {
                MessageBox.Show("Connection Failed!");
            }
        }
        private void button1_Click(object sender, EventArgs e) {
            connect();
            string ToTitleCase(string input)
            {
                return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
            }
            try
            {
                // 1. Doctor ID validation
                if (string.IsNullOrWhiteSpace(textBox1.Text) || !System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, @"^D\d+$"))
                {
                    MessageBox.Show("Doctor ID must start with 'D' followed by digits (e.g., D101).", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Name validation
                if (string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Doctor name cannot be empty.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3. Specialty validation
                if (string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    MessageBox.Show("Specialty cannot be empty.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 4. Phone number validation
                string phone = textBox4.Text.Trim();
                if (!long.TryParse(phone, out _) || phone.Length != 10)
                {
                    MessageBox.Show("Phone number must be a 10-digit numeric value.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Capitalize first letter and lowercase the rest
                string name = ToTitleCase(textBox2.Text.Trim().Replace("'", "''"));

                string specialtyFormatted = char.ToUpper(textBox3.Text.Trim()[0]) + textBox3.Text.Trim().Substring(1).ToLower();

                // Build safe query string manually
                string query = "INSERT INTO DOCTOR VALUES ('" +
                               textBox1.Text.Trim().Replace("'", "''") + "', '" +    // escape single quotes
                               name.Replace("'", "''") + "', '" +
                               specialtyFormatted.Replace("'", "''") + "', '" +
                               phone.Replace("'", "''") + "')";

                OracleCommand cm = new OracleCommand();
                cm.Connection = con; 
                cm.CommandText = query;
                cm.ExecuteNonQuery();

                MessageBox.Show("Doctor inserted successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

    }
}
