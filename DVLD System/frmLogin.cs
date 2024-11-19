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
    public partial class frmLogin : Form
    {
        
        public frmLogin()
        {
            InitializeComponent();
        }


        private void frmLogin_Load(object sender, EventArgs e)
        {
            string UserName = "", Password = "";
            if (clsGlobalSettings.GetStoredCredentials(ref UserName,ref Password))
            {
                txtUsername.Text = UserName;
                txtPassword.Text = Password;
                cbRememberMe.Checked = true;
            }else
                cbRememberMe.Checked = false;

                
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string _UserName = txtUsername.Text;

            string _Password = txtPassword.Text;

            clsUser user = clsUser.Find(_UserName, _Password);
            if (user != null)
            {
               
                if (!user.IsActive)
                {
                    MessageBox.Show("Your Account Is Not Active ,Contact Your Admin", "Login Faild", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cbRememberMe.Checked)
                {
                    clsGlobalSettings.RememberUserNameAndPassword(_UserName,_Password);
                }
                else
                {
                    clsGlobalSettings.RememberUserNameAndPassword("","");
                }


                clsGlobalSettings.CurrentUser = user;

                this.Hide();

                Form1 MainForm = new Form1(this);

                MainForm.ShowDialog();

            }
            else
            {
                MessageBox.Show("Login Faild Invalid UserName/Password", "Credential Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
