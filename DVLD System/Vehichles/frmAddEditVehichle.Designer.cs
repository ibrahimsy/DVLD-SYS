namespace DVLD_System.Vehichles
{
    partial class frmAddEditVehichle
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
            this.tpOwnerInfo = new System.Windows.Forms.TabPage();
            this.btnNext = new System.Windows.Forms.Button();
            this.ctrlPersonCardWithFilter1 = new DVLD_System.Controls.ctrlPersonCardWithFilter();
            this.tpVehichleInfo = new System.Windows.Forms.TabPage();
            this.lblVehichleID = new System.Windows.Forms.Label();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.txtPlateNumber = new System.Windows.Forms.TextBox();
            this.txtChassisNumber = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.lblOwnerFullName = new System.Windows.Forms.Label();
            this.lblCreatedBy = new System.Windows.Forms.Label();
            this.cbMake = new System.Windows.Forms.ComboBox();
            this.cbModel = new System.Windows.Forms.ComboBox();
            this.cbSubModel = new System.Windows.Forms.ComboBox();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.tabControl1.SuspendLayout();
            this.tpOwnerInfo.SuspendLayout();
            this.tpVehichleInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpOwnerInfo);
            this.tabControl1.Controls.Add(this.tpVehichleInfo);
            this.tabControl1.Location = new System.Drawing.Point(15, 52);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(981, 477);
            this.tabControl1.TabIndex = 3;
            // 
            // tpOwnerInfo
            // 
            this.tpOwnerInfo.Controls.Add(this.btnNext);
            this.tpOwnerInfo.Controls.Add(this.ctrlPersonCardWithFilter1);
            this.tpOwnerInfo.Location = new System.Drawing.Point(4, 22);
            this.tpOwnerInfo.Name = "tpOwnerInfo";
            this.tpOwnerInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpOwnerInfo.Size = new System.Drawing.Size(973, 451);
            this.tpOwnerInfo.TabIndex = 0;
            this.tpOwnerInfo.Text = "Owner Info";
            this.tpOwnerInfo.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Image = global::DVLD_System.Properties.Resources.Next_32;
            this.btnNext.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNext.Location = new System.Drawing.Point(855, 410);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(101, 38);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "Next";
            this.btnNext.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNext.UseVisualStyleBackColor = true;
            // 
            // ctrlPersonCardWithFilter1
            // 
            this.ctrlPersonCardWithFilter1.EnableFilter = true;
            this.ctrlPersonCardWithFilter1.Location = new System.Drawing.Point(13, 6);
            this.ctrlPersonCardWithFilter1.Name = "ctrlPersonCardWithFilter1";
            this.ctrlPersonCardWithFilter1.Size = new System.Drawing.Size(943, 398);
            this.ctrlPersonCardWithFilter1.TabIndex = 0;
            // 
            // tpVehichleInfo
            // 
            this.tpVehichleInfo.Controls.Add(this.cbType);
            this.tpVehichleInfo.Controls.Add(this.cbSubModel);
            this.tpVehichleInfo.Controls.Add(this.cbModel);
            this.tpVehichleInfo.Controls.Add(this.cbMake);
            this.tpVehichleInfo.Controls.Add(this.lblCreatedBy);
            this.tpVehichleInfo.Controls.Add(this.lblOwnerFullName);
            this.tpVehichleInfo.Controls.Add(this.txtColor);
            this.tpVehichleInfo.Controls.Add(this.label11);
            this.tpVehichleInfo.Controls.Add(this.label10);
            this.tpVehichleInfo.Controls.Add(this.label9);
            this.tpVehichleInfo.Controls.Add(this.label8);
            this.tpVehichleInfo.Controls.Add(this.label7);
            this.tpVehichleInfo.Controls.Add(this.label6);
            this.tpVehichleInfo.Controls.Add(this.label1);
            this.tpVehichleInfo.Controls.Add(this.lblVehichleID);
            this.tpVehichleInfo.Controls.Add(this.txtYear);
            this.tpVehichleInfo.Controls.Add(this.txtPlateNumber);
            this.tpVehichleInfo.Controls.Add(this.txtChassisNumber);
            this.tpVehichleInfo.Controls.Add(this.label5);
            this.tpVehichleInfo.Controls.Add(this.label4);
            this.tpVehichleInfo.Controls.Add(this.label3);
            this.tpVehichleInfo.Controls.Add(this.label2);
            this.tpVehichleInfo.Location = new System.Drawing.Point(4, 22);
            this.tpVehichleInfo.Name = "tpVehichleInfo";
            this.tpVehichleInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpVehichleInfo.Size = new System.Drawing.Size(973, 451);
            this.tpVehichleInfo.TabIndex = 1;
            this.tpVehichleInfo.Text = "Vehichle Info";
            this.tpVehichleInfo.UseVisualStyleBackColor = true;
            // 
            // lblVehichleID
            // 
            this.lblVehichleID.AutoSize = true;
            this.lblVehichleID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVehichleID.Location = new System.Drawing.Point(222, 45);
            this.lblVehichleID.Name = "lblVehichleID";
            this.lblVehichleID.Size = new System.Drawing.Size(89, 20);
            this.lblVehichleID.TabIndex = 8;
            this.lblVehichleID.Text = "[? ? ? ? ?]";
            // 
            // txtYear
            // 
            this.txtYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtYear.Font = new System.Drawing.Font("Titillium Web", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtYear.Location = new System.Drawing.Point(671, 194);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(185, 32);
            this.txtYear.TabIndex = 7;
            // 
            // txtPlateNumber
            // 
            this.txtPlateNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPlateNumber.Font = new System.Drawing.Font("Titillium Web", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlateNumber.Location = new System.Drawing.Point(226, 148);
            this.txtPlateNumber.Name = "txtPlateNumber";
            this.txtPlateNumber.Size = new System.Drawing.Size(185, 32);
            this.txtPlateNumber.TabIndex = 6;
            // 
            // txtChassisNumber
            // 
            this.txtChassisNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtChassisNumber.Font = new System.Drawing.Font("Titillium Web", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChassisNumber.Location = new System.Drawing.Point(226, 103);
            this.txtChassisNumber.Name = "txtChassisNumber";
            this.txtChassisNumber.Size = new System.Drawing.Size(185, 32);
            this.txtChassisNumber.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Titillium Web SemiBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(143, 195);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 29);
            this.label5.TabIndex = 3;
            this.label5.Text = "Make :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Titillium Web SemiBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(78, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 29);
            this.label4.TabIndex = 2;
            this.label4.Text = "Plate Number :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Titillium Web SemiBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(56, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 29);
            this.label3.TabIndex = 1;
            this.label3.Text = "Chassis Number :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Titillium Web SemiBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(93, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 29);
            this.label2.TabIndex = 0;
            this.label2.Text = "Vehichle ID :";
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Titillium Web", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTitle.Location = new System.Drawing.Point(27, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(965, 40);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Register Vehichle";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::DVLD_System.Properties.Resources.Close_321;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(790, 535);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(101, 38);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::DVLD_System.Properties.Resources.Save_32;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(897, 535);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(101, 38);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Titillium Web SemiBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(138, 239);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 29);
            this.label1.TabIndex = 9;
            this.label1.Text = "Model :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Titillium Web SemiBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(106, 288);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 29);
            this.label6.TabIndex = 10;
            this.label6.Text = "SubModel :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Titillium Web SemiBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(586, 149);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 29);
            this.label7.TabIndex = 11;
            this.label7.Text = "Type :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Titillium Web SemiBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(488, 103);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(163, 29);
            this.label8.TabIndex = 12;
            this.label8.Text = "Owner Full Name :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Titillium Web SemiBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(586, 195);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 29);
            this.label9.TabIndex = 13;
            this.label9.Text = "Year :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Titillium Web SemiBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(582, 241);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 29);
            this.label10.TabIndex = 14;
            this.label10.Text = "Color :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Titillium Web SemiBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(535, 287);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(111, 29);
            this.label11.TabIndex = 15;
            this.label11.Text = "Created By :";
            // 
            // txtColor
            // 
            this.txtColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtColor.Font = new System.Drawing.Font("Titillium Web", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtColor.Location = new System.Drawing.Point(671, 243);
            this.txtColor.Name = "txtColor";
            this.txtColor.Size = new System.Drawing.Size(185, 32);
            this.txtColor.TabIndex = 16;
            // 
            // lblOwnerFullName
            // 
            this.lblOwnerFullName.AutoSize = true;
            this.lblOwnerFullName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOwnerFullName.Location = new System.Drawing.Point(667, 107);
            this.lblOwnerFullName.Name = "lblOwnerFullName";
            this.lblOwnerFullName.Size = new System.Drawing.Size(89, 20);
            this.lblOwnerFullName.TabIndex = 17;
            this.lblOwnerFullName.Text = "[? ? ? ? ?]";
            // 
            // lblCreatedBy
            // 
            this.lblCreatedBy.AutoSize = true;
            this.lblCreatedBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreatedBy.Location = new System.Drawing.Point(667, 294);
            this.lblCreatedBy.Name = "lblCreatedBy";
            this.lblCreatedBy.Size = new System.Drawing.Size(89, 20);
            this.lblCreatedBy.TabIndex = 18;
            this.lblCreatedBy.Text = "[? ? ? ? ?]";
            // 
            // cbMake
            // 
            this.cbMake.Font = new System.Drawing.Font("Titillium Web", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMake.FormattingEnabled = true;
            this.cbMake.Location = new System.Drawing.Point(226, 193);
            this.cbMake.Name = "cbMake";
            this.cbMake.Size = new System.Drawing.Size(185, 32);
            this.cbMake.TabIndex = 19;
            // 
            // cbModel
            // 
            this.cbModel.Font = new System.Drawing.Font("Titillium Web", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbModel.FormattingEnabled = true;
            this.cbModel.Location = new System.Drawing.Point(226, 238);
            this.cbModel.Name = "cbModel";
            this.cbModel.Size = new System.Drawing.Size(185, 32);
            this.cbModel.TabIndex = 20;
            // 
            // cbSubModel
            // 
            this.cbSubModel.Font = new System.Drawing.Font("Titillium Web", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSubModel.FormattingEnabled = true;
            this.cbSubModel.Location = new System.Drawing.Point(226, 283);
            this.cbSubModel.Name = "cbSubModel";
            this.cbSubModel.Size = new System.Drawing.Size(185, 32);
            this.cbSubModel.TabIndex = 21;
            // 
            // cbType
            // 
            this.cbType.Font = new System.Drawing.Font("Titillium Web", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbType.FormattingEnabled = true;
            this.cbType.Location = new System.Drawing.Point(671, 145);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(185, 32);
            this.cbType.TabIndex = 22;
            // 
            // frmAddEditVehichle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 580);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmAddEditVehichle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Edit Vehichle";
            this.tabControl1.ResumeLayout(false);
            this.tpOwnerInfo.ResumeLayout(false);
            this.tpVehichleInfo.ResumeLayout(false);
            this.tpVehichleInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpOwnerInfo;
        private System.Windows.Forms.Button btnNext;
        private Controls.ctrlPersonCardWithFilter ctrlPersonCardWithFilter1;
        private System.Windows.Forms.TabPage tpVehichleInfo;
        private System.Windows.Forms.Label lblVehichleID;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.TextBox txtPlateNumber;
        private System.Windows.Forms.TextBox txtChassisNumber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.ComboBox cbSubModel;
        private System.Windows.Forms.ComboBox cbModel;
        private System.Windows.Forms.ComboBox cbMake;
        private System.Windows.Forms.Label lblCreatedBy;
        private System.Windows.Forms.Label lblOwnerFullName;
        private System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
    }
}