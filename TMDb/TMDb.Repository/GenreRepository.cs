using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Model;
using TMDb.Repository.Common;

namespace TMDb.Repository
{
    public class GenreRepository : IGenreRepository
    {
        private SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AzureConnectionString"].ConnectionString);

        public async Task<List<Genre>> ReturnAllGenresAsync()
        {
            var list = new List<Genre>();
            var command = new SqlCommand("SELECT * FROM Genre", connection);
            connection.Open();
            SqlDataReader reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    list.Add(new Genre(reader.GetGuid(0), reader.GetString(1)));
                }
            }

            reader.Close();
            connection.Close();
            return list;
        }
    }
}
