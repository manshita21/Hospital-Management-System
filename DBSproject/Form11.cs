using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DBSproject
{
    public partial class Form11 : Form
    {
        OracleConnection? con;
        public Form11()
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
           
                string input = textBox1.Text.Trim();

                if (!int.TryParse(input, out int patientId))
                {
                    MessageBox.Show("Please enter a valid numeric Patient ID.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                connect(); // your existing DB connection function

                try
                {
                    string query = "SELECT DISCHARGE_STATUS FROM PATIENT WHERE PATIENT_ID = " + patientId;
                    OracleCommand cmd = new OracleCommand(query, con);
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        string dischargeStatus = result.ToString().Trim().ToUpper();
                        string message;

                        if (dischargeStatus == "YES")
                            message = "✅ Patient is registered and currently discharged.";
                        else
                            message = "🚑 Patient is registered and currently admitted.";

                        // Open Form12 with the result
                        Form12 statusForm = new Form12(patientId, message);
                        statusForm.Show();
                    }
                    else
                    {
                        MessageBox.Show("❌ Patient ID not found in the database.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (con != null && con.State == ConnectionState.Open)
                        con.Close();
                }
            

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
