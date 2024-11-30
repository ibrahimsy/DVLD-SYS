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

namespace DVLD_System.Tests
{
    public partial class frmTakeTest : Form
    {


        int _AppointmentID = -1;
        clsTestType.enTestType _TestType = clsTestType.enTestType.enVisionTest;
        int _TestID = -1;

        clsTest _Test;
        public frmTakeTest(int AppointmentID,clsTestType.enTestType TesyType)
        {
            InitializeComponent();

            _TestType = TesyType;
            _AppointmentID = AppointmentID;
        }

     
        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            ctrlScheduledTest1.TestType = _TestType;
            ctrlScheduledTest1.LoadInfo(_AppointmentID);
            _TestID = ctrlScheduledTest1.TestID;

            if (_TestID != -1)
            {
                _Test = clsTest.Find(_TestID);
                if (_Test.TestResult)
                    rbPass.Checked = true;
                else
                    rbFail.Checked = true;
                rbPass.Enabled = false;
                rbFail.Enabled = false;

                btnSave.Enabled = false;
            }
            else
                _Test = new clsTest();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            DialogResult Result =  MessageBox.Show("Are You Sure You Want To Save Test\n You can't change The Result After Save",
                            "Confirm",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

            if (Result == DialogResult.No)
                return;
            

            _Test.TestAppointmentID = _AppointmentID;
            _Test.Notes = txtNotes.Text;
            _Test.TestResult = rbPass.Checked;
            _Test.CreatedByUserID = clsGlobalSettings.CurrentUser.UserID;

            if (_Test.Save())
            {
                MessageBox.Show("Test Was Saved Successfuly","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("An Error Occurred", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Close();
        }
    }
}
