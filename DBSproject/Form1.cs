using System.Windows.Forms;

namespace DBSproject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
          //this.Resize += new EventHandler(Form1_Resize);
        }




      /*  private void Form1_Resize(object sender, EventArgs e)
        {
            ResizeAndPositionControls();
        }*/
       
        private void ResizeAndPositionControls()
        {
            int spacing = 40;
            int imageWidth = 48;
            int imageHeight = 48;

            // Set image sizes
            pictureBox1.Size = new Size(imageWidth, imageHeight);
            pictureBox2.Size = new Size(imageWidth, imageHeight);
            pictureBox3.Size = new Size(imageWidth, imageHeight);

            int totalWidth = button1.Width + button2.Width + button3.Width + (2 * spacing);
            int startX = (this.ClientSize.Width - totalWidth) / 2;
            int buttonY = this.ClientSize.Height - 150; // Position buttons near bottom

            // Position button1 and picture
            button1.Left = startX;
            button1.Top = buttonY;
            pictureBox1.Left = button1.Left + (button1.Width - pictureBox1.Width) / 2;
            pictureBox1.Top = button1.Top - pictureBox1.Height - 10;

            // Position button2 and picture
            button2.Left = button1.Right + spacing;
            button2.Top = buttonY;
            pictureBox2.Left = button2.Left + (button2.Width - pictureBox2.Width) / 2;
            pictureBox2.Top = button2.Top - pictureBox2.Height - 10;

            // Position button3 and picture
            button3.Left = button2.Right + spacing;
            button3.Top = buttonY;
            pictureBox3.Left = button3.Left + (button3.Width - pictureBox3.Width) / 2;
            pictureBox3.Top = button3.Top - pictureBox3.Height - 10;

            // Center the welcome label (label1)
            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            label1.Top = 30;

            // Center the second label (label2) just below label1
            label2.Left = (this.ClientSize.Width - label2.Width) / 2;
            label2.Top = label1.Bottom + 10;
        }






        private void button3_Click_1(object sender, EventArgs e)
        {
            Form11 form11 = new Form11();
            form11.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form3 Form3 = new Form3();
            Form3.Show();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
        //    this.WindowState = FormWindowState.Maximized;

            // Resize and reposition controls
            ResizeAndPositionControls();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
