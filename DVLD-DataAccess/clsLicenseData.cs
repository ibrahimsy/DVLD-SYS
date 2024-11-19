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
      int LicenseID
      int ApplicationID
      int DriverID
      int LicenseClass
      DateTime IssueDate
      DateTime ExpirationDate
      string Notes
      float PaidFees
      byte IsActive
      byte IssueReason
      int CreatedByUserID
     */
    public class clsLicenseData
    {
        public static bool GetLicenseInfoByID( int LicenseID
                                              ,ref int ApplicationID
                                              ,ref int DriverID
                                              ,ref int LicenseClass
                                              ,ref DateTime IssueDate
                                              ,ref DateTime ExpirationDate
                                              ,ref string Notes
                                              ,ref float PaidFees
                                              ,ref bool IsActive
                                              ,ref byte IssueReason
                                              , ref int CreatedByUserID)

        {
            bool _isFound = false;

            string query = @"SELECT * FROM Licenses WHERE LicenseID = @LicenseID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    _isFound = true;

                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClass = (int)reader["LicenseClass"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    
                    if (reader["Notes"] == DBNull.Value)
                    {
                        Notes = "";
                    }
                    else
                    {
                        Notes = (string)reader["Notes"];
                    }
                   
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    IsActive = (bool)reader["IsActive"];
                    IssueReason = (byte)reader["IssueReason"];
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

        public static int AddNewLicense(  int ApplicationID
                                        , int DriverID
                                        , int LicenseClass
                                        , DateTime IssueDate
                                        , DateTime ExpirationDate
                                        , string Notes
                                        , float PaidFees
                                        , bool IsActive
                                        , byte IssueReason
                                        , int CreatedByUserID)
        {
            int LicenseID = -1;

            string query = @"INSERT INTO Licenses
                               (ApplicationID
                               ,DriverID
                               ,LicenseClass
                               ,IssueDate
                               ,ExpirationDate
                               ,Notes
                               ,PaidFees
                               ,IsActive
                               ,IssueReason
                               ,CreatedByUserID)
                         VALUES
                               (@ApplicationID
                               ,@DriverID
                               ,@LicenseClass
                               ,@IssueDate
                               ,@ExpirationDate
                               ,@Notes
                               ,@PaidFees
                               ,@IsActive
                               ,@IssueReason
                               ,@CreatedByUserID)
                                SELECT SCOPE_IDENTITY();";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

            if (Notes == "")
            {
                command.Parameters.AddWithValue("@Notes", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@Notes", Notes);
            }
            
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null && (int.TryParse(result.ToString(), out int ID)))
                {
                    LicenseID = ID;
                }
            }
            catch (Exception ex)
            {

            }
            finally { connection.Close(); }

            return LicenseID;
        }
        public static bool UpdateLicense(int LicenseID
                                        , int ApplicationID
                                        , int DriverID
                                        , int LicenseClass
                                        , DateTime IssueDate
                                        , DateTime ExpirationDate
                                        , string Notes
                                        , float PaidFees
                                        , bool IsActive
                                        , byte IssueReason
                                        , int CreatedByUserID)
        {
            int AffectedRows = 0;

            string query = @"UPDATE Licenses
                           SET ApplicationID = @ApplicationID
                              ,DriverID = @DriverID
                              ,LicenseClass = @LicenseClass
                              ,IssueDate = @IssueDate
                              ,ExpirationDate = @ExpirationDate
                              ,Notes = @Notes
                              ,PaidFees = @PaidFees
                              ,IsActive = @IsActive
                              ,IssueReason = @IssueReason
                              ,CreatedByUserID = @CreatedByUserID
                         WHERE LicenseID = @LicenseID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            
            if (Notes == "")
            {
                command.Parameters.AddWithValue("@Notes", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@Notes", Notes);
            }
            
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
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
        public static bool DeleteLicense(int LicenseID)
        {
            int AffectedRows = 0;

            string query = @"DELETE FROM Licenses
                                WHERE LicenseID =@LicenseID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

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

        public static bool IsLicenseExist(int LicenseID)
        {
            bool _isFound = false;

            string query = "SELECT found = 1 FROM Licenses WHERE LicenseID = @LicenseID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

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

        public static DataTable GetAllLicenses()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT * FROM Licenses";

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

        public static DataTable GetAllLocalLicensesByDriverID(int DriverID)
        {
            DataTable dt = new DataTable();

            string query = @"SELECT Licenses.LicenseID, Licenses.ApplicationID, LicenseClasses.ClassName, Licenses.IssueDate, Licenses.ExpirationDate, Licenses.IsActive
                             FROM  Licenses INNER JOIN
                             LicenseClasses ON Licenses.LicenseClass = LicenseClasses.LicenseClassID
		                     WHERE DriverID = @DriverID
                             Order By IsActive ASC";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);

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

        public static DataTable GetAllInternationalLicensesByDriverID(int DriverID)
        {
            DataTable dt = new DataTable();

            string query = @"SELECT InternationalLicenseID, ApplicationID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive
                                FROM  InternationalLicenses
                                WHERE DriverID = @DriverID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);

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

        public static int GetActiveLicensePerPersonId(int PersonID,int LicenseClassID)
        {
            int LicenseID = -1;

            string query = @"SELECT Licenses.LicenseID
                             FROM  Licenses INNER JOIN
                             Drivers ON Licenses.DriverID = Drivers.DriverID
		                     WHERE LicenseClass = @LicenseClassID And Drivers.PersonID = @PersonID
		                     And Licenses.IsActive = 1";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(),out int licenseId))
                {
                    LicenseID = licenseId;
                }

            }catch(Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return LicenseID;
        }

        public static int GetLicenseByPersonId(int PersonID, int LicenseClassID)
        {
            int LicenseID = -1;

            string query = @"SELECT Licenses.LicenseID
                             FROM  Licenses INNER JOIN
                             Drivers ON Licenses.DriverID = Drivers.DriverID
		                     WHERE LicenseClass = @LicenseClassID And Drivers.PersonID = @PersonID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int licenseId))
                {
                    LicenseID = licenseId;
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return LicenseID;
        }
    }
}
