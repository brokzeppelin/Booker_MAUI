using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace Booker
{
    public class Book
    {
        public string Id { get; set; } = String.Empty;
        public string Extention {  get; set; } = String.Empty;
        public string Title { get; set; } = String.Empty;
        public int Words { get; set; } = 0;
        public double Bookmark { get; set; } = 0;

        public void RegisterToXML()
        {            
            Filer.InsertBookElement(XmlParser.Serialize(this));
        }

        public void DeregisterFromXML()
        {
            Filer.RemoveBookElement(XmlParser.Serialize(this));
        }

        public void SetAsPreferenced()
        {
            Preferences.Default.Set("last", Id);
        }

        public void UnsetAsPreferenced()
        {
            if (Preferences.Default.Get("last", String.Empty) == Id)
                Preferences.Default.Set("last", String.Empty);
        }

        public void SetBookmark(double value)
        {
            Bookmark = value;
            Filer.ChangeElemenlValueById(Id, value);
        }

        public string GetFilename()
        {
            if (this.Title != String.Empty || this.Extention != String.Empty)
                return String.Concat(this.Title, this.Extention);
            else
                return String.Empty;
        }

        public void Delete()
        {
            Filer.DeleteFile(this.GetFilename());
        }
    }

    [XmlRootAttribute("Settings")]
    public class BookCollection
    {
        [XmlElement("Book")]
        public ObservableCollection<Book> Books { get; set; } = [];

        public void Add(Book book)
        {
            Books.Add(book);
        }

        public void Remove(Book book)
        {
            book.DeregisterFromXML();
            book.UnsetAsPreferenced();
            book.Delete();
            Books.Remove(book);
        }

        public bool Contains(Book book)
        {
            foreach (Book b in Books)
            {
                if (b.GetFilename() == book.GetFilename())
                    return true;
            }
            return false;
        }

        public Book GetBookById(string id)
        {
            var books = Books.Where(b => b.Id == id);
            return books.Any() ? books.Single() : new Book();
        }
    }
}
