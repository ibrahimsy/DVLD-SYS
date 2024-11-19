using DVLD_Bussiness;
using DVLD_System.Tests.Test_Types;
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
    public partial class frmManageApplicationTypes : Form
    {
        DataTable _dtApplicationTypes;
        public frmManageApplicationTypes()
        {
            InitializeComponent();
        }

        void _RefreshApplicationTypesList()
        {
            _dtApplicationTypes = clsApplicationTypes.ApplicationTypesList();

            dgvApplicationTypes.DataSource = _dtApplicationTypes;

        }

        private void clsManageApplicationTypes_Load(object sender, EventArgs e)
        {
            _RefreshApplicationTypesList();

            dgvApplicationTypes.Columns[0].HeaderText = "ID";
            dgvApplicationTypes.Columns[0].Width = 100;

            dgvApplicationTypes.Columns[1].HeaderText = "Title";
            dgvApplicationTypes.Columns[1].Width = 150;

            dgvApplicationTypes.Columns[2].HeaderText = "Fees";
            dgvApplicationTypes.Columns[2].Width = 100;

            lblNumberOfRecords.Text = _dtApplicationTypes.Rows.Count.ToString();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ApplicationTypeID = (int)dgvApplicationTypes.CurrentRow.Cells[0].Value;
            Form frm = new frmEditApplicationType(ApplicationTypeID);
            frm.ShowDialog();

            _RefreshApplicationTypesList();

            lblNumberOfRecords.Text = _dtApplicationTypes.Rows.Count.ToString();
        }
    }
}
