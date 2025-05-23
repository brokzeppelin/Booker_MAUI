﻿namespace Booker
{
    public partial class BookPage : ContentPage
    {
        Book book;
        public BookPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            book = ((App)Application.Current).Library.GetBookById(Preferences.Default.Get("last", String.Empty));
            LoadBook(book.GetFilename());
        }

        public async void LoadBook(string filename)
        {
            if (filename == String.Empty)
            {
                await Shell.Current.GoToAsync("//ShelfPage");
            }
            else
            {
                string content = Filer.GetTxtFileContent(Path.Combine(FileSystem.AppDataDirectory, Constants.UserFolder, filename));
                lblContent.Text = content;
                Timer timer = new Timer(obj =>
                {
                    MainThread.BeginInvokeOnMainThread(() => scroller.ScrollToAsync(0, book.Bookmark, false));
                }, null, 100, Timeout.Infinite);
                UpdateStatistics();
            }
        }

        private async void OnOnceTapped(object sender, EventArgs e)
        {
            switch (!btnToShelf.IsVisible && !btnInfo.IsVisible)
            {
                case true:
                    btnToShelf.IsVisible = true;
                    btnInfo.IsVisible = true;
                    btnToShelf.TranslateTo(0, 20, 300);
                    btnInfo.TranslateTo(0, 20, 300);
                    break;
                case false:
                    btnToShelf.TranslateTo(0, -20, 300);
                    await btnInfo.TranslateTo(0, -20, 300);
                    btnToShelf.IsVisible = false;
                    btnInfo.IsVisible = false;
                    break;
            }
        }

        private void OnTwiceTapped(object sender, EventArgs e)
        {
            book.SetBookmark(scroller.ScrollY);
            PopUp("Success");
        }

        private async void OnBtnToShelfClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ShelfPage());
            btnInfo.IsVisible = false;
            btnToShelf.IsVisible = false;
        }

        private void OnBtnInfoClicked(object sender, EventArgs e)
        {
            PopUp("Double tap to set a bookmark");
        }

        private void OnScrollViewScrolled(object sender, EventArgs e)
        {
            UpdateStatistics();
        }

        private void PopUp(string text)
        {
            lblPopUp.Text = text;
            popUp.IsVisible = true;
            Timer timer = new Timer(obj => {
                MainThread.BeginInvokeOnMainThread(() => popUp.IsVisible = false);
            }, null, 2000, Timeout.Infinite);
        }

        private void UpdateStatistics()
        {                
            progressBar.Progress = 
                (scroller.ContentSize.Height == 0) ? 0 
                                                   : scroller.ScrollY / scroller.ContentSize.Height;
            lblPageCount.Text = $"Page {GetCurrentPageNumber()} of {book.Pages}";
        }

        private int GetCurrentPageNumber()
        {
            int pageNumber = (int)Math.Ceiling(scroller.ScrollY / (scroller.ContentSize.Height / book.Pages));
            return pageNumber == 0 ? 1 : pageNumber;
        }
    }
}