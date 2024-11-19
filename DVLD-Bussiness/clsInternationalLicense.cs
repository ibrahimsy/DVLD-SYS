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
       int InternationalLicenseID
      ,int ApplicationID
      ,int DriverID
      ,int IssuedUsingLocalLicenseID
      ,DateTimeIssueDate
      ,DateTime ExpirationDate
      ,byte IsActive
      ,int CreatedByUserID
     */
    public class clsInternationalLicense
    {
        enum Mode { enAddNew = 1, enUpdate = 2 }

        Mode _Mode;

      public int InternationalLicenseID { set; get; }
      public int ApplicationID { set; get; }

      public clsApplication ApplicationInfo;
      public int DriverID { set; get; }

      public clsDriver DriverInfo;
      public int IssuedUsingLocalLicenseID { set; get; }

      public clsLocalDrivingLicenseApplication LocalDrivingLicenseApplicationInfo;
      public DateTime IssueDate{ set; get; }
      public  DateTime ExpirationDate { set; get; }
      public bool IsActive { set; get; }
      public int CreatedByUserID { set; get; }

       public clsUser UserInfo;

    public clsInternationalLicense()
        {
            _Mode = Mode.enAddNew;

            this.InternationalLicenseID = -1;

            this.ApplicationID = -1;
            
            this.DriverID = -1;
            this.IssuedUsingLocalLicenseID = -1;
           // this.LocalDrivingLicenseApplicationInfo = clsLocalDrivingLicenseApplication.Find(this.IssuedUsingLocalLicenseID);
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.IsActive = true;
            this.CreatedByUserID = -1;
            


        }

        private clsInternationalLicense(int InternationalLicenseID
                                      , int ApplicationID
                                      , int DriverID
                                      , int IssuedUsingLocalLicenseID
                                      , DateTime IssueDate
                                      , DateTime ExpirationDate
                                      , bool IsActive
                                      , int CreatedByUserID)
        {
            _Mode = Mode.enUpdate;

            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.ApplicationInfo = clsApplication.Find(this.ApplicationID);
            this.DriverID = DriverID;
            this.DriverInfo = clsDriver.FindByDriverID(this.DriverID);
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;
            this.UserInfo = clsUser.Find(this.CreatedByUserID);

        }

        private bool _AddNewInternationalLicense()
        {
            this.InternationalLicenseID = clsInternationalLicenseData.AddNewInternationalLicense(ApplicationID
                                                                                                  ,DriverID
                                                                                                  ,IssuedUsingLocalLicenseID
                                                                                                  ,IssueDate
                                                                                                  ,ExpirationDate
                                                                                                  ,IsActive
                                                                                                  ,CreatedByUserID);

            return InternationalLicenseID != -1;
        }

        private bool _UpdateInternationalLicense()
        {
            return clsInternationalLicenseData.UpdateInternationalLicense(InternationalLicenseID
                                                                          ,ApplicationID
                                                                          ,DriverID
                                                                          ,IssuedUsingLocalLicenseID
                                                                          ,IssueDate
                                                                          ,ExpirationDate
                                                                          ,IsActive
                                                                          ,CreatedByUserID);
        }

        public static clsInternationalLicense Find(int InternationalLicenseID)
        {

            int ApplicationID = -1;
            int DriverID = -1;
            int IssuedUsingLocalLicenseID = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            bool IsActive = true;
            int CreatedByUserID = -1;


            if (clsInternationalLicenseData.GetInternationalLicenseInfoByID(InternationalLicenseID
                                                                          ,ref ApplicationID
                                                                          ,ref DriverID
                                                                          ,ref IssuedUsingLocalLicenseID
                                                                          ,ref IssueDate
                                                                          ,ref ExpirationDate
                                                                          ,ref IsActive
                                                                          ,ref CreatedByUserID))
            {
                return new clsInternationalLicense(InternationalLicenseID
                                                 , ApplicationID
                                                 , DriverID
                                                 , IssuedUsingLocalLicenseID
                                                 , IssueDate
                                                 , ExpirationDate
                                                 , IsActive
                                                 , CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

        public static bool IsExist(int InternationalLicenseID)
        {
            return clsInternationalLicenseData.IsInternationalLicenseExist(InternationalLicenseID);
        }

        public static bool IsExistByDriverID(int DriverID)
        {
            return clsInternationalLicenseData.IsExistByDriverID(DriverID);
        }

        public static bool Delete(int InternationalLicenseID)
        {
            return clsInternationalLicenseData.DeleteInternationalLicense(InternationalLicenseID);
        }

        public static DataTable GetAllInternationalLicensees()
        {
            return clsInternationalLicenseData.GetAllInternationalLicenses();
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case Mode.enAddNew:
                    if (_AddNewInternationalLicense())
                    {
                        _Mode = Mode.enUpdate;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case Mode.enUpdate:
                    return _UpdateInternationalLicense();

                default: return false;
            }
        }
    }
}
