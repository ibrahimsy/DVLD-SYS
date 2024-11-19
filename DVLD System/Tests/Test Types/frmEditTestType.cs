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

namespace DVLD_System.Tests.Test_Types
{
    public partial class frmEditTestType : Form
    {
        clsTestType _TestType;
        public frmEditTestType(int TestTypeID)
        {
            InitializeComponent();

            _TestType = clsTestType.Find(TestTypeID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEditTestType_Load(object sender, EventArgs e)
        {
            lblTestTypeID.Text = _TestType.TestTypeID.ToString();

            txtTitle.Text = _TestType.TestTypeTitle;

            txtDescription.Text = _TestType.TestTypeDescription;

            txtFees.Text = _TestType.TestTypeFees.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
                return;

            _TestType.TestTypeTitle = txtTitle.Text;
            _TestType.TestTypeDescription = txtDescription.Text;
            _TestType.TestTypeFees = Convert.ToSingle(txtFees.Text);

            if (_TestType.Save())
            {
                MessageBox.Show("Test Type Edited Sucessfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Something went wrong.", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void ValidateEmptyValue(object sender, CancelEventArgs e)
        {
            TextBox temp = (TextBox)sender;

            if (String.IsNullOrEmpty(temp.Text))
            {
                errorProvider1.SetError(temp, "This Feild is Required");
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
