using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess;
using Oracle.ManagedDataAccess.Client;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace DBSproject
{
    public partial class Form17 : Form
    {

        OracleConnection? conn;
        string expectedPatientId;
        public Form17(string id)
        {
            expectedPatientId = id;
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
        private void button1_Click(object sender, EventArgs e)
        {
            {
                // Get input values from form fields
                string labNumber = textBox1.Text.Trim();          // LAB NUMBER
                string patientId = textBox2.Text.Trim();          // PATIENT ID
                string testType = comboBox2.Text.Trim();          // TEST TYPE
                string height = textBox4.Text.Trim();             // HEIGHT
                string weight = textBox5.Text.Trim();             // WEIGHT
                string bloodPressure = textBox6.Text.Trim();      // BLOOD PRESSURE
                string bloodGroup = comboBox1.Text.Trim();        // BLOOD GROUP
                string testCode = textBox7.Text.Trim();           // TEST CODE
                DateTime labDate = dateTimePicker1.Value;         // LAB DATE

                // Constraint 1: Check if patient ID matches
                if (patientId != expectedPatientId)
                {
                    MessageBox.Show("Patient ID does not match the expected ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Constraint 2: Lab Number must begin with "L"
                if (!labNumber.StartsWith("L"))
                {
                    MessageBox.Show("Lab Number must start with 'L'.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Constraint 3: Test Code must begin with "T"
                if (!testCode.StartsWith("T"))
                {
                    MessageBox.Show("Test Code must start with 'T'.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // If all validations pass
                // MessageBox.Show("All validations passed. You can now proceed to save this data to the database.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                try
                {
                    connect(); // Open DB connection

                    // Format the date for Oracle (dd-MMM-yyyy)
                    string formattedDate = labDate.ToString("dd-MMM-yyyy");

                    string insertQuery = $@"
        INSERT INTO lab
        (lab_no, patient_id, test_type, height, weight, blood_pressure, blood_group, test_code, lab_date) 
        VALUES (
            '{labNumber}', 
            '{patientId}', 
            '{testType}', 
            '{height}', 
            '{weight}', 
            '{bloodPressure}', 
            '{bloodGroup}', 
            '{testCode}', 
            TO_DATE('{formattedDate}', 'DD-MON-YYYY')
        )";

                    using (OracleCommand cmd = new OracleCommand(insertQuery, conn))
                    {
                        int rowsInserted = cmd.ExecuteNonQuery();

                        if (rowsInserted > 0)
                        {
                            MessageBox.Show("Lab test booked successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to insert lab test record.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (conn != null && conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
        }
    }
}
