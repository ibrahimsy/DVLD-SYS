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
using static DVLD_Bussiness.clsApplication;

namespace DVLD_System.Licenses.International_Licenses
{
    public partial class frmInternationalLicensesApplications : Form
    {
        DataTable _dtInternationalLicenses;

        public frmInternationalLicensesApplications()
        {
            InitializeComponent();
        }

        void _RefreshInternationalLicensesList()
        {
            _dtInternationalLicenses = clsInternationalLicense.GetAllInternationalLicensees();

            dgvInternationalLicenses.DataSource = _dtInternationalLicenses;

            lblNumberOfRecords.Text = _dtInternationalLicenses.Rows.Count.ToString();
        }

        void _LoadDataInfo()
        {
            _RefreshInternationalLicensesList();

            if (dgvInternationalLicenses.Rows.Count > 0)
            {
                dgvInternationalLicenses.Columns[0].HeaderText = "IL.ID";
                dgvInternationalLicenses.Columns[0].Width = 100;

                dgvInternationalLicenses.Columns[1].HeaderText = "App.ID";
                dgvInternationalLicenses.Columns[1].Width = 100;

                dgvInternationalLicenses.Columns[2].HeaderText = "Driver ID";
                dgvInternationalLicenses.Columns[2].Width = 100;

                dgvInternationalLicenses.Columns[3].HeaderText = "L.L.ID";
                dgvInternationalLicenses.Columns[3].Width = 100;

                dgvInternationalLicenses.Columns[4].HeaderText = "Issue Date";
                dgvInternationalLicenses.Columns[4].Width = 150;

                dgvInternationalLicenses.Columns[5].HeaderText = "Expiration Date";
                dgvInternationalLicenses.Columns[5].Width = 150;

                dgvInternationalLicenses.Columns[6].HeaderText = "Is Active";
                dgvInternationalLicenses.Columns[6].Width = 100;
            }

            cbFilterBy.SelectedIndex = 0;

        }

        string _SelectedColumnText(string selectedColumn)
        {
            switch (selectedColumn)
            {
                case "International License ID":
                    return "InternationalLicenseID";
                case "Application ID":
                    return "ApplicationID";
                case "Local License ID":
                    return "IssuedUsingLocalLicenseID";
                case "Driver ID":
                    return "DriverID";
                case "Status":
                    return "IsActive";
            }
            return "";
        }
        
        private void frmInternationalLicensesApplications_Load(object sender, EventArgs e)
        {
            _LoadDataInfo();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNewLicense_Click(object sender, EventArgs e)
        {
            frmAddNewInternationalApplication frm  = new frmAddNewInternationalApplication();
            frm.ShowDialog();

            _RefreshInternationalLicensesList();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string SelectedColumn = _SelectedColumnText(cbFilterBy.Text);

            string FilterValue = ((TextBox)sender).Text.Trim();

            if (FilterValue == "")
            {
                _dtInternationalLicenses.DefaultView.RowFilter = "";
                lblNumberOfRecords.Text = _dtInternationalLicenses.DefaultView.Count.ToString();
                return;
            }

           _dtInternationalLicenses.DefaultView.RowFilter = string.Format("{0} = {1}", SelectedColumn, FilterValue);
           lblNumberOfRecords.Text = _dtInternationalLicenses.DefaultView.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "None")
            {
                txtFilterValue.Visible = false;
                cbLicenseStatus.Visible = false;

                _dtInternationalLicenses.DefaultView.RowFilter = "";
                lblNumberOfRecords.Text = _dtInternationalLicenses.DefaultView.Count.ToString();
                return;
            }

            if (cbFilterBy.Text == "Status")
            {
                cbLicenseStatus.Visible = true;
                cbLicenseStatus.SelectedIndex = 0;
                txtFilterValue.Visible = false;
                return;
            }

            txtFilterValue.Visible = true;
            txtFilterValue.Focus();
            cbLicenseStatus.Visible = false;
        }

        private void cbLicenseStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterValue = cbLicenseStatus.Text;

            switch (FilterValue)
            {
                case "All":
                    _dtInternationalLicenses.DefaultView.RowFilter = "";
                    break;
                case "Active":
                    _dtInternationalLicenses.DefaultView.RowFilter = "IsActive = 1";
                    break;
                case "DisActive":
                    _dtInternationalLicenses.DefaultView.RowFilter = "IsActive = 0";
                    break;
            }

            lblNumberOfRecords.Text = _dtInternationalLicenses.DefaultView.Count.ToString();
        }

        private void showPersonDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dgvInternationalLicenses.CurrentRow.Cells[2].Value;

            frmPersonDetails frm = new frmPersonDetails(clsDriver.FindByDriverID(DriverID).PersonID);
            frm.ShowDialog();
        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int InternationalLicenseID = (int)dgvInternationalLicenses.CurrentRow.Cells[0].Value;

            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(InternationalLicenseID);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dgvInternationalLicenses.CurrentRow.Cells[2].Value;

            frmShowLicensesHistory frm = new frmShowLicensesHistory(clsDriver.FindByDriverID(DriverID).PersonID);
            frm.ShowDialog();
        }
    }
}
