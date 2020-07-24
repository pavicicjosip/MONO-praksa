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
        static string connStr = ConfigurationManager.ConnectionStrings["AzureConnectionString"].ConnectionString;
        static SqlConnection sqlCon = new SqlConnection(connStr);

        public async Task<Account> SelectAccountAsync(string username, string userPassword)
        {
            await sqlCon.OpenAsync();

            string sql = "SELECT * FROM Account WHERE UserName = @Username AND UserPassword = @UserPassword; ";
            SqlCommand command = new SqlCommand(sql, sqlCon);
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@UserPassword", userPassword);
            SqlDataReader reader = await command.ExecuteReaderAsync();

            return new Account(reader.GetGuid(0), reader.GetString(1), reader.GetString(2), reader.GetString(3),  reader.GetGuid(5)  );
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
