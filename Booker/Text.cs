using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Booker
{
    public static class Text
    {
        public static int GetPageCount(string text) // assuming that a single page holds approximately 300 words
        {
            int wordCount = 0, index = 0;

            while (index < text.Length)
            {
                while (index < text.Length && !char.IsWhiteSpace(text[index]))
                    index++;

                wordCount++;

                while (index < text.Length && char.IsWhiteSpace(text[index]))
                    index++;
            }

            return (int)Math.Ceiling((double)wordCount / Constants.WordsPerPage);
        }
    }
}
