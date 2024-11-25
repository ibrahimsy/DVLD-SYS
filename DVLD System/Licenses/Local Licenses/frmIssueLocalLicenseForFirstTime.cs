using DVLD_Bussiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_System.Licenses
{
    public partial class frmIssueLocalLicenseForFirstTime : Form
    {
        int _LocalApplicationID = -1;

        clsLocalDrivingLicenseApplication _LocalApplicationInfo;
        public frmIssueLocalLicenseForFirstTime(int LocalApplicationID)
        {
            InitializeComponent();
            _LocalApplicationID = LocalApplicationID;
        }

        void _LoadInfo()
        {
            _LocalApplicationInfo = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseID(_LocalApplicationID);

            if (_LocalApplicationInfo == null)
            {
                MessageBox.Show("No Application Found","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
                return;
            }

            if (!_LocalApplicationInfo.PassedAllTests())
            {
                MessageBox.Show("Person Should Pass All Test First", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            if (_LocalApplicationInfo.IsLicenseIssued())
            {
                MessageBox.Show("There is License Issued For This Application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrlLocalDrivingLicenseApplicationInfo1.LoadApplicationInfo(_LocalApplicationID);
           
        }

        private void frmIssueLocalLicenseForFirstTime_Load(object sender, EventArgs e)
        {
            _LoadInfo();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            int LicensID = _LocalApplicationInfo.IssueLicenseForFirstTime(txtNotes.Text.Trim(),clsGlobalSettings.CurrentUser.UserID);
            if (LicensID != -1)
            {
                MessageBox.Show($"License Issued Successfuly With ID = [{LicensID}]", "Successed",MessageBoxButtons.OK,MessageBoxIcon.Information);             
                this.Close();
            }
            else
            {
                MessageBox.Show($"License Was Not Issued !","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);          
            }
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
