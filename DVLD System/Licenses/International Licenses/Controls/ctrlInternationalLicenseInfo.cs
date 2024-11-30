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

namespace DVLD_System.Licenses.International_Licenses.Controls
{
    public partial class ctrlInternationalLicenseInfo : UserControl
    {
        clsInternationalLicense _InternationalLicenseInfo;

        int _InternationalLicenseID = -1;

        public int InterationalLicenseID
        {
            get
            {
                return _InternationalLicenseID;
            }
        }
        public ctrlInternationalLicenseInfo()
        {
            InitializeComponent();
        }

        void _LoadDriverImage()
        {
            if (_InternationalLicenseInfo.DriverInfo.PersonInfo.Gendor == 0)
                pbDriverImage.Image = Resources.male;
            else
                pbDriverImage.Image= Resources.female;

            string ImagePath = _InternationalLicenseInfo.DriverInfo.PersonInfo.ImagePath;
            if (ImagePath != "")
            {
                if (File.Exists(ImagePath))
                {
                    pbDriverImage.Load(ImagePath);
                }
                else
                {
                    MessageBox.Show("File Doesn't Exist", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                MessageBox.Show("An Error Occurred During Load The Image",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
        }

        public void LoadInfo(int InternationalLicenseID)
        {
            _InternationalLicenseID= InternationalLicenseID;
            _InternationalLicenseInfo = clsInternationalLicense.Find(InternationalLicenseID);
            if (_InternationalLicenseInfo == null) 
            {
                MessageBox.Show($"No International License Found With LicenseID [{InternationalLicenseID}]!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            lblInternationalLicenseID.Text = _InternationalLicenseInfo.InternationalLicenseID.ToString();
            lblDriverName.Text = _InternationalLicenseInfo.DriverInfo.PersonInfo.FullName;
            lblLicenseID.Text = _InternationalLicenseInfo.IssuedUsingLocalLicenseID.ToString();
            lblNationalNo.Text = _InternationalLicenseInfo.DriverInfo.PersonInfo.NationalNo;
            lblGendor.Text = _InternationalLicenseInfo.DriverInfo.PersonInfo.Gendor == 0?"Male":"Female";
            lblIssueDate.Text = clsFormat.DateToShort(_InternationalLicenseInfo.IssueDate);
            lblApplicationID.Text = _InternationalLicenseInfo.ApplicationID.ToString();
            lblIsActive.Text = _InternationalLicenseInfo.IsActive ? "Yes" : "No";
            lblDateOfBirth.Text = clsFormat.DateToShort( _InternationalLicenseInfo.DriverInfo.PersonInfo.DateOfBirth);
            lblDriverID.Text = _InternationalLicenseInfo.DriverID.ToString();
            lblExpirationDate.Text = clsFormat.DateToShort(_InternationalLicenseInfo.ExpirationDate);

            _LoadDriverImage();
        }
    }
}
