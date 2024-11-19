using DVLD_Bussiness;
using DVLD_System.Licenses;
using DVLD_System.Licenses.International_Licenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_System.Drivers
{
    public partial class frmShowDrivers : Form
    {
        DataTable _dtDrivers;
        public frmShowDrivers()
        {
            InitializeComponent();
        }

        private void frmShowDrivers_Load(object sender, EventArgs e)
        {
            dgvDrivers.DefaultCellStyle.Font = new Font("Arial",12);

            cbFilterBy.SelectedIndex = 0;

            _dtDrivers = clsDriver.GetAllDrivers();

            dgvDrivers.DataSource = _dtDrivers;

            lblDriversCount.Text = _dtDrivers.Rows.Count.ToString();

            if (_dtDrivers.Rows.Count > 0)
            {
               
                dgvDrivers.Columns[0].HeaderText = "Driver ID";
                dgvDrivers.Columns[0].Width = 100;

                dgvDrivers.Columns[1].HeaderText = "Person ID";
                dgvDrivers.Columns[1].Width = 100;

                dgvDrivers.Columns[2].HeaderText = "National No";
                dgvDrivers.Columns[2].Width = 100;

                dgvDrivers.Columns[3].HeaderText = "Full Name";
                dgvDrivers.Columns[3].Width = 300;

                dgvDrivers.Columns[4].HeaderText = "Created Date";
                dgvDrivers.Columns[4].Width = 200;

                dgvDrivers.Columns[5].HeaderText = "Active Licenses";
                dgvDrivers.Columns[5].Width = 100;

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "None")
            {
                txtFilterValue.Visible = false;
                dgvDrivers.DataSource = _dtDrivers;
                return;
            }
            else
            {
                txtFilterValue.Visible = true;
                txtFilterValue.Focus();
            }
        }

        string _GetColumnText(string columnName)
        {
            string ColumnString = "";

            switch (columnName)
            {
                case "Driver ID":
                    ColumnString = "DriverID";
                    break;
                case "Person ID":
                    ColumnString = "PersonID";
                    break;
                case "National No":
                    ColumnString = "NationalNo";
                    break;
                case "Full Name":
                    ColumnString = "FullName";
                    break;
            }
            return ColumnString;
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
           
            string FilterValue = txtFilterValue.Text;

            if (FilterValue == "")
            {
                _dtDrivers.DefaultView.RowFilter = "";
                lblDriversCount.Text = _dtDrivers.DefaultView.Count.ToString();
                return;
            }
            string ColumnText = _GetColumnText(cbFilterBy.Text);

            if (cbFilterBy.Text == "Driver ID" || cbFilterBy.Text == "Person ID")
            {
                _dtDrivers.DefaultView.RowFilter = $"{ColumnText} = '{int.Parse(FilterValue)}'";
            }
            else
            {
                _dtDrivers.DefaultView.RowFilter = $"{ColumnText} Like '{FilterValue}%'";
            }
            lblDriversCount.Text = _dtDrivers.DefaultView.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Person ID" || cbFilterBy.Text == "Driver ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvDrivers.CurrentRow.Cells[1].Value;

            frmPersonDetails PersonInfo = new frmPersonDetails(PersonID);
            PersonInfo.ShowDialog();
        }

        private void issueInternationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dgvDrivers.CurrentRow.Cells[0].Value;
            
            clsDriver driver  = clsDriver.FindByDriverID(DriverID);
            int LicenseID = -1;
            if ((LicenseID = driver.GetOrdinaryLicense()) != -1)
            {
                frmAddNewInternationalApplication frmAddNewInternationalApplication = new frmAddNewInternationalApplication(LicenseID);
                frmAddNewInternationalApplication.ShowDialog();
            }
            else
            {
                MessageBox.Show("The Driver Doesn't Have Class-3 Ordinary Driving License.");
                return;
            }
            
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvDrivers.CurrentRow.Cells[1].Value;

            frmShowLicensesHistory showLicenseHistory = new frmShowLicensesHistory(PersonID);
            showLicenseHistory.ShowDialog();
        }
    }
}
