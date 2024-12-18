namespace DVLD_System.VehicleLicenses
{
    partial class frmRenewVehicleLicense
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
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlVehicleInfoWithFilter1 = new DVLD_System.Vehicles.Controls.ctrlVehicleInfoWithFilter();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCreatedBy = new System.Windows.Forms.Label();
            this.lblExpirationDate = new System.Windows.Forms.Label();
            this.lblVehicleID = new System.Windows.Forms.Label();
            this.lblVehicleLicenseID = new System.Windows.Forms.Label();
            this.lblLicenseFees = new System.Windows.Forms.Label();
            this.lblApplicationFees = new System.Windows.Forms.Label();
            this.lblIssueDate = new System.Windows.Forms.Label();
            this.lblApplicationDate = new System.Windows.Forms.Label();
            this.lblApplicationID = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.llbShowVehicleLicense = new System.Windows.Forms.LinkLabel();
            this.llShowVehicleLicenseHistory = new System.Windows.Forms.LinkLabel();
            this.btnRenewLicense = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Titillium Web", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(855, 44);
            this.label1.TabIndex = 13;
            this.label1.Text = "Renew Vehichle License";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ctrlVehicleInfoWithFilter1
            // 
            this.ctrlVehicleInfoWithFilter1.BackColor = System.Drawing.Color.White;
            this.ctrlVehicleInfoWithFilter1.FilterEnabled = true;
            this.ctrlVehicleInfoWithFilter1.Location = new System.Drawing.Point(6, 68);
            this.ctrlVehicleInfoWithFilter1.Name = "ctrlVehicleInfoWithFilter1";
            this.ctrlVehicleInfoWithFilter1.Size = new System.Drawing.Size(855, 415);
            this.ctrlVehicleInfoWithFilter1.TabIndex = 14;
            this.ctrlVehicleInfoWithFilter1.OnVehicleSelected += new System.Action<int>(this.ctrlVehicleInfoWithFilter1_OnVehicleSelected);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.lblCreatedBy);
            this.groupBox1.Controls.Add(this.lblExpirationDate);
            this.groupBox1.Controls.Add(this.lblVehicleID);
            this.groupBox1.Controls.Add(this.lblVehicleLicenseID);
            this.groupBox1.Controls.Add(this.lblLicenseFees);
            this.groupBox1.Controls.Add(this.lblApplicationFees);
            this.groupBox1.Controls.Add(this.lblIssueDate);
            this.groupBox1.Controls.Add(this.lblApplicationDate);
            this.groupBox1.Controls.Add(this.lblApplicationID);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Font = new System.Drawing.Font("Titillium Web", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(6, 489);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(855, 257);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Applicationa Renew License Info";
            // 
            // lblCreatedBy
            // 
            this.lblCreatedBy.AutoSize = true;
            this.lblCreatedBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreatedBy.Location = new System.Drawing.Point(567, 147);
            this.lblCreatedBy.Name = "lblCreatedBy";
            this.lblCreatedBy.Size = new System.Drawing.Size(79, 20);
            this.lblCreatedBy.TabIndex = 21;
            this.lblCreatedBy.Text = "[ ? ? ? ?]";
            // 
            // lblExpirationDate
            // 
            this.lblExpirationDate.AutoSize = true;
            this.lblExpirationDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpirationDate.Location = new System.Drawing.Point(567, 115);
            this.lblExpirationDate.Name = "lblExpirationDate";
            this.lblExpirationDate.Size = new System.Drawing.Size(79, 20);
            this.lblExpirationDate.TabIndex = 20;
            this.lblExpirationDate.Text = "[ ? ? ? ?]";
            // 
            // lblVehicleID
            // 
            this.lblVehicleID.AutoSize = true;
            this.lblVehicleID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVehicleID.Location = new System.Drawing.Point(567, 78);
            this.lblVehicleID.Name = "lblVehicleID";
            this.lblVehicleID.Size = new System.Drawing.Size(79, 20);
            this.lblVehicleID.TabIndex = 19;
            this.lblVehicleID.Text = "[ ? ? ? ?]";
            // 
            // lblVehicleLicenseID
            // 
            this.lblVehicleLicenseID.AutoSize = true;
            this.lblVehicleLicenseID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVehicleLicenseID.Location = new System.Drawing.Point(567, 42);
            this.lblVehicleLicenseID.Name = "lblVehicleLicenseID";
            this.lblVehicleLicenseID.Size = new System.Drawing.Size(79, 20);
            this.lblVehicleLicenseID.TabIndex = 18;
            this.lblVehicleLicenseID.Text = "[ ? ? ? ?]";
            // 
            // lblLicenseFees
            // 
            this.lblLicenseFees.AutoSize = true;
            this.lblLicenseFees.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLicenseFees.Location = new System.Drawing.Point(213, 180);
            this.lblLicenseFees.Name = "lblLicenseFees";
            this.lblLicenseFees.Size = new System.Drawing.Size(79, 20);
            this.lblLicenseFees.TabIndex = 16;
            this.lblLicenseFees.Text = "[ ? ? ? ?]";
            // 
            // lblApplicationFees
            // 
            this.lblApplicationFees.AutoSize = true;
            this.lblApplicationFees.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApplicationFees.Location = new System.Drawing.Point(213, 147);
            this.lblApplicationFees.Name = "lblApplicationFees";
            this.lblApplicationFees.Size = new System.Drawing.Size(79, 20);
            this.lblApplicationFees.TabIndex = 15;
            this.lblApplicationFees.Text = "[ ? ? ? ?]";
            // 
            // lblIssueDate
            // 
            this.lblIssueDate.AutoSize = true;
            this.lblIssueDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIssueDate.Location = new System.Drawing.Point(215, 112);
            this.lblIssueDate.Name = "lblIssueDate";
            this.lblIssueDate.Size = new System.Drawing.Size(79, 20);
            this.lblIssueDate.TabIndex = 14;
            this.lblIssueDate.Text = "[ ? ? ? ?]";
            // 
            // lblApplicationDate
            // 
            this.lblApplicationDate.AutoSize = true;
            this.lblApplicationDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApplicationDate.Location = new System.Drawing.Point(215, 78);
            this.lblApplicationDate.Name = "lblApplicationDate";
            this.lblApplicationDate.Size = new System.Drawing.Size(79, 20);
            this.lblApplicationDate.TabIndex = 13;
            this.lblApplicationDate.Text = "[ ? ? ? ?]";
            // 
            // lblApplicationID
            // 
            this.lblApplicationID.AutoSize = true;
            this.lblApplicationID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApplicationID.Location = new System.Drawing.Point(215, 42);
            this.lblApplicationID.Name = "lblApplicationID";
            this.lblApplicationID.Size = new System.Drawing.Size(79, 20);
            this.lblApplicationID.TabIndex = 12;
            this.lblApplicationID.Text = "[ ? ? ? ?]";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(450, 141);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(106, 29);
            this.label10.TabIndex = 9;
            this.label10.Text = "Created By:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(410, 106);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(146, 29);
            this.label9.TabIndex = 8;
            this.label9.Text = "Expiration Date :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(458, 71);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 29);
            this.label8.TabIndex = 7;
            this.label8.Text = "Vehicle ID :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(394, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(162, 29);
            this.label7.TabIndex = 6;
            this.label7.Text = "Vehicle License ID :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(79, 171);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 29);
            this.label5.TabIndex = 4;
            this.label5.Text = "License Fees :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(51, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 29);
            this.label4.TabIndex = 3;
            this.label4.Text = "Application Fees :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(100, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 29);
            this.label3.TabIndex = 2;
            this.label3.Text = "Issue Date :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(57, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 29);
            this.label2.TabIndex = 1;
            this.label2.Text = "Application Date :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(79, 36);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(130, 29);
            this.label12.TabIndex = 0;
            this.label12.Text = "Application ID :";
            // 
            // llbShowVehicleLicense
            // 
            this.llbShowVehicleLicense.AutoSize = true;
            this.llbShowVehicleLicense.Enabled = false;
            this.llbShowVehicleLicense.Font = new System.Drawing.Font("Titillium Web", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llbShowVehicleLicense.Location = new System.Drawing.Point(288, 762);
            this.llbShowVehicleLicense.Name = "llbShowVehicleLicense";
            this.llbShowVehicleLicense.Size = new System.Drawing.Size(188, 29);
            this.llbShowVehicleLicense.TabIndex = 26;
            this.llbShowVehicleLicense.TabStop = true;
            this.llbShowVehicleLicense.Text = "Show Vehicle License";
            // 
            // llShowVehicleLicenseHistory
            // 
            this.llShowVehicleLicenseHistory.AutoSize = true;
            this.llShowVehicleLicenseHistory.Enabled = false;
            this.llShowVehicleLicenseHistory.Font = new System.Drawing.Font("Titillium Web", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llShowVehicleLicenseHistory.Location = new System.Drawing.Point(21, 762);
            this.llShowVehicleLicenseHistory.Name = "llShowVehicleLicenseHistory";
            this.llShowVehicleLicenseHistory.Size = new System.Drawing.Size(252, 29);
            this.llShowVehicleLicenseHistory.TabIndex = 25;
            this.llShowVehicleLicenseHistory.TabStop = true;
            this.llShowVehicleLicenseHistory.Text = "Show Vehicle License History";
            // 
            // btnRenewLicense
            // 
            this.btnRenewLicense.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRenewLicense.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRenewLicense.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRenewLicense.Enabled = false;
            this.btnRenewLicense.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRenewLicense.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRenewLicense.Image = global::DVLD_System.Properties.Resources.refresh;
            this.btnRenewLicense.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRenewLicense.Location = new System.Drawing.Point(699, 762);
            this.btnRenewLicense.Name = "btnRenewLicense";
            this.btnRenewLicense.Size = new System.Drawing.Size(162, 37);
            this.btnRenewLicense.TabIndex = 24;
            this.btnRenewLicense.Text = "Renew License";
            this.btnRenewLicense.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRenewLicense.UseVisualStyleBackColor = true;
            this.btnRenewLicense.Click += new System.EventHandler(this.btnRenewLicense_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::DVLD_System.Properties.Resources.close__1_;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(595, 762);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(98, 37);
            this.btnClose.TabIndex = 23;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmRenewVehicleLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 812);
            this.Controls.Add(this.llbShowVehicleLicense);
            this.Controls.Add(this.llShowVehicleLicenseHistory);
            this.Controls.Add(this.btnRenewLicense);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ctrlVehicleInfoWithFilter1);
            this.Controls.Add(this.label1);
            this.Name = "frmRenewVehicleLicense";
            this.Text = "Renew Vehicle License";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Vehicles.Controls.ctrlVehicleInfoWithFilter ctrlVehicleInfoWithFilter1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblCreatedBy;
        private System.Windows.Forms.Label lblExpirationDate;
        private System.Windows.Forms.Label lblVehicleID;
        private System.Windows.Forms.Label lblVehicleLicenseID;
        private System.Windows.Forms.Label lblLicenseFees;
        private System.Windows.Forms.Label lblApplicationFees;
        private System.Windows.Forms.Label lblIssueDate;
        private System.Windows.Forms.Label lblApplicationDate;
        private System.Windows.Forms.Label lblApplicationID;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.LinkLabel llbShowVehicleLicense;
        private System.Windows.Forms.LinkLabel llShowVehicleLicenseHistory;
        private System.Windows.Forms.Button btnRenewLicense;
        private System.Windows.Forms.Button btnClose;
    }
}