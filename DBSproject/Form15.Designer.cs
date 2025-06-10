namespace DBSproject
{
    partial class Form15
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.appointmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uPCOMINGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pREVIOUSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.billToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aLLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prescribedMedicineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.roomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bOOKToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.appointmentToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.labToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.roomToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aDDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.billToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.bOOKToolStripMenuItem,
            this.aDDToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(917, 31);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.appointmentToolStripMenuItem,
            this.reportToolStripMenuItem,
            this.billToolStripMenuItem,
            this.prescribedMedicineToolStripMenuItem,
            this.roomToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(147, 27);
            this.toolStripMenuItem1.Text = "CHECK_RECORD";
            // 
            // appointmentToolStripMenuItem
            // 
            this.appointmentToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uPCOMINGToolStripMenuItem,
            this.pREVIOUSToolStripMenuItem});
            this.appointmentToolStripMenuItem.Name = "appointmentToolStripMenuItem";
            this.appointmentToolStripMenuItem.Size = new System.Drawing.Size(233, 28);
            this.appointmentToolStripMenuItem.Text = "Appointment";
            this.appointmentToolStripMenuItem.Click += new System.EventHandler(this.appointmentToolStripMenuItem_Click);
            // 
            // uPCOMINGToolStripMenuItem
            // 
            this.uPCOMINGToolStripMenuItem.Name = "uPCOMINGToolStripMenuItem";
            this.uPCOMINGToolStripMenuItem.Size = new System.Drawing.Size(180, 28);
            this.uPCOMINGToolStripMenuItem.Text = "UPCOMING";
            this.uPCOMINGToolStripMenuItem.Click += new System.EventHandler(this.uPCOMINGToolStripMenuItem_Click);
            // 
            // pREVIOUSToolStripMenuItem
            // 
            this.pREVIOUSToolStripMenuItem.Name = "pREVIOUSToolStripMenuItem";
            this.pREVIOUSToolStripMenuItem.Size = new System.Drawing.Size(180, 28);
            this.pREVIOUSToolStripMenuItem.Text = "PREVIOUS";
            this.pREVIOUSToolStripMenuItem.Click += new System.EventHandler(this.pREVIOUSToolStripMenuItem_Click);
            // 
            // reportToolStripMenuItem
            // 
            this.reportToolStripMenuItem.Name = "reportToolStripMenuItem";
            this.reportToolStripMenuItem.Size = new System.Drawing.Size(233, 28);
            this.reportToolStripMenuItem.Text = "Report";
            this.reportToolStripMenuItem.Click += new System.EventHandler(this.reportToolStripMenuItem_Click);
            // 
            // billToolStripMenuItem
            // 
            this.billToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aLLToolStripMenuItem});
            this.billToolStripMenuItem.Name = "billToolStripMenuItem";
            this.billToolStripMenuItem.Size = new System.Drawing.Size(233, 28);
            this.billToolStripMenuItem.Text = "Bill";
            // 
            // aLLToolStripMenuItem
            // 
            this.aLLToolStripMenuItem.Name = "aLLToolStripMenuItem";
            this.aLLToolStripMenuItem.Size = new System.Drawing.Size(187, 28);
            this.aLLToolStripMenuItem.Text = "SHOW ALL";
            this.aLLToolStripMenuItem.Click += new System.EventHandler(this.aLLToolStripMenuItem_Click);
            // 
            // prescribedMedicineToolStripMenuItem
            // 
            this.prescribedMedicineToolStripMenuItem.Name = "prescribedMedicineToolStripMenuItem";
            this.prescribedMedicineToolStripMenuItem.Size = new System.Drawing.Size(233, 28);
            this.prescribedMedicineToolStripMenuItem.Text = "Prescribed medicine";
            this.prescribedMedicineToolStripMenuItem.Click += new System.EventHandler(this.prescribedMedicineToolStripMenuItem_Click);
            // 
            // roomToolStripMenuItem
            // 
            this.roomToolStripMenuItem.Name = "roomToolStripMenuItem";
            this.roomToolStripMenuItem.Size = new System.Drawing.Size(233, 28);
            this.roomToolStripMenuItem.Text = "Room";
            this.roomToolStripMenuItem.Click += new System.EventHandler(this.roomToolStripMenuItem_Click);
            // 
            // bOOKToolStripMenuItem
            // 
            this.bOOKToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.appointmentToolStripMenuItem1,
            this.labToolStripMenuItem,
            this.roomToolStripMenuItem1});
            this.bOOKToolStripMenuItem.Name = "bOOKToolStripMenuItem";
            this.bOOKToolStripMenuItem.Size = new System.Drawing.Size(68, 27);
            this.bOOKToolStripMenuItem.Text = "BOOK";
            // 
            // appointmentToolStripMenuItem1
            // 
            this.appointmentToolStripMenuItem1.Name = "appointmentToolStripMenuItem1";
            this.appointmentToolStripMenuItem1.Size = new System.Drawing.Size(181, 28);
            this.appointmentToolStripMenuItem1.Text = "Appointment";
            this.appointmentToolStripMenuItem1.Click += new System.EventHandler(this.appointmentToolStripMenuItem1_Click);
            // 
            // labToolStripMenuItem
            // 
            this.labToolStripMenuItem.Name = "labToolStripMenuItem";
            this.labToolStripMenuItem.Size = new System.Drawing.Size(181, 28);
            this.labToolStripMenuItem.Text = "Lab";
            this.labToolStripMenuItem.Click += new System.EventHandler(this.labToolStripMenuItem_Click);
            // 
            // roomToolStripMenuItem1
            // 
            this.roomToolStripMenuItem1.Name = "roomToolStripMenuItem1";
            this.roomToolStripMenuItem1.Size = new System.Drawing.Size(181, 28);
            this.roomToolStripMenuItem1.Text = "Room";
            this.roomToolStripMenuItem1.Click += new System.EventHandler(this.roomToolStripMenuItem1_Click);
            // 
            // aDDToolStripMenuItem
            // 
            this.aDDToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.billToolStripMenuItem1});
            this.aDDToolStripMenuItem.Name = "aDDToolStripMenuItem";
            this.aDDToolStripMenuItem.Size = new System.Drawing.Size(57, 27);
            this.aDDToolStripMenuItem.Text = "ADD";
            // 
            // billToolStripMenuItem1
            // 
            this.billToolStripMenuItem1.Name = "billToolStripMenuItem1";
            this.billToolStripMenuItem1.Size = new System.Drawing.Size(180, 28);
            this.billToolStripMenuItem1.Text = "Bill";
            this.billToolStripMenuItem1.Click += new System.EventHandler(this.billToolStripMenuItem1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.AntiqueWhite;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(173, 109);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(602, 261);
            this.dataGridView1.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(669, 421);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(153, 57);
            this.button1.TabIndex = 4;
            this.button1.Text = "DISCHARGE";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form15
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 505);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form15";
            this.Text = "Form15";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem appointmentToolStripMenuItem;
        private ToolStripMenuItem reportToolStripMenuItem;
        private ToolStripMenuItem billToolStripMenuItem;
        private ToolStripMenuItem prescribedMedicineToolStripMenuItem;
        private ToolStripMenuItem roomToolStripMenuItem;
        private ToolStripMenuItem bOOKToolStripMenuItem;
        private ToolStripMenuItem appointmentToolStripMenuItem1;
        private ToolStripMenuItem labToolStripMenuItem;
        private ToolStripMenuItem roomToolStripMenuItem1;
        private ToolStripMenuItem aDDToolStripMenuItem;
        private ToolStripMenuItem billToolStripMenuItem1;
        private ToolStripMenuItem uPCOMINGToolStripMenuItem;
        private ToolStripMenuItem pREVIOUSToolStripMenuItem;
        private DataGridView dataGridView1;
        private ToolStripMenuItem aLLToolStripMenuItem;
        private Button button1;
    }
}