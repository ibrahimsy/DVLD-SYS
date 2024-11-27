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
        int _SelectedLicenseID = -1;
        int _DetainID = -1;
        public frmDetainLicense()
        {
            InitializeComponent();
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
            _SelectedLicenseID = obj;
          
            if (_SelectedLicenseID == -1)
            {
                return;
            }

            llShowLicenseHistory.Enabled = (_SelectedLicenseID != -1);

            lblLicenseID.Text = _SelectedLicenseID.ToString();

            if (ctrlLicenseInfoWithFilter1.LicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License Allready Detained", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            int DetainID = ctrlLicenseInfoWithFilter1.LicenseInfo.Detain(
                                    Convert.ToSingle(txtDetainFees.Text.Trim()),
                                    clsGlobalSettings.CurrentUser.UserID);
            if (DetainID == -1)
            {
                MessageBox.Show($"An Error Occurred During Detain The License",
                   "Error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                return;
            }
            
            MessageBox.Show($"Selected Licesne Is Detaind Successfuly With Detain ID = [{DetainID}]",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

            lblLicenseID.Text = _SelectedLicenseID.ToString();
            lblDetainID.Text = DetainID.ToString();
            llShowLicenseInfo.Enabled = true;
            btnDetain.Enabled = false;
            ctrlLicenseInfoWithFilter1.FilterEnable = false;
            txtDetainFees.Enabled = false;

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
            frmShowLicensesHistory frm = new frmShowLicensesHistory(ctrlLicenseInfoWithFilter1.LicenseInfo.DriverInfo.PersonID);
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
            ctrlLicenseInfoWithFilter1.TextLicenseIDFocus();
        }
    }
}
