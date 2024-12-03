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



    public class clsMake
    {

        enum enMode { enAddNew = 1, enUpdate = 2 }
        enMode _Mode = enMode.enAddNew;



        public int MakeID { set; get; }
        public string Make { set; get; }


        public clsMake()
        {
            this.MakeID = -1;
            this.Make = "";
            _Mode = enMode.enAddNew;
        }




        private clsMake(int MakeID, string Make)
        {
            this.MakeID = MakeID;
            this.Make = Make;
            _Mode = enMode.enUpdate;
        }



        private bool _AddMake()
        {
            MakeID = clsMakeData.AddMake(Make);

            return (MakeID != -1);
        }


        private bool _UpdateMake()
        {
            return clsMakeData.UpdateMakeByID(MakeID, Make);
        }


        public static clsMake FindMakeByID(int MakeID)
        {
            string Make = default;
            if (clsMakeData.GetMakeByID( MakeID, ref Make))
            {
                return new clsMake(MakeID, Make);
            }
            else
            {
                return null;
            }
        }


        public static bool IsExistByMakeID(int MakeID)
        {
            return clsMakeData.IsMakeExistByMakeID(MakeID);
        }


        public static bool DeleteMake(int MakeID)
        {
            return clsMakeData.DeleteMakeByID(MakeID);
        }


        public static DataTable GetMakesList()
        {
            return clsMakeData.GetAllMakes();
        }



        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.enAddNew:
                    if (_AddMake())
                    {
                        _Mode = enMode.enUpdate;
                        return true;
                    }
                    else
                        return false;

                case enMode.enUpdate:
                    if (_UpdateMake())
                        return true;
                    else
                        return false;
            }
            return false;
        }


    }
}
