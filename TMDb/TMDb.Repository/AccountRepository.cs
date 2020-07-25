
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
    public class AccountRepository : IAccountRepository
    {
        private SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AzureConnectionString"].ConnectionString);

        public async Task<Account> SelectAccountAsync(Account acc)
        {

            SqlCommand command = new SqlCommand("p_SelectByUserAndPass", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Username", acc.Username));
            command.Parameters.Add(new SqlParameter("@UserPassword", acc.UserPassword));

            Account account = new Account();

            await connection.OpenAsync();
            SqlDataReader reader = await command.ExecuteReaderAsync();

            await reader.ReadAsync();

            account = new Account(reader.GetGuid(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetGuid(4));
            reader.Close();
            connection.Close();
            return account;

        }

        public async Task<Account> SelectAccountAsync(Guid accountID)
        {

            SqlCommand command = new SqlCommand("p_SelectByAccountID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@AccountID", accountID));

            Account account = new Account();

            await connection.OpenAsync();
            SqlDataReader reader = await command.ExecuteReaderAsync();

            await reader.ReadAsync();

            account = new Account(reader.GetGuid(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetGuid(4));
            reader.Close();
            connection.Close();
            return account;

        }


        public async Task DeleteAccountAsync(Guid accountID)
        {
            SqlCommand command = new SqlCommand("p_DeleteAccount", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@AccountID", accountID));

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
            connection.Close();
        }

        public async Task UpdateAccountAsync(Account acc)
        {

            await connection.OpenAsync();

            SqlCommand command = new SqlCommand("p_UpdateAccount", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@AccountID", acc.AccountID));
            command.Parameters.Add(new SqlParameter("@Email", acc.Email));
            command.Parameters.Add(new SqlParameter("@Username", acc.Username));
            command.Parameters.Add(new SqlParameter("@UserPassword", acc.UserPassword));
            command.Parameters.Add(new SqlParameter("@FileID", acc.FileID));

            await command.ExecuteNonQueryAsync();
            connection.Close();
        }

    }
}