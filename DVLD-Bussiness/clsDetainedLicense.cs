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
       int DetainID
      ,int LicenseID
      , DateTime DetainDate
      ,float FineFees
      ,int CreatedByUserID
      ,byte IsReleased
      , DateTime ReleaseDate
      ,int ReleasedByUserID
      ,int ReleaseApplicationID
     */
    public class clsDetainedLicense
    {
        enum Mode { enAddNew = 1, enUpdate = 2 }

        Mode _Mode;

        public int DetainID { get; set; }
        public int LicenseID { get; set; }
      //  public clsLicense LicenseInfo;
        public  DateTime DetainDate { get; set; }
        public float FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser CreatedUserInfo;
        public bool IsReleased { get; set; }
        public  DateTime? ReleaseDate { get; set; }
        public int ReleasedByUserID { get; set; }
        public clsUser ReleasedUserInfo;
        public int ReleaseApplicationID { get; set; }
        public clsApplication ReleaseApplicationInfo;

        public clsDetainedLicense()
        {
            _Mode = Mode.enAddNew;

            this.DetainID = -1;
            this.LicenseID = -1;
            this.DetainDate = DateTime.Now;
            this.FineFees = 0;
            this.CreatedByUserID = -1;
            this.IsReleased = false;
            this.ReleaseDate = null;
            this.ReleasedByUserID = -1;
            this.ReleaseApplicationID = -1;
        }

        private clsDetainedLicense(int DetainID
                                  , int LicenseID
                                  , DateTime DetainDate
                                  , float FineFees
                                  , int CreatedByUserID
                                  , bool IsReleased
                                  , DateTime? ReleaseDate
                                  , int ReleasedByUserID
                                  , int ReleaseApplicationID)
        {
            _Mode = Mode.enUpdate;

            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
           // this.LicenseInfo = clsLicense.Find(this.LicenseID);
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedUserInfo = clsUser.Find(this.CreatedByUserID);
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleasedUserInfo = clsUser.Find(this.ReleasedByUserID);
            this.ReleaseApplicationID = ReleaseApplicationID;
            this.ReleaseApplicationInfo = clsApplication.Find(this.ReleaseApplicationID);

        }

        private bool _AddNewDetainedLicense()
        {
            this.DetainID = clsDetainedLicenseData.AddNewDetainedLicense(LicenseID
                                                                          ,DetainDate
                                                                          ,FineFees
                                                                          ,CreatedByUserID
                                                                          ,IsReleased
                                                                          ,ReleaseDate
                                                                          ,ReleasedByUserID
                                                                          ,ReleaseApplicationID);

            return DetainID != -1;
        }

        private bool _UpdateDetainedLicense()
        {
            return clsDetainedLicenseData.UpdateDetainedLicense(DetainID
                                                               , LicenseID
                                                               , DetainDate
                                                               , FineFees
                                                               , CreatedByUserID
                                                               , IsReleased
                                                               , ReleaseDate
                                                               , ReleasedByUserID
                                                               , ReleaseApplicationID);
        }

        public static clsDetainedLicense Find(int DetainID)
        {

              int LicenseID = -1;
              DateTime DetainDate  = DateTime.Now;
              float FineFees = 0;
              int CreatedByUserID = -1;
              bool IsReleased = false;
              DateTime? ReleaseDate = null;
              int ReleasedByUserID = -1;
              int ReleaseApplicationID = -1;

            if (clsDetainedLicenseData.GetDetainedLicenseInfoByID(DetainID
                                                               ,ref LicenseID
                                                               ,ref DetainDate
                                                               ,ref FineFees
                                                               ,ref CreatedByUserID
                                                               ,ref IsReleased
                                                               ,ref ReleaseDate
                                                               ,ref ReleasedByUserID
                                                               ,ref ReleaseApplicationID))
            {
                return new clsDetainedLicense(DetainID
                                              , LicenseID
                                              , DetainDate
                                              , FineFees
                                              , CreatedByUserID
                                              , IsReleased
                                              , ReleaseDate
                                              , ReleasedByUserID
                                              , ReleaseApplicationID);
            }
            else
            {
                return null;
            }
        }


        public static clsDetainedLicense FindByLicenseID(int LicenseID)
        {

            int DetainID = -1;
            DateTime DetainDate = DateTime.Now;
            float FineFees = 0;
            int CreatedByUserID = -1;
            bool IsReleased = false;
            DateTime? ReleaseDate = null;
            int ReleasedByUserID = -1;
            int ReleaseApplicationID = -1;

            if (clsDetainedLicenseData.GetDetainedLicenseInfoByLicenseID(ref DetainID
                                                                       , LicenseID
                                                                       , ref DetainDate
                                                                       , ref FineFees
                                                                       , ref CreatedByUserID
                                                                       , ref IsReleased
                                                                       , ref ReleaseDate
                                                                       , ref ReleasedByUserID
                                                                       , ref ReleaseApplicationID))
            {
                return new clsDetainedLicense(DetainID
                                              , LicenseID
                                              , DetainDate
                                              , FineFees
                                              , CreatedByUserID
                                              , IsReleased
                                              , ReleaseDate
                                              , ReleasedByUserID
                                              , ReleaseApplicationID);
            }
            else
            {
                return null;
            }
        }

        public static bool IsLicenseDetained(int LicenseID)
        {
            return clsDetainedLicenseData.IsLicenseDetained(LicenseID);
        }

        public static bool Delete(int DetainID)
        {
            return clsDetainedLicenseData.DeleteDetainedLicense(DetainID);
        }

        public static DataTable GetAllDetainedLicenses()
        {
            return clsDetainedLicenseData.GetAllDetainedLicenses();
        }

        public  bool ReleaseDetainedLicense(int ReleaseApplicationID, int ReleasedByUserID)
        {
            return clsDetainedLicenseData.ReleaseDetainedLices(this.DetainID, ReleaseApplicationID, ReleasedByUserID);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case Mode.enAddNew:
                    if (_AddNewDetainedLicense())
                    {
                        _Mode = Mode.enUpdate;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case Mode.enUpdate:
                    return _UpdateDetainedLicense();

                default: return false;
            }
        }
    }
}
