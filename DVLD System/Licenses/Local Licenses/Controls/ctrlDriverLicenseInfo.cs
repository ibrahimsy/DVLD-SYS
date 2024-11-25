using DVLD_Bussiness;
using DVLD_System.Global_Classes;
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
        public clsLicense LicenseInfo
        {
            get { return _LicenseInfo; }
        }
        public int LicenseID
        {
            get { return _LicenseID; }
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
                MessageBox.Show("Image Doesn't Exist","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
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
                _LicenseID = -1;
                return;
            }
            
            lblClassName.Text = _LicenseInfo.LicenseClassInfo.ClassName;
            lblDriverName.Text = _LicenseInfo.DriverInfo.PersonInfo.FullName;
            lblLicenseID.Text = _LicenseID.ToString();

            lblNationalNo.Text = _LicenseInfo.DriverInfo.PersonInfo.NationalNo;
            lblGendor.Text = _LicenseInfo.DriverInfo.PersonInfo.Gendor == 0?"Male":"Female";
            lblIssueDate.Text =clsFormat.DateToShort( _LicenseInfo.IssueDate);
            lblIssueReason.Text = _LicenseInfo.IssueReasonText;
            lblNotes.Text = _LicenseInfo.Notes;
            lblIsActive.Text = _LicenseInfo.IsActive?"Yes":"No";
            lblDateOfBirth.Text = clsFormat.DateToShort(_LicenseInfo.DriverInfo.PersonInfo.DateOfBirth);
            lblDriverID.Text = _LicenseInfo.DriverID.ToString();
            lblExpirationDate.Text = clsFormat.DateToShort(_LicenseInfo.ExpirationDate);
            lblIsDetained.Text = _LicenseInfo.IsDetained?"Yes":"No";

            _LoadDriverImage();
        }

        public void ResetLicenseInfo()
        {
            lblClassName.Text = "[? ? ? ?]";
            lblDriverName.Text = "[? ? ? ?]";
            lblLicenseID.Text = "[? ? ? ?]";
            lblNationalNo.Text = "[? ? ? ?]";
            lblGendor.Text = "[? ? ? ?]";
            lblIssueDate.Text = "[? ? ? ?]";
            lblIssueReason.Text = "[? ? ? ?]";
            lblNotes.Text = "[? ? ? ?]";
            lblIsActive.Text = "[? ? ? ?]";
            lblDateOfBirth.Text = "[? ? ? ?]";
            lblDriverID.Text = "[? ? ? ?]";
            lblExpirationDate.Text = "[? ? ? ?]";
            lblIsDetained.Text = "[? ? ? ?]";

        }
    
       
    }
}
