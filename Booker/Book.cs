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

        public void SetAsPreferenced()
        {
            Preferences.Default.Set("last", Id);
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
            List<Book> bk = Books.Where(b => b.Id == id).ToList();
            if (bk.Count > 0)
                return bk[0];
            else
                return new Book();
        }
    }
}
