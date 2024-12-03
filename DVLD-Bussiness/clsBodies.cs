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



    public class clsBody
    {

        enum enMode { enAddNew = 1, enUpdate = 2 }
        enMode _Mode = enMode.enAddNew;



        public int BodyID { set; get; }
        public string BodyName { set; get; }


        public clsBody()
        {
            this.BodyID = -1;
            this.BodyName = "";
            _Mode = enMode.enAddNew;
        }




        private clsBody(int BodyID, string BodyName)
        {
            this.BodyID = BodyID;
            this.BodyName = BodyName;
            _Mode = enMode.enUpdate;
        }



        private bool _AddBody()
        {
            BodyID = clsBodyData.AddBody(BodyName);

            return (BodyID != -1);
        }


        private bool _UpdateBody()
        {
            return clsBodyData.UpdateBodyByID(BodyID, BodyName);
        }


        public static clsBody FindBodyByID(int BodyID)
        {
            string BodyName = default;
            if (clsBodyData.GetBodyByID(BodyID, ref BodyName))
            {
                return new clsBody(BodyID, BodyName);
            }
            else
            {
                return null;
            }
        }


        public static bool IsExistByBodyID(int BodyID)
        {
            return clsBodyData.IsBodyExistByBodyID(BodyID);
        }


        public static bool DeleteBody(int BodyID)
        {
            return clsBodyData.DeleteBodyByID(BodyID);
        }


        public static DataTable GetBodiesList()
        {
            return clsBodyData.GetAllBody();
        }



        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.enAddNew:
                    if (_AddBody())
                    {
                        _Mode = enMode.enUpdate;
                        return true;
                    }
                    else
                        return false;

                case enMode.enUpdate:
                    if (_UpdateBody())
                        return true;
                    else
                        return false;
            }
            return false;
        }


    }
}
