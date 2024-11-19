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

            ctrlApplicationBasicInfo1.LoadApplicationBasicInfo(_LDLApp.ApplicationID);

        }

      
    }
}
