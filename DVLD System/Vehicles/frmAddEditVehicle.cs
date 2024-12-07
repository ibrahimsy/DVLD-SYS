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
        clsVehichle _VehicleInfo;

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
                _VehicleInfo = new clsVehichle();
                lblTitle.Text = "Register Vehichle";
                this.Text = lblTitle.Text;
                tpVehichleInfo.Enabled = false;
                btnSave.Enabled = false;
            }
            else
            {
                lblTitle.Text = "Edit Vehichle";
                this.Text = lblTitle.Text;
                tpVehichleInfo.Enabled = true;
            }

            _FillMakeInComboBox();
            _FillBodyTypeInComboBox();
            txtPlateNumber.Text = clsUtil.GenerateVehiclePlateNumber();
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

            //cbModel.SelectedIndex = 0;
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

        private void _LoadData()
        {
            _VehicleInfo = clsVehichle.Find(_VehichleID);
            if (_VehicleInfo == null)
            {
                MessageBox.Show($"Vehicle Not Found With ID=[{_VehichleID}]","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            ctrlPersonCardWithFilter1.LoadInfo(_VehicleInfo.OwnerID);
            ctrlPersonCardWithFilter1.EnableFilter = false;
            lblTitle.Text = "Edit Vehicle";
            this.Text = lblTitle.Text;

            lblVehichleID.Text = _VehicleInfo.VehichleID.ToString();
            txtChassisNumber.Text = _VehicleInfo.ChassisNumber;
            txtPlateNumber.Text = _VehicleInfo.PlateNumber;
            cbMake.SelectedIndex = cbMake.FindString(clsMake.FindMakeByID(_VehicleInfo.MakeID).Make);
            _FillMakeModelInComboBox();
            cbModel.SelectedIndex = cbModel.FindString(clsMakeModel.FindMakeModelByID(_VehicleInfo.ModelID).ModelName);
            _FillSubModelInComboBox();

            if(_VehicleInfo.SubModelID != -1)
               cbSubModel.SelectedIndex =  cbSubModel.FindString(clsSubModel.FindSubModelByID(_VehicleInfo.SubModelID).SubModelName);
            
            cbType.SelectedIndex = cbType.FindString(clsBody.FindBodyByID(_VehicleInfo.BodyID).BodyName);
            txtColor.Text = _VehicleInfo.Color; 
            txtYear.Text = _VehicleInfo.Year.ToString();
            lblOwnerFullName.Text = _VehicleInfo.OwnerInfo.FullName;
            lblCreatedBy.Text = _VehicleInfo.UserInfo.UserName;
        }
        private void frmAddEditVehichle_Load(object sender, EventArgs e)
        {
            _ResetDefaultData();
            
            if (_Mode == enMode.Update)
            {
                _LoadData();
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
            btnSave.Enabled = true;
            lblCreatedBy.Text = clsGlobalSettings.CurrentUser.UserName;
            lblOwnerFullName.Text = ctrlPersonCardWithFilter1.SelectedPersonInfo.FullName;
           
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
            
            _VehicleInfo.ChassisNumber = txtChassisNumber.Text.ToUpper();
            _VehicleInfo.PlateNumber = txtPlateNumber.Text;
            _VehicleInfo.MakeID = clsMake.FindMakeByName(cbMake.Text).MakeID;
            _VehicleInfo.ModelID = clsMakeModel.FindMakeModelByName(cbModel.Text).ModelID;
            _VehicleInfo.SubModelID = cbSubModel.Text == ""? -1 : clsSubModel.FindSubModelByName(cbSubModel.Text).SubModelID;
            _VehicleInfo.BodyID = clsBody.FindBodyByName(cbType.Text).BodyID;
            _VehicleInfo.Color = txtColor.Text;
            _VehicleInfo.Year =  Convert.ToInt32( txtYear.Text);
            _VehicleInfo.OwnerID = ctrlPersonCardWithFilter1.PersonID;
            _VehicleInfo.CreatedBy = clsGlobalSettings.CurrentUser.UserID;

            if (_VehicleInfo.Save())
            {
                MessageBox.Show("Vehicle Registered Successfuly","Sucess",MessageBoxButtons.OK,MessageBoxIcon.Information);
                btnSave.Enabled = false;
                lblVehichleID.Text = _VehicleInfo.VehichleID.ToString();
                lblTitle.Text = "Edit Vehichle";
                this.Text = lblTitle.Text;
                ctrlPersonCardWithFilter1.EnableFilter = false;
            }
            else
            {
                MessageBox.Show("Vehicle Not Registered! ,An Error Occurred", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void ChassisNumber_Validate(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtChassisNumber.Text))
            {
                errorProvider1.SetError(txtChassisNumber, "Required Feild");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtChassisNumber, null);
            }

            if (!clsValidation.ValidateChassisNumber(txtChassisNumber.Text.ToUpper()))
            {
                errorProvider1.SetError(txtChassisNumber, "Ivalid Chassis Number");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtChassisNumber, null);
            }

        }
    }
}
