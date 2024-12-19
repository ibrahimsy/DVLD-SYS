using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankDataAccess;
using DVLD_Bussiness;
using System.Runtime.CompilerServices;

namespace BankBussiness
{



    public class clsVehichleLicense:clsApplication
    {

        enum enMode { enAddNew = 1, enUpdate = 2 }
        enMode _Mode = enMode.enAddNew;

        public enum enStatus { Active = 1, InActive = 2 }
        public enum enIssueReason { ForFirstTime = 1,ForRenew = 2,ForLost = 3}
        public int VehichleLicenseID { set; get; }
        public int VehichleID { set; get; }
        public DateTime IssuedDate { set; get; }
        public DateTime ExpiryDate { set; get; }
        public Decimal LicenseFee {  set; get; }
        public byte Status { set; get; }


        public enIssueReason IssueReason { set; get; }
        public string IssueReasonText
        { get 
            {
                return GetIssueReasonText(this.IssueReason);
            }
        }
        public bool IsActive
        {
            get
            {
                return this.Status == (byte)enStatus.Active;
            }
        }

        public bool IsExpired
        {
            get
            {
                return this.ExpiryDate < DateTime.Now ;
            }
        }

        public int CreatedBy { set; get; }

        public clsUser UserInfo;
        public clsVehichleLicense()
        {
            this.VehichleLicenseID = -1;
            this.VehichleID = -1;
            this.IssuedDate = DateTime.Now;
            this.ExpiryDate = DateTime.MaxValue;
            this.LicenseFee = 0;
            this.Status = 0;
            this.CreatedBy = -1;
            this.IssueReason = enIssueReason.ForFirstTime;
            _Mode = enMode.enAddNew;
        }




        private clsVehichleLicense(int ApplicationID ,int ApplicantPersonID,DateTime ApplicationDate,int ApplicationTypeID,
           byte ApplicationStatus,DateTime LastStatusDate,float PaidFees, int VehichleLicenseID, int VehichleID, DateTime IssuedDate, DateTime ExpiryDate, Decimal LicenseFee, byte Status,enIssueReason IssueReason, int CreatedBy)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.VehichleLicenseID = VehichleLicenseID;
            this.VehichleID = VehichleID;
            this.IssuedDate = IssuedDate;
            this.ExpiryDate = ExpiryDate;
            this.LicenseFee = LicenseFee;
            this.Status = Status;
            this.IssueReason = IssueReason;
            this.CreatedBy = CreatedBy;
            this.UserInfo = clsUser.Find(CreatedBy);
            _Mode = enMode.enUpdate;
        }


        public string GetIssueReasonText(enIssueReason IssueReason)
        {
            switch (IssueReason)
            {
                case clsVehichleLicense.enIssueReason.ForFirstTime:
                    return "First Time";
                case clsVehichleLicense.enIssueReason.ForRenew:
                    return "For Renew";
                case clsVehichleLicense.enIssueReason.ForLost:
                    return "For Lost";
                default:
                    return "Unknown";
            }
        }

        private bool _AddVehichleLicense()
        {
            VehichleLicenseID = clsVehichleLicenseData.AddVehichleLicense(ApplicationID, VehichleID, IssuedDate, ExpiryDate, LicenseFee, Status,(byte)IssueReason, CreatedBy);

            return (VehichleLicenseID != -1);
        }


        private bool _UpdateVehichleLicense()
        {
            return clsVehichleLicenseData.UpdateVehichleLicenseByID(VehichleLicenseID, ApplicationID, VehichleID, IssuedDate, ExpiryDate, LicenseFee, Status,(byte)IssueReason, CreatedBy);
        }


        public static clsVehichleLicense FindByID(int VehichleLicenseID)
        {

            int VehichleID = -1;
            int ApplicationID = -1;
            DateTime IssuedDate = DateTime.Now;
            DateTime ExpiryDate = DateTime.Now;
            byte Status = 0;
            Decimal LicenseFee = 0;
            byte IssueReason = 1;
            int CreatedBy = -1;
            if (clsVehichleLicenseData.GetVehichleLicenseByID(VehichleLicenseID,ref ApplicationID, ref VehichleID, ref IssuedDate, ref ExpiryDate,ref LicenseFee, ref Status,ref IssueReason, ref CreatedBy))
            {
                clsApplication Application = clsApplication.Find(ApplicationID);

                return new clsVehichleLicense(
                    Application.ApplicationID,
                    Application.ApplicantPersonID,
                    Application.ApplicationDate,
                    Application.ApplicationTypeID,
                    Application.ApplicationStatus,
                    Application.LastStatusDate,
                    Application.PaidFees,
                    VehichleLicenseID, VehichleID, IssuedDate, ExpiryDate, LicenseFee, Status, (enIssueReason)IssueReason, CreatedBy);
            }
            else
            {
                return null;
            }
        }

        public static clsVehichleLicense FindByVehicleID(int VehichleID)
        {

            int VehichleLicenseID = -1;
            int ApplicationID = -1;
            DateTime IssuedDate = DateTime.Now;
            DateTime ExpiryDate = DateTime.Now;
            byte Status = 0;
            Decimal LicenseFee = 0;
            byte IssueReason = 1;
            int CreatedBy = -1;
            if (clsVehichleLicenseData.GetVehicleLicenseByVehicleID(VehichleID,ref VehichleLicenseID, ref ApplicationID, ref IssuedDate, ref ExpiryDate, ref LicenseFee, ref Status, ref IssueReason, ref CreatedBy))
            {
                clsApplication Application = clsApplication.Find(ApplicationID);

                return new clsVehichleLicense(
                    Application.ApplicationID,
                    Application.ApplicantPersonID,
                    Application.ApplicationDate,
                    Application.ApplicationTypeID,
                    Application.ApplicationStatus,
                    Application.LastStatusDate,
                    Application.PaidFees,
                    VehichleLicenseID, VehichleID, IssuedDate, ExpiryDate, LicenseFee, Status, (enIssueReason)IssueReason, CreatedBy);
            }
            else
            {
                return null;
            }
        }

        public static bool IsExistByVehichleLicenseID(int VehichleLicenseID)
        {
            return clsVehichleLicenseData.IsVehichleLicenseExistByVehichleLicenseID(VehichleLicenseID);
        }


        public static bool DeleteVehichleLicense(int VehichleLicenseID)
        {
            return clsVehichleLicenseData.DeleteVehichleLicenseByID(VehichleLicenseID);
        }


        public static DataTable GetVehichleLicensesList()
        {
            return clsVehichleLicenseData.GetAllVehichleLicenses();
        }

        public static int GetVehicleLicenseID(int VehicleID)
        {
            return clsVehichleLicenseData.GetVehicleLicenseIDByVehicleID(VehicleID);
        }
      
        

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.enAddNew:
                    if (_AddVehichleLicense())
                    {
                        _Mode = enMode.enUpdate;
                        return true;
                    }
                    else
                        return false;

                case enMode.enUpdate:
                    if (_UpdateVehichleLicense())
                        return true;
                    else
                        return false;
            }
            return false;
        }


    }
}
