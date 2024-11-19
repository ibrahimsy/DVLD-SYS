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

namespace DVLD_System.Applications
{
    public partial class frmLocalDrivingLicenseApplications : Form
    {
        DataTable _LocalLicenseApplications;
        public frmLocalDrivingLicenseApplications()
        {
            InitializeComponent();

            _LocalLicenseApplications = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();

            dgvLocalDrivingLicenseApplicatios.DataSource = _LocalLicenseApplications;
        }

        void _FillComboBoxWithColumnsNames()
        {
            foreach (DataColumn column in _LocalLicenseApplications.Columns)
            {
                column.ColumnName = _ColumnTextFormat(column.ColumnName);
                cbFilterBy.Items.Add(column.ColumnName);
            }
            /* 
             * LDLAPPID
             * LicenceClassName
             * National No
             * FullName
             * ApplicationDate
             * PassedTest
             * Status
            */
        }

        string _ColumnTextFormat(string ColumnName)
        {
            switch (ColumnName)
            {
                case "LocalDrivingLicenseApplicationID":
                    return "L.D.L.APPID";
                case "ClassName":
                    return "Class Name";
                case "NationalNo":
                    return "National No";
                case "FullName":
                    return "Full Name";
                case "ApplicationDate":
                    return "Application Date";
                case "PassedTestCount":
                    return "Passed Test Count";
                case "Status":
                    return "App.Status";
                default:
                    return string.Empty;
            }
        }

        void _LoadDataInfo()
        {
            _FillComboBoxWithColumnsNames();

            cbFilterBy.SelectedIndex = 0;
        }

        private void frmLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            _LoadDataInfo();
        }
    }
}
