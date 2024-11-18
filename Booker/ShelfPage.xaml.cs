using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Serialization;

namespace Booker;

public partial class ShelfPage : ContentPage
{
    public ShelfPage()
	{
		InitializeComponent(); 
        PopulateListView();
    }

    public void PopulateListView()
    {
        XmlParser xmlParser = new XmlParser();
        //TODO: ACTUAL binding (MVVM)
        BookCollection v = xmlParser.Parse();
        BooksListBox.ItemsSource = xmlParser.Parse().Books;
    }

    private async void AddToShelf(object sender, EventArgs e)
    {
        string pathToFile = await Filer.GetPickedFileFullPath();
        Book newBook = new Book() { 
            Id = Guid.NewGuid().ToString(), 
            Title = Path.GetFileName(pathToFile)};

        string content = Filer.GetTxtFileContent(pathToFile);
        Filer.WriteToFile(newBook.Title, Path.Combine(FileSystem.Current.AppDataDirectory, Constants.UserFolder), content);

        newBook.RegisterToXML();
    }
}