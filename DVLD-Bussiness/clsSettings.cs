using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;
namespace BankBussiness
{



    public class clsSetting
    {

        enum enMode { enAddNew = 1, enUpdate = 2 }
        enMode _Mode = enMode.enAddNew;

        public enum enSettings { PassingScore  = 1, QuestionsPerExam  = 2 ,VehicleFee = 3}

        public int SettingID { set; get; }
        public string SettingName { set; get; }
        public byte SettingValue { set; get; }
        public string SettingDescription { set; get; }

        public clsSetting()
        {
            this.SettingID = -1;
            this.SettingName = "";
            this.SettingValue = 1;
            this.SettingDescription = "";
            _Mode = enMode.enAddNew;
        }

        private clsSetting(int SettingID, string SettingName, byte SettingValue, string SettingDescription)
        {
            this.SettingID = SettingID;
            this.SettingName = SettingName;
            this.SettingValue = SettingValue;
            this.SettingDescription = SettingDescription;
            _Mode = enMode.enUpdate;
        }

        private bool _AddSetting()
        {
            SettingID = clsSettingData.AddSetting(SettingName, SettingValue, SettingDescription);

            return (SettingID != -1);
        }


        private bool _UpdateSetting()
        {
            return clsSettingData.UpdateSettingByID(SettingID, SettingName, SettingValue, SettingDescription);
        }


        public static clsSetting FindSettingByID(int SettingID)
        {

            string SettingName = "";
            byte SettingValue = 1;
            string SettingDescription = "";
            if (clsSettingData.GetSettingByID(SettingID, ref SettingName, ref SettingValue, ref SettingDescription))
            {
                return new clsSetting(SettingID, SettingName, SettingValue, SettingDescription);
            }
            else
            {
                return null;
            }
        }


        public static bool IsExistBySettingID(int SettingID)
        {
            return clsSettingData.IsSettingExistBySettingID(SettingID);
        }


        public static bool DeleteSetting(int SettingID)
        {
            return clsSettingData.DeleteSettingByID(SettingID);
        }


        public static DataTable GetSettingsList()
        {
            return clsSettingData.GetAllSettings();
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.enAddNew:
                    if (_AddSetting())
                    {
                        _Mode = enMode.enUpdate;
                        return true;
                    }
                    else
                        return false;

                case enMode.enUpdate:
                    if (_UpdateSetting())
                        return true;
                    else
                        return false;
            }
            return false;
        }


    }
}
