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



    public class clsFine
    {

        enum enMode { enAddNew = 1, enUpdate = 2 }
        enMode _Mode = enMode.enAddNew;



        public int FineID { set; get; }
        public int VehichleID { set; get; }
        public int DriverID { set; get; }
        public DateTime FineDate { set; get; }
        public byte ViolationType { set; get; }
        public decimal FineAmount { set; get; }
        public DateTime DueDate { set; get; }
        public bool Status { set; get; }
        public int PaymentID { set; get; }


        public clsFine()
        {
            this.FineID = -1;
            this.VehichleID = -1;
            this.DriverID = -1;
            this.FineDate = DateTime.Now;
            this.ViolationType = 0;
            this.FineAmount = 0;
            this.DueDate = DateTime.MaxValue;
            this.Status = true; // to be return
            this.PaymentID = -1;
            _Mode = enMode.enAddNew;
        }




        private clsFine(int FineID, int VehichleID, int DriverID, DateTime FineDate, byte ViolationType, decimal FineAmount, DateTime DueDate, bool Status, int PaymentID)
        {
            this.FineID = FineID;
            this.VehichleID = VehichleID;
            this.DriverID = DriverID;
            this.FineDate = FineDate;
            this.ViolationType = ViolationType;
            this.FineAmount = FineAmount;
            this.DueDate = DueDate;
            this.Status = Status;
            this.PaymentID = PaymentID;
            _Mode = enMode.enUpdate;
        }



        private bool _AddFine()
        {
            FineID = clsFineData.AddFine( VehichleID, DriverID, FineDate, ViolationType, FineAmount, DueDate, Status, PaymentID);

            return (FineID != -1);
        }


        private bool _UpdateFine()
        {
            return clsFineData.UpdateFineByID(FineID, VehichleID, DriverID, FineDate, ViolationType, FineAmount, DueDate, Status, PaymentID);
        }


        public static clsFine FindFineByID(int FineID)
        {

            int VehichleID = -1;
            int DriverID = -1;
            DateTime FineDate = DateTime.Now;
            byte ViolationType = 0;
            decimal FineAmount = 0;
            DateTime DueDate = DateTime.MaxValue;
            bool Status = true;
            int PaymentID = -1;
            if (clsFineData.GetFineByID(FineID, ref VehichleID, ref DriverID, ref FineDate, ref ViolationType, ref FineAmount, ref DueDate, ref Status, ref PaymentID))
            {
                return new clsFine(FineID, VehichleID, DriverID, FineDate, ViolationType, FineAmount, DueDate, Status, PaymentID);
            }
            else
            {
                return null;
            }
        }


        public static bool IsExistByFineID(int FineID)
        {
            return clsFineData.IsFineExistByFineID(FineID);
        }


        public static bool DeleteFine(int FineID)
        {
            return clsFineData.DeleteFineByID(FineID);
        }


        public static DataTable GetFinesList()
        {
            return clsFineData.GetAllFines();
        }



        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.enAddNew:
                    if (_AddFine())
                    {
                        _Mode = enMode.enUpdate;
                        return true;
                    }
                    else
                        return false;

                case enMode.enUpdate:
                    if (_UpdateFine())
                        return true;
                    else
                        return false;
            }
            return false;
        }


    }
}
