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
     DriverID
     PersonID
     CreatedByUserID
     CreatedDate
     */
    public class clsDriver
    {
        enum Mode { enAddNew = 1, enUpdate = 2 }

        Mode _Mode;

        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public clsPerson PersonInfo;
        public int CreatedByUserID { get; set; }
        public clsUser UserInfo;
        public DateTime CreatedDate { get; set; }

        public clsDriver()
        {
            _Mode = Mode.enAddNew;

            this.DriverID = -1;
            this.PersonID = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.Now;
        }

        private clsDriver(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            _Mode = Mode.enUpdate;

            this.DriverID = DriverID;

            this.PersonID = PersonID;
            PersonInfo = clsPerson.Find(this.PersonID); 

            this.CreatedByUserID = CreatedByUserID;
            this.UserInfo = clsUser.Find(this.CreatedByUserID);

            this.CreatedDate = CreatedDate;
   
        }

        private bool _AddNewDriver()
        {
            this.DriverID = clsDriverData.AddNewDriver(PersonID,CreatedByUserID,CreatedDate);

            return DriverID != -1;
        }

        private bool _UpdateDriver()
        {
            return clsDriverData.UpdateDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
        }

        public static clsDriver Find(int DriverID)
        {
            int PersonID = -1;
            int CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;
            

            if (clsDriverData.GetDriverInfoByID(DriverID,ref PersonID,ref CreatedByUserID,ref CreatedDate))
            {
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            }
            else
            {
                return null;
            }
        }

        public static bool IsExist(int DriverID)
        {
            return clsDriverData.IsDriverExist(DriverID);
        }

        public static bool Delete(int DriverID)
        {
            return clsDriverData.DeleteDriver(DriverID);
        }

        public static DataTable GetAllDriveres()
        {
            return clsDriverData.GetAllDrivers();
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case Mode.enAddNew:
                    if (_AddNewDriver())
                    {
                        _Mode = Mode.enUpdate;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case Mode.enUpdate:
                    return _UpdateDriver();

                default: return false;
            }
        }
    }
}
