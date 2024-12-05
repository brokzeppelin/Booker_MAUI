namespace Booker
{
    public partial class BookPage : ContentPage
    {
        Book book = ((App)Application.Current).Library.GetBookById(Preferences.Default.Get("last", String.Empty));
        public BookPage()
        {
            InitializeComponent();
            LoadBook(book.Title);
        }

        public async void LoadBook(string title)
        {
            if (title == String.Empty)
            {
                await Shell.Current.GoToAsync("//ShelfPage");
                return;
            }
            string content = Filer.GetTxtFileContent(Path.Combine(FileSystem.AppDataDirectory, Constants.UserFolder, title));
            bookBox.Add(
                new Label { Text = content });
            Timer timer = new Timer(obj => {
                MainThread.BeginInvokeOnMainThread(() => scroller.ScrollToAsync(0, book.Bookmark, false));
            }, null, 100, Timeout.Infinite);
        }

        private void OnOnceTapped(object sender, EventArgs e)
        {
            btnToShelf.IsVisible = !btnToShelf.IsVisible;
        }

        private void OnTwiceTapped(object sender, EventArgs e)
        {
            book.SetBookmark(scroller.ScrollY);
            PopUp();
        }

        private async void OnBtnToShelfClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//ShelfPage");
            btnToShelf.IsVisible = false;
        }

        private void PopUp()
        {
            popUp.IsVisible = true;
            Timer timer = new Timer(obj => {
                MainThread.BeginInvokeOnMainThread(() => popUp.IsVisible = false);
            }, null, 2000, Timeout.Infinite);
        }
    }
}