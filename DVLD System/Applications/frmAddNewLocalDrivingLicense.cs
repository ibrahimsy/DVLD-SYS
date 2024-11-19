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
       
        public frmAddNewLocalDrivingLicense()
        {
            InitializeComponent();
        }
        
        private void _FillComboBoxwithLicenseClasses()
        {
            DataTable LicenseClassesDataTable = clsLicenseClass.GetAllLicenseClasses();

            foreach (DataRow dr in LicenseClassesDataTable.Rows)
            {
                cbLicenseClasses.Items.Add(dr["ClassName"]);
            }

            cbLicenseClasses.SelectedIndex = 0;
        }

        private void _FillInitialApplicationInfo()
        {
            lblApplicationDate.Text = DateTime.Now.ToString();
            
            lblCreatedByUser.Text = clsGlobalSettings.CurrentUser.UserName;

            lblApplicationFees.Text = _CalculateApplicationPaidFees((int)clsApplication.enApplicationTypes.enNewLocalDrivingLicense).ToString();

            lblCreatedByUser.Text = clsGlobalSettings.CurrentUser.UserName;
        }

        private float _CalculateApplicationPaidFees(int applicationTypeID)
        {
            float paidFees = 0;

            paidFees = clsLicenseClass.Find(cbLicenseClasses.SelectedItem.ToString()).ClassFees
                       + clsApplicationTypes.Find(applicationTypeID).Fees;
            return paidFees;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelectPerson_Click(object sender, EventArgs e)
        {
            frmFindPerson findPerson = new frmFindPerson();

            findPerson.DataBack += _FindPerson_DataBack;

            findPerson.ShowDialog();
        }

        private void _FindPerson_DataBack(int PersonID)
        {
            ctrlPersonCard1.LoadPersonInfo(PersonID);

            btnNext.Enabled = true;


        }

        private void btnNext_Click(object sender, EventArgs e)
        {
        
                tcNewLocalDrivingLicense.SelectedIndex = 1;
        }

        private void frmAddNewLocalDrivingLicense_Load(object sender, EventArgs e)
        {
            _FillComboBoxwithLicenseClasses();

            _FillInitialApplicationInfo();

        }

        private void _FillApplicationWithData(ref clsApplication application)
        {
            application.ApplicationStatus = (byte)clsApplication.enApplicationStatus.enNew;
            application.ApplicantPersonID = ctrlPersonCard1.PersonID;
            application.ApplicationDate = DateTime.Now;
            application.ApplicationTypeID = (int)clsApplication.enApplicationTypes.enNewLocalDrivingLicense;
            application.PaidFees = Convert.ToSingle(lblApplicationFees.Text);
            application.CreatedByUserID = clsGlobalSettings.CurrentUser.UserID;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int LicenseClassID = cbLicenseClasses.SelectedIndex + 1;

            bool isFound = clsLocalDrivingLicenseApplication.IsExist(ctrlPersonCard1.PersonID, (short)clsApplication.enApplicationStatus.enNew, LicenseClassID);

            if (isFound)
            {
                MessageBox.Show("This Person Allready Has UnComplete Application For This Class", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            clsApplication application = new clsApplication();
            
            _FillApplicationWithData(ref application);

            if (application.Save())
            {
                clsLocalDrivingLicenseApplication LDLApplication = new clsLocalDrivingLicenseApplication();
                
                LDLApplication.ApplicationID = application.ApplicationID;
                LDLApplication.LicenseClassID = cbLicenseClasses.SelectedIndex + 1;

                if (LDLApplication.Save())
                {
                    MessageBox.Show("Application Created Successfuly.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    lblApplicationID.Text = application.ApplicationID.ToString();

                    btnSave.Enabled = false;
                }
                else
                {
                    MessageBox.Show("An Error Occoured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    clsApplication.Delete(application.ApplicationID);
                }  
            }
            else
            {
                MessageBox.Show("An Error Occoured","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }

        private void cbLicenseClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblApplicationFees.Text = _CalculateApplicationPaidFees((int)clsApplication.enApplicationTypes.enNewLocalDrivingLicense).ToString();
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == tcNewLocalDrivingLicense.TabPages[1])
            {
                if(ctrlPersonCard1.PersonID == -1)
                {
                    e.Cancel = true;
                    MessageBox.Show("You can't Create Application Before Choose A Person","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                else
                {
                    btnSave.Enabled = true;
                }
            }
        }
    }
}
