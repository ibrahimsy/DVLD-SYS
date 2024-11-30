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

namespace DVLD_System.Applications.Release_Detained_License_Applications
{
    public partial class frmReleaseDetainedLicense : Form
    {
        int _SelectedLicenseID = -1;

        public frmReleaseDetainedLicense()
        {
            InitializeComponent();
        }

        public frmReleaseDetainedLicense(int LicenseID)
        {
            InitializeComponent();
            _SelectedLicenseID = LicenseID;
            ctrlLicenseInfoWithFilter1.LoadInfo(_SelectedLicenseID);
            ctrlLicenseInfoWithFilter1.FilterEnable = false;
        }



        void _ResetDefaultInfo()
        {
            btnRelease.Enabled = false;
            llShowLicenseInfo.Enabled = false;
            llShowLicenseHistory.Enabled = false;
        }

        
        private void ctrlLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _SelectedLicenseID = obj;
            if (_SelectedLicenseID == -1)
            {
                return;
            }
            llShowLicenseHistory.Enabled = (_SelectedLicenseID != -1);
            
            lblLicenseID.Text = _SelectedLicenseID.ToString();

            //check if license allready detained to release it
            if (!ctrlLicenseInfoWithFilter1.LicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License Is Not Detained,Choose Another One.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return ;
            }


            lblDetainID.Text = ctrlLicenseInfoWithFilter1.LicenseInfo.DetainedInfo.DetainID.ToString();
            lblDetainDate.Text = clsFormat.DateToShort(ctrlLicenseInfoWithFilter1.LicenseInfo.DetainedInfo.DetainDate);
            lblFineFees.Text = ctrlLicenseInfoWithFilter1.LicenseInfo.DetainedInfo.FineFees.ToString();
            lblAppFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationTypes.enReleaseDetainedDrivingLicsense).Fees.ToString();
            lblCreatedBy.Text = clsGlobalSettings.CurrentUser.UserName;
            lblTotalFees.Text = (Convert.ToSingle(lblFineFees.Text) + Convert.ToSingle(lblAppFees.Text)).ToString();
            

            llShowLicenseHistory.Enabled = true;
            btnRelease.Enabled = true;
            
        }

        private void frmReleaseDetainedLicense_Load(object sender, EventArgs e)
        {
           
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            int ReleasedApplicationID = -1;

            bool IsReleased = ctrlLicenseInfoWithFilter1.LicenseInfo.ReleaseDetainedLicense(
                clsGlobalSettings.CurrentUser.UserID,
                ref ReleasedApplicationID);

            if (!IsReleased)
            {
                MessageBox.Show("The License Didn't Release.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("The License Released Successfuly.", "Released Successful", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            lblReleasedApplicationID.Text = ReleasedApplicationID.ToString();

            btnRelease.Enabled = false;
            llShowLicenseInfo.Enabled = true;
            ctrlLicenseInfoWithFilter1.FilterEnable = false;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_SelectedLicenseID);
            frm.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicensesHistory frm = new frmShowLicensesHistory(clsLicense.Find(_SelectedLicenseID).DriverInfo.PersonID);
            frm.ShowDialog();
        }
    }
}
