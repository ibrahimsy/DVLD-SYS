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

namespace DVLD_System.VehicleLicenses
{
    public partial class frmRenewVehicleLicense : Form
    {
        int _SelectedVehicleID = -1;
        clsVehichleLicense VehicleLicenseInfo;
        public frmRenewVehicleLicense()
        {
            InitializeComponent();
        }

        public frmRenewVehicleLicense(int VehicleID)
        {
            InitializeComponent();
            _SelectedVehicleID = VehicleID;
            ctrlVehicleInfoWithFilter1.LoadData(_SelectedVehicleID);
            ctrlVehicleInfoWithFilter1.FilterEnabled = false;
        }

        private void btnRenewLicense_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some Feild is incorrect,put mouse over red button to see error",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            clsVehichleLicense NewVehicleLicense = VehicleLicenseInfo.Renew(clsGlobalSettings.CurrentUser.UserID);
            if (NewVehicleLicense == null)
            {
                MessageBox.Show("An Error Occured ,License Hasn't Renewed",
                   "Error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                return;
            }

            lblRenewedLicenseID.Text = NewVehicleLicense.VehichleLicenseID.ToString();
            MessageBox.Show($"License Renewed Successfuly With New ID [{NewVehicleLicense.VehichleLicenseID}]",
                   "Success",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Information);


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlVehicleInfoWithFilter1_OnVehicleSelected(int obj)
        {
            _SelectedVehicleID = obj;

            if (_SelectedVehicleID == -1)
            {
                MessageBox.Show($"No Vehicle License With ID [{_SelectedVehicleID}]","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            VehicleLicenseInfo = clsVehichleLicense.FindByVehicleID(_SelectedVehicleID);
            if (VehicleLicenseInfo == null) 
            {
                MessageBox.Show($"No License Found For Vehicle With ID [{_SelectedVehicleID}]", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!VehicleLicenseInfo.IsActive)
            {
                MessageBox.Show($"License With ID [{VehicleLicenseInfo.VehichleLicenseID}] Is Not Active", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!VehicleLicenseInfo.IsExpired)
            {
                MessageBox.Show($"License With ID [{VehicleLicenseInfo.VehichleLicenseID}] Is Not Expired," +
                    $"\n Vehicle License Expired At {clsFormat.DateToShort( VehicleLicenseInfo.ExpiryDate)}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btnRenewLicense.Enabled = true;
            llShowVehicleLicenseHistory.Enabled = true;
            llbShowVehicleLicense.Enabled = true;

            lblApplicationID.Text = VehicleLicenseInfo.ApplicationID.ToString();
            lblApplicationDate.Text = clsFormat.DateToShort(VehicleLicenseInfo.ApplicationDate);
            lblIssueDate.Text = clsFormat.DateToShort(VehicleLicenseInfo.IssuedDate);
            lblExpirationDate.Text = clsFormat.DateToShort(VehicleLicenseInfo.ExpiryDate);
            lblApplicationFees.Text = VehicleLicenseInfo.PaidFees.ToString();
            lblLicenseFees.Text = VehicleLicenseInfo.LicenseFee.ToString();
            lblVehicleID.Text = VehicleLicenseInfo.VehichleID.ToString();
            lblVehicleLicenseID.Text = VehicleLicenseInfo.VehichleLicenseID.ToString();
            lblCreatedBy.Text = VehicleLicenseInfo.UserInfo.UserName;

        }
    }
}
