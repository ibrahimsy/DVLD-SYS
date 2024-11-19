using DevExpress.XtraEditors;
using DVLD_Bussiness;
using DVLD_System.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_System.Applications.Local_Applications
{
    public partial class frmAddEditLocalDrivingLicenseApplication : DevExpress.XtraEditors.XtraForm
    {
        enum enMode { enAddNew = 1,enUpdate = 2}
        enMode _Mode = enMode.enAddNew;

        int _LocalDrivingLicenseAppID = -1;
        clsLocalDrivingLicenseApplication _LocalDrivingLicenseAppInfo;
        public frmAddEditLocalDrivingLicenseApplication()
        {
            InitializeComponent();
            _Mode = enMode.enAddNew;
        }

        public frmAddEditLocalDrivingLicenseApplication(int LocalDrivingLicenseAppID)
        {
            InitializeComponent();
            _LocalDrivingLicenseAppID = LocalDrivingLicenseAppID;
            _Mode = enMode.enUpdate;
        }

        private void _ResetDefaultInfo()
        {
            cbLicenseClasses.SelectedIndex = 0;

            lblApplicationDate.Text = DateTime.Now.ToString();

            lblCreatedByUser.Text = clsGlobalSettings.CurrentUser.UserName;

            lblApplicationFees.Text = clsApplicationTypes.Find((int) clsApplication.enApplicationTypes.enNewLocalDrivingLicense).Fees.ToString();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if(ctrlPersonCardWithFilter1.PersonID == -1)
            {
                MessageBox.Show("No Person Selected Yet",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            btnSave.Enabled = true;
            tcNewLocalDrivingLicense.SelectedIndex = 1;
                
        }

        private void _LoadInfo()
        {
            int ApplicantID = _LocalDrivingLicenseAppInfo.ApplicantPersonID;
            //ctrlPersonCardWithFilter1.TxtFilterValue = ApplicantID;
            //ctrlPersonCardWithFilter1.FilterByPersonIDIndex = 1;
            //ctrlPersonCardWithFilter1.BtnFindPerson.PerformClick();
            ctrlPersonCardWithFilter1.EnableFilter = false;

            lblApplicationID.Text = _LocalDrivingLicenseAppInfo.LocalDrivingLicenseApplicationID.ToString();
            lblApplicationDate.Text = _LocalDrivingLicenseAppInfo.ApplicationDate.ToString();
            lblApplicationFees.Text = _LocalDrivingLicenseAppInfo.PaidFees.ToString();
            lblCreatedByUser.Text = clsUser.Find(_LocalDrivingLicenseAppInfo.CreatedByUserID).UserName;
            cbLicenseClasses.SelectedIndex = cbLicenseClasses.FindString(clsLicenseClass.Find(_LocalDrivingLicenseAppInfo.LicenseClassID).ClassName);
        
            btnSave.Enabled = true;
            lblAddEditUser.Text = "Edit Local Driving License Application";
        }
        private void frmAddEditLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            if (_Mode == enMode.enAddNew)
            {
                _LocalDrivingLicenseAppInfo = new clsLocalDrivingLicenseApplication();
                _ResetDefaultInfo();
            }
            else
            {
                _LocalDrivingLicenseAppInfo = 
                    clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseID(_LocalDrivingLicenseAppID);
                _LoadInfo();
            }
        }

        private void tcNewLocalDrivingLicense_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if(e.TabPage == tpApplicationInfo && ctrlPersonCardWithFilter1.PersonID == -1)
                e.Cancel = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int LicenseClassID = cbLicenseClasses.SelectedIndex + 1;

            int LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplication.DoesApplicationExist(ctrlPersonCardWithFilter1.PersonID, LicenseClassID);

            if (LocalDrivingLicenseApplicationID != -1)
            {
                MessageBox.Show($"Choose another license class,the selected person has an active application" +
                         $" for the selected class with Application ID [{LocalDrivingLicenseApplicationID}]", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            _LocalDrivingLicenseAppInfo = new clsLocalDrivingLicenseApplication();

            _LocalDrivingLicenseAppInfo.ApplicationStatus = (byte)clsApplication.enApplicationStatus.enNew;
            _LocalDrivingLicenseAppInfo.ApplicantPersonID = ctrlPersonCardWithFilter1.PersonID;
            _LocalDrivingLicenseAppInfo.ApplicationDate = DateTime.Now;
            _LocalDrivingLicenseAppInfo.LastStatusDate = DateTime.Now;
            _LocalDrivingLicenseAppInfo.ApplicationTypeID = (int)clsLocalDrivingLicenseApplication.enApplicationTypes.enNewLocalDrivingLicense;
            _LocalDrivingLicenseAppInfo.PaidFees = Convert.ToSingle(lblApplicationFees.Text);
            _LocalDrivingLicenseAppInfo.CreatedByUserID = clsGlobalSettings.CurrentUser.UserID;
            _LocalDrivingLicenseAppInfo.LicenseClassID = LicenseClassID;

            if (_LocalDrivingLicenseAppInfo.Save())
            {
                MessageBox.Show("Application Saved Successfuly.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                lblApplicationID.Text = _LocalDrivingLicenseAppInfo.LocalDrivingLicenseApplicationID.ToString();

                btnSave.Enabled = false;
            }
            else
            {
                MessageBox.Show("An Error Occoured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}