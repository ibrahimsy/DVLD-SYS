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
     int TestAppointmentID
     int TestTypeID
     int LocalDrivingLicenseApplicationID
     DateTime AppointmentDate
     float PaidFees
     int CreatedByUserID
     byte IsLocked
     */
    public class clsTestAppointmentData
    {
        public static bool GetTestAppointmentInfoByID(int TestAppointmentID,
                                                      ref int TestTypeID,
                                                      ref int LocalDrivingLicenseApplicationID,
                                                      ref DateTime AppointmentDate,
                                                      ref float PaidFees,
                                                      ref int CreatedByUserID,
                                                      ref bool IsLocked,
                                                      ref int RetakApplicationID)
        {
            bool _isFound = false;

            string query = @"SELECT * FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID";

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

                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestTypeID = (int)reader["TestTypeID"];
                    LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsLocked = (bool)reader["IsLocked"];
                    if(reader["RetakeTestApplicationID"] != DBNull.Value)
                        RetakApplicationID = (int)reader["RetakeTestApplicationID"];
                    else
                    {
                        RetakApplicationID = -1;
                    }

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

        public static int AddNewTestAppointment(int TestTypeID,
                                                int LocalDrivingLicenseApplicationID,
                                                DateTime AppointmentDate,
                                                float PaidFees,
                                                int CreatedByUserID,
                                                bool IsLocked,
                                                int RetakeApplicationID)
        {
            int TestAppointmentID = -1;

            string query = @"INSERT INTO TestAppointments
                               ( TestTypeID,
                                 LocalDrivingLicenseApplicationID,
                                 AppointmentDate,
                                 PaidFees,
                                 CreatedByUserID,
                                 IsLocked,
                                  RetakeTestApplicationID
                               )
                         VALUES
                               (@TestTypeID,
                                 @LocalDrivingLicenseApplicationID,
                                 @AppointmentDate,
                                 @PaidFees,
                                 @CreatedByUserID,
                                 @IsLocked,
                                 @RetakeTestApplicationID
                               );
                                SELECT SCOPE_IDENTITY();";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            if (RetakeApplicationID != -1)
            {
                command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeApplicationID);
            }
            else
            {
                command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
            }
            command.Parameters.AddWithValue("@IsLocked", IsLocked);
            //command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null && (int.TryParse(result.ToString(), out int ID)))
                {
                    TestAppointmentID = ID;
                }
            }
            catch (Exception ex)
            {

            }
            finally { connection.Close(); }

            return TestAppointmentID;
        }
        public static bool UpdateTestAppointment(int TestAppointmentID,
                                                 int TestTypeID,
                                                 int LocalDrivingLicenseApplicationID,
                                                 DateTime AppointmentDate,
                                                 float PaidFees,
                                                 int CreatedByUserID,
                                                 bool IsLocked,
                                                int RetakeTestApplicationID)

        {
            int AffectedRows = 0;

            string query = @"UPDATE TestAppointments
                           SET TestTypeID = @TestTypeID
                              ,LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                              ,AppointmentDate = @AppointmentDate
                              ,PaidFees = @PaidFees
                              ,CreatedByUserID = @CreatedByUserID
                              ,IsLocked = @IsLocked
                              ,RetakeTestApplicationID = @RetakeTestApplicationID
                           WHERE TestAppointmentID = @TestAppointmentID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);
            //command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);

            if (RetakeTestApplicationID != -1)
            {
                command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);
            }
            else
            {
                command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
            }

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

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
        public static bool DeleteTestAppointment(int TestAppointmentID)
        {
            int AffectedRows = 0;

            string query = @"DELETE FROM TestAppointments
                                WHERE TestAppointmentID =@TestAppointmentID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

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

        public static bool IsTestAppointmentExist(int TestAppointmentID)
        {
            bool _isFound = false;

            string query = "SELECT found = 1 FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

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

        public static bool IsTestAppointmentExist(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool _isFound = false;

            string query = @"SELECT found = 1 from TestAppointments WHERE
                            LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                            And TestTypeID = @TestTypeID
                            ORDER BY AppointmentDate DESC";

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
       
        public static bool IsTestAppointmentLocked(int TestAppointmentID)
        {
            bool _isFound = false;

            string query = "SELECT found = 1 FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID AND IsLocked = 1" ;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

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
        
        public static bool IsTestAppointmentLocked(int LocalDrivingLicenseApplicationID,int TestTypeID)
        {
            bool _isFound = false;

            string query = @"select found = 1 from
                            (
                            SELECT top 1 * from TestAppointments WHERE
                            LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                            And TestTypeID = @TestTypeID
                            ORDER BY AppointmentDate DESC) R1
						    where R1.Islocked = 1";

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

        public static DataTable GetAllTestAppointments()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT * FROM TestAppointments";

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
        
        public static DataTable GetTestAppointmentsByLocalApplicationIDAndTestType(int LocalDrivingLicenseApplicationID,int TestTypeID)
        {
            DataTable dt = new DataTable();

            string query = @"select * from TestAppointments where
                            LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                            And TestTypeID = @TestTypeID
                            order by TestAppointmentID Desc";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

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

        public static DataTable GetScheduleTestsPerTestType(int LocalApplicationID, int TestTypeID)
        {
            DataTable dt = new DataTable();

            string query = @"SELECT TestAppointmentID,TestAppointments.AppointmentDate,TestAppointments.PaidFees,TestAppointments.IsLocked
                            FROM  TestAppointments INNER JOIN
                            LocalDrivingLicenseApplications ON TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
		                    where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                            And TestAppointments.TestTypeID = @TestTypeID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

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

        public static int GetTrailTestByTestTypeAndLocalDrivingApplication(int TestTypeID,int LDLAppID)
        {
            int TrailCount = -1;

            string query = @"select count(TestTypeID) As TrailVisionTest from TestAppointments
                                where TestTypeID = @TestTypeID and
                                LocalDrivingLicenseApplicationID = @LDLAppID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LDLAppID", LDLAppID);
           

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null && (int.TryParse(result.ToString(), out int ID)))
                {
                    TrailCount = ID;
                }
            }
            catch (Exception ex)
            {

            }
            finally { connection.Close(); }

            return TrailCount;
        }
    
        public static DateTime GetLatestAppointmentDateByLocalApplicationIDAndTestType(int LDLAppID, int TestTypeID)
        {
            DateTime TestAppointmentDate = DateTime.Now;

            string query = @"SELECT TOP 1 AppointmentDate FROM TestAppointments WHERE
                            LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                            and TestTypeID = @TestTypeID
                            ORDER BY TestAppointmentID DESC";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDLAppID);
           
            try
            {
                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null && (DateTime.TryParse(result.ToString(), out DateTime Date)))
                {
                    TestAppointmentDate = Date;
                }
            }
            catch (Exception ex)
            {

            }
            finally { connection.Close(); }

            return TestAppointmentDate;
        }
    
    
    }
}
