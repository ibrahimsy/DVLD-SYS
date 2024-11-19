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
     LicenseClassID
     ClassName
     ClassDescription
     MinimumAllowedAge
     DefaultValidityLength
     ClassFees
     From LicenseClasses
     */
    public class clsLicenseClassData
    {
        public static bool GetLicenseClassInfoByID(int LicenseClassID, ref string ClassName, ref string ClassDescription, ref short MinimumAllowedAge,
                                               ref short DefaultValidityLength, ref float ClassFees)
        {
            bool _isFound = false;

            string query = @"SELECT * FROM LicenseClasses WHERE LicenseClassID = @LicenseClassID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    _isFound = true;

                    ClassName = (string)reader["ClassName"];
                    ClassDescription = (string)(reader["ClassDescription"]);
                    MinimumAllowedAge = Convert.ToInt16(reader["MinimumAllowedAge"]);
                    DefaultValidityLength = Convert.ToInt16(reader["DefaultValidityLength"]);
                    ClassFees = Convert.ToSingle(reader["ClassFees"]);

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

        public static bool GetLicenseClassInfoByClassName(ref int LicenseClassID, string ClassName, ref string ClassDescription, ref short MinimumAllowedAge,
                                               ref short DefaultValidityLength, ref float ClassFees)
        {
            bool _isFound = false;

            string query = @"SELECT * FROM LicenseClasses WHERE ClassName = @ClassName";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);
        
            command.Parameters.AddWithValue("@ClassName", ClassName);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    _isFound = true;

                    LicenseClassID = (int)reader["LicenseClassID"];
                    ClassDescription = (string)(reader["ClassDescription"]);
                    MinimumAllowedAge = Convert.ToInt16(reader["MinimumAllowedAge"]);
                    DefaultValidityLength = Convert.ToInt16(reader["DefaultValidityLength"]);
                    ClassFees = Convert.ToInt16(reader["ClassFees"]);

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

        public static int AddNewLicenseClass(string ClassName, string ClassDescription,short MinimumAllowedAge,
                                              short DefaultValidityLength,float ClassFees)
        {
            int LicenseClassID = -1;

            string query = @"INSERT INTO LicenseClasses
                               (ClassName
                               ,ClassDescription
                               ,MinimumAllowedAge
                               ,DefaultValidityLength
                               ,ClassFees )
                         VALUES
                               (@ClassName
                               ,@ClassDescription
                               ,@MinimumAllowedAge
                               ,@DefaultValidityLength
                               ,@ClassFees);
                                SELECT SCOPE_IDENTITY();";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ClassName", ClassName);
            command.Parameters.AddWithValue("@ClassDescription", ClassDescription);
            command.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
            command.Parameters.AddWithValue("@DefaultValidityLength", DefaultValidityLength);
            command.Parameters.AddWithValue("@ClassFees", ClassFees);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null && (int.TryParse(result.ToString(), out int ID)))
                {
                    LicenseClassID = ID;
                }
            }
            catch (Exception ex)
            {

            }
            finally { connection.Close(); }

            return LicenseClassID;
        }
        public static bool UpdateLicenseClass(int LicenseClassID, string ClassName, string ClassDescription, short MinimumAllowedAge,
                                              short DefaultValidityLength, float ClassFees)
        {
            int AffectedRows = 0;

            string query = @"UPDATE LicenseClasses
                           SET ClassName = @ClassName
                              ,ClassDescription = @ClassDescription
                              ,MinimumAllowedAge = @MinimumAllowedAge
                              ,DefaultValidityLength = @DefaultValidityLength
                              ,ClassFees = @ClassFees
                           WHERE LicenseClassID = @LicenseClassID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ClassName", ClassName);
            command.Parameters.AddWithValue("@ClassDescription", ClassDescription);
            command.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
            command.Parameters.AddWithValue("@DefaultValidityLength", DefaultValidityLength);
            command.Parameters.AddWithValue("@ClassFees", ClassFees);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
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
        public static bool DeleteLicenseClass(int LicenseClassID)
        {
            int AffectedRows = 0;

            string query = @"DELETE FROM LicenseClasses
                                WHERE LicenseClassID =@LicenseClassID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

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

        public static bool IsLicenseClassExist(int LicenseClassID)
        {
            bool _isFound = false;

            string query = "SELECT found = 1 FROM LicenseClasses WHERE LicenseClassID = @LicenseClassID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

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

        public static DataTable GetAllLicenseClasses()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT * FROM LicenseClasses";

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
