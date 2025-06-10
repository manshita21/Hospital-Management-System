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
    public partial class Form18 : Form
    {
        OracleConnection? conn;
        string expectedpatientid;
        public Form18(string id)
        {
            expectedpatientid = id;
            InitializeComponent();
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
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                // Extract form values
                string patientId = textBox1.Text.Trim();     // PATIENT ID
                string roomNo = textBox2.Text.Trim();        // ROOM NO
                string roomType = comboBox1.Text.Trim();     // ROOM TYPE
                string daysText = textBox3.Text.Trim();      // NO. OF DAYS
                DateTime bookingDate = dateTimePicker1.Value;

                // Basic validation
                if (patientId != expectedpatientid)
                {
                    MessageBox.Show("Patient ID does not match expected value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!int.TryParse(daysText, out int noOfDays) || noOfDays < 1)
                {
                    MessageBox.Show("Number of days must be a positive integer (1 or more).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

          //  connect(); // Open DB connection

                try
                {

                connect(); // Open DB connection
                         

                // Check if room is already occupied
                string statusQuery = $"SELECT ROOM_STATUS FROM ROOM WHERE ROOM_NO = '{roomNo}'";
                using (OracleCommand statusCmd = new OracleCommand(statusQuery, conn))
                {
                    object? result = statusCmd.ExecuteScalar();

                    if (result != null && result.ToString().ToUpper() == "OCCUPIED")
                    {
                        MessageBox.Show("This room is already occupied. Please select a different room.", "Room Occupied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

            

                    // Format date for Oracle
                    string formattedDate = bookingDate.ToString("dd-MMM-yyyy");

                    // Insert into ROOM table
                    string insertQuery = $@"
            INSERT INTO ROOM (PATIENT_ID, ROOM_NO, ROOM_TYPE, NO_OF_DAYS, ROOM_STATUS, ROOM_DATE)
            VALUES ({patientId}, {roomNo}, '{roomType}', {noOfDays}, 'OCCUPIED', TO_DATE('{formattedDate}', 'DD-MON-YYYY'))";

                    using (OracleCommand insertCmd = new OracleCommand(insertQuery, conn))
                    {
                        int rowsInserted = insertCmd.ExecuteNonQuery();

                        if (rowsInserted > 0)
                        {
                            MessageBox.Show("Room successfully booked and marked as OCCUPIED.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to book the room.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (conn != null && conn.State == ConnectionState.Open)
                        conn.Close();
                }
            

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
