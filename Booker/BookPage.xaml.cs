namespace Booker
{
    public partial class BookPage : ContentPage
    {
        string lastOpenedId = Preferences.Get("lastOpenedId", "noId");
        public BookPage()
        {
            InitializeComponent();
            //LoadBook(lastOpenedId);
        }

        //public void LoadBook(string bookId)
        //{
        //    string bookText;
        //    bookBox.Add(
        //        new Label { Text = bookText}
        //        );
        //}

    }

}
