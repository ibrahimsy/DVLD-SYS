using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Bussiness
{
    /*
     [TestTypeID]
     [TestTypeTitle]
     [TestTypeDescription]
     [TestTypeFees]
     [dbo].[TestTypes]
     */
    public class clsTestType
    {
        enum Mode { enAddNew = 1, enUpdate = 2 }

        Mode _Mode;

        public int TestTypeID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public float TestTypeFees { get; set; }
        public clsTestType()
        {
            _Mode = Mode.enAddNew;

            this.TestTypeID = -1;
            this.TestTypeTitle = "";
            this.TestTypeDescription = "";
            this.TestTypeFees = 0;
        }

        private clsTestType(int TestTypeID, string TestTypeTitle, string TestTypeDescription,
                                float TestTypeFees)
        {
            _Mode = Mode.enUpdate;

            this.TestTypeID = TestTypeID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.TestTypeFees = TestTypeFees;
        }

        private bool _AddNewTestType()
        {
            this.TestTypeID = clsTestTypeData.AddNewTestType(TestTypeTitle,TestTypeDescription,TestTypeFees);

            return TestTypeID != -1;
        }

        private bool _UpdateTestType()
        {
            return clsTestTypeData.UpdateTestType(TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);
        }

        public static clsTestType Find(int TestTypeID)
        {
            string TestTypeTitle = "";
            string TestTypeDescription = "";
            float TestTypeFees = 0;

            if (clsTestTypeData.GetTestTypeInfoByID(TestTypeID, ref TestTypeTitle, ref TestTypeDescription, ref TestTypeFees))
            {
                return new clsTestType(TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);
            }
            else
            {
                return null;
            }
        }

        public static clsTestType Find(string TestTypeTitle)
        {
            int TestTypeID = -1;
            string TestTypeDescription = "";
            float TestTypeFees = 0;
 

            if (clsTestTypeData.GetTestTypeInfoByTitle(ref TestTypeID, TestTypeTitle, ref TestTypeDescription, ref TestTypeFees))
            {
                return new clsTestType(TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);
            }
            else
            {
                return null;
            }
        }

        public static bool IsExist(int TestTypeID)
        {
            return clsTestTypeData.IsTestTypeExist(TestTypeID);
        }

        public static bool Delete(int TestTypeID)
        {
            return clsTestTypeData.DeleteTestType(TestTypeID);
        }

        public static DataTable GetAllTestTypees()
        {
            return clsTestTypeData.GetAllTestTypes();
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case Mode.enAddNew:
                    if (_AddNewTestType())
                    {
                        _Mode = Mode.enUpdate;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case Mode.enUpdate:
                    return _UpdateTestType();

                default: return false;
            }
        }

    }
}
