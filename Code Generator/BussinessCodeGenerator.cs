
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Code_Generator.Program;

namespace Code_Generator
{
    public static class BussinessCodeGenerator
    {
        public static string GenerateBussinessCode(string tableName, List<ColumnInfo> columns)
        {
            StringBuilder code = new StringBuilder();
            tableName = tableName.Substring(0, tableName.Length - 1);
            string ClassName = "cls" + tableName;
            // Generate Class Definition
            code.AppendLine($@"using System;
                using System.Collections.Generic;
                using System.Data.SqlClient;
                using System.Data;
                using System.Linq;
                using System.Text;
                using System.Threading.Tasks;

                namespace BankBussiness
                {{
                ");
            code.AppendLine("\n");
            code.AppendLine($"public class {ClassName}");
            code.AppendLine("{");
            code.AppendLine($@"
            enum enMode {{enAddNew = 1,enUpdate = 2}}
            enMode _Mode = enMode.enAddNew;
                    ");

            code.AppendLine("\n");

            //Generate Properties
            foreach (ColumnInfo colInfo in columns)
            {
                code.AppendLine($@"public {colInfo.DataType} {colInfo.Name} {{ set; get; }}");
            }

            //Generate Constructors

            code.AppendLine("\n");

            //Default Constructor
            code.AppendLine($@"public {ClassName}()
            {{ ");
            foreach (ColumnInfo colInfo in columns)
            {
                code.AppendLine($"this.{colInfo.Name} = default;");
            }
            code.AppendLine($"_Mode = enMode.enAddNew;}}");
            code.AppendLine("\n");

            code.AppendLine("\n");

            //Parametrized Constructor
            code.AppendLine($@"private {ClassName}({CodeGenerator.GenerateMethodParameters(columns)}){{");
            foreach (ColumnInfo colInfo in columns)
            {
                code.AppendLine($"this.{colInfo.Name} = {colInfo.Name};");
            }
            code.AppendLine($"_Mode = enMode.enUpdate;}}");

            code.AppendLine("\n");

            //Add New
            code.AppendLine($@"
             private bool _Add{tableName}(){{
                {columns[0].Name} = cls{tableName}Data.AddNew{tableName}({CodeGenerator.GenerateMethodUnReferencedParameters(columns)});

                return ({columns[0].Name} != -1);
                }}");

            code.AppendLine("\n");

            //Update
            code.AppendLine($"private bool _Update{tableName}(){{");
            code.AppendLine($" return {ClassName}Data.Update{tableName}ByID({CodeGenerator.GenerateMethodUnReferencedParameters(columns)});}}");

            code.AppendLine("\n");
            
            //Find
            code.AppendLine($"public static cls{tableName} Find{tableName}ByID(int {columns[0].Name}) {{");

            foreach (ColumnInfo colInfo in columns)
            {
                code.AppendLine($"{colInfo.DataType} {colInfo.Name} = default;");
            }
            code.AppendLine($@" if (cls{tableName}Data.Get{tableName}ByID({CodeGenerator.GenerateMethodUnReferencedParameters(columns,"Get")}))
                {{
                    return new {ClassName}({CodeGenerator.GenerateMethodUnReferencedParameters(columns)});
                }}
                else
                {{
                    return null;
                }} 
            }}");

            //IsExist
            code.AppendLine("\n");

            code.AppendLine($@"public static bool IsExistBy{tableName}ID(int {columns[0].Name})
            {{
                return {ClassName}Data.Is{tableName}ExistBy{tableName}ID({columns[0].Name});
            }} ");

            //Delete 
            code.AppendLine("\n");

            code.AppendLine($@"public static bool Delete{tableName}(int {columns[0].Name}){{");
            code.AppendLine($@"return {ClassName}Data.Delete{tableName}ByID({columns[0].Name});}}");

            //Get All Data
            code.AppendLine("\n");

            code.AppendLine($"public static DataTable Get{tableName}sList(){{");
            code.AppendLine($"return {ClassName}Data.GetAll{tableName}s();}}");


            //Save Method
            code.AppendLine("\n");

            code.AppendLine($@"
            public bool Save()
            {{
                switch (_Mode)
                {{
                    case enMode.enAddNew:
                        if (_AddNew{tableName}())
                        {{
                            _Mode = enMode.enUpdate;
                            return true;
                        }}
                        else
                            return false;

                    case enMode.enUpdate:
                        if (_Update{tableName}())
                            return true;
                        else
                            return false;
                }}
                return false;
            }}

            ");
            
            code.AppendLine("}}");

            return code.ToString();
        }
    }
}
