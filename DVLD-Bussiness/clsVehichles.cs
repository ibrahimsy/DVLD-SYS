using BankDataAccess;
using DVLD_Bussiness;
using System;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace BankBussiness
{



    public class clsVehichle
    {

        enum enMode { enAddNew = 1, enUpdate = 2 }
        enMode _Mode = enMode.enAddNew;



        public int VehichleID { set; get; }
        public string ChassisNumber { set; get; }
        public string PlateNumber { set; get; }
        public int MakeID { set; get; }

        public clsMake MakeInfo;
        public int ModelID { set; get; }

        public clsMakeModel ModelInfo;
        public int SubModelID { set; get; }

        public clsSubModel SubModelInfo;
        public int BodyID { set; get; }

        public clsBody BodyInfo;
        public int OwnerID { set; get; }

        public clsPerson OwnerInfo;
        public int Year { set; get; }
        public string Color { set; get; }
        public int CreatedBy { set; get; }

        public clsUser UserInfo;

        public clsVehichle()
        {
            this.VehichleID = -1;
            this.ChassisNumber = "";
            this.PlateNumber = "";
            this.MakeID = -1;
            this.ModelID = -1;
            this.SubModelID = -1;
            this.BodyID = -1;
            this.OwnerID = -1;
            this.Year = 1900;
            this.Color = "";
            this.CreatedBy = -1;
            _Mode = enMode.enAddNew;
        }

        private clsVehichle(int VehichleID, string ChassisNumber, string PlateNumber, int MakeID, int ModelID, int SubModelID, int BodyID, int OwnerID, int Year, string Color, int CreatedBy)
        {
            this.VehichleID = VehichleID;
            this.ChassisNumber = ChassisNumber;
            this.PlateNumber = PlateNumber;
            this.MakeID = MakeID;
            this.MakeInfo = clsMake.FindMakeByID(MakeID);
            this.ModelID = ModelID;
            this.ModelInfo = clsMakeModel.FindMakeModelByID(ModelID);
            this.SubModelID = SubModelID;
            this.SubModelInfo = clsSubModel.FindSubModelByID(SubModelID);
            this.BodyID = BodyID;
            this.BodyInfo = clsBody.FindBodyByID(BodyID);
            this.OwnerID = OwnerID;
            this.OwnerInfo = clsPerson.Find(OwnerID);
            this.Year = Year;
            this.Color = Color;
            this.CreatedBy = CreatedBy;
            this.UserInfo = clsUser.Find(CreatedBy);
            _Mode = enMode.enUpdate;
        }

        private bool _AddVehichle()
        {
            VehichleID = clsVehichleData.AddVehichle(ChassisNumber, PlateNumber, MakeID, ModelID, SubModelID, BodyID, OwnerID, Year, Color, CreatedBy);

            return (VehichleID != -1);
        }

        private bool _UpdateVehichle()
        {
            return clsVehichleData.UpdateVehichleByID(VehichleID, ChassisNumber, PlateNumber, MakeID, ModelID, SubModelID, BodyID, OwnerID, Year, Color, CreatedBy);
        }

        public static clsVehichle Find(int VehichleID)
        {
            string ChassisNumber = "";
            string PlateNumber = "";
            int SubModelID = -1;
            int MakeID = -1;
            int ModelID = -1;
            int BodyID = -1;
            int OwnerID = -1;
            int Year = 1900;
            string Color = "";
            int CreatedBy = -1;
            if (clsVehichleData.GetVehichleByID(VehichleID, ref ChassisNumber, ref PlateNumber, ref MakeID, ref ModelID, ref SubModelID, ref BodyID, ref OwnerID, ref Year, ref Color, ref CreatedBy))
            {
                return new clsVehichle(VehichleID, ChassisNumber, PlateNumber, MakeID, ModelID, SubModelID, BodyID, OwnerID, Year, Color, CreatedBy);
            }
            else
            {
                return null;
            }
        }

        public static clsVehichle FindByChassisNumber(string ChassisNumber)
        {
            int VehichleID = -1;
            string PlateNumber = "";
            int SubModelID = -1;
            int MakeID = -1;
            int ModelID = -1;
            int BodyID = -1;
            int OwnerID = -1;
            int Year = 1900;
            string Color = "";
            int CreatedBy = -1;
            if (clsVehichleData.GetVehichleByChassisNumber(ChassisNumber, ref VehichleID, ref PlateNumber, ref MakeID, ref ModelID, ref SubModelID, ref BodyID, ref OwnerID, ref Year, ref Color, ref CreatedBy))
            {
                return new clsVehichle(VehichleID, ChassisNumber, PlateNumber, MakeID, ModelID, SubModelID, BodyID, OwnerID, Year, Color, CreatedBy);
            }
            else
            {
                return null;
            }
        }

        public static clsVehichle FindByPlateNumber(string PlateNumber)
        {
            int VehichleID = -1;
            string ChassisNumber = "";
            int SubModelID = -1;
            int MakeID = -1;
            int ModelID = -1;
            int BodyID = -1;
            int OwnerID = -1;
            int Year = 1900;
            string Color = "";
            int CreatedBy = -1;
            if (clsVehichleData.GetVehichleByPlateNumber(PlateNumber, ref VehichleID,ref ChassisNumber, ref MakeID, ref ModelID, ref SubModelID, ref BodyID, ref OwnerID, ref Year, ref Color, ref CreatedBy))
            {
                return new clsVehichle(VehichleID, ChassisNumber, PlateNumber, MakeID, ModelID, SubModelID, BodyID, OwnerID, Year, Color, CreatedBy);
            }
            else
            {
                return null;
            }
        }

        public static bool IsExistByVehichleID(int VehichleID)
        {
            return clsVehichleData.IsVehichleExistByVehichleID(VehichleID);
        }

        public static bool DeleteVehichle(int VehichleID)
        {
            return clsVehichleData.DeleteVehichleByID(VehichleID);
        }

        public static DataTable VehichlesList()
        {
            return clsVehichleData.GetAllVehichles();
        }

        public int IssueLicense(int CreatedByID,ref int ApplicationID)
        {
            clsApplication Application = new clsApplication();

            Application.ApplicantPersonID = this.OwnerID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = (int)clsApplication.enApplicationTypes.IssueVehicleLicense;
            Application.ApplicationStatus = (int)clsApplication.enApplicationStatus.enCompleted;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationTypes.Find((int)clsApplication.enApplicationTypes.IssueVehicleLicense).Fees;
            Application.CreatedByUserID = CreatedByID;

            if (!Application.Save())
            {
                return -1;
            }

            ApplicationID = Application.ApplicationID;
            int VehicleLicenseID = -1;

            clsVehichleLicense VehichleLicense = new clsVehichleLicense();

            VehichleLicense.ApplicationID = Application.ApplicationID;
            VehichleLicense.VehichleID = this.VehichleID;
            VehichleLicense.IssuedDate = DateTime.Now;
            VehichleLicense.ExpiryDate = DateTime.Now.AddYears(1);
            VehichleLicense.LicenseFee = clsSetting.FindSettingByID( (int)clsSetting.enSettings.VehicleFee).SettingValue;
            VehichleLicense.Status = (byte)clsVehichleLicense.enStatus.Active;
            VehichleLicense.CreatedBy = CreatedByID;

            if (VehichleLicense.Save())
                VehicleLicenseID = VehichleLicense.VehichleLicenseID;
            
            return VehicleLicenseID;
        }

        public bool HasActiveLicense()
        {
            return (clsVehichleLicense.GetVehicleLicenseID(this.VehichleID) != -1);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.enAddNew:
                    if (_AddVehichle())
                    {
                        _Mode = enMode.enUpdate;
                        return true;
                    }
                    else
                        return false;

                case enMode.enUpdate:
                    if (_UpdateVehichle())
                        return true;
                    else
                        return false;
            }
            return false;
        }

    }
}
