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
    public partial class Form6 : Form
    {
        OracleConnection? con;
        public Form6()
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
        private void button2_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7();
            form7.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connect();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            cmd.CommandText = "select med_id ,company, quantity,exp_date from Inventory ";
            cmd.CommandType = CommandType.Text;
            OracleDataAdapter adapter = new OracleDataAdapter(cmd.CommandText,con);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Inventory");
            dataGridView1.DataSource = ds.Tables[0]; 
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
