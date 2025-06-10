using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Oracle.ManagedDataAccess.Client;
namespace DBSproject
{
    public partial class Form10 : Form
    {
        OracleConnection? con;
        public Form10()
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

            // Function to format the room type to Title Case
            string ToTitleCase(string input)
            {
                return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
            }

            try
            {
                // 1. Room Type validation (required)
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Room type cannot be empty.",
                                    "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Room Price validation (must be numeric and non-negative)
                string priceInput = textBox2.Text.Trim();
                if (!decimal.TryParse(priceInput, out decimal roomPrice) || roomPrice < 0)
                {
                    MessageBox.Show("Room price must be a valid non-negative number.",
                                    "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3. Number of Beds validation (must be a positive integer)
                string bedInput = textBox3.Text.Trim();
                if (!int.TryParse(bedInput, out int noBed) || noBed <= 0)
                {
                    MessageBox.Show("Number of beds must be a positive integer.",
                                    "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Sanitize and format values
                string roomType = ToTitleCase(textBox1.Text.Trim().Replace("'", "''"));

                // Build query manually
                string query = "INSERT INTO ROOM_DETAIL VALUES ('" +
                               roomType + "', " +
                               roomPrice + ", " +
                               noBed + ")";

                // Execute query
                OracleCommand cm = new OracleCommand();
                cm.Connection = con;
                cm.CommandText = query;
                cm.ExecuteNonQuery();

                MessageBox.Show("Room inserted successfully!");
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

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
