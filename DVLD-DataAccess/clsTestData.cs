using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    /*
     TestID
     TestAppointmentID
     TestResult
     Notes
     CreatedByUserID
     */

    public class clsTestData
    {
        public static bool GetTestInfoByID(int TestID, ref int TestAppointmentID, ref bool TestResult, ref string Notes,ref int CreatedByUserID)
        {
            bool _isFound = false;

            string query = @"SELECT * FROM Tests WHERE TestID = @TestID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestID", TestID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    _isFound = true;

                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestResult = (bool)(reader["TestResult"]);
                    Notes = (string)reader["Notes"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                }
                reader.Close();
            }
            catch (Exception ex)
            {
                _isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return _isFound;
        }

        public static bool GetTestInfoByAppointmentID(ref int TestID, int TestAppointmentID, ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            bool _isFound = false;

            string query = @"SELECT * FROM Tests WHERE TestAppointmentID = @TestAppointmentID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    _isFound = true;

                    TestID = (int)reader["TestID"];
                    TestResult = (bool)reader["TestResult"];
                    Notes = (string)reader["Notes"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                }
                reader.Close();
            }
            catch (Exception ex)
            {
                _isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return _isFound;
        }

        public static int AddNewTest(int TestAppointmentID,bool TestResult,string Notes,int CreatedByUserID)
        {
            int TestID = -1;

            string query = @"INSERT INTO Tests
                               (TestAppointmentID
                               ,TestResult
                               ,Notes
                               ,CreatedByUserID)
                         VALUES
                               (@TestAppointmentID
                               ,@TestResult
                               ,@Notes
                               ,@CreatedByUserID );
                    UPDATE TestAppointments
                    SET IsLocked = 1
                    WHERE TestAppointmentID = @TestAppointmentID
                                SELECT SCOPE_IDENTITY();";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            command.Parameters.AddWithValue("@Notes", Notes);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null && (int.TryParse(result.ToString(), out int ID)))
                {
                    TestID = ID;
                }
            }
            catch (Exception ex)
            {

            }
            finally { connection.Close(); }

            return TestID;
        }
        
        public static bool UpdateTest(int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int AffectedRows = 0;

            string query = @"UPDATE Tests
                           SET TestAppointmentID = @TestAppointmentID
                              ,TestResult = @TestResult
                              ,Notes = @Notes
                              ,CreatedByUserID = @CreatedByUserID
                           WHERE TestID = @TestID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestID", TestID);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            command.Parameters.AddWithValue("@Notes", Notes);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                AffectedRows = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return AffectedRows > 0;
        }
        
        public static bool DeleteTest(int TestID)
        {
            int AffectedRows = 0;

            string query = @"DELETE FROM Tests
                                WHERE TestID =@TestID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestID", TestID);

            try
            {
                connection.Open();

                AffectedRows = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

            }
            finally { connection.Close(); }

            return AffectedRows > 0;
        }

        public static bool IsTestExist(int TestID)
        {
            bool _isFound = false;

            string query = "SELECT found = 1 FROM Tests WHERE TestID = @TestID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestID", TestID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    _isFound = true;

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                _isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return _isFound;
        }

        public static bool IsTestPassed(int LocalDrivingLicenseApplicationID,int TestTypeID)
        {
            bool _isFound = false;

            string query = @"select found = 1 from Tests where TestAppointmentID =(
                            select top 1 TestAppointmentID from TestAppointments where
                            LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                            And TestTypeID = @TestTypeID
                            order by AppointmentDate Desc)
                            and TestResult = 1";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    _isFound = true;

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                _isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return _isFound;
        }
       
        public static bool GetLastTestPerTestType(int LocalDrivingLicenseApplicationID,int TestTypeID,ref int TestID,ref int TestAppointmentID,ref bool TestResult,ref string Notes,ref int CreatedByUserID)
        {
            bool _isFound = false;

            string query = @"SELECT TOP 1 Tests.*
                            FROM  LocalDrivingLicenseApplications INNER JOIN
                                     TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                     Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
		                             WHERE LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
		                             And TestTypeID = @TestTypeID
                            ORDER BY TestID DESC";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    _isFound = true;

                    TestID = (int)reader["TestID"];
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestResult = (bool)(reader["TestResult"]);
                    Notes = (string)reader["Notes"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                }
                reader.Close();
            }
            catch (Exception ex)
            {
                _isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return _isFound;
        } 


        public static DataTable GetAllTests()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT * FROM Tests";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);

                    reader.Close();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static int GetTestID(int TestAppointmentID)
        {
            int TestID = -1;

            string query = @"SELECT * FROM Tests WHERE TestAppointmentID = @TestAppointmentID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null && (int.TryParse(result.ToString(), out int ID)))
                {
                    TestID = ID;
                }
            }
            catch (Exception ex)
            {

            }
            finally { connection.Close(); }

            return TestID;
        }
    }
}
