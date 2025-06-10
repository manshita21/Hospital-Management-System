using Microsoft.VisualBasic;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;


namespace DBSproject
{
    public partial class Form16 : Form
    {
        OracleConnection? conn;
        string expectedPatientId;
        public Form16(string id)
        {
            expectedPatientId = id;
            InitializeComponent();
           
            
            dateTimePicker2.Format = DateTimePickerFormat.Time;
            dateTimePicker2.ShowUpDown = true;
        }
        void connect()
        {
            if (conn == null)
            {
                conn = new OracleConnection("User Id=C##myuser; Password=DBMS123; Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)))");
            }

            if (conn != null)
            {
                conn.Open();
                MessageBox.Show("Connected to Database!");
            }
            else
            {
                MessageBox.Show("Connection Failed!");
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            try
            {
                connect();
                string appointmentId = textBox1.Text.Trim();
                string patientId = textBox2.Text.Trim();  // from Form15
                string doctorId = textBox3.Text.Trim();
                string date = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                string time = dateTimePicker2.Text.Trim();
                string desc = textBox4.Text.Trim();

                // Empty field check
                if (string.IsNullOrEmpty(appointmentId) || string.IsNullOrEmpty(patientId) ||
                    string.IsNullOrEmpty(doctorId) || string.IsNullOrEmpty(time) || string.IsNullOrEmpty(desc))
                {
                    MessageBox.Show("All fields must be filled.");
                    return;
                }

                // Format check
                if (!appointmentId.StartsWith("A"))
                {
                    MessageBox.Show("Appointment ID must start with 'A'.");
                    return;
                }

                if (!doctorId.StartsWith("D"))
                {
                    MessageBox.Show("Doctor ID must start with 'D'.");
                    return;
                }

                // Check if Appointment ID already exists
                string checkAppointmentQuery = $"SELECT COUNT(*) FROM APPOINTMENT WHERE APPOINTMENT_ID = '{appointmentId}'";
                OracleCommand checkAppointmentCmd = new OracleCommand(checkAppointmentQuery, conn);
                int appointmentExists = Convert.ToInt32(checkAppointmentCmd.ExecuteScalar());
                if (appointmentExists > 0)
                {
                    MessageBox.Show("Appointment ID already exists.");
                    return;
                }

                // Check if Doctor ID exists
                string checkDoctorQuery = $"SELECT COUNT(*) FROM DOCTOR WHERE DOCTOR_ID = '{doctorId}'";
                OracleCommand checkDoctorCmd = new OracleCommand(checkDoctorQuery, conn);
                int doctorExists = Convert.ToInt32(checkDoctorCmd.ExecuteScalar());
                if (doctorExists == 0)
                {
                    MessageBox.Show("Doctor ID does not exist.");
                    return;
                }

                // Optional: Check patient ID against Form15 (assumes you passed it via constructor or variable)
             //   string expectedPatientId = textBox2.Text.Trim(); // or from Form15 variable
                if (patientId != expectedPatientId)
                {
                    MessageBox.Show("Patient ID does not match the current user.");
                    return;
                }

                // Insert if all is well
                string insertQuery = $@"
                INSERT INTO APPOINTMENT 
                (APPOINTMENT_ID, PATIENT_ID, DOCTOR_ID, APPOINTMENT_DATE, APPOINTMENT_TIME, APPOINTMENT_DESC)
                VALUES (
                    '{appointmentId}',
                    {patientId},
                    '{doctorId}',
                    TO_DATE('{date}', 'YYYY-MM-DD'),
                    '{time}',
                    '{desc}'
                )";

                OracleCommand insertCmd = new OracleCommand(insertQuery, conn);
                int result = insertCmd.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show("Appointment booked successfully!");
                }
              
            }
            catch (OracleException ex)
            {
                if (ex.Number == 20001) // Trigger error
                {
                    MessageBox.Show(ex.Message);
                }
                else
                {
                    MessageBox.Show("Database Error: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
    }
}

