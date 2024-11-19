using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_DataAccess
{
    public static class clsApplicationData
    {
 
        public static bool GetApplicationInfoByID(int ApplicationID,ref int ApplicantPersonID, ref DateTime ApplicationDate,ref int ApplicationTypeID,
                                               ref byte ApplicationStatus,ref DateTime LastStatusDate,ref float PaidFees,ref int CreatedByUserID)
        {
            bool _isFound = false;

            string query = @"SELECT * FROM Applications WHERE ApplicationID = @ApplicationID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                SqlDataReader reader =  command.ExecuteReader();
                while (reader.Read())
                {
                    _isFound = true;
                    ApplicantPersonID = (int)reader["ApplicantPersonID"];
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    ApplicationTypeID = (int)reader["ApplicationTypeID"];
                    ApplicationStatus = (byte)(reader["ApplicationStatus"]);
                    LastStatusDate   = (DateTime)reader["LastStatusDate"];
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                }
                reader.Close();
            }catch (Exception ex)
            {
                _isFound=false;
            }
            finally
            {
                 connection.Close();
            }

            return _isFound;
        }
        public static int AddNewApplication( int ApplicantPersonID, DateTime ApplicationDate,  int ApplicationTypeID,
                                                short ApplicationStatus,  DateTime LastStatusDate,  float PaidFees,  int CreatedByUserID)
        {
            int ApplicationID = -1;
            
            string query = @"INSERT INTO Applications
                               (ApplicantPersonID
                               ,ApplicationDate
                               ,ApplicationTypeID
                               ,ApplicationStatus
                               ,LastStatusDate
                               ,PaidFees
                               ,CreatedByUserID)
                         VALUES
                               (@ApplicantPersonID
                               ,@ApplicationDate
                               ,@ApplicationTypeID
                               ,@ApplicationStatus
                               ,@LastStatusDate
                               ,@PaidFees
                               ,@CreatedByUserID);
                                SELECT SCOPE_IDENTITY();";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null && (int.TryParse(result.ToString(), out int ID)))
                {
                    ApplicationID = ID;
                }
            }
            catch (Exception ex)
            {

            }
            finally { connection.Close(); }

            return ApplicationID;
        }

        public static bool UpdateApplication(int ApplicationID,int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID,
                                                short ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            int AffectedRows = 0;

            string query = @"UPDATE Applications
                           SET ApplicantPersonID = @ApplicantPersonID
                              ,ApplicationDate = @ApplicationDate
                              ,ApplicationTypeID = @ApplicationTypeID
                              ,ApplicationStatus = @ApplicationStatus
                              ,LastStatusDate = @LastStatusDate
                              ,PaidFees = @PaidFees
                              ,CreatedByUserID = @CreatedByUserID
                           WHERE ApplicationID = @ApplicationID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID",CreatedByUserID);
            command.Parameters.AddWithValue("@ApplicationID",ApplicationID);
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
        public static bool DeleteApplication(int ApplicationID)
        {
            int AffectedRows = 0;

            string query = @"DELETE FROM Applications
                                WHERE ApplicationID =@ApplicationID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

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

        public static bool IsApplicationExist(int ApplicationID)
        {
            bool _isFound = false;

            string query = "SELECT found = 1 FROM Applications WHERE ApplicationID = @ApplicationID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

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

        public static bool CancelApplication(int ApplicationID)
        {
            int AffectedRows = 0;
            string query = @"UPDATE Applications
                             SET ApplicationStatus = @ApplicationStatus
                             WHERE ApplicationID = @ApplicationID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@ApplicationStatus", 2);

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
        
        public static bool UpdateStatus(int ApplicationID,int NewStatus)
        {
            int AffectedRows = 0;
            string query = @"UPDATE Applications
                             SET ApplicationStatus = @ApplicationStatus,
                                 LastStatusDate = @LastStatusDate
                             WHERE ApplicationID = @ApplicationID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@ApplicationStatus", NewStatus);
            command.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);

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
        
        public static DataTable GetAllApplications()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT * FROM Applications";

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
