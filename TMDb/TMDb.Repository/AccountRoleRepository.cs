﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using TMDb.Model;
using TMDb.Repository.Common;

namespace TMDb.Repository
{
    public class AccountRoleRepository : IAccountRoleRepository
    {
        private SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AzureConnectionString"].ConnectionString);

        public async Task DeleteAccountAsync(Guid accountID, string role)
        {
            await connection.OpenAsync();

            SqlCommand command = new SqlCommand("p_DeleteAccountRole", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("@AccountID", accountID));
            command.Parameters.Add(new SqlParameter("@Role", role));
            await command.ExecuteNonQueryAsync();
            connection.Close();
        }

        public async Task<List<string>> GetRoleByAccountIdAsync(Guid accountID)
        {
            await connection.OpenAsync();
            List<string> list = new List<string>();
            var command = new SqlCommand("p_GetRoleById", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("@AccountID", accountID));
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
        public async Task UpdateAccountRoleAsync(AccountRole accountRole)
        {
            connection.Open();
            var command = new SqlCommand(string.Format("UPDATE AccountRole SET Role = '{0}'" +
                "WHERE AccountID = '{1}'", accountRole.Role, accountRole.AccountID), connection);
            await command.ExecuteReaderAsync();
            connection.Close();
        }
    }
}
