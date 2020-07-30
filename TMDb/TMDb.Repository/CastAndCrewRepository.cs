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
using TMDb.Common.CastAndCrew;

namespace TMDb.Repository 
{
    public class CastAndCrewRepository : ICastAndCrewRepository
    {
        private SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AzureConnectionString"].ConnectionString);
        public async Task<List<CastAndCrew>> SelectAsync(int pageNumberStart, int pageNumberEnd, ICastAndCrewFacade castAndCrewFacade)
        {
            await connection.OpenAsync();
            List<CastAndCrew> _out = new List<CastAndCrew>();

            string sql = castAndCrewFacade.SQLStatement();

            SqlCommand command = new SqlCommand(
                "SELECT * FROM " +
                " ( " + sql + " ) AS RowConstrainedResult " +
                String.Format("WHERE   RowNum > {0} ", pageNumberStart )+
                String.Format("AND RowNum <= {0} ", pageNumberEnd),
                connection);



            SqlDataReader reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    _out.Add(new CastAndCrew( reader.GetGuid(1), reader.GetString(2), reader.GetString(3), reader.GetDateTime(4).Date , reader.GetString(5), reader.GetGuid(6)));
                }
            }

            reader.Close();
            connection.Close();

            return _out;
        }

        public async Task<int> HowMany()
        {
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand("p_HowManyCastAndCrew", connection);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = await command.ExecuteReaderAsync();
            reader.Read();
            int howMany = reader.GetInt32(0);
            reader.Close();
            connection.Close();
            return howMany;
        }

        public async Task InsertAsync(CastAndCrew castAndCrew)
        {
            await connection.OpenAsync();

            SqlCommand command = new SqlCommand("p_InsertCastAndCrew", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@FirstName", castAndCrew.FirstName));
            command.Parameters.Add(new SqlParameter("@LastName", castAndCrew.LastName));
            command.Parameters.Add(new SqlParameter("@DateOfBirth", castAndCrew.DateOfBirth.Date.ToString("yyyy-MM-dd")));
            command.Parameters.Add(new SqlParameter("@Gender", castAndCrew.Gender));
            command.Parameters.Add(new SqlParameter("@FileID", castAndCrew.FileID));

            await command.ExecuteNonQueryAsync();
            connection.Close();
        }

        public async Task UpdateAsync(Guid castID, CastAndCrew castAndCrew)
        {
            await connection.OpenAsync();

            SqlCommand command = new SqlCommand("p_UpdateCastAndCrew", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@CastID", castID));
            command.Parameters.Add(new SqlParameter("@FirstName", castAndCrew.FirstName));
            command.Parameters.Add(new SqlParameter("@LastName", castAndCrew.LastName));
            command.Parameters.Add(new SqlParameter("@DateOfBirth", castAndCrew.DateOfBirth.Date.ToString("yyyy-MM-dd")));
            command.Parameters.Add(new SqlParameter("@Gender", castAndCrew.Gender));
            command.Parameters.Add(new SqlParameter("@FileID", castAndCrew.FileID));

            await command.ExecuteNonQueryAsync();
            connection.Close();
        }

        public async Task DeleteAsync(Guid castID)
        {
            await connection.OpenAsync();

            SqlCommand command = new SqlCommand("p_DeleteCastAndCrew", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@CastID", castID));

            await command.ExecuteNonQueryAsync();
            connection.Close();
        }

    }
}
