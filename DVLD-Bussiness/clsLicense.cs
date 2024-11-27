using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVLD_Bussiness.clsLicense;

namespace DVLD_Bussiness
{
    /*
      int LicenseID
      int ApplicationID
      int DriverID
      int LicenseClass
      DateTime IssueDate
      DateTime ExpirationDate
      string Notes
      float PaidFees
      byte IsActive
      int IssueReason
      int CreatedByUserID
     */
    public class clsLicense
    {
        enum Mode { enAddNew = 1, enUpdate = 2 }

        Mode _Mode;

        public enum enIssueReson { enFirstTime = 1, enRenew = 2, enDamageReplacment = 3, enLostReplacement = 4 };
        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }

        public clsApplication ApplicationInfo;
        public int DriverID { get; set; }

        public clsDriver DriverInfo;

        public int LicenseClass { get; set; }

        public clsLicenseClass LicenseClassInfo;
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public float PaidFees { get; set; }
        public bool IsActive { get; set; }
        public enIssueReson IssueReason { get; set; }
        public int CreatedByUserID { get; set; }

        public bool IsDetained
        {
            get
            {
                return IsLicenseDetained();
            }
        }

        public clsUser UserInfo;

        public string IssueReasonText
        {
            get
            {
                return GetIssueReasonText(this.IssueReason);
            }
        }

        public clsLicense()
        {
            _Mode = Mode.enAddNew;

              this.LicenseID = -1;
              this.ApplicationID = -1;
              this.DriverID = -1;
              this.LicenseClass = -1;
              this.IssueDate = DateTime.Now;
              this.ExpirationDate = DateTime.Now;
              this.Notes = "";
              this.PaidFees = 0;
              this.IsActive = true;
              this.IssueReason = enIssueReson.enFirstTime;
              this.CreatedByUserID = -1;
             
        }

        private clsLicense( int LicenseID ,int ApplicationID,int DriverID,int LicenseClass,DateTime IssueDate,DateTime ExpirationDate
                           ,string Notes,float PaidFees,bool IsActive,enIssueReson IssueReason,int CreatedByUserID)
        {
            _Mode = Mode.enUpdate;

            this.LicenseID = LicenseID;
            this.LicenseClassInfo = clsLicenseClass.Find(this.LicenseID);
            this.ApplicationID = ApplicationID;
            this.ApplicationInfo = clsApplication.Find(this.ApplicationID);
            this.DriverID = DriverID;
            this.DriverInfo = clsDriver.FindByDriverID(this.DriverID);
            this.LicenseClass = LicenseClass;
            this.LicenseClassInfo = clsLicenseClass.Find(this.LicenseClass);
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;
            this.UserInfo = clsUser.Find(CreatedByUserID);
        }

        private bool _AddNewLicense()
        {
            this.LicenseID = clsLicenseData.AddNewLicense(
                                                ApplicationID
                                               ,DriverID
                                               ,LicenseClass
                                               ,IssueDate
                                               ,ExpirationDate
                                               ,Notes
                                               ,PaidFees
                                               ,IsActive
                                               ,(byte)IssueReason
                                               ,CreatedByUserID);

            return LicenseID != -1;
        }

        private bool _UpdateLicense()
        {
            return clsLicenseData.UpdateLicense(LicenseID
                                               , ApplicationID
                                               , DriverID
                                               , LicenseClass
                                               , IssueDate
                                               , ExpirationDate
                                               , Notes
                                               , PaidFees
                                               , IsActive
                                               ,(byte) IssueReason
                                               , CreatedByUserID);
        }

        public static clsLicense Find(int LicenseID)
        {
            int ApplicationID = -1;
            int DriverID = -1;
            int LicenseClass = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            string Notes = "";
            float PaidFees = 0;
            bool IsActive = true;
            byte IssueReason = 1;
            int CreatedByUserID = -1;

            if (clsLicenseData.GetLicenseInfoByID(LicenseID
                                                   , ref ApplicationID
                                                   , ref DriverID
                                                   , ref LicenseClass
                                                   , ref IssueDate
                                                   , ref ExpirationDate
                                                   , ref Notes
                                                   , ref PaidFees
                                                   , ref IsActive
                                                   , ref IssueReason
                                                   , ref CreatedByUserID))
            {
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes,
                               PaidFees, IsActive, (enIssueReson)IssueReason, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

        public static bool IsExist(int LicenseID)
        {
            return clsLicenseData.IsLicenseExist(LicenseID);
        }

        public static bool Delete(int LicenseID)
        {
            return clsLicenseData.DeleteLicense(LicenseID);
        }

        public bool IsLicenseDetained()
        {
            return clsDetainedLicense.IsLicenseDetained(this.LicenseID);
        }

        public static DataTable GetAllLicensees()
        {
            return clsLicenseData.GetAllLicenses();
        }

        public static DataTable GetAllLocalLicenseByDriverID(int DriverID)
        {
            return clsLicenseData.GetAllLocalLicensesByDriverID(DriverID);
        }

        public static DataTable GetAllInternationalLicenseByDriverID(int DriverID)
        {
            return clsLicenseData.GetAllInternationalLicensesByDriverID(DriverID);
        }

        public string GetIssueReasonText(enIssueReson IssueReason)
        {
            switch (IssueReason)
            {
                case enIssueReson.enFirstTime:
                    return "First Time";
                case enIssueReson.enRenew:
                    return "Renew";
                case enIssueReson.enDamageReplacment:
                    return "Damage Replacment";
                case enIssueReson.enLostReplacement:
                    return "Lost Replacment";
                default:
                    return "First Time";
            }
        }


        public static int GetActiveLicensePerPersonID(int PersonID,int LicenseClass)
        {
            return clsLicenseData.GetActiveLicensePerPersonId(PersonID, LicenseClass);
        }

        public static int GetLicenseByPersonID(int PersonID, int LicenseClass)
        {
            return clsLicenseData.GetLicenseByPersonId(PersonID, LicenseClass);
        }

        public bool DeactivateCurrentLicense()
        {
            return clsLicenseData.DeactivateLicenseByID(this.LicenseID);
        }
        public clsLicense Replace(enIssueReson IssueReson,int CreatedByUserID)
        {

            //First We Initilize A Replacment Application
            clsApplication ReplacmentApplication = new clsApplication();

            ReplacmentApplication.ApplicantPersonID = this.DriverInfo.PersonID;
            ReplacmentApplication.ApplicationDate = DateTime.Now;
            ReplacmentApplication.ApplicationStatus = (int)clsApplication.enApplicationStatus.enCompleted;

            if (IssueReson == enIssueReson.enDamageReplacment)
                ReplacmentApplication.ApplicationTypeID = (int)clsApplication.enApplicationTypes.enReplacementForDamagedDrivingLicense;
            else
                ReplacmentApplication.ApplicationTypeID = (int)clsApplication.enApplicationTypes.enReplacementForLostDrivingLicense;

            ReplacmentApplication.LastStatusDate = DateTime.Now;
            ReplacmentApplication.PaidFees = this.LicenseClassInfo.ClassFees;
            ReplacmentApplication.CreatedByUserID = CreatedByUserID;

            if (!ReplacmentApplication.Save())
                return null;

            //Create New License
            clsLicense NewLicense = new clsLicense();
            NewLicense.ApplicationID = ReplacmentApplication.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClass = this.LicenseClass;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = DateTime.Now.AddYears(clsLicenseClass.Find(this.LicenseClass).DefaultValidityLength);
            NewLicense.PaidFees = this.PaidFees;
            NewLicense.IsActive = true;    
            NewLicense.IssueReason = IssueReson;
            NewLicense.CreatedByUserID = CreatedByUserID;


            if (!NewLicense.Save())
                return null;

            DeactivateCurrentLicense();

            return NewLicense;
        }

        public clsLicense Renew(string Notes,int CreatedByUserID)
        {
            //First We Initilize A Replacment Application
            clsApplication ReplacmentApplication = new clsApplication();

            ReplacmentApplication.ApplicantPersonID = this.DriverInfo.PersonID;
            ReplacmentApplication.ApplicationDate = DateTime.Now;
            ReplacmentApplication.ApplicationStatus = (int)clsApplication.enApplicationStatus.enCompleted;
            ReplacmentApplication.ApplicationTypeID = (int)clsApplication.enApplicationTypes.enRenewDrivingLicense;
            ReplacmentApplication.LastStatusDate = DateTime.Now;
            ReplacmentApplication.PaidFees = this.LicenseClassInfo.ClassFees;
            ReplacmentApplication.CreatedByUserID = CreatedByUserID;

            if (!ReplacmentApplication.Save())
                return null;
            //Create New License
            clsLicense NewLicense = new clsLicense();
            NewLicense.ApplicationID = ReplacmentApplication.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClass = this.LicenseClass;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            NewLicense.PaidFees = this.PaidFees;
            NewLicense.IsActive = true;
            NewLicense.IssueReason = enIssueReson.enRenew;
            NewLicense.CreatedByUserID = CreatedByUserID;
            NewLicense.Notes = Notes;

            if (!NewLicense.Save())
                return null;

            DeactivateCurrentLicense();

            return NewLicense;
        }

        public int Detain(float Fees,int CreatedByUserID)
        {
            clsDetainedLicense _DetainedLicense = new clsDetainedLicense();
            _DetainedLicense.LicenseID = this.LicenseID;
            _DetainedLicense.DetainDate = DateTime.Now;
            _DetainedLicense.FineFees = Convert.ToSingle( Fees);
            _DetainedLicense.CreatedByUserID = CreatedByUserID;

            if (!_DetainedLicense.Save())
            {
                return -1;
            }

            return _DetainedLicense.DetainID;
        }
        public bool IsLicenseExpired()
        {
            return this.ExpirationDate < DateTime.Now;
        }
        public bool Save()
        {
            switch (_Mode)
            {
                case Mode.enAddNew:
                    if (_AddNewLicense())
                    {
                        _Mode = Mode.enUpdate;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case Mode.enUpdate:
                    return _UpdateLicense();

                default: return false;
            }
        }

    }
}
