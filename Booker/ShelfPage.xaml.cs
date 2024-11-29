

namespace Booker;

public partial class ShelfPage : ContentPage
{
    public ShelfPage()
	{
		InitializeComponent();
        BindingContext = ((App)Application.Current).Library.Books;
    }

    private async void AddToShelf(object sender, EventArgs e)
    {
        string pathToFile = await Filer.GetPickedFileFullPath();
        Book newBook = new Book() { 
            Id = Guid.NewGuid().ToString(), 
            Title = Path.GetFileName(pathToFile)
        };

        if (!((App)Application.Current).Library.Contains(newBook))
        {
            ((App)Application.Current).Library.Add(newBook);
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
        await Navigation.PushAsync(new BookPage());
        BooksListView.SelectedItem = null;
    }
}