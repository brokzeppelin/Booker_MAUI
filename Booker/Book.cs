using System;
using System.Collections;
using System.Xml.Serialization;

namespace Booker
{
    public class Book
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public double Bookmark { get; set; }
    }

    [XmlRootAttribute("settings")]
    public class BookCollection : IEnumerable
    {
        [XmlElement("book")]
        public Book[] Books { get; set; }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }
        public void Add(Book book)
        {
        }

        public BookEnumerator GetEnumerator()
        {
            return new BookEnumerator(Books);
        }
    }

    // When you implement IEnumerable, you must also implement IEnumerator.
    public class BookEnumerator : IEnumerator
    {
        public Book[] books;

        // Enumerators are positioned before the first element
        // until the first MoveNext() call.
        int position = -1;

        public BookEnumerator(Book[] list)
        {
            books = list;
        }

        public bool MoveNext()
        {
            position++;
            return (position < books.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public Book Current
        {
            get
            {
                try
                {
                    return books[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
