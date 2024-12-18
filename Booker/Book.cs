﻿using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace Booker
{
    public class Book
    {
        public string Id { get; set; } = String.Empty;
        public string Title { get; set; } = String.Empty;
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

        public void Delete()
        {
            Filer.DeleteFile(Title);
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
            Filer.DeleteFile(book.Title);
            Books.Remove(book);
        }

        public bool Contains(Book book)
        {
            foreach (Book b in Books)
            {
                if (b.Title == book.Title)
                    return true;
            }
            return false;
        }

        public Book GetBookById(string id)
        {
            var books = Books.Where(b => b.Id == id);
            if (books.Count() > 0)
                return books.Single();
            else
                return new Book();
        }
    }
}
