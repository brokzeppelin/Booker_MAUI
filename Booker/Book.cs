using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public Book[] Books { get; set; }
    }
}
