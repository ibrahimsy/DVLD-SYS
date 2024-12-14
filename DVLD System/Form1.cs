using DVLD_Bussiness;
using DVLD_System.Applications;
using DVLD_System.Applications.Application_Types;
using DVLD_System.Applications.Release_Detained_License_Applications;
using DVLD_System.Applications.Renew_Driving_License_Application;
using DVLD_System.Applications.Replacment_License_Applications;
using DVLD_System.Drivers;
using DVLD_System.Licenses;
using DVLD_System.Licenses.Detain_Licenses;
using DVLD_System.Licenses.Detained_Licenses;
using DVLD_System.Licenses.International_Licenses;
using DVLD_System.Tests.Test_Types;
using DVLD_System.Users;
using DVLD_System.Vehichles;
using DVLD_System.VehicleLicenses;
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

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmManageApplicationTypes();
            frm.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmManageTestTypes();
            frm.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowDrivers frm = new frmShowDrivers();
            frm.ShowDialog();
        }

        private void internationDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewInternationalApplication frm = new frmAddNewInternationalApplication();
            frm.ShowDialog();
        }

        private void internationalApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInternationalLicensesApplications frm = new frmInternationalLicensesApplications();
            frm.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewDrivingLicense frm = new frmRenewDrivingLicense();
            frm.ShowDialog();
        }

        private void replacmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplacmentLicenseApplication frm = new frmReplacmentLicenseApplication();
            frm.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.ShowDialog();   
        }

        private void manageDetainLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDetainedLicenses frm = new frmListDetainedLicenses();
            frm.ShowDialog();
        }

        private void releaseDetainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense();    
            frm.ShowDialog();
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplications frm = new frmLocalDrivingLicenseApplications();
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense();
            frm.ShowDialog();
        }

        private void showVehichlesListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmVehiclesList frm = new frmVehiclesList();
            frm.ShowDialog();
        }

        private void addNewVehichleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditVehicle frm = new frmAddEditVehicle();
            frm.ShowDialog();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            frmIssueVehicleLicense frm = new frmIssueVehicleLicense();
            frm.ShowDialog();
        }

        private void renewVehichleLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
