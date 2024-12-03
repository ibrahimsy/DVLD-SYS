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



    public class clsMakeData
    {
        public static int AddMake(string Make)
        {
            int _MakeID = -1;
            string query = @"INSERT INTO Makes(
                            Make
                            ) VALUES (
                            @Make
                            );
                            SELECT SCOPE_IDENTITY();";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Make", Make);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int Id))
                {
                    _MakeID = Id;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return _MakeID;
        }



        public static bool UpdateMakeByID(int MakeID, string Make)
        {


            int AffectedRows = 0;

            string query = @"UPDATE Makes SET 
                            MakeID = @MakeID,
                            Make = @Make
                            WHERE MakeID = @MakeID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@MakeID", MakeID);
            command.Parameters.AddWithValue("@Make", Make);

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



        public static bool DeleteMakeByID(int MakeID)
        {


            int AffectedRows = 0;

            string query = @"DELETE FROM Makes
                                     WHERE MakeID = @MakeID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@MakeID", MakeID);

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



        public static bool GetMakeByID(int MakeID, ref string Make)
        {

            bool IsFound = false;

            string query = "SELECT * FROM Makes WHERE MakeID = @MakeID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@MakeID", MakeID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    MakeID = (int)reader["MakeID"];
                    Make = (string)reader["Make"];

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





        public static bool IsMakeExistByMakeID(int MakeID)
        {
            bool IsFound = false;

            string query = @"SELECT found = 1 FROM Makes WHERE MakeID = @MakeID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@MakeID", MakeID);

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





        public static DataTable GetAllMakes()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT * FROM Makes ORDER BY MakeID DESC";

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
