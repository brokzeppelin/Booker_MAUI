using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Serialization;

namespace Booker;

public partial class ShelfPage : ContentPage
{
	public ShelfPage()
	{
		InitializeComponent();        
	}

    private async void AddToShelf(object sender, EventArgs e)
    {
        string pathToFile = await PickTheFile();
        string userDir = FileSystem.Current.AppDataDirectory + "/UserBooks";

        CreateUserFolder(userDir);

        if (pathToFile != null)
        {
            ProcessAndSave(pathToFile, userDir);
        }

        // TODO: create book class to make listview on its base
    }

    private async Task<string?> PickTheFile()
    {
        // Define new file type for picking
        var txtFileType = new FilePickerFileType(
                  new Dictionary<DevicePlatform, IEnumerable<string>>
                  {
                       { DevicePlatform.iOS, new[] { "public.text" } }, // UTType values  
                       { DevicePlatform.Android, new[] { "text/plain" } }, // MIME type  
                       { DevicePlatform.WinUI, new[] { ".txt" } }, // file extension  
                       { DevicePlatform.macOS, new[] { "txt" } },
                  });

        // Get the file
        var txtPicked = await FilePicker.Default.PickAsync(new PickOptions
        {
            PickerTitle = "Pick a .txt file",
            FileTypes = txtFileType
        });

        // Pass the path to file
        return txtPicked?.FullPath;
    }

    private void CreateUserFolder(string dir) 
    {
        if (!System.IO.Directory.Exists(dir))
        {
            System.IO.Directory.CreateDirectory(dir);
            CreateSettingsXML(dir);
        }
    }

    private void CreateSettingsXML(string userFolder)
    {
        XmlDocument settingsXML = new XmlDocument();
        settingsXML.LoadXml("<settings></settings>");
        XmlWriter writerXML = XmlWriter.Create(userFolder + "/settings.xml");
        settingsXML.Save(writerXML);
    }

    private void InsertSettingsXML(string userFolder, string fileName)
    {
        XmlDocument settingsXML = new XmlDocument();
        settingsXML.Load(userFolder + "/settings.xml");

        XmlElement bookXmlElement = settingsXML.CreateElement("book");
        bookXmlElement.InnerXml = $"""
                                  <id>{Guid.NewGuid()}</id>
                                  <name>{fileName.TrimEnd()}</name>
                                  <bookmark></bookmark>
                                  """;

        settingsXML.DocumentElement.AppendChild(bookXmlElement);
        settingsXML.Save(userFolder + "/settings.xml");
    }

    private async void ProcessAndSave(string what, string where)
    {
        string fileContent;

        using (StreamReader reader = new StreamReader(what))
        {
            fileContent = await reader.ReadToEndAsync();
        }

        string fileName = Path.GetFileName(what);

        using (StreamWriter writer = new StreamWriter(where + fileName))
        {
            if (!System.IO.File.Exists(where + fileName))
            {
                writer.WriteLine(fileContent);
            }
        }

        InsertSettingsXML(where, Path.GetFileNameWithoutExtension(what));
    }
}