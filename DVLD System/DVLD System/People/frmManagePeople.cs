using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Bussiness;
namespace DVLD_System
{
    public partial class frmManagePeople : Form
    {
        DataView _PeopleDataView;
        public frmManagePeople()
        {
            InitializeComponent();

            _FillPeopleList();
        }

        void _FillPeopleList()
        {
            DataTable dataTable = clsPerson.PeopleList();
            
            _PeopleDataView = dataTable.DefaultView;
            
            dgvPeopleList.DataSource = _PeopleDataView;

            lblNumberOfRecords.Text = _GetNumberOfPeopleRecords();
        }

        void _FillComboBoxWithFilterBy()
        {
            cbFilterBy.Items.Add("None");

            foreach (DataColumn column in _PeopleDataView.Table.Columns)
            {
                cbFilterBy.Items.Add(column.ColumnName);
            }

            cbFilterBy.SelectedIndex = 0;
        }

        string _GetNumberOfPeopleRecords()
        {
            return _PeopleDataView.Count.ToString();
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmManagePeople_Load(object sender, EventArgs e)
        {

            lblNumberOfRecords.Text = _GetNumberOfPeopleRecords();

            _FillComboBoxWithFilterBy();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedIndex == 0)
            {
                txtFilterBy.Visible = false;
                _PeopleDataView.RowFilter = "[Person ID] Is Not Null";
                lblNumberOfRecords.Text = _GetNumberOfPeopleRecords();
                return;
            }

            txtFilterBy.Visible = true;
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
           
            string SelectedColumn = cbFilterBy.SelectedItem.ToString();

            string FilterValue = txtFilterBy.Text.ToString();

            _PeopleDataView.RowFilter = $"Convert([{SelectedColumn}], 'System.String') Like '{FilterValue}%'";

            lblNumberOfRecords.Text = _GetNumberOfPeopleRecords();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo addEditPersonInfo = new frmAddEditPersonInfo(-1);
            
            addEditPersonInfo.ShowDialog();

            _FillPeopleList();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int SelectedPersonId = (int)dgvPeopleList.CurrentRow.Cells[0].Value;

            frmPersonDetails personDetails = new frmPersonDetails(SelectedPersonId);

            personDetails.ShowDialog();

            _FillPeopleList();

        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo addEditPersonInfo = new frmAddEditPersonInfo(-1);

            addEditPersonInfo.ShowDialog();

            _FillPeopleList();
        }

        private void editPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int SelectedPersonId = (int)dgvPeopleList.CurrentRow.Cells[0].Value;

            frmAddEditPersonInfo addEditPersonInfo = new frmAddEditPersonInfo(SelectedPersonId);

            addEditPersonInfo.ShowDialog();

            _FillPeopleList();
        }

        private bool _CheckPersonHasUser(int PersonID)
        {
            return clsUser.IsLinkedToPerson(PersonID);
        }

        private void deletePersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int SelectedPersonId = (int)dgvPeopleList.CurrentRow.Cells[0].Value;

            if (_CheckPersonHasUser(SelectedPersonId))
            {
                MessageBox.Show("You can't Delete this Person ,This Person Has User","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }

            if(MessageBox.Show("Are You Sure To Delete Person With ID " + SelectedPersonId,
                "Delete Person",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (clsPerson.Delete(SelectedPersonId))
                {
                    _FillPeopleList();
                }
                else
                {
                    MessageBox.Show("Faild To Delete Person With Person ID : " + SelectedPersonId);
                }
            }
            
        }
    }
}
