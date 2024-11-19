using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_Bussiness
{
    public class clsPerson
    {
       
        enum Mode { enAddNew = 1,enUpdate = 2}
        
        Mode _Mode;
       
        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }

        public string FullName
        {
            get { return FirstName + " " + SecondName + " " + ThirdName + " " + LastName; }
        }
        public DateTime DateOfBirth { get; set; }
        public short Gendor { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }

        public clsCountry CountryInfo;
        public string ImagePath { get; set; }   


        public clsPerson()
        {
            PersonID = -1;
            NationalNo = "";
            FirstName = "";
            SecondName = "";
            ThirdName = "";
            LastName = "";
            DateOfBirth = DateTime.Now;
            Gendor = 0;
            Address = "";
            Phone = "";
            Email = "";
            NationalityCountryID = -1;
            ImagePath = "";

            _Mode = Mode.enAddNew;

            }

        private clsPerson(int personID,string nationalNo,string firstName,string secondName,string thirdName,string lastName,
            DateTime dateOfBirth,short gendor,string address,string phone,string email,int nationalityCountryId,string imagePath)
        {
            PersonID = personID;
            NationalNo = nationalNo;
            FirstName = firstName;
            SecondName = secondName;
            ThirdName = thirdName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gendor = gendor;
            Address = address;
            Phone = phone;
            Email = email;
            NationalityCountryID = nationalityCountryId;
            CountryInfo = clsCountry.Find(NationalityCountryID);
            ImagePath = imagePath;

            _Mode = Mode.enUpdate;
        }

        private bool _AddNewPerson()
        {
            this.PersonID = clsPersonData.AddNewPerson(NationalNo,FirstName,SecondName,ThirdName,LastName,
                DateOfBirth,Gendor,Address,Phone,Email,NationalityCountryID,ImagePath);

            return PersonID != -1;
        }

        private bool _UpdatePerson()
        {
            return clsPersonData.UpdatePerson(PersonID,NationalNo, FirstName, SecondName, ThirdName, LastName,
                DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);
        }

        public static clsPerson Find(int PersonID)
        {
            string NationalNo = "";
            string FirstName = "";
            string SecondName = "";
            string ThirdName = "";
            string LastName = "";
            DateTime DateOfBirth = DateTime.Now;
            short Gendor = 0;
            string Address = "";
            string Phone = "";
            string Email = "";
            int NationalityCountryID = -1;
            string ImagePath = "";

             if (clsPersonData.GetPersonInfoByID(PersonID,ref NationalNo,ref FirstName,ref SecondName,ref ThirdName,ref LastName,
                 ref DateOfBirth,ref Gendor,ref Address,ref Phone,ref Email,ref NationalityCountryID,ref ImagePath))
            {
                return new clsPerson(PersonID, NationalNo,  FirstName,  SecondName,  ThirdName,  LastName,
                  DateOfBirth,  Gendor,  Address,  Phone,  Email,  NationalityCountryID,  ImagePath);
            }
            else
            {
                return null;
            }
        }

        public static clsPerson Find(string NationalNo)
        {
            int PersonID = -1;
            string FirstName = "";
            string SecondName = "";
            string ThirdName = "";
            string LastName = "";
            DateTime DateOfBirth = DateTime.Now;
            short Gendor = 0;
            string Address = "";
            string Phone = "";
            string Email = "";
            int NationalityCountryID = -1;
            string ImagePath = "";

            if (clsPersonData.GetPersonInfoByNo(ref PersonID, NationalNo, ref FirstName, ref SecondName, ref ThirdName, ref LastName,
                ref DateOfBirth, ref Gendor, ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath))
            {
                return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName,
                  DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);
            }
            else
            {
                return null;
            }
        }
    
        public static bool IsExist(int PersonID)
        {
            return clsPersonData.IsPersonExist(PersonID);
        }

        public static bool IsExist(string NationalNo)
        {
            return clsPersonData.IsPersonExist(NationalNo);
        }

        public static bool Delete(int PersonID)
        {
              return  clsPersonData.DeletePerson(PersonID);
        }

        public static DataTable PeopleList()
        {
            return clsPersonData.GetAllPeople();
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case Mode.enAddNew:
                    if (_AddNewPerson())
                    {
                        _Mode = Mode.enUpdate;
                        return true;
                    }else 
                    {
                        return false; 
                    }
                case Mode.enUpdate:
                    return _UpdatePerson();

                default: return false;
            }
        }
    }
}
