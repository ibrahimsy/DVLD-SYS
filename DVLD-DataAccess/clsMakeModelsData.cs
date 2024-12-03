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



    public class clsMakeModelData
    {
        public static int AddMakeModel( int MakeID, string ModelName)
        {
            int _ModelID = -1;
            string query = @"INSERT INTO MakeModels(
                            MakeID,
                            ModelName
                            ) VALUES (
                            @MakeID,
                            @ModelName
                             );
                            SELECT SCOPE_IDENTITY();";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MakeID", MakeID);
            command.Parameters.AddWithValue("@ModelName", ModelName);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int Id))
                {
                    _ModelID = Id;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return _ModelID;
        }



        public static bool UpdateMakeModelByID(int ModelID, int MakeID, string ModelName)
        {


            int AffectedRows = 0;

            string query = @"UPDATE MakeModels SET 
                            ModelID = @ModelID,
                            MakeID = @MakeID,
                            ModelName = @ModelName,
                            WHERE ModelID = @ModelID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ModelID", ModelID);
            command.Parameters.AddWithValue("@MakeID", MakeID);
            command.Parameters.AddWithValue("@ModelName", ModelName);

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



        public static bool DeleteMakeModelByID(int ModelID)
        {


            int AffectedRows = 0;

            string query = @"DELETE FROM MakeModels
                                     WHERE ModelID = @ModelID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ModelID", ModelID);

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



        public static bool GetMakeModelByID(int ModelID, ref int MakeID, ref string ModelName)
        {

            bool IsFound = false;

            string query = "SELECT * FROM MakeModels WHERE ModelID = @ModelID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ModelID", ModelID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    ModelID = (int)reader["ModelID"];
                    MakeID = (int)reader["MakeID"];
                    ModelName = (string)reader["ModelName"];

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





        public static bool IsMakeModelExistByModelID(int ModelID)
        {
            bool IsFound = false;

            string query = @"SELECT found = 1 FROM MakeModels WHERE ModelID = @ModelID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ModelID", ModelID);

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





        public static DataTable GetAllMakeModels()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT * FROM MakeModels ORDER BY ModelID DESC";

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
