using DVLD_Bussiness;
using DVLD_System.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_System.Licenses.Controls
{
    public partial class ctrlDriverLicenseInfo : UserControl
    {
        clsLicense _LicenseInfo;
        int _LicenseID = -1;
        public ctrlDriverLicenseInfo()
        {
            InitializeComponent();
          
        }

        void _LoadDriverImage()
        {
            if (_LicenseInfo.DriverInfo.PersonInfo.Gendor == 0)
                pbDriverImage.Image = Resources.male;
            else
                pbDriverImage.Image = Resources.female;

            string ImagePath = _LicenseInfo.DriverInfo.PersonInfo.ImagePath;
            
            if (ImagePath != "")
            {
                if (File.Exists(ImagePath))
                {
                    pbDriverImage.Load(ImagePath);
                }
            }
            else{
                MessageBox.Show("The Doesn't Exist","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
        }

        public void LoadLicenseInfo(int LicenseID)
        {
            _LicenseID = LicenseID;
            _LicenseInfo = clsLicense.Find(_LicenseID);

            if (_LicenseInfo == null)
            {
                MessageBox.Show("No License Exist","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            lblClassName.Text = _LicenseInfo.LicenseClassInfo.ClassName;
            lblDriverName.Text = _LicenseInfo.DriverInfo.PersonInfo.FullName;
            lblLicenseID.Text = _LicenseID.ToString();

            lblNationalNo.Text = _LicenseInfo.DriverInfo.PersonInfo.NationalNo;
            lblGendor.Text = _LicenseInfo.DriverInfo.PersonInfo.Gendor == 0?"Male":"Female";
            lblIssueDate.Text = _LicenseInfo.IssueDate.ToShortDateString();
            lblIssueReason.Text = _LicenseInfo.IssueReasonText;
            lblNotes.Text = _LicenseInfo.Notes;
            lblIsActive.Text = _LicenseInfo.IsActive?"Yes":"No";
            lblDateOfBirth.Text = _LicenseInfo.DriverInfo.PersonInfo.DateOfBirth.ToShortDateString();
            lblDriverID.Text = _LicenseInfo.DriverID.ToString();
            lblExpirationDate.Text = _LicenseInfo.ExpirationDate.ToShortDateString();
            lblIsDetained.Text = "No";

            _LoadDriverImage();
        }

    
    }
}
