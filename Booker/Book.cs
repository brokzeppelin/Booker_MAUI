using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booker
{
    public class Book
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public double Bookmark { get; private set; }

        public Book(string id, string title)
        {
            Id = id;
            Title = title;
            Bookmark = 0.0;  // Default value for bookmark
        }
        public void SetBookmark(double position)
        {
            if (position >= 0.0)
            {
                Bookmark = position;
            }
            else
            {
                throw new ArgumentException("Bookmark position cannot be negative.");
            }
        }
        public double GetBookmark()
        {
            return Bookmark;
        }
    }
}
