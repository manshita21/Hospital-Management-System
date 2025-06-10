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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text.RegularExpressions;
namespace DBSproject
{
    public partial class Form7 : Form
    {
        OracleConnection? con;
        public Form7()
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
       
        private void button1_Click(object sender, EventArgs e)
        {

            connect();

            try
            {
                if (!Regex.IsMatch(textBox1.Text, @"^M_\d+$"))
                {
                    MessageBox.Show("Medicine ID must start with 'M_' followed by digits (e.g., M_001).", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Trim and capitalize company name
                string companyName = textBox2.Text.Trim();
                if (string.IsNullOrWhiteSpace(companyName))
                {
                    MessageBox.Show("Company name cannot be empty.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                companyName = char.ToUpper(companyName[0]) + companyName.Substring(1).ToLower();

                if (!int.TryParse(textBox3.Text, out int quantity) || quantity < 0)
                {
                    MessageBox.Show("Please enter a valid non-negative quantity.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dateTimePicker1.Value.Date < DateTime.Today)
                {
                    MessageBox.Show("Expiry date cannot be in the past.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                OracleCommand cm = new OracleCommand();
                cm.Connection = con;
                cm.CommandText = "INSERT INTO Inventory VALUES ('" +
                textBox1.Text + "', '" +
                companyName + "', " +
                textBox3.Text + ", " +
                "TO_DATE('" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', 'YYYY-MM-DD'))";



                cm.CommandType = CommandType.Text;
                cm.ExecuteNonQuery();
                MessageBox.Show("Inserted Successfully!");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
