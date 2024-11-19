using DVLD_Bussiness;
using DVLD_System.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_System
{
    public partial class ctrlScheduleTest : UserControl
    {
        clsTestType.enTestType _TestType;

        enum enMode { enAddNew = 1,enUpdate = 2};
        enMode _Mode = enMode.enAddNew;

        enum enCreationMode { enFirstTime = 1, enRetakeTestSchedule = 2 };
        enCreationMode _CreationMode = enCreationMode.enFirstTime;

        int _LocalDrivingLicenseApplicationID = -1;

        int _TestAppointmentID = -1;

        clsTestAppointment _TestAppointmentInfo;

        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplicationInfo;
        public ctrlScheduleTest()
        {
            InitializeComponent();
        }

        public clsTestType.enTestType TestType
        {
            get { return _TestType; }

            set
            {
                _TestType = value;

                switch (_TestType)
                {
                    case clsTestType.enTestType.enVisionTest:
                        pbScheduleTestmage.Image = Resources.Vision_512;
                        this.Text = "Vision Schedule Test";
                        gpScheduleTest.Text = this.Text;
                        lblScheduleTestTitle.Text = this.Text;
                        break;
                    case clsTestType.enTestType.enWrittinTest:
                        pbScheduleTestmage.Image = Resources.Written_Test_512;
                        this.Text = "Written Schedule Test";
                        gpScheduleTest.Text = this.Text;
                        lblScheduleTestTitle.Text = this.Text;
                        break;
                    case clsTestType.enTestType.enStreetTest:
                        pbScheduleTestmage.Image = Resources.driving_test_512;
                        this.Text = "Street Schedule Test";
                        gpScheduleTest.Text = this.Text;
                        lblScheduleTestTitle.Text = this.Text;
                        break;
                }

            }
        }

        public void LoadInfo(int LocalDrivingLicenseApplicationID,int TestAppointmentID = -1)
        {
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;

            _TestAppointmentID = TestAppointmentID;

            if (TestAppointmentID == -1)
                _Mode = enMode.enAddNew;
            else
                _Mode = enMode.enUpdate;

            _LocalDrivingLicenseApplicationInfo =
                            clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseID(LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplicationInfo == null)
            {
                MessageBox.Show("LocalApplication is not found","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }

            lblDLAppID.Text = LocalDrivingLicenseApplicationID.ToString();
            lblDClass.Text = _LocalDrivingLicenseApplicationInfo.LicenseClassInfo.ClassName;
            lblFullName.Text = _LocalDrivingLicenseApplicationInfo.FullName;
            lblTrial.Text = _LocalDrivingLicenseApplicationInfo.GetTrialTestCountPerTestType(TestType).ToString();

            if (_LocalDrivingLicenseApplicationInfo.DoesAttendScheduleTest(_TestType))

                _CreationMode = enCreationMode.enRetakeTestSchedule;
            else
                _CreationMode = enCreationMode.enFirstTime;


            if (_CreationMode == enCreationMode.enRetakeTestSchedule)
            {
                gbRetakeTest.Enabled = true;
                lblRAppFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationTypes.enRetakeTest).Fees.ToString();
                lblRTestAppID.Text = "0";
                lblScheduleTestTitle.Text = "Retake Schedule Test";
            }
            else
            {
                gbRetakeTest.Enabled = false;
                lblRTestAppID.Text = "N/A";
                lblRAppFees.Text = "0";
                lblScheduleTestTitle.Text = "Schedule Test";
            }

            if (_Mode == enMode.enAddNew)
            {
                dtpDate.Value = DateTime.Now;
                dtpDate.MinDate = DateTime.Now;
                lblFees.Text = clsTestType.Find((int)TestType).TestTypeFees.ToString();

                _TestAppointmentInfo = new clsTestAppointment();
            }
            else
            {
                if (!_LoadTestAppointmentData())
                    return;
            }


        }

        private bool _LoadTestAppointmentData()
        {
            _TestAppointmentInfo = clsTestAppointment.Find(_TestAppointmentID);
            if (_TestAppointmentInfo == null)
            {
                MessageBox.Show($"No TestAppointment Found With Number [{_TestAppointmentID}]","Error",
                    MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }

            lblFees.Text = _TestAppointmentInfo.PaidFees.ToString();

            if (DateTime.Compare(DateTime.Now, _TestAppointmentInfo.AppointmentDate) < 0)
            {
                dtpDate.MinDate = DateTime.Now;
            }
            else
            {
                dtpDate.MinDate = _TestAppointmentInfo.AppointmentDate;
            }
            
            dtpDate.Value = _TestAppointmentInfo.AppointmentDate;
            
            if (_TestAppointmentInfo.RetakApplicationID == -1)
            {
                lblRTestAppID.Text = "N/A";
                lblRAppFees.Text = "0";
            }
            else
            {
                lblScheduleTestTitle.Text = "Retake Schedule Test";
                gbRetakeTest.Enabled = true;
                lblRTestAppID.Text = _TestAppointmentInfo.RetakApplicationID.ToString();
                lblRAppFees.Text = _TestAppointmentInfo.RetakeTestAppInfo.PaidFees.ToString();
            }


            return true;
        }

        private void ctrlScheduleTest_Load(object sender, EventArgs e)
        {
           
        }

        private bool HandleRetakeApplication()
        {
            if (_Mode == enMode.enAddNew && _CreationMode == enCreationMode.enRetakeTestSchedule)
            {
                clsApplication Application = new clsApplication();

                Application.ApplicantPersonID = _LocalDrivingLicenseApplicationInfo.ApplicantPersonID;
                Application.ApplicationStatus = (int)clsApplication.enApplicationStatus.enCompleted;
                Application.ApplicationDate = _LocalDrivingLicenseApplicationInfo.ApplicationDate;
                Application.ApplicationTypeID = (int)clsApplication.enApplicationTypes.enRetakeTest;
                Application.LastStatusDate = DateTime.Now;
                Application.PaidFees = clsApplicationTypes.Find((int)clsApplication.enApplicationTypes.enRetakeTest).Fees;
                Application.CreatedByUserID = clsGlobalSettings.CurrentUser.UserID;

                if (!Application.Save())
                {
                    _TestAppointmentInfo.RetakApplicationID = -1;
                    MessageBox.Show("Faild to create application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else
                {
                    _TestAppointmentInfo.RetakApplicationID = Application.ApplicationID;
                }
            }
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!HandleRetakeApplication())
            {
                return;
            }

            _TestAppointmentInfo.TestTypeID = (int)_TestType;
            _TestAppointmentInfo.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplicationID;
            _TestAppointmentInfo.AppointmentDate = dtpDate.Value;
            _TestAppointmentInfo.PaidFees = Convert.ToSingle(lblFees.Text);
            _TestAppointmentInfo.CreatedByUserID = clsGlobalSettings.CurrentUser.UserID;
            _TestAppointmentInfo.RetakApplicationID = -1;

            if (_TestAppointmentInfo.Save())
            {
                _Mode = enMode.enUpdate;
                MessageBox.Show("Test has been scheduled successfully","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                
            }
            else
            {
                MessageBox.Show("An Error Occoured","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }
    }
}
