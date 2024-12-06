using BankBussiness;
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

namespace DVLD_System.Vehichles
{
    public partial class frmVehiclesList : Form
    {
        private static DataTable _dtAllVehichles;
        public frmVehiclesList()
        {
            InitializeComponent();
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

        }

        private void editVehicleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditVehicle frm = new frmAddEditVehicle((int)dgvVehichleList.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            frmVehichlesList_Load(null,null);
        }
    }
}
