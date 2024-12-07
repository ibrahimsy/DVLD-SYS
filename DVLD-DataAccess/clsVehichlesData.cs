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



    public class clsVehichleData
    {
        public static int AddVehichle( string ChassisNumber, string PlateNumber,int MakeID,int ModelID, int SubModelID, int BodyID, int OwnerID, int Year, string Color, int CreatedBy)
        {
            int _VehichleID = -1;
            string query = @"INSERT INTO Vehichles(
                        ChassisNumber,
                        PlateNumber,
                        MakeID,
                        ModelID,
                        SubModelID,
                        BodyID,
                        OwnerID,
                        Year,
                        Color,
                        CreatedBy
                        ) VALUES (
                        @ChassisNumber,
                        @PlateNumber,
                        @MakeID,
                        @ModelID,
                        @SubModelID,
                        @BodyID,
                        @OwnerID,
                        @Year,
                        @Color,
                        @CreatedBy
                        );
                SELECT SCOPE_IDENTITY();";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ChassisNumber", ChassisNumber);
            command.Parameters.AddWithValue("@PlateNumber", PlateNumber);
            command.Parameters.AddWithValue("@MakeID", MakeID);
            command.Parameters.AddWithValue("@ModelID", ModelID);

            if (SubModelID == -1)
            {
                command.Parameters.AddWithValue("@SubModelID", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@SubModelID", SubModelID);
            }
            
            command.Parameters.AddWithValue("@BodyID", BodyID);
            command.Parameters.AddWithValue("@OwnerID", OwnerID);
            command.Parameters.AddWithValue("@Year", Year);
            command.Parameters.AddWithValue("@Color", Color);
            command.Parameters.AddWithValue("@CreatedBy", CreatedBy);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int Id))
                {
                    _VehichleID = Id;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return _VehichleID;
        }

        public static bool UpdateVehichleByID(int VehichleID, string ChassisNumber, string PlateNumber, int MakeID, int ModelID, int SubModelID, int BodyID, int OwnerID, int Year, string Color, int CreatedBy)
        {


            int AffectedRows = 0;

            string query = @"UPDATE Vehichles SET 
                                ChassisNumber = @ChassisNumber,
                                PlateNumber = @PlateNumber,
                                MakeID = @MakeID,
                                ModelID = @ModelID,
                                SubModelID = @SubModelID,
                                BodyID = @BodyID,
                                OwnerID = @OwnerID,
                                Year = @Year,
                                Color = @Color,
                                CreatedBy = @CreatedBy
                                WHERE VehichleID = @VehichleID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@VehichleID", VehichleID);
            command.Parameters.AddWithValue("@ChassisNumber", ChassisNumber);
            command.Parameters.AddWithValue("@PlateNumber", PlateNumber);
            command.Parameters.AddWithValue("@MakeID", MakeID);
            command.Parameters.AddWithValue("@ModelID", ModelID);
            if (SubModelID == -1)
            {
                command.Parameters.AddWithValue("@SubModelID", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@SubModelID", SubModelID);
            }
            command.Parameters.AddWithValue("@BodyID", BodyID);
            command.Parameters.AddWithValue("@OwnerID", OwnerID);
            command.Parameters.AddWithValue("@Year", Year);
            command.Parameters.AddWithValue("@Color", Color);
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

        public static bool DeleteVehichleByID(int VehichleID)
        {


            int AffectedRows = 0;

            string query = @"DELETE FROM Vehichles
                                     WHERE VehichleID = @VehichleID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@VehichleID", VehichleID);

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

        public static bool GetVehichleByID(int VehichleID, ref string ChassisNumber, ref string PlateNumber,ref int MakeID,ref int ModelID, ref int SubModelID, ref int BodyID, ref int OwnerID, ref int Year, ref string Color, ref int CreatedBy)
        {

            bool IsFound = false;

            string query = "SELECT * FROM Vehichles WHERE VehichleID = @VehichleID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@VehichleID", VehichleID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    ChassisNumber = (string)reader["ChassisNumber"];
                    PlateNumber = (string)reader["PlateNumber"];
                    MakeID = (int)reader["MakeID"];
                    ModelID = (int)reader["ModelID"];
                    if (reader["SubModelID"] == DBNull.Value)
                    {
                        SubModelID = -1;
                    }
                    else
                    {
                        SubModelID = (int)reader["SubModelID"];
                    }
                    
                    BodyID = (int)reader["BodyID"];
                    OwnerID = (int)reader["OwnerID"];
                    Year = (int)reader["Year"];
                    Color = (string)reader["Color"];
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

        public static bool GetVehichleByChassisNumber( string ChassisNumber,ref int VehichleID, ref string PlateNumber, ref int MakeID, ref int ModelID, ref int SubModelID, ref int BodyID, ref int OwnerID, ref int Year, ref string Color, ref int CreatedBy)
        {

            bool IsFound = false;

            string query = "SELECT * FROM Vehichles WHERE ChassisNumber = @ChassisNumber";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ChassisNumber", ChassisNumber);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    VehichleID = (int)reader["VehichleID"];
                    PlateNumber = (string)reader["PlateNumber"];
                    MakeID = (int)reader["MakeID"];
                    ModelID = (int)reader["ModelID"];
                    if (reader["SubModelID"] == DBNull.Value)
                    {
                        SubModelID = -1;
                    }
                    else
                    {
                        SubModelID = (int)reader["SubModelID"];
                    }

                    BodyID = (int)reader["BodyID"];
                    OwnerID = (int)reader["OwnerID"];
                    Year = (int)reader["Year"];
                    Color = (string)reader["Color"];
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

        public static bool GetVehichleByPlateNumber(string PlateNumber, ref int VehichleID, ref string ChassisNumber, ref int MakeID, ref int ModelID, ref int SubModelID, ref int BodyID, ref int OwnerID, ref int Year, ref string Color, ref int CreatedBy)
        {

            bool IsFound = false;

            string query = "SELECT * FROM Vehichles WHERE PlateNumber = @PlateNumber";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PlateNumber", PlateNumber);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    VehichleID = (int)reader["VehichleID"];
                    ChassisNumber = (string)reader["ChassisNumber"];
                    MakeID = (int)reader["MakeID"];
                    ModelID = (int)reader["ModelID"];
                    if (reader["SubModelID"] == DBNull.Value)
                    {
                        SubModelID = -1;
                    }
                    else
                    {
                        SubModelID = (int)reader["SubModelID"];
                    }

                    BodyID = (int)reader["BodyID"];
                    OwnerID = (int)reader["OwnerID"];
                    Year = (int)reader["Year"];
                    Color = (string)reader["Color"];
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

        public static bool IsVehichleExistByVehichleID(int VehichleID)
        {
            bool IsFound = false;

            string query = @"SELECT found = 1 FROM Vehichles WHERE VehichleID = @VehichleID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@VehichleID", VehichleID);

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

        public static DataTable GetAllVehichles()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT * FROM Vehicles_View ORDER BY VehichleID DESC";

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
