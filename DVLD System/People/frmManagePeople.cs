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

        private static DataTable _dtAllPeople = clsPerson.PeopleList();

        private static DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(false,"PersonID","NationalNo",
                              "FirstName", "SecondName", "ThirdName", "LastName",
                              "DateOfBirth","GendorCaption", "CountryName", "Phone", "Email");

        private void _RefreshPeoplList()
        {
            _dtAllPeople = clsPerson.PeopleList();
            _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                              "FirstName", "SecondName", "ThirdName", "LastName",
                              "DateOfBirth", "GendorCaption", "CountryName", "Phone", "Email");

            dgvPeopleList.DataSource = _dtPeople;
           lblNumberOfRecords.Text =  _dtPeople.Rows.Count.ToString();
        }

        public frmManagePeople()
        {
            InitializeComponent();

            //_FillPeopleList();
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

            dgvPeopleList.DataSource = _dtPeople;
            lblNumberOfRecords.Text = _dtPeople.Rows.Count.ToString();
            cbFilterBy.SelectedIndex = 0;

            if (_dtPeople.Rows.Count > 0)
            {
                dgvPeopleList.Columns[0].HeaderText = "Person ID";
                dgvPeopleList.Columns[0].Width = 80;

                dgvPeopleList.Columns[1].HeaderText = "National No";
                dgvPeopleList.Columns[1].Width = 90;

                dgvPeopleList.Columns[2].HeaderText = "First Name";
                dgvPeopleList.Columns[2].Width = 100;

                dgvPeopleList.Columns[3].HeaderText = "Second Name";
                dgvPeopleList.Columns[3].Width = 100;

                dgvPeopleList.Columns[4].HeaderText = "Third Name";
                dgvPeopleList.Columns[4].Width = 100;

                dgvPeopleList.Columns[5].HeaderText = "Last Name";
                dgvPeopleList.Columns[5].Width = 100;

                dgvPeopleList.Columns[6].HeaderText = "Date Of Birth";
                dgvPeopleList.Columns[6].Width = 100;

                dgvPeopleList.Columns[7].HeaderText = "Gendor Caption";
                dgvPeopleList.Columns[7].Width = 120;

                dgvPeopleList.Columns[8].HeaderText = "Country Name";
                dgvPeopleList.Columns[8].Width = 120;

                dgvPeopleList.Columns[9].HeaderText = "Phone";
                dgvPeopleList.Columns[9].Width = 100;

                dgvPeopleList.Columns[10].HeaderText = "Email";
                dgvPeopleList.Columns[10].Width = 100;

            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtFilterBy.Visible = cbFilterBy.Text != "None";
            
            if (txtFilterBy.Visible)
            {
                txtFilterBy.Text = "";
                txtFilterBy.Focus();
            }
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
           
            string FilterColumn = cbFilterBy.Text;

            switch (FilterColumn)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "National No":
                    FilterColumn = "NationalNo";
                    break;
                case "First Name":
                    FilterColumn = "FirstName";
                    break;
                case "Second Name":
                    FilterColumn = "SecondName";
                    break;
                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;
                case "Last Name":
                    FilterColumn = "LastName";
                    break;
                case "Date Of Birth":
                    FilterColumn = "DateOfBirth";
                    break;
                case "Gendor Caption":
                    FilterColumn = "Gendor Caption";
                    break;
                case "Country Name":
                    FilterColumn = "CountryName";
                    break;
                case "Phone":
                    FilterColumn = "Phone";
                    break;
                case "Email":
                    FilterColumn = "Phone";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }
            if (txtFilterBy.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblNumberOfRecords.Text = _dtPeople.Rows.Count.ToString();
                return;
            }        

            if (FilterColumn == "PersonID")
               {
                  _dtPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}",FilterColumn,txtFilterBy.Text.Trim());
            }
            else
            {
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] Like '{1}%'",FilterColumn, txtFilterBy.Text.Trim());
            }
    

            lblNumberOfRecords.Text = _dtPeople.Rows.Count.ToString();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditPersonInfo();
            
            frm.ShowDialog();

            _RefreshPeoplList();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Form frm = new frmPersonDetails((int)dgvPeopleList.CurrentRow.Cells[0].Value);

            frm.ShowDialog();

            _RefreshPeoplList();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditPersonInfo();

            frm.ShowDialog();

            _RefreshPeoplList();
        }

        private void editPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int SelectedPersonId = (int)dgvPeopleList.CurrentRow.Cells[0].Value;

            Form frm = new frmAddEditPersonInfo(SelectedPersonId);

            frm.ShowDialog();

            _RefreshPeoplList();
        }


        private void deletePersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int SelectedPersonId = (int)dgvPeopleList.CurrentRow.Cells[0].Value;

            if(MessageBox.Show("Are You Sure To Delete Person With ID " + SelectedPersonId,
                "Delete Person",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (clsPerson.Delete(SelectedPersonId))
                {
                    _RefreshPeoplList();
                }
                else
                {
                    MessageBox.Show("Faild To Delete,There is data connected to this Person With Person ID : [" + SelectedPersonId + "]");
                }
            }
            
        }

        private void dgvPeopleList_DoubleClick(object sender, EventArgs e)
        {
            Form frm = new frmPersonDetails((int)dgvPeopleList.CurrentRow.Cells[0].Value);

            frm.ShowDialog();

            _RefreshPeoplList();
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Person ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }
    }
}
