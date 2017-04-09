namespace Restaurantservice
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnCreateLabels = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dtpDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.rbnTodaysDate = new System.Windows.Forms.RadioButton();
            this.rbnPickDate = new System.Windows.Forms.RadioButton();
            this.btnTentativeOrders = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnCreateInvoices = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(13, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(387, 230);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.LightGray;
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.btnCreateLabels);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(379, 204);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Etiketter";
            // 
            // btnCreateLabels
            // 
            this.btnCreateLabels.Location = new System.Drawing.Point(58, 103);
            this.btnCreateLabels.Name = "btnCreateLabels";
            this.btnCreateLabels.Size = new System.Drawing.Size(212, 85);
            this.btnCreateLabels.TabIndex = 1;
            this.btnCreateLabels.Text = "Skapa etiketter";
            this.btnCreateLabels.UseVisualStyleBackColor = true;
            this.btnCreateLabels.Click += new System.EventHandler(this.btnCreateLabels_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.PaleGreen;
            this.tabPage2.Controls.Add(this.dtpDateTimePicker);
            this.tabPage2.Controls.Add(this.rbnTodaysDate);
            this.tabPage2.Controls.Add(this.rbnPickDate);
            this.tabPage2.Controls.Add(this.btnTentativeOrders);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(379, 204);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Preliminära beställningar";
            // 
            // dtpDateTimePicker
            // 
            this.dtpDateTimePicker.Location = new System.Drawing.Point(59, 21);
            this.dtpDateTimePicker.Name = "dtpDateTimePicker";
            this.dtpDateTimePicker.Size = new System.Drawing.Size(157, 20);
            this.dtpDateTimePicker.TabIndex = 8;
            // 
            // rbnTodaysDate
            // 
            this.rbnTodaysDate.AutoSize = true;
            this.rbnTodaysDate.Location = new System.Drawing.Point(59, 71);
            this.rbnTodaysDate.Name = "rbnTodaysDate";
            this.rbnTodaysDate.Size = new System.Drawing.Size(132, 17);
            this.rbnTodaysDate.TabIndex = 7;
            this.rbnTodaysDate.TabStop = true;
            this.rbnTodaysDate.Text = "Använd dagens datum";
            this.rbnTodaysDate.UseVisualStyleBackColor = true;
            this.rbnTodaysDate.CheckedChanged += new System.EventHandler(this.rbnTodaysDate_CheckedChanged);
            // 
            // rbnPickDate
            // 
            this.rbnPickDate.AutoSize = true;
            this.rbnPickDate.Location = new System.Drawing.Point(59, 47);
            this.rbnPickDate.Name = "rbnPickDate";
            this.rbnPickDate.Size = new System.Drawing.Size(116, 17);
            this.rbnPickDate.TabIndex = 6;
            this.rbnPickDate.TabStop = true;
            this.rbnPickDate.Text = "Välj datum manuellt";
            this.rbnPickDate.UseVisualStyleBackColor = true;
            this.rbnPickDate.CheckedChanged += new System.EventHandler(this.rbnPickDate_CheckedChanged);
            // 
            // btnTentativeOrders
            // 
            this.btnTentativeOrders.Location = new System.Drawing.Point(59, 94);
            this.btnTentativeOrders.Name = "btnTentativeOrders";
            this.btnTentativeOrders.Size = new System.Drawing.Size(157, 85);
            this.btnTentativeOrders.TabIndex = 5;
            this.btnTentativeOrders.Text = "Tag fram preliminära beställningar";
            this.btnTentativeOrders.UseVisualStyleBackColor = true;
            this.btnTentativeOrders.Click += new System.EventHandler(this.btnTentativeOrders_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.Khaki;
            this.tabPage3.Controls.Add(this.btnCreateInvoices);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(379, 204);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Fakturaunderlag";
            // 
            // btnCreateInvoices
            // 
            this.btnCreateInvoices.Location = new System.Drawing.Point(92, 56);
            this.btnCreateInvoices.Name = "btnCreateInvoices";
            this.btnCreateInvoices.Size = new System.Drawing.Size(99, 87);
            this.btnCreateInvoices.TabIndex = 1;
            this.btnCreateInvoices.Text = "Ta fram fakturaunderlag";
            this.btnCreateInvoices.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(215, 78);
            this.label1.TabIndex = 2;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 255);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "K&G Hemleverans";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnCreateLabels;
        private System.Windows.Forms.DateTimePicker dtpDateTimePicker;
        private System.Windows.Forms.RadioButton rbnTodaysDate;
        private System.Windows.Forms.RadioButton rbnPickDate;
        private System.Windows.Forms.Button btnTentativeOrders;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnCreateInvoices;
        private System.Windows.Forms.Label label1;
    }
}

