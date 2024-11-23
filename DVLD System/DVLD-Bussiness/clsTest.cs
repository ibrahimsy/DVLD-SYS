﻿using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Bussiness
{
    public class clsTest
    {
        /*
     TestID
     TestAppointmentID
     TestResult
     Notes
     CreatedByUserID
     */
        enum Mode { enAddNew = 1, enUpdate = 2 }

        Mode _Mode;

        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }

        public clsTestAppointment TestAppointmentInfo;
        public byte TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }

        public clsUser UserInfo;
        public clsTest()
        {
            _Mode = Mode.enAddNew;

            this.TestID = -1;
            this.TestAppointmentID = -1;
            this.TestResult = 0;
            this.Notes = "";
            this.CreatedByUserID = -1;
        }

        private clsTest(int TestID, int TestAppointmentID, byte TestResult,
                                string Notes,int CreatedByUserID)
        {
            _Mode = Mode.enUpdate;

            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            TestAppointmentInfo = clsTestAppointment.Find(TestAppointmentID);
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;
            UserInfo = clsUser.Find(CreatedByUserID);
        }

        private bool _AddNewTest()
        {
            this.TestID = clsTestData.AddNewTest(TestAppointmentID,  TestResult,
                                                    Notes,  CreatedByUserID);

            return TestID != -1;
        }

        private bool _UpdateTest()
        {
            return clsTestData.UpdateTest(TestID, TestAppointmentID, TestResult,
                                                    Notes, CreatedByUserID);
        }

        public static clsTest Find(int TestID)
        {
            int TestAppointmentID = -1;
            byte TestResult = 0;
            string Notes = "";
            int CreatedByUserID = -1; 

            if (clsTestData.GetTestInfoByID(TestID, ref TestAppointmentID, ref TestResult, ref Notes,ref CreatedByUserID))
            {
                return new clsTest(TestID, TestAppointmentID,  TestResult,  Notes,  CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

        public static bool IsExist(int TestID)
        {
            return clsTestData.IsTestExist(TestID);
        }

        public static bool Delete(int TestID)
        {
            return clsTestData.DeleteTest(TestID);
        }

        public static DataTable GetAllTestes()
        {
            return clsTestData.GetAllTests();
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case Mode.enAddNew:
                    if (_AddNewTest())
                    {
                        _Mode = Mode.enUpdate;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case Mode.enUpdate:
                    return _UpdateTest();

                default: return false;
            }
        }

    }
}