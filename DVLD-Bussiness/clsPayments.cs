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



    public class clsPayment
    {

        enum enMode { enAddNew = 1, enUpdate = 2 }
        enMode _Mode = enMode.enAddNew;



        public int PaymentID { set; get; }
        public int VehichleLicenseID { set; get; }
        public decimal Amount { set; get; }
        public DateTime PaymentDate { set; get; }
        public byte ServiceType { set; get; }
        public int CreatedBy { set; get; }


        public clsPayment()
        {
            this.PaymentID = -1;
            this.VehichleLicenseID = -1;
            this.Amount = 0;
            this.PaymentDate = DateTime.Now;
            this.ServiceType = 0;
            this.CreatedBy = -1;
            _Mode = enMode.enAddNew;
        }




        private clsPayment(int PaymentID, int VehichleLicenseID, decimal Amount, DateTime PaymentDate, byte ServiceType, int CreatedBy)
        {
            this.PaymentID = PaymentID;
            this.VehichleLicenseID = VehichleLicenseID;
            this.Amount = Amount;
            this.PaymentDate = PaymentDate;
            this.ServiceType = ServiceType;
            this.CreatedBy = CreatedBy;
            _Mode = enMode.enUpdate;
        }



        private bool _AddPayment()
        {
            PaymentID = clsPaymentData.AddPayment( VehichleLicenseID, Amount, PaymentDate, ServiceType, CreatedBy);

            return (PaymentID != -1);
        }


        private bool _UpdatePayment()
        {
            return clsPaymentData.UpdatePaymentByID(PaymentID, VehichleLicenseID, Amount, PaymentDate, ServiceType, CreatedBy);
        }


        public static clsPayment FindPaymentByID(int PaymentID)
        {

            int VehichleLicenseID = -1;
            decimal Amount = 0;
            DateTime PaymentDate = DateTime.Now;
            byte ServiceType = 0;
            int CreatedBy = -1;
            if (clsPaymentData.GetPaymentByID(PaymentID, ref VehichleLicenseID, ref Amount, ref PaymentDate, ref ServiceType, ref CreatedBy))
            {
                return new clsPayment(PaymentID, VehichleLicenseID, Amount, PaymentDate, ServiceType, CreatedBy);
            }
            else
            {
                return null;
            }
        }


        public static bool IsExistByPaymentID(int PaymentID)
        {
            return clsPaymentData.IsPaymentExistByPaymentID(PaymentID);
        }


        public static bool DeletePayment(int PaymentID)
        {
            return clsPaymentData.DeletePaymentByID(PaymentID);
        }


        public static DataTable GetPaymentsList()
        {
            return clsPaymentData.GetAllPayments();
        }



        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.enAddNew:
                    if (_AddPayment())
                    {
                        _Mode = enMode.enUpdate;
                        return true;
                    }
                    else
                        return false;

                case enMode.enUpdate:
                    if (_UpdatePayment())
                        return true;
                    else
                        return false;
            }
            return false;
        }


    }
}
