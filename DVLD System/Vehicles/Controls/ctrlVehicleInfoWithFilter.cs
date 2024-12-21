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

namespace DVLD_System.Vehicles.Controls
{
    public partial class ctrlVehicleInfoWithFilter : UserControl
    {

        public event Action<int> OnVehicleSelected;
        protected virtual void VehicleSelected(int VehicleID)
        {
            Action<int> handler = OnVehicleSelected;
            if (handler != null)
                handler(VehicleID);
        }
     
        bool _FilterEnabled = true;

        public bool FilterEnabled
        {
            set
            {
                _FilterEnabled = value;
                gpFilterBy.Enabled = _FilterEnabled;
            }

            get
            {
                return _FilterEnabled;
            }
        }

        public int VehicleID
        {
            get
            {
                return ctrlVehicleInfo1.VehicleID;
            }
        }

        public clsVehichle VehicleInfo
        {
            get
            {
                return ctrlVehicleInfo1.VehichleInfo;
            }
        }


        public ctrlVehicleInfoWithFilter()
        {
            InitializeComponent();
        }

        void _FindNow()
        {
            switch (cbFilterBy.Text)
            {
                case "Vehicle ID":
                    ctrlVehicleInfo1.LoadInfoByVehcileID(Convert.ToInt32(txtFilterValue.Text));
                    break;
                case "Chassis Number":
                    ctrlVehicleInfo1.LoadInfoByChassisNumber(txtFilterValue.Text);
                    break;
                case "Plate Number":
                    ctrlVehicleInfo1.LoadInfoByPlateNumber(txtFilterValue.Text);
                    break;
            }

            if (OnVehicleSelected != null && FilterEnabled)
                OnVehicleSelected(ctrlVehicleInfo1.VehicleID);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            TextFocus();
            _FindNow();
        }

        public void LoadData(int VehicleID)
        {
            TextFocus();

            cbFilterBy.Text = "Vehicle ID";
            txtFilterValue.Text = VehicleID.ToString();

            _FindNow();
        }

        public void TextFocus()
        {
            txtFilterValue.Focus();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Vehicle ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }

            if (e.KeyChar == (char)13)
            {
                _FindNow();
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Clear();
            TextFocus();
        }

        private void txtFilterValue_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilterValue.Text))
            {
                errorProvider1.SetError(txtFilterValue,"This Feild Is Required");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtFilterValue, null);
            }
        }
    }
}
