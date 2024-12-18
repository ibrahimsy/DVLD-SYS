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
    public partial class frmShowVehicleLicense : Form
    {
        int _VehicleLicenseID = -1;
        public frmShowVehicleLicense(int vehicleLicenseID)
        {
            InitializeComponent();
            _VehicleLicenseID = vehicleLicenseID;   
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowVehicleLicense_Load(object sender, EventArgs e)
        {
            ctrlVehicleLicense1.LoadInfo(_VehicleLicenseID);
        }
    }
}
