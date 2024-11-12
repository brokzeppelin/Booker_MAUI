using System.Xml;

namespace Booker
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
        protected override void OnStart() 
        {
            SetUp();
        }
        public void SetUp()
        {
            if (System.IO.File.Exists(Path.Combine(FileSystem.Current.AppDataDirectory, Constants.UserFolder, Constants.SettingsFile)))
                return;

            CreateUserFolder(Path.Combine(FileSystem.Current.AppDataDirectory, Constants.UserFolder));
            CreateSettingsXml();
        }

        private void CreateUserFolder(string dir)
        {
            if (!System.IO.Directory.Exists(dir))
            {
                System.IO.Directory.CreateDirectory(dir);
            }
        }

        private void CreateSettingsXml()
        {
            XmlDocument settingsXML = new XmlDocument();
            settingsXML.LoadXml("""
                                    <?xml version="1.0" encoding="UTF-8" ?>
                                    <settings>
                                    </settings>
                                    """
            );
            XmlWriter writerXML = XmlWriter.Create(Path.Combine(FileSystem.Current.AppDataDirectory, Constants.UserFolder, Constants.SettingsFile));
            settingsXML.Save(writerXML);
        }
    }
}
