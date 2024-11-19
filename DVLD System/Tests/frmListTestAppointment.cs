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

namespace DVLD_System.Tests
{
    public partial class frmListTestAppointment : Form
    {
      

        DataTable _dtTestAppointments;

        int _LocalApplicationID = -1;
        clsLocalDrivingLicenseApplication _LocalApplicationInfo;
        clsTestType.enTestType _TestType = clsTestType.enTestType.enVisionTest;
        public frmListTestAppointment(int LocalApplicationID,clsTestType.enTestType TestType)
        {
            InitializeComponent();

            this._LocalApplicationID = LocalApplicationID;
            this._LocalApplicationInfo = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseID(_LocalApplicationID);
            this._TestType = TestType;
        }

        private void _LoadTestTypeTitleAndImage()
        {
            switch (_TestType)
            {
                case clsTestType.enTestType.enVisionTest:
                    pbTestTypeImage.Image = Resources.Vision_512;
                    this.Text = "Vision Test Appointments";
                    lblTestTypeTitle.Text = this.Text;
                    break;
                case clsTestType.enTestType.enWrittinTest:
                    pbTestTypeImage.Image = Resources.Written_Test_512;
                    this.Text = "Vision Test Appointments";
                    lblTestTypeTitle.Text = this.Text;
                    break;
                case clsTestType.enTestType.enStreetTest:
                    pbTestTypeImage.Image = Resources.driving_test_512;
                    this.Text = "Driving Test Appointments";
                    lblTestTypeTitle.Text = this.Text;
                    break;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddAppointment_click(object sender, EventArgs e)
        {

            if (_LocalApplicationInfo.IsThereActiveAppointment(_TestType))
            {
                MessageBox.Show("There is an active appointment for this Application.",
                    "Error",MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            clsTest LastTest = _LocalApplicationInfo.GetLastTestPerTestType(_TestType);

            if (LastTest == null)
            {
                //Add New Appointment
                Form frm = new frmScheduleTest(_LocalApplicationID,_TestType);
                frm.ShowDialog();
                _RefreshAppintmentList();
                return;
            }

            if (LastTest.TestResult == true)
            {
                MessageBox.Show("The Person Has Passed This Test,you can't Add New Appointment.",
                    "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            //here add retake test
            frmScheduleTest frmSchedulaRetakeTest = new frmScheduleTest(_LocalApplicationID, _TestType);
            frmSchedulaRetakeTest.ShowDialog();
            _RefreshAppintmentList();


        }

        private void _RefreshAppintmentList()
        {
            _dtTestAppointments = clsTestAppointment.GetTestAppointmentsByLocalApplicationIDAndTestTypeID(_LocalApplicationID, (int)_TestType);

            if (_dtTestAppointments.Rows.Count <= 0)
                return;

            _dtTestAppointments = _dtTestAppointments.DefaultView
                                             .ToTable(false, "TestAppointmentID", "AppointmentDate", "PaidFees", "IsLocked");

            dgvAppointments.DataSource = _dtTestAppointments;

            lblRecordsCount.Text = _dtTestAppointments.Rows.Count.ToString();
        }

        private void frmVisionTestAppointment_Load(object sender, EventArgs e)
        {
            _LoadTestTypeTitleAndImage();

            _RefreshAppintmentList();

            ctrlLocalDrivingLicenseApplicationInfo1.LoadApplicationInfo(_LocalApplicationID);

            if (_dtTestAppointments.Rows.Count <= 0)
                return;
           
            dgvAppointments.DataSource = _dtTestAppointments;

                dgvAppointments.Columns[0].HeaderText = "Appointment ID";
                dgvAppointments.Columns[0].Width = 150;

                dgvAppointments.Columns[1].HeaderText = "Appointment Date";
                dgvAppointments.Columns[1].Width = 150;

                dgvAppointments.Columns[2].HeaderText = "Paid Fees";
                dgvAppointments.Columns[2].Width = 100;

                dgvAppointments.Columns[3].HeaderText = "Is Locked";
                dgvAppointments.Columns[3].Width = 100;

            lblRecordsCount.Text = _dtTestAppointments.Rows.Count.ToString();

           
        }

        private void editAppointmentToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int AppointmentID = (int)dgvAppointments.CurrentRow.Cells[0].Value;
  
                Form frm = new frmScheduleTest(_LocalApplicationID,_TestType, AppointmentID);
                frm.ShowDialog();

            _RefreshAppintmentList();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int AppointmentID = (int)dgvAppointments.CurrentRow.Cells[0].Value;


            Form frm = new frmTakeTest(AppointmentID,_TestType);
            frm.ShowDialog();

            _RefreshAppintmentList();
        }
    }
}
