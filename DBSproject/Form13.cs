using System;
using System.Data;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace DBSproject
{
    public partial class Form13 : Form
    {
        OracleConnection? con;

        public Form13()
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

        // Function to format the diagnose string to Title Case
        string ToTitleCase(string input)
        {
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connect();
            try
            {
                connect();

                // 1. Report ID validation (required + must start with 'R')
                string reportId = textBox2.Text.Trim();
                if (string.IsNullOrWhiteSpace(reportId) || !reportId.StartsWith("R"))
                {
                    MessageBox.Show("Report ID is required and must start with 'R'.",
                                    "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Patient ID validation (must be numeric)
                string patientIdInput = textBox1.Text;
                if (!int.TryParse(patientIdInput, out int patientId))
                {
                    MessageBox.Show("Patient ID must be a valid number.",
                                    "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3. Doctor ID validation (required + must start with 'D')
                string doctorId = textBox3.Text.Trim();
                if (string.IsNullOrWhiteSpace(doctorId) || !doctorId.StartsWith("D"))
                {
                    MessageBox.Show("Doctor ID is required and must start with 'D'.",
                                    "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 4. Diagnose validation (required and title case)
                string diagnoseRaw = textBox4.Text.Trim();
                if (string.IsNullOrWhiteSpace(diagnoseRaw))
                {
                    MessageBox.Show("Diagnose cannot be empty.",
                                    "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string diagnose = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(diagnoseRaw.ToLower());
                {
                    // 5. Report Date validation (required)
                    if (dateTimePicker1.Value == null)
                    {
                        MessageBox.Show("Report date is required.",
                                        "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    DateTime reportDate = dateTimePicker1.Value;

                  
                    reportId = reportId.Replace("'", "''");
                    doctorId = doctorId.Replace("'", "''");
                    diagnose = diagnose.Replace("'", "''");

                  
                    string query = @"
                    INSERT INTO PATIENT_REPORT (REPORT_ID, PATIENT_ID, DOCTOR_ID, DIAGNOSE, REPORT_DATE)
                    SELECT '" + reportId + @"',
                           " + patientId + @",
                           '" + doctorId + @"',
                           '" + diagnose + @"',
                           TO_DATE('" + reportDate.ToString("yyyy-MM-dd") + @"', 'YYYY-MM-DD')
                    FROM DUAL
                    WHERE NOT EXISTS (
                        SELECT 1 FROM PATIENT_REPORT WHERE REPORT_ID = '" + reportId + @"'
                    )";

                    OracleCommand cm = new OracleCommand();
                    cm.Connection = con;
                    cm.CommandText = query;

                    int rowsAffected = cm.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Report inserted successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Report with this ID already exists.", "Insertion Skipped", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
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
