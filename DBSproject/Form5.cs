using System;
using System.Data;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace DBSproject
{
    public partial class Form5 : Form
    {
        OracleConnection? con;
        public Form5()
        {
            InitializeComponent();
        }

        void connect()
        {
            // Establish the database connection
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

        private void button2_Click(object sender, EventArgs e)
        {
            connect();

            string patientIdInput = textBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(patientIdInput))
            {
                MessageBox.Show("Patient ID cannot be empty.",
                                 "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string funcCall = "BEGIN :result := check_patient_exists(:pid); END;";
                OracleCommand cmd = new OracleCommand(funcCall, con);
                cmd.CommandType = CommandType.Text;

                // Return value
                cmd.Parameters.Add("result", OracleDbType.Varchar2, 20).Direction = ParameterDirection.ReturnValue;

                // Input value
                cmd.Parameters.Add("pid", OracleDbType.Int32).Value = Convert.ToInt32(patientIdInput);

                cmd.ExecuteNonQuery();

                string result = cmd.Parameters["result"].Value.ToString();

                if (result == "NOT_FOUND")
                {
                    MessageBox.Show("Patient ID does not exist. Redirecting to Add Patient form.",
                                    "Patient Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Form4 form4 = new Form4(); // Add Patient Form
                    form4.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Patient ID found. Proceeding with the operation.");
                    Form15 form15 = new Form15(patientIdInput); // Replace with actual Form15 logic
                    form15.Show();
                    this.Hide();
                }
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
