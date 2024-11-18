using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Booker
{
    public static class Filer
    {
        // New file type for picking
        public static FilePickerFileType TxtFileType
        {
            get => GetFilePickerFileTypeTxt();
        }

        private static FilePickerFileType GetFilePickerFileTypeTxt()
        {
            return new FilePickerFileType(
                        new Dictionary<DevicePlatform, IEnumerable<string>>
                        {
                       { DevicePlatform.iOS, new[] { "public.text" } }, // UTType values  
                       { DevicePlatform.Android, new[] { "text/plain" } }, // MIME type  
                       { DevicePlatform.WinUI, new[] { ".txt" } }, // file extension  
                       { DevicePlatform.macOS, new[] { "txt" } },
                        });
        }

        public static async Task<string> GetPickedFileFullPath()
        {
            var filePicked =  await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "Pick a .txt file",
                FileTypes = TxtFileType
            });

            return filePicked?.FullPath ?? String.Empty;
        }

        public static string GetTxtFileContent(string pathToFile)
        {
            using (StreamReader reader = new StreamReader(pathToFile))
            {
                return reader.ReadToEnd();
            }
        }

        public static void WriteToFile(string title, string pathToSave, string content)
        {
            using (StreamWriter writer = new StreamWriter(Path.Combine(pathToSave, title)))
            {
                writer.WriteLine(content);
            }
        }

        public static void CreateUserFolder()
        {
            if (!System.IO.Directory.Exists(Path.Combine(FileSystem.Current.AppDataDirectory, Constants.UserFolder)))
            {
                System.IO.Directory.CreateDirectory(Path.Combine(FileSystem.Current.AppDataDirectory, Constants.UserFolder));
            }
        }

        public static void CreateSettingsXml()
        {
            XDocument xmlDoc = new XDocument(
                new XDeclaration("1.0", "utf-8","yes"),
                new XElement("Settings", ""));
            xmlDoc.Save(Path.Combine(FileSystem.Current.AppDataDirectory, Constants.UserFolder, Constants.SettingsFile));
        }

        public static void InsertBookElement(string xmlElement)
        {
            XDocument xmlDoc = XDocument.Load(Path.Combine(FileSystem.Current.AppDataDirectory, Constants.UserFolder, Constants.SettingsFile));
            xmlDoc.Root!.Add(XElement.Parse(xmlElement));
            xmlDoc.Save(Path.Combine(FileSystem.Current.AppDataDirectory, Constants.UserFolder, Constants.SettingsFile));
        }
    }
}
