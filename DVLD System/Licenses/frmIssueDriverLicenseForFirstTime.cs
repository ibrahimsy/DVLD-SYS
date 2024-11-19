using DevExpress.Utils.VisualEffects;
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

namespace DVLD_System.Licenses
{
    public partial class frmIssueDriverLicenseForFirstTime : Form
    {
        int _LocalApplicationID = -1;

        clsLocalDrivingLicenseApplication _LocalApplicationInfo;
        public frmIssueDriverLicenseForFirstTime(int LocalApplicationID)
        {
            InitializeComponent();
            _LocalApplicationID = LocalApplicationID;
        }

        void _LoadInfo()
        {
            ctrlLocalDrivingLicenseApplicationInfo1.LoadApplicationInfo(_LocalApplicationID);
        }
        private void frmIssueDriverLicenseForFirstTime_Load(object sender, EventArgs e)
        {
            _LoadInfo();
        }

        void _IssueLicense()
        {
            clsLicense NewLicense = new clsLicense();

            NewLicense.
        }
        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
