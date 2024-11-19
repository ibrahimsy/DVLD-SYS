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
using static DVLD_Bussiness.clsTestType;

namespace DVLD_System
{
    public partial class ctrlScheduledTest : UserControl
    {
        public ctrlScheduledTest()
        {
            InitializeComponent();
        }

        clsTestType.enTestType _TestType;

        int _AppointmentID = -1;

        clsTestAppointment _AppointmentInfo;
        public clsTestType.enTestType TestType
        {
            get { return _TestType; }

            set
            {
                _TestType = value;

                switch (_TestType)
                {
                    case clsTestType.enTestType.enVisionTest:
                        gbTestType.Text = "Vision Test";
                        pbTestTypeImage.Image = Resources.Vision_512;
                        lblScheduleTitle.Text = "Schedule Vision Test";
                        break;
                    case clsTestType.enTestType.enWrittinTest:
                        gbTestType.Text = "Written Test";
                        pbTestTypeImage.Image = Resources.Written_Test_512;
                        lblScheduleTitle.Text = "Schedule Written Test";
                        break;
                    case clsTestType.enTestType.enStreetTest:
                        gbTestType.Text = "Street Test";
                        pbTestTypeImage.Image = Resources.driving_test_512;
                        lblScheduleTitle.Text = "Schedule Street Test";
                        break;
                }
            }
        }

        public clsTestAppointment AppointmentInfo
        {
            get
            {
                return _AppointmentInfo;
            }
        }

        public int TestID
        {
            get { return _AppointmentInfo.TestID; }
        }

        public void LoadInfo(int AppointmentID)
        {
            _AppointmentID = AppointmentID;

             _AppointmentInfo = clsTestAppointment.Find(AppointmentID);

            if (_AppointmentInfo == null)
            {
                MessageBox.Show("No Appointment Found","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            clsLocalDrivingLicenseApplication LDLAppInfo = _AppointmentInfo.LocalDrivingLicenseApplicationInfo;

            lblLDLAppID.Text = LDLAppInfo.LocalDrivingLicenseApplicationID.ToString();
            lblDClass.Text = LDLAppInfo.LicenseClassInfo.ClassName.ToString();
            lblName.Text = LDLAppInfo.FullName;
            lblTrail.Text = LDLAppInfo.GetTestTrialPerTestType(_TestType).ToString();

            lblFees.Text = _AppointmentInfo.PaidFees.ToString();

            lblDate.Text = _AppointmentInfo.AppointmentDate.ToString();

        }
    
    }
}
