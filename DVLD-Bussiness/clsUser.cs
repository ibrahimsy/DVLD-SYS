using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Bussiness
{
    public class clsUser
    {

        //public static clsUser CurrentUser;
        enum Mode { enAddNew = 1, enUpdate = 2 }

        Mode _Mode;

        public int UserID { get; set; }
        public int PersonID { get; set; }

        public clsPerson PersonInfo;
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        


        public clsUser()
        {
            UserID = -1;
            PersonID = -1;
            UserName = "";
            Password = "";
            IsActive = false;
            _Mode = Mode.enAddNew;

        }

        private clsUser(int UserID,int PersonID,string UserName,string Password,bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.PersonInfo = clsPerson.Find(PersonID);
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;

            _Mode = Mode.enUpdate;
        }

        private bool _AddNewUser()
        {
            this.UserID = clsUserData.AddNewUser(PersonID, UserName, Password, IsActive);

            return UserID != -1;
        }

        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(UserID, PersonID, UserName, Password, IsActive);
        }

        public static clsUser Find(int UserID)
        {
            int PersonID = -1;
            string UserName = "";
            string Password = "";
            bool IsActive = false;

            if (clsUserData.GetUserInfoByID(UserID,ref PersonID, ref UserName, ref Password, ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }

        public static clsUser Find(string UserName)
        {
            int PersonID = -1;
            int UserID = -1;
            string Password = "";
            bool IsActive = false;

            if (clsUserData.GetUserInfoByUserName(ref UserID, ref PersonID,UserName, ref Password, ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }

        public static clsUser Find(string UserName,string Password)
        {
            int PersonID = -1;
            int UserID = -1;
            bool IsActive = false;

            if (clsUserData.GetUserInfoByUserNameAndPassword(ref UserID, ref PersonID, UserName, Password, ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }

        public static bool IsExist(int UserID)
        {
            return clsUserData.IsExist(UserID);
        }

        public static bool IsExist(string UserName)
        {
            return clsUserData.IsExist(UserName);
        }

        public static bool IsExist(string UserName,string Password)
        {
            return clsUserData.IsExist(UserName,Password);
        }

        public static bool IsLinkedToPerson(int PersonID)
        {
            return clsUserData.IsUserLinkedToPerson(PersonID);
        }
        public static bool Delete(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }

        public  bool ChangeUserPassword(string NewPassword)
        {
            return clsUserData.ChangePassword(this.UserID, NewPassword);
        }
        public static DataTable UsersList()
        {
            return clsUserData.GetUsersList();
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case Mode.enAddNew:
                    if (_AddNewUser())
                    {
                        _Mode = Mode.enUpdate;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case Mode.enUpdate:
                    return _UpdateUser();

                default: return false;
            }
        }
    }
}
