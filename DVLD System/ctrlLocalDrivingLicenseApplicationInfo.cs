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
    public partial class ctrlLocalDrivingLicenseApplicationInfo : UserControl
    {
        clsLocalDrivingLicenseApplication _LDLApp;
        public ctrlLocalDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }

        string _ApplicationStatusTest(int StatusID)
        {
            switch (StatusID)
            {
                case 1:
                    return "New";
                case 2:
                    return "Canceled";
                case 3:
                    return "Completed";
                default:
                    return "";
            }
        }
        public void LoadApplicationInfo(int _ApplicationID)
        {
            _LDLApp = clsLocalDrivingLicenseApplication.Find(_ApplicationID);
            
            if (_LDLApp != null)
            {
                _FillApplicationInfo();
            }
            else
            {
                MessageBox.Show("Application Not Found");
                return;
            }
        }

        void _FillApplicationInfo()
        {
            lblDLAppID.Text = _LDLApp.LocalDrivingLicenseApplicationID.ToString();
            
            lblAppliedForLicense.Text = _LDLApp.LicenseClassInfo.ClassName;
            
            lblPassedTest.Text = clsLocalDrivingLicenseApplication.GetPassedTestCount(_LDLApp.LocalDrivingLicenseApplicationID).ToString();
            
            lblID.Text = _LDLApp.ApplicationInfo.ApplicationID.ToString();

            lblStatus.Text = _ApplicationStatusTest(_LDLApp.ApplicationInfo.ApplicationStatus);
            
            lblFees.Text = _LDLApp.ApplicationInfo.PaidFees.ToString();
            
            lblType.Text = _LDLApp.ApplicationInfo.ApplicationTypeInfo.Title.ToString();
            
            lblApplicant.Text = _LDLApp.ApplicationInfo.PersonInfo.FullName();
            
            lblDate.Text = _LDLApp.ApplicationInfo.ApplicationDate.ToString();
            
            lblStatusDate.Text = _LDLApp.ApplicationInfo.LastStatusDate.ToString();
            
            lblCreatedBy.Text = _LDLApp.ApplicationInfo.UserInfo.UserName;

        }

        private void llbViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonDetails personDetails = new frmPersonDetails(_LDLApp.ApplicationInfo.ApplicantPersonID);
            personDetails.ShowDialog();
        }
    }
}
