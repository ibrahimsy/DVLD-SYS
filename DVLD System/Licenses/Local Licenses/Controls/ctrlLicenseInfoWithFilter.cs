using DVLD_Bussiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_System.Licenses.Controls
{
    public partial class ctrlLicenseInfoWithFilter : UserControl
    {

        public event Action<int> OnLicenseSelected;

        protected virtual void LicenseSelected(int LicenseID)
        {
            Action<int> handler = OnLicenseSelected;

            if (handler != null) 
            {
                handler(LicenseID);
            }
        }

        int _LicenseID = -1;
        public int LicenseID
        {
            get
            {
                return _LicenseID;
            }
        } 
        
        bool _FilterEnabled = true;
        public bool FilterEnable
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gbFilter.Enabled = _FilterEnabled;
            }
        }
        
        public clsLicense LicenseInfo
        {
            get
            {
                return ctrlDriverLicenseInfo1.LicenseInfo;
            }
        }
        
        public ctrlLicenseInfoWithFilter()
        {
            InitializeComponent();
        }

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            if (e.KeyChar == (char)13)
            {
                btnSearch.PerformClick();
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("some feild are not valid,put mouse on red icon to see error ","Validation Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                txtLicenseID.Focus();
                return;
            }
            int LicenseID = int.Parse(txtLicenseID.Text.Trim());
            LoadInfo(LicenseID);
        }

        private void txtLicenseID_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtLicenseID.Text))
            {
                errorProvider1.SetError(txtLicenseID, "This Field Is Required");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtLicenseID, "");
            }
        }

        public void LoadInfo(int LicenseID)
        {
            ctrlDriverLicenseInfo1.LoadLicenseInfo(LicenseID);
            txtLicenseID.Text = LicenseID.ToString();
            txtLicenseID.Focus();

            _LicenseID = ctrlDriverLicenseInfo1.LicenseID;

            if (OnLicenseSelected != null && FilterEnable)
                OnLicenseSelected(_LicenseID);
        }
        
        public void ResetLicenseInfo()
        {
            ctrlDriverLicenseInfo1.ResetLicenseInfo();
        }
        
        public void TextLicenseIDFocus()
        {
            txtLicenseID.Focus();
        }
    }

}
