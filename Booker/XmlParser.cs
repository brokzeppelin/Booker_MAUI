
using System.Xml.Serialization;

namespace Booker
{
    public class XmlParser : IParser<BookCollection>
    {
        public BookCollection Parse()
        {            
            XmlSerializer serializer = new XmlSerializer(typeof(BookCollection));

            using (Stream reader = new FileStream(Path.Combine(FileSystem.Current.AppDataDirectory, Constants.UserFolder, Constants.SettingsFile), FileMode.Open))
            {
                return serializer?.Deserialize(reader) as BookCollection ?? new BookCollection();
            }
        }
    }
}
