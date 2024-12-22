using BankBussiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_System.VehicleLicenses.Controls
{
    public partial class ctrlVehicleLcenseWithFilter : UserControl
    {
        public event Action<int> OnVehicleLicensesSelected;
        protected virtual void VehicleLicensesSelected(int VehicleLicenseID)
        {
            Action<int> handler = OnVehicleLicensesSelected;
            if (handler != null)
            {
                handler(VehicleLicenseID);
            }
        }
        
        public bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            set
            {
                _FilterEnabled = value;
            }
            get
            {
                return _FilterEnabled;
            }
        }
        public clsVehichleLicense VehicleLicenseInfo
        {
            get
            {
                return ctrlVehicleLicense1.VehicleLicenseInfo;
            }
        }
        
        public ctrlVehicleLcenseWithFilter()
        {
            InitializeComponent();
        }

        private void btnFindLicense_Click(object sender, EventArgs e)
        {
            _FindNow();
        }
        
        private void _FindNow()
        {

            int VehicleLicenseID = int.Parse(txtVehicleLicenseID.Text);
            ctrlVehicleLicense1.LoadInfo(VehicleLicenseID);
            
            if (OnVehicleLicensesSelected != null && FilterEnabled)
            {
                OnVehicleLicensesSelected(VehicleLicenseID);
            }
        }

        public void FocusText()
        {
            txtVehicleLicenseID.Focus();
        }
        private void txtVehicleLicenseID_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtVehicleLicenseID.Text))
            {
                errorProvider1.SetError(txtVehicleLicenseID,"This Feild Is Required");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtVehicleLicenseID, null);
            }
        }

        private void txtVehicleLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            if (e.KeyChar == (char)13)
                _FindNow();
        }

        private void txtVehicleLicenseID_TextChanged(object sender, EventArgs e)
        {
           // _FindNow();
        }
    }
}
