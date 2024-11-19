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
                                                      ref byte IsLocked)
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
                    PaidFees = (float)reader["PaidFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsLocked = (byte)reader["IsLocked"];
                    //RetakeTestApplicationID = (int)reader["RetakeTestApplicationID"];

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
                                                byte IsLocked)
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
                                                byte IsLocked)

        {
            int AffectedRows = 0;

            string query = @"UPDATE TestAppointments
                           SET TestTypeID = @TestTypeID
                              ,LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                              ,AppointmentDate = @AppointmentDate
                              ,PaidFees = @PaidFees
                              ,CreatedByUserID = @CreatedByUserID
                              ,IsLocked = @IsLocked
                              
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

    }
}
