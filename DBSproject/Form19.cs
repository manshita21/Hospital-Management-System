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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DBSproject
{

    public partial class Form19 : Form
    {
        OracleConnection? conn;
    
        public Form19()
        {
            
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

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Get input values from form fields
            string billNo = textBox1.Text.Trim();
            string patientId = textBox2.Text.Trim();
            string docCharge = textBox3.Text.Trim();
            string medCharge = textBox4.Text.Trim();
            string roomCharge = textBox5.Text.Trim();
            string noOfDays = textBox6.Text.Trim();
            string labCharge = textBox7.Text.Trim();
            string reportId = textBox8.Text.Trim();
            DateTime billDate = dateTimePicker1.Value;

            // Constraint 1: Report ID must start with 'R'
            if (!reportId.StartsWith("R"))
            {
                MessageBox.Show("Report ID must start with 'R'.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                connect(); // Open DB connection

                // Check if BILL_NO already exists
                string checkBillQuery = $"SELECT COUNT(*) FROM BILL WHERE BILL_NO = '{billNo}'";
                OracleCommand checkBillCmd = new OracleCommand(checkBillQuery, conn);
                int billExists = Convert.ToInt32(checkBillCmd.ExecuteScalar());

                if (billExists > 0)
                {
                    MessageBox.Show("Bill No already exists. Please enter a unique Bill No.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

               

                // Check if REPORT_ID exists
                string checkReportQuery = $"SELECT COUNT(*) FROM PATIENT_REPORT WHERE REPORT_ID = '{reportId}'";
                OracleCommand checkReportCmd = new OracleCommand(checkReportQuery, conn);
                int reportExists = Convert.ToInt32(checkReportCmd.ExecuteScalar());

                if (reportExists == 0)
                {
                    MessageBox.Show("Report ID not found. Please enter a valid Report ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Parse charges
                int doc = int.Parse(docCharge);
                int med = int.Parse(medCharge);
                int room = int.Parse(roomCharge);
                int days = int.Parse(noOfDays);
                int lab = int.Parse(labCharge);
                int total = doc + med + lab + (room * days);

                // Format the date
                string formattedDate = billDate.ToString("dd-MMM-yyyy");

                // Insert into BILL table
                string insertQuery = $@"
INSERT INTO BILL 
(BILL_NO, PATIENT_ID, DOC_CHARGE, MED_CHARGE, ROOM_CHARGE, NO_OF_DAYS, LAB_CHARGE, TOTAL_BILL, BILL_DATE, REPORT_ID) 
VALUES 
('{billNo}', '{patientId}', {doc}, {med}, {room}, {days}, {lab}, {total}, TO_DATE('{formattedDate}', 'DD-MON-YYYY'), '{reportId}')";

                OracleCommand insertCmd = new OracleCommand(insertQuery, conn);
                int rowsInserted = insertCmd.ExecuteNonQuery();

                if (rowsInserted > 0)
                {
                    MessageBox.Show("Bill details added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to insert bill record.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
