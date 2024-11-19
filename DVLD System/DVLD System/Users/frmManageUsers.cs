using DVLD_Bussiness;
using DVLD_System.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_System
{
    public partial class frmManageUsers : Form
    {
        DataView _UsersDataView;

        enum enActiveStatus { enActive = 0,enInActive = 1,enAll = 2};
        public frmManageUsers()
        {
            InitializeComponent();

            _FillUsersList();
        }

        void _FillComboBoxWithFilterBy()
        {
            cbFilterUserBy.Items.Add("None");

            foreach (DataColumn column in _UsersDataView.Table.Columns)
            {
                cbFilterUserBy.Items.Add(column.ColumnName);
            }

            cbFilterUserBy.SelectedIndex = 0;
        }

        void _FillComboBoxActiveStatus()
        {
            cbActiveStatus.Items.Add("Active");
            cbActiveStatus.Items.Add("InActive");
            cbActiveStatus.Items.Add("All");
            

            cbActiveStatus.SelectedIndex = 2;
        }

        string _GetNumberOfUsersRecords()
        {
            return _UsersDataView.Count.ToString();
        }
        
        void _FillUsersList()
        {
            DataTable dataTable = clsUser.UsersList();

            _UsersDataView = dataTable.DefaultView;

            dgvUsersList.DataSource = _UsersDataView;

            lblUsersCount.Text = _GetNumberOfUsersRecords();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            lblUsersCount.Text = _GetNumberOfUsersRecords();

            _FillComboBoxWithFilterBy();

            _FillComboBoxActiveStatus();
        }

        private void cbFilterUserBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterUserBy.SelectedIndex == 0)
            {
                txtFilterByValue.Visible = false;
                _UsersDataView.RowFilter = $"UserID IS NOT NULL";
            }
            else 
            {
                txtFilterByValue.Visible = true;
            }

            if (cbFilterUserBy.SelectedItem.ToString() == "IsActive")
            {
                txtFilterByValue.Visible = false;
                cbActiveStatus.Visible = true;
            }
            else
            {
                cbActiveStatus.Visible = false;
            }
        }

        private void txtFilterByValue_TextChanged(object sender, EventArgs e)
        {
            string SelectedColumn = cbFilterUserBy.SelectedItem.ToString();

            string FilterValue = txtFilterByValue.Text.ToString();

            _UsersDataView.RowFilter = $"Convert([{SelectedColumn}], 'System.String') Like '{FilterValue}%'";

            lblUsersCount.Text = _GetNumberOfUsersRecords();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            frmAddNewUser addNewUser = new frmAddNewUser();
            addNewUser.ShowDialog();
            _FillUsersList();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAddUser.PerformClick();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvUsersList.CurrentRow.Cells[0].Value;

            frmUserDetails userDetails = new frmUserDetails(UserID);
            userDetails.ShowDialog();
        }

        private void EditUsertoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvUsersList.CurrentRow.Cells[0].Value;

            frmEditUser editUser  = new frmEditUser(UserID);
            editUser.ShowDialog();
            _FillUsersList();
        }

        private void DeleteUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvUsersList.CurrentRow.Cells[0].Value;

            if(MessageBox.Show("Are You Sure You Want To Delete This User",
                "Delete User",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (clsUser.Delete(UserID))
                {
                    MessageBox.Show($"User With ID [{UserID}] Deleted Successfuly.");
                    _FillUsersList();
                }
                else
                {
                    MessageBox.Show($"Somthing Went Wrong.");
                }
            }
           
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbActiveStatus.SelectedIndex)
            {
                case (int)enActiveStatus.enActive:
                    _UsersDataView.RowFilter = $"IsActive = True";
                    lblUsersCount.Text = _GetNumberOfUsersRecords();
                    break;
                case (int)enActiveStatus.enInActive:
                    _UsersDataView.RowFilter = $"IsActive = False";
                    lblUsersCount.Text = _GetNumberOfUsersRecords();
                    break;
                case (int)enActiveStatus.enAll:
                    _UsersDataView.RowFilter = $"UserID IS NOT NULL";
                    lblUsersCount.Text = _GetNumberOfUsersRecords();
                    break;

            }
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvUsersList.CurrentRow.Cells[0].Value;

            frmChangePassword changePassword = new frmChangePassword(UserID);
            changePassword.ShowDialog();
        }
    }
}
