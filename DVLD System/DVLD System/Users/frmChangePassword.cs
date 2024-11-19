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

        clsUser _user;
        public frmChangePassword(int UserID)
        {
            InitializeComponent();

            _UserID = UserID;

            _user = clsUser.Find(_UserID);
        }

        void _LoadUserInfo()
        {
            ctrlUserCard1.LoadUserInfo(_UserID);
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            _LoadUserInfo();
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (_user.Password != txtCurrentPassword.Text)
            {
                errorProvider1.SetError(txtCurrentPassword, "Incorrect Password");
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
           
        }

        private void txtConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                errorProvider1.SetError(txtCurrentPassword, "Incorrect Password");
                // e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, "");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_user.Password != txtCurrentPassword.Text )
            {
                MessageBox.Show("Your Current Password is Incorrect","Incorrect Password",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if((txtNewPassword.Text != txtConfirmPassword.Text) || string.IsNullOrEmpty(txtNewPassword.Text))
            {
                MessageBox.Show("Your Password Must to be matched and Not Empty", "Mismatch Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _user.Password = txtNewPassword.Text;

            if (_user.Save())
            {
                MessageBox.Show("Password has changed successfuly", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else{
                MessageBox.Show("An Error Occoured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
