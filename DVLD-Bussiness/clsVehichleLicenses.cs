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



    public class clsVehichleLicense
    {

        enum enMode { enAddNew = 1, enUpdate = 2 }
        enMode _Mode = enMode.enAddNew;



        public int VehichleLicenseID { set; get; }
        public int VehichleID { set; get; }
        public DateTime IssuedDate { set; get; }
        public DateTime ExpiryDate { set; get; }
        public byte Status { set; get; }
        public int CreatedBy { set; get; }


        public clsVehichleLicense()
        {
            this.VehichleLicenseID = -1;
            this.VehichleID = -1;
            this.IssuedDate = DateTime.Now;
            this.ExpiryDate = DateTime.MaxValue;
            this.Status = 0;
            this.CreatedBy = -1;
            _Mode = enMode.enAddNew;
        }




        private clsVehichleLicense(int VehichleLicenseID, int VehichleID, DateTime IssuedDate, DateTime ExpiryDate, byte Status, int CreatedBy)
        {
            this.VehichleLicenseID = VehichleLicenseID;
            this.VehichleID = VehichleID;
            this.IssuedDate = IssuedDate;
            this.ExpiryDate = ExpiryDate;
            this.Status = Status;
            this.CreatedBy = CreatedBy;
            _Mode = enMode.enUpdate;
        }



        private bool _AddVehichleLicense()
        {
            VehichleLicenseID = clsVehichleLicenseData.AddVehichleLicense( VehichleID, IssuedDate, ExpiryDate, Status, CreatedBy);

            return (VehichleLicenseID != -1);
        }


        private bool _UpdateVehichleLicense()
        {
            return clsVehichleLicenseData.UpdateVehichleLicenseByID(VehichleLicenseID, VehichleID, IssuedDate, ExpiryDate, Status, CreatedBy);
        }


        public static clsVehichleLicense FindVehichleLicenseByID(int VehichleLicenseID)
        {

            int VehichleID = -1;
            DateTime IssuedDate = DateTime.Now;
            DateTime ExpiryDate = DateTime.Now;
            byte Status = 0;
            int CreatedBy = -1;
            if (clsVehichleLicenseData.GetVehichleLicenseByID(VehichleLicenseID, ref VehichleID, ref IssuedDate, ref ExpiryDate, ref Status, ref CreatedBy))
            {
                return new clsVehichleLicense(VehichleLicenseID, VehichleID, IssuedDate, ExpiryDate, Status, CreatedBy);
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
