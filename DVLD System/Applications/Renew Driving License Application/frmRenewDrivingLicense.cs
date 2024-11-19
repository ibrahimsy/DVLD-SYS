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

namespace DVLD_System.Applications.Renew_Driving_License_Application
{
    public partial class frmRenewDrivingLicense : Form
    {
        int _LicenseID = -1;
        clsLicense _OldLicenseInfo;
        clsLicense _RenewedLicenseInfo;
        clsApplication _RenewApplication;
        public frmRenewDrivingLicense()
        {
            InitializeComponent();
        }

        bool _HandleExpiredLicense()
        {
            if (DateTime.Compare(_OldLicenseInfo.ExpirationDate, DateTime.Now) > 0)
            {
                MessageBox.Show($"Selected License Is Not Expired Yet,It Will Expire On :" +
                    $" \n {clsFormat.DateToShort(_OldLicenseInfo.ExpirationDate)}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                btnRenew.Enabled = false;
                llShowLicenseInfo.Enabled = false;

                return false;
            }
            return true;
        }

        bool _HandleActiveLicenseConstraint()
        {
           if(!_OldLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License Is Not Active",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                
                btnRenew.Enabled = false;
                llShowLicenseInfo.Enabled = false;

                return false;
            }

           btnRenew.Enabled = true;
            return true;
        }
       
        
        private void ctrlLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _LicenseID = obj;
            _OldLicenseInfo = clsLicense.Find(_LicenseID);
            if (_OldLicenseInfo == null)
            {
                ctrlLicenseInfoWithFilter1.ResetLicenseInfo();
                llShowLicenseHistory.Enabled = false;
                return;
            }

            llShowLicenseHistory.Enabled = true;

            if (!_HandleExpiredLicense())
            {
                return;
            }

            if (!_HandleActiveLicenseConstraint())
            {
                return;
            }

                lblApplicationDate.Text = clsFormat.DateToShort( DateTime.Now);
                lblIssueDate.Text = clsFormat.DateToShort (DateTime.Now);
                lblApplicationFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationTypes.enRenewDrivingLicense).Fees.ToString();
                lblLicenseFees.Text = _OldLicenseInfo.PaidFees.ToString();
                lblOldLicenseID.Text = _OldLicenseInfo.LicenseID.ToString();
                lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(clsLicenseClass.Find(_OldLicenseInfo.LicenseClass).DefaultValidityLength));
                lblCreatedBy.Text = clsGlobalSettings.CurrentUser.UserName;
                lblTotalFees.Text = (Convert.ToSingle(lblApplicationFees.Text) + Convert.ToSingle(lblLicenseFees.Text)).ToString();
                
            
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private bool _HandleRenewApplication()
        {
             _RenewApplication = new clsApplication();

            _RenewApplication.ApplicantPersonID = clsLicense.Find(_LicenseID).DriverInfo.PersonID;
            _RenewApplication.ApplicationDate = DateTime.Now;
            _RenewApplication.ApplicationTypeID = (int)clsApplication.enApplicationTypes.enRenewDrivingLicense;
            _RenewApplication.ApplicationStatus = (int)clsApplication.enApplicationStatus.enCompleted;
            _RenewApplication.LastStatusDate = DateTime.Now;
            _RenewApplication.PaidFees = clsApplicationTypes.Find((int)clsApplication.enApplicationTypes.enRenewDrivingLicense).Fees;
            _RenewApplication.CreatedByUserID = clsGlobalSettings.CurrentUser.UserID;

            if (!_RenewApplication.Save())
            {
                MessageBox.Show("An Error Occurred", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        
        private void btnRenew_Click(object sender, EventArgs e)
        {

            if (!_HandleRenewApplication())
            {
                return;
            }

            _RenewedLicenseInfo = new clsLicense();
            _RenewedLicenseInfo.ApplicationID = _RenewApplication.ApplicationID;
            _RenewedLicenseInfo.DriverID = _OldLicenseInfo.DriverID;
            _RenewedLicenseInfo.LicenseClass = _OldLicenseInfo.LicenseClass;
            _RenewedLicenseInfo.IssueDate = DateTime.Now;
            _RenewedLicenseInfo.ExpirationDate = DateTime.Now.AddYears(clsLicenseClass.Find(_OldLicenseInfo.LicenseClass).DefaultValidityLength);
            _RenewedLicenseInfo.Notes = txtNotes.Text;
            _RenewedLicenseInfo.PaidFees = _OldLicenseInfo.PaidFees;
            _RenewedLicenseInfo.IsActive = true;
            _RenewedLicenseInfo.IssueReason = clsLicense.enIssueReson.enRenew;
            _RenewedLicenseInfo.CreatedByUserID = clsGlobalSettings.CurrentUser.UserID;

            if (!_RenewedLicenseInfo.Save()) 
            {
                MessageBox.Show("An Error Occurred", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            MessageBox.Show($"The License Renewed Successfuly,With License ID = [{_RenewedLicenseInfo.LicenseID}]",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            _OldLicenseInfo.IsActive = false;
            _OldLicenseInfo.Save();

            lblRLApplicationID.Text = _RenewApplication.ApplicationID.ToString();
            lblRenewedLicenseID.Text = _RenewedLicenseInfo.LicenseID.ToString();
            lblOldLicenseID.Text = _OldLicenseInfo.LicenseID.ToString();
            
            btnRenew.Enabled = false;
            llShowLicenseInfo.Enabled = true; 
            ctrlLicenseInfoWithFilter1.FilterEnable = false;
        }

        private void frmRenewDrivingLicense_Load(object sender, EventArgs e)
        {
            btnRenew.Enabled = false;
            llShowLicenseInfo.Enabled = false;
            llShowLicenseHistory.Enabled = false;   
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_RenewedLicenseInfo.LicenseID);
            frm.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicensesHistory frm = new frmShowLicensesHistory(_OldLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }
    }
}
