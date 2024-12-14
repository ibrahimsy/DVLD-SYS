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

namespace DVLD_System.VehicleLicenses
{
    public partial class frmIssueVehicleLicense : Form
    {
        int _SelectedVehicleID = -1;

        int _VehicleID = -1;
        public frmIssueVehicleLicense()
        {
            InitializeComponent();
        }

        public frmIssueVehicleLicense(int VehicleID)
        {
            InitializeComponent();
            _VehicleID = VehicleID;
            ctrlVehicleInfoWithFilter1.LoadData(_VehicleID);
            ctrlVehicleInfoWithFilter1.FilterEnabled = false;
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
            {
                ResetData();
                return;
            }

            if (ctrlVehicleInfoWithFilter1.VehicleInfo.HasActiveLicense())
            {
                MessageBox.Show("Vehicle Has Active License,Choose Another Vehicle","Not Allowed",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            lblVehicleID.Text = ctrlVehicleInfoWithFilter1.VehicleID.ToString();
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(1));
            lblApplicationFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationTypes.IssueVehicleLicense).Fees.ToString();
            lblLicenseFees.Text = clsSetting.FindSettingByID((int)clsSetting.enSettings.VehicleFee).SettingValue.ToString();
            lblCreatedBy.Text = clsGlobalSettings.CurrentUser.UserID.ToString();

            btnIssueLicense.Enabled = true;
            llShowVehicleLicenseHistory.Enabled = true;
            
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            int ApplicationID = -1;
            int VehicleLicenseID = ctrlVehicleInfoWithFilter1.VehicleInfo.IssueLicense(clsGlobalSettings.CurrentUser.UserID,ref ApplicationID);

            if (VehicleLicenseID != -1) 
            {
                MessageBox.Show("Vehicle License Created Successfuly","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                lblVehicleLicenseID.Text = VehicleLicenseID.ToString();
                lblApplicationID.Text = ApplicationID.ToString();
                
                
                llbShowVehicleLicense.Enabled = true;
                btnIssueLicense.Enabled = false;
                ctrlVehicleInfoWithFilter1.FilterEnabled = false;
            }
            else
            {
                MessageBox.Show("Vehicle License Havin't Created ,An Error Occurred", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ResetData()
        {
            lblVehicleID.Text = "[? ? ? ?]";
            lblApplicationDate.Text = "[? ? ? ?]";
            lblIssueDate.Text = "[? ? ? ?]";
            lblExpirationDate.Text = "[? ? ? ?]";
            lblApplicationFees.Text = "[? ? ? ?]";
            lblLicenseFees.Text = "[? ? ? ?]";
            lblCreatedBy.Text = "[? ? ? ?]";
        }

        private void frmIssueVehicleLicense_Load(object sender, EventArgs e)
        {
           
        }
    }
}
