using DevExpress.Utils.Filtering.Internal;
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
    public partial class frmAddEditUser : Form
    {
        enum enMode { enAddNew = 1,enUpdate = 2 }
        enMode _Mode = enMode.enAddNew;
       
        int _UserID = -1;
        clsUser _UserInfo;
        public frmAddEditUser()
        {
            InitializeComponent();
            _Mode = enMode.enAddNew;
        }

        public frmAddEditUser(int UserID)
        {
            InitializeComponent();
            _Mode = enMode.enUpdate;
            _UserID = UserID;
        }

        void _ResetDefaultValue()
        {
            if (_Mode == enMode.enAddNew)
            {
                _UserInfo = new clsUser();

                lblTitle.Text = "ADD NEW USER";
                this.Text = "Add New User";

                tpLoginInfo.Enabled = false;
                btnSave.Enabled = false;
               
            }
            else
            {
                lblTitle.Text = "UPDATE USER";
                this.Text = "Update User";

                tpLoginInfo.Enabled = true;
            }

            txtUsername.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            chkIsActive.Checked = true;
        }

        void _LoadData()
        {
            _UserInfo = clsUser.Find(_UserID);
            if (_UserInfo == null) 
            {
                MessageBox.Show("User Is Not Found",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                this.Close();
                return;
            }
            ctrlPersonCardWithFilter1.EnableFilter = false;
            ctrlPersonCardWithFilter1.LoadInfo(_UserInfo.PersonID);

            lblUserID.Text = _UserInfo.UserID.ToString();

            txtUsername.Text = _UserInfo.UserName.ToString();
            txtPassword.Text = _UserInfo.Password.ToString();
            txtConfirmPassword.Text = _UserInfo.Password.ToString();
            chkIsActive.Checked = _UserInfo.IsActive;

            btnSave.Enabled = true;
            tpLoginInfo.Enabled = true;

        }
        private void frmAddEditUser_Load(object sender, EventArgs e)
        {
            _ResetDefaultValue();

            if(_Mode == enMode.enUpdate)
                _LoadData();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.enUpdate)
            {
                tpLoginInfo.Enabled = true;    
                btnSave.Enabled = true;
                tabControl1.SelectedTab = tpLoginInfo;
                return;
            }

            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {
                if (clsUser.IsLinkedToPerson(ctrlPersonCardWithFilter1.PersonID))
                {
                    MessageBox.Show("This Person Is Allready Assigned To User.",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    tpLoginInfo.Enabled = true;
                    tabControl1.SelectedTab = tpLoginInfo;
                    btnSave.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("Please Select A Person.",
                                    "Select Person",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();
            }


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some Feild Are Incorrect,Hover On Red Icon",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            if (clsUser.IsLinkedToPerson(ctrlPersonCardWithFilter1.PersonID) && _Mode == enMode.enAddNew)
            {
                MessageBox.Show("This Person Is Allready Assigned To User.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }


            _UserInfo.PersonID = ctrlPersonCardWithFilter1.PersonID;
            _UserInfo.UserName = txtUsername.Text.Trim();
            _UserInfo.Password = txtPassword.Text.Trim();
            _UserInfo.IsActive = chkIsActive.Checked;

            if (_UserInfo.Save())
            {
                MessageBox.Show("User Added Successfully","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                lblUserID.Text = _UserInfo.UserID.ToString();
                _Mode = enMode.enUpdate;
                lblTitle.Text = "UPDATE USER";
                this.Text = "Update User";
            }
            else
            {
                MessageBox.Show("An Error Occurred","Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void txtUsername_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtUsername.Text.Trim()))
            {
                errorProvider1.SetError(txtUsername, "This Feild Is Required");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtUsername, null);
            }

            if (clsUser.IsExist(txtUsername.Text.Trim()))
            {
                errorProvider1.SetError(txtUsername, "This Username Is Taken By Another User");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtUsername, null);
            }

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            //int PersonID = obj;  
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (!txtConfirmPassword.Equals(txtPassword))
            {
                errorProvider1.SetError(txtConfirmPassword, "Mismatch Password");
                e.Cancel= true; 
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text.Trim()))
            {
                errorProvider1.SetError(txtUsername, "This Feild Is Required");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtUsername, null);
            }
        }
    }
}
