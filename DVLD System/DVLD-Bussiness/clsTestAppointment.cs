using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Bussiness
{
    public class clsTestAppointment
    {
        /*
         TestAppointmentID
         TestTypeID
         LocalDrivingLicenseApplicationID
         AppointmentDate
         PaidFees
         CreatedByUserID
         IsLocked

         [dbo].[TestAppointments]
         */

        enum Mode { enAddNew = 1, enUpdate = 2 }

        Mode _Mode;

        public int TestAppointmentID { get; set; }
        public int TestTypeID { get; set; }
        clsTestType TestTypeInfo;
        public int LocalDrivingLicenseApplicationID { get; set; }
        clsLocalDrivingLicenseApplication LocalDrivingLicenseApplicationInfo;  
        public DateTime AppointmentDate { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        clsUser UserInfo;
        public byte IsLocked { set; get; }



        public clsTestAppointment()
        {
            _Mode = Mode.enAddNew;

            this.TestAppointmentID = -1;
            this.TestTypeID = -1;
            this.LocalDrivingLicenseApplicationID = -1;
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = 0;  
            this.CreatedByUserID = -1;
            this.IsLocked = 0;
        }

        private clsTestAppointment(int TestAppointmentID,int TestTypeID,int LocalDrivingLicenseApplicationID,DateTime AppointmentDate,
                                  float PaidFees,int CreatedByUserID,byte IsLocked)
        {
            _Mode = Mode.enUpdate;

            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            TestTypeInfo = clsTestType.Find(TestTypeID);
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            LocalDrivingLicenseApplicationInfo = clsLocalDrivingLicenseApplication.Find(LocalDrivingLicenseApplicationID);
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            UserInfo = clsUser.Find(CreatedByUserID);
            this.IsLocked = IsLocked;
        }

        private bool _AddNewTestAppointment()
        {
            this.TestAppointmentID = clsTestAppointmentData.AddNewTestAppointment(TestTypeID
                                                                                  ,LocalDrivingLicenseApplicationID
                                                                                  ,AppointmentDate
                                                                                  ,PaidFees
                                                                                  ,CreatedByUserID
                                                                                  ,IsLocked);
            return TestAppointmentID != -1;
        }

        private bool _UpdateTestAppointment()
        {
            return clsTestAppointmentData.UpdateTestAppointment(TestAppointmentID,
                                                                TestTypeID
                                                                ,LocalDrivingLicenseApplicationID
                                                                ,AppointmentDate
                                                                ,PaidFees
                                                                ,CreatedByUserID
                                                                ,IsLocked);
        }

        public static clsTestAppointment Find(int TestAppointmentID)
        {
            int TestTypeID = -1;
            int LocalDrivingLicenseApplicationID = -1;
            DateTime AppointmentDate = DateTime.Now;
            float PaidFees = 0;
            int CreatedByUserID = -1;
            byte IsLocked = 0;

            if (clsTestAppointmentData.GetTestAppointmentInfoByID(TestAppointmentID
                                                                 ,ref TestTypeID
                                                                 ,ref LocalDrivingLicenseApplicationID
                                                                 ,ref AppointmentDate
                                                                 ,ref PaidFees
                                                                 ,ref CreatedByUserID
                                                                 ,ref IsLocked))
            {
                return new clsTestAppointment(TestAppointmentID
                                              ,TestTypeID
                                              ,LocalDrivingLicenseApplicationID
                                              ,AppointmentDate
                                              ,PaidFees
                                              ,CreatedByUserID
                                              ,IsLocked);
            }
            else
            {
                return null;
            }
        }


        public static bool IsExist(int TestAppointmentID)
        {
            return clsTestAppointmentData.IsTestAppointmentExist(TestAppointmentID);
        }

        public static bool Delete(int TestAppointmentID)
        {
            return clsTestAppointmentData.DeleteTestAppointment(TestAppointmentID);
        }

        public static DataTable GetAllTestAppointmentes()
        {
            return clsTestAppointmentData.GetAllTestAppointments();
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case Mode.enAddNew:
                    if (_AddNewTestAppointment())
                    {
                        _Mode = Mode.enUpdate;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case Mode.enUpdate:
                    return _UpdateTestAppointment();

                default: return false;
            }
        }

    }
}
