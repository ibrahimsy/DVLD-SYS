using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace BankDataAccess
{



    public class clsVehichleLicenseData
    {
        public static int AddVehichleLicense(int VehichleID, DateTime IssuedDate, DateTime ExpiryDate, byte Status, int CreatedBy)
        {
            int _VehichleLicenseID = -1;
            string query = @"INSERT INTO VehichleLicenses(
                            VehichleID,
                            IssuedDate,
                            ExpiryDate,
                            Status,
                            CreatedBy
                            ) VALUES (
                            @VehichleID,
                            @IssuedDate,
                            @ExpiryDate,
                            @Status,
                            @CreatedBy
                            );
                             SELECT SCOPE_IDENTITY();";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@VehichleID", VehichleID);
            command.Parameters.AddWithValue("@IssuedDate", IssuedDate);
            command.Parameters.AddWithValue("@ExpiryDate", ExpiryDate);
            command.Parameters.AddWithValue("@Status", Status);
            command.Parameters.AddWithValue("@CreatedBy", CreatedBy);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int Id))
                {
                    _VehichleLicenseID = Id;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return _VehichleLicenseID;
        }



        public static bool UpdateVehichleLicenseByID(int VehichleLicenseID, int VehichleID, DateTime IssuedDate, DateTime ExpiryDate, byte Status, int CreatedBy)
        {


            int AffectedRows = 0;

            string query = @"UPDATE VehichleLicenses SET 
                                VehichleLicenseID = @VehichleLicenseID,
                                VehichleID = @VehichleID,
                                IssuedDate = @IssuedDate,
                                ExpiryDate = @ExpiryDate,
                                Status = @Status,
                                CreatedBy = @CreatedBy
                                WHERE VehichleLicenseID = @VehichleLicenseID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@VehichleLicenseID", VehichleLicenseID);
            command.Parameters.AddWithValue("@VehichleID", VehichleID);
            command.Parameters.AddWithValue("@IssuedDate", IssuedDate);
            command.Parameters.AddWithValue("@ExpiryDate", ExpiryDate);
            command.Parameters.AddWithValue("@Status", Status);
            command.Parameters.AddWithValue("@CreatedBy", CreatedBy);

            try
            {
                connection.Open();

                AffectedRows = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            return AffectedRows > 0;
        }



        public static bool DeleteVehichleLicenseByID(int VehichleLicenseID)
        {


            int AffectedRows = 0;

            string query = @"DELETE FROM VehichleLicenses
                                     WHERE VehichleLicenseID = @VehichleLicenseID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@VehichleLicenseID", VehichleLicenseID);

            try
            {
                connection.Open();

                AffectedRows = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return false;
            }
            finally { connection.Close(); }


            return AffectedRows > 0;
        }



        public static bool GetVehichleLicenseByID(int VehichleLicenseID, ref int VehichleID, ref DateTime IssuedDate, ref DateTime ExpiryDate, ref byte Status, ref int CreatedBy)
        {

            bool IsFound = false;

            string query = "SELECT * FROM VehichleLicenses WHERE VehichleLicenseID = @VehichleLicenseID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@VehichleLicenseID", VehichleLicenseID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    VehichleID = (int)reader["VehichleID"];
                    IssuedDate = (DateTime)reader["IssuedDate"];
                    ExpiryDate = (DateTime)reader["ExpiryDate"];
                    Status = (byte)reader["Status"];
                    CreatedBy = (int)reader["CreatedBy"];

                }
                reader.Close();
            }
            catch (Exception ex)
            {
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }


            return IsFound;
        }





        public static bool IsVehichleLicenseExistByVehichleLicenseID(int VehichleLicenseID)
        {
            bool IsFound = false;

            string query = @"SELECT found = 1 FROM VehichleLicenses WHERE VehichleLicenseID = @VehichleLicenseID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@VehichleLicenseID", VehichleLicenseID);

            try
            {
                connection.Open();
                object result = command.ExecuteNonQuery();
                if (result != null)
                {
                    IsFound = true;
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
            return IsFound;
        }





        public static DataTable GetAllVehichleLicenses()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT * FROM VehichleLicenses ORDER BY VehichleLicenseID DESC";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
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
            return dt;
        }


    }
}
