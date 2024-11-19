using DVLD_Bussiness;
using DVLD_System.Tests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DVLD_Bussiness.clsApplication;

namespace DVLD_System.Applications
{
    public partial class frmLocalDrivingLicenseApplications : Form
    {
        DataTable _LocalLicenseApplications;
        public frmLocalDrivingLicenseApplications()
        {
            InitializeComponent();

        }

        void _RefreshData()
        {
            _LocalLicenseApplications = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();

            dgvLocalDrivingLicenseApplicatios.DataSource = _LocalLicenseApplications;
        }
        void _FillComboBoxWithColumnsNames()
        {
            cbFilterBy.Items.Add("No");

            foreach (DataColumn column in _LocalLicenseApplications.Columns)
            {
                column.ColumnName = _ColumnTextFormat(column.ColumnName);
                cbFilterBy.Items.Add(column.ColumnName);
            }
            /* No
             * LDLAPPID
             * LicenceClassName
             * National No
             * FullName
             * ApplicationDate
             * PassedTest
             * Status
            */
        }

        void _FillComboBoxApplicationStatus()
        {
            cbApplicationStatus.Items.Add("All");
            cbApplicationStatus.Items.Add("New");
            cbApplicationStatus.Items.Add("Canceled");
            cbApplicationStatus.Items.Add("Completed");
        }

        string _ColumnTextFormat(string ColumnName)
        {
            switch (ColumnName)
            {
                case "LocalDrivingLicenseApplicationID":
                    return "L.D.L.APPID";
                case "ClassName":
                    return "Class Name";
                case "NationalNo":
                    return "National No";
                case "FullName":
                    return "Full Name";
                case "ApplicationDate":
                    return "Application Date";
                case "PassedTestCount":
                    return "Passed Test Count";
                case "Status":
                    return "Status";
                default:
                    return string.Empty;
            }
        }

        void _LoadDataInfo()
        {
            _RefreshData();

            _FillComboBoxWithColumnsNames();

            _FillComboBoxApplicationStatus();

            cbFilterBy.SelectedIndex = 0;
           
            
        }

        private void frmLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            _LoadDataInfo();
        }
        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string SelectedColumn = cbFilterBy.SelectedItem.ToString();

            string FilterValue = ((TextBox)sender).Text;

            _LocalLicenseApplications.DefaultView.RowFilter = $"Convert([{SelectedColumn}], 'System.String') Like '{FilterValue}%'";
        }

        private void cbApplicationStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = false;
            string FilterValue = cbApplicationStatus.SelectedItem.ToString();
            _LocalLicenseApplications.DefaultView.RowFilter = 
                cbApplicationStatus.SelectedIndex == 0? $"L.D.L.APPID IS NOT NULL" : $"Status Like '{FilterValue}%'";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            frmAddNewLocalDrivingLicense addNewLocalDrivingLicense = new frmAddNewLocalDrivingLicense();
            addNewLocalDrivingLicense.ShowDialog();

            _RefreshData();
        }

        private void ccbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedIndex == 0)
            {
                txtFilterValue.Visible = false;
                cbApplicationStatus.Visible = false;

                _LocalLicenseApplications.DefaultView.RowFilter = $"L.D.L.APPID IS NOT NULL";
                return;
            }
            if (cbFilterBy.SelectedItem.ToString() == "Status")
            {
                cbApplicationStatus.Visible = true;
                cbApplicationStatus.SelectedIndex = 0;
                txtFilterValue.Visible = false;
                return;
            }

            txtFilterValue.Visible = true;
            cbApplicationStatus.Visible = false;
        }

        private void btnClose_click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ApplicationID = (int)dgvLocalDrivingLicenseApplicatios.CurrentRow.Cells[0].Value;   
            
            frmLocalDrivingLicenseApplicationInfo LDLAppInfo = new frmLocalDrivingLicenseApplicationInfo(ApplicationID);
            LDLAppInfo.ShowDialog();
        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void schedualVisionTestToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int ApplicationID = (int)dgvLocalDrivingLicenseApplicatios.CurrentRow.Cells[0].Value;

            frmVisionTestAppointment visionTestAppintment = new frmVisionTestAppointment(ApplicationID);
            visionTestAppintment.ShowDialog();
        }

        private void schedualWrittenTestToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void schedualStreetTestToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLApplicationID = (int)dgvLocalDrivingLicenseApplicatios.CurrentRow.Cells[0].Value;



            DialogResult result = MessageBox.Show("Are You Sue You Want To Delete This Application",
                                                  "Confirm",
                                                   MessageBoxButtons.YesNo,
                                                   MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                bool isCanceled = clsApplication.Cancel(clsLocalDrivingLicenseApplication.Find(LDLApplicationID).ApplicationID);
                if (isCanceled)
                {
                    _RefreshData();
                }
                else
                {
                    MessageBox.Show("An Error Occoured");
                }
            }
        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLApplicationID = (int)dgvLocalDrivingLicenseApplicatios.CurrentRow.Cells[0].Value;

            
           
                DialogResult result = MessageBox.Show("Are You Sue You Want To Cancel This Application",
                                                      "Confirm",
                                                       MessageBoxButtons.YesNo,
                                                       MessageBoxIcon.Question);
                if (result == DialogResult.Yes) 
                {
                    bool isCanceled = clsApplication.Cancel(clsLocalDrivingLicenseApplication.Find(LDLApplicationID).ApplicationID);
                    if (isCanceled)
                    {
                        _RefreshData();
                    }
                    else
                    {
                        MessageBox.Show("An Error Occoured");
                    }
                }
        }

        private void dgvLocalDrivingLicenseApplicatios_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvLocalDrivingLicenseApplicatios.Columns[e.ColumnIndex].Name == "Status")
            {
                if (e.Value != null && e.Value.ToString() == "Cancelled")
                {
                    dgvLocalDrivingLicenseApplicatios.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightYellow;
                }
            }
        }
    }
}
