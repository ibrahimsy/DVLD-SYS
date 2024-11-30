using DVLD_Bussiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_System.Tests.WrittenTest.Controls
{
    public partial class ctrlQuestion : UserControl
    {
        int _QuestionID = -1;

        private int _SelectedOptionID = -1;

        clsQuestion _QuestionInfo;
        public clsQuestion QuestionInfo
        {
            get
            {
                return _QuestionInfo;
            }
        }

        public int SelectedOptionID
        {
            get
            {
                return _SelectedOptionID;
            }
        }
        public ctrlQuestion()
        {
            InitializeComponent();
            rbOption1.Checked = true;
        }

        public void LoadInfo(int QuestionID)
        {
            _QuestionID = QuestionID;

            _QuestionInfo = clsQuestion.Find(QuestionID);

            if(_QuestionInfo == null)
            {
                MessageBox.Show($"No Question Found By ID = [{_QuestionID}]","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            lblQuestion.Text = QuestionInfo.Question +" - ("+QuestionID.ToString()+")";
            
            rbOption1.Text = QuestionInfo.Option1;
            rbOption2.Text = QuestionInfo.Option2;
            rbOption3.Text = QuestionInfo.Option3;
            rbOption4.Text = QuestionInfo.Option4;

            rbOption1.Checked = true;
        }

        private void rbSelectedOption_CheckedChanged(object sender, EventArgs e)
        {
            _SelectedOptionID = Convert.ToInt32(((RadioButton)sender).Tag);
        }
    }
}
