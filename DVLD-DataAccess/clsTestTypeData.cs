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
     [TestTypeID]
     [TestTypeTitle]
     [TestTypeDescription]
     [TestTypeFees]
     [dbo].[TestTypes]
     */
    public class clsTestTypeData
    {
        public static bool GetTestTypeInfoByID(int TestTypeID, ref string TestTypeTitle,ref string TestTypeDescription,ref float TestTypeFees)
        {
            bool _isFound = false;

            string query = @"SELECT * FROM TestTypes WHERE TestTypeID = @TestTypeID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    _isFound = true;

                    TestTypeTitle = (string)reader["TestTypeTitle"];
                    TestTypeDescription = (string)(reader["TestTypeDescription"]);
                    TestTypeFees = Convert.ToSingle(reader["TestTypeFees"]);

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

        public static bool GetTestTypeInfoByTitle(ref int TestTypeID,string TestTypeTitle, ref string TestTypeDescription, ref float TestTypeFees)
        {
            bool _isFound = false;

            string query = @"SELECT * FROM TestTypes WHERE TestTypeTitle = @TestTypeTitle";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    _isFound = true;

                    TestTypeID = (int)reader["TestTypeID"];
                    TestTypeDescription = (string)(reader["TestTypeDescription"]);
                    TestTypeFees = Convert.ToSingle(reader["TestTypeFees"]);

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
        
        public static int AddNewTestType(string TestTypeTitle, string TestTypeDescription, float TestTypeFees)
        {
            int TestTypeID = -1;

            string query = @"INSERT INTO TestTypes
                               (TestTypeTitle
                               ,TestTypeDescription
                               ,TestTypeFees
                               )
                         VALUES
                               (@TestTypeTitle
                               ,@TestTypeDescription
                               ,@TestTypeFees
                               );
                                SELECT SCOPE_IDENTITY();";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
            command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
            command.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);
            

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null && (int.TryParse(result.ToString(), out int ID)))
                {
                    TestTypeID = ID;
                }
            }
            catch (Exception ex)
            {

            }
            finally { connection.Close(); }

            return TestTypeID;
        }
        
        public static bool UpdateTestType(int TestTypeID, string TestTypeTitle, string TestTypeDescription, float TestTypeFees)
        {
            int AffectedRows = 0;

            string query = @"UPDATE TestTypes
                           SET TestTypeTitle = @TestTypeTitle
                              ,TestTypeDescription = @TestTypeDescription
                              ,TestTypeFees = @TestTypeFees
                           WHERE TestTypeID = @TestTypeID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
            command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
            command.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

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
        
        public static bool DeleteTestType(int TestTypeID)
        {
            int AffectedRows = 0;

            string query = @"DELETE FROM TestTypes
                                WHERE TestTypeID =@TestTypeID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

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

        public static bool IsTestTypeExist(int TestTypeID)
        {
            bool _isFound = false;

            string query = "SELECT found = 1 FROM TestTypes WHERE TestTypeID = @TestTypeID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

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

        public static DataTable GetAllTestTypes()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT * FROM TestTypes";

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
