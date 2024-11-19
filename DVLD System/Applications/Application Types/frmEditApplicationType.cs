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

namespace DVLD_System.Applications.Application_Types
{
    public partial class frmEditApplicationType : Form
    {
        clsApplicationTypes _ApplicationType;
        public frmEditApplicationType(int ApplicationTypeID)
        {
            InitializeComponent();

            _ApplicationType = clsApplicationTypes.Find(ApplicationTypeID);   
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEditApplicationType_Load(object sender, EventArgs e)
        {
            lblApplicationTypeID.Text = _ApplicationType.TypeID.ToString();

            txtTitle.Text = _ApplicationType.Title;

            txtFees.Text = _ApplicationType.Fees.ToString();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
                return;

            _ApplicationType.Title = txtTitle.Text;
            _ApplicationType.Fees = Convert.ToSingle(txtFees.Text);

            if (_ApplicationType.Save())
            {
                MessageBox.Show("Application Type Edited Sucessfully.","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Something went wrong.", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void ValidatEmptyTextBox(object sender, CancelEventArgs e)
        {
            TextBox temp = (TextBox)sender;

            if (String.IsNullOrEmpty(temp.Text))
            {
                errorProvider1.SetError(temp,"This Feild is Required");
                e.Cancel = true;
                return;
            }
            else
            {
                errorProvider1.SetError(temp, null);
            }
        }

        private void txtFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
