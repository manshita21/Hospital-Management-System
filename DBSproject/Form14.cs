using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows.Forms;

namespace DBSproject
{
    public partial class Form14 : Form
    {
        OracleConnection? con;

        public Form14()
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
            try
            {
                connect();

                string reportId = textBox1.Text.Trim();
                string medId = textBox2.Text.Trim();
                string medName = textBox3.Text.Trim();
                string medPriceText = textBox4.Text.Trim();

                // Input validation
                if (string.IsNullOrWhiteSpace(reportId) || string.IsNullOrWhiteSpace(medId) ||
                    string.IsNullOrWhiteSpace(medName) || string.IsNullOrWhiteSpace(medPriceText))
                {
                    MessageBox.Show("All fields are required.");
                    return;
                }

                if (!reportId.StartsWith("R"))
                {
                    MessageBox.Show("REPORT_ID must start with 'R'.");
                    return;
                }

                if (!medId.StartsWith("M_"))
                {
                    MessageBox.Show("MED_ID must start with 'M_'.");
                    return;
                }

                // Format medicine name (capitalize first letter only)
                medName = char.ToUpper(medName[0]) + medName.Substring(1).ToLower();

                if (!decimal.TryParse(medPriceText, out decimal medPrice) || medPrice < 0)
                {
                    MessageBox.Show("Invalid price format. Must be a non-negative number.");
                    return;
                }

                // Escape single quotes for SQL
                reportId = reportId.Replace("'", "''");
                medId = medId.Replace("'", "''");
                medName = medName.Replace("'", "''");

                // Check if REPORT_ID exists in PATIENT_REPORT
                string checkReportQuery = "SELECT COUNT(*) FROM PATIENT_REPORT WHERE REPORT_ID = '" + reportId + "'";
                OracleCommand checkReportCmd = new OracleCommand(checkReportQuery, con);
                int reportExists = Convert.ToInt32(checkReportCmd.ExecuteScalar());

                if (reportExists == 0)
                {
                    MessageBox.Show("Invalid REPORT_ID: It does not exist in PATIENT_REPORT.",
                                    "Integrity Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Check if MED_ID exists in INVENTORY
                string checkInventoryQuery = "SELECT COUNT(*) FROM INVENTORY WHERE MED_ID = '" + medId + "'";
                OracleCommand checkInventoryCmd = new OracleCommand(checkInventoryQuery, con);
                int medExists = Convert.ToInt32(checkInventoryCmd.ExecuteScalar());

                if (medExists == 0)
                {
                    MessageBox.Show("Invalid MED_ID: It does not exist in INVENTORY.",
                                    "Inventory Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Insert into PRESCRIBED_MED
                string insertQuery = "INSERT INTO PRESCRIBED_MED VALUES ('" +
                                     reportId + "', '" +
                                     medId + "', '" +
                                     medName + "', " +
                                     medPrice + ")";

                OracleCommand insertCmd = new OracleCommand(insertQuery, con);
                int rows = insertCmd.ExecuteNonQuery();

                if (rows > 0)
                    MessageBox.Show("Medicine prescribed successfully!");
                else
                    MessageBox.Show("Failed to prescribe medicine.");
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
