using DVLD_Bussiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_System.Users
{
    public partial class frmEditUser : Form
    {
        int _UserID;

        clsUser _user;
        public frmEditUser(int userID)
        {
            InitializeComponent();
            _UserID = userID;

            _LoadUserInfo();
        }

        void _LoadUserInfo()
        {
            _user = clsUser.Find(_UserID);
            if (_user == null)
            {
                MessageBox.Show($"No User With User ID {_UserID} is found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo addEditPersonInfo = new frmAddEditPersonInfo(_user.PersonID);

            addEditPersonInfo.DataBack += _UpdateDataBackPersonInfo;

            addEditPersonInfo.ShowDialog();
        }

        private void frmEditUser_Load(object sender, EventArgs e)
        {
            ctrlPersonCard1.LoadPersonInfo(_user.PersonID);

            lblUserId.Text = _user.UserID.ToString();

            txtUserName.Text = _user.UserName.ToString();   

            txtPassword.Text = _user.Password.ToString();

            txtConfirmPassword.Text = _user.Password.ToString();

            cbIsActive.Checked = _user.IsActive;
        }

        private void _UpdateDataBackPersonInfo(int PersonId)
        {
            ctrlPersonCard1.LoadPersonInfo(PersonId);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!txtPassword.Text.Equals(txtConfirmPassword.Text))
            {
                MessageBox.Show("Password must to be matched", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

          
            _user.UserName = txtUserName.Text;
            _user.Password = txtPassword.Text;
            _user.PersonID = ctrlPersonCard1.PersonID;
            _user.IsActive = cbIsActive.Checked;

            if (_user.Save())
            {
                MessageBox.Show("User Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblUserId.Text = _user.UserID.ToString();
                lblAddEditUser.Text = "Update User";
            }
            else
            {
                MessageBox.Show("Something went wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

            btnSave.Enabled = true;

            tabControl1.SelectedIndex = 1;
        }
    }
}
