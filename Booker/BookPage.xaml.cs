namespace Booker
{
    public partial class BookPage : ContentPage
    {
        public BookPage(Book book)
        {
            InitializeComponent();
            LoadBook(book);
        }

        public void LoadBook(Book book)
        {
            if (book.Title == String.Empty) 
                return;
            string content = Filer.GetTxtFileContent(Path.Combine(FileSystem.AppDataDirectory, Constants.UserFolder, book.Title));
            bookBox.Add(
                new Label { Text = content});
        }
    }
}
