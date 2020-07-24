﻿using System;
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
    public class ReviewRepository : IReviewRepository
    {
        private SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AzureConnectionString"].ConnectionString);
        public async Task<List<Review>> SelectMovieReviewsAsync(Guid movieID)
        {
            var list = new List<Review>();
            var command = new SqlCommand(
                "SELECT r.ReviewID, r.NumberOfStars, r.Comment, r.DateAndTime, acc.Username, r.MovieID " +
                "FROM Review r, Account acc " +
                "WHERE r.AccountID = acc.AccountID AND r.MovieID = @MovieID", connection);
            command.Parameters.AddWithValue("@MovieID", movieID);
            connection.Open();
            SqlDataReader reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    list.Add(new Review(reader.GetGuid(0), reader.GetInt32(1), reader.GetString(2), reader.GetDateTime(3), reader.GetString(4), reader.GetGuid(5)));
                }
            }

            reader.Close();
            connection.Close();
            return list;
        }
    }
}
