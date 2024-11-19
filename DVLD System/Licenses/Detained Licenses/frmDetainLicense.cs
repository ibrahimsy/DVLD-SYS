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

namespace DVLD_System.Licenses.Detain_Licenses
{
    public partial class frmDetainLicense : Form
    {
        int _LicenseID = -1;
        clsLicense _LicenseInfo;
        public frmDetainLicense()
        {
            InitializeComponent();
        }

        bool _HandelDetainedLicense()
        {
            if (_LicenseInfo.IsDetained())
            {
                MessageBox.Show("Selected License Allready Detained","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        void _ResetDefaultInfo()
        {
            lblDetainDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblCreatedBy.Text = clsGlobalSettings.CurrentUser.UserName;    

            btnDetain.Enabled = false;
            llShowLicenseHistory.Enabled = false;
            llShowLicenseInfo.Enabled = false;
            ctrlLicenseInfoWithFilter1.Focus();
        }
        private void ctrlLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _LicenseID = obj;
            _LicenseInfo = clsLicense.Find(_LicenseID);

            if (_LicenseInfo == null)
            {
                return;
            }

            llShowLicenseHistory.Enabled = true;
            lblLicenseID.Text = _LicenseID.ToString();

            if (!_HandelDetainedLicense())
            {
                return;
            }

            btnDetain.Enabled = true;

        }

        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            _ResetDefaultInfo();

        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                return;
            }
            /*
             LicenseID
             DetainDate
             FineFees
             CreatedByUserID
             IsReleased
             ReleaseDate
             ReleasedByUserID
             ReleaseApplicationID
             */

            clsDetainedLicense _DetainedLicense = new clsDetainedLicense();
            _DetainedLicense.LicenseID = _LicenseID;
            _DetainedLicense.DetainDate  = DateTime.Now;
            _DetainedLicense.FineFees = Convert.ToSingle(txtDetainFees.Text.Trim());
            _DetainedLicense.CreatedByUserID = clsGlobalSettings.CurrentUser.UserID;

            if (_DetainedLicense.Save())
            {
                MessageBox.Show($"Selected Licesne Is Detaind Successfuly With Detain ID = [{_DetainedLicense.DetainID}]",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                lblLicenseID.Text = _LicenseID.ToString();
                lblDetainID.Text = _DetainedLicense.DetainID.ToString();
                llShowLicenseInfo.Enabled = true;
                btnDetain.Enabled = false;
                ctrlLicenseInfoWithFilter1.FilterEnable = false;
            }
            else
            {
                MessageBox.Show($"An Error Occurred During Detain The License",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

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
            frmShowLicensesHistory frm = new frmShowLicensesHistory(_LicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void txtDetainFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDetainFees.Text))
            {
                errorProvider1.SetError(txtDetainFees,"This Feild Is Required");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtDetainFees, "");
                e.Cancel = false;
            }
        }

        private void txtDetainFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void frmDetainLicense_Activated(object sender, EventArgs e)
        {
            ctrlLicenseInfoWithFilter1.TxtLicenseIDFocus();
        }
    }
}
