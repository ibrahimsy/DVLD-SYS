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
        enum enMode { enAddNew = 1,enUpdate = 2}
        enMode _Mode = enMode.enAddNew;

        enum enCreationMode { enFirstTime = 1,enRetakeScheduleTest = 2}
        enCreationMode _CreationMode = enCreationMode.enFirstTime;
        public ctrlScheduleTest()
        {
            InitializeComponent();
        }
        clsTestType.enTestType _TestType;

        int _LocalDrivingLicenseApplicationID = -1;

        int _TestAppointmentID = -1;

        clsLocalDrivingLicenseApplication _LocalDLAppInfo;

        clsTestAppointment _TestAppointmentInfo;
        public clsTestType.enTestType TestType
        {
            get { return _TestType; }

            set
            {
                _TestType = value;

                switch (_TestType)
                {
                    case clsTestType.enTestType.enVisionTest:
                        gbTestTitle.Text = "Vision Test";
                        pbTestTypeImage.Image = Resources.Vision_512;
                        lblTestTypeTitle.Text = "Schedule Vision Test";
                        break;
                    case clsTestType.enTestType.enWrittinTest:
                        gbTestTitle.Text = "Written Test";
                        pbTestTypeImage.Image = Resources.Written_Test_512;
                        lblTestTypeTitle.Text = "Schedule Written Test";
                        break;
                    case clsTestType.enTestType.enStreetTest:
                        gbTestTitle.Text = "Street Test";
                        pbTestTypeImage.Image = Resources.driving_test_512;
                        lblTestTypeTitle.Text = "Schedule Street Test";
                        break;
                }
            }
        }
    
        public void LoadInfo(int LocalDrivingLicenseApplicationID,int _AppointmentID)
        {
            _TestAppointmentID = _AppointmentID;

            if (_TestAppointmentID == -1)  
                _Mode = enMode.enAddNew;
            else
                _Mode = enMode.enUpdate;

            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;

            _LocalDLAppInfo = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseID(LocalDrivingLicenseApplicationID);
           
            if (_LocalDLAppInfo == null)
            {
                MessageBox.Show("No LocalApplication Exists","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            lblDLAppID.Text = _LocalDrivingLicenseApplicationID.ToString();
            lblDClass.Text = _LocalDLAppInfo.LicenseClassInfo.ClassName.ToString();
            lblFullName.Text = _LocalDLAppInfo.FullName;
            lblTrial.Text = _LocalDLAppInfo.GetTestTrialPerTestType(_TestType).ToString();

            if (_LocalDLAppInfo.DoesAttendTestType(_TestType))
                _CreationMode = enCreationMode.enRetakeScheduleTest;        
            else         
                _CreationMode = enCreationMode.enFirstTime;
            

            if(_CreationMode == enCreationMode.enFirstTime)
            {
               gbRetakeTest.Enabled = false;
                lblRAppFees.Text = "0";
                lblRTestAppID.Text = "N/A";
                lblTotalFees.Text = "0";
                lblTestTypeTitle.Text = $"Schedule {gbTestTitle.Text}";
            }
            else
            {
                gbRetakeTest.Enabled = true;
                lblRAppFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationTypes.enRetakeTest).Fees.ToString();
                lblRTestAppID.Text = "0";
                lblTestTypeTitle.Text = $"Retake Schedule {gbTestTitle.Text}";
            }


            if (_Mode == enMode.enAddNew)
            {
                lblFees.Text = clsTestType.Find((int)_TestType).TestTypeFees.ToString();
                dtpDate.Value = DateTime.Now;
                dtpDate.MinDate = DateTime.Now;
                _TestAppointmentInfo = new clsTestAppointment();
            }
            else
            {
                if (!_LoadAppointmentInfo())
                    return;
            }

            lblTotalFees.Text = (Convert.ToSingle(lblFees.Text) + Convert.ToSingle(lblRAppFees.Text)).ToString();


            _HandleLockedScheduleConstraint();
        }
        bool _LoadAppointmentInfo()
        {
            _TestAppointmentInfo = clsTestAppointment.Find(_TestAppointmentID);
            if (_TestAppointmentInfo == null)
            {
                MessageBox.Show("No Schedule found for this application");
                return false;
            }
            else
            {
                if (DateTime.Compare(DateTime.Now, _TestAppointmentInfo.AppointmentDate) > 0)
                {
                    dtpDate.MinDate = DateTime.Now;
                }
                else
                {
                    dtpDate.MinDate = _TestAppointmentInfo.AppointmentDate;
                }
                dtpDate.Value = _TestAppointmentInfo.AppointmentDate;

                lblFees.Text = _TestAppointmentInfo.PaidFees.ToString();
                

                if (_TestAppointmentInfo.RetakApplicationID == -1)
                {
                    lblRTestAppID.Text = "N/A";
                    gbRetakeTest.Enabled = false;
                    lblRAppFees.Text = "0";
                }
                else
                {
                    lblRTestAppID.Text = _TestAppointmentInfo.RetakApplicationID.ToString();
                }
            }
            return true;
        }
        bool _HandleLockedScheduleConstraint()
        {
            if (_TestAppointmentInfo.IsLocked)
            {
                lblLockedTestMessage.Text = "Person Allready Sat for this Appointment,Appointment is Locked";
                lblLockedTestMessage.Visible = true;
                dtpDate.Enabled = false;
                btnSave.Enabled = false;
                return true;
            } else
                return false;
        }
        bool _HandleRetakeScheduleTest()
        {
            if (_Mode == enMode.enAddNew && _CreationMode == enCreationMode.enRetakeScheduleTest)
            {
                clsApplication RetakeApplication = new clsApplication();
                RetakeApplication.ApplicantPersonID = _LocalDLAppInfo.ApplicantPersonID;
                RetakeApplication.ApplicationDate = _LocalDLAppInfo.ApplicationDate;
                RetakeApplication.ApplicationTypeID = (int)clsApplication.enApplicationTypes.enRetakeTest;
                RetakeApplication.ApplicationStatus = (int)clsApplication.enApplicationStatus.enCompleted;
                RetakeApplication.LastStatusDate = DateTime.Now;
                RetakeApplication.PaidFees = clsApplicationTypes.Find(RetakeApplication.ApplicationTypeID).Fees;
                RetakeApplication.CreatedByUserID = clsGlobalSettings.CurrentUser.UserID;

                if (RetakeApplication.Save())
                {
                    _TestAppointmentInfo.RetakApplicationID = RetakeApplication.ApplicationID;
                    lblRTestAppID.Text = RetakeApplication.ApplicationID.ToString();
                }
                else
                {
                    MessageBox.Show("An Error Occurred : Retake Application has not created Successfuly","Error");
                    return false;
                }

            }
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_HandleRetakeScheduleTest())
            {
                return;
            }
            _TestAppointmentInfo.TestTypeID = (int)_TestType;
            _TestAppointmentInfo.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplicationID;
            _TestAppointmentInfo.AppointmentDate = dtpDate.Value;
            _TestAppointmentInfo.PaidFees = Convert.ToSingle(lblFees.Text);
            _TestAppointmentInfo.CreatedByUserID = clsGlobalSettings.CurrentUser.UserID;

            if (_TestAppointmentInfo.Save())
            {
                _Mode = enMode.enUpdate;
                btnSave.Enabled = false;
                MessageBox.Show($"The Test Scheduled Successfuly with TestAppointment ID [{_TestAppointmentInfo.TestAppointmentID}].");       
            }
            else
            {
                MessageBox.Show("An Error Occurred","Error");
                return;
            }
           
        }
    }
}
