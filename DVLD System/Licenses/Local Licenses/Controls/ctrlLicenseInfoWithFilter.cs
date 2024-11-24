using DVLD_Bussiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        
        public bool FilterEnable
        {
            set
            {
                gbFilter.Enabled = value;
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
            if (string.IsNullOrEmpty(txtLicenseID.Text))
            {
                return;
            }
            int LicenseID = int.Parse(txtLicenseID.Text.Trim());

            if (!this.ValidateChildren())
            {
                return;
            }

            ctrlDriverLicenseInfo1.LoadLicenseInfo(LicenseID);

            if(OnLicenseSelected != null)
                OnLicenseSelected(LicenseID);
        }

        private void txtLicenseID_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtLicenseID.Text))
            {
                errorProvider1.SetError(txtLicenseID, "This Field Is Required");
                //e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtLicenseID, "");
                e.Cancel = false;
            }
        }

        public void LoadInfo(int LicenseID)
        {
            _LicenseID = LicenseID;
            ctrlDriverLicenseInfo1.LoadLicenseInfo(LicenseID);
            FindNow();
        }
        public void FindNow()
        {
            txtLicenseID.Text = LicenseID.ToString(); 
            txtLicenseID.Focus();

            if(OnLicenseSelected !=null)
                OnLicenseSelected(LicenseID);
        }
        
        public void ResetLicenseInfo()
        {
            ctrlDriverLicenseInfo1.ResetLicenseInfo();
        }
        public void TxtLicenseIDFocus()
        {
            txtLicenseID.Focus();
        }
    }
}
