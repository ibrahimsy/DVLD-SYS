using DVLD_Bussiness;
using DVLD_System.Global_Classes;
using DVLD_System.Licenses;
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

namespace DVLD_System.Applications.Replacment_License_Applications
{
    public partial class frmReplacmentLicenseApplication : Form
    {
        enum enReplacmentMode { enDamageLicense = 1,enLostLicense = 2};
        enReplacmentMode _ReplacmentMode = enReplacmentMode.enDamageLicense;

        int _LicenseID = -1;
        clsLicense _OldLicenseInfo;
        clsApplication _ReplacmentApplication;
        clsLicense _ReplacmentLicenseInfo;
        public frmReplacmentLicenseApplication()
        {
            InitializeComponent();
        }

        void _ChangeReplacmentMode()
        {
            
            if (rbDamaged.Checked)
            {
                _ReplacmentMode = enReplacmentMode.enDamageLicense;
                this.Text = "Replacment For Damaged License";
                lblTitle.Text = this.Text;
                lblApplicationFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationTypes.enReplacementForDamagedDrivingLicense).Fees.ToString();
            }
            else
            {
                _ReplacmentMode = enReplacmentMode.enLostLicense;
                this.Text = "Replacment For Lost License";
                lblTitle.Text = this.Text;
                lblApplicationFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationTypes.enReplacementForLostDrivingLicense).Fees.ToString();
            }
        }
        
        void _ResetDefaultInfo()
        {
            rbDamaged.Checked = true;
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblCreatedBy.Text = clsGlobalSettings.CurrentUser.UserName;
            llShowLicenseHistory.Enabled = false;
            llShowLicenseInfo.Enabled = false;
            btnIssueReplacment.Enabled = false;

        }
        private void frmReplacmentLicenseApplication_Load(object sender, EventArgs e)
        {
            _ResetDefaultInfo();

        }

        private void rbDamaged_CheckedChanged_1(object sender, EventArgs e)
        {
            _ChangeReplacmentMode();
        }

        bool _HandleActiveLicenseConstraint()
        {
            if (!_OldLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License Is Not Active",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                btnIssueReplacment.Enabled = false;
                llShowLicenseInfo.Enabled = false;

                return false;
            }

            btnIssueReplacment.Enabled = true;
            return true;
        }

        private void ctrlLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _LicenseID = obj;
            _OldLicenseInfo = clsLicense.Find(_LicenseID);
            if (_OldLicenseInfo == null) 
            {
                MessageBox.Show($"No Licesne Found With ID = {_LicenseID}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (!_HandleActiveLicenseConstraint())
            {
                return;
            }

            llShowLicenseHistory.Enabled = true;
            lblOldLicenseID.Text = _LicenseID.ToString();
            btnIssueReplacment.Enabled = true;
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_ReplacmentLicenseInfo.LicenseID);
            frm.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicensesHistory frm = new frmShowLicensesHistory(_OldLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        bool _HandleReplacmentApplication()
        {
            _ReplacmentApplication = new clsApplication();

            _ReplacmentApplication.ApplicantPersonID = _OldLicenseInfo.DriverInfo.PersonID;
            _ReplacmentApplication.ApplicationDate = DateTime.Now;
            _ReplacmentApplication.ApplicationStatus = (int)clsApplication.enApplicationStatus.enCompleted;

            if (_ReplacmentMode == enReplacmentMode.enDamageLicense)
                _ReplacmentApplication.ApplicationTypeID = (int)clsApplication.enApplicationTypes.enReplacementForDamagedDrivingLicense;
            else
                _ReplacmentApplication.ApplicationTypeID = (int)clsApplication.enApplicationTypes.enReplacementForLostDrivingLicense;
            
            _ReplacmentApplication.LastStatusDate = DateTime.Now;
            _ReplacmentApplication.PaidFees = Convert.ToSingle(lblApplicationFees.Text);
            _ReplacmentApplication.CreatedByUserID = clsGlobalSettings.CurrentUser.UserID;

            if (!_ReplacmentApplication.Save())
            {
                MessageBox.Show("An Error Occurred", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void btnIssueReplacment_Click(object sender, EventArgs e)
        {
            if (!_HandleReplacmentApplication())
            {
                return;
            }

            _ReplacmentLicenseInfo = new clsLicense();
            _ReplacmentLicenseInfo.ApplicationID = _ReplacmentApplication.ApplicationID;
            _ReplacmentLicenseInfo.DriverID = _OldLicenseInfo.DriverID;
            _ReplacmentLicenseInfo.LicenseClass = _OldLicenseInfo.LicenseClass;
            _ReplacmentLicenseInfo.IssueDate = DateTime.Now;
            _ReplacmentLicenseInfo.ExpirationDate = DateTime.Now.AddYears(clsLicenseClass.Find(_OldLicenseInfo.LicenseClass).DefaultValidityLength);
            
            _ReplacmentLicenseInfo.PaidFees = _OldLicenseInfo.PaidFees;
            _ReplacmentLicenseInfo.IsActive = true;

            if (_ReplacmentMode == enReplacmentMode.enDamageLicense)
                _ReplacmentLicenseInfo.IssueReason = clsLicense.enIssueReson.enDamageReplacment;
            else
                _ReplacmentLicenseInfo.IssueReason = clsLicense.enIssueReson.enLostReplacement;

            _ReplacmentLicenseInfo.CreatedByUserID = clsGlobalSettings.CurrentUser.UserID;


            if (!_ReplacmentLicenseInfo.Save())
            {
                MessageBox.Show("An Error Occurred", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            MessageBox.Show($"The License Replacmented Successfuly,With License ID = [{_ReplacmentLicenseInfo.LicenseID}]",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            _OldLicenseInfo.IsActive = false;
            _OldLicenseInfo.Save();

            lblLRApplicationID.Text = _ReplacmentApplication.ApplicationID.ToString();
            lblReplacmentLicenseID.Text = _ReplacmentLicenseInfo.LicenseID.ToString();
            lblOldLicenseID.Text = _OldLicenseInfo.LicenseID.ToString();

            btnIssueReplacment.Enabled = false;
            llShowLicenseInfo.Enabled = true;
            ctrlLicenseInfoWithFilter1.FilterEnable = false;

        }
    }
}
