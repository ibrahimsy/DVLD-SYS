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
        public static bool GetTestInfoByID(int TestID, ref int TestAppointmentID, ref byte TestResult, ref string Notes,ref int CreatedByUserID)
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
                    TestResult = (byte)(reader["TestResult"]);
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

        public static int AddNewTest(int TestAppointmentID,byte TestResult,string Notes,int CreatedByUserID)
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
        public static bool UpdateTest(int TestID, int TestAppointmentID, byte TestResult, string Notes, int CreatedByUserID)
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
    }
}
