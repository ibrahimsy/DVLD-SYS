using DVLD_Bussiness;
using DVLD_System.Applications.Application_Types;
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
    public partial class frmManageTestTypes : Form
    {
        DataTable _dtTestTypes;
        public frmManageTestTypes()
        {
            InitializeComponent();
        }

        void _RefreshTestTypesList()
        {
            _dtTestTypes = clsTestType.GetAllTestTypees();

            dgvTestTypea.DataSource = _dtTestTypes;

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            _RefreshTestTypesList();
            
            dgvTestTypea.Columns[0].HeaderText = "ID";
            dgvTestTypea.Columns[0].Width = 20;

            dgvTestTypea.Columns[1].HeaderText = "Title";
            dgvTestTypea.Columns[1].Width = 50;

            dgvTestTypea.Columns[2].HeaderText = "Description";
            dgvTestTypea.Columns[2].Width = 120;

            dgvTestTypea.Columns[3].HeaderText = "Fees";
            dgvTestTypea.Columns[3].Width = 40;

            lblNumberOfRecords.Text = _dtTestTypes.Rows.Count.ToString();

        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestTypeID = (int)dgvTestTypea.CurrentRow.Cells[0].Value;
            Form frm = new frmEditTestType(TestTypeID);
            frm.ShowDialog();

            _RefreshTestTypesList();

            lblNumberOfRecords.Text = _dtTestTypes.Rows.Count.ToString();
        }
    }
}
