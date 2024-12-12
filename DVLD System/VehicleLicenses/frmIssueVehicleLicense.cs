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
    public partial class frmIssueVehicleLicense : Form
    {
        int _SelectedVehicleID = -1;
        public frmIssueVehicleLicense()
        {
            InitializeComponent();
        }

        private void frmIssueVehicleLicense_Activated(object sender, EventArgs e)
        {
            ctrlVehicleInfoWithFilter1.TextFocus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlVehicleInfoWithFilter1_OnVehicleSelected(int obj)
        {
            _SelectedVehicleID = obj;
            //MessageBox.Show($"Selected Vehicle ID : [{_SelectedVehicleID}]");
            if (_SelectedVehicleID == -1)
                return;
            btnIssueLicense.Enabled = true;
            llShowVehicleLicenseHistory.Enabled = true;
            
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            int VehicleLicenseID = ctrlVehicleInfoWithFilter1.VehicleInfo.IssueLicense(clsGlobalSettings.CurrentUser.UserID);

            if (VehicleLicenseID != -1) 
            {
                MessageBox.Show("Vehicle License Created Successfuly","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                llbShowVehicleLicense.Enabled = true;
                btnIssueLicense.Enabled = false;
                ctrlVehicleInfoWithFilter1.FilterEnabled = false;
            }
            else
            {
                MessageBox.Show("Vehicle License Havin't Created ,An Error Occurred", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frmIssueVehicleLicense_Load(object sender, EventArgs e)
        {
            
        }
    }
}
