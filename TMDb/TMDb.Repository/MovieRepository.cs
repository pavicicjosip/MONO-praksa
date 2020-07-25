
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Model;
using TMDb.Repository.Common;

namespace TMDb.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AzureConnectionString"].ConnectionString);
        public async Task<List<Movie>> SelectMovieByTitleAsync(string title)
        {
            var list = new List<Movie>();
            var command = new SqlCommand("p_GetMovieByName", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Title", title);
            connection.Open();
            SqlDataReader reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    list.Add(new Movie(reader.GetGuid(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetGuid(6)));
                }
            }

            reader.Close();
            connection.Close();
            return list;
        }

        public async Task<List<Movie>> SelectMovieByYearAsync(int yearOfProduction)
        {
            var list = new List<Movie>();
            var command = new SqlCommand("p_GetMovieByYear", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@YearOfProduction", yearOfProduction);
            connection.Open();
            SqlDataReader reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    list.Add(new Movie(reader.GetGuid(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetGuid(6)));
                }
            }

            reader.Close();
            connection.Close();
            return list;
        }

    }
}