using DVLD_Bussiness;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_System
{
    public static class clsGlobalSettings
    {

        public static clsUser CurrentUser = new clsUser();
        
        public static bool RememberUserNameAndPassword(string UserName,string Password)
        {
            try
            {
                string CurrentDirectory = Directory.GetCurrentDirectory();

                string filePath = CurrentDirectory + "\\data.txt";

                if (UserName == "" && File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }
                
                string dataToSave = UserName + "#//#" + Password; 

                using (StreamWriter writer  = new StreamWriter(filePath))
                {
                    writer.WriteLine(dataToSave);
                        return true;
                }

            }
            catch (IOException ex)
            {
                MessageBox.Show("An Error Occourred : " + ex.ToString(), "Error");
                return false;
            }
        }

        public static bool GetStoredCredentials(ref string UserName,ref string Password)
        {
            try
            {
                string CurrentDirectory = Directory.GetCurrentDirectory();

                string filePath = CurrentDirectory + "\\data.txt";

                if (File.Exists(filePath))
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string line = "";
                        while (( line = reader.ReadLine())!= null)
                        {
                            string[] data = line.Split(new string[] {"#//#"},StringSplitOptions.None);
                            UserName = data[0];
                            Password = data[1];
                        }
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }catch (Exception ex)
            {
                MessageBox.Show("An Error Occourred : " + ex.ToString(), "Error");
                return false;
            }
        }
    }
}
