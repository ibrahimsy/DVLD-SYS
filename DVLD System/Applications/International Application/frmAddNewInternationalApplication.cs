using DVLD_Bussiness;
using DVLD_System.Global_Classes;
using DVLD_System.Licenses.Local_Licenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_System.Licenses.International_Licenses
{
    public partial class frmAddNewInternationalApplication : Form
    {
        int _SelectedLicenseID = -1;
        public frmAddNewInternationalApplication()
        {
            InitializeComponent();
        }

        public frmAddNewInternationalApplication(int LicenseID)
        {
            InitializeComponent();
            _SelectedLicenseID = LicenseID;
            ctrlLicenseInfoWithFilter1.LoadInfo(_SelectedLicenseID);
            ctrlLicenseInfoWithFilter1.FilterEnable = false;
        }

        private void frmAddNewInternationalApplication_Load(object sender, EventArgs e)
        {
            btnIssueInternationalLicense.Enabled = false;
            llShowLicenseInfo.Enabled = false;
        }

        private void ctrlLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _SelectedLicenseID = obj;
            llShowLicenseHistory.Enabled = (_SelectedLicenseID != -1);
            if (_SelectedLicenseID == -1)
            {
                return;
            }
            int ActiveInternationalLicenseID = clsInternationalLicense.GetActiveInternationalLicenseByDriverID(ctrlLicenseInfoWithFilter1.LicenseInfo.DriverID);

            if (ActiveInternationalLicenseID != -1)
            {
                MessageBox.Show($"Person Allready Has An Active International License With ID [{ActiveInternationalLicenseID}]",
                  "Error",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Error);
                return;
            }

            if (ctrlLicenseInfoWithFilter1.LicenseInfo.LicenseClass != (int)clsLicenseClass.enLicenseClass.enOrdinary)
            {
                MessageBox.Show($"License Must Be Class-3 Ordinary Driving License",
                   "Error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                return;
            }

            if (ctrlLicenseInfoWithFilter1.LicenseInfo.IsLicenseExpired())
            {
                MessageBox.Show($"License With ID=[{_SelectedLicenseID}] Is Expired",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            if (!ctrlLicenseInfoWithFilter1.LicenseInfo.IsActive)
            {
                MessageBox.Show($"License With ID=[{_SelectedLicenseID}] Is Not Active",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }


            lblLocalLicenseID.Text = _SelectedLicenseID.ToString();
            btnIssueInternationalLicense.Enabled = true;
            
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationTypes.enNewInternationalLicense).Fees.ToString();
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(1));
            lblCreatedBy.Text = clsGlobalSettings.CurrentUser.UserName;
            llShowLicenseInfo.Enabled = false;
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int InternationalLicenseID = int.Parse(lblILicenseID.Text);
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(InternationalLicenseID);
            frm.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicensesHistory frm = new frmShowLicensesHistory(ctrlLicenseInfoWithFilter1.LicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private bool _IssueInternationalLicense()
        {


            clsInternationalLicense InternationalLicense = new clsInternationalLicense();
            
            InternationalLicense.DriverID = ctrlLicenseInfoWithFilter1.LicenseInfo.DriverID;
            InternationalLicense.IssuedUsingLocalLicenseID = _SelectedLicenseID;
            InternationalLicense.IssueDate = DateTime.Now;
            InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);
            InternationalLicense.IsActive = true;

            InternationalLicense.ApplicantPersonID = ctrlLicenseInfoWithFilter1.LicenseInfo.DriverInfo.PersonID;
            InternationalLicense.ApplicationDate = DateTime.Now;
            InternationalLicense.ApplicationTypeID = (int)clsApplication.enApplicationTypes.enNewInternationalLicense;
            InternationalLicense.ApplicationStatus = (int)clsApplication.enApplicationStatus.enCompleted;
            InternationalLicense.LastStatusDate = DateTime.Now;
            InternationalLicense.PaidFees = clsApplicationTypes.Find((int)clsApplication.enApplicationTypes.enNewInternationalLicense).Fees;
            InternationalLicense.CreatedByUserID = clsGlobalSettings.CurrentUser.UserID;

            if (!InternationalLicense.Save())
            {
                return false;
            }

            lblILApplicationID.Text = InternationalLicense.ApplicationID.ToString();
            lblILicenseID.Text = InternationalLicense.InternationalLicenseID.ToString();
            

            return true;

        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            
            if (_IssueInternationalLicense())
            {
                MessageBox.Show($"International License Issued Successfuly With ID = {lblILicenseID.Text}",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                
                btnIssueInternationalLicense.Enabled = false;
                llShowLicenseInfo.Enabled = true;
                ctrlLicenseInfoWithFilter1.FilterEnable = false;
            }
            else
            {
                MessageBox.Show($"International License Not Issued ,Somthing Went Wrong",
                "Issue Faild",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
