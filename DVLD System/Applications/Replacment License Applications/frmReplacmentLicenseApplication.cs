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
        clsLicense _NewLicenseInfo;
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
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_NewLicenseInfo.LicenseID);
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
        
        clsLicense.enIssueReson _GetIssueReason()
        {
            if (_ReplacmentMode == enReplacmentMode.enLostLicense)
                return clsLicense.enIssueReson.enLostReplacement;
            else
                return clsLicense.enIssueReson.enDamageReplacment;
        }

        private void btnIssueReplacment_Click(object sender, EventArgs e)
        {

            clsLicense NewLicense = _OldLicenseInfo.Replace(_GetIssueReason(),clsGlobalSettings.CurrentUser.UserID);

            if (NewLicense == null)
            {
                MessageBox.Show("Faild To Issue A Replacement For This License","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            
            lblLRApplicationID.Text = NewLicense.ApplicationID.ToString();
            _NewLicenseInfo = NewLicense;
            lblReplacmentLicenseID.Text = NewLicense.LicenseID.ToString();

            MessageBox.Show($"New License Issued Successfuly With ID [{NewLicense.LicenseID}]", "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnIssueReplacment.Enabled = false;
            llShowLicenseInfo.Enabled = true;
            ctrlLicenseInfoWithFilter1.FilterEnable = false;

        }
    }
}
