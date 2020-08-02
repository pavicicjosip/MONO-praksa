using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Repository.Common;
using TMDb.Model;
using TMDb.Service.Common;
using System.Security.Cryptography;
using TMDb.Common.Account;
using TMDb.Common;

namespace TMDb.Service
{
    public class AccountService : IAccountService
    {
        protected IAccountRepository AccountRepository { get; set; }

        public AccountService(IAccountRepository accountRepository)
        {
            this.AccountRepository = accountRepository;
        }

        public async Task<Account> SelectAccountAsync(IAccountFacade iAccountFacade)
        {
            if (iAccountFacade.UserPassword.UserPassword != "")
                iAccountFacade.UserPassword.UserPassword = Sha256Hash(iAccountFacade.UserPassword.UserPassword);
            string whereStatement = iAccountFacade.WhereStatement();

            return await AccountRepository.SelectAccountAsync(whereStatement);
        }

        public async Task DeleteAccountAsync(Guid accountID)
        {
            await AccountRepository.DeleteAccountAsync(accountID);
        }

        public async Task UpdateAccountAsync(Account acc)
        {
            Account account = await AccountRepository.SelectAccountAsync(" WHERE AccountID = " + "'" + acc.AccountID + "'");


            if (acc.Email != "")
                account.Email = acc.Email;
            if (acc.UserName != "")
                account.UserName = acc.UserName;
            if (acc.UserPassword != "")
                account.UserPassword = Sha256Hash(acc.UserPassword);
            if (acc.FileID.ToString() != "")
                account.FileID = acc.FileID;

            await AccountRepository.UpdateAccountAsync(account);
        }

        public async Task InsertAccountAsync(Account acc)
        {
            acc.UserPassword = Sha256Hash(acc.UserPassword);
            await AccountRepository.InsertAccountAsync(acc);
        }

        static string Sha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}