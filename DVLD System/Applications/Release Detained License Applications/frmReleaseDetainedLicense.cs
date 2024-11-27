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
        int _LicenseID = -1;
        clsLicense _LicenseInfo;
        clsDetainedLicense _DetainedLicense;
        clsApplication _ReleaseApplicationInfo;
        public frmReleaseDetainedLicense()
        {
            InitializeComponent();
        }

        public frmReleaseDetainedLicense(int LicenseID)
        {
            InitializeComponent();
            _LicenseID = LicenseID;
        }



        void _ResetDefaultInfo()
        {
            btnRelease.Enabled = false;
            llShowLicenseInfo.Enabled = false;
            llShowLicenseHistory.Enabled = false;
        }

        bool _HandelDetainedLicenseConstraint()
        {
            if (!_LicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License Is Not Detained,Choose Another One.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void ctrlLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _LicenseID = obj;
            _LicenseInfo = clsLicense.Find(_LicenseID);
            if (_LicenseInfo == null)
            {
                return;
            }

            lblLicenseID.Text = _LicenseID.ToString();

            //check if license allready detained to release it
            if (!_HandelDetainedLicenseConstraint())
                return;

            _DetainedLicense = clsDetainedLicense.FindByLicenseID(_LicenseID);

            lblDetainID.Text = _DetainedLicense.LicenseID.ToString();
            lblDetainDate.Text = clsFormat.DateToShort(_DetainedLicense.DetainDate);
            lblFineFees.Text = _DetainedLicense.FineFees.ToString();
            lblAppFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationTypes.enReleaseDetainedDrivingLicsense).Fees.ToString();
            lblCreatedBy.Text = clsGlobalSettings.CurrentUser.UserName;
            lblTotalFees.Text = (Convert.ToSingle(lblFineFees.Text) + Convert.ToSingle(lblAppFees.Text)).ToString();
            

            llShowLicenseHistory.Enabled = true;
            btnRelease.Enabled = true;
            
        }

        private void frmReleaseDetainedLicense_Load(object sender, EventArgs e)
        {
            _ResetDefaultInfo();

            if (_LicenseID != -1)
            {
                ctrlLicenseInfoWithFilter1.FilterEnable = false;
                ctrlLicenseInfoWithFilter1.LoadInfo(_LicenseID);
            }
        }

        bool _HandelReleaseDetainedLicenseApplication()
        {
             _ReleaseApplicationInfo = new clsApplication();

            _ReleaseApplicationInfo.ApplicantPersonID = _LicenseInfo.DriverInfo.PersonID;
            _ReleaseApplicationInfo.ApplicationDate = DateTime.Now;
            _ReleaseApplicationInfo.ApplicationStatus = (int)clsApplication.enApplicationStatus.enCompleted;
            _ReleaseApplicationInfo.ApplicationTypeID = (int)clsApplication.enApplicationTypes.enReleaseDetainedDrivingLicsense;
            _ReleaseApplicationInfo.CreatedByUserID = clsGlobalSettings.CurrentUser.UserID;
            _ReleaseApplicationInfo.LastStatusDate = DateTime.Now;
            _ReleaseApplicationInfo.PaidFees = clsApplicationTypes.Find((int)clsApplication.enApplicationTypes.enReleaseDetainedDrivingLicsense).Fees;

            if (_ReleaseApplicationInfo.Save())
            {
                lblApplicationID.Text = _ReleaseApplicationInfo.ApplicationID.ToString();
                return true;
            }
            else
            {
                MessageBox.Show("An Error Occurred,Release Application is not completed",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

        }
        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (!_HandelReleaseDetainedLicenseApplication())
            {
                return;
            }

            _DetainedLicense.IsReleased = true;
            _DetainedLicense.ReleaseDate = DateTime.Now;
            _DetainedLicense.ReleaseApplicationID = _ReleaseApplicationInfo.ApplicationID;
            _DetainedLicense.ReleasedByUserID = clsGlobalSettings.CurrentUser.UserID;

            if (!_DetainedLicense.Save())
            {
                MessageBox.Show("Release License Faild,An Error Occurred",
                   "Error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error);

                return;
            }

            MessageBox.Show("License Released Successfully",
                   "Success",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Information);

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
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_LicenseID);
            frm.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicensesHistory frm = new frmShowLicensesHistory(clsLicense.Find(_LicenseID).DriverInfo.PersonID);
            frm.ShowDialog();
        }
    }
}
