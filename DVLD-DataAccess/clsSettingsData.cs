using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_DataAccess
{



    public class clsSettingData
    {
        public static int AddSetting(string SettingName, byte SettingValue, string SettingDescription)
        {
            int _SettingID = -1;
            string query = @"INSERT INTO Settings(
                            SettingID,
                            SettingName,
                            SettingValue,
                            SettingDescription,
                            ) VALUES (
                            @SettingID,
                            @SettingName,
                            @SettingValue,
                            @SettingDescription,
                            );
                            SELECT SCOPE_IDENTITY();";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@SettingName", SettingName);
            command.Parameters.AddWithValue("@SettingValue", SettingValue);
            command.Parameters.AddWithValue("@SettingDescription", SettingDescription);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int Id))
                {
                    _SettingID = Id;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return _SettingID;
        }



        public static bool UpdateSettingByID(int SettingID, string SettingName, byte SettingValue, string SettingDescription)
        {


            int AffectedRows = 0;

            string query = @"UPDATE Settings SET 
                            SettingName = @SettingName,
                            SettingValue = @SettingValue,
                            SettingDescription = @SettingDescription,
                            WHERE SettingID = @SettingID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@SettingName", SettingName);
            command.Parameters.AddWithValue("@SettingValue", SettingValue);
            command.Parameters.AddWithValue("@SettingDescription", SettingDescription);

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



        public static bool DeleteSettingByID(int SettingID)
        {


            int AffectedRows = 0;

            string query = @"DELETE FROM Settings
                                     WHERE SettingID = @SettingID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@SettingID", SettingID);

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



        public static bool GetSettingByID(int SettingID, ref string SettingName, ref byte SettingValue, ref string SettingDescription)
        {

            bool IsFound = false;

            string query = "SELECT * FROM Settings WHERE SettingID = @SettingID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@SettingID", SettingID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    SettingName = (string)reader["SettingName"];
                    SettingValue = (byte)reader["SettingValue"];
                    SettingDescription = (string)reader["SettingDescription"];

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





        public static bool IsSettingExistBySettingID(int SettingID)
        {
            bool IsFound = false;

            string query = @"SELECT found = 1 FROM Settings WHERE SettingID = @SettingID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@SettingID", SettingID);

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





        public static DataTable GetAllSettings()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT * FROM Settings";

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
