using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_System.Vehicles
{
    public partial class frmShowVehicleInfo : Form
    {
        int _VehicleID = -1;
        public frmShowVehicleInfo(int vehicleID)
        {
            InitializeComponent();
            _VehicleID = vehicleID;
        }

        private void frmShowVehicleInfo_Load(object sender, EventArgs e)
        {
            ctrlVehicleInfo1.LoadInfoByVehcileID(_VehicleID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
