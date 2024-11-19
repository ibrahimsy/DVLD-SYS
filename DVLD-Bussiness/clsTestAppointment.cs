﻿using DVLD_DataAccess;
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
        public clsTestType TestTypeInfo;
        public int LocalDrivingLicenseApplicationID { get; set; }
        public clsLocalDrivingLicenseApplication LocalDrivingLicenseApplicationInfo;  
        public DateTime AppointmentDate { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser UserInfo;
        public bool IsLocked { set; get; }

        public int TestID
        {
            get { return GetTestID(); }
        }
    public int RetakApplicationID { set; get; }

        public clsTestAppointment()
        {
            _Mode = Mode.enAddNew;

            this.TestAppointmentID = -1;
            this.TestTypeID = -1;
            this.LocalDrivingLicenseApplicationID = -1;
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = 0;  
            this.CreatedByUserID = -1;
            this.IsLocked = false;
            this.RetakApplicationID = -1;
        }

        private clsTestAppointment(int TestAppointmentID,int TestTypeID,int LocalDrivingLicenseApplicationID,DateTime AppointmentDate,
                                  float PaidFees,int CreatedByUserID,bool IsLocked,int RetakApplicationID)
        {
            _Mode = Mode.enUpdate;

            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            TestTypeInfo = clsTestType.Find(TestTypeID);
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            LocalDrivingLicenseApplicationInfo = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseID(LocalDrivingLicenseApplicationID);
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            UserInfo = clsUser.Find(CreatedByUserID);
            this.IsLocked = IsLocked;
            this.RetakApplicationID=RetakApplicationID;
        }

        private bool _AddNewTestAppointment()
        {
            this.TestAppointmentID = clsTestAppointmentData.AddNewTestAppointment(TestTypeID
                                                                                  ,LocalDrivingLicenseApplicationID
                                                                                  ,AppointmentDate
                                                                                  ,PaidFees
                                                                                  ,CreatedByUserID
                                                                                  ,IsLocked
                                                                                  ,RetakApplicationID);
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
                                                                ,IsLocked
                                                                , RetakApplicationID);
        }

        public static clsTestAppointment Find(int TestAppointmentID)
        {
            int TestTypeID = -1;
            int LocalDrivingLicenseApplicationID = -1;
            DateTime AppointmentDate = DateTime.Now;
            float PaidFees = 0;
            int CreatedByUserID = -1;
            bool IsLocked = false;
            int RetakApplicationID = -1;
            if (clsTestAppointmentData.GetTestAppointmentInfoByID(TestAppointmentID
                                                                 ,ref TestTypeID
                                                                 ,ref LocalDrivingLicenseApplicationID
                                                                 ,ref AppointmentDate
                                                                 ,ref PaidFees
                                                                 ,ref CreatedByUserID
                                                                 ,ref IsLocked,
                                                                 ref RetakApplicationID))
            {
                return new clsTestAppointment(TestAppointmentID
                                              ,TestTypeID
                                              ,LocalDrivingLicenseApplicationID
                                              ,AppointmentDate
                                              ,PaidFees
                                              ,CreatedByUserID
                                              ,IsLocked
                                              ,RetakApplicationID);
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

        

        public static bool IsTestAppointmentLocked(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return clsTestAppointmentData.IsTestAppointmentLocked(LocalDrivingLicenseApplicationID, TestTypeID);
        }

        public static bool IsTestAppointmentExist(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return clsTestAppointmentData.IsTestAppointmentExist(LocalDrivingLicenseApplicationID,TestTypeID);
        }

        public static bool IsTestAppointmentLocked(int TestAppointment)
        {
            return clsTestAppointmentData.IsTestAppointmentLocked(TestAppointment);
        }

        public static bool Delete(int TestAppointmentID)
        {
            return clsTestAppointmentData.DeleteTestAppointment(TestAppointmentID);
        }

        public static DataTable GetAllTestAppointmentes()
        {
            return clsTestAppointmentData.GetAllTestAppointments();
        }

        public static DataTable GetTestAppointmentsByLocalApplicationIDAndTestTypeID(int LDLAppID,int TestTypeID)
        {
            return clsTestAppointmentData.GetTestAppointmentsByLocalApplicationIDAndTestType(LDLAppID, TestTypeID);
        }

        public static DataTable GetScheduleTestsPerTestType(int LocalDrivingLicenseApplicationID,clsTestType.enTestType TestType)
        {
            return clsTestAppointmentData.GetScheduleTestsPerTestType(LocalDrivingLicenseApplicationID, (int)TestType);
        }

        public static int GetTestTrailCount(int TestTypeID,int LocalDrivingLicenseApplicationID)
        {
            return clsTestAppointmentData.GetTrailTestByTestTypeAndLocalDrivingApplication(TestTypeID,LocalDrivingLicenseApplicationID);
        }

        public static DateTime GetLatestAppointmentDate(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return clsTestAppointmentData.GetLatestAppointmentDateByLocalApplicationIDAndTestType(LocalDrivingLicenseApplicationID, TestTypeID);
        }

        public int GetTestID()
        {
            return clsTestData.GetTestID(this.TestAppointmentID);
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
