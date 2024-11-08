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
    public class BookCollection
    {
        [XmlElement("book")]
        public List<Book> Books { get; set; }
    }
}
