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

namespace DVLD_System.Tests
{
    public partial class frmListTestAppointments : Form
    {
        int _LocalApplicationID;
        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplicationInfo;
        DataTable _dtTestAppointments;
        clsTestType.enTestType _TestType;
        public frmListTestAppointments(int ApplicationID,clsTestType.enTestType TestType)
        {
            InitializeComponent();

            _LocalApplicationID = ApplicationID;

            _TestType = TestType;

            _LocalDrivingLicenseApplicationInfo = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseID(ApplicationID);

            _dtTestAppointments = _LocalDrivingLicenseApplicationInfo.GetTestAppintmentsByTestType(TestType);

        }

        private void _LoadTestAppointmentsTitleAndImage(clsTestType.enTestType TestType)
        {
            switch (TestType)
            {
                case clsTestType.enTestType.enVisionTest:
                    pbTestAppintmentImage.Image = Resources.Vision_512;  
                    this.Text = "Vision Test Appointment";
                    lblTestAppointmentType.Text = this.Text;
                    break;
                case clsTestType.enTestType.enWrittinTest:
                    pbTestAppintmentImage.Image = Resources.Written_Test_512;
                    this.Text = "Written Test Appointment";
                    lblTestAppointmentType.Text = this.Text;
                    break;
                case clsTestType.enTestType.enStreetTest:
                    pbTestAppintmentImage.Image = Resources.driving_test_512;
                    this.Text = "Street Test Appointment";
                    lblTestAppointmentType.Text = this.Text;
                    break;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddAppointment_click(object sender, EventArgs e)
        {
            if (clsTestAppointment.IsTestAppointmentExist(_LocalApplicationID, (int)clsTestType.enTestType.enVisionTest))
            {
                if (!clsTestAppointment.IsTestAppointmentLocked(_LocalApplicationID, (int)clsTestType.enTestType.enVisionTest))
                {
                    MessageBox.Show("The Person Has Allready An Active Appointment", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    if (!clsTest.IsTestPassed(_LocalApplicationID, (int)clsTestType.enTestType.enVisionTest))
                    {
                        frmSchedualVisionTest scheduleVisionTest = new frmSchedualVisionTest(_LocalApplicationID,true);
                        scheduleVisionTest.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("The Person Allready Pass This Test", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

            }
            else
            {
                frmSchedualVisionTest scheduleVisionTest = new frmSchedualVisionTest(_LocalApplicationID,false);
                scheduleVisionTest.ShowDialog();
            }

            _RefreshAppintmentList();
        }

        private void _RefreshAppintmentList()
        {
            _dtTestAppointments = _LocalDrivingLicenseApplicationInfo.GetTestAppintmentsByTestType(_TestType);

            if (_dtTestAppointments.Rows.Count <= 0)
                return;

            //_dtTestAppointments = _dtTestAppointments.DefaultView
            //                                 .ToTable(false, "TestAppointmentID", "AppointmentDate", "PaidFees", "IsLocked");

            dgvAppointments.DataSource = _dtTestAppointments;

            lblRecordsCount.Text = _dtTestAppointments.Rows.Count.ToString();
        }

        private void editAppointmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int AppointmentID = (int)dgvAppointments.CurrentRow.Cells[0].Value;

            bool isLocked = clsTestAppointment.IsTestAppointmentLocked(AppointmentID);
            
                Form frm = new frmSchedualVisionTest(isLocked, AppointmentID);
                frm.ShowDialog();

            _RefreshAppintmentList();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int AppointmentID = (int)dgvAppointments.CurrentRow.Cells[0].Value;

            bool isLocked = clsTestAppointment.IsTestAppointmentLocked(AppointmentID);

            Form frm = new frmTakeTest(AppointmentID, isLocked);
            frm.ShowDialog();

            _RefreshAppintmentList();
        }

        private void frmListTestAppointment_Load(object sender, EventArgs e)
        {

            _LoadTestAppointmentsTitleAndImage(_TestType);

            ctrlLocalDrivingLicenseApplicationInfo1.LoadApplicationInfo(_LocalApplicationID);

            dgvAppointments.DataSource = _dtTestAppointments;

            if (_dtTestAppointments.Rows.Count > 0)
            {
                dgvAppointments.Columns[0].HeaderText = "Appintment ID";
                dgvAppointments.Columns[0].Width = 100;

                dgvAppointments.Columns[1].HeaderText = "Appintment Date";
                dgvAppointments.Columns[1].Width = 100;

                dgvAppointments.Columns[2].HeaderText = "Paid Fees";
                dgvAppointments.Columns[2].Width = 100;

                dgvAppointments.Columns[3].HeaderText = "Is Locked";
                dgvAppointments.Columns[3].Width = 80;  
            }

            lblRecordsCount.Text = _dtTestAppointments.Rows.Count.ToString();
        }
    }
}
