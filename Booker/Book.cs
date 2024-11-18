using System;
using System.Collections;
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
    }

    [XmlRootAttribute("Settings")]
    public class BookCollection
    {
        [XmlElement("Book")]
        public List<Book> Books { get; set; }
    }
}
