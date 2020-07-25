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

namespace TMDb.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AzureConnectionString"].ConnectionString);

        public async Task<Account> SelectAccountAsync(Account acc)
        {
            string sql = "SELECT * FROM Account WHERE UserName = @Username AND UserPassword = @UserPassword; ";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Username", acc.Username);
            command.Parameters.AddWithValue("@UserPassword", acc.UserPassword);

            Account account = new Account();

            await connection.OpenAsync();
            SqlDataReader reader = await command.ExecuteReaderAsync();

            await reader.ReadAsync();

            account = new Account(reader.GetGuid(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetGuid(4));
            reader.Close();
            connection.Close();
            return account;
        }

        /*
        public async Task DeleteAccountAsync(Guid accountID)
        {

        }
        public async Task UpdateAccount(Account acc)
        { 
            
        }
        */
    }
}