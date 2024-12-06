using System.Xml;

namespace Booker
{
    public partial class App : Application
    {
        public BookCollection Library { get; set; } = new BookCollection();
        public App()
        {
            InitializeComponent();
            SetUpSettings();
            PopulateLibrary();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
        private void SetUpSettings() 
        {
            if (System.IO.File.Exists(Path.Combine(FileSystem.Current.AppDataDirectory, Constants.UserFolder, Constants.SettingsFile)))
                return;

            Filer.CreateUserFolder();
            Filer.CreateSettingsXml();
        }
        private void PopulateLibrary()
        {
            XmlParser xmlParser = new XmlParser();
            Library = xmlParser.Parse();
        }
    }
}
