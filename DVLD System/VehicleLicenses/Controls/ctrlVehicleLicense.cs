using BankBussiness;
using DVLD_Bussiness;
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
        clsVehichleLicense _VehicleLicnseInfo;
        public ctrlVehicleLicense()
        {
            InitializeComponent();
           
        }

        public int VehicleLicenseID
        {
            get
            {
                return _VehicleLicenseID;
            }
        }
        public clsVehichleLicense VehicleLicenseInfo
        {
            get 
            {
                return _VehicleLicnseInfo;
            }

        }

        public void LoadInfo(int VehicleLicenseID)
        {
             _VehicleLicnseInfo = clsVehichleLicense.FindByID(VehicleLicenseID);
            if (_VehicleLicnseInfo == null)
            {
                MessageBox.Show($"No VehicleLicense With ID {VehicleLicenseID}","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                _VehicleLicenseID = -1;
                return;
            }

            _VehicleLicenseID = VehicleLicenseID;

            lblVehichleLicenseID.Text = _VehicleLicnseInfo.VehichleLicenseID.ToString();
            lblApplicationID.Text = _VehicleLicnseInfo.ApplicationID.ToString();
            lblVehicleID.Text = _VehicleLicnseInfo.VehichleID.ToString();
            lblIssueDate.Text = clsFormat.DateToShort(_VehicleLicnseInfo.IssuedDate);
            lblLicenseFee.Text = _VehicleLicnseInfo.LicenseFee.ToString();
            lblIssueReason.Text = _VehicleLicnseInfo.IssueReasonText;
            lblStatus.Text = _VehicleLicnseInfo.StatusText;
            lblExpiryDate.Text = clsFormat.DateToShort(_VehicleLicnseInfo.ExpiryDate);
            lblCreatedBy.Text = clsUser.Find(_VehicleLicnseInfo.CreatedBy).UserName;
        }
        

    }
}
