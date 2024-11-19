using DVLD_Bussiness;
using DVLD_System.Properties;
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
    public partial class frmAddEditPersonInfo : Form
    {
        enum Mode { enAddNew = 1,enUpdate = 2 }
        
        Mode _mode;

        clsPerson _person;

        int _PersonID;

        public delegate void DataBackEventHandler(int PersonID);
        
        public event DataBackEventHandler DataBack;

        
        public frmAddEditPersonInfo(int PersonID)
        {
            InitializeComponent();

            _PersonID = PersonID;

            if (_PersonID == -1)
            {
                _mode = Mode.enAddNew;
            }
            else
            {
                _mode = Mode.enUpdate;
            }
        }

        void _FillComboBoxWithCountries()
        {
            DataTable countriesDataTable = clsCountry.GetAllCountries();
            foreach (DataRow row in countriesDataTable.Rows)
            {
                cbCountry.Items.Add(row["CountryName"]);
            }

            cbCountry.SelectedIndex = 168;
        }

        void _LoadData()
        {
            //llRemove.Visible = false;

            if (_mode == Mode.enAddNew)
            {
                _person = new clsPerson();
                lblFormMode.Text = "ADD NEW PERSON";
                _FillComboBoxWithCountries();
                return;
            }

            _person = clsPerson.Find(_PersonID);
            if (_person != null)
            {
                lblFormMode.Text = "UPDATE PERSON";

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

                if (_person.Gendor == 0)
                {
                    rbMale.Checked = true;
                }
                else
                {
                    rbFemale.Checked = true;
                }

                if (_person.ImagePath != "")
                {
                    pbPersonImage.Image = Image.FromFile( _person.ImagePath );
                    llRemove.Visible = true;
                }
                else
                {
                    pbPersonImage.Image = _person.Gendor == 0?Resources.male:Resources.female;
                    llRemove.Visible = false;
                }
                _FillComboBoxWithCountries();

                cbCountry.SelectedIndex = _person.NationalityCountryID  - 1;
            }   
        }
        
        private void frmAddEditPersonInfo_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void txtNationalNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            string inputNationalNo = txtNationalNo.Text;

            if (clsPerson.IsExist(inputNationalNo) && _mode != Mode.enUpdate)
            {
                NationalNoErrorProvider.SetError(txtNationalNo,"National Number Is Exist");
                e.Cancel = true;
            }
            else
            {
                NationalNoErrorProvider.SetError(txtNationalNo, string.Empty); // Clear the error if the input is valid
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void txtboxex_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtvalidating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(((TextBox)sender).Text))
            {
                NationalNoErrorProvider.SetError(((TextBox)sender), "Feild Is Empty");
                e.Cancel = true;
            }
            else
            {
                NationalNoErrorProvider.SetError(txtNationalNo, string.Empty);
            }
        }

        private void rtbAddress_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(rtbAddress.Text))
            {
                NationalNoErrorProvider.SetError(rtbAddress, "Feild Is Empty");
                e.Cancel = true;
            }
            else
            {
                NationalNoErrorProvider.SetError(txtNationalNo, string.Empty);
            }
        }
        void _PreparePersonObjectWithData()
        {
            _person.FirstName = txtFirstName.Text;
            _person.SecondName = txtSecondName.Text;
            _person.ThirdName = txtThirdName.Text;
            _person.LastName = txtLastName.Text;
            _person.NationalNo = txtNationalNo.Text;
            _person.DateOfBirth = dtpDateOfBirth.Value;
            _person.Email = txtEmail.Text;
            _person.Phone = txtPhone.Text;
            _person.Address = rtbAddress.Text;
            _person.Gendor = rbMale.Checked ? (short)0 : (short)1;

            _person.NationalityCountryID = cbCountry.FindString(cbCountry.SelectedItem.ToString()) + 1;

            _person.ImagePath = (pbPersonImage.ImageLocation != null) ? pbPersonImage.ImageLocation : "";
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            _PreparePersonObjectWithData();

            if (_person.Save())
            {
                MessageBox.Show("Person Added Successfuly","Successful",MessageBoxButtons.OK,MessageBoxIcon.Information);
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

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pbPersonImage.ImageLocation = openFileDialog1.FileName;

                pbPersonImage.Image = Image.FromFile(openFileDialog1.FileName);

            }

            llRemove.Visible = true;
        }

        private void rbGendor_CheckedChanged(object sender, EventArgs e)
        {
            pbPersonImage.Image = (RadioButton)sender == rbMale? Resources.male: Resources.female;
        }

        private void llRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.Image = rbMale.Checked?Resources.male: Resources.female;

            llRemove.Visible = rbMale.Checked;
        }
    }
}
