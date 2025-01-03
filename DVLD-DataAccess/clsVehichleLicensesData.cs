using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;
using static System.Net.Mime.MediaTypeNames;

namespace BankDataAccess
{



    public class clsVehichleLicenseData
    {
        public static int AddVehichleLicense(int ApplicationID,int VehichleID, DateTime IssuedDate, DateTime ExpiryDate,Decimal LicenseFee, byte Status,byte IssueReason, int CreatedBy)
        {
            int _VehichleLicenseID = -1;
            string query = @"INSERT INTO VehichleLicenses(
                            ApplicationID,
                            VehichleID,
                            IssuedDate,
                            ExpiryDate,
                            LicenseFee,
                            Status,
                            IssueReason,
                            CreatedBy
                            ) VALUES (
                            @ApplicationID,
                            @VehichleID,
                            @IssuedDate,
                            @ExpiryDate,
                            @LicenseFee,
                            @Status,
                            @IssueReason,
                            @CreatedBy
                            );
                             SELECT SCOPE_IDENTITY();";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@VehichleID", VehichleID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@IssuedDate", IssuedDate);
            command.Parameters.AddWithValue("@ExpiryDate", ExpiryDate);
            command.Parameters.AddWithValue("@LicenseFee", LicenseFee);
            command.Parameters.AddWithValue("@Status", Status);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
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

        public static bool UpdateVehichleLicenseByID(int VehichleLicenseID,int ApplicationID,int VehichleID, DateTime IssuedDate, DateTime ExpiryDate,Decimal LicenseFee, byte Status, byte IssueReason, int CreatedBy)
        {


            int AffectedRows = 0;

            string query = @"UPDATE VehichleLicenses SET 
                                VehichleLicenseID = @VehichleLicenseID,
                                ApplicationID = @ApplicationID,
                                VehichleID = @VehichleID,
                                IssuedDate = @IssuedDate,
                                ExpiryDate = @ExpiryDate,
                                LicenseFee = @LicenseFee,
                                Status = @Status,
                                IssueReason = @IssueReason,
                                CreatedBy = @CreatedBy
                                WHERE VehichleLicenseID = @VehichleLicenseID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@VehichleLicenseID", VehichleLicenseID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@VehichleID", VehichleID);
            command.Parameters.AddWithValue("@IssuedDate", IssuedDate);
            command.Parameters.AddWithValue("@ExpiryDate", ExpiryDate);
            command.Parameters.AddWithValue("@LicenseFee", LicenseFee);
            command.Parameters.AddWithValue("@Status", Status);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
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

        public static bool GetVehichleLicenseByID(int VehichleLicenseID,ref int ApplicationID, ref int VehichleID, ref DateTime IssuedDate, ref DateTime ExpiryDate,ref Decimal LicenseFee, ref byte Status,ref byte IssueReason, ref int CreatedBy)
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
                    ApplicationID = (int)reader["ApplicationID"];
                    IssuedDate = (DateTime)reader["IssuedDate"];
                    ExpiryDate = (DateTime)reader["ExpiryDate"];
                    LicenseFee = Convert.ToDecimal( reader["LicenseFee"]);
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

        public static int GetVehicleLicenseIDByVehicleID(int VehicleID)
        {
            int VehicleLicenseID = -1;

            string query = @"SELECT TOP 1 VehichleLicenseID 
                             FROM VehichleLicenses 
                             WHERE VehichleID = @VehicleID AND Status = 1 ORDER BY VehichleLicenseID DESC";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@VehicleID", VehicleID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(),out int ID))
                {
                    VehicleLicenseID = ID;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                connection.Close();
            }
            return VehicleLicenseID;
        }

        public static bool GetVehicleLicenseByVehicleID(int VehicleID,ref int VehichleLicenseID, ref int ApplicationID, ref DateTime IssuedDate, ref DateTime ExpiryDate, ref Decimal LicenseFee, ref byte Status, ref byte IssueReason, ref int CreatedBy)
        {
            bool IsFound = false;

            string query = @"SELECT TOP 1 * 
                             FROM VehichleLicenses 
                             WHERE VehichleID = @VehicleID AND Status = 1 ORDER BY VehichleLicenseID DESC";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@VehicleID", VehicleID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    VehichleLicenseID = (int)reader["VehichleLicenseID"];
                    ApplicationID = (int)reader["ApplicationID"];
                    IssuedDate = (DateTime)reader["IssuedDate"];
                    ExpiryDate = (DateTime)reader["ExpiryDate"];
                    LicenseFee = Convert.ToDecimal(reader["LicenseFee"]);
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

        public static bool DeactivateVehicleLicense(int VehicleLicenseID)
        {

            int AffectedRows = 0;

            string query = @"UPDATE VehichleLicenses
                             SET Status = 2
                             WHERE VehichleLicenseID = @VehicleLicenseID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@VehicleLicenseID", VehicleLicenseID);
           
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

        public static bool UpdateStatus(byte Status,int VehichleLicenseID)
        {
            int AffectedRows = 0;

            string query = @"UPDATE VehichleLicenses SET 
                                Status = @Status
                                WHERE VehichleLicenseID = @VehichleLicenseID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Status", Status);
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
            finally
            {
                connection.Close();
            }

            return AffectedRows > 0;
        }

    }
}
