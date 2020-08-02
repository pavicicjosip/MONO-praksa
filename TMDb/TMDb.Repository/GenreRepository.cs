using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
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
            var sortedList = list.OrderBy(x => x.Title).ToList();
            return sortedList;
        }

        public async Task<Genre> ReturnGenreByTitleAsync(string title)
        {
            Genre genre;
            var command = new SqlCommand(String.Format("SELECT GenreID FROM Genre WHERE Title = '{0}'", title), connection);
            connection.Open();
            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                await reader.ReadAsync();
                genre = new Genre(reader.GetGuid(0), "");
            }
            else
            {
                reader.Close();
                connection.Close();
                return null;
            }

            reader.Close();
            connection.Close();
            return genre;
        }
        public async Task InsertGenreAsync(string title)
        {
            var command = new SqlCommand(String.Format("INSERT INTO Genre(Title) VALUES('{0}')", title), connection);
            connection.Open();
            await command.ExecuteReaderAsync();
            connection.Close();
            return;
        }
        public async Task UpdateGenreAsync(Genre genre)
        {
            connection.Open();
            var command = new SqlCommand(String.Format("UPDATE Genre SET Title = '{0}' " +
                "WHERE GenreID = '{1}'", genre.Title, genre.GenreID), connection);
            await command.ExecuteReaderAsync();
            connection.Close();
        }
    }
}
