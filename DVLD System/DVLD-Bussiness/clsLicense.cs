using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }

        public clsApplication ApplicationInfo;
        public int DriverID { get; set; }

        //public clsDriver DriverInfo;

        public int LicenseClass { get; set; }

        public clsLicenseClass LicenseClassInfo;
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public float PaidFees { get; set; }
        public byte IsActive { get; set; }
        public int IssueReason { get; set; }
        public int CreatedByUserID { get; set; }

        public clsUser UserInfo;

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
              this.IsActive = 0;
              this.IssueReason = -1;
              this.CreatedByUserID = -1;
             
        }

        private clsLicense( int LicenseID ,int ApplicationID,int DriverID,int LicenseClass,DateTime IssueDate,DateTime ExpirationDate
                           ,string Notes,float PaidFees,byte IsActive,int IssueReason,int CreatedByUserID)
        {
            _Mode = Mode.enUpdate;

            this.LicenseID = LicenseID;
            this.LicenseClassInfo = clsLicenseClass.Find(this.LicenseID);
            this.ApplicationID = ApplicationID;
            this.ApplicationInfo = clsApplication.Find(this.ApplicationID);
            this.DriverID = DriverID;
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
                                               ,IssueReason
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
                                               , IssueReason
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
            byte IsActive = 0;
            int IssueReason = -1;
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
                               PaidFees, IsActive, IssueReason, CreatedByUserID);
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

        public static DataTable GetAllLicensees()
        {
            return clsLicenseData.GetAllLicenses();
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
