using DVLD_Bussiness;
using DVLD_System.Applications.Release_Detained_License_Applications;
using DVLD_System.Licenses.Detain_Licenses;
using DVLD_System.Licenses.Local_Licenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_System.Licenses.Detained_Licenses
{
    public partial class frmListDetainedLicenses : Form
    {
        DataTable _dtDetainedLicenses;
        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }

        void _RefreshDetainedLicensesList()
        {
            _dtDetainedLicenses = clsDetainedLicense.GetAllDetainedLicenses();
            dgvDetainedLicenses.DataSource = _dtDetainedLicenses;
            lblRecordsCount.Text = _dtDetainedLicenses.Rows.Count.ToString();

            if (_dtDetainedLicenses.Rows.Count > 0)
            {
                dgvDetainedLicenses.Columns[0].HeaderText = "Detain ID";
                dgvDetainedLicenses.Columns[0].Width = 100;

                dgvDetainedLicenses.Columns[1].HeaderText = "License ID";
                dgvDetainedLicenses.Columns[1].Width = 100;

                dgvDetainedLicenses.Columns[2].HeaderText = "Detain Date";
                dgvDetainedLicenses.Columns[2].Width = 150;

                dgvDetainedLicenses.Columns[3].HeaderText = "Is Released";
                dgvDetainedLicenses.Columns[3].Width = 100;

                dgvDetainedLicenses.Columns[4].HeaderText = "Fine Fees";
                dgvDetainedLicenses.Columns[4].Width = 100;

                dgvDetainedLicenses.Columns[5].HeaderText = "Release Date";
                dgvDetainedLicenses.Columns[5].Width = 150;

                dgvDetainedLicenses.Columns[6].HeaderText = "National No";
                dgvDetainedLicenses.Columns[6].Width = 100;

                dgvDetainedLicenses.Columns[7].HeaderText = "Full Name";
                dgvDetainedLicenses.Columns[7].Width = 150;

                dgvDetainedLicenses.Columns[8].HeaderText = "Release Application ID";
                dgvDetainedLicenses.Columns[8].Width = 100;
            }
        }

        string _GetColumnNameText(string columnName)
        {
            switch (columnName)
            {
                case "Detain ID":
                    return "DetainID";
                case "License ID":
                    return "LicenseID";
                case "National No":
                    return "NationalNo";
                case "Full Name":
                    return "FullName";
                default:
                    return "";
            }
        }
        
        private void frmListDetainedLicenses_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.ShowDialog();

            _RefreshDetainedLicensesList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense(LicenseID);
            frm.ShowDialog();
            _RefreshDetainedLicensesList();
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;
            frmPersonDetails frm = new frmPersonDetails(clsLicense.Find(LicenseID).DriverInfo.PersonID);
            frm.Show();
        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;
            frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;
            frmShowLicensesHistory frm = new frmShowLicensesHistory(clsLicense.Find(LicenseID).DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense();
            frm.ShowDialog();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilterValue.Text))
            {
                _dtDetainedLicenses.DefaultView.RowFilter = "";
                lblRecordsCount.Text = _dtDetainedLicenses.DefaultView.Count.ToString();
                return;
            }

            if (_dtDetainedLicenses.Rows.Count == 0)
                return;

            string FilterColumnName = _GetColumnNameText(cbFilterBy.Text);

            string FilterValue = txtFilterValue.Text.Trim();

            if (FilterColumnName == "DetainID" || FilterColumnName == "LicenseID")
            {
                _dtDetainedLicenses.DefaultView.RowFilter = $"[{FilterColumnName}] = {FilterValue}";
            }
            else
            {
                _dtDetainedLicenses.DefaultView.RowFilter = $"{FilterColumnName} Like '{FilterValue}%'";
            }

            lblRecordsCount.Text = _dtDetainedLicenses.DefaultView.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "None")
            {
                txtFilterValue.Visible = false;
                _RefreshDetainedLicensesList();
                return;
            }
            txtFilterValue.Visible = true;
            txtFilterValue.Clear();
            txtFilterValue.Focus();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterBy.Text == "Detain ID" || cbFilterBy.Text == "License ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;
            releaseDetainedLicenseToolStripMenuItem.Enabled = clsLicense.Find(LicenseID).IsDetained;
        }
    }
}
