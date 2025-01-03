﻿using BankBussiness;
using DVLD_Bussiness;
using DVLD_System.VehicleLicenses;
using DVLD_System.Vehicles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_System.Vehichles
{
    public partial class frmVehiclesList : Form
    {
        private static DataTable _dtAllVehichles;
        public frmVehiclesList()
        {
            InitializeComponent();
        }

        string _ColumnText(string columnName)
        {
            switch (columnName)
            {
                case "Vehichle ID":
                    return "VehichleID";
                case "Chassis Number":
                    return "ChassisNumber";
                case "Plate Number":
                    return "PlateNumber";
                case "Make":
                    return "Make";
                case "Model":
                    return "Model";
                case "Body Type":
                    return "Body";
                case "Owner Name":
                    return "FullName";
                case "Year":
                    return "Year";
                case "Color":
                    return "Color";
                default:
                    return "None";
            }
        }

        private void frmVehichlesList_Load(object sender, EventArgs e)
        {
            _dtAllVehichles = clsVehichle.VehichlesList();
            dgvVehichleList.DataSource = _dtAllVehichles;
            lblNumberOfRecords.Text = _dtAllVehichles.Rows.Count.ToString();

            cbFilterBy.SelectedIndex = 0;

            if (_dtAllVehichles.Rows.Count > 0)
            {
                dgvVehichleList.Columns[0].HeaderText = "Vehichle ID";
                dgvVehichleList.Columns[0].Width = 80;

                dgvVehichleList.Columns[1].HeaderText = "Chassis Number";
                dgvVehichleList.Columns[1].Width = 90;

                dgvVehichleList.Columns[2].HeaderText = "Plate Number";
                dgvVehichleList.Columns[2].Width = 100;

                dgvVehichleList.Columns[3].HeaderText = "Make";
                dgvVehichleList.Columns[3].Width = 100;

                dgvVehichleList.Columns[4].HeaderText = "Model";
                dgvVehichleList.Columns[4].Width = 100;

                dgvVehichleList.Columns[5].HeaderText = "Sub Model";
                dgvVehichleList.Columns[5].Width = 100;

                dgvVehichleList.Columns[6].HeaderText = "Body";
                dgvVehichleList.Columns[6].Width = 100;

                dgvVehichleList.Columns[7].HeaderText = "Owner Full Name";
                dgvVehichleList.Columns[7].Width = 120;

                dgvVehichleList.Columns[8].HeaderText = "Year";
                dgvVehichleList.Columns[8].Width = 120;

                dgvVehichleList.Columns[9].HeaderText = "Color";
                dgvVehichleList.Columns[9].Width = 100;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddVehichle_Click(object sender, EventArgs e)
        {
            frmAddEditVehicle frm = new frmAddEditVehicle();
            frm.ShowDialog();

            frmVehichlesList_Load(null,null);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            bool ActiveLicesneExist = clsVehichle.Find((int)dgvVehichleList.CurrentRow.Cells[0].Value).HasActiveLicense();
           
            issueLicenseForFirstTimeToolStripMenuItem.Enabled = !ActiveLicesneExist;

            ShowVehicleLicense_toolStripMenuItem7.Enabled = ActiveLicesneExist;
        }

        private void editVehicleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditVehicle frm = new frmAddEditVehicle((int)dgvVehichleList.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            frmVehichlesList_Load(null,null);
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedIndex == cbFilterBy.FindString("None"))
            {
                txtFilterBy.Visible = false;
                _dtAllVehichles.DefaultView.RowFilter = "";
                return;
            }

            txtFilterBy.Visible = true;       
            txtFilterBy.Focus();
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            string SelectedColumn = _ColumnText(cbFilterBy.Text);

            string FilterValue = txtFilterBy.Text.Trim();

            if (FilterValue == "")
            {
                _dtAllVehichles.DefaultView.RowFilter = "";
                lblNumberOfRecords.Text = _dtAllVehichles.DefaultView.Count.ToString();
                return;
            }

            if (SelectedColumn == "VehichleID" || SelectedColumn == "Year")
            {

                _dtAllVehichles.DefaultView.RowFilter = string.Format("[{0}] = {1}", SelectedColumn, FilterValue);
            }
            else
            {
                _dtAllVehichles.DefaultView.RowFilter = string.Format("[{0}] Like '{1}%'", SelectedColumn, FilterValue);
            }

            lblNumberOfRecords.Text = _dtAllVehichles.DefaultView.Count.ToString();
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "VehichleID" || cbFilterBy.Text == "Year")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            frmShowVehicleInfo frm = new frmShowVehicleInfo((int)dgvVehichleList.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void addVehicleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditVehicle frm = new frmAddEditVehicle();
            frm.ShowDialog();

            frmVehichlesList_Load(null, null);
        }

        private void deleteVehicleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want To Delete This Vehicle", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;            
            
            if (clsVehichle.DeleteVehichle((int)dgvVehichleList.CurrentRow.Cells[0].Value))
                
                MessageBox.Show("Vehicle Deleted Successfuly",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            else
                MessageBox.Show("Vehicle Not Deleted",
                    "Faild",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
        }

        private void issueLicenseForFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIssueVehicleLicense frm = new frmIssueVehicleLicense((int)dgvVehichleList.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            int VehicleLicensID = clsVehichle.Find((int)dgvVehichleList.CurrentRow.Cells[0].Value).GetLicenseIDByVehicleID();
            frmShowVehicleLicense frm = new frmShowVehicleLicense(VehicleLicensID);
            frm.ShowDialog();
        }

        private void renewLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewVehicleLicense frm = new frmRenewVehicleLicense((int)dgvVehichleList.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void cancelLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want To Cancel Vehicle License", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            bool IsCancelled = clsVehichle.Find((int)dgvVehichleList.CurrentRow.Cells[0].Value).CancelLicense();

            if (IsCancelled)
                MessageBox.Show("The License is cancelled Successfully", "Done", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            else
                MessageBox.Show("The License is didn't cancel ,An Error Occurred", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
