namespace DVLD_System.Vehicles.Controls
{
    partial class ctrlVehicleInfoWithFilter
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
            this.ctrlVehicleInfo1 = new DVLD_System.Vehicles.Controls.ctrlVehicleInfo();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFilterValue = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.gpFilterBy = new System.Windows.Forms.GroupBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.gpFilterBy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // ctrlVehicleInfo1
            // 
            this.ctrlVehicleInfo1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ctrlVehicleInfo1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlVehicleInfo1.Location = new System.Drawing.Point(3, 58);
            this.ctrlVehicleInfo1.Name = "ctrlVehicleInfo1";
            this.ctrlVehicleInfo1.Size = new System.Drawing.Size(849, 353);
            this.ctrlVehicleInfo1.TabIndex = 0;
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterBy.Font = new System.Drawing.Font("Titillium Web", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Items.AddRange(new object[] {
            "Vehicle ID",
            "Chassis Number",
            "Plate Number"});
            this.cbFilterBy.Location = new System.Drawing.Point(114, 15);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(217, 31);
            this.cbFilterBy.TabIndex = 1;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Titillium Web", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = "Filter By :";
            // 
            // txtFilterValue
            // 
            this.txtFilterValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFilterValue.Font = new System.Drawing.Font("Titillium Web", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilterValue.Location = new System.Drawing.Point(337, 14);
            this.txtFilterValue.Name = "txtFilterValue";
            this.txtFilterValue.Size = new System.Drawing.Size(216, 32);
            this.txtFilterValue.TabIndex = 3;
            this.txtFilterValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilterValue_KeyPress);
            this.txtFilterValue.Validating += new System.ComponentModel.CancelEventHandler(this.txtFilterValue_Validating);
            // 
            // btnSearch
            // 
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Titillium Web", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(559, 13);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(82, 33);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // gpFilterBy
            // 
            this.gpFilterBy.Controls.Add(this.cbFilterBy);
            this.gpFilterBy.Controls.Add(this.btnSearch);
            this.gpFilterBy.Controls.Add(this.label1);
            this.gpFilterBy.Controls.Add(this.txtFilterValue);
            this.gpFilterBy.Location = new System.Drawing.Point(3, 3);
            this.gpFilterBy.Name = "gpFilterBy";
            this.gpFilterBy.Size = new System.Drawing.Size(849, 53);
            this.gpFilterBy.TabIndex = 5;
            this.gpFilterBy.TabStop = false;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ctrlVehicleInfoWithFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.gpFilterBy);
            this.Controls.Add(this.ctrlVehicleInfo1);
            this.Name = "ctrlVehicleInfoWithFilter";
            this.Size = new System.Drawing.Size(855, 415);
            this.gpFilterBy.ResumeLayout(false);
            this.gpFilterBy.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlVehicleInfo ctrlVehicleInfo1;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFilterValue;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.GroupBox gpFilterBy;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
