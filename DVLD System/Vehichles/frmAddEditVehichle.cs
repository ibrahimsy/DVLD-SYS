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

namespace DVLD_System.Vehichles
{
    public partial class frmAddEditVehichle : Form
    {
        enum enMode { AddNew = 1, Update = 2 }
        enMode _Mode = enMode.AddNew;
        int _VehichleID = -1;
        int _SelectedPerson = -1;
        public frmAddEditVehichle()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddEditVehichle(int VehichleID)
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
    }
}
