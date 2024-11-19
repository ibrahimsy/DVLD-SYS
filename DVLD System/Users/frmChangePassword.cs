using DVLD_Bussiness;
using DVLD_System.Controls;
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
    public partial class frmChangePassword : Form
    {
        int _UserID;

        clsUser _UserInfo;
        public frmChangePassword(int UserID)
        {
            InitializeComponent();

            _UserID = UserID;  
        }

        void _LoadUserInfo()
        {
            _UserInfo = clsUser.Find(_UserID);
            if (_UserInfo == null)
            {
                MessageBox.Show($"Couldn't Find User With ID = [{_UserID}]","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
                return;
            }
            ctrlUserCard1.LoadUserInfo(_UserID);
        }

        void _ResetDefaultValues()
        {
            txtCurrentPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";
            txtCurrentPassword.Focus();
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            _LoadUserInfo();
        }

        private void txtConfirmPassword_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some Feild Are Not Valid,Put The Mouse On Red Icon",
                                "Validation Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                
                return;
            }

            _UserInfo.Password = txtNewPassword.Text;

            if (_UserInfo.ChangeUserPassword(_UserInfo.Password))
            {
                MessageBox.Show("Password has changed successfuly",
                                "success",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                _ResetDefaultValues();
            }
            else{
                MessageBox.Show("An Error Occoured,Password Didn't Change",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtCurrentPassword.Text.Trim()))
            {
                errorProvider1.SetError(txtCurrentPassword, "This Feild is required");
                e.Cancel = true;
                return;
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, null);
            }

            if (_UserInfo.Password != txtCurrentPassword.Text)
            {
                errorProvider1.SetError(txtCurrentPassword, "Your Current Password is Incorrect");
                e.Cancel = true;
                return;
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, null);
            }

        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCurrentPassword.Text.Trim()))
            {
                errorProvider1.SetError(txtCurrentPassword, "This Feild is required");
                e.Cancel = true;
                return;
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, null);
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if ((txtNewPassword.Text.Trim() != txtConfirmPassword.Text.Trim()))
            {
                errorProvider1.SetError(txtConfirmPassword, "Mismatched Password");
                e.Cancel = true;
                return;
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            }
        }
    }
}
