namespace DVLD_System.VehicleLicenses.Controls
{
    partial class ctrlVehicleLcenseWithFilter
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtVehicleLicenseID = new System.Windows.Forms.TextBox();
            this.gbVehicleLicenseFilter = new System.Windows.Forms.GroupBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnFindLicense = new System.Windows.Forms.Button();
            this.ctrlVehicleLicense1 = new DVLD_System.VehicleLicenses.Controls.ctrlVehicleLicense();
            this.label1 = new System.Windows.Forms.Label();
            this.gbVehicleLicenseFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtVehicleLicenseID
            // 
            this.txtVehicleLicenseID.Font = new System.Drawing.Font("Titillium Web", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVehicleLicenseID.Location = new System.Drawing.Point(176, 15);
            this.txtVehicleLicenseID.Name = "txtVehicleLicenseID";
            this.txtVehicleLicenseID.Size = new System.Drawing.Size(199, 36);
            this.txtVehicleLicenseID.TabIndex = 0;
            this.txtVehicleLicenseID.TextChanged += new System.EventHandler(this.txtVehicleLicenseID_TextChanged);
            this.txtVehicleLicenseID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVehicleLicenseID_KeyPress);
            this.txtVehicleLicenseID.Validating += new System.ComponentModel.CancelEventHandler(this.txtVehicleLicenseID_Validating);
            // 
            // gbVehicleLicenseFilter
            // 
            this.gbVehicleLicenseFilter.Controls.Add(this.label1);
            this.gbVehicleLicenseFilter.Controls.Add(this.btnFindLicense);
            this.gbVehicleLicenseFilter.Controls.Add(this.txtVehicleLicenseID);
            this.gbVehicleLicenseFilter.Location = new System.Drawing.Point(15, 3);
            this.gbVehicleLicenseFilter.Name = "gbVehicleLicenseFilter";
            this.gbVehicleLicenseFilter.Size = new System.Drawing.Size(463, 64);
            this.gbVehicleLicenseFilter.TabIndex = 19;
            this.gbVehicleLicenseFilter.TabStop = false;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // btnFindLicense
            // 
            this.btnFindLicense.Image = global::DVLD_System.Properties.Resources.VehicleLicense;
            this.btnFindLicense.Location = new System.Drawing.Point(381, 12);
            this.btnFindLicense.Name = "btnFindLicense";
            this.btnFindLicense.Size = new System.Drawing.Size(67, 46);
            this.btnFindLicense.TabIndex = 1;
            this.btnFindLicense.UseVisualStyleBackColor = true;
            this.btnFindLicense.Click += new System.EventHandler(this.btnFindLicense_Click);
            // 
            // ctrlVehicleLicense1
            // 
            this.ctrlVehicleLicense1.BackColor = System.Drawing.Color.OldLace;
            this.ctrlVehicleLicense1.Location = new System.Drawing.Point(15, 73);
            this.ctrlVehicleLicense1.Name = "ctrlVehicleLicense1";
            this.ctrlVehicleLicense1.Size = new System.Drawing.Size(718, 283);
            this.ctrlVehicleLicense1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Titillium Web", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = "Vehicle License ID";
            // 
            // ctrlVehicleLcenseWithFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbVehicleLicenseFilter);
            this.Controls.Add(this.ctrlVehicleLicense1);
            this.Name = "ctrlVehicleLcenseWithFilter";
            this.Size = new System.Drawing.Size(754, 375);
            this.gbVehicleLicenseFilter.ResumeLayout(false);
            this.gbVehicleLicenseFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtVehicleLicenseID;
        private System.Windows.Forms.Button btnFindLicense;
        private ctrlVehicleLicense ctrlVehicleLicense1;
        private System.Windows.Forms.GroupBox gbVehicleLicenseFilter;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label1;
    }
}
