using DVLD_Bussiness;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD_System.WrittenTest
{
    public partial class frmWrittenTest : Form
    {
        int _AppointmentID = -1;
        DataTable _dtQuestionList;
        int _CurrentQuestionIndex = 1;
        int _PassedQuestionCount = 0;
        clsTestType.enTestType _TestType = clsTestType.enTestType.enWrittinTest;
        public frmWrittenTest(int AppointmentID)
        {
            InitializeComponent();
            _AppointmentID = AppointmentID;
            _dtQuestionList = clsQuestion.GetAllQuestiones();
        }

        private void frmWrittenTest_Load(object sender, EventArgs e)
        {
            ctrlScheduledTest1.TestType = _TestType;
            ctrlScheduledTest1.LoadInfo(_AppointmentID);
            ctrlQuestion1.LoadInfo(_CurrentQuestionIndex);
        }
        
        private void _DisplayNextQuestion()
        {
           
            if (_CurrentQuestionIndex <= _dtQuestionList.Rows.Count)
            {
                ctrlQuestion1.LoadInfo(_CurrentQuestionIndex);
                
                if (ctrlQuestion1.SelectedOptionID == ctrlQuestion1.QuestionInfo.CorrectAnswerID)
                {
                    _PassedQuestionCount++;
                }

                _CurrentQuestionIndex++;
            }
            else
            {
                btnNext.Enabled = false;
                MessageBox.Show($"Test Finished With Result [{_PassedQuestionCount}]/40","",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }
        
        private void btnNext_Click(object sender, EventArgs e)
        {
            _DisplayNextQuestion();
        }
    }
}
