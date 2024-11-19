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
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_System.Applications
{
    public partial class frmAddNewLocalDrivingLicense : Form
    {
        enum enMode { enAddNew = 1, enUpdate = 2 }
        enMode _Mode = enMode.enAddNew;

        int _LocalDrivingLicenseAppID = -1;
        
        int _SelectedPersonID = -1;
        
        clsLocalDrivingLicenseApplication _LocalDrivingLicenseAppInfo;
        public frmAddNewLocalDrivingLicense()
        {
            InitializeComponent();
            _Mode = enMode.enAddNew;
        }

        public frmAddNewLocalDrivingLicense(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseAppID = LocalDrivingLicenseApplicationID;
            _Mode = enMode.enUpdate;
        }

        void _FillComboBoxLicenseClass()
        {
            DataTable dtLicenseClass = clsLicenseClass.GetAllLicenseClasses();

            foreach (DataRow row in dtLicenseClass.Rows)
            {
                cbLicenseClasses.Items.Add(row["ClassName"]);
            }
        }

        private void _ResetDefaultInfo()
        {
            _FillComboBoxLicenseClass();

            if (_Mode == enMode.enAddNew)
            {
                lblAddEditTitle.Text = "ADD NEW LOCAL DRIVING LICENSE APPLICATION";
                this.Text = "ADD NEW LOCAL DRIVING LICENSE APPLICATION";
                _LocalDrivingLicenseAppInfo = new clsLocalDrivingLicenseApplication();
                tpApplicationInfo.Enabled = false;
                ctrlPersonCardWithFilter1.FilterFocus();

                cbLicenseClasses.SelectedIndex = 2;

                lblApplicationDate.Text = DateTime.Now.ToString();

                lblCreatedByUser.Text = clsGlobalSettings.CurrentUser.UserName;

                lblApplicationFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationTypes.enNewLocalDrivingLicense).Fees.ToString();

                btnSave.Enabled = false;
    
            }
            else 
            {
                lblAddEditTitle.Text = "UPDATE LOCAL DRIVING LICENSE APPLICATION";

                this.Text = "UPDATE LOCAL DRIVING LICENSE APPLICATION";

                btnSave.Enabled = true;

                tpApplicationInfo.Enabled = true;
            }
            
        }  

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

            if (ctrlPersonCardWithFilter1.PersonID == -1)
            {
                MessageBox.Show("No Person Selected Yet",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            tpApplicationInfo.Enabled = true;
            btnSave.Enabled = true;
            tcNewLocalDrivingLicense.SelectedTab = tcNewLocalDrivingLicense.TabPages["tpApplicationInfo"];
        }

        void _LoadInfo()
        {
            ctrlPersonCardWithFilter1.EnableFilter = false;
            _LocalDrivingLicenseAppInfo = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseID(_LocalDrivingLicenseAppID);

            if (_LocalDrivingLicenseAppInfo == null)
            {
                MessageBox.Show("No Application Found","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            
            ctrlPersonCardWithFilter1.LoadInfo(_LocalDrivingLicenseAppInfo.ApplicantPersonID);

            lblApplicationID.Text = _LocalDrivingLicenseAppInfo.LocalDrivingLicenseApplicationID.ToString();
            lblApplicationDate.Text = _LocalDrivingLicenseAppInfo.ApplicationDate.ToShortDateString();
            cbLicenseClasses.SelectedIndex = cbLicenseClasses.FindString(clsLicenseClass.Find(_LocalDrivingLicenseAppInfo.LicenseClassID).ClassName);
            lblApplicationFees.Text = _LocalDrivingLicenseAppInfo.PaidFees.ToString();
            lblCreatedByUser.Text = clsUser.Find(_LocalDrivingLicenseAppInfo.CreatedByUserID).UserName;

        }
        
        private void frmAddNewLocalDrivingLicense_Load(object sender, EventArgs e)
        {
           
             _ResetDefaultInfo();

            if (_Mode == enMode.enUpdate)
            {
                _LoadInfo();
            }
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int LicenseClassID = clsLicenseClass.Find(cbLicenseClasses.Text).LicenseClassID;

            //check if there is an active application with license class

            int LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplication.GetActiveApplication(ctrlPersonCardWithFilter1.PersonID,clsApplication.enApplicationStatus.enNew,LicenseClassID);

            if (LocalDrivingLicenseApplicationID != -1)
            {
                MessageBox.Show($"Choose another license class,the selected person has an active application" +
                         $" for the selected class with Application ID [{LocalDrivingLicenseApplicationID}]", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //check if there is an active License For license class

            int LicenseID = clsLicense.GetActiveLicensePerPersonID(_SelectedPersonID, LicenseClassID);
            
            if (LicenseID != -1)
            {
                MessageBox.Show($"There is Active License For This Person With License Class"
                               , "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
                _LocalDrivingLicenseAppInfo.CreatedByUserID = clsGlobalSettings.CurrentUser.UserID;
                _LocalDrivingLicenseAppInfo.ApplicantPersonID = ctrlPersonCardWithFilter1.PersonID;
                _LocalDrivingLicenseAppInfo.ApplicationStatus = (byte)clsApplication.enApplicationStatus.enNew;
                _LocalDrivingLicenseAppInfo.ApplicationDate = DateTime.Now;
                _LocalDrivingLicenseAppInfo.LastStatusDate = DateTime.Now;
                _LocalDrivingLicenseAppInfo.ApplicationTypeID = (int)clsLocalDrivingLicenseApplication.enApplicationTypes.enNewLocalDrivingLicense;
                _LocalDrivingLicenseAppInfo.PaidFees = Convert.ToSingle(lblApplicationFees.Text);
                _LocalDrivingLicenseAppInfo.LicenseClassID = LicenseClassID;

            if (_LocalDrivingLicenseAppInfo.Save())
            {
                _Mode = enMode.enUpdate;

                MessageBox.Show("Application Created Successfuly.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                lblApplicationID.Text = _LocalDrivingLicenseAppInfo.LocalDrivingLicenseApplicationID.ToString();

                lblAddEditTitle.Text = "UPDATE LOCAL DRIVING LICENSE APPLICATION";

                btnSave.Enabled = false;
            }
            else
            {
                MessageBox.Show("An Error Occoured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
        }

        private void cbLicenseClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            //if (e.TabPage == tcNewLocalDrivingLicense.TabPages[1])
            //{
            //    if(ctrlPersonCardWithFilter1.PersonID == -1)
            //    {
            //        e.Cancel = true;
            //        MessageBox.Show("You can't Create Application Before Choose A Person","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            //    }
            //    else
            //    {
            //        btnSave.Enabled = true;
            //    }
            //}
        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _SelectedPersonID = obj;
        }

        private void frmAddNewLocalDrivingLicense_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.FilterFocus();
        }

        
    }
}
