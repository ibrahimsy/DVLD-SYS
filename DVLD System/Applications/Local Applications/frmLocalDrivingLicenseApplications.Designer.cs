namespace DVLD_System.Applications
{
    partial class frmLocalDrivingLicenseApplications
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.dgvLocalDrivingLicenseApplicatios = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.editApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cancelApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.schedualTest = new System.Windows.Forms.ToolStripMenuItem();
            this.schedualVisionTest = new System.Windows.Forms.ToolStripMenuItem();
            this.schedualWrittenTest = new System.Windows.Forms.ToolStripMenuItem();
            this.schedualStreetTest = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.issueDrivingLicenseFirstTime = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.showLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.showLicensePersonHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtFilterValue = new System.Windows.Forms.TextBox();
            this.cbApplicationStatus = new System.Windows.Forms.ComboBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAddNewApplication = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblNumberOfRecords = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalDrivingLicenseApplicatios)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(392, 157);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(330, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Local Driving License Applications";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 213);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Filter By : ";
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Items.AddRange(new object[] {
            "None",
            "LDLAppID",
            "National No",
            "Full Name",
            "Status"});
            this.cbFilterBy.Location = new System.Drawing.Point(99, 215);
            this.cbFilterBy.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(198, 21);
            this.cbFilterBy.TabIndex = 7;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.ccbFilterBy_SelectedIndexChanged);
            // 
            // dgvLocalDrivingLicenseApplicatios
            // 
            this.dgvLocalDrivingLicenseApplicatios.AllowUserToAddRows = false;
            this.dgvLocalDrivingLicenseApplicatios.AllowUserToDeleteRows = false;
            this.dgvLocalDrivingLicenseApplicatios.AllowUserToOrderColumns = true;
            this.dgvLocalDrivingLicenseApplicatios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLocalDrivingLicenseApplicatios.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLocalDrivingLicenseApplicatios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocalDrivingLicenseApplicatios.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvLocalDrivingLicenseApplicatios.Location = new System.Drawing.Point(18, 255);
            this.dgvLocalDrivingLicenseApplicatios.Name = "dgvLocalDrivingLicenseApplicatios";
            this.dgvLocalDrivingLicenseApplicatios.ReadOnly = true;
            this.dgvLocalDrivingLicenseApplicatios.RowHeadersWidth = 82;
            this.dgvLocalDrivingLicenseApplicatios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLocalDrivingLicenseApplicatios.Size = new System.Drawing.Size(1121, 263);
            this.dgvLocalDrivingLicenseApplicatios.TabIndex = 8;
            this.dgvLocalDrivingLicenseApplicatios.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvLocalDrivingLicenseApplicatios_CellFormatting);
            this.dgvLocalDrivingLicenseApplicatios.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvLocalDrivingLicenseApplicatios_CellMouseDown);
            this.dgvLocalDrivingLicenseApplicatios.DoubleClick += new System.EventHandler(this.dgvLocalDrivingLicenseApplicatios_DoubleClick);
            this.dgvLocalDrivingLicenseApplicatios.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvLocalDrivingLicenseApplicatios_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showDetailsToolStripMenuItem,
            this.toolStripSeparator1,
            this.editApplication,
            this.deleteApplication,
            this.toolStripSeparator2,
            this.cancelApplication,
            this.toolStripSeparator3,
            this.schedualTest,
            this.toolStripSeparator4,
            this.issueDrivingLicenseFirstTime,
            this.toolStripSeparator5,
            this.showLicense,
            this.toolStripSeparator6,
            this.showLicensePersonHistoryToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(262, 344);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.cmsApplication_opening);
            // 
            // showDetailsToolStripMenuItem
            // 
            this.showDetailsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showDetailsToolStripMenuItem.Image = global::DVLD_System.Properties.Resources.PersonDetails_32;
            this.showDetailsToolStripMenuItem.Name = "showDetailsToolStripMenuItem";
            this.showDetailsToolStripMenuItem.Size = new System.Drawing.Size(261, 38);
            this.showDetailsToolStripMenuItem.Text = "Show Application Details";
            this.showDetailsToolStripMenuItem.Click += new System.EventHandler(this.showDetailsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(258, 6);
            // 
            // editApplication
            // 
            this.editApplication.Image = global::DVLD_System.Properties.Resources.edit_32;
            this.editApplication.Name = "editApplication";
            this.editApplication.Size = new System.Drawing.Size(261, 38);
            this.editApplication.Text = "Edit Application";
            this.editApplication.Click += new System.EventHandler(this.editApplicationToolStripMenuItem_Click);
            // 
            // deleteApplication
            // 
            this.deleteApplication.Image = global::DVLD_System.Properties.Resources.Delete_32_2;
            this.deleteApplication.Name = "deleteApplication";
            this.deleteApplication.Size = new System.Drawing.Size(261, 38);
            this.deleteApplication.Text = "Delete Application";
            this.deleteApplication.Click += new System.EventHandler(this.deleteApplicationToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(258, 6);
            // 
            // cancelApplication
            // 
            this.cancelApplication.Image = global::DVLD_System.Properties.Resources.Delete_32;
            this.cancelApplication.Name = "cancelApplication";
            this.cancelApplication.Size = new System.Drawing.Size(261, 38);
            this.cancelApplication.Text = "Cancel Application";
            this.cancelApplication.Click += new System.EventHandler(this.cancelApplicationToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(258, 6);
            // 
            // schedualTest
            // 
            this.schedualTest.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.schedualVisionTest,
            this.schedualWrittenTest,
            this.schedualStreetTest});
            this.schedualTest.Image = global::DVLD_System.Properties.Resources.Schedule_Test_32;
            this.schedualTest.Name = "schedualTest";
            this.schedualTest.Size = new System.Drawing.Size(261, 38);
            this.schedualTest.Text = "Schedual Test";
            this.schedualTest.Click += new System.EventHandler(this.schedualTestToolStripMenuItem_Click);
            // 
            // schedualVisionTest
            // 
            this.schedualVisionTest.Image = global::DVLD_System.Properties.Resources.Vision_Test_32;
            this.schedualVisionTest.Name = "schedualVisionTest";
            this.schedualVisionTest.Size = new System.Drawing.Size(187, 22);
            this.schedualVisionTest.Text = "Schedual Vision Test";
            this.schedualVisionTest.Click += new System.EventHandler(this.schedualVisionTestToolStripMenuItem1_Click);
            // 
            // schedualWrittenTest
            // 
            this.schedualWrittenTest.Image = global::DVLD_System.Properties.Resources.Test_32;
            this.schedualWrittenTest.Name = "schedualWrittenTest";
            this.schedualWrittenTest.Size = new System.Drawing.Size(187, 22);
            this.schedualWrittenTest.Text = "Schedual Written Test";
            this.schedualWrittenTest.Click += new System.EventHandler(this.schedualWrittenTestToolStripMenuItem1_Click);
            // 
            // schedualStreetTest
            // 
            this.schedualStreetTest.Image = global::DVLD_System.Properties.Resources.Street_Test_32;
            this.schedualStreetTest.Name = "schedualStreetTest";
            this.schedualStreetTest.Size = new System.Drawing.Size(187, 22);
            this.schedualStreetTest.Text = "Schedual Street Test";
            this.schedualStreetTest.Click += new System.EventHandler(this.schedualStreetTestToolStripMenuItem1_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(258, 6);
            // 
            // issueDrivingLicenseFirstTime
            // 
            this.issueDrivingLicenseFirstTime.Image = global::DVLD_System.Properties.Resources.IssueDrivingLicense_32;
            this.issueDrivingLicenseFirstTime.Name = "issueDrivingLicenseFirstTime";
            this.issueDrivingLicenseFirstTime.Size = new System.Drawing.Size(261, 38);
            this.issueDrivingLicenseFirstTime.Text = "Issue Driving License (First Time)";
            this.issueDrivingLicenseFirstTime.Click += new System.EventHandler(this.issueDrivingLicenseFirstTime_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(258, 6);
            // 
            // showLicense
            // 
            this.showLicense.Image = global::DVLD_System.Properties.Resources.License_View_32;
            this.showLicense.Name = "showLicense";
            this.showLicense.Size = new System.Drawing.Size(261, 38);
            this.showLicense.Text = "Show License";
            this.showLicense.Click += new System.EventHandler(this.showLicense_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(258, 6);
            // 
            // showLicensePersonHistoryToolStripMenuItem
            // 
            this.showLicensePersonHistoryToolStripMenuItem.Image = global::DVLD_System.Properties.Resources.PersonLicenseHistory_32;
            this.showLicensePersonHistoryToolStripMenuItem.Name = "showLicensePersonHistoryToolStripMenuItem";
            this.showLicensePersonHistoryToolStripMenuItem.Size = new System.Drawing.Size(261, 38);
            this.showLicensePersonHistoryToolStripMenuItem.Text = "Show License Person History";
            this.showLicensePersonHistoryToolStripMenuItem.Click += new System.EventHandler(this.showLicensePersonHistoryToolStripMenuItem_Click);
            // 
            // txtFilterValue
            // 
            this.txtFilterValue.Location = new System.Drawing.Point(312, 215);
            this.txtFilterValue.Name = "txtFilterValue";
            this.txtFilterValue.Size = new System.Drawing.Size(218, 20);
            this.txtFilterValue.TabIndex = 9;
            this.txtFilterValue.TextChanged += new System.EventHandler(this.txtFilterValue_TextChanged);
            this.txtFilterValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilterValue_KeyPress);
            // 
            // cbApplicationStatus
            // 
            this.cbApplicationStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbApplicationStatus.FormattingEnabled = true;
            this.cbApplicationStatus.Items.AddRange(new object[] {
            "All",
            "New",
            "Canceled",
            "Completed"});
            this.cbApplicationStatus.Location = new System.Drawing.Point(312, 213);
            this.cbApplicationStatus.Name = "cbApplicationStatus";
            this.cbApplicationStatus.Size = new System.Drawing.Size(218, 21);
            this.cbApplicationStatus.TabIndex = 10;
            this.cbApplicationStatus.SelectedIndexChanged += new System.EventHandler(this.cbApplicationStatus_SelectedIndexChanged);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::DVLD_System.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1054, 534);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(88, 36);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_click);
            // 
            // btnAddNewApplication
            // 
            this.btnAddNewApplication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddNewApplication.Image = global::DVLD_System.Properties.Resources.New_Application_64;
            this.btnAddNewApplication.Location = new System.Drawing.Point(1054, 161);
            this.btnAddNewApplication.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAddNewApplication.Name = "btnAddNewApplication";
            this.btnAddNewApplication.Size = new System.Drawing.Size(79, 75);
            this.btnAddNewApplication.TabIndex = 4;
            this.btnAddNewApplication.UseVisualStyleBackColor = true;
            this.btnAddNewApplication.Click += new System.EventHandler(this.btnAddNewApplication_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DVLD_System.Properties.Resources.Local_32;
            this.pictureBox2.Location = new System.Drawing.Point(594, 46);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(40, 36);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD_System.Properties.Resources.Applications;
            this.pictureBox1.Location = new System.Drawing.Point(488, 16);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(124, 130);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblNumberOfRecords
            // 
            this.lblNumberOfRecords.AutoSize = true;
            this.lblNumberOfRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfRecords.Location = new System.Drawing.Point(119, 539);
            this.lblNumberOfRecords.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNumberOfRecords.Name = "lblNumberOfRecords";
            this.lblNumberOfRecords.Size = new System.Drawing.Size(45, 18);
            this.lblNumberOfRecords.TabIndex = 13;
            this.lblNumberOfRecords.Text = "? ? ?";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 539);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 20);
            this.label3.TabIndex = 12;
            this.label3.Text = "#Records : ";
            // 
            // frmLocalDrivingLicenseApplications
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1155, 574);
            this.Controls.Add(this.lblNumberOfRecords);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbApplicationStatus);
            this.Controls.Add(this.txtFilterValue);
            this.Controls.Add(this.dgvLocalDrivingLicenseApplicatios);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAddNewApplication);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmLocalDrivingLicenseApplications";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Local Driving License Applications";
            this.Load += new System.EventHandler(this.frmLocalDrivingLicenseApplications_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalDrivingLicenseApplicatios)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddNewApplication;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.DataGridView dgvLocalDrivingLicenseApplicatios;
        private System.Windows.Forms.TextBox txtFilterValue;
        private System.Windows.Forms.ComboBox cbApplicationStatus;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editApplication;
        private System.Windows.Forms.ToolStripMenuItem deleteApplication;
        private System.Windows.Forms.ToolStripMenuItem cancelApplication;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem schedualTest;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem issueDrivingLicenseFirstTime;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem showLicense;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem showLicensePersonHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem schedualVisionTest;
        private System.Windows.Forms.ToolStripMenuItem schedualWrittenTest;
        private System.Windows.Forms.ToolStripMenuItem schedualStreetTest;
        private System.Windows.Forms.Label lblNumberOfRecords;
        private System.Windows.Forms.Label label3;
    }
}