using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace DBSproject
{
    public partial class Form4 : Form
    {
        OracleConnection? con;

        public Form4()
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

        private bool ValidateInputs()
        {
            // Validate Patient ID (should be numeric)
            if (!int.TryParse(textBox1.Text, out _))
            {
                MessageBox.Show("Patient ID must be a numeric value!", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validate Phone Number (should be exactly 10 digits)
            if (!Regex.IsMatch(textBox4.Text, @"^\d{10}$"))
            {
                MessageBox.Show("Phone number must be exactly 10 digits!", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validate Email (should contain '@' and proper format)
            if (!Regex.IsMatch(textBox5.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Invalid email address format!", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void Insert_Click()
        {
            if (!ValidateInputs()) return; // Stop execution if validation fails

            connect();

            try
            {
                OracleCommand cm = new OracleCommand();
                cm.Connection = con;
                cm.CommandText = "INSERT INTO Patient (patient_id, first_name, last_name, gender, street, city, state, dob, phone, email) VALUES (" +
                textBox1.Text + ", '" +
                textBox2.Text + "', '" +
                textBox6.Text + "', '" +
                (radioButton1.Checked ? "FEMALE" : "MALE") + "', '" +
                textBox3.Text + "', '" +
                textBox7.Text + "', '" +
                comboBox1.Text + "', " + // No quote here before TO_DATE
                "TO_DATE('" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', 'YYYY-MM-DD'), '" +
                textBox4.Text + "', '" +
                textBox5.Text + "')";


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

        private void button1_Click_1(object sender, EventArgs e)
        {
            Insert_Click();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
