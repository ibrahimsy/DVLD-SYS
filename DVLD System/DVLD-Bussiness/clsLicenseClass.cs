using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Bussiness
{
    public class clsLicenseClass
    {
        /*
     LicenseClassID
     ClassName
     ClassDescription
     MinimumAllowedAge
     DefaultValidityLength
     ClassFees
     From LicenseClasses
     */
        enum Mode { enAddNew = 1, enUpdate = 2 }

        Mode _Mode;

        public int LicenseClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public short MinimumAllowedAge { get; set; }
        public short DefaultValidityLength { get; set;}
        public float ClassFees { get; set; }

        public clsLicenseClass()
        {
            _Mode = Mode.enAddNew;

            this.LicenseClassID = -1;
            this.ClassName = "";
            this.ClassDescription = "";
            this.MinimumAllowedAge = 0;
            this.DefaultValidityLength = 0;
            this.ClassFees = 0;
        }

        private clsLicenseClass(int LicenseClassID,string ClassName, string ClassDescription,
                                short MinimumAllowedAge,short DefaultValidityLength,float ClassFees)
        {
            _Mode = Mode.enUpdate;

            this.LicenseClassID = LicenseClassID;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;
        }

        private bool _AddNewLicesnseClass()
        {
            this.LicenseClassID = clsLicenseClassData.AddNewLicenseClass( ClassName,  ClassDescription,
                                 MinimumAllowedAge,  DefaultValidityLength,  ClassFees);

            return LicenseClassID != -1;
        }

        private bool _UpdateLicesnseClass()
        {
            return clsLicenseClassData.UpdateLicenseClass(LicenseClassID, ClassName, ClassDescription,
                                 MinimumAllowedAge, DefaultValidityLength, ClassFees);
        }

        public static clsLicenseClass Find(int LicenseClassID)
        {
            string ClassName = "";
            string ClassDescription = "";
            short MinimumAllowedAge = 0;
            short DefaultValidityLength = 0;
            float ClassFees = 0;

            if (clsLicenseClassData.GetLicenseClassInfoByID(LicenseClassID, ref ClassName, ref ClassDescription, ref MinimumAllowedAge,
                ref DefaultValidityLength,ref ClassFees))
            {
                return new clsLicenseClass(LicenseClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
            }
            else
            {
                return null;
            }
        }

        public static clsLicenseClass Find(string LicenseClassName)
        {
            int LicenseClassID = -1;
            string ClassDescription = "";
            short MinimumAllowedAge = 0;
            short DefaultValidityLength = 0;
            float ClassFees = 0;

            if (clsLicenseClassData.GetLicenseClassInfoByClassName(ref LicenseClassID, LicenseClassName, ref ClassDescription, ref MinimumAllowedAge,
                ref DefaultValidityLength, ref ClassFees))
            {
                return new clsLicenseClass(LicenseClassID, LicenseClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
            }
            else
            {
                return null;
            }
        }
       
        public static bool IsExist(int LicenseClassID)
        {
            return clsLicenseClassData.IsLicenseClassExist(LicenseClassID);
        }

        public static bool Delete(int LicenseClassID)
        {
            return clsLicenseClassData.DeleteLicenseClass(LicenseClassID);
        }

        public static DataTable GetAllLicenseClasses()
        {
            return clsLicenseClassData.GetAllLicenseClasses();
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case Mode.enAddNew:
                    if (_AddNewLicesnseClass())
                    {
                        _Mode = Mode.enUpdate;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case Mode.enUpdate:
                    return _UpdateLicesnseClass();

                default: return false;
            }
        }


    }
}
