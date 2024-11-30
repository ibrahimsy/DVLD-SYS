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
     QuestionID,Question,Option1,Option3,Option4,CorrectAnserID
     */
    public static class clsQuestionData
    {
        public static bool GetQuestionInfoByID(int QuestionID,ref string Question, ref string Option1, ref string Option2, ref string Option3, ref string Option4,ref int CorrectAnserID)
        {
            bool _isFound = false;

            string query = @"SELECT * FROM WrittenTestQuestions WHERE QuestionID = @QuestionID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@QuestionID", QuestionID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    _isFound = true;

                    Question = (string)reader["Question"];
                    Option1 = (string)(reader["Option1"]);
                    Option2 = (string)(reader["Option2"]);
                    Option3 = (string)(reader["Option3"]);
                    Option4 = (string)(reader["Option4"]);
                    CorrectAnserID = (int)reader["CorrectAnserID"];

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

        public static int AddNewQuestion( string Question,  string Option1,  string Option2,  string Option3,  string Option4,  int CorrectAnserID)
        {
            int QuestionID = -1;

            string query = @"INSERT INTO WrittenTestQuestions
                                   (QuestionID
                                   ,Question
                                   ,Option1
                                   ,Option2
                                   ,Option3
                                   ,Option4
                                   ,CorrectAnserID)
                             VALUES
                                   (@Question
                                   ,@Option1
                                   ,@Option2
                                   ,@Option3
                                   ,@Option4
                                   ,@CorrectAnserID);
		                           SELECT SCOPE_IDENTITY();";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Question", Question);
            command.Parameters.AddWithValue("@Option1", Option1);
            command.Parameters.AddWithValue("@Option2", Option2);
            command.Parameters.AddWithValue("@Option3", Option3);
            command.Parameters.AddWithValue("@Option4", Option4);
            command.Parameters.AddWithValue("@CorrectAnserID", CorrectAnserID);
            try
            {
                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null && (int.TryParse(result.ToString(), out int ID)))
                {
                    QuestionID = ID;
                }
            }
            catch (Exception ex)
            {

            }
            finally { connection.Close(); }

            return QuestionID;
        }

        public static bool UpdateQuestion(int QuestionID, string Question, string Option1, string Option2, string Option3, string Option4, int CorrectAnserID)
        {
            int AffectedRows = 0;

            string query = @"UPDATE WrittenTestQuestions
                            SET Question = @Question
                              ,Option1 = @Option1
                              ,Option2 = @Option2
                              ,Option3 = @Option3
                              ,Option4 = @Option4
                              ,CorrectAnserID = @CorrectAnserID
                            WHERE QuestionID = @QuestionID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@QuestionID", QuestionID);
            command.Parameters.AddWithValue("@Question", Question);
            command.Parameters.AddWithValue("@Option1", Option1);
            command.Parameters.AddWithValue("@Option2", Option2);
            command.Parameters.AddWithValue("@Option3", Option3);
            command.Parameters.AddWithValue("@Option4", Option4);
            command.Parameters.AddWithValue("@CreatedByUserID", CorrectAnserID);

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

        public static bool DeleteQuestion(int QuestionID)
        {
            int AffectedRows = 0;

            string query = @"DELETE FROM WrittenTestQuestions
                                WHERE QuestionID =@QuestionID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@QuestionID", QuestionID);

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

        public static DataTable GetAllQuestions()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT * FROM WrittenTestQuestions ORDER BY NEWID()";

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
