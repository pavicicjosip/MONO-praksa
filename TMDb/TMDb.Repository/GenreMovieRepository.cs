using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Repository.Common;
using TMDb.Model;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Win32;
using System.Data;

namespace TMDb.Repository
{
    public class GenreMovieRepository : IGenreMovieRepository
    {
        private SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AzureConnectionString"].ConnectionString);

        public async Task InsertGenreMovieAsync(GenreMovie genreMovie)
        {
            await connection.OpenAsync();

            SqlCommand command = new SqlCommand("p_InsertGenreMovie", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("@MovieID", genreMovie.MovieID));
            command.Parameters.Add(new SqlParameter("@GenreID", genreMovie.GenreID));
            await command.ExecuteNonQueryAsync();
            connection.Close();
        }

        public async Task<List<string>> GetGenreOfMovieAsync(Guid movieID)
        {
            await connection.OpenAsync();
            List<string> list = new List<string>();
            var command = new SqlCommand("p_SelectGenreOfMovie", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("@MovieID", movieID));
            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    list.Add(reader.GetString(0));
                }
            }
            reader.Close();
            connection.Close();
            return list;
        }
        public async Task RemoveGenreMovieAsync(Guid movieID, Guid genreID)
        {
            await connection.OpenAsync();

            SqlCommand command = new SqlCommand("p_DeleteGenreMovie", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("@MovieID", movieID));
            command.Parameters.Add(new SqlParameter("@GenreID", genreID));
            await command.ExecuteNonQueryAsync();
            connection.Close();
        }
    }
}
