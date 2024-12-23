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
    public partial class frmCancelVehicleLicense : Form
    {
        int _SelectedVehicleLicenseID = -1;

        clsVehichleLicense _VehicleLicenseInfo;
        public frmCancelVehicleLicense()
        {
            InitializeComponent();
        }

        private void ctrlVehicleLcenseWithFilter1_OnVehicleLicensesSelected(int obj)
        {
            _SelectedVehicleLicenseID = obj;
            if (_SelectedVehicleLicenseID == -1)
                return;
            _VehicleLicenseInfo = ctrlVehicleLcenseWithFilter1.VehicleLicenseInfo;
            if (_VehicleLicenseInfo == null)
            {
                MessageBox.Show($"No License Exist With ID = [{_SelectedVehicleLicenseID}]", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_VehicleLicenseInfo.IsCanceled )
            {
                MessageBox.Show("The License Is Allready Cancelled","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            btnCancel.Enabled = true;
        }

        private void frmCancelVehicleLicense_Activated(object sender, EventArgs e)
        {
            ctrlVehicleLcenseWithFilter1.FocusText();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want To Cancel Vehicle License", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            bool IsCancelled = _VehicleLicenseInfo.Cancel();

            if (IsCancelled)
            {
                MessageBox.Show("The License is cancelled Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
                MessageBox.Show("The License is didn't cancel ,An Error Occurred", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
