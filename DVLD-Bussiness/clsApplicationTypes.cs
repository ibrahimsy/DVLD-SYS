using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Bussiness
{
    public class clsApplicationTypes
    {
        enum Mode { enAddNew = 1, enUpdate = 2 }

        Mode _Mode;

        public int TypeID { get; set; }
        public string Title { get; set; }
        public float Fees { get; set; }

        public clsApplicationTypes()
        {
            this.TypeID = -1;
            this.Title = "";
            this.Fees = 0;

            _Mode = Mode.enAddNew;
        }

        private clsApplicationTypes(int TypeID,string Title,float Fees)
        {
            _Mode = Mode.enUpdate;

            this.TypeID = TypeID;
            this.Title = Title;
            this.Fees = Fees;
        }

        private bool _AddNewType()
        {

            this.TypeID = clsApplicationTypesData.AddNewApplicationType(this.Title,this.Fees);

            return this.TypeID != -1;
        }

        private bool _UpdateType()
        {
            return clsApplicationTypesData.UpdateApplicationType(this.TypeID,this.Title,this.Fees);
        }

        public static clsApplicationTypes Find(int TypeID)
        {
            string Title = "";
            float Fees = 0;

            if (clsApplicationTypesData.GetTypeInfoByID (TypeID, ref Title, ref Fees))
            {
                return new clsApplicationTypes(TypeID, Title, Fees);
            }
            else
            {
                return null;
            }
        }

        public static bool Delete(int TypeID)
        {
            return clsApplicationTypesData.DeleteApplicationType(TypeID);
        }

        public static DataTable ApplicationTypesList()
        {
            return clsApplicationTypesData.GetAllApplicationTypes();
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case Mode.enAddNew:
                    if (_AddNewType())
                    {
                        _Mode = Mode.enUpdate;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case Mode.enUpdate:
                    return _UpdateType();

                default: return false;
            }
        }
    }
}
