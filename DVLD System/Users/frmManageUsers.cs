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
    /*
            None
            User ID
            Person ID
            UserName
            Password
            Is Active
         */
    public partial class frmManageUsers : Form
    {
        DataTable _dtUsersList;

        enum enActiveStatus { enAll = 0,enYes = 1,enNo = 2,};
        
        public frmManageUsers()
        {
            InitializeComponent();

        }
        
        string _ColumnText(string columnName)
        {
            switch(columnName)
            {
                case "Person ID":
                    return "PersonID";
                case "User ID":
                    return "UserID";
                case "Full Name":
                    return "FullName";
                case "User Name":
                    return "UserName";
                case "Password":
                    return "Password";
                case "Is Active":
                    return "IsActive";
                default:
                    return "None";
            }
        }

        void _RefreshUsersList()
        {
            _dtUsersList = clsUser.UsersList();

            dgvUsersList.DataSource = _dtUsersList;

            lblUsersCount.Text = _dtUsersList.Rows.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            _RefreshUsersList();
           
            if (_dtUsersList.Rows.Count > 0)
            {
                dgvUsersList.Columns[0].HeaderText = "User ID";
                dgvUsersList.Columns[0].Width = 100;

                dgvUsersList.Columns[1].HeaderText = "Person ID";
                dgvUsersList.Columns[1].Width = 100;

                dgvUsersList.Columns[2].HeaderText = "Full Name";
                dgvUsersList.Columns[2].Width = 250;

                dgvUsersList.Columns[3].HeaderText = "User Name";
                dgvUsersList.Columns[3].Width = 100;

                dgvUsersList.Columns[4].HeaderText = "Password";
                dgvUsersList.Columns[4].Width = 100;

                dgvUsersList.Columns[5].HeaderText = "Is Active";
                dgvUsersList.Columns[5].Width = 100;

                cbFilterUserBy.SelectedIndex = 0;
            }
        }

        private void cbFilterUserBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterUserBy.SelectedIndex == cbFilterUserBy.FindString("None"))
            {
                cbActiveStatus.Visible = false;
                txtFilterByValue.Visible = false;
                _dtUsersList.DefaultView.RowFilter = "";
                return;
            }

            if (cbFilterUserBy.SelectedIndex == cbFilterUserBy.FindString("Is Active"))
            {
                cbActiveStatus.SelectedIndex = 0;

                txtFilterByValue.Visible = false;
                cbActiveStatus.Visible = true;
                return;
            }

            txtFilterByValue.Visible = true;
            cbActiveStatus.Visible = false;
            txtFilterByValue.Focus();
        }


        private void txtFilterByValue_TextChanged(object sender, EventArgs e)
        {

            string SelectedColumn = _ColumnText(cbFilterUserBy.Text);

            string FilterValue = txtFilterByValue.Text.Trim();

            if (FilterValue == "")
            {
                _dtUsersList.DefaultView.RowFilter = "";
                lblUsersCount.Text = _dtUsersList.DefaultView.Count.ToString();
                return;
            }

            if (SelectedColumn == "PersonID" || SelectedColumn == "UserID")
            {

                _dtUsersList.DefaultView.RowFilter = string.Format("[{0}] = {1}",SelectedColumn, FilterValue);
            }
            else
            {
                _dtUsersList.DefaultView.RowFilter = string.Format("[{0}] Like '{1}%'", SelectedColumn, FilterValue);
            }

            lblUsersCount.Text = _dtUsersList.DefaultView.Count.ToString();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            //frmAddNewUser addNewUser = new frmAddNewUser();
            //addNewUser.ShowDialog();

           frmAddEditUser frm = new frmAddEditUser();
            frm.ShowDialog();
            _RefreshUsersList();
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
        
            frmUserDetails userDetails = new frmUserDetails((int)dgvUsersList.CurrentRow.Cells[0].Value);
            userDetails.ShowDialog();
        }

        private void EditUsertoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser((int)dgvUsersList.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _RefreshUsersList();
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
                    _RefreshUsersList();
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
                case (int)enActiveStatus.enAll:
                    _dtUsersList.DefaultView.RowFilter = "";
                  
                    break;
                case (int)enActiveStatus.enYes:
                    _dtUsersList.DefaultView.RowFilter = $"IsActive = True";
                  
                    break;
                case (int)enActiveStatus.enNo:
                    _dtUsersList.DefaultView.RowFilter = "IsActive = False";
                    
                    break;
            }
            lblUsersCount.Text= _dtUsersList.DefaultView.Count.ToString();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvUsersList.CurrentRow.Cells[0].Value;

            frmChangePassword changePassword = new frmChangePassword(UserID);
            changePassword.ShowDialog();

            _RefreshUsersList();
        }

        private void txtFilterByValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterUserBy.Text == "Person ID" || cbFilterUserBy.Text == "User ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }
    }
}
