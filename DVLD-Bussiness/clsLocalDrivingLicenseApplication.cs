using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
namespace DVLD_Bussiness
{
    public  class clsLocalDrivingLicenseApplication:clsApplication
    {
        /*
          [LocalDrivingLicenseApplicationID]
           ,[ApplicationID]
           ,[LicenseClassID]
          */
        enum Mode { enAddNew = 1, enUpdate = 2 }

        Mode _Mode;

        public int LocalDrivingLicenseApplicationID { get; set; }

        public int LicenseClassID { get; set; }

        public clsLicenseClass LicenseClassInfo;   

        public string FullName
        {
            get { return clsPerson.Find(ApplicantPersonID).FullName; }
        }

        public clsPerson PersonInfo;
        public clsLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.LicenseClassID = -1;

            _Mode = Mode.enAddNew;
        }
        
        
        private clsLocalDrivingLicenseApplication(int LDLApplicationID, int ApplicationID,
                                int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, byte ApplicationStatus,
                                DateTime LastStatusDate, float PaidFees, int CreatedByUserID, int LicenseClassID)
        {
            _Mode = Mode.enUpdate;

            this.LocalDrivingLicenseApplicationID = LDLApplicationID;
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.PersonInfo = clsPerson.Find(ApplicantPersonID);
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.LicenseClassID = LicenseClassID;
            this.LicenseClassInfo = clsLicenseClass.Find(this.LicenseClassID);
            

        }

        private bool _AddNewLocalDrivingLicenseApplication()
        {

            this.LocalDrivingLicenseApplicationID = 
                        clsLocalDrivingLicenseApplicationData.AddNewLocalDrivingLicenseApplication(base.ApplicationID, this.LicenseClassID);

            return this.LocalDrivingLicenseApplicationID != -1;
        }

        private bool _UpdateLocalDrivingLicenseApplication()
        {
            return clsLocalDrivingLicenseApplicationData.UpdateLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID, base.ApplicationID, this.LicenseClassID);
        }

        public static clsLocalDrivingLicenseApplication FindByLocalDrivingLicenseID(int LDLApplicationID)
        {
            int ApplicationID = -1;
            int LicenseClassID = -1;

            bool IsFound = clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationIDInfoByID(LDLApplicationID, ref ApplicationID, ref LicenseClassID);
            
            if(IsFound)
            {
                clsApplication Application = clsApplication.Find(ApplicationID);

                return new clsLocalDrivingLicenseApplication(LDLApplicationID, ApplicationID, Application.ApplicantPersonID, Application.ApplicationDate, Application.ApplicationTypeID, Application.ApplicationStatus,
                              Application.LastStatusDate, Application.PaidFees, Application.CreatedByUserID, LicenseClassID);
            }else
                return null;
        }

        public static int DoesApplicationExist(int ApplicantPersonID,int LicenseClassID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesApplicationExistWithLicenseClass(ApplicantPersonID, LicenseClassID);
        }


        public static int GetActiveApplication(int PersonID,clsApplication.enApplicationStatus status,int LicenseClassID)
        {
            return clsLocalDrivingLicenseApplicationData.GetActiveApplication(PersonID, (int)status, LicenseClassID);
        }
        
        public  bool Delete()
        {
            bool IsLocalDrivingLicenseApplicationDeleted = false;
            bool IsBaseApplicationDeleted = false;

            IsLocalDrivingLicenseApplicationDeleted =  
                clsLocalDrivingLicenseApplicationData.DeleteLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID);
            if (!IsLocalDrivingLicenseApplicationDeleted)
                return false;
            IsBaseApplicationDeleted = base.Delete();

            return IsBaseApplicationDeleted;
        }
        
        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationData.GetAllLocalDrivingLicenseApplications();
        }

        public  bool IsThereActiveAppointment(clsTestType.enTestType TestType)
        {
            return clsLocalDrivingLicenseApplicationData.IsThereAnActiveAppointment(this.LocalDrivingLicenseApplicationID,(int)TestType);
        }

        public clsTest GetLastTestPerTestType(clsTestType.enTestType TestType)
        {
            return clsTest.GetLastTestPerTestType(this.LocalDrivingLicenseApplicationID,TestType);
        }

        public static int GetPassedTestCount(int LDLApplicationID)
        {
            return clsLocalDrivingLicenseApplicationData.GetPassedTestCount(LDLApplicationID);
        }

        public bool PassedAllTests()
        {
            return GetPassedTestCount(this.LocalDrivingLicenseApplicationID) == 3;
        }

        public bool IsLicenseIssued()
        {
            return (GetActiveLicenseByPersonID() != -1);
        }

        public int GetActiveLicenseByPersonID()
        {
            return clsLicense.GetActiveLicensePerPersonID(this.ApplicantPersonID, this.LicenseClassID);
        }
        public int GetLicenseByPersonID()
        {
            return clsLicense.GetLicenseByPersonID(this.ApplicantPersonID, this.LicenseClassID);
        }
        public bool DoesPassTestType(clsTestType.enTestType TestType)
        {
            return clsLocalDrivingLicenseApplicationData.DoesPassTestType(this.LocalDrivingLicenseApplicationID,(int)TestType);
        }

        public int GetTestTrialPerTestType(clsTestType.enTestType TestType)
        {
            return clsLocalDrivingLicenseApplicationData.GetTestCountPerTestType(this.LocalDrivingLicenseApplicationID, (int)TestType);
        }

        public bool DoesAttendTestType(clsTestType.enTestType TestType)
        {
            return clsLocalDrivingLicenseApplicationData.DoesAttendTestType(this.LocalDrivingLicenseApplicationID,(int)TestType);
        }

        public int IssueLicenseForFirstTime(string Notes,int CreatedByUser)
        {
            int DriverID = -1;
            clsDriver driver = clsDriver.FindByPersonID(this.ApplicantPersonID);

            if (driver == null)
            {
                driver = new clsDriver();

                driver.PersonID = this.ApplicantPersonID;
                driver.CreatedByUserID = CreatedByUser;

                if (driver.Save())
                {
                    DriverID = driver.DriverID;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                DriverID = driver.DriverID;
            }

            clsLicense newLicense = new clsLicense();

            newLicense.ApplicationID = this.ApplicationID;
            newLicense.DriverID = DriverID;
            newLicense.LicenseClass = this.LicenseClassID;
            newLicense.IssueDate = DateTime.Now;
            newLicense.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            newLicense.Notes = Notes;
            newLicense.PaidFees = this.LicenseClassInfo.ClassFees;
            newLicense.IsActive = true;
            newLicense.IssueReason = clsLicense.enIssueReson.enFirstTime;
            newLicense.CreatedByUserID = CreatedByUser;

            if (newLicense.Save())
            {
                this.SetComplete();

                return newLicense.LicenseID;
            }
            else
            {
                return -1;
            }
        }


        public bool Save()
        {
            base.mode = (clsApplication.Mode)_Mode;
            base.Save();

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
