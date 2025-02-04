using Dapper;
using EncurtaUrl.Models;
using Microsoft.Data.SqlClient;
using System.Text;

namespace EncurtaUrl.Interfaces.Repository
{
    public class ShortRepository : IShortRepository
    {
        private static readonly string Base62Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private readonly string _connectionString;
        public ShortRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CloudConnection");
        }

        public string CreteEncondig(string s)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(s);

            long number = 0;
            foreach (byte b in bytes)
            {
                number = number * 256 + b;

            }

            StringBuilder encoded = new StringBuilder();
            while (number > 0)
            {
                int reminder = (int)(number % 62);
                encoded.Insert(0, Base62Chars[reminder]);
                number /= 62;
            }

            return encoded.Length > 0 ? encoded.ToString() : "0";

        }

        public void Add(ShortClass shortModel)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "INSERT INTO UrlTable (Url, shortUrl) VALUES (@Url, @ShortUrl) ";

               connection.Execute(sql, shortModel);

                
            }
        }

        public string GetLongUrl(string ShortUrl)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT Url FROM UrlTable WHERE shortUrl = @ShortUrl";

                string longUrl = connection.ExecuteScalar<string>(sql,new { ShortUrl });

                return longUrl;

            }



        }


    }
}
