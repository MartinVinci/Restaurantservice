﻿namespace Restaurantservice
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
            this.rbnPickupJagersro = new System.Windows.Forms.RadioButton();
            this.rbnPickupMobilia = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCreateLabels = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dtpDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.btnTentativeOrders = new System.Windows.Forms.Button();
            this.pnlPrelDate = new System.Windows.Forms.Panel();
            this.rbnPickDate = new System.Windows.Forms.RadioButton();
            this.rbnTodaysDate = new System.Windows.Forms.RadioButton();
            this.pnlPrelPickupRest = new System.Windows.Forms.Panel();
            this.rbnPrelPickupJägersro = new System.Windows.Forms.RadioButton();
            this.rbnPrelPickupMobilia = new System.Windows.Forms.RadioButton();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnCreateInvoices = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.pnlChangeColumnPosition = new System.Windows.Forms.Panel();
            this.btnColumnAdjustmentInformation = new System.Windows.Forms.Button();
            this.btnColumnXPosition = new System.Windows.Forms.Button();
            this.lblRightColumn = new System.Windows.Forms.Label();
            this.lblLeftColumn = new System.Windows.Forms.Label();
            this.nudRightColumn = new System.Windows.Forms.NumericUpDown();
            this.nudLeftColumn = new System.Windows.Forms.NumericUpDown();
            this.pnlAdminPassword = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxAdminPassWord = new System.Windows.Forms.TextBox();
            this.btnAdminPassWord = new System.Windows.Forms.Button();
            this.pnlDatabaseSelection = new System.Windows.Forms.Panel();
            this.rbnTestDataBase = new System.Windows.Forms.RadioButton();
            this.rbnRealDatabase = new System.Windows.Forms.RadioButton();
            this.lblVersion = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.pnlPrelDate.SuspendLayout();
            this.pnlPrelPickupRest.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.pnlChangeColumnPosition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRightColumn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeftColumn)).BeginInit();
            this.pnlAdminPassword.SuspendLayout();
            this.pnlDatabaseSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(13, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(387, 230);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.LightGray;
            this.tabPage1.Controls.Add(this.rbnPickupJagersro);
            this.tabPage1.Controls.Add(this.rbnPickupMobilia);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.btnCreateLabels);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPage1.Size = new System.Drawing.Size(379, 204);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Etiketter";
            // 
            // rbnPickupJagersro
            // 
            this.rbnPickupJagersro.AutoSize = true;
            this.rbnPickupJagersro.Location = new System.Drawing.Point(262, 110);
            this.rbnPickupJagersro.Name = "rbnPickupJagersro";
            this.rbnPickupJagersro.Size = new System.Drawing.Size(65, 17);
            this.rbnPickupJagersro.TabIndex = 4;
            this.rbnPickupJagersro.TabStop = true;
            this.rbnPickupJagersro.Text = "Jägersro";
            this.rbnPickupJagersro.UseVisualStyleBackColor = true;
            // 
            // rbnPickupMobilia
            // 
            this.rbnPickupMobilia.AutoSize = true;
            this.rbnPickupMobilia.Location = new System.Drawing.Point(262, 86);
            this.rbnPickupMobilia.Name = "rbnPickupMobilia";
            this.rbnPickupMobilia.Size = new System.Drawing.Size(58, 17);
            this.rbnPickupMobilia.TabIndex = 3;
            this.rbnPickupMobilia.TabStop = true;
            this.rbnPickupMobilia.Text = "Mobilia";
            this.rbnPickupMobilia.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(215, 52);
            this.label1.TabIndex = 2;
            this.label1.Text = "När du trycker på knappen kommer dagens \r\nbeställningar hämtas från databasen.\r\n\r" +
    "\nTryck inte på knappen innan stopptiden!\r\n";
            // 
            // btnCreateLabels
            // 
            this.btnCreateLabels.Location = new System.Drawing.Point(28, 86);
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
            this.tabPage2.Controls.Add(this.btnTentativeOrders);
            this.tabPage2.Controls.Add(this.pnlPrelDate);
            this.tabPage2.Controls.Add(this.pnlPrelPickupRest);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPage2.Size = new System.Drawing.Size(379, 204);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Preliminära beställningar";
            // 
            // dtpDateTimePicker
            // 
            this.dtpDateTimePicker.Location = new System.Drawing.Point(59, 6);
            this.dtpDateTimePicker.Name = "dtpDateTimePicker";
            this.dtpDateTimePicker.Size = new System.Drawing.Size(157, 20);
            this.dtpDateTimePicker.TabIndex = 8;
            // 
            // btnTentativeOrders
            // 
            this.btnTentativeOrders.Location = new System.Drawing.Point(59, 118);
            this.btnTentativeOrders.Name = "btnTentativeOrders";
            this.btnTentativeOrders.Size = new System.Drawing.Size(157, 61);
            this.btnTentativeOrders.TabIndex = 5;
            this.btnTentativeOrders.Text = "Tag fram preliminära beställningar";
            this.btnTentativeOrders.UseVisualStyleBackColor = true;
            this.btnTentativeOrders.Click += new System.EventHandler(this.btnTentativeOrders_Click);
            // 
            // pnlPrelDate
            // 
            this.pnlPrelDate.Controls.Add(this.rbnPickDate);
            this.pnlPrelDate.Controls.Add(this.rbnTodaysDate);
            this.pnlPrelDate.Location = new System.Drawing.Point(42, 35);
            this.pnlPrelDate.Name = "pnlPrelDate";
            this.pnlPrelDate.Size = new System.Drawing.Size(174, 64);
            this.pnlPrelDate.TabIndex = 11;
            // 
            // rbnPickDate
            // 
            this.rbnPickDate.AutoSize = true;
            this.rbnPickDate.Location = new System.Drawing.Point(17, 13);
            this.rbnPickDate.Name = "rbnPickDate";
            this.rbnPickDate.Size = new System.Drawing.Size(116, 17);
            this.rbnPickDate.TabIndex = 6;
            this.rbnPickDate.TabStop = true;
            this.rbnPickDate.Text = "Välj datum manuellt";
            this.rbnPickDate.UseVisualStyleBackColor = true;
            this.rbnPickDate.CheckedChanged += new System.EventHandler(this.rbnPickDate_CheckedChanged);
            // 
            // rbnTodaysDate
            // 
            this.rbnTodaysDate.AutoSize = true;
            this.rbnTodaysDate.Location = new System.Drawing.Point(17, 36);
            this.rbnTodaysDate.Name = "rbnTodaysDate";
            this.rbnTodaysDate.Size = new System.Drawing.Size(132, 17);
            this.rbnTodaysDate.TabIndex = 7;
            this.rbnTodaysDate.TabStop = true;
            this.rbnTodaysDate.Text = "Använd dagens datum";
            this.rbnTodaysDate.UseVisualStyleBackColor = true;
            this.rbnTodaysDate.CheckedChanged += new System.EventHandler(this.rbnTodaysDate_CheckedChanged);
            // 
            // pnlPrelPickupRest
            // 
            this.pnlPrelPickupRest.Controls.Add(this.rbnPrelPickupJägersro);
            this.pnlPrelPickupRest.Controls.Add(this.rbnPrelPickupMobilia);
            this.pnlPrelPickupRest.Location = new System.Drawing.Point(241, 71);
            this.pnlPrelPickupRest.Name = "pnlPrelPickupRest";
            this.pnlPrelPickupRest.Size = new System.Drawing.Size(99, 108);
            this.pnlPrelPickupRest.TabIndex = 12;
            // 
            // rbnPrelPickupJägersro
            // 
            this.rbnPrelPickupJägersro.AutoSize = true;
            this.rbnPrelPickupJägersro.Location = new System.Drawing.Point(12, 69);
            this.rbnPrelPickupJägersro.Name = "rbnPrelPickupJägersro";
            this.rbnPrelPickupJägersro.Size = new System.Drawing.Size(65, 17);
            this.rbnPrelPickupJägersro.TabIndex = 10;
            this.rbnPrelPickupJägersro.TabStop = true;
            this.rbnPrelPickupJägersro.Text = "Jägersro";
            this.rbnPrelPickupJägersro.UseVisualStyleBackColor = true;
            // 
            // rbnPrelPickupMobilia
            // 
            this.rbnPrelPickupMobilia.AutoSize = true;
            this.rbnPrelPickupMobilia.Location = new System.Drawing.Point(12, 47);
            this.rbnPrelPickupMobilia.Name = "rbnPrelPickupMobilia";
            this.rbnPrelPickupMobilia.Size = new System.Drawing.Size(58, 17);
            this.rbnPrelPickupMobilia.TabIndex = 9;
            this.rbnPrelPickupMobilia.TabStop = true;
            this.rbnPrelPickupMobilia.Text = "Mobilia";
            this.rbnPrelPickupMobilia.UseVisualStyleBackColor = true;
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
            this.btnCreateInvoices.Click += new System.EventHandler(this.btnCreateInvoices_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.Red;
            this.tabPage4.Controls.Add(this.pnlChangeColumnPosition);
            this.tabPage4.Controls.Add(this.pnlAdminPassword);
            this.tabPage4.Controls.Add(this.pnlDatabaseSelection);
            this.tabPage4.Controls.Add(this.lblVersion);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(379, 204);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Administratörssida";
            // 
            // pnlChangeColumnPosition
            // 
            this.pnlChangeColumnPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlChangeColumnPosition.Controls.Add(this.btnColumnAdjustmentInformation);
            this.pnlChangeColumnPosition.Controls.Add(this.btnColumnXPosition);
            this.pnlChangeColumnPosition.Controls.Add(this.lblRightColumn);
            this.pnlChangeColumnPosition.Controls.Add(this.lblLeftColumn);
            this.pnlChangeColumnPosition.Controls.Add(this.nudRightColumn);
            this.pnlChangeColumnPosition.Controls.Add(this.nudLeftColumn);
            this.pnlChangeColumnPosition.Location = new System.Drawing.Point(174, 20);
            this.pnlChangeColumnPosition.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlChangeColumnPosition.Name = "pnlChangeColumnPosition";
            this.pnlChangeColumnPosition.Size = new System.Drawing.Size(151, 159);
            this.pnlChangeColumnPosition.TabIndex = 11;
            // 
            // btnColumnAdjustmentInformation
            // 
            this.btnColumnAdjustmentInformation.Location = new System.Drawing.Point(21, 5);
            this.btnColumnAdjustmentInformation.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnColumnAdjustmentInformation.Name = "btnColumnAdjustmentInformation";
            this.btnColumnAdjustmentInformation.Size = new System.Drawing.Size(110, 35);
            this.btnColumnAdjustmentInformation.TabIndex = 10;
            this.btnColumnAdjustmentInformation.Text = "Information";
            this.btnColumnAdjustmentInformation.UseVisualStyleBackColor = true;
            this.btnColumnAdjustmentInformation.Click += new System.EventHandler(this.btnColumnAdjustmentInformation_Click);
            // 
            // btnColumnXPosition
            // 
            this.btnColumnXPosition.Location = new System.Drawing.Point(21, 126);
            this.btnColumnXPosition.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnColumnXPosition.Name = "btnColumnXPosition";
            this.btnColumnXPosition.Size = new System.Drawing.Size(110, 23);
            this.btnColumnXPosition.TabIndex = 9;
            this.btnColumnXPosition.Text = "Spara ändringar";
            this.btnColumnXPosition.UseVisualStyleBackColor = true;
            this.btnColumnXPosition.Click += new System.EventHandler(this.btnColumnXPosition_Click);
            // 
            // lblRightColumn
            // 
            this.lblRightColumn.AutoSize = true;
            this.lblRightColumn.Location = new System.Drawing.Point(19, 87);
            this.lblRightColumn.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRightColumn.Name = "lblRightColumn";
            this.lblRightColumn.Size = new System.Drawing.Size(114, 13);
            this.lblRightColumn.TabIndex = 7;
            this.lblRightColumn.Text = "Högra etikettkolumnen";
            // 
            // lblLeftColumn
            // 
            this.lblLeftColumn.AutoSize = true;
            this.lblLeftColumn.Location = new System.Drawing.Point(19, 48);
            this.lblLeftColumn.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLeftColumn.Name = "lblLeftColumn";
            this.lblLeftColumn.Size = new System.Drawing.Size(121, 13);
            this.lblLeftColumn.TabIndex = 7;
            this.lblLeftColumn.Text = "Vänstra etikettkolumnen";
            // 
            // nudRightColumn
            // 
            this.nudRightColumn.Location = new System.Drawing.Point(21, 102);
            this.nudRightColumn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.nudRightColumn.Name = "nudRightColumn";
            this.nudRightColumn.Size = new System.Drawing.Size(50, 20);
            this.nudRightColumn.TabIndex = 6;
            // 
            // nudLeftColumn
            // 
            this.nudLeftColumn.Location = new System.Drawing.Point(21, 64);
            this.nudLeftColumn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.nudLeftColumn.Name = "nudLeftColumn";
            this.nudLeftColumn.Size = new System.Drawing.Size(50, 20);
            this.nudLeftColumn.TabIndex = 5;
            // 
            // pnlAdminPassword
            // 
            this.pnlAdminPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAdminPassword.Controls.Add(this.label2);
            this.pnlAdminPassword.Controls.Add(this.tbxAdminPassWord);
            this.pnlAdminPassword.Controls.Add(this.btnAdminPassWord);
            this.pnlAdminPassword.Location = new System.Drawing.Point(20, 20);
            this.pnlAdminPassword.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlAdminPassword.Name = "pnlAdminPassword";
            this.pnlAdminPassword.Size = new System.Drawing.Size(109, 82);
            this.pnlAdminPassword.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Skriv lösenord";
            // 
            // tbxAdminPassWord
            // 
            this.tbxAdminPassWord.Location = new System.Drawing.Point(3, 22);
            this.tbxAdminPassWord.Name = "tbxAdminPassWord";
            this.tbxAdminPassWord.Size = new System.Drawing.Size(100, 20);
            this.tbxAdminPassWord.TabIndex = 1;
            // 
            // btnAdminPassWord
            // 
            this.btnAdminPassWord.Location = new System.Drawing.Point(3, 48);
            this.btnAdminPassWord.Name = "btnAdminPassWord";
            this.btnAdminPassWord.Size = new System.Drawing.Size(100, 23);
            this.btnAdminPassWord.TabIndex = 0;
            this.btnAdminPassWord.Text = "Lås upp admin";
            this.btnAdminPassWord.UseVisualStyleBackColor = true;
            this.btnAdminPassWord.Click += new System.EventHandler(this.btnAdminPassWord_Click);
            // 
            // pnlDatabaseSelection
            // 
            this.pnlDatabaseSelection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDatabaseSelection.Controls.Add(this.rbnTestDataBase);
            this.pnlDatabaseSelection.Controls.Add(this.rbnRealDatabase);
            this.pnlDatabaseSelection.Location = new System.Drawing.Point(20, 106);
            this.pnlDatabaseSelection.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlDatabaseSelection.Name = "pnlDatabaseSelection";
            this.pnlDatabaseSelection.Size = new System.Drawing.Size(109, 73);
            this.pnlDatabaseSelection.TabIndex = 8;
            // 
            // rbnTestDataBase
            // 
            this.rbnTestDataBase.AutoSize = true;
            this.rbnTestDataBase.Location = new System.Drawing.Point(10, 17);
            this.rbnTestDataBase.Name = "rbnTestDataBase";
            this.rbnTestDataBase.Size = new System.Drawing.Size(84, 17);
            this.rbnTestDataBase.TabIndex = 2;
            this.rbnTestDataBase.TabStop = true;
            this.rbnTestDataBase.Text = "Testdatabas";
            this.rbnTestDataBase.UseVisualStyleBackColor = true;
            this.rbnTestDataBase.CheckedChanged += new System.EventHandler(this.rbnTestDataBase_CheckedChanged);
            // 
            // rbnRealDatabase
            // 
            this.rbnRealDatabase.AutoSize = true;
            this.rbnRealDatabase.Location = new System.Drawing.Point(10, 41);
            this.rbnRealDatabase.Name = "rbnRealDatabase";
            this.rbnRealDatabase.Size = new System.Drawing.Size(93, 17);
            this.rbnRealDatabase.TabIndex = 3;
            this.rbnRealDatabase.TabStop = true;
            this.rbnRealDatabase.Text = "Riktig databas";
            this.rbnRealDatabase.UseVisualStyleBackColor = true;
            this.rbnRealDatabase.CheckedChanged += new System.EventHandler(this.rbnRealDatabase_CheckedChanged);
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(3, 181);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(42, 13);
            this.lblVersion.TabIndex = 4;
            this.lblVersion.Text = "Version";
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
            this.pnlPrelDate.ResumeLayout(false);
            this.pnlPrelDate.PerformLayout();
            this.pnlPrelPickupRest.ResumeLayout(false);
            this.pnlPrelPickupRest.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.pnlChangeColumnPosition.ResumeLayout(false);
            this.pnlChangeColumnPosition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRightColumn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeftColumn)).EndInit();
            this.pnlAdminPassword.ResumeLayout(false);
            this.pnlAdminPassword.PerformLayout();
            this.pnlDatabaseSelection.ResumeLayout(false);
            this.pnlDatabaseSelection.PerformLayout();
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
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.RadioButton rbnRealDatabase;
        private System.Windows.Forms.RadioButton rbnTestDataBase;
        private System.Windows.Forms.TextBox tbxAdminPassWord;
        private System.Windows.Forms.Button btnAdminPassWord;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.RadioButton rbnPickupJagersro;
        private System.Windows.Forms.RadioButton rbnPickupMobilia;
        private System.Windows.Forms.RadioButton rbnPrelPickupJägersro;
        private System.Windows.Forms.RadioButton rbnPrelPickupMobilia;
        private System.Windows.Forms.Panel pnlPrelDate;
        private System.Windows.Forms.Panel pnlPrelPickupRest;
        private System.Windows.Forms.Panel pnlDatabaseSelection;
        private System.Windows.Forms.Label lblRightColumn;
        private System.Windows.Forms.Label lblLeftColumn;
        private System.Windows.Forms.NumericUpDown nudRightColumn;
        private System.Windows.Forms.NumericUpDown nudLeftColumn;
        private System.Windows.Forms.Button btnColumnXPosition;
        private System.Windows.Forms.Panel pnlAdminPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlChangeColumnPosition;
        private System.Windows.Forms.Button btnColumnAdjustmentInformation;
    }
}

