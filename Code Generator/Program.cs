
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Generator
{
    public class Program
    {
        public class ColumnInfo
        {
            public string Name { get; set; }
            public string DataType { get; set; }
        }

        public static class CodeGenerator
        {
            public static List<ColumnInfo> GetTableColumns(string connectionString, string tableName)
            {
                var columns = new List<ColumnInfo>();
                string query = @"SELECT COLUMN_NAME, DATA_TYPE 
                         FROM INFORMATION_SCHEMA.COLUMNS 
                         WHERE TABLE_NAME = @TableName";

                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@TableName", tableName);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    {
                        while (reader.Read())
                        {
                            columns.Add(new ColumnInfo
                            {
                                Name = reader["COLUMN_NAME"].ToString(),
                                DataType = MapSqlTypeToCSharpType(reader["DATA_TYPE"].ToString())
                            });
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return columns;
            }

            private static string MapSqlTypeToCSharpType(string sqlType)
            {
                switch (sqlType)
                {
                    case "int": return "int";
                    case "nvarchar": return "string";
                    case "varchar": return "string";
                    case "datetime": return "DateTime";
                    case "bit": return "bool";
                    case "decimal": return "decimal";
                    case "Default": return "string";
                    case "tinyint": return "byte";
                    case "char": return "string";
                    case "float": return "float";

                }
                return "";
            }

            //Default CRUD Operations
            private static string _GetDataByID(string TableName, List<ColumnInfo> columns)
            {
                StringBuilder GetDataByIDcode = new StringBuilder();

                GetDataByIDcode.AppendLine($"    public static bool Get{TableName}ByID({GenerateMethodParameters(columns, "Get")})");
                GetDataByIDcode.AppendLine("    {");
                GetDataByIDcode.AppendLine($@" 
                bool IsFound = false;

                string query = ""SELECT * FROM {TableName} WHERE {columns[0].Name} = @{columns[0].Name}"";

                SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue(""@{columns[0].Name}"", {columns[0].Name});

                try
                {{
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                 {{
                        IsFound = true;
                        ");
                foreach (ColumnInfo colInfo in columns)
                {
                    {
                        GetDataByIDcode.AppendLine($@"{colInfo.Name} = ({colInfo.DataType})reader[""{colInfo.Name}""];");
                    }
                }
                GetDataByIDcode.AppendLine($@"
                }}
                reader.Close();
                }}
                catch (Exception ex)
                {{
                    IsFound = false;
                }}
                finally
                {{
                    connection.Close();
                }}
            
                                ");
                GetDataByIDcode.AppendLine($" return IsFound;"); // Placeholder return statement
                GetDataByIDcode.AppendLine("    }");

                return GetDataByIDcode.ToString();
            }

            private static string _DeleteByID(string TableName, List<ColumnInfo> columns)
            {

                StringBuilder DeleteCode = new StringBuilder();
                DeleteCode.AppendLine($"    public static bool Delete{TableName}ByID({columns[0].DataType} {columns[0].Name})");
                DeleteCode.AppendLine("    {");
                DeleteCode.AppendLine($@"

                    int AffectedRows = 0;

                    string query = @""DELETE FROM {TableName}
                                     WHERE {columns[0].Name} = @{columns[0].Name}"";
                    SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue(""@{columns[0].Name}"", {columns[0].Name});

                    try
                    {{
                        connection.Open();

                        AffectedRows = command.ExecuteNonQuery();
                    }}
                    catch (Exception ex)
                    {{
                        return false;
                    }}
                    finally {{ connection.Close(); }}

                        ");
                DeleteCode.AppendLine($"  return AffectedRows > 0;"); // Placeholder return statement
                DeleteCode.AppendLine("    }");

                return DeleteCode.ToString();
            }

            private static string _UpdateByID(string TableName, List<ColumnInfo> columns)
            {
                StringBuilder UpdateCode = new StringBuilder();
                UpdateCode.AppendLine($"    public static bool Update{TableName}ByID({GenerateMethodParameters(columns)})");
                UpdateCode.AppendLine("    {");
                UpdateCode.AppendLine($@" 

                    int AffectedRows = 0;
             
            string query = @""UPDATE {TableName} SET ");

                foreach (ColumnInfo colInfo in columns)
                {
                    UpdateCode.AppendLine($"{colInfo.Name} = @{colInfo.Name},");
                }

                UpdateCode.AppendLine($@" WHERE {columns[0].Name} = @{columns[0].Name}"";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);
                ");

                foreach (ColumnInfo colInfo in columns)
                {
                    UpdateCode.AppendLine($@"command.Parameters.AddWithValue(""@{colInfo.Name}"", {colInfo.Name});");
                }

                UpdateCode.AppendLine($@" 
                        try
                        {{
                            connection.Open();

                            AffectedRows = command.ExecuteNonQuery();
                        }}
                        catch (Exception ex)
                        {{
                            return false;
                        }}
                        finally
                        {{
                            connection.Close();
                        }}
                    ");

                UpdateCode.AppendLine($"  return AffectedRows > 0;"); // Placeholder return statement
                UpdateCode.AppendLine("    }");

                return UpdateCode.ToString();
            }

            private static string _AddData(string TableName, List<ColumnInfo> columns)
            {
                StringBuilder AddCode = new StringBuilder();

                AddCode.AppendLine($"    public static int Add{TableName}({GenerateMethodParameters(columns)})");
                AddCode.AppendLine("    {");
                AddCode.AppendLine($"int _{columns[0].Name} = -1;");
                AddCode.AppendLine($@"string query = @""INSERT INTO {TableName}(");
                foreach (ColumnInfo colInfo in columns)
                {
                    AddCode.AppendLine($"{colInfo.Name},");
                }

                AddCode.AppendLine(") VALUES (");

                foreach (ColumnInfo colInfo in columns)
                {
                    AddCode.AppendLine($"@{colInfo.Name},");
                }
                AddCode.AppendLine($@");
                SELECT SCOPE_IDENTITY();"";");

                AddCode.AppendLine(@"
                SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
                SqlCommand command = new SqlCommand(query, connection);");
                foreach (ColumnInfo colInfo in columns)
                {
                    AddCode.AppendLine($@"command.Parameters.AddWithValue(""@{colInfo.Name}"", {colInfo.Name});");
                }
                AddCode.AppendLine($@"
                        try
                    {{
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int Id))
                        {{
                            _{columns[0].Name} = Id;
                        }}
                    }}
                    catch (Exception ex)
                    {{

                    }}
                    finally
                    {{
                        connection.Close();
                    }}
                    return _{columns[0].Name};
                    }}");
                return AddCode.ToString();
            }

            private static string _GetAllData(string TableName, List<ColumnInfo> columns)
            {
                StringBuilder AllDataCode = new StringBuilder();

                AllDataCode.AppendLine($@"
                    public static DataTable GetAll{TableName}()
        {{
            DataTable dt = new DataTable();

            string query = @""SELECT * FROM {TableName} ORDER BY {columns[0].Name} DESC"";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);

            try
            {{
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {{
                    dt.Load(reader);
                }}
                reader.Close();
            }}
            catch (Exception ex)
            {{

            }}
            finally
            {{
                connection.Close();
            }}
            return dt;
            }}
                        ");
                return AllDataCode.ToString();
            }

            private static string _IsDataExist(string TableName, List<ColumnInfo> columns)
            {
                StringBuilder IsExistCode = new StringBuilder();

                IsExistCode.AppendLine($@"
           
            public static bool Is{TableName}ExistBy{columns[0].Name}(int {columns[0].Name})
                {{
            bool IsFound = false;

            string query = @""SELECT found = 1 FROM {TableName} WHERE {columns[0].Name} = @{columns[0].Name}"";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(""@{columns[0].Name}"", {columns[0].Name});

            try
            {{
                connection.Open();
                object result = command.ExecuteNonQuery();
                if (result != null)
                {{
                    IsFound = true;
                }}
            }}
            catch (Exception ex)
            {{
                return false;
            }}
            finally
            {{
                connection.Close();
            }}
            return IsFound;
             }}
            ");

                return IsExistCode.ToString();
            }


            public static string GenerateDataAccessCode(string tableName, List<ColumnInfo> columns)
            {
                StringBuilder code = new StringBuilder();

                // Generate Class Definition
                code.AppendLine($@"using System;
                using System.Collections.Generic;
                using System.Data.SqlClient;
                using System.Data;
                using System.Linq;
                using System.Text;
                using System.Threading.Tasks;

                namespace BankDataAccess
                {{
                ");
                code.AppendLine("\n");
                code.AppendLine($"public class {tableName}Data");
                code.AppendLine("{");

                // Generate Insert Method
                code.AppendLine(_AddData(tableName, columns));
                code.AppendLine("\n");
                // Generate Update Method
                code.AppendLine(_UpdateByID(tableName, columns));

                code.AppendLine("\n");
                // Generate Delete Method
                code.AppendLine(_DeleteByID(tableName, columns));

                code.AppendLine("\n");
                // Generate GetById Method
                code.AppendLine(_GetDataByID(tableName, columns));

                code.AppendLine("\n");
                //Generate Is Data Exist
                code.AppendLine(_IsDataExist(tableName, columns));

                code.AppendLine("\n");
                // Generate GetAllDate Method
                code.AppendLine(_GetAllData(tableName, columns));

                code.AppendLine("}}");

                return code.ToString();
            }

            public static string GenerateMethodParameters(List<ColumnInfo> columns, string ProccessName = "")
            {
                StringBuilder parameters = new StringBuilder();

                foreach (var column in columns)
                {
                    if (ProccessName == "Get")
                        parameters.Append($"ref {column.DataType} {column.Name}, ");
                    else
                        parameters.Append($"{column.DataType} {column.Name}, ");
                }

                // Remove trailing comma and space
                if (parameters.Length > 0)
                {
                    parameters.Length -= 2;
                }

                return parameters.ToString();
            }

            public static string GenerateMethodUnReferencedParameters(List<ColumnInfo> columns, string ProccessName = "")
            {
                StringBuilder parameters = new StringBuilder();

                foreach (var column in columns)
                {
                    if (ProccessName == "Get")
                        parameters.Append($"ref {column.Name}, ");
                    else
                        parameters.Append($"{column.Name}, ");
                }

                // Remove trailing comma and space
                if (parameters.Length > 0)
                {
                    parameters.Length -= 2;
                }

                return parameters.ToString();
            }
            public static void CreateClassFile(string className, string directoryPath, string ClassContent)
            {
                // Create the directory if it doesn't exist
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Define the path for the new class file
                string filePath = Path.Combine(directoryPath, $"{className}.cs");

                // Generate the class content
                string classContent = ClassContent;

                // Write the content to the .cs file
                File.WriteAllText(filePath, classContent);
                Console.WriteLine($"Class file '{className}.cs' has been created at {directoryPath}");
            }

        }
        static void Main(string[] args)
        {
            string connectionString = "Server=.;Database=DVLD;User Id =sa;Password=sa123456;";
            string tableName = "Fines";


            var columns = CodeGenerator.GetTableColumns(connectionString, tableName);
            string DataAccessCode = CodeGenerator.GenerateDataAccessCode(tableName, columns);
            string BussinessCode = BussinessCodeGenerator.GenerateBussinessCode(tableName, columns);
            //Console.WriteLine(code);

            string DataclassName = "cls" + tableName + "Data";
            string BussinessclassName = "cls" + tableName;

            string DataAccessdirectoryPath = @"D:\Development\C#\My-Github\DVLD SYS\DVLD-DataAccess"; // Set your desired directory path
            string BussinessDirectoryPath = @"D:\Development\C#\My-Github\DVLD SYS\DVLD-Bussiness";

            CodeGenerator.CreateClassFile(DataclassName, DataAccessdirectoryPath, DataAccessCode);
            CodeGenerator.CreateClassFile(BussinessclassName, BussinessDirectoryPath, BussinessCode);

            Console.ReadLine();
        }
    }
}
