using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsApplicationTypesData
    {
        /*
         [ApplicationTypeID]
         [ApplicationTypeTitle]
         [ApplicationFees]
         
         */
        public static bool GetTypeInfoByID(int TypeID,ref string Tilte,ref float Fees)
        {
            bool _isFound = false;

            string query = @"SELECT *
                             FROM ApplicationTypes
                             WHERE ApplicationTypeID = @TypeID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TypeID", TypeID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    _isFound = true;

                    Tilte = (string)reader["ApplicationTypeTitle"];
                    Fees = Convert.ToSingle(reader["ApplicationFees"]);
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

        public static int AddNewApplicationType(string Tilte,  float Fees)
        {
            int ApplicationTypeID = -1;
            string query = @"INSERT INTO ApplicationTypes
                               (ApplicationTypeTitle,ApplicationFees)
                         VALUES
                               (@Tilte,@Fees);
                                SELECT SCOPE_IDENTITY();";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationTypeTitle", Tilte);
            command.Parameters.AddWithValue("@ApplicationFees", Fees);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null && (int.TryParse(result.ToString(), out int ID)))
                {
                    ApplicationTypeID = ID;
                }
            }
            catch (Exception ex)
            {

            }
            finally { connection.Close(); }

            return ApplicationTypeID;
        }

        public static bool UpdateApplicationType(int TypeID,  string Tilte,  float Fees)
        {
            int AffectedRows = 0;

            string query = @"UPDATE ApplicationTypes
                           SET ApplicationTypeTitle = @Tilte
                              ,ApplicationFees = @Fees     
                           WHERE ApplicationTypeID = @TypeID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TypeID", TypeID);
            command.Parameters.AddWithValue("@Tilte", Tilte);
            command.Parameters.AddWithValue("@Fees", Fees);

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

        public static bool DeleteApplicationType(int TypeID)
        {
            int AffectedRows = 0;

            string query = @"DELETE FROM ApplicationTypes
                                WHERE ApplicationTypeID =@TypeID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TypeID", TypeID);

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

        public static bool IsApplicationTypeExist(int TypeID)
        {
            string query = "SELECT found = 1 FROM ApplicationTypes WHERE ApplicationTypeID = @TypeID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TypeID", TypeID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return false;
        }
     
        public static DataTable GetAllApplicationTypes()
        {
            DataTable dt = new DataTable();

            string query = "SELECT * FROM ApplicationTypes";

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
