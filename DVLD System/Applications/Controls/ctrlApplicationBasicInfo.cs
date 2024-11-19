using DVLD_Bussiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Internal;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_System
{
    public partial class ctrlApplicationBasicInfo : UserControl
    {
        clsApplication _application;
        public ctrlApplicationBasicInfo()
        {
            InitializeComponent();
        }

        private string _StatusText(clsApplication.enApplicationStatus status)
        {
            switch(status)
            {
               case clsApplication.enApplicationStatus.enNew:
                    return "New";
               case clsApplication.enApplicationStatus.enCanceled:
                    return "Canceled";
               case clsApplication.enApplicationStatus.enCompleted:
                    return "Completed";

            }
            return "Other";
        }

        private void _FillApplicationBasicInfo()
        {
            lblApplicationID.Text = _application.ApplicationID.ToString();
            lblFees.Text = _application.PaidFees.ToString();
            lblStatus.Text = _StatusText((clsApplication.enApplicationStatus)_application.ApplicationStatus);
            lblType.Text = _application.ApplicationTypeInfo.Title.ToString();
            lblApplicant.Text = _application.PersonInfo.FullName;
            lblDate.Text = _application.ApplicationDate.ToString();
            lblStatusDate.Text = _application.LastStatusDate.ToString();
            lblCreatedBy.Text = _application.UserInfo.UserName;
        }
        public void LoadApplicationBasicInfo(int ApplicationID)
        {
            _application = clsApplication.Find(ApplicationID);

            if (_application == null)
            {
                MessageBox.Show("Application Not Found");
                return;
            }

            _FillApplicationBasicInfo();

        }

        private void llbViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmPersonDetails(_application.ApplicantPersonID);
            frm.ShowDialog();
        }
    }
}
