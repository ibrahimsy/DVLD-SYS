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

namespace DVLD_System
{
    public partial class frmAddNewUser : Form
    {
        public frmAddNewUser()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmFindPerson findPerson = new frmFindPerson();

            findPerson.DataBack += _FindPerson_DataBack;

            findPerson.ShowDialog();
        }

        private void _FindPerson_DataBack(int PersonID)
        {
            ctrlPersonCard1.LoadPersonInfo(PersonID);

            btnNext.Enabled = true;


        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (clsUser.IsLinkedToPerson(ctrlPersonCard1.PersonID))
            {
                MessageBox.Show("This Person Is Allready Assigned To User.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            btnSave.Enabled = true;

            tabControl1.SelectedIndex = 1;

        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text) || clsUser.IsExist(txtUserName.Text))
            {
                errorProvider1.SetError(txtUserName,"User is Exist");
                e.Cancel = true;
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (!txtPassword.Text.Equals(txtConfirmPassword.Text))
            {
                errorProvider1.SetError(txtConfirmPassword,"Password must be matched");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!txtPassword.Text.Equals(txtConfirmPassword.Text))
            {
                MessageBox.Show("Password must to be matched","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            clsUser user = new clsUser();
            user.UserName = txtUserName.Text;
            user.Password = txtPassword.Text;
            user.PersonID = ctrlPersonCard1.PersonID;
            user.IsActive = cbIsActive.Checked;

            if (user.Save())
            {
                MessageBox.Show("User Added Successfully","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                lblUserId.Text = user.UserID.ToString();
               lblAddEditUser.Text = "Update User";
            }
            else
            {
                MessageBox.Show("Something went wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
