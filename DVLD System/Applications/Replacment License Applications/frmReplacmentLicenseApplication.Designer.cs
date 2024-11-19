﻿namespace DVLD_System.Applications.Replacment_License_Applications
{
    partial class frmReplacmentLicenseApplication
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbLost = new System.Windows.Forms.RadioButton();
            this.rbDamaged = new System.Windows.Forms.RadioButton();
            this.llShowLicenseInfo = new System.Windows.Forms.LinkLabel();
            this.llShowLicenseHistory = new System.Windows.Forms.LinkLabel();
            this.btnIssueReplacment = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblCreatedBy = new System.Windows.Forms.Label();
            this.lblOldLicenseID = new System.Windows.Forms.Label();
            this.lblReplacmentLicenseID = new System.Windows.Forms.Label();
            this.lblApplicationFees = new System.Windows.Forms.Label();
            this.lblApplicationDate = new System.Windows.Forms.Label();
            this.lblLRApplicationID = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.ctrlLicenseInfoWithFilter1 = new DVLD_System.Licenses.Controls.ctrlLicenseInfoWithFilter();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTitle.Location = new System.Drawing.Point(48, 25);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1652, 71);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Replacment For Damage License";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.rbLost);
            this.groupBox1.Controls.Add(this.rbDamaged);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(834, 123);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox1.Size = new System.Drawing.Size(870, 115);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Replacment For";
            // 
            // rbLost
            // 
            this.rbLost.AutoSize = true;
            this.rbLost.Location = new System.Drawing.Point(324, 44);
            this.rbLost.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.rbLost.Name = "rbLost";
            this.rbLost.Size = new System.Drawing.Size(103, 40);
            this.rbLost.TabIndex = 1;
            this.rbLost.TabStop = true;
            this.rbLost.Text = "Lost";
            this.rbLost.UseVisualStyleBackColor = true;
            this.rbLost.CheckedChanged += new System.EventHandler(this.rbDamaged_CheckedChanged_1);
            // 
            // rbDamaged
            // 
            this.rbDamaged.AutoSize = true;
            this.rbDamaged.Location = new System.Drawing.Point(58, 44);
            this.rbDamaged.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.rbDamaged.Name = "rbDamaged";
            this.rbDamaged.Size = new System.Drawing.Size(172, 40);
            this.rbDamaged.TabIndex = 0;
            this.rbDamaged.TabStop = true;
            this.rbDamaged.Text = "Damaged";
            this.rbDamaged.UseVisualStyleBackColor = true;
            this.rbDamaged.CheckedChanged += new System.EventHandler(this.rbDamaged_CheckedChanged_1);
            // 
            // llShowLicenseInfo
            // 
            this.llShowLicenseInfo.AutoSize = true;
            this.llShowLicenseInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llShowLicenseInfo.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.llShowLicenseInfo.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.llShowLicenseInfo.Location = new System.Drawing.Point(522, 1280);
            this.llShowLicenseInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.llShowLicenseInfo.Name = "llShowLicenseInfo";
            this.llShowLicenseInfo.Size = new System.Drawing.Size(294, 37);
            this.llShowLicenseInfo.TabIndex = 26;
            this.llShowLicenseInfo.TabStop = true;
            this.llShowLicenseInfo.Text = "Show License Info";
            this.llShowLicenseInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llShowLicenseInfo_LinkClicked);
            // 
            // llShowLicenseHistory
            // 
            this.llShowLicenseHistory.AutoSize = true;
            this.llShowLicenseHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llShowLicenseHistory.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.llShowLicenseHistory.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.llShowLicenseHistory.Location = new System.Drawing.Point(110, 1280);
            this.llShowLicenseHistory.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.llShowLicenseHistory.Name = "llShowLicenseHistory";
            this.llShowLicenseHistory.Size = new System.Drawing.Size(360, 37);
            this.llShowLicenseHistory.TabIndex = 25;
            this.llShowLicenseHistory.TabStop = true;
            this.llShowLicenseHistory.Text = "Show Licenses History";
            this.llShowLicenseHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llShowLicenseHistory_LinkClicked);
            // 
            // btnIssueReplacment
            // 
            this.btnIssueReplacment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnIssueReplacment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIssueReplacment.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIssueReplacment.Image = global::DVLD_System.Properties.Resources.Renew_Driving_License_32;
            this.btnIssueReplacment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIssueReplacment.Location = new System.Drawing.Point(1330, 1261);
            this.btnIssueReplacment.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnIssueReplacment.Name = "btnIssueReplacment";
            this.btnIssueReplacment.Size = new System.Drawing.Size(374, 71);
            this.btnIssueReplacment.TabIndex = 24;
            this.btnIssueReplacment.Text = "Issue Replacment";
            this.btnIssueReplacment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnIssueReplacment.UseVisualStyleBackColor = true;
            this.btnIssueReplacment.Click += new System.EventHandler(this.btnIssueReplacment_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::DVLD_System.Properties.Resources.close__1_;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1116, 1263);
            this.btnClose.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(202, 71);
            this.btnClose.TabIndex = 23;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.lblCreatedBy);
            this.groupBox2.Controls.Add(this.lblOldLicenseID);
            this.groupBox2.Controls.Add(this.lblReplacmentLicenseID);
            this.groupBox2.Controls.Add(this.lblApplicationFees);
            this.groupBox2.Controls.Add(this.lblApplicationDate);
            this.groupBox2.Controls.Add(this.lblLRApplicationID);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(48, 1018);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox2.Size = new System.Drawing.Size(1656, 231);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Applicationa  Info For License Replacment";
            // 
            // lblCreatedBy
            // 
            this.lblCreatedBy.AutoSize = true;
            this.lblCreatedBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreatedBy.Location = new System.Drawing.Point(1134, 164);
            this.lblCreatedBy.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCreatedBy.Name = "lblCreatedBy";
            this.lblCreatedBy.Size = new System.Drawing.Size(149, 37);
            this.lblCreatedBy.TabIndex = 20;
            this.lblCreatedBy.Text = "[ ? ? ? ?]";
            // 
            // lblOldLicenseID
            // 
            this.lblOldLicenseID.AutoSize = true;
            this.lblOldLicenseID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOldLicenseID.Location = new System.Drawing.Point(1134, 112);
            this.lblOldLicenseID.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblOldLicenseID.Name = "lblOldLicenseID";
            this.lblOldLicenseID.Size = new System.Drawing.Size(149, 37);
            this.lblOldLicenseID.TabIndex = 19;
            this.lblOldLicenseID.Text = "[ ? ? ? ?]";
            // 
            // lblReplacmentLicenseID
            // 
            this.lblReplacmentLicenseID.AutoSize = true;
            this.lblReplacmentLicenseID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReplacmentLicenseID.Location = new System.Drawing.Point(1134, 55);
            this.lblReplacmentLicenseID.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblReplacmentLicenseID.Name = "lblReplacmentLicenseID";
            this.lblReplacmentLicenseID.Size = new System.Drawing.Size(149, 37);
            this.lblReplacmentLicenseID.TabIndex = 18;
            this.lblReplacmentLicenseID.Text = "[ ? ? ? ?]";
            // 
            // lblApplicationFees
            // 
            this.lblApplicationFees.AutoSize = true;
            this.lblApplicationFees.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApplicationFees.Location = new System.Drawing.Point(394, 162);
            this.lblApplicationFees.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblApplicationFees.Name = "lblApplicationFees";
            this.lblApplicationFees.Size = new System.Drawing.Size(149, 37);
            this.lblApplicationFees.TabIndex = 14;
            this.lblApplicationFees.Text = "[ ? ? ? ?]";
            // 
            // lblApplicationDate
            // 
            this.lblApplicationDate.AutoSize = true;
            this.lblApplicationDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApplicationDate.Location = new System.Drawing.Point(394, 107);
            this.lblApplicationDate.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblApplicationDate.Name = "lblApplicationDate";
            this.lblApplicationDate.Size = new System.Drawing.Size(149, 37);
            this.lblApplicationDate.TabIndex = 13;
            this.lblApplicationDate.Text = "[ ? ? ? ?]";
            // 
            // lblLRApplicationID
            // 
            this.lblLRApplicationID.AutoSize = true;
            this.lblLRApplicationID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLRApplicationID.Location = new System.Drawing.Point(394, 56);
            this.lblLRApplicationID.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblLRApplicationID.Name = "lblLRApplicationID";
            this.lblLRApplicationID.Size = new System.Drawing.Size(149, 37);
            this.lblLRApplicationID.TabIndex = 12;
            this.lblLRApplicationID.Text = "[ ? ? ? ?]";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(954, 163);
            this.label9.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(178, 36);
            this.label9.TabIndex = 8;
            this.label9.Text = "Created By :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(900, 113);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(233, 36);
            this.label8.TabIndex = 7;
            this.label8.Text = "Old  License ID :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(788, 55);
            this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(344, 36);
            this.label7.TabIndex = 6;
            this.label7.Text = "Replacment  License ID :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(118, 163);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(245, 36);
            this.label3.TabIndex = 2;
            this.label3.Text = "Application Fees:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(114, 111);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(249, 36);
            this.label2.TabIndex = 1;
            this.label2.Text = "Application Date :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(94, 56);
            this.label12.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(272, 36);
            this.label12.TabIndex = 0;
            this.label12.Text = "L.R.Application ID :";
            // 
            // ctrlLicenseInfoWithFilter1
            // 
            this.ctrlLicenseInfoWithFilter1.Location = new System.Drawing.Point(22, 100);
            this.ctrlLicenseInfoWithFilter1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlLicenseInfoWithFilter1.Name = "ctrlLicenseInfoWithFilter1";
            this.ctrlLicenseInfoWithFilter1.Size = new System.Drawing.Size(1710, 908);
            this.ctrlLicenseInfoWithFilter1.TabIndex = 1;
            this.ctrlLicenseInfoWithFilter1.OnLicenseSelected += new System.Action<int>(this.ctrlLicenseInfoWithFilter1_OnLicenseSelected);
            // 
            // frmReplacmentLicenseApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1754, 1347);
            this.Controls.Add(this.llShowLicenseInfo);
            this.Controls.Add(this.llShowLicenseHistory);
            this.Controls.Add(this.btnIssueReplacment);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ctrlLicenseInfoWithFilter1);
            this.Controls.Add(this.lblTitle);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "frmReplacmentLicenseApplication";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmReplacmentLicenseApplication";
            this.Load += new System.EventHandler(this.frmReplacmentLicenseApplication_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private Licenses.Controls.ctrlLicenseInfoWithFilter ctrlLicenseInfoWithFilter1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbLost;
        private System.Windows.Forms.RadioButton rbDamaged;
        private System.Windows.Forms.LinkLabel llShowLicenseInfo;
        private System.Windows.Forms.LinkLabel llShowLicenseHistory;
        private System.Windows.Forms.Button btnIssueReplacment;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblCreatedBy;
        private System.Windows.Forms.Label lblOldLicenseID;
        private System.Windows.Forms.Label lblReplacmentLicenseID;
        private System.Windows.Forms.Label lblApplicationFees;
        private System.Windows.Forms.Label lblApplicationDate;
        private System.Windows.Forms.Label lblLRApplicationID;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label12;
    }
}