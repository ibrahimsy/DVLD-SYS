using DevExpress.Utils;
using DVLD_Bussiness;
using DVLD_System.Licenses.International_Licenses;
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

namespace DVLD_System.Licenses.Controls
{
    public partial class ctrlDriverLicensesHistory : UserControl
    {
        DataTable _dtLocalLicenses;
        DataTable _dtInternationalLicenses;

        clsDriver _Driver;
        int _DriverID = -1;

        public ctrlDriverLicensesHistory()
        {
            InitializeComponent();
        }

        void _LoadLocalLicensesInfo()
        {
               
                _dtLocalLicenses = clsDriver.GetLicenses(_DriverID);
                dgvLocalLicenses.DataSource = _dtLocalLicenses;
           

            if (dgvLocalLicenses.Rows.Count > 0)
            {
                dgvLocalLicenses.Columns[0].HeaderText = "Lic.ID";
                dgvLocalLicenses.Columns[0].Width = 100;

                dgvLocalLicenses.Columns[1].HeaderText = "App.ID";
                dgvLocalLicenses.Columns[1].Width = 100;

                dgvLocalLicenses.Columns[2].HeaderText = "Class Name";
                dgvLocalLicenses.Columns[2].Width = 210;

                dgvLocalLicenses.Columns[3].HeaderText = "Issue Date";
                dgvLocalLicenses.Columns[3].Width = 160;

                dgvLocalLicenses.Columns[4].HeaderText = "Expiration Date";
                dgvLocalLicenses.Columns[4].Width = 160;

                dgvLocalLicenses.Columns[5].HeaderText = "Is Active";
                dgvLocalLicenses.Columns[5].Width = 100;
            }
        }
        void _LoadInternationalLicensesInfo()
        {

            _dtInternationalLicenses = clsDriver.GetInternationalLicenses(_DriverID);
            dgvInternationalLicenses.DataSource = _dtInternationalLicenses;


            if (_dtInternationalLicenses.Rows.Count > 0)
            {
                dgvInternationalLicenses.Columns[0].HeaderText = "Int.Lic.ID";
                dgvInternationalLicenses.Columns[0].Width = 100;

                dgvInternationalLicenses.Columns[1].HeaderText = "App.ID";
                dgvInternationalLicenses.Columns[1].Width = 100;

                dgvInternationalLicenses.Columns[2].HeaderText = "L.LID";
                dgvInternationalLicenses.Columns[2].Width = 210;

                dgvInternationalLicenses.Columns[3].HeaderText = "Issue Date";
                dgvInternationalLicenses.Columns[3].Width = 160;

                dgvInternationalLicenses.Columns[4].HeaderText = "Expiration Date";
                dgvInternationalLicenses.Columns[4].Width = 160;

                dgvInternationalLicenses.Columns[5].HeaderText = "Is Active";
                dgvInternationalLicenses.Columns[5].Width = 100;
            }
        }
        public void LoadInfo(int PersonID)
        {
             _Driver = clsDriver.FindByPersonID(PersonID);
            if(_Driver == null)
            {
                MessageBox.Show("No Driver Found","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            else
            {
                _DriverID = _Driver.DriverID;
            }

            _LoadLocalLicensesInfo();

            _LoadInternationalLicensesInfo();
        }

        void _showLicenseInfo()
        {
            int LicenseID = (int)dgvLocalLicenses.CurrentRow.Cells[0].Value;

            frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);
            frm.ShowDialog();
        }
        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _showLicenseInfo();
        }

        void _ShowInternationalLicenseInfo()
        {
            int InternationalLicenseID = (int)dgvInternationalLicenses.CurrentRow.Cells[0].Value;

            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(InternationalLicenseID);
            frm.ShowDialog();
        }
       
        private void showInternationalLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ShowInternationalLicenseInfo();
        }

        private void cmsInternationalLicenses_Opening(object sender, CancelEventArgs e)
        {

        }

        private void dgvLocalLicenses_DoubleClick(object sender, EventArgs e)
        {
            _showLicenseInfo();
        }

        private void dgvInternationalLicenses_DoubleClick(object sender, EventArgs e)
        {
            _ShowInternationalLicenseInfo();
        }
    }
}
