using DVLD_Bussiness;
using DVLD_System.Licenses;
using DVLD_System.Licenses.Local_Licenses;
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
        DataTable _dtLocalLicenseApplications;
        public frmLocalDrivingLicenseApplications()
        {
            InitializeComponent();

        }

        void _RefreshApplicationsList()
        {
            _dtLocalLicenseApplications = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();

            dgvLocalDrivingLicenseApplicatios.DataSource = _dtLocalLicenseApplications;

            lblNumberOfRecords.Text = _dtLocalLicenseApplications.Rows.Count.ToString();
        }
        
        void _LoadDataInfo()
        {
            _RefreshApplicationsList();
           
            if (dgvLocalDrivingLicenseApplicatios.Rows.Count > 0)
            {
                dgvLocalDrivingLicenseApplicatios.Columns[0].HeaderText = "LDLAppID";
                dgvLocalDrivingLicenseApplicatios.Columns[0].Width = 40;

                dgvLocalDrivingLicenseApplicatios.Columns[1].HeaderText = "Driving Class";
                dgvLocalDrivingLicenseApplicatios.Columns[1].Width = 100;

                dgvLocalDrivingLicenseApplicatios.Columns[2].HeaderText = "National No";
                dgvLocalDrivingLicenseApplicatios.Columns[2].Width = 40;

                dgvLocalDrivingLicenseApplicatios.Columns[3].HeaderText = "Full Name";
                dgvLocalDrivingLicenseApplicatios.Columns[3].Width = 100;

                dgvLocalDrivingLicenseApplicatios.Columns[4].HeaderText = "Application Date";
                dgvLocalDrivingLicenseApplicatios.Columns[4].Width = 50;

                dgvLocalDrivingLicenseApplicatios.Columns[5].HeaderText = "Passed Tests";
                dgvLocalDrivingLicenseApplicatios.Columns[5].Width = 40;

                dgvLocalDrivingLicenseApplicatios.Columns[6].HeaderText = "Status";
                dgvLocalDrivingLicenseApplicatios.Columns[6].Width = 100;
            }
            

            cbFilterBy.SelectedIndex = 0;
           
            
        }

        private void frmLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            _LoadDataInfo();
        }
        
        string _SelectedColumnText(string selectedColumn)
        {
            switch (selectedColumn)
            {
                case "LDLAppID":
                    return "LocalDrivingLicenseApplicationID";
                case "Driving Class":
                    return "ClassName";
                case "National No":
                    return "NationalNo";
                case "Full Name":
                    return "FullName";
                case "Application Date":
                    return "ApplicationDate";
                case "Passed Test":
                    return "PassedTestCount";
                case "Status":
                    return "Status";      
            }
            return "";
        }
        
        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string SelectedColumn = _SelectedColumnText(cbFilterBy.Text);

            string FilterValue = ((TextBox)sender).Text.Trim();

            if (FilterValue == "")
            {
                _dtLocalLicenseApplications.DefaultView.RowFilter = "";
                lblNumberOfRecords.Text = _dtLocalLicenseApplications.DefaultView.Count.ToString();
                return;
            }
          

                if (SelectedColumn == "LocalDrivingLicenseApplicationID" )
            
                     _dtLocalLicenseApplications.DefaultView.RowFilter = string.Format("{0} = {1}", SelectedColumn,FilterValue);
                else
                     _dtLocalLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] Like '{1}%'", SelectedColumn, FilterValue);

            lblNumberOfRecords.Text = _dtLocalLicenseApplications.DefaultView.Count.ToString();
        }
                                                                                       
        private void cbApplicationStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
           // txtFilterValue.Visible = false;


            string FilterValue = cbApplicationStatus.Text;

            switch (FilterValue)
            {
                case "All":
                    _dtLocalLicenseApplications.DefaultView.RowFilter = "";
                    break;
                case "New":
                    _dtLocalLicenseApplications.DefaultView.RowFilter = "Status = 'New'";
                    break;
                case "Completed":
                    _dtLocalLicenseApplications.DefaultView.RowFilter = "Status = 'Completed'";
                    break;
                case "Canceled":
                    _dtLocalLicenseApplications.DefaultView.RowFilter = "Status = 'Cancelled'";
                    break; 
            }

            lblNumberOfRecords.Text = _dtLocalLicenseApplications.DefaultView.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            frmAddNewLocalDrivingLicense addNewLocalDrivingLicense = new frmAddNewLocalDrivingLicense();
            addNewLocalDrivingLicense.ShowDialog();

            _RefreshApplicationsList();
        }

        private void ccbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbFilterBy.Text == "None")
            {
                txtFilterValue.Visible = false;
                cbApplicationStatus.Visible = false;

                _dtLocalLicenseApplications.DefaultView.RowFilter = "";
                lblNumberOfRecords.Text = _dtLocalLicenseApplications.DefaultView.Count.ToString();
                return;
            }

            if (cbFilterBy.Text == "Status")
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
            int LocalApplicationID = (int)dgvLocalDrivingLicenseApplicatios.CurrentRow.Cells[0].Value;

            frmAddNewLocalDrivingLicense addEditLDLApp = new frmAddNewLocalDrivingLicense(LocalApplicationID);
            addEditLDLApp.Show();
            frmLocalDrivingLicenseApplications_Load(null, null);
        }

        private void _SceduleTest(clsTestType.enTestType TestType)
        {
            int LocalApplicationID = (int)dgvLocalDrivingLicenseApplicatios.CurrentRow.Cells[0].Value;

            Form frm = new frmListTestAppointment(LocalApplicationID, TestType);
            frm.ShowDialog();

            frmLocalDrivingLicenseApplications_Load(null,null);

        }
        private void schedualVisionTestToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _SceduleTest(clsTestType.enTestType.enVisionTest);
        }

        private void schedualWrittenTestToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _SceduleTest(clsTestType.enTestType.enWrittinTest);
        }

        private void schedualStreetTestToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _SceduleTest(clsTestType.enTestType.enStreetTest);
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
                clsLocalDrivingLicenseApplication LDLApp = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseID(LDLApplicationID);
                
                bool isDeleted = LDLApp.Delete();
               
                if (isDeleted)
                {
                    _RefreshApplicationsList();
                }
                else
                {
                    MessageBox.Show("You Can't Delete This Application,\n There is data depends on it",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
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
                clsLocalDrivingLicenseApplication LDLApp = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseID(LDLApplicationID);
                    bool isCanceled = LDLApp.Cancel();
                    
                    if (isCanceled)
                    {
                        _RefreshApplicationsList();
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

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
       {
            if(cbFilterBy.Text == "LDLAppID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void dgvLocalDrivingLicenseApplicatios_DoubleClick(object sender, EventArgs e)
        {
            int ApplicationID = (int)dgvLocalDrivingLicenseApplicatios.CurrentRow.Cells[0].Value;

            frmLocalDrivingLicenseApplicationInfo LDLAppInfo = new frmLocalDrivingLicenseApplicationInfo(ApplicationID);
            LDLAppInfo.ShowDialog();
        }

        private void schedualTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

        }

        private void dgvLocalDrivingLicenseApplicatios_MouseDown(object sender, MouseEventArgs e)
        {
            

        }

        private void dgvLocalDrivingLicenseApplicatios_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            
           
        }

        private void cmsApplication_opening(object sender, CancelEventArgs e)
        {
            int LocaDrivingLicenselApplicationID = (int)dgvLocalDrivingLicenseApplicatios.CurrentRow.Cells[0].Value;
            
            int PassedTestsCount = (int)dgvLocalDrivingLicenseApplicatios.CurrentRow.Cells[5].Value;

            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseID(LocaDrivingLicenselApplicationID);



            bool LicenseExists =  localDrivingLicenseApplication.IsLicenseIssued();

            editApplication.Enabled = !LicenseExists && localDrivingLicenseApplication.ApplicationStatus == (int)clsApplication.enApplicationStatus.enNew;

            deleteApplication.Enabled = localDrivingLicenseApplication.ApplicationStatus == (int)clsApplication.enApplicationStatus.enNew;

            cancelApplication.Enabled = localDrivingLicenseApplication.ApplicationStatus == (int)clsApplication.enApplicationStatus.enNew;

            showLicense.Enabled = LicenseExists && localDrivingLicenseApplication.ApplicationStatus == (int)clsApplication.enApplicationStatus.enCompleted;

            issueDrivingLicenseFirstTime.Enabled = (PassedTestsCount == 3) && !LicenseExists;

            bool PassedVisionTest = localDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.enVisionTest);
            bool PassedWrittenTest = localDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.enWrittinTest); ;
            bool PassedStrretTest = localDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.enStreetTest); ;

            schedualTest.Enabled = (!PassedVisionTest || !PassedWrittenTest || !PassedStrretTest) && localDrivingLicenseApplication.ApplicationStatus == (int)clsApplication.enApplicationStatus.enNew;

            if (schedualTest.Enabled)
            {
                schedualVisionTest.Enabled = !PassedVisionTest;
                schedualWrittenTest.Enabled = PassedVisionTest && !PassedWrittenTest;
                schedualStreetTest.Enabled = PassedVisionTest && PassedWrittenTest && !PassedStrretTest;
            }
        }

        private void issueDrivingLicenseFirstTime_Click(object sender, EventArgs e)
        {
            int LocalApplicationID = (int)dgvLocalDrivingLicenseApplicatios.CurrentRow.Cells[0].Value;

            frmIssueLocalLicenseForFirstTime frm = new frmIssueLocalLicenseForFirstTime(LocalApplicationID);
            frm.ShowDialog();

            _RefreshApplicationsList();
        }

        private void showLicense_Click(object sender, EventArgs e)
        {
            int LocalApplicationID = (int)dgvLocalDrivingLicenseApplicatios.CurrentRow.Cells[0].Value;

            int LicenseID = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseID(LocalApplicationID).GetLicenseByPersonID();

            if (LicenseID != -1)
            {
                frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show($"No License Found With LicenseID = [{LicenseID}]",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            
        }

        private void showLicensePersonHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalApplicationID = (int)dgvLocalDrivingLicenseApplicatios.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplication LocalApplicationInfo = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseID(LocalApplicationID);
           

            
                frmShowLicensesHistory showLicenseHistory = new frmShowLicensesHistory(LocalApplicationInfo.ApplicantPersonID);
                showLicenseHistory.ShowDialog();
           
        }
    }
}
