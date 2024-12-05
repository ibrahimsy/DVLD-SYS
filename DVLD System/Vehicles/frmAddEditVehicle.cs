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

namespace DVLD_System.Vehichles
{
    public partial class frmAddEditVehicle : Form
    {
        enum enMode { AddNew = 1, Update = 2 }
        enMode _Mode = enMode.AddNew;
        int _VehichleID = -1;
        int _SelectedPerson = -1;
        public frmAddEditVehicle()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddEditVehicle(int VehichleID)
        {
            InitializeComponent();
            _VehichleID = VehichleID;
            _Mode = enMode.Update;
        }
       
        void _ResetDefaultData()
        {
            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Register Vehichle";
                this.Text = lblTitle.Text;
                tpVehichleInfo.Enabled = false;
            }
            else
            {
                lblTitle.Text = "Edit Vehichle";
                this.Text = lblTitle.Text;
                tpVehichleInfo.Enabled = true;
            }
        }
        void _FillMakeInComboBox()
        {
            DataTable _dtmanufacturers = clsMake.GetMakesList();
            foreach (DataRow Row in _dtmanufacturers.Rows)
            {
                cbMake.Items.Add(Row["Make"]);
            }
        }

        void _FillMakeModelInComboBox()
        {
            cbModel.Items.Clear();

            DataTable _dtMakeModels = clsMakeModel.GetModelsListByMakeName(cbMake.Text);
            foreach (DataRow Row in _dtMakeModels.Rows)
            {
                cbModel.Items.Add(Row["ModelName"]);
            }

            cbModel.SelectedIndex = 0;
        }

        void _FillSubModelInComboBox()
        {
            cbSubModel.Items.Clear();
            DataTable _dtSubModels = clsSubModel.GetSubModelsByModelName(cbModel.Text);
            foreach (DataRow Row in _dtSubModels.Rows)
            {
                cbSubModel.Items.Add(Row["SubModelName"]);
            }

            //cbSubModel.SelectedIndex = 0;

        }

        void _FillBodyTypeInComboBox()
        {
            DataTable _dtBodyTypes = clsBody.GetBodiesList();
            foreach(DataRow Row in _dtBodyTypes.Rows)
            {
                cbType.Items.Add(Row["BodyName"]);
            }
            cbType.SelectedIndex = 0;
        }

        private void frmAddEditVehichle_Load(object sender, EventArgs e)
        {
            if(_Mode == enMode.AddNew)
            {
                _ResetDefaultData();
            }
            else
            {

            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_SelectedPerson == -1)
            {
                MessageBox.Show("Select An Person First.","Not Allowed",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            tabControl1.SelectedTab = tabControl1.TabPages["tpVehichleInfo"];
            tpVehichleInfo.Enabled = true;
            lblCreatedBy.Text = clsGlobalSettings.CurrentUser.UserName;
            lblOwnerFullName.Text = ctrlPersonCardWithFilter1.SelectedPersonInfo.FullName;
            _FillMakeInComboBox();
            _FillBodyTypeInComboBox();

            txtPlateNumber.Text = clsUtil.GenerateVehiclePlateNumber();
        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _SelectedPerson = obj;
        }

        private void cbMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbModel.Enabled = true;
            _FillMakeModelInComboBox();
        }

        private void cbModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbSubModel.Enabled = true;  
            _FillSubModelInComboBox();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some Feild Incorrect,Put Mouse On Red Icon","Not Allowed",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
        }

        private void ValidateEmpty(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(((TextBox)sender).Text))
            {
                errorProvider1.SetError(txtChassisNumber, "Required Feild");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtChassisNumber, null);
            }
        }

        private void txtYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
