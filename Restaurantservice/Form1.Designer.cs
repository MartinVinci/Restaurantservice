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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnTentativeOrders = new System.Windows.Forms.Button();
            this.btnCreateLabels = new System.Windows.Forms.Button();
            this.btnCreateInvoices = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(13, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(387, 230);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnCreateLabels);
            this.tabPage1.Controls.Add(this.btnTentativeOrders);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(379, 204);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnCreateInvoices);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(379, 204);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnTentativeOrders
            // 
            this.btnTentativeOrders.Location = new System.Drawing.Point(66, 52);
            this.btnTentativeOrders.Name = "btnTentativeOrders";
            this.btnTentativeOrders.Size = new System.Drawing.Size(92, 85);
            this.btnTentativeOrders.TabIndex = 0;
            this.btnTentativeOrders.Text = "Dagens preliminära beställningar";
            this.btnTentativeOrders.UseVisualStyleBackColor = true;
            this.btnTentativeOrders.Click += new System.EventHandler(this.btnTentativeOrders_Click);
            // 
            // btnCreateLabels
            // 
            this.btnCreateLabels.Location = new System.Drawing.Point(219, 52);
            this.btnCreateLabels.Name = "btnCreateLabels";
            this.btnCreateLabels.Size = new System.Drawing.Size(92, 85);
            this.btnCreateLabels.TabIndex = 1;
            this.btnCreateLabels.Text = "Skapa etiketter";
            this.btnCreateLabels.UseVisualStyleBackColor = true;
            this.btnCreateLabels.Click += new System.EventHandler(this.btnCreateLabels_Click);
            // 
            // btnCreateInvoices
            // 
            this.btnCreateInvoices.Location = new System.Drawing.Point(64, 49);
            this.btnCreateInvoices.Name = "btnCreateInvoices";
            this.btnCreateInvoices.Size = new System.Drawing.Size(99, 87);
            this.btnCreateInvoices.TabIndex = 0;
            this.btnCreateInvoices.Text = "Ta fram fakturaunderlag";
            this.btnCreateInvoices.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 255);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnCreateLabels;
        private System.Windows.Forms.Button btnTentativeOrders;
        private System.Windows.Forms.Button btnCreateInvoices;
    }
}

