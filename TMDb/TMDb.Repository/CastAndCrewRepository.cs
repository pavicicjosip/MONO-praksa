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
        public async Task<List<CastAndCrew>> SelectAsync(ICastAndCrewFacade castAndCrewFacade)
        {
            await connection.OpenAsync();
            List<CastAndCrew> _out = new List<CastAndCrew>();

            string sql = castAndCrewFacade.SQLStatement();

            SqlCommand command = new SqlCommand( sql  , connection);

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
        
    }
}
