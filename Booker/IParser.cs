
namespace Booker
{
    interface IParser<T> where T : class
    {
        T Parse();
    }
}
