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



    public class clsSubModel
    {

        enum enMode { enAddNew = 1, enUpdate = 2 }
        enMode _Mode = enMode.enAddNew;



        public int SubModelID { set; get; }
        public int ModelID { set; get; }
        public string SubModelName { set; get; }


        public clsSubModel()
        {
            this.SubModelID = -1;
            this.ModelID = -1;
            this.SubModelName = "";
            _Mode = enMode.enAddNew;
        }




        private clsSubModel(int SubModelID, int ModelID, string SubModelName)
        {
            this.SubModelID = SubModelID;
            this.ModelID = ModelID;
            this.SubModelName = SubModelName;
            _Mode = enMode.enUpdate;
        }



        private bool _AddSubModel()
        {
            SubModelID = clsSubModelData.AddSubModel( ModelID, SubModelName);

            return (SubModelID != -1);
        }


        private bool _UpdateSubModel()
        {
            return clsSubModelData.UpdateSubModelByID(SubModelID, ModelID, SubModelName);
        }


        public static clsSubModel FindSubModelByID(int SubModelID)
        {
            int ModelID = default;
            string SubModelName = default;
            if (clsSubModelData.GetSubModelByID(SubModelID, ref ModelID, ref SubModelName))
            {
                return new clsSubModel(SubModelID, ModelID, SubModelName);
            }
            else
            {
                return null;
            }
        }

        public static clsSubModel FindSubModelByName(string SubModelName)
        {
            int ModelID = -1;
            int SubModelID = -1;
            if (clsSubModelData.GetSubModelByName(SubModelName,ref SubModelID, ref ModelID ))
            {
                return new clsSubModel(SubModelID, ModelID, SubModelName);
            }
            else
            {
                return null;
            }
        }

        public static bool IsExistBySubModelID(int SubModelID)
        {
            return clsSubModelData.IsSubModelExistBySubModelID(SubModelID);
        }


        public static bool DeleteSubModel(int SubModelID)
        {
            return clsSubModelData.DeleteSubModelByID(SubModelID);
        }


        public static DataTable GetSubModelsList()
        {
            return clsSubModelData.GetAllSubModels();
        }

        public static DataTable GetSubModelsByModelName(string ModelName)
        {
            return clsSubModelData.GetAllSubModelsByModelName(ModelName);
        }


        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.enAddNew:
                    if (_AddSubModel())
                    {
                        _Mode = enMode.enUpdate;
                        return true;
                    }
                    else
                        return false;

                case enMode.enUpdate:
                    if (_UpdateSubModel())
                        return true;
                    else
                        return false;
            }
            return false;
        }


    }
}
