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
    public partial class frmSchedualVisionTest : Form
    {

        enum enMode { enAdd = 1,enUpdate = 2}
        
        enMode _Mode = enMode.enAdd;

        clsTestAppointment _TestAppointment = new clsTestAppointment();

        int _LDLApplicationID = 0;

        bool _isRetake;

        bool _isLocked;
        clsLocalDrivingLicenseApplication _LDLAppInfo;
        public frmSchedualVisionTest(int LocalDrivingLicenseID,bool isRetake)
        {
            InitializeComponent();

            _LDLApplicationID = LocalDrivingLicenseID;
            _isRetake = isRetake;
            _LDLAppInfo = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseID(LocalDrivingLicenseID);
            _Mode = enMode.enAdd;

        }
        
        public frmSchedualVisionTest(bool isLocked, int TestAppointmentID)
        {
            InitializeComponent();

            _TestAppointment = clsTestAppointment.Find(TestAppointmentID);

            _LDLApplicationID = _TestAppointment.LocalDrivingLicenseApplicationID;

            _LDLAppInfo = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseID(_LDLApplicationID);

            _Mode = enMode.enUpdate;

            _isLocked = isLocked;
        }

        void _FillDefaultScheduleTestData()
        {
           
            lblDLAppID.Text = _LDLApplicationID.ToString();
            lblDClass.Text = clsLicenseClass.Find(_LDLAppInfo.LicenseClassID).ClassName;
            lblFullName.Text = _LDLAppInfo.FullName;
            lblTrial.Text = clsTestAppointment.GetTestTrailCount((int)clsTestType.enTestType.enVisionTest, _LDLApplicationID).ToString();
            lblFees.Text = clsTestType.Find((int)clsTestType.enTestType.enVisionTest).TestTypeFees.ToString();
            
            dtpDate.MinDate = DateTime.Now;
            dtpDate.Value = DateTime.Now;
            
            if (_Mode == enMode.enUpdate)
            {
                dtpDate.MinDate = _TestAppointment.AppointmentDate;
                dtpDate.Value = _TestAppointment.AppointmentDate;

            }
                

            if (_isRetake)
            {
                gbRetakeTest.Enabled = true;
                lblTestTitle.Text = "Schedule Retake Vision Test";
                float retakeApplicationFees = clsApplicationTypes.Find((int)clsApplication.enApplicationTypes.enRetakeTest).Fees;
                lblRAppFees.Text = retakeApplicationFees.ToString();
                lblTotalFees.Text = (retakeApplicationFees + Convert.ToSingle(lblFees.Text)).ToString();
                lblRTestAppID.Text = _LDLAppInfo.ApplicationID.ToString();
                _TestAppointment.RetakApplicationID = _LDLAppInfo.ApplicationID;
                
                DateTime minDate = clsTestAppointment.GetLatestAppointmentDate(_LDLApplicationID,(int)clsTestType.enTestType.enVisionTest);
                dtpDate.MinDate= minDate;
                dtpDate.Value = minDate;
            }  
            else
            {
                gbRetakeTest.Enabled = false;
                lblTestTitle.Text = "Schedule Vision Test";
            }

                btnSave.Enabled = !_isLocked;
                dtpDate.Enabled = !_isLocked;
                lblLockedTestMessage.Visible = _isLocked;
                
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSchedualVisionTest_Load(object sender, EventArgs e)
        {
            _FillDefaultScheduleTestData();
        }

        private void _SaveTestAppintmentData()
        {
            if (_Mode == enMode.enAdd)
            {
                _TestAppointment.TestTypeID = (int)clsTestType.enTestType.enVisionTest;
                _TestAppointment.LocalDrivingLicenseApplicationID = _LDLApplicationID;
                _TestAppointment.AppointmentDate = dtpDate.Value;
                _TestAppointment.PaidFees = _LDLAppInfo.PaidFees;
                _TestAppointment.CreatedByUserID = clsGlobalSettings.CurrentUser.UserID;
                _TestAppointment.IsLocked = false;              
            }
            else
            {
                _TestAppointment.AppointmentDate = dtpDate.Value;
            }

            if (_isRetake)
            {
                float retakeApplicationFees = clsApplicationTypes.Find((int)clsApplication.enApplicationTypes.enRetakeTest).Fees;
                _TestAppointment.PaidFees = retakeApplicationFees + _LDLAppInfo.PaidFees;
            }


            if (_TestAppointment.Save())
            {

                MessageBox.Show("Appointment was Scheduled Successfuly.","Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;
            }
            else
            {
                MessageBox.Show("An Error Occoured.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            _SaveTestAppintmentData();
        }
    }
}
