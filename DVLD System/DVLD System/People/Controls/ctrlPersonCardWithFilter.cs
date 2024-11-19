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

namespace DVLD_System.Controls
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {
        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }

        public int PersonID
        {
            get
            {
                return ctrlPersonCard1.PersonID;
            }
        }

        void _FillComboBox()
        {
            cbFilterBy.Items.Add("National No");
            cbFilterBy.Items.Add("Person ID");

            cbFilterBy.SelectedIndex = 0;
        }
        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            _FillComboBox();

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Focus();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            btnFindPerson.Enabled = !string.IsNullOrWhiteSpace(txtFilterValue.Text); 
        }

        private void btnFindPerson_Click(object sender, EventArgs e)
        {
            if(cbFilterBy.SelectedItem.ToString() == "National No")
            {
                string NationalNo = txtFilterValue.Text;

                ctrlPersonCard1.LoadPersonInfo(NationalNo);   
            }
            else
            {
                if (int.TryParse(txtFilterValue.Text,out int personId))
                {
                        ctrlPersonCard1.LoadPersonInfo(personId);
                }
                else
                {
                    MessageBox.Show("Please enter a valid integer number.");
                }
            }
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo addEditPersonInfo = new frmAddEditPersonInfo(-1);

            addEditPersonInfo.DataBack += _PersonInformatio_DataBack;

            addEditPersonInfo.ShowDialog();
        }

        private void _PersonInformatio_DataBack(int PersonID)
        {
            txtFilterValue.Text = PersonID.ToString();
            cbFilterBy.SelectedIndex = 1;
            ctrlPersonCard1.LoadPersonInfo(PersonID);
        }
    }
}
