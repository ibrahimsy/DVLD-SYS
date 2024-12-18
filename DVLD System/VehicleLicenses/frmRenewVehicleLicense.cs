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

namespace DVLD_System.VehicleLicenses
{
    public partial class frmRenewVehicleLicense : Form
    {
        int _SelectedVehicleID = -1;
        public frmRenewVehicleLicense()
        {
            InitializeComponent();
        }

        private void btnRenewLicense_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }

        private void ctrlVehicleInfoWithFilter1_OnVehicleSelected(int obj)
        {
            _SelectedVehicleID = obj;

            if (_SelectedVehicleID == -1)
            {
                MessageBox.Show($"No Vehicle License With ID [{_SelectedVehicleID}]","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            clsVehichleLicense VehicleLicense = clsVehichleLicense.Find


        }
    }
}
