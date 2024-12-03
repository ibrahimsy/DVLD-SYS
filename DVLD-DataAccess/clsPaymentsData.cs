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



    public class clsPaymentData
    {
        public static int AddPayment(int VehichleLicenseID, decimal Amount, DateTime PaymentDate, byte ServiceType, int CreatedBy)
        {
            int _PaymentID = -1;
            string query = @"INSERT INTO Payments(
                            VehichleLicenseID,
                            Amount,
                            PaymentDate,
                            ServiceType,
                            CreatedBy
                            ) VALUES (
                            @VehichleLicenseID,
                            @Amount,
                            @PaymentDate,
                            @ServiceType,
                            @CreatedBy
                            );
                            SELECT SCOPE_IDENTITY();";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@VehichleLicenseID", VehichleLicenseID);
            command.Parameters.AddWithValue("@Amount", Amount);
            command.Parameters.AddWithValue("@PaymentDate", PaymentDate);
            command.Parameters.AddWithValue("@ServiceType", ServiceType);
            command.Parameters.AddWithValue("@CreatedBy", CreatedBy);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int Id))
                {
                    _PaymentID = Id;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return _PaymentID;
        }



        public static bool UpdatePaymentByID(int PaymentID, int VehichleLicenseID, decimal Amount, DateTime PaymentDate, byte ServiceType, int CreatedBy)
        {


            int AffectedRows = 0;

            string query = @"UPDATE Payments SET 
                                PaymentID = @PaymentID,
                                VehichleLicenseID = @VehichleLicenseID,
                                Amount = @Amount,
                                PaymentDate = @PaymentDate,
                                ServiceType = @ServiceType,
                                CreatedBy = @CreatedBy
                                WHERE PaymentID = @PaymentID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@VehichleLicenseID", VehichleLicenseID);
            command.Parameters.AddWithValue("@Amount", Amount);
            command.Parameters.AddWithValue("@PaymentDate", PaymentDate);
            command.Parameters.AddWithValue("@ServiceType", ServiceType);
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



        public static bool DeletePaymentByID(int PaymentID)
        {


            int AffectedRows = 0;

            string query = @"DELETE FROM Payments
                                     WHERE PaymentID = @PaymentID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

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
            finally { connection.Close(); }


            return AffectedRows > 0;
        }



        public static bool GetPaymentByID(int PaymentID, ref int VehichleLicenseID, ref decimal Amount, ref DateTime PaymentDate, ref byte ServiceType, ref int CreatedBy)
        {

            bool IsFound = false;

            string query = "SELECT * FROM Payments WHERE PaymentID = @PaymentID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PaymentID", PaymentID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    VehichleLicenseID = (int)reader["VehichleLicenseID"];
                    Amount = (decimal)reader["Amount"];
                    PaymentDate = (DateTime)reader["PaymentDate"];
                    ServiceType = (byte)reader["ServiceType"];
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





        public static bool IsPaymentExistByPaymentID(int PaymentID)
        {
            bool IsFound = false;

            string query = @"SELECT found = 1 FROM Payments WHERE PaymentID = @PaymentID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PaymentID", PaymentID);

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





        public static DataTable GetAllPayments()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT * FROM Payments ORDER BY PaymentID DESC";

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
