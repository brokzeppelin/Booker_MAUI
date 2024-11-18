using System.Xml;
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

        public static string Serialize(Book book)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Book));
            // Remove namespaces and xml declaration  
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces([XmlQualifiedName.Empty]);

            using (StringWriter swriter = new StringWriter())
            using (XmlWriter writer = XmlWriter.Create(swriter, settings))
            {
                serializer.Serialize(writer, book, ns);
                return swriter.ToString(); 
            }
        }
    }
}
