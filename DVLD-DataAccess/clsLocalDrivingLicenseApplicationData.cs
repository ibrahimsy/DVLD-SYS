using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_DataAccess
{
    public class clsLocalDrivingLicenseApplicationData
    {
        /*
         [LocalDrivingLicenseApplicationID]
          ,[ApplicationID]
          ,[LicenseClassID]
         */
        public static bool GetLocalDrivingLicenseApplicationIDInfoByID(int LDLApplicationID, ref int ApplicationID, ref int LicenseClassID)
        {
            bool _isFound = false;

            string query = @"SELECT *
                             FROM LocalDrivingLicenseApplications
                             WHERE LocalDrivingLicenseApplicationID = @LDLApplicationID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LDLApplicationID", LDLApplicationID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    _isFound = true;

                    ApplicationID = (int)reader["ApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return _isFound;
        }

        public static int AddNewLocalDrivingLicenseApplication(int ApplicationID, int LicenseClassID)
        {
            int LDLApplicationID = -1;
            string query = @"INSERT INTO LocalDrivingLicenseApplications
                               (ApplicationID,LicenseClassID)
                         VALUES
                               (@ApplicationID,@LicenseClassID);
                                SELECT SCOPE_IDENTITY();";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null && (int.TryParse(result.ToString(), out int ID)))
                {
                    LDLApplicationID = ID;
                }
            }
            catch (Exception ex)
            {

            }
            finally { connection.Close(); }

            return LDLApplicationID;
        }

        public static bool UpdateLocalDrivingLicenseApplication(int LDLApplicationID, int ApplicationID, int LicenseClassID)
        {
            int AffectedRows = 0;

            string query = @"UPDATE LocalDrivingLicenseApplications
                           SET ApplicationID = @ApplicationID
                              ,LicenseClassID = @LicenseClassID     
                           WHERE LocalDrivingLicenseApplicationID = @LDLApplicationID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LDLApplicationID", LDLApplicationID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

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

        public static bool DeleteLocalDrivingLicenseApplication(int LDLApplicationID)
        {
            int AffectedRows = 0;

            string query = @"DELETE FROM LocalDrivingLicenseApplications
                                WHERE LocalDrivingLicenseApplicationID =@LDLApplicationID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LDLApplicationID", LDLApplicationID);

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

        public static bool IsLocalDrivingLicenseApplicationExist(int LDLApplicationID)
        {
            string query = "SELECT found = 1 FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID = @LDLApplicationID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LDLApplicationID", LDLApplicationID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return false;
        }

        public static int DoesApplicationExistWithLicenseClass(int ApplicantPersonID, int LicenseClassID)
        {
            int LocalDrivingLicenseApplicationID = -1;

            string query = @"SELECT   LocalDrivingLicenseApplicationID
                            FROM            Applications INNER JOIN LocalDrivingLicenseApplications 
                            ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                            where Applications.ApplicantPersonID = @ApplicantPersonID 
                            And ApplicationStatus in (1,3) 
                            And LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(),out int ID))
                {
                    LocalDrivingLicenseApplicationID = ID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return LocalDrivingLicenseApplicationID;
        }

        public static int GetActiveApplication(int ApplicantPersonID,int ApplicationStatus, int LicenseClassID)
        {
            int LocalDrivingLicenseApplicationID = -1;

            string query = @"SELECT   LocalDrivingLicenseApplicationID
                            FROM            Applications INNER JOIN LocalDrivingLicenseApplications 
                            ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                            where Applications.ApplicantPersonID = @ApplicantPersonID 
                            And ApplicationStatus  = @ApplicationStatus
                            And LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int ID))
                {
                    LocalDrivingLicenseApplicationID = ID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return LocalDrivingLicenseApplicationID;
        }

        public static int GetPassedTestCount(int LDLApplicationID)
        {
            int PassedTestCount = -1;

            string query = @"SELECT PassedTestCount FROM LocalDrivingLicenseApplications_View 
                            where LocalDrivingLicenseApplicationID = @LDLApplicationID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LDLApplicationID", LDLApplicationID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null && (int.TryParse(result.ToString(), out int ID)))
                {
                    PassedTestCount = ID;
                }
            }
            catch (Exception ex)
            {

            }
            finally { connection.Close(); }

            return PassedTestCount;
        }

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT * FROM LocalDrivingLicenseApplications_View
                            ORDER BY LocalDrivingLicenseApplicationID DESC";

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

        public static bool IsThereAnActiveAppointment(int LocalDrivingLicenseApplicationID,int TestTypeID)
        {
            bool hasActiveAppointment = false;

            string query = @"SELECT TOP 1 IsLocked  
                            FROM  LocalDrivingLicenseApplications INNER JOIN
                                  TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID
		                          WHERE LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
		                          AND TestTypeID = @TestTypeID AND IsLocked = 0
                            ORDER BY TestAppointmentID DESC";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object Result = command.ExecuteScalar();

                if (Result != null)
                {
                    hasActiveAppointment = true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
            return hasActiveAppointment;
        }

        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID,int TestTypeID)
        {
            bool Result = false;

            string query = @"SELECT Tests.TestResult
                             FROM  LocalDrivingLicenseApplications INNER JOIN
                             TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                             Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
		                     where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
		                     And TestTypeID = @TestTypeID And TestResult = 1";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && bool.TryParse(result.ToString(), out bool IsPassed))
                {
                    Result = IsPassed;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
            return Result;

        }

        public static int GetTestCountPerTestType(int LocalDrivingLicenseApplicationID,int TestTypeID)
        {
            int TestCountPerTestType = -1;

            string query = @"SELECT Count(TestID) TestCountPerTestType
                             FROM  LocalDrivingLicenseApplications INNER JOIN
                             TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                             Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
		                     where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                             And TestTypeID = @TestTypeID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int TestTrial))
                {
                    TestCountPerTestType = TestTrial;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return TestCountPerTestType;
        }
    
        public static bool DoesAttendTestType(int LocalDrivingLicenseApplicationID,int TestTypeID)
        {
            bool Result = false;

            string query = @"SELECT top 1 found = 1
                             FROM  LocalDrivingLicenseApplications INNER JOIN
                             TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                             Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
		                     where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
		                     And TestTypeID = @TestTypeID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    Result = true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
            return Result;
        }
    }
}
