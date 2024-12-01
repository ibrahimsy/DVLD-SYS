namespace DVLD_System.Tests.WrittenTest.Controls
{
    partial class ctrlQuestion
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
            this.lblQuestion = new System.Windows.Forms.Label();
            this.rbOption1 = new System.Windows.Forms.RadioButton();
            this.rbOption2 = new System.Windows.Forms.RadioButton();
            this.rbOption3 = new System.Windows.Forms.RadioButton();
            this.rbOption4 = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // lblQuestion
            // 
            this.lblQuestion.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestion.Location = new System.Drawing.Point(3, 10);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new System.Drawing.Size(622, 51);
            this.lblQuestion.TabIndex = 0;
            this.lblQuestion.Text = "Question";
            this.lblQuestion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rbOption1
            // 
            this.rbOption1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbOption1.AutoSize = true;
            this.rbOption1.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbOption1.Location = new System.Drawing.Point(502, 89);
            this.rbOption1.Name = "rbOption1";
            this.rbOption1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rbOption1.Size = new System.Drawing.Size(101, 27);
            this.rbOption1.TabIndex = 1;
            this.rbOption1.TabStop = true;
            this.rbOption1.Tag = "1";
            this.rbOption1.Text = "Option 1";
            this.rbOption1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbOption1.UseVisualStyleBackColor = true;
            this.rbOption1.CheckedChanged += new System.EventHandler(this.rbSelectedOption_CheckedChanged);
            // 
            // rbOption2
            // 
            this.rbOption2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbOption2.AutoSize = true;
            this.rbOption2.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbOption2.Location = new System.Drawing.Point(502, 134);
            this.rbOption2.Name = "rbOption2";
            this.rbOption2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rbOption2.Size = new System.Drawing.Size(101, 27);
            this.rbOption2.TabIndex = 2;
            this.rbOption2.TabStop = true;
            this.rbOption2.Tag = "2";
            this.rbOption2.Text = "Option 2";
            this.rbOption2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbOption2.UseVisualStyleBackColor = true;
            this.rbOption2.CheckedChanged += new System.EventHandler(this.rbSelectedOption_CheckedChanged);
            // 
            // rbOption3
            // 
            this.rbOption3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbOption3.AutoSize = true;
            this.rbOption3.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbOption3.Location = new System.Drawing.Point(502, 179);
            this.rbOption3.Name = "rbOption3";
            this.rbOption3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rbOption3.Size = new System.Drawing.Size(101, 27);
            this.rbOption3.TabIndex = 3;
            this.rbOption3.TabStop = true;
            this.rbOption3.Tag = "3";
            this.rbOption3.Text = "Option 3";
            this.rbOption3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbOption3.UseVisualStyleBackColor = true;
            this.rbOption3.CheckedChanged += new System.EventHandler(this.rbSelectedOption_CheckedChanged);
            // 
            // rbOption4
            // 
            this.rbOption4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbOption4.AutoSize = true;
            this.rbOption4.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbOption4.Location = new System.Drawing.Point(502, 224);
            this.rbOption4.Name = "rbOption4";
            this.rbOption4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rbOption4.Size = new System.Drawing.Size(101, 27);
            this.rbOption4.TabIndex = 4;
            this.rbOption4.TabStop = true;
            this.rbOption4.Tag = "4";
            this.rbOption4.Text = "Option 4";
            this.rbOption4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbOption4.UseVisualStyleBackColor = true;
            this.rbOption4.CheckedChanged += new System.EventHandler(this.rbSelectedOption_CheckedChanged);
            // 
            // ctrlQuestion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Controls.Add(this.rbOption4);
            this.Controls.Add(this.rbOption3);
            this.Controls.Add(this.rbOption2);
            this.Controls.Add(this.rbOption1);
            this.Controls.Add(this.lblQuestion);
            this.Name = "ctrlQuestion";
            this.Size = new System.Drawing.Size(628, 280);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblQuestion;
        private System.Windows.Forms.RadioButton rbOption1;
        private System.Windows.Forms.RadioButton rbOption2;
        private System.Windows.Forms.RadioButton rbOption3;
        private System.Windows.Forms.RadioButton rbOption4;
    }
}
