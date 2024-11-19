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
    public partial class frmVisionTestAppointment : Form
    {
        int _ApplicationID;

        DataTable _dtTestAppointments;
        public frmVisionTestAppointment(int ApplicationID)
        {
            InitializeComponent();

            _ApplicationID = ApplicationID;

            _dtTestAppointments =  clsTestAppointment.GetTestAppointmentsByLocalApplicationIDAndTestTypeID(_ApplicationID, (int)clsTestType.enTestType.enVisionTest);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddAppointment_click(object sender, EventArgs e)
        {
            if (clsTestAppointment.IsTestAppointmentExist(_ApplicationID, (int)clsTestType.enTestType.enVisionTest))
            {
                if (!clsTestAppointment.IsTestAppointmentLocked(_ApplicationID, (int)clsTestType.enTestType.enVisionTest))
                {
                    MessageBox.Show("The Person Has Allready An Active Appointment", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    if (!clsTest.IsTestPassed(_ApplicationID, (int)clsTestType.enTestType.enVisionTest))
                    {
                        frmSchedualVisionTest scheduleVisionTest = new frmSchedualVisionTest(_ApplicationID,true);
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
                frmSchedualVisionTest scheduleVisionTest = new frmSchedualVisionTest(_ApplicationID,false);
                scheduleVisionTest.ShowDialog();
            }

            _RefreshAppintmentList();
        }

        private void _RefreshAppintmentList()
        {
            _dtTestAppointments = clsTestAppointment.GetTestAppointmentsByLocalApplicationIDAndTestTypeID(_ApplicationID, (int)clsTestType.enTestType.enVisionTest);

            if (_dtTestAppointments.Rows.Count <= 0)
                return;

            _dtTestAppointments = _dtTestAppointments.DefaultView
                                             .ToTable(false, "TestAppointmentID", "AppointmentDate", "PaidFees", "IsLocked");

            dgvAppointments.DataSource = _dtTestAppointments;

            lblRecordsCount.Text = _dtTestAppointments.Rows.Count.ToString();
        }

        private void frmVisionTestAppointment_Load(object sender, EventArgs e)
        {
            ctrlLocalDrivingLicenseApplicationInfo1.LoadApplicationInfo(_ApplicationID);

            if (_dtTestAppointments.Rows.Count <= 0)
                return;
            _dtTestAppointments = _dtTestAppointments.DefaultView
                                               .ToTable(false, "TestAppointmentID", "AppointmentDate", "PaidFees", "IsLocked");

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
    }
}
