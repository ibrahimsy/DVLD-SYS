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
            MessageBox.Show($"Selected Vehicle ID : [{_SelectedVehicleID}]");
        }
    }
}
