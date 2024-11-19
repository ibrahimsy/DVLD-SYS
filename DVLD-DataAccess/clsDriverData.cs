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
     DriverID
     PersonID
     CreatedByUserID
     CreatedDate
     */
    public class clsDriverData
    {
        public static bool GetDriverInfoByDriverID(int DriverID, ref int PersonID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool _isFound = false;

            string query = @"SELECT * FROM Drivers WHERE DriverID = @DriverID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    _isFound = true;

                    PersonID = (int)reader["PersonID"];
                    CreatedByUserID = (int)(reader["CreatedByUserID"]);
                    CreatedDate = (DateTime)reader["CreatedDate"];
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

        public static bool GetDriverInfoByPersonID(int PersonID, ref int DriverID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool _isFound = false;

            string query = @"SELECT * FROM Drivers WHERE PersonID = @PersonID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    _isFound = true;

                    DriverID = (int)reader["DriverID"];
                    CreatedByUserID = (int)(reader["CreatedByUserID"]);
                    CreatedDate = (DateTime)reader["CreatedDate"];
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
        public static int AddNewDriver(int PersonID,  int CreatedByUserID,  DateTime CreatedDate)
        {
            int DriverID = -1;

            string query = @"INSERT INTO Drivers
                                   (PersonID
                                   ,CreatedByUserID
                                   ,CreatedDate)
                             VALUES
                                   (@PersonID
                                   ,@CreatedByUserID
                                   ,@CreatedDate);
                                SELECT SCOPE_IDENTITY();";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null && (int.TryParse(result.ToString(), out int ID)))
                {
                    DriverID = ID;
                }
            }
            catch (Exception ex)
            {

            }
            finally { connection.Close(); }

            return DriverID;
        }
        public static bool UpdateDriver(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            int AffectedRows = 0;

            string query = @"UPDATE Drivers
                               SET PersonID = @PersonID
                                  ,CreatedByUserID = @CreatedByUserID
                                  ,CreatedDate = @CreatedDate
                             WHERE DriverID = @DriverID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);

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
        public static bool DeleteDriver(int DriverID)
        {
            int AffectedRows = 0;

            string query = @"DELETE FROM Drivers
                                WHERE DriverID =@DriverID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);

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

        public static bool IsDriverExist(int DriverID)
        {
            bool _isFound = false;

            string query = "SELECT found = 1 FROM Drivers WHERE DriverID = @DriverID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);

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

        public static DataTable GetAllDrivers()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT        
                            Drivers.DriverID,
                            Drivers.PersonID,
                            People.NationalNo,
                            (People.FirstName +' '+ People.SecondName+' '+ISNULL( People.ThirdName,'')+' '+People.LastName) As FullName,
                            Drivers.CreatedDate,
                            (SELECT COUNT(Licenses.LicenseID) FROM Licenses  
	                            WHERE Licenses.DriverID = Drivers.DriverID And Licenses.IsActive = 1 ) AS ActiveLicenses
                            FROM            Drivers INNER JOIN
                                            People ON Drivers.PersonID = People.PersonID
                            ORDER BY FullName";

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
