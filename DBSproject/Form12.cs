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
    public partial class Form12 : Form
    {
        OracleConnection? con;
        private int patientId;
        public Form12(int id, string resultMessage)
        {
            InitializeComponent();
            patientId = id;
            this.Text = "Patient Status";
            // Configure label1 properties
            label1 = new Label();
            label1.Text = resultMessage;
            label1.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            label1.ForeColor = Color.DarkBlue;
            label1.AutoSize = true;
            label1.Location = new Point(30, 40); // Position on form

            // Add label to form
            this.Controls.Add(label1);

            // Optional: Set form size
            this.Size = new Size(450, 200);
            this.StartPosition = FormStartPosition.CenterScreen;
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
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                connect();

                // Check if patient is active
                string query = "SELECT DISCHARGE_STATUS FROM PATIENT WHERE PATIENT_ID = " + patientId;
                OracleCommand cmd = new OracleCommand(query, con);
                OracleDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string status = reader["DISCHARGE_STATUS"].ToString().Trim().ToLower();

                    if (status == "active")
                    {
                        Form13 form13 = new Form13();
                        form13.Show();
                    }
                    else
                    {
                        MessageBox.Show("Patient is inactive. Cannot add a new report.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Patient not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                reader.Close();
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

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                connect();

                // Check discharge status for the current patient
                string query = "SELECT DISCHARGE_STATUS FROM PATIENT WHERE PATIENT_ID = " + patientId;
                OracleCommand cmd = new OracleCommand(query, con);
                OracleDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string status = reader["DISCHARGE_STATUS"].ToString().Trim().ToLower();

                    if (status == "active")
                    {
                        Form14 form14 = new Form14();
                        form14.Show();
                    }
                    else
                    {
                        MessageBox.Show("Patient is inactive. Cannot prescribe medicine.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Patient not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                reader.Close();
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

        

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                connect();

                // Build SQL query string manually using patientId
                string query = "SELECT REPORT_ID, PATIENT_ID, DOCTOR_ID, DIAGNOSE, REPORT_DATE " +
                               "FROM PATIENT_REPORT WHERE PATIENT_ID = " + patientId;

                OracleCommand cmd = new OracleCommand(query, con);
                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "PatientReport");

                dataGridView1.DataSource = ds.Tables[0];
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                connect();

                // Join PATIENT_REPORT and PRESCRIBED_MED to get medicines for the patient
                string query = "SELECT pm.REPORT_ID, pm.MED_ID, pm.MED_NAME, pm.MED_PRICE " +
                               "FROM PRESCRIBED_MED pm " +
                               "JOIN PATIENT_REPORT pr ON pm.REPORT_ID = pr.REPORT_ID " +
                               "WHERE pr.PATIENT_ID = " + patientId;

                OracleCommand cmd = new OracleCommand(query, con);
                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds, " PRESCRIBED_MED,Patient_report");

                dataGridView2.DataSource = ds.Tables[0];
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

        private void contextMenu1_Popup(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
