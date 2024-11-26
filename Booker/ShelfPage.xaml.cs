

namespace Booker;

public partial class ShelfPage : ContentPage
{
    public BookCollection bookCollection { get; set; } = new BookCollection();
    public ShelfPage()
	{
		InitializeComponent();
        PopulateListView();
        BindingContext = bookCollection.Books;
    }

    public void PopulateListView()
    {
        XmlParser xmlParser = new XmlParser();
        bookCollection = xmlParser.Parse();
    }

    private async void AddToShelf(object sender, EventArgs e)
    {
        string pathToFile = await Filer.GetPickedFileFullPath();
        Book newBook = new Book() { 
            Id = Guid.NewGuid().ToString(), 
            Title = Path.GetFileName(pathToFile)
        };

        if (!bookCollection.Contains(newBook))
        {
            bookCollection.Add(newBook);
            string content = Filer.GetTxtFileContent(pathToFile);
            Filer.WriteToFile(newBook.Title, Path.Combine(FileSystem.Current.AppDataDirectory, Constants.UserFolder), content);
            newBook.RegisterToXML();
        }
    }

    private async void OnBookClicked(object sender, ItemTappedEventArgs args)
    {
        var item = args.Item as Book;
        if (item == null)
            return;
        item.SetAsPreferenced();
        await Navigation.PushAsync(new BookPage(item));
        BooksListView.SelectedItem = null;
    }
}