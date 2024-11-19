using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_System.Global_Classes
{
    public class clsUtil
    {

        static string _CreateGUID()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }

        static string _RenameImageWithGUID(string sourseFile)
        {
            string fileName = sourseFile;
            FileInfo fileInfo = new FileInfo(fileName);
            string fileExtention = fileInfo.Extension;

            return _CreateGUID() + fileExtention;
        }

        static bool _CreateDirectoryIfDoesntExist(string folderPath)
        {
            
            if (!Directory.Exists(folderPath))
            {
                try
                {
                    Directory.CreateDirectory(folderPath);
                    return true;
                }
                catch (IOException ex)
                {
                    return false;
                }
                
            }
            return true;    
        }

        public static bool CopyImageToProjectImagesFolder(ref string sourceFile)
        {

            string ImagesDirectory = @"D:\Devlopment\C#\DVLD System\Images\";

            if(_CreateDirectoryIfDoesntExist(ImagesDirectory))
            {
                try
                {
                    string DestinationFile = ImagesDirectory + _RenameImageWithGUID(sourceFile);
                    File.Copy(sourceFile, DestinationFile, true);
                    sourceFile = DestinationFile;
                
                }catch(IOException ex)
                {
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Error To Copy Image To Folder","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
    }
}
