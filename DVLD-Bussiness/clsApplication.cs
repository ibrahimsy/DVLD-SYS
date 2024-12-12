using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Bussiness
{
    public class clsApplication
    {

        public enum Mode { enAddNew = 1, enUpdate = 2 }
        public Mode mode;

        public enum enApplicationStatus { enNew = 1, enCanceled = 2, enCompleted = 3 };

        public enum enApplicationTypes { enNewLocalDrivingLicense = 1, enRenewDrivingLicense = 2, enReplacementForLostDrivingLicense = 3,
                                        enReplacementForDamagedDrivingLicense = 4, enReleaseDetainedDrivingLicsense = 5, enNewInternationalLicense = 6,
                                        enRetakeTest = 7,IssueVehicleLicense = 8
        }

        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }

        public clsPerson PersonInfo;

        public string FullName
        {
            get
            {
                return clsPerson.Find(ApplicantPersonID).FullName;
            }
        }
        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID { get; set; }

        public clsApplicationTypes ApplicationTypeInfo;
        public byte ApplicationStatus { get; set; }
        public DateTime LastStatusDate { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }

        public clsUser UserInfo;


        public clsApplication()
        {
            mode = Mode.enAddNew;

            ApplicationID = -1;
            ApplicantPersonID = -1;
            ApplicationDate = DateTime.Now;
            ApplicationTypeID = -1;
            ApplicationStatus = 0;
            LastStatusDate = DateTime.Now;
            PaidFees = 0;
            CreatedByUserID = -1;
        }

        private clsApplication(int ApplicationID,int ApplicantPersonID, DateTime ApplicationDate,int ApplicationTypeID,
                               byte ApplicationStatus, DateTime LastStatusDate, float PaidFees,int CreatedByUserID)
        {
            mode = Mode.enUpdate;

            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.PersonInfo = clsPerson.Find(ApplicantPersonID);
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeInfo = clsApplicationTypes.Find(ApplicationTypeID);
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.UserInfo = clsUser.Find(CreatedByUserID);


        }

        private bool _AddNewApplication()
        {
            this.ApplicationID = clsApplicationData.AddNewApplication(ApplicantPersonID, ApplicationDate, ApplicationTypeID,
                                                         ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);
            return ApplicationID != -1;
        }

        private bool _UpdateApplication()
        {
            return clsApplicationData.UpdateApplication(ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID,
                                                         ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);
        }

        public static clsApplication Find(int ApplicationID)
        {

            int ApplicantPersonID = -1;
            DateTime ApplicationDate = DateTime.Now;
            int ApplicationTypeID = -1;
            byte ApplicationStatus = 0;
            DateTime LastStatusDate = DateTime.Now;
            float PaidFees = 0.0f;
            int CreatedByUserID = -1;

            if (clsApplicationData.GetApplicationInfoByID( ApplicationID, ref ApplicantPersonID, ref  ApplicationDate, ref  ApplicationTypeID,
                                               ref  ApplicationStatus, ref  LastStatusDate, ref  PaidFees, ref  CreatedByUserID))
            {
                return new clsApplication(ApplicationID,ApplicantPersonID,ApplicationDate, ApplicationTypeID,
                                               ApplicationStatus, LastStatusDate,PaidFees, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

        public static bool IsExist(int ApplicationID)
        {
            return clsApplicationData.IsApplicationExist(ApplicationID);
        }

        public bool Delete()
        {
            return clsApplicationData.DeleteApplication(this.ApplicationID);
        }

        public bool Cancel()
        {
            return clsApplicationData.CancelApplication(this.ApplicationID);
        }

        public bool SetComplete()
        {
            return clsApplicationData.UpdateStatus(this.ApplicationID,3);
        }

        public static DataTable GetApplicationList()
        {
            return clsApplicationData.GetAllApplications();
        }

        public bool Save()
        {
            switch (mode)
            {
                case Mode.enAddNew:
                    if (_AddNewApplication())
                    {
                        mode = Mode.enUpdate;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case Mode.enUpdate:
                    return _UpdateApplication();

                default: return false;
            }
        }
    }


}
