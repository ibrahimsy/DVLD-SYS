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
    public partial class ctrlLicenseInfoWithFilter : UserControl
    {
        int _LicenseID = -1;

        public int LicenseID
        {
            get
            {
                return _LicenseID;
            }
        } 
        public ctrlLicenseInfoWithFilter()
        {
            InitializeComponent();
        }

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int LicenseID = int.Parse(txtLicenseID.Text);

            ctrlDriverLicenseInfo1.LoadLicenseInfo(LicenseID);

            _LicenseID = ctrlDriverLicenseInfo1.LicenseID;

        }
    }
}
