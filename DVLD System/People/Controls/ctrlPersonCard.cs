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

namespace DVLD_System.Controls
{
    public partial class ctrlPersonCard : UserControl
    {
       
        clsPerson _Person;

        int _PersonID = -1;
        public ctrlPersonCard()
        {
            InitializeComponent();
        
        }

        public int PersonID
        {
            get
            {
                return _PersonID;
            }
        }

        public clsPerson SelectedPersonInfo
        {
            get
            {
                return _Person;
            }
        }

        public void LoadPersonInfo(int PersonID)
        {
            _Person = clsPerson.Find(PersonID);
            if (_Person == null)
            {
                MessageBox.Show($"No Person With Person ID {PersonID} is found","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                ResetPersonInfo();
            }
            else
                _FillPersonInfo();
        }

        public void LoadPersonInfo(string NationalNo)
        {
            _Person = clsPerson.Find(NationalNo);
            if (_Person == null)
            {
                MessageBox.Show($"No Person With National Number {NationalNo} is found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ResetPersonInfo();
            }
            else
                _FillPersonInfo();
        }

        void _LoadPersonImage()
        {
            if (_Person.Gendor == 0)
                pbPersonImage.Image = Resources.male;
            else
                pbPersonImage.Image = Resources.female;

            string ImagePath = _Person.ImagePath;

            if (ImagePath != "")
            {
                if (File.Exists(ImagePath))
                    pbPersonImage.ImageLocation = ImagePath;
                //else
                //    MessageBox.Show("No Image Exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void _FillPersonInfo()
        {
            _PersonID = _Person.PersonID;

            lblPersonID.Text = _Person.PersonID.ToString();

            lblFullName.Text = _Person.FullName;

            lblNationalNo.Text = _Person.NationalNo.ToString();

            lblEmail.Text = _Person.Email.ToString();

            lblPhone.Text = _Person.Phone.ToString();

            lblDateOfBirth.Text = _Person.DateOfBirth.ToString();

            lblCountry.Text = clsCountry.Find(_Person.NationalityCountryID).CountryName;

            lblGendor.Text = _Person.Gendor == 0 ? "Male" : "Female";

            lblAddress.Text = _Person.Address;

            _LoadPersonImage();

        }

        public void ResetPersonInfo()
        {
            _PersonID = -1;

            lblPersonID.Text = "[ ? ? ? ]";

            lblFullName.Text = "[ ? ? ? ]"; 

            lblNationalNo.Text = "[ ? ? ? ]"; 

            lblEmail.Text = "[ ? ? ? ]"; 

            lblPhone.Text = "[ ? ? ? ]"; 

            lblDateOfBirth.Text = "[ ? ? ? ]"; 

            lblCountry.Text = "[ ? ? ? ]"; 

            lblGendor.Text = "[ ? ? ? ]";

            pbPersonImage.Image = Resources.Male_512;

            lblAddress.Text = "[ ? ? ? ]";
        }

        private void llbEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditPersonInfo addEditPersonInfo = new frmAddEditPersonInfo(PersonID);
            addEditPersonInfo.ShowDialog();

            LoadPersonInfo(PersonID);


        }
    }
}
