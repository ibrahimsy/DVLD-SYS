using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public static class clsCountryData
    {

        public static bool GetCountryByID(int CountryId,ref string CountryName)
        {
            bool _isFound = false;

            string query = @"SELECT CountryName
                             FROM Countries
                             WHERE CountryID = @CountryId";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CountryId", CountryId);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    _isFound = true;

                    CountryName = (string)reader["CountryName"];
                   
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
            return _isFound;
        }

        public static bool GetCountryByName(string CountryName,ref int CountryId)
        {
            bool _isFound = false;

            string query = @"SELECT CountryID
                             FROM Countries
                             WHERE CountryName = @CountryName";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    _isFound = true;

                    CountryId = (int)reader["CountryID"];

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
            return _isFound;
        }

        public static DataTable GetAllCountries()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT * FROM Countries";

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
