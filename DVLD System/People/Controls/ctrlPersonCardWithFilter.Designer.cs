namespace DVLD_System.Controls
{
    partial class ctrlPersonCardWithFilter
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.txtFilterValue = new System.Windows.Forms.TextBox();
            this.btnFindPerson = new System.Windows.Forms.Button();
            this.ctrlPersonCard1 = new DVLD_System.Controls.ctrlPersonCard();
            this.btnAddPerson = new System.Windows.Forms.Button();
            this.gpFilterBy = new System.Windows.Forms.GroupBox();
            this.gpFilterBy.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(27, 52);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 39);
            this.label1.TabIndex = 1;
            this.label1.Text = "Filter By";
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterBy.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Location = new System.Drawing.Point(193, 45);
            this.cbFilterBy.Margin = new System.Windows.Forms.Padding(6);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(356, 44);
            this.cbFilterBy.TabIndex = 2;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            // 
            // txtFilterValue
            // 
            this.txtFilterValue.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilterValue.Location = new System.Drawing.Point(565, 45);
            this.txtFilterValue.Margin = new System.Windows.Forms.Padding(6);
            this.txtFilterValue.Name = "txtFilterValue";
            this.txtFilterValue.Size = new System.Drawing.Size(378, 46);
            this.txtFilterValue.TabIndex = 3;
            this.txtFilterValue.TextChanged += new System.EventHandler(this.txtFilterValue_TextChanged);
            this.txtFilterValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilterValue_KeyPress);
            // 
            // btnFindPerson
            // 
            this.btnFindPerson.BackgroundImage = global::DVLD_System.Properties.Resources.person_boy__1_;
            this.btnFindPerson.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFindPerson.Enabled = false;
            this.btnFindPerson.Location = new System.Drawing.Point(967, 43);
            this.btnFindPerson.Margin = new System.Windows.Forms.Padding(6);
            this.btnFindPerson.Name = "btnFindPerson";
            this.btnFindPerson.Size = new System.Drawing.Size(66, 56);
            this.btnFindPerson.TabIndex = 4;
            this.btnFindPerson.UseVisualStyleBackColor = true;
            this.btnFindPerson.Click += new System.EventHandler(this.btnFindPerson_Click);
            // 
            // ctrlPersonCard1
            // 
            this.ctrlPersonCard1.BackColor = System.Drawing.Color.White;
            this.ctrlPersonCard1.Location = new System.Drawing.Point(28, 150);
            this.ctrlPersonCard1.Margin = new System.Windows.Forms.Padding(12);
            this.ctrlPersonCard1.Name = "ctrlPersonCard1";
            this.ctrlPersonCard1.Size = new System.Drawing.Size(1824, 633);
            this.ctrlPersonCard1.TabIndex = 5;
            // 
            // btnAddPerson
            // 
            this.btnAddPerson.BackgroundImage = global::DVLD_System.Properties.Resources.user__1_;
            this.btnAddPerson.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddPerson.Location = new System.Drawing.Point(1045, 43);
            this.btnAddPerson.Margin = new System.Windows.Forms.Padding(6);
            this.btnAddPerson.Name = "btnAddPerson";
            this.btnAddPerson.Size = new System.Drawing.Size(66, 56);
            this.btnAddPerson.TabIndex = 6;
            this.btnAddPerson.UseVisualStyleBackColor = true;
            this.btnAddPerson.Click += new System.EventHandler(this.btnAddPerson_Click);
            // 
            // gpFilterBy
            // 
            this.gpFilterBy.Controls.Add(this.txtFilterValue);
            this.gpFilterBy.Controls.Add(this.btnAddPerson);
            this.gpFilterBy.Controls.Add(this.label1);
            this.gpFilterBy.Controls.Add(this.cbFilterBy);
            this.gpFilterBy.Controls.Add(this.btnFindPerson);
            this.gpFilterBy.Location = new System.Drawing.Point(28, 8);
            this.gpFilterBy.Name = "gpFilterBy";
            this.gpFilterBy.Size = new System.Drawing.Size(1824, 127);
            this.gpFilterBy.TabIndex = 7;
            this.gpFilterBy.TabStop = false;
            // 
            // ctrlPersonCardWithFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gpFilterBy);
            this.Controls.Add(this.ctrlPersonCard1);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "ctrlPersonCardWithFilter";
            this.Size = new System.Drawing.Size(1886, 812);
            this.Load += new System.EventHandler(this.ctrlPersonCardWithFilter_Load);
            this.gpFilterBy.ResumeLayout(false);
            this.gpFilterBy.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.TextBox txtFilterValue;
        private System.Windows.Forms.Button btnFindPerson;
        private ctrlPersonCard ctrlPersonCard1;
        private System.Windows.Forms.Button btnAddPerson;
        private System.Windows.Forms.GroupBox gpFilterBy;
    }
}
