using Microsoft.VisualBasic;
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
    public partial class Form15 : Form
    {
        string patientId;
        public Form15(string pid)
        {
            InitializeComponent();
            patientId = pid;
        }




        private void appointmentToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void prescribedMedicineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string reportId = Microsoft.VisualBasic.Interaction.InputBox(
        "Enter Report ID", "Report Lookup", "R");

            if (string.IsNullOrWhiteSpace(reportId))
            {
                MessageBox.Show("Report ID cannot be empty.");
                return;
            }

            try
            {
                OracleConnection con = new OracleConnection("User Id=C##myuser; Password=DBMS123; Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)))");
                con.Open();

                // Query to check if the report belongs to the given patient
                string checkQuery = "SELECT COUNT(*) FROM PATIENT_REPORT WHERE REPORT_ID = '" + reportId + "' AND PATIENT_ID = " + patientId;
                OracleCommand checkCmd = new OracleCommand(checkQuery, con);
                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count == 0)
                {
                    MessageBox.Show("This Report ID does not belong to the current Patient.");
                    return;
                }

                // Fetch prescribed medicines
                string fetchQuery = "SELECT * FROM PRESCRIBED_MED WHERE REPORT_ID = '" + reportId + "'";
                OracleDataAdapter da = new OracleDataAdapter(fetchQuery, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Show in DataGridView (ensure you have one on the form)
                dataGridView1.DataSource = dt;

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void aLLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OracleConnection con = new OracleConnection("User Id=C##myuser; Password=DBMS123; Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)))");
                con.Open();

                string query = "SELECT * FROM BILL WHERE PATIENT_ID = :patientId";
                OracleCommand cmd = new OracleCommand(query, con);
                cmd.Parameters.Add(new OracleParameter("patientId", patientId));

                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dataGridView1.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No bill records found for this patient.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching bill data: " + ex.Message);
            }
        }

        private void bYREPORTIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string reportId = Microsoft.VisualBasic.Interaction.InputBox(
       "Enter Report ID", "Fetch Bill", "R");

            if (string.IsNullOrWhiteSpace(reportId))
            {
                MessageBox.Show("Report ID cannot be empty.");
                return;
            }

            try
            {
                OracleConnection con = new OracleConnection("User Id=C##myuser; Password=DBMS123; Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)))");
                con.Open();

                // Check if REPORT_ID is linked to this PATIENT_ID
                string checkQuery = "SELECT COUNT(*) FROM PATIENT_REPORT WHERE REPORT_ID = '" + reportId + "' AND PATIENT_ID = " + patientId;
                OracleCommand checkCmd = new OracleCommand(checkQuery, con);
                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count == 0)
                {
                    MessageBox.Show("This Report ID does not belong to the current Patient.");
                    return;
                }

                // Fetch the bill details
                string fetchQuery = "SELECT * FROM BILL WHERE REPORT_ID = '" + reportId + "'";
                OracleDataAdapter da = new OracleDataAdapter(fetchQuery, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void appointmentToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form16 form16 = new Form16(patientId);
            form16.Show();

        }

        private void labToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Form17 form17 = new Form17(patientId);
            form17.Show();
        }

        private void roomToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form18 form18 = new Form18(patientId);
            form18.Show();
        }

        private void billToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form19 form19 = new Form19();
            form19.Show();
        }

        private void reportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                OracleConnection con = new OracleConnection("User Id=C##myuser; Password=DBMS123; Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)))");
                con.Open();


                string query = $"SELECT * FROM PATIENT_REPORT WHERE PATIENT_ID = :patientId";
                OracleCommand cmd = new OracleCommand(query, con);
                cmd.Parameters.Add(new OracleParameter("patientId", patientId));

                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dataGridView1.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No reports found for this patient.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching reports: " + ex.Message);
            }

        }

        private void uPCOMINGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OracleConnection con = new OracleConnection("User Id=C##myuser; Password=DBMS123; Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)))");
                con.Open();

                string query = "SELECT * FROM APPOINTMENT WHERE APPOINTMENT_DATE > SYSDATE AND PATIENT_ID = :patientId ORDER BY APPOINTMENT_DATE ASC";

                OracleCommand cmd = new OracleCommand(query, con);
                cmd.Parameters.Add(new OracleParameter("patientId", patientId)); // Set the patient ID parameter

                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dataGridView1.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No upcoming appointments found.");
                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching upcoming appointments: " + ex.Message);
            }
        }

        private void pREVIOUSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OracleConnection con = new OracleConnection("User Id=C##myuser; Password=DBMS123; Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)))");
                con.Open();

                string query = "SELECT * FROM APPOINTMENT WHERE APPOINTMENT_DATE < SYSDATE AND PATIENT_ID = :patientId ORDER BY APPOINTMENT_DATE DESC";

                OracleCommand cmd = new OracleCommand(query, con);
                cmd.Parameters.Add(new OracleParameter("patientId", patientId)); // Bind patient ID

                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dataGridView1.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No previous appointments found.");
                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching previous appointments: " + ex.Message);
            }
        }

        private void roomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //   string patientId = currentPatientId; // Replace this with your actual patient ID variable

                OracleConnection con = new OracleConnection("User Id=C##myuser; Password=DBMS123; Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)))");
                con.Open();

                string query = "SELECT * FROM ROOM WHERE PATIENT_ID = :patientId ORDER BY ROOM_DATE DESC";

                OracleCommand cmd = new OracleCommand(query, con);
                cmd.Parameters.Add(new OracleParameter("patientId", patientId));

                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dataGridView1.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No room bookings found for this patient.");
                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching room bookings: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
    "Are you sure you want to discharge this patient?",
    "Confirm Discharge",
    MessageBoxButtons.YesNo,
    MessageBoxIcon.Question);

            if (result == DialogResult.No)
                return;

            try
            {
                OracleConnection con = new OracleConnection("User Id=C##myuser; Password=DBMS123; Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)))");
                con.Open();

                MessageBox.Show("Connected to DB. Patient ID: " + patientId); // Debug

                string updateQuery = "UPDATE PATIENT SET DISCHARGE_STATUS = 'INACTIVE' WHERE PATIENT_ID = :patientId";
                OracleCommand cmd = new OracleCommand(updateQuery, con);
                cmd.Parameters.Add(new OracleParameter("patientId", patientId));

                int rowsAffected = cmd.ExecuteNonQuery();
                MessageBox.Show("Rows affected: " + rowsAffected); // Debug

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Discharge status updated to INACTIVE.");
                }
                else
                {
                    MessageBox.Show("No record updated. Check if patient ID is correct.");
                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }
    }
}

