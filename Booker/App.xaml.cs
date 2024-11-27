using System.Xml;

namespace Booker
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(Preferences.Default.ContainsKey("last") ? new BookPage() : new ShelfPage());
        }
        protected override void OnStart() 
        {
            if (System.IO.File.Exists(Path.Combine(FileSystem.Current.AppDataDirectory, Constants.UserFolder, Constants.SettingsFile)))
                return;

            Filer.CreateUserFolder();
            Filer.CreateSettingsXml();
        }
    }
}
