using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankDataAccess;

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
        public int ModelID { set; get; }
        public int SubModelID { set; get; }
        public int BodyID { set; get; }
        public int OwnerID { set; get; }
        public int Year { set; get; }
        public string Color { set; get; }
        public int CreatedBy { set; get; }


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
            this.ModelID = ModelID;
            this.SubModelID = SubModelID;
            this.BodyID = BodyID;
            this.OwnerID = OwnerID;
            this.Year = Year;
            this.Color = Color;
            this.CreatedBy = CreatedBy;
            _Mode = enMode.enUpdate;
        }



        private bool _AddVehichle()
        {
            VehichleID = clsVehichleData.AddVehichle(ChassisNumber, PlateNumber,MakeID,ModelID, SubModelID, BodyID, OwnerID, Year, Color, CreatedBy);

            return (VehichleID != -1);
        }


        private bool _UpdateVehichle()
        {
            return clsVehichleData.UpdateVehichleByID(VehichleID, ChassisNumber, PlateNumber, MakeID, ModelID, SubModelID, BodyID, OwnerID, Year, Color, CreatedBy);
        }


        public static clsVehichle FindVehichleByID(int VehichleID)
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
            if (clsVehichleData.GetVehichleByID(VehichleID, ref ChassisNumber, ref PlateNumber,ref MakeID,ref ModelID, ref SubModelID, ref BodyID, ref OwnerID, ref Year, ref Color, ref CreatedBy))
            {
                return new clsVehichle(VehichleID, ChassisNumber, PlateNumber,MakeID,ModelID, SubModelID, BodyID, OwnerID, Year, Color, CreatedBy);
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