namespace Booker
{
    public partial class BookPage : ContentPage
    {
        Book book = new Book();
        public BookPage(Book book)
        {
            InitializeComponent();
            this.book = book;
            LoadBook(book.Title);
        }
        public BookPage()
        {
            InitializeComponent();
            Title = DateTime.Today.ToString();
            string lastTitle = Preferences.Default.Get("last", String.Empty);
            LoadBook(lastTitle);
        }

        public void LoadBook(string title)
        {
            if (title == String.Empty)
                return;
            string content = Filer.GetTxtFileContent(Path.Combine(FileSystem.AppDataDirectory, Constants.UserFolder, title));
            bookBox.Add(
                new Label { Text = content });
        }

        private void OnOnceTapped(object sender, EventArgs e)
        {
            btnToShelf.IsVisible = !btnToShelf.IsVisible;
        }

        private async void OnBtnToShelfClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ShelfPage());
            btnToShelf.IsVisible = false;
        }
    }
}