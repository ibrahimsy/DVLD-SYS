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



    public class clsMakeModel
    {

        enum enMode { enAddNew = 1, enUpdate = 2 }
        enMode _Mode = enMode.enAddNew;



        public int ModelID { set; get; }
        public int MakeID { set; get; }
        public string ModelName { set; get; }


        public clsMakeModel()
        {
            this.ModelID = -1;
            this.MakeID = -1;
            this.ModelName = "";
            _Mode = enMode.enAddNew;
        }




        private clsMakeModel(int ModelID, int MakeID, string ModelName)
        {
            this.ModelID = ModelID;
            this.MakeID = MakeID;
            this.ModelName = ModelName;
            _Mode = enMode.enUpdate;
        }



        private bool _AddMakeModel()
        {
            ModelID = clsMakeModelData.AddMakeModel(MakeID, ModelName);

            return (ModelID != -1);
        }


        private bool _UpdateMakeModel()
        {
            return clsMakeModelData.UpdateMakeModelByID(ModelID, MakeID, ModelName);
        }


        public static clsMakeModel FindMakeModelByID(int ModelID)
        {
            int MakeID = -1;
            string ModelName = "";
            if (clsMakeModelData.GetMakeModelByID( ModelID, ref MakeID, ref ModelName))
            {
                return new clsMakeModel(ModelID, MakeID, ModelName);
            }
            else
            {
                return null;
            }
        }


        public static bool IsExistByMakeModelID(int ModelID)
        {
            return clsMakeModelData.IsMakeModelExistByModelID(ModelID);
        }


        public static bool DeleteMakeModel(int ModelID)
        {
            return clsMakeModelData.DeleteMakeModelByID(ModelID);
        }


        public static DataTable GetMakeModelsList()
        {
            return clsMakeModelData.GetAllMakeModels();
        }



        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.enAddNew:
                    if (_AddMakeModel())
                    {
                        _Mode = enMode.enUpdate;
                        return true;
                    }
                    else
                        return false;

                case enMode.enUpdate:
                    if (_UpdateMakeModel())
                        return true;
                    else
                        return false;
            }
            return false;
        }


    }
}
