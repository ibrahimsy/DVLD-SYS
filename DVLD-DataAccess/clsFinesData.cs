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



    public class clsFineData
    {
        public static int AddFine( int VehichleID, int DriverID, DateTime FineDate, byte ViolationType, decimal FineAmount, DateTime DueDate, bool Status, int PaymentID)
        {
            int _FineID = -1;
            string query = @"INSERT INTO Fines(
                            VehichleID,
                            DriverID,
                            FineDate,
                            ViolationType,
                            FineAmount,
                            DueDate,
                            Status,
                            PaymentID
                            ) VALUES (
                            @VehichleID,
                            @DriverID,
                            @FineDate,
                            @ViolationType,
                            @FineAmount,
                            @DueDate,
                            @Status,
                            @PaymentID
                            );
                            SELECT SCOPE_IDENTITY();";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@VehichleID", VehichleID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@FineDate", FineDate);
            command.Parameters.AddWithValue("@ViolationType", ViolationType);
            command.Parameters.AddWithValue("@FineAmount", FineAmount);
            command.Parameters.AddWithValue("@DueDate", DueDate);
            command.Parameters.AddWithValue("@Status", Status);
            command.Parameters.AddWithValue("@PaymentID", PaymentID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int Id))
                {
                    _FineID = Id;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return _FineID;
        }



        public static bool UpdateFineByID(int FineID, int VehichleID, int DriverID, DateTime FineDate, byte ViolationType, decimal FineAmount, DateTime DueDate, bool Status, int PaymentID)
        {


            int AffectedRows = 0;

            string query = @"UPDATE Fines SET 
                            FineID = @FineID,
                            VehichleID = @VehichleID,
                            DriverID = @DriverID,
                            FineDate = @FineDate,
                            ViolationType = @ViolationType,
                            FineAmount = @FineAmount,
                            DueDate = @DueDate,
                            Status = @Status,
                            PaymentID = @PaymentID
                            WHERE FineID = @FineID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@FineID", FineID);
            command.Parameters.AddWithValue("@VehichleID", VehichleID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@FineDate", FineDate);
            command.Parameters.AddWithValue("@ViolationType", ViolationType);
            command.Parameters.AddWithValue("@FineAmount", FineAmount);
            command.Parameters.AddWithValue("@DueDate", DueDate);
            command.Parameters.AddWithValue("@Status", Status);
            command.Parameters.AddWithValue("@PaymentID", PaymentID);

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



        public static bool DeleteFineByID(int FineID)
        {


            int AffectedRows = 0;

            string query = @"DELETE FROM Fines
                                     WHERE FineID = @FineID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@FineID", FineID);

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



        public static bool GetFineByID(int FineID, ref int VehichleID, ref int DriverID, ref DateTime FineDate, ref byte ViolationType, ref decimal FineAmount, ref DateTime DueDate, ref bool Status, ref int PaymentID)
        {

            bool IsFound = false;

            string query = "SELECT * FROM Fines WHERE FineID = @FineID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@FineID", FineID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    VehichleID = (int)reader["VehichleID"];
                    DriverID = (int)reader["DriverID"];
                    FineDate = (DateTime)reader["FineDate"];
                    ViolationType = (byte)reader["ViolationType"];
                    FineAmount = (decimal)reader["FineAmount"];
                    DueDate = (DateTime)reader["DueDate"];
                    Status = (bool)reader["Status"];
                    PaymentID = (int)reader["PaymentID"];

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





        public static bool IsFineExistByFineID(int FineID)
        {
            bool IsFound = false;

            string query = @"SELECT found = 1 FROM Fines WHERE FineID = @FineID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@FineID", FineID);

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





        public static DataTable GetAllFines()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT * FROM Fines ORDER BY FineID DESC";

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
