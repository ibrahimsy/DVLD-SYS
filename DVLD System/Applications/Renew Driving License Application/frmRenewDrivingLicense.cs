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
        int _NewLicenseID = -1;
        //clsLicense _OldLicenseInfo;
       
        
        public frmRenewDrivingLicense()
        {
            InitializeComponent();
        }
   
        private void ctrlLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;
            lblOldLicenseID.Text = SelectedLicenseID.ToString();
            llShowLicenseHistory.Enabled = (SelectedLicenseID != -1);

            if (SelectedLicenseID == -1)
            {
                return;
            }

            lblLicenseFees.Text = ctrlLicenseInfoWithFilter1.LicenseInfo.LicenseClassInfo.ClassFees.ToString();  
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(ctrlLicenseInfoWithFilter1.LicenseInfo.LicenseClassInfo.DefaultValidityLength));          
            lblTotalFees.Text = (Convert.ToSingle(lblApplicationFees.Text) + Convert.ToSingle(lblLicenseFees.Text)).ToString();
            txtNotes.Text = ctrlLicenseInfoWithFilter1.LicenseInfo.Notes;

            if (!ctrlLicenseInfoWithFilter1.LicenseInfo.IsLicenseExpired())
            {
                MessageBox.Show($"The License Is Not Expired yet ,It Will Expired At {clsFormat.DateToShort(ctrlLicenseInfoWithFilter1.LicenseInfo.ExpirationDate)}",
                                "Not Allowed",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            if (!ctrlLicenseInfoWithFilter1.LicenseInfo.IsActive)
            {
                MessageBox.Show($"The License Is Not Active ,Choose Another One",
                                "Not Allowed",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            btnRenew.Enabled = true;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void btnRenew_Click(object sender, EventArgs e)
        {

            clsLicense NewLicense = ctrlLicenseInfoWithFilter1.LicenseInfo.Renew(txtNotes.Text.Trim(),clsGlobalSettings.CurrentUser.UserID);

            if (NewLicense == null)
            {
                MessageBox.Show("Faild To Renew The License","Error",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }

            lblRLApplicationID.Text = NewLicense.ApplicationID.ToString();
            lblRenewedLicenseID.Text = NewLicense.LicenseID.ToString();
            _NewLicenseID = NewLicense.LicenseID;

            MessageBox.Show($"License Renewed Successfuly With ID [{NewLicense.LicenseID}]", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnRenew.Enabled = false;
            llShowLicenseInfo.Enabled = true; 
            ctrlLicenseInfoWithFilter1.FilterEnable = false;
        }

        private void frmRenewDrivingLicense_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = lblApplicationDate.Text;
            lblApplicationFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationTypes.enRenewDrivingLicense).Fees.ToString();
            lblCreatedBy.Text = clsGlobalSettings.CurrentUser.UserName;
            lblExpirationDate.Text = "[? ? ? ?]";
            
            btnRenew.Enabled = false;
            llShowLicenseInfo.Enabled = false;
            llShowLicenseHistory.Enabled = false;
            ctrlLicenseInfoWithFilter1.TextLicenseIDFocus();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_NewLicenseID);
            frm.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicensesHistory frm = new frmShowLicensesHistory(ctrlLicenseInfoWithFilter1.LicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }
    }
}
