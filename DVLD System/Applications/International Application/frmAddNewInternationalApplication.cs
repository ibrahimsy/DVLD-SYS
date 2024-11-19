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
        int _LicenseID = -1;
        public frmAddNewInternationalApplication()
        {
            InitializeComponent();
        }

        public frmAddNewInternationalApplication(int LicenseID)
        {
            InitializeComponent();
            _LicenseID = LicenseID;
        }

        private void frmAddNewInternationalApplication_Load(object sender, EventArgs e)
        {
            if (_LicenseID != -1)
            {
                ctrlLicenseInfoWithFilter1.FilterEnable = false;
                ctrlLicenseInfoWithFilter1.LoadInfo(_LicenseID);

                llShowLicenseHistory.Enabled = true;              
                btnIssueInternationalLicense.Enabled = true;

            }
            else
            {
                btnIssueInternationalLicense.Enabled = false;
                llShowLicenseHistory.Enabled = false;
            }

            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationTypes.enNewInternationalLicense).Fees.ToString();
            //lblLocalLicenseID.Text = ctrlLicenseInfoWithFilter1.LicenseID.ToString();
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(1));
            lblCreatedBy.Text = clsGlobalSettings.CurrentUser.UserName;
            llShowLicenseInfo.Enabled = false;

        }

        private void ctrlLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _LicenseID = obj;
            lblLocalLicenseID.Text = _LicenseID.ToString();

            btnIssueInternationalLicense.Enabled = true;
            llShowLicenseHistory.Enabled = true;
        }

        private void ctrlLicenseInfoWithFilter1_Load(object sender, EventArgs e)
        {
            //if (_LicenseID != -1)
            //{
            //    ctrlLicenseInfoWithFilter1.FilterEnable = false;
            //    ctrlLicenseInfoWithFilter1.LoadInfo(_LicenseID);
            //}
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int InternationalLicenseID = int.Parse(lblILicenseID.Text);
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(InternationalLicenseID);
            frm.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicensesHistory frm = new frmShowLicensesHistory(clsLicense.Find(_LicenseID).DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private bool _IssueInternationalLicense()
        {
            
            clsApplication InternationalApplication = new clsApplication();
            InternationalApplication.ApplicantPersonID = clsLicense.Find(_LicenseID).DriverInfo.PersonID;
            InternationalApplication.ApplicationDate = DateTime.Now;
            InternationalApplication.ApplicationTypeID = (int)clsApplication.enApplicationTypes.enNewInternationalLicense;
            InternationalApplication.ApplicationStatus = (int)clsApplication.enApplicationStatus.enCompleted;
            InternationalApplication.LastStatusDate = DateTime.Now;
            InternationalApplication.PaidFees = clsApplicationTypes.Find((int)clsApplication.enApplicationTypes.enNewInternationalLicense).Fees;
            InternationalApplication.CreatedByUserID = clsGlobalSettings.CurrentUser.UserID;

            if (!InternationalApplication.Save())
            {
                return false;
            }

            clsInternationalLicense InternationalLicense = new clsInternationalLicense();

            InternationalLicense.ApplicationID = InternationalApplication.ApplicationID;
            InternationalLicense.DriverID = clsLicense.Find(_LicenseID).DriverID;
            InternationalLicense.IssuedUsingLocalLicenseID = _LicenseID;
            InternationalLicense.IssueDate = DateTime.Now;
            InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);
            InternationalLicense.IsActive = true;
            InternationalLicense.CreatedByUserID = InternationalApplication.CreatedByUserID;

            if (!InternationalLicense.Save())
            {
                return false;
            }

            lblILApplicationID.Text = InternationalLicense.ApplicationID.ToString();
            lblILicenseID.Text = InternationalLicense.InternationalLicenseID.ToString();
            llShowLicenseInfo.Enabled = true;

            return true;

        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (_LicenseID == -1)
            {
                return;
            }

            clsLicense _LicenseInfo = clsLicense.Find(_LicenseID);

            if (_LicenseInfo == null)
            {
                MessageBox.Show($"No License Found With This ID=[{_LicenseID}]",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (_LicenseInfo.LicenseClass != (int)clsLicenseClass.enLicenseClass.enOrdinary)
            {
                MessageBox.Show($"License Must Be Class-3 Ordinary Driving License",
                   "Error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                return;
            }

            if (DateTime.Compare(DateTime.Now, _LicenseInfo.ExpirationDate) > 0)
            {
                MessageBox.Show($"License With ID=[{_LicenseID}] Is Expired",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (!_LicenseInfo.IsActive)
            {
                MessageBox.Show($"License With ID=[{_LicenseID}] Is Not Active",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (_LicenseInfo.DriverInfo.HasInternationalLicense())
            {
                MessageBox.Show("The Driver Has Allready An Active International License",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }


            if (_IssueInternationalLicense())
            {
                MessageBox.Show($"The License Issued Successfuly With ID = {lblILicenseID.Text}",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                
                btnIssueInternationalLicense.Enabled = false;
            }
            else
            {
                MessageBox.Show($"The License Not Issued ,Somthing Went Wrong",
                "Faild",
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
