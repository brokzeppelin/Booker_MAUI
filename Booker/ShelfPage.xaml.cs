using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Serialization;

namespace Booker;

public partial class ShelfPage : ContentPage
{
    public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>();
    public ShelfPage()
	{
		InitializeComponent();
        PopulateListView();
        BindingContext = Books;
    }

    public void PopulateListView()
    {
        XmlParser xmlParser = new XmlParser();
        BookCollection bc = xmlParser.Parse();
        foreach (Book book in bc.Books)
        {
            Books.Add(book);
        }
    }

    private async void AddToShelf(object sender, EventArgs e)
    {
        string pathToFile = await Filer.GetPickedFileFullPath();
        Book newBook = new Book() { 
            Id = Guid.NewGuid().ToString(), 
            Title = Path.GetFileName(pathToFile)
        };
        Books.Add(newBook);
        string content = Filer.GetTxtFileContent(pathToFile);
        Filer.WriteToFile(newBook.Title, Path.Combine(FileSystem.Current.AppDataDirectory, Constants.UserFolder), content);

        newBook.RegisterToXML();
    }
}