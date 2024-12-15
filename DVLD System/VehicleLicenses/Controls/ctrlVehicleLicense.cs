using BankBussiness;
using DVLD_System.Global_Classes;
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
    public partial class ctrlVehicleLicense : UserControl
    {
        int _VehicleLicenseID = -1;
        public ctrlVehicleLicense()
        {
            InitializeComponent();
           
        }

        void LoadInfo(int VehicleLicenseID)
        {
            clsVehichleLicense VehicleLicnseInfo = clsVehichleLicense.FindVehichleLicenseByID(VehicleLicenseID);
            if (VehicleLicnseInfo != null)
            {
                MessageBox.Show($"No VehicleLicense With ID {VehicleLicenseID}","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            lblVehichleLicenseID.Text = VehicleLicnseInfo.VehichleLicenseID.ToString();
            lblApplicationID.Text = VehicleLicnseInfo.ApplicationID.ToString();
            lblVehicleID.Text = VehicleLicnseInfo.VehichleID.ToString();
            lblIssueDate.Text = clsFormat.DateToShort( VehicleLicnseInfo.IssuedDate);
        }
        

    }
}
