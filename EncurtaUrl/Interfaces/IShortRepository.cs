using EncurtaUrl.Models;

namespace EncurtaUrl.Interfaces
{
    public interface IShortRepository
    {
        string CreteEncondig(string s);

        void Add(ShortClass shortModel);

        string GetLongUrl(string ShortUrl);


    }
}
