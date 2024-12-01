namespace DVLD_System.WrittenTest
{
    partial class frmWrittenTest
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
            this.btnNext = new System.Windows.Forms.Button();
            this.gpQuestionBox = new System.Windows.Forms.GroupBox();
            this.btnStartTest = new System.Windows.Forms.Button();
            this.lblConditionLabel = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblTimerCount = new System.Windows.Forms.Label();
            this.ctrlQuestion1 = new DVLD_System.Tests.WrittenTest.Controls.ctrlQuestion();
            this.ctrlScheduledTest1 = new DVLD_System.ctrlScheduledTest();
            this.gpQuestionBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Titillium Web", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(634, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "Written Test Questions";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Image = global::DVLD_System.Properties.Resources.Next_32;
            this.btnNext.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNext.Location = new System.Drawing.Point(548, 353);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(92, 40);
            this.btnNext.TabIndex = 3;
            this.btnNext.Text = "Next";
            this.btnNext.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // gpQuestionBox
            // 
            this.gpQuestionBox.Controls.Add(this.ctrlQuestion1);
            this.gpQuestionBox.Controls.Add(this.label1);
            this.gpQuestionBox.Controls.Add(this.btnNext);
            this.gpQuestionBox.Location = new System.Drawing.Point(506, 32);
            this.gpQuestionBox.Name = "gpQuestionBox";
            this.gpQuestionBox.Size = new System.Drawing.Size(652, 421);
            this.gpQuestionBox.TabIndex = 4;
            this.gpQuestionBox.TabStop = false;
            // 
            // btnStartTest
            // 
            this.btnStartTest.BackColor = System.Drawing.Color.DarkRed;
            this.btnStartTest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStartTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartTest.Font = new System.Drawing.Font("Titillium Web", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartTest.ForeColor = System.Drawing.Color.Transparent;
            this.btnStartTest.Location = new System.Drawing.Point(714, 163);
            this.btnStartTest.Name = "btnStartTest";
            this.btnStartTest.Size = new System.Drawing.Size(260, 57);
            this.btnStartTest.TabIndex = 4;
            this.btnStartTest.Text = "Start Test";
            this.btnStartTest.UseVisualStyleBackColor = false;
            this.btnStartTest.Click += new System.EventHandler(this.btnStartTest_Click);
            // 
            // lblConditionLabel
            // 
            this.lblConditionLabel.Font = new System.Drawing.Font("Titillium Web", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConditionLabel.Location = new System.Drawing.Point(613, 75);
            this.lblConditionLabel.Name = "lblConditionLabel";
            this.lblConditionLabel.Size = new System.Drawing.Size(462, 68);
            this.lblConditionLabel.TabIndex = 5;
            this.lblConditionLabel.Text = "You Have 40 Question To Answer Them, The Minimum Question Number To Pass This Tes" +
    "t Is 35 OF 40 Question  ";
            this.lblConditionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblTimerCount
            // 
            this.lblTimerCount.AutoSize = true;
            this.lblTimerCount.Font = new System.Drawing.Font("Titillium Web", 71.99999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimerCount.Location = new System.Drawing.Point(769, 139);
            this.lblTimerCount.Name = "lblTimerCount";
            this.lblTimerCount.Size = new System.Drawing.Size(116, 146);
            this.lblTimerCount.TabIndex = 6;
            this.lblTimerCount.Text = "1";
            // 
            // ctrlQuestion1
            // 
            this.ctrlQuestion1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ctrlQuestion1.Location = new System.Drawing.Point(6, 51);
            this.ctrlQuestion1.Name = "ctrlQuestion1";
            this.ctrlQuestion1.Size = new System.Drawing.Size(634, 296);
            this.ctrlQuestion1.TabIndex = 2;
            // 
            // ctrlScheduledTest1
            // 
            this.ctrlScheduledTest1.Location = new System.Drawing.Point(11, 21);
            this.ctrlScheduledTest1.Margin = new System.Windows.Forms.Padding(2);
            this.ctrlScheduledTest1.Name = "ctrlScheduledTest1";
            this.ctrlScheduledTest1.Size = new System.Drawing.Size(490, 446);
            this.ctrlScheduledTest1.TabIndex = 0;
            this.ctrlScheduledTest1.TestType = DVLD_Bussiness.clsTestType.enTestType.enWrittinTest;
            // 
            // frmWrittenTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1161, 465);
            this.Controls.Add(this.gpQuestionBox);
            this.Controls.Add(this.ctrlScheduledTest1);
            this.Controls.Add(this.btnStartTest);
            this.Controls.Add(this.lblConditionLabel);
            this.Controls.Add(this.lblTimerCount);
            this.Name = "frmWrittenTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Written Test";
            this.Load += new System.EventHandler(this.frmWrittenTest_Load);
            this.gpQuestionBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlScheduledTest ctrlScheduledTest1;
        private System.Windows.Forms.Label label1;
        private Tests.WrittenTest.Controls.ctrlQuestion ctrlQuestion1;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.GroupBox gpQuestionBox;
        private System.Windows.Forms.Button btnStartTest;
        private System.Windows.Forms.Label lblConditionLabel;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblTimerCount;
    }
}