using BankBussiness;
using DVLD_Bussiness;
using DVLD_System.Tests.WrittenTest.Controls;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD_System.WrittenTest
{
    public partial class frmWrittenTest : Form
    {
        int _AppointmentID = -1;
        int _TestID = -1;
        clsTest _Test;
        DataTable _dtQuestionList;
        int _CurrentQuestionIndex = 1;
        int _PassedQuestionCount = 0;
        int CounterLabel = 0;
        bool IsPassed = false;
        int PassingScore = clsSetting.FindSettingByID((int)clsSetting.enSettings.PassingScore).SettingValue;
        int QuestionPerExam = clsSetting.FindSettingByID((int)clsSetting.enSettings.QuestionsPerExam).SettingValue;

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
            _TestID = ctrlScheduledTest1.TestID;

            if (_TestID != -1)
            {
                _Test = clsTest.Find(_TestID);
                btnStartTest.Enabled = false;
            }
            else
                _Test = new clsTest();

            gpQuestionBox.Visible = false;
            lblTimerCount.Visible = false;

            lblConditionLabel.Text = $"You Have {QuestionPerExam} Question To Answer Them, The Minimum Question Number To Pass This Test Is {PassingScore} OF {QuestionPerExam} Question";
        }
        
        private void SaveTestInfo()
        {
            _Test.TestAppointmentID = _AppointmentID;
            _Test.Notes = "";
            _Test.TestResult = IsPassed;
            _Test.CreatedByUserID = clsGlobalSettings.CurrentUser.UserID;

            if (_Test.Save())
            {
                MessageBox.Show("Test Was Saved Successfuly", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("An Error Occurred", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Close();
        }
        
        private void _DisplayQuestion()
        {
            if (_CurrentQuestionIndex <= _dtQuestionList.Rows.Count)
            {
                ctrlQuestion1.ClearSelection();
                ctrlQuestion1.LoadInfo(Convert.ToInt32(_dtQuestionList.Rows[_CurrentQuestionIndex - 1]["QuestionID"]), _CurrentQuestionIndex);
            }
            else
            {
                btnNext.Enabled = false;
                
                if (_PassedQuestionCount < PassingScore)
                {
                    IsPassed = false;
                    MessageBox.Show($"Exam Finished: You Faild,\n\n Number Of Correct Answer Is [{_PassedQuestionCount}]/{QuestionPerExam}", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    IsPassed = true;
                    MessageBox.Show($"Congratulations ,You Passed The Test ,\n\n Number Of Correct Answer Is [{_PassedQuestionCount}]/{QuestionPerExam}","",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }

                SaveTestInfo();
            }
        }
        
        private void btnNext_Click(object sender, EventArgs e)
        {

            if(!ctrlQuestion1.IsAnswered())
            {
                MessageBox.Show("Please Choose An Answer Before Proceeding!");
                return;
            }
            if (ctrlQuestion1.SelectedOptionID == ctrlQuestion1.QuestionInfo.CorrectAnswerID)
            {
                _PassedQuestionCount++;
            }

            _CurrentQuestionIndex++;
            _DisplayQuestion();
        }

        private void btnStartTest_Click(object sender, EventArgs e)
        {
            
            timer1.Enabled = true;
            lblTimerCount.Visible=true;
            btnStartTest.Visible = false;
            lblConditionLabel.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CounterLabel++;
            if (CounterLabel == 4)
            {
                gpQuestionBox.Visible = true;
                timer1.Enabled=false;
                lblTimerCount.Visible = false;
            }
            lblTimerCount.Text = CounterLabel.ToString();
            _DisplayQuestion();
        }
    }
}
