using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public static class clsPersonData
    {
        public static bool GetPersonInfoByID(int personID,ref string nationalNo,ref string firstName,ref string secondName,ref string thirdName,ref string lastName,
          ref  DateTime dateOfBirth,ref short gendor,ref string address,ref string phone,ref string email,ref int nationalityCountryId,ref string imagePath)
        {
            bool _isFound = false;

            string query = @"SELECT PersonID
                              ,NationalNo
                              ,FirstName
                              ,SecondName
                              ,ThirdName
                              ,LastName
                              ,DateOfBirth
                              ,Gendor
                              ,Address
                              ,Phone
                              ,Email
                              ,NationalityCountryID
                              ,ImagePath
                             FROM People
                             WHERE PersonID = @personID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@personID", personID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                   _isFound = true;

                    nationalNo = (string)reader["NationalNo"];
                    firstName = (string)reader["FirstName"];
                    secondName = (string)reader["SecondName"];
                    thirdName = (string)reader["ThirdName"];
                    lastName = (string)reader["LastName"];
                    dateOfBirth = (DateTime)reader["DateOfBirth"];
                    gendor = Convert.ToInt16(reader["Gendor"]);
                    address = (string)reader["Address"];
                    phone = (string)reader["Phone"];
                    email = (string)reader["Email"];
                    nationalityCountryId = (int)reader["NationalityCountryId"];
                    
                    if (reader["ImagePath"] == System.DBNull.Value)
                    {
                        imagePath = "";
                    }
                    else
                    {
                        imagePath = (string)reader["ImagePath"];
                    }
                    
                }
               reader.Close();
            }catch (Exception ex)
            {
                
            }
            finally
            {
                connection.Close();
            }
            return _isFound;
        }

        public static bool GetPersonInfoByNo(ref int personID, string nationalNo, ref string firstName, ref string secondName, ref string thirdName, ref string lastName,
          ref DateTime dateOfBirth, ref short gendor, ref string address, ref string phone, ref string email, ref int nationalityCountryId, ref string imagePath)
        {
            bool _isFound = false;

            string query = @"SELECT 
                               PersonID
                              ,NationalNo
                              ,FirstName
                              ,SecondName
                              ,ThirdName
                              ,LastName
                              ,DateOfBirth
                              ,Gendor
                              ,Address
                              ,Phone
                              ,Email
                              ,NationalityCountryID
                              ,ImagePath
                             FROM People
                             WHERE NationalNo = @NationalNo";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", nationalNo);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    _isFound = true;

                    personID = (int)reader["PersonID"];
                    firstName = (string)reader["FirstName"];
                    secondName = (string)reader["SecondName"];
                    thirdName = (string)reader["ThirdName"];
                    lastName = (string)reader["LastName"];
                    dateOfBirth = (DateTime)reader["DateOfBirth"];
                    gendor = Convert.ToInt16(reader["Gendor"]);
                    address = (string)reader["Address"];
                    phone = (string)reader["Phone"];
                    email = (string)reader["Email"];
                    nationalityCountryId = (int)reader["NationalityCountryId"];
                    if (reader["ImagePath"] == System.DBNull.Value)
                    {
                        imagePath = "";
                    }
                    else
                    {
                        imagePath = (string)reader["ImagePath"];
                    }
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

        public static int AddNewPerson( string nationalNo, string firstName, string secondName, string thirdName, string lastName,
           DateTime dateOfBirth, short gendor, string address, string phone, string email, int nationalityCountryId, string imagePath)
        {
            int PersonID = -1;
            string query = @"INSERT INTO People
                               (NationalNo
                               ,FirstName
                               ,SecondName
                               ,ThirdName
                               ,LastName
                               ,DateOfBirth
                               ,Gendor
                               ,Address
                               ,Phone
                               ,Email
                               ,NationalityCountryID
                               ,ImagePath)
                         VALUES
                               (@NationalNo
                               ,@FirstName
                               ,@SecondName
                               ,@ThirdName
                               ,@LastName
                               ,@DateOfBirth
                               ,@Gendor
                               ,@Address
                               ,@Phone
                               ,@Email
                               ,@NationalityCountryID
                               ,@ImagePath);
                                SELECT SCOPE_IDENTITY();";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query,connection);

            command.Parameters.AddWithValue("@NationalNo",nationalNo);
            command.Parameters.AddWithValue("@FirstName",firstName);
            command.Parameters.AddWithValue("@SecondName",secondName);
            command.Parameters.AddWithValue("@ThirdName",thirdName);
            command.Parameters.AddWithValue("@LastName",lastName);
            command.Parameters.AddWithValue("@DateOfBirth",dateOfBirth);
            command.Parameters.AddWithValue("@Gendor",gendor);
            command.Parameters.AddWithValue("@Address",address);
            command.Parameters.AddWithValue("@Phone",phone);
            command.Parameters.AddWithValue("@Email",email);
            command.Parameters.AddWithValue("@NationalityCountryID",nationalityCountryId);

            if (imagePath == "")
            {
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@ImagePath", imagePath);
            }
            

            try
            {
                connection.Open();

               object result =  command.ExecuteScalar();
                if (result != null && (int.TryParse(result.ToString(),out int ID)))
                {
                    PersonID = ID;
                }
            }catch (Exception ex)
            {

            }finally { connection.Close(); }

            return PersonID;
        }

        public static bool UpdatePerson(int PersonID,string nationalNo, string firstName, string secondName, string thirdName, string lastName,
           DateTime dateOfBirth, short gendor, string address, string phone, string email, int nationalityCountryId, string imagePath)
        {
            int AffectedRows = 0;

            string query = @"UPDATE People
                           SET NationalNo = @NationalNo
                              ,FirstName = @FirstName
                              ,SecondName = @SecondName
                              ,ThirdName = @ThirdName
                              ,LastName = @LastName
                              ,DateOfBirth = @DateOfBirth
                              ,Gendor = @Gendor
                              ,Address = @Address
                              ,Phone = @Phone
                              ,Email = @Email
                              ,NationalityCountryID = @NationalityCountryID
                              ,ImagePath = @ImagePath
                         WHERE PersonID = @PersonID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query,connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@NationalNo", nationalNo);
            command.Parameters.AddWithValue("@FirstName", firstName);
            command.Parameters.AddWithValue("@SecondName", secondName);
            command.Parameters.AddWithValue("@ThirdName", thirdName);
            command.Parameters.AddWithValue("@LastName", lastName);
            command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
            command.Parameters.AddWithValue("@Gendor", gendor);
            command.Parameters.AddWithValue("@Address", address);
            command.Parameters.AddWithValue("@Phone", phone);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@NationalityCountryID", nationalityCountryId);
            
            if (imagePath == "")
            {
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@ImagePath", imagePath);
            }
            
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

        public static bool DeletePerson(int PersonID) 
        {
            int AffectedRows = 0;

            string query = @"DELETE FROM People
                                WHERE PersonID =@PersonID";

            SqlConnection connection = new SqlConnection (clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand (query,connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open ();

                AffectedRows =  command.ExecuteNonQuery ();

            }catch (Exception ex)
            {

            }finally { connection.Close(); }

            return AffectedRows > 0;    
        }

        public static bool IsPersonExist(int PersonID)
        {
            bool _isFound = false;

            string query = "SELECT found = 1 FROM People WHERE PersonID = @PersonID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query,connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open ();

                SqlDataReader reader =  command.ExecuteReader ();

                if (reader.HasRows)
                {
                    _isFound = true;

                    reader.Close();  
                }
            }catch(Exception ex)
            {
                _isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return _isFound;
        }

        public static bool IsPersonExist(string NationalNo)
        {
            bool _isFound = false;

            string query = "SELECT found = 1 FROM People WHERE NationalNo = @NationalNo";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);

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
                _isFound= false;
            }
            finally
            {
                connection.Close();
            }
            return _isFound;
        }

        public static DataTable GetAllPeople()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT 
                            PersonID   As [Person ID],
                            NationalNo AS [National No],
                            FirstName  AS [First Name],
                            SecondName AS [Second Name],
                            ThirdName  AS [Third Name],
                            LastName   AS [Last Name],
                            DateOfBirth AS [Date Of Birth],
                            CASE
	                            WHEN Gendor = 0 THEN 'Male'
	                            WHEN Gendor = 1 THEN 'Female'
                            End
                            AS Gendor,
                            Address,
                            Phone,
                            Email
                            FROM People";

            SqlConnection connection = new SqlConnection (clsDataAccessSettings.ConnectionString) ;

            SqlCommand command = new SqlCommand (query,connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader ();

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
