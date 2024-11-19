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
       int DetainID
      ,int LicenseID
      ,DateTime DetainDate
      ,float FineFees
      ,int CreatedByUserID
      ,byte IsReleased
      ,DateTime ReleaseDate
      ,int ReleasedByUserID
      ,int ReleaseApplicationID
     */
    public class clsDetainedLicenseData
    {
        public static bool GetDetainedLicenseInfoByID(int DetainID
                                                      ,ref int LicenseID
                                                      ,ref DateTime DetainDate
                                                      ,ref float FineFees
                                                      ,ref int CreatedByUserID
                                                      ,ref bool IsReleased
                                                      ,ref DateTime? ReleaseDate
                                                      ,ref int ReleasedByUserID
                                                      ,ref int ReleaseApplicationID)
        {
            bool _isFound = false;

            string query = @"SELECT * FROM DetainedLicenses WHERE DetainedLicenseID = @DetainedLicenseID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DetainID", DetainID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                  _isFound = true;

                    LicenseID = (int)reader["LicenseID"];
                    DetainDate = (DateTime)reader["DetainDate"];
                    FineFees = (float)reader["FineFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsReleased = (bool)reader["IsReleased"];
                    
                    if (reader["ReleaseDate"] == DBNull.Value)
                    {
                        ReleaseDate = null;
                    }
                    else
                    {
                        ReleaseDate = (DateTime)reader["ReleaseDate"];
                    }

                    if (reader["ReleasedByUserID"] == DBNull.Value)
                    {
                        ReleasedByUserID = -1;
                    }
                    else
                    {
                        ReleasedByUserID = (int)reader["ReleasedByUserID"];
                    }

                    if (reader["ReleaseApplicationID"] ==  DBNull.Value)
                    {
                        ReleaseApplicationID = -1;
                    }
                    else
                    {
                        ReleaseApplicationID = (int)reader["ReleaseApplicationID"];
                    }

                    

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

        public static bool GetDetainedLicenseInfoByLicenseID(ref int DetainID
                                                              , int LicenseID
                                                              , ref DateTime DetainDate
                                                              , ref float FineFees
                                                              , ref int CreatedByUserID
                                                              , ref bool IsReleased
                                                              , ref DateTime? ReleaseDate
                                                              , ref int ReleasedByUserID
                                                              , ref int ReleaseApplicationID)
        {
            bool _isFound = false;

            string query = @"SELECT * FROM DetainedLicenses WHERE LicenseID = @LicenseID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    _isFound = true;

                    DetainID = (int)reader["DetainID"];
                    DetainDate = (DateTime)reader["DetainDate"];
                    FineFees = Convert.ToSingle(reader["FineFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsReleased = (bool)reader["IsReleased"];

                    if (reader["ReleaseDate"] == DBNull.Value)
                    {
                        ReleaseDate = null;
                    }
                    else
                    {
                        ReleaseDate = (DateTime)reader["ReleaseDate"];
                    }

                    if (reader["ReleasedByUserID"] == DBNull.Value)
                    {
                        ReleasedByUserID = -1;
                    }
                    else
                    {
                        ReleasedByUserID = (int)reader["ReleasedByUserID"];
                    }

                    if (reader["ReleaseApplicationID"] == DBNull.Value)
                    {
                        ReleaseApplicationID = -1;
                    }
                    else
                    {
                        ReleaseApplicationID = (int)reader["ReleaseApplicationID"];
                    }



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


        public static int AddNewDetainedLicense(int LicenseID
                                                ,DateTime DetainDate
                                                ,float FineFees
                                                ,int CreatedByUserID
                                                ,bool IsReleased
                                                ,DateTime? ReleaseDate
                                                ,int ReleasedByUserID
                                                ,int ReleaseApplicationID)
        {
            int DetainedLicenseID = -1;

            string query = @"INSERT INTO DetainedLicenses
                               (LicenseID
                               ,DetainDate
                               ,FineFees
                               ,CreatedByUserID
                               ,IsReleased
                               ,ReleaseDate
                               ,ReleasedByUserID
                               ,ReleaseApplicationID)
                         VALUES
                               (@LicenseID
                               ,@DetainDate
                               ,@FineFees
                               ,@CreatedByUserID
                               ,@IsReleased
                               ,@ReleaseDate
                               ,@ReleasedByUserID
                               ,@ReleaseApplicationID);
                                SELECT SCOPE_IDENTITY();";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsReleased", IsReleased);
            
            if (ReleaseDate == null)
            {
                command.Parameters.AddWithValue("@ReleaseDate", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
            }

            if (ReleasedByUserID == -1)
            {
                command.Parameters.AddWithValue("@ReleasedByUserID", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@ReleasedByUserID", ReleaseDate);
            }

            if (ReleaseApplicationID == -1)
            {
                command.Parameters.AddWithValue("@ReleaseApplicationID", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
            }
            


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null && (int.TryParse(result.ToString(), out int ID)))
                {
                    DetainedLicenseID = ID;
                }
            }
            catch (Exception ex)
            {

            }
            finally { connection.Close(); }

            return DetainedLicenseID;
        }
        
        public static bool UpdateDetainedLicense(int DetainID
                                                , int LicenseID
                                                , DateTime DetainDate
                                                , float FineFees
                                                , int CreatedByUserID
                                                , bool IsReleased
                                                , DateTime? ReleaseDate
                                                , int ReleasedByUserID
                                                , int ReleaseApplicationID)
        {
            int AffectedRows = 0;

            string query = @"UPDATE DetainedLicenses
                           SET LicenseID = @LicenseID
                              ,DetainDate = @DetainDate
                              ,FineFees = @FineFees
                              ,CreatedByUserID = @CreatedByUserID
                              ,IsReleased = @IsReleased
                              ,ReleaseDate = @ReleaseDate
                              ,ReleasedByUserID = @ReleasedByUserID
                              ,ReleaseApplicationID = @ReleaseApplicationID
                         WHERE DetainID = @DetainID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DetainID", DetainID);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsReleased", IsReleased);

            if (ReleaseDate == null)
            {
                command.Parameters.AddWithValue("@ReleaseDate", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
            }
            
            command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);

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
        
        public static bool DeleteDetainedLicense(int DetainID)
        {
            int AffectedRows = 0;

            string query = @"DELETE FROM DetainedLicenses
                                WHERE DetainID =@DetainID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DetainID", DetainID);

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

        public static bool IsLicenseDetained(int LicenseID)
        {
            bool _isFound = false;

            string query = @"SELECT found = 1 FROM DetainedLicenses
                             WHERE LicenseID = @LicenseID AND IsReleased = 0";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    _isFound = true;
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

        public static DataTable GetAllDetainedLicenses()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT * FROM DetainedLicenses_View 
                            ORDER BY DetainID DESC";

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
