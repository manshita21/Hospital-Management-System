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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void Form2_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
          

            // Resize and reposition controls
            ResizeAndPositionControls();
        }
       
        private void ResizeAndPositionControls()
        {
            int buttonSpacing = 20;
            int formCenterX = this.ClientSize.Width / 2;

            // Center label1 at the top
            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            label1.Top = 30;

            // Position buttons vertically centered
            int totalHeight = button1.Height + button2.Height + button3.Height + (2 * buttonSpacing);
            int startY = (this.ClientSize.Height - totalHeight) / 2;

            // Position button1
            button1.Left = formCenterX - button1.Width / 2;
            button1.Top = startY;

            // Position button2
            button2.Left = formCenterX - button2.Width / 2;
            button2.Top = button1.Bottom + buttonSpacing;

            // Position button3
            button3.Left = formCenterX - button3.Width / 2;
            button3.Top = button2.Bottom + buttonSpacing;
        }



        private void button1_Click_1(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            form6.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form8 form8 = new Form8();
            form8.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form10 form10 = new Form10();
            form10.Show();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
