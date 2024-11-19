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
    public partial class frmScheduleTest : Form
    {
        int _LocalDrivingLicenseApplicationID = -1;
        clsTestType.enTestType _TestType = clsTestType.enTestType.enVisionTest;
        int _AppointmentID = -1;
        public frmScheduleTest(int LocalDrivingLicenseApplicationID,clsTestType.enTestType TestType,int AppointmentID = -1)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestType = TestType;
            _AppointmentID = AppointmentID;
        }
       


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            ctrlScheduleTest1.TestType = _TestType;
            ctrlScheduleTest1.LoadInfo(_LocalDrivingLicenseApplicationID,_AppointmentID);
        }
    }
}
