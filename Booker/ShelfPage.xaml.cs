

namespace Booker;

public partial class ShelfPage : ContentPage
{
    public ShelfPage()
	{
		InitializeComponent();
        BindingContext = ((App)Application.Current).Library.Books;
    }

    void OnBtnExitClicked(object sender, EventArgs e)
    { }

    private async void OnBtnAddClicked(object sender, EventArgs e)
    {
        string pathToFile = await Filer.GetPickedFileFullPath();
        if (pathToFile != null)
        {
            Book newBook = new Book()
            {
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
            else
            {
                //TODO: Change to PopUp
                await DisplayAlert("Error", "A book with the same title has already been added", "Dismiss");
            }
        }
    }
    
    private void OnBtnSettingsClicked(object sender, EventArgs e)
    { }

    private async void OnBookClicked(object sender, ItemTappedEventArgs args)
    {
        if (args.Item is Book bookItem)
        {
            bookItem.SetAsPreferenced();
            await Navigation.PushAsync(new BookPage());
            BooksListView.SelectedItem = null;
        }
    }

    private void OnMenuItemDeleteClicked(object sender, EventArgs e)
    {
        if (sender is MenuItem menuItem)
        {
            Book bookItem = (Book)menuItem.BindingContext;
            ((App)Application.Current).Library.Remove(bookItem);
        }
    }
}