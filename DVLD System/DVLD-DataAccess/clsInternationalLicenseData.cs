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
       int InternationalLicenseID
      ,int ApplicationID
      ,int DriverID
      ,int IssuedUsingLocalLicenseID
      ,DateTimeIssueDate
      ,DateTime ExpirationDate
      ,byte IsActive
      ,int CreatedByUserID
     */
    public class clsInternationalLicenseData
    {
        public static bool GetInternationalLicenseInfoByID(int InternationalLicenseID
                                                          ,ref int ApplicationID
                                                          ,ref int DriverID
                                                          , ref int IssuedUsingLocalLicenseID
                                                          ,ref DateTime IssueDate
                                                          ,ref DateTime ExpirationDate
                                                          ,ref byte IsActive
                                                          ,ref int CreatedByUserID)
        {
            bool _isFound = false;

            string query = @"SELECT * FROM InternationalLicenses WHERE InternationalLicenseID = @InternationalLicenseID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);


            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    _isFound = true;

                   ApplicationID = (int)reader["ApplicationID"];
                   DriverID = (int)reader["DriverID"];
                   IssuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];
                   IssueDate = (DateTime)reader["IssueDate"];
                   ExpirationDate = (DateTime)reader["ExpirationDate"];
                   IsActive = (byte)reader["IsActive"];
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

        public static int AddNewInternationalLicense( int ApplicationID
                                                      ,int DriverID
                                                      , int IssuedUsingLocalLicenseID
                                                      , DateTime IssueDate
                                                      , DateTime ExpirationDate
                                                      , byte IsActive
                                                      , int CreatedByUserID)
        {
            int InternationalLicenseID = -1;

            string query = @"INSERT INTO InternationalLicenses
                               (ApplicationID
                               ,DriverID
                               ,IssuedUsingLocalLicenseID
                               ,IssueDate
                               ,ExpirationDate
                               ,IsActive
                               ,CreatedByUserID)
                         VALUES
                               (@ApplicationID
                               ,@DriverID
                               ,@IssuedUsingLocalLicenseID
                               ,@IssueDate
                               ,@ExpirationDate
                               ,@IsActive
                               ,@CreatedByUserID);
                                SELECT SCOPE_IDENTITY();";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null && (int.TryParse(result.ToString(), out int ID)))
                {
                    InternationalLicenseID = ID;
                }
            }
            catch (Exception ex)
            {

            }
            finally { connection.Close(); }

            return InternationalLicenseID;
        }
        public static bool UpdateInternationalLicense(  int InternationalLicenseID
                                                      , int ApplicationID
                                                      , int DriverID
                                                      , int IssuedUsingLocalLicenseID
                                                      , DateTime IssueDate
                                                      , DateTime ExpirationDate
                                                      , byte IsActive
                                                      , int CreatedByUserID)
        {
            int AffectedRows = 0;

            string query = @"UPDATE InternationalLicenses
                           SET ApplicationID = @ApplicationID
                              ,DriverID = @DriverID
                              ,IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID
                              ,IssueDate = @IssueDate
                              ,ExpirationDate = @ExpirationDate
                              ,IsActive = @IsActive
                              ,CreatedByUserID = @CreatedByUserID
                         WHERE InternationalLicenseID = @InternationalLicenseID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
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
        public static bool DeleteInternationalLicense(int InternationalLicenseID)
        {
            int AffectedRows = 0;

            string query = @"DELETE FROM InternationalLicenses
                                WHERE InternationalLicenseID =@InternationalLicenseID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

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

        public static bool IsInternationalLicenseExist(int InternationalLicenseID)
        {
            bool _isFound = false;

            string query = "SELECT found = 1 FROM InternationalLicenses WHERE InternationalLicenseID = @InternationalLicenseID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

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

        public static DataTable GetAllInternationalLicenses()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT * FROM InternationalLicenses";

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
