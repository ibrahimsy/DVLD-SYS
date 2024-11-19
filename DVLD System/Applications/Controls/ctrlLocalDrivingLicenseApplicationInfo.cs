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
            _LDLApp = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseID(_ApplicationID);
            
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
            
            lblAppliedForLicense.Text =clsLicenseClass.Find(_LDLApp.LicenseClassID).ClassName;
            
            lblPassedTest.Text = clsLocalDrivingLicenseApplication.GetPassedTestCount(_LDLApp.LocalDrivingLicenseApplicationID).ToString() + "/3";
            
            lblID.Text = _LDLApp.ApplicationID.ToString();

            lblStatus.Text = _ApplicationStatusTest(_LDLApp.ApplicationStatus);
            
            lblFees.Text = _LDLApp.PaidFees.ToString();

            lblType.Text = clsApplicationTypes.Find(_LDLApp.ApplicationTypeID).Title;

            lblApplicant.Text = clsPerson.Find(_LDLApp.ApplicantPersonID).FullName;
            
            lblDate.Text = _LDLApp.ApplicationDate.ToString();
            
            lblStatusDate.Text = _LDLApp.LastStatusDate.ToString();

            lblCreatedBy.Text = clsUser.Find(_LDLApp.CreatedByUserID).UserName;

        }

        private void llbViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonDetails personDetails = new frmPersonDetails(_LDLApp.ApplicantPersonID);
            personDetails.ShowDialog();
        }
    }
}
