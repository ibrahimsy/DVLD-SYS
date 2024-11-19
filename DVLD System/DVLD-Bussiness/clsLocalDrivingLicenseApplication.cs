using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Bussiness
{
    public  class clsLocalDrivingLicenseApplication
    {
        /*
          [LocalDrivingLicenseApplicationID]
           ,[ApplicationID]
           ,[LicenseClassID]
          */
        enum Mode { enAddNew = 1, enUpdate = 2 }

        Mode _Mode;

        public int LocalDrivingLicenseApplicationID { get; set; }
        public int ApplicationID { get; set; }

        public clsApplication ApplicationInfo;
        public int LicenseClassID { get; set; }

        public clsLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.ApplicationID = -1;
            this.LicenseClassID = -1;

            _Mode = Mode.enAddNew;
        }

        private clsLocalDrivingLicenseApplication(int LDLApplicationID, int ApplicationID, int LicenseClassID)
        {
            _Mode = Mode.enUpdate;

            this.LocalDrivingLicenseApplicationID = LDLApplicationID;
            this.ApplicationID = ApplicationID;
            this.ApplicationInfo = clsApplication.Find(this.ApplicationID);
            this.LicenseClassID = LicenseClassID;
        }

        private bool _AddNewLocalDrivingLicenseApplication()
        {

            this.LocalDrivingLicenseApplicationID = 
                        clsLocalDrivingLicenseApplicationData.AddNewLocalDrivingLicenseApplication(this.ApplicationID, this.LicenseClassID);

            return this.LocalDrivingLicenseApplicationID != -1;
        }

        private bool _UpdateLocalDrivingLicenseApplication()
        {
            return clsLocalDrivingLicenseApplicationData.UpdateLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID, this.ApplicationID, this.LicenseClassID);
        }

        public static clsLocalDrivingLicenseApplication Find(int LDLApplicationID)
        {
            int ApplicationID = -1;
            int LicenseClassID = -1;

            if (clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationIDInfoByID(LDLApplicationID, ref ApplicationID, ref LicenseClassID))
            {
                return new clsLocalDrivingLicenseApplication(LDLApplicationID, ApplicationID, LicenseClassID);
            }
            else
            {
                return null;
            }
        }

        public static bool IsExist(int ApplicantPersonID,short ApplicationStatus,int LicenseClassID)
        {
            return clsLocalDrivingLicenseApplicationData.IsLocalDrivingLicenseApplicationExist(ApplicantPersonID, ApplicationStatus, LicenseClassID);
        }
        public static bool Delete(int LDLApplicationID)
        {
            return clsLocalDrivingLicenseApplicationData.DeleteLocalDrivingLicenseApplication(LDLApplicationID);
        }

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationData.GetAllLocalDrivingLicenseApplications();
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case Mode.enAddNew:
                    if (_AddNewLocalDrivingLicenseApplication())
                    {
                        _Mode = Mode.enUpdate;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case Mode.enUpdate:
                    return _UpdateLocalDrivingLicenseApplication();

                default: return false;
            }
        }
    }
}
