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



    public class clsSubModelData
    {
        public static int AddSubModel(int ModelID, string SubModelName)
        {
            int _SubModelID = -1;
            string query = @"INSERT INTO SubModels(
                            ModelID,
                            SubModelName
                            ) VALUES (
                            @ModelID,
                            @SubModelName
                            );
                            SELECT SCOPE_IDENTITY();";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ModelID", ModelID);
            command.Parameters.AddWithValue("@SubModelName", SubModelName);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int Id))
                {
                    _SubModelID = Id;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return _SubModelID;
        }



        public static bool UpdateSubModelByID(int SubModelID, int ModelID, string SubModelName)
        {


            int AffectedRows = 0;

            string query = @"UPDATE SubModels SET 
                            SubModelID = @SubModelID,
                            ModelID = @ModelID,
                            SubModelName = @SubModelName
                            WHERE SubModelID = @SubModelID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@SubModelID", SubModelID);
            command.Parameters.AddWithValue("@ModelID", ModelID);
            command.Parameters.AddWithValue("@SubModelName", SubModelName);

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



        public static bool DeleteSubModelByID(int SubModelID)
        {


            int AffectedRows = 0;

            string query = @"DELETE FROM SubModels
                                     WHERE SubModelID = @SubModelID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@SubModelID", SubModelID);

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



        public static bool GetSubModelByID( int SubModelID, ref int ModelID, ref string SubModelName)
        {

            bool IsFound = false;

            string query = "SELECT * FROM SubModels WHERE SubModelID = @SubModelID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@SubModelID", SubModelID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    SubModelID = (int)reader["SubModelID"];
                    ModelID = (int)reader["ModelID"];
                    SubModelName = (string)reader["SubModelName"];

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





        public static bool IsSubModelExistBySubModelID(int SubModelID)
        {
            bool IsFound = false;

            string query = @"SELECT found = 1 FROM SubModels WHERE SubModelID = @SubModelID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@SubModelID", SubModelID);

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





        public static DataTable GetAllSubModels()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT * FROM SubModels ORDER BY SubModelID DESC";

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


        public static DataTable GetAllSubModelsByModelName(string ModelName)
        {
            DataTable dt = new DataTable();

            string query = @"SELECT      MakeModels.ModelName, SubModels.SubModelName
                             FROM        SubModels INNER JOIN
                                         MakeModels ON SubModels.ModelID = MakeModels.ModelID
			                             WHERE ModelName = @ModelName";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ModelName", ModelName);
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
