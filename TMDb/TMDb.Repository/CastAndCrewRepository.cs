using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Repository.Common;
using TMDb.Model;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Runtime.CompilerServices;

namespace TMDb.Repository 
{
    public class CastAndCrewRepository : ICastAndCrewRepository
    {
        private SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AzureConnectionString"].ConnectionString);
        public async Task<List<CastAndCrew>> SelectByFirstNameAsync(string firstName)
        {
            await connection.OpenAsync();
            List<CastAndCrew> _out = new List<CastAndCrew>();

            SqlCommand command = new SqlCommand("p_GetByFirstName", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@FirstName", firstName));

            SqlDataReader reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    _out.Add(new CastAndCrew( reader.GetGuid(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3).Date , reader.GetString(4), reader.GetGuid(5)));
                }
            }

            reader.Close();
            connection.Close();

            return _out;
        }

        public async Task<List<CastAndCrew>> SelectByLastNameAsync(string lastName)
        {
            await connection.OpenAsync();
            List<CastAndCrew> _out = new List<CastAndCrew>();

            SqlCommand command = new SqlCommand("p_GetByLastName", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@LastName", lastName));

            SqlDataReader reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    _out.Add(new CastAndCrew(reader.GetGuid(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3).Date, reader.GetString(4), reader.GetGuid(5)));
                }
            }

            reader.Close();
            connection.Close();

            return _out;
        }

        public async Task<List<CastAndCrew>> SelectByDateOfBirthAsync(string date)
        {
            await connection.OpenAsync();
            List<CastAndCrew> _out = new List<CastAndCrew>();

            SqlCommand command = new SqlCommand("p_GetByLastName", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@DateOfBirth", date));

            SqlDataReader reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    _out.Add(new CastAndCrew(reader.GetGuid(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3).Date, reader.GetString(4), reader.GetGuid(5)));
                }
            }

            reader.Close();
            connection.Close();

            return _out;  
        }
    }
}
