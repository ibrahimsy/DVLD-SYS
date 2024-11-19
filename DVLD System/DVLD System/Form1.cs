using DVLD_Bussiness;
using DVLD_System.Applications;
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
    public partial class Form1 : Form
    {
        frmLogin _LoginForm;
        public Form1(frmLogin loginForm)
        {
            InitializeComponent();
           
            this.WindowState = FormWindowState.Maximized;

            _LoginForm = loginForm;

           
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManagePeople managePeople = new frmManagePeople();
            managePeople.ShowDialog();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageUsers manageUsers = new frmManageUsers();
            manageUsers.ShowDialog();
        }

        private void showCurrentUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserDetails currentUserInfo = new frmUserDetails(clsGlobalSettings.CurrentUser.UserID);
            currentUserInfo.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobalSettings.CurrentUser = null;

            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _LoginForm.Show();
        }
       
        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword changePasswordForm = new frmChangePassword(clsGlobalSettings.CurrentUser.UserID);

            changePasswordForm.ShowDialog();
        }

        private void Form1_FormClosed(object sender, CancelEventArgs e)
        {

            
        }

        private void localDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewLocalDrivingLicense addNewLocalDrivingLicense = new frmAddNewLocalDrivingLicense();
            addNewLocalDrivingLicense.ShowDialog();
        }

        private void localAppliToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplications frmLDLapplicatios = new frmLocalDrivingLicenseApplications();
            frmLDLapplicatios.ShowDialog();
        }
    }
}
