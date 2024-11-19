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

namespace DVLD_System
{
    public partial class frmAddEditPersonInfo : Form
    {
        enum Mode { enAddNew = 1, enUpdate = 2 }

        Mode _mode;

        clsPerson _person;

        int _PersonID;

        public delegate void DataBackEventHandler(int PersonID);

        public event DataBackEventHandler DataBack;

        public frmAddEditPersonInfo()
        {
            InitializeComponent();
            _mode = Mode.enAddNew;
        }
        
        public frmAddEditPersonInfo(int PersonID)
        {
            InitializeComponent();

            _PersonID = PersonID;

            _mode = Mode.enUpdate;
        }

        void _FillComboBoxWithCountries()
        {
            DataTable countriesDataTable = clsCountry.GetAllCountries();
            foreach (DataRow row in countriesDataTable.Rows)
            {
                cbCountry.Items.Add(row["CountryName"]);
            }

            cbCountry.SelectedIndex = cbCountry.FindString("Syria");
        }

        void _ResetDefaultValue()
        {
            _FillComboBoxWithCountries();

            if (_mode == Mode.enAddNew)
            {
                lblFormMode.Text = "ADD NEW PERSON";
                _person = new clsPerson();
            }
            else
            {
                lblFormMode.Text = "UPDATE PERSON";
            }

            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;

            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);

            if (rbMale.Checked)
            {
                pbPersonImage.Image = Resources.male;
            }
            else
            {
                pbPersonImage.Image = Resources.female;
            }

            llRemove.Visible = (pbPersonImage.ImageLocation != null);

            cbCountry.SelectedIndex = cbCountry.FindString("Syria");

            lblPersonID.Text = "";
            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtNationalNo.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            rtbAddress.Text = "";
            rbMale.Checked = true;
        }
        
        void _LoadPersonImage()
        {
            if (_person.Gendor == 0)
                rbMale.Checked = true;
            else
                rbFemale.Checked = true;

            string ImagePath = _person.ImagePath;

            if (ImagePath  != "")
            {
                if (File.Exists(ImagePath))
                {
                    pbPersonImage.ImageLocation = ImagePath;
                }
                else
                {
                    MessageBox.Show("The File is not Exist", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
            }
        }

        void _LoadData()
        {
            _person = clsPerson.Find(_PersonID);
            if (_person == null)
            {
                MessageBox.Show($"No Person With ID [{_PersonID}]", "Person not found", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                this.Close();
                return;
            }


            lblPersonID.Text = _person.PersonID.ToString();
            txtFirstName.Text = _person.FirstName;
            txtSecondName.Text = _person.LastName;
            txtThirdName.Text = _person.ThirdName;
            txtLastName.Text = _person.LastName;
            txtNationalNo.Text = _person.NationalNo;
            txtPhone.Text = _person.Phone;
            txtEmail.Text = _person.Email;
            rtbAddress.Text = _person.Address;
            dtpDateOfBirth.Value = _person.DateOfBirth;

            _LoadPersonImage();

            llRemove.Visible = (_person.ImagePath != "");


            cbCountry.SelectedIndex = cbCountry.FindString(_person.CountryInfo.CountryName);


        }

        private void frmAddEditPersonInfo_Load(object sender, EventArgs e)
        {
            _ResetDefaultValue();
            if (_mode == Mode.enUpdate)
            {
                _LoadData();
            }
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            string inputNationalNo = txtNationalNo.Text.Trim();

            if (string.IsNullOrEmpty(inputNationalNo))
            {
                NationalNoErrorProvider.SetError(txtNationalNo, "This Feild Is Empty");
                e.Cancel = true;
                return;
            }
            else
            {
                NationalNoErrorProvider.SetError(txtNationalNo, null);
            }

            if (txtNationalNo.Text.Trim() != _person.NationalNo && clsPerson.IsExist(txtNationalNo.Text.Trim()))
            {
                NationalNoErrorProvider.SetError(txtNationalNo, "National Number Is Exist");
                e.Cancel = true;
            }
            else
            {
                NationalNoErrorProvider.SetError(txtNationalNo, null); // Clear the error if the input is valid
            }
        }

        private void rtbAddress_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(rtbAddress.Text))
            {
                NationalNoErrorProvider.SetError(rtbAddress, "This Feild Is Required");
                e.Cancel = true;
            }
            else
            {
                NationalNoErrorProvider.SetError(txtNationalNo, string.Empty);
            }
        }

        bool _HandlePersonImage()
        {

            if (_person.ImagePath != pbPersonImage.ImageLocation)
            {
                if (_person.ImagePath != "")
                {
                    try
                    {
                        File.Delete(_person.ImagePath);

                    }catch (Exception ex)
                    {

                    }
                }
                if (pbPersonImage.ImageLocation != null)
                {
                    string sourceFile = pbPersonImage.ImageLocation.ToString();

                    if (clsUtil.CopyImageToProjectImagesFolder(ref sourceFile))
                    {
                        pbPersonImage.ImageLocation = sourceFile;
                        return true;
                    }

                }
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // _PreparePersonObjectWithData();

            if (!this.ValidateChildren())
            {
                return;
            }

            if (!_HandlePersonImage())
            {
                return;
            }

            _person.FirstName   = txtFirstName.Text.Trim();
            _person.SecondName  = txtSecondName.Text.Trim();
            _person.ThirdName   = txtThirdName.Text.Trim();
            _person.LastName    = txtLastName.Text.Trim();
            _person.NationalNo  = txtNationalNo.Text.Trim();
            _person.DateOfBirth = dtpDateOfBirth.Value;
            _person.Email       = txtEmail.Text.Trim();
            _person.Phone       = txtPhone.Text.Trim();
            _person.Address     = rtbAddress.Text.Trim();
            _person.Gendor      = rbMale.Checked ? (short)0 : (short)1;

            _person.NationalityCountryID = clsCountry.Find(cbCountry.Text).CountryID;

            _person.ImagePath = (pbPersonImage.ImageLocation != null) ? pbPersonImage.ImageLocation : "";


            if (_person.Save())
            {
                MessageBox.Show("Person Added Successfuly", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblPersonID.Text = _person.PersonID.ToString();


                if (_person != null)
                {
                    DataBack?.Invoke(_person.PersonID);
                }
            }
            else
            {
                MessageBox.Show("Something went wrong", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Title = "Person Image";

            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string SelectedFilePath = openFileDialog1.FileName;

                pbPersonImage.Load(SelectedFilePath);

                llRemove.Visible = true;
            }


        }

        private void llRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.ImageLocation = null;

            pbPersonImage.Image = rbMale.Checked ? Resources.male : Resources.female;

            llRemove.Visible = false;
        }

        private void rbGendor_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPersonImage.ImageLocation == null)
                pbPersonImage.Image = (RadioButton)sender == rbMale ? Resources.male : Resources.female;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void txtboxex_TextChanged(object sender, EventArgs e)
        {

        }

        private void ValidateEmptyText(object sender, CancelEventArgs e)
        {
            TextBox temp = (TextBox)sender;
            if (string.IsNullOrEmpty(temp.Text.Trim()))
            {
                NationalNoErrorProvider.SetError(temp, "This Feild is required");
                e.Cancel = true;
            }
            else
            {
                NationalNoErrorProvider.SetError(temp, null);
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            TextBox temp = (TextBox)sender;
            if (temp.Text.Trim() == "")
                return;


            if (!clsValidation.ValidateEmail(temp.Text.Trim()))
            {
                NationalNoErrorProvider.SetError(temp, "Email Address is not correct");
                e.Cancel = true;
            }
            else
            {
                NationalNoErrorProvider.SetError(temp, null);
            }
        }
    }

}
