using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ClientAccounts
{
    [DataContract]
    public class ClientAccount
    {
        [DataMember]
        public string AccountNumber;

        [DataMember]
        public string Name;

        [DataMember]
        public string PhoneNumber;
    }

    public struct ClientAccounts
    {
        public Dictionary<string, ClientAccount> accounts;

        public bool IsValidAccount(ClientAccount account)
        {
            if(account == null || string.IsNullOrEmpty(account.AccountNumber) || 
                string.IsNullOrEmpty(account.Name) || string.IsNullOrEmpty(account.PhoneNumber))
            {
                return false;
            }

            return true;
        }

        public void LoadAccounts(string fileName)
        {
            accounts = JsonHelper.ReadJsonToObject<Dictionary<string, ClientAccount>>(fileName);
        }

        public bool CreateAccount(ClientAccount account)
        {
            if(!accounts.ContainsKey(account.AccountNumber))
            {
                accounts.Add(account.AccountNumber, account);
                return true;
            }

            return false;
        }

        public ClientAccount RetrieveAccount(string accountNumber)
        {
            if(accounts.ContainsKey(accountNumber))
            {
                return accounts[accountNumber];
            }
            else
            {
                return null;
            }
        }

        public bool UpdateAccount(ClientAccount account)
        {
            if(accounts.ContainsKey(account.AccountNumber))
            {
                accounts[account.AccountNumber] = account;
                return true;
            }

            return false;
        }

        public bool DeleteAccount(string accountNumber)
        {
            if(accounts.ContainsKey(accountNumber))
            {
                accounts.Remove(accountNumber);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SaveAccountsToFile(string fileName)
        {
            return JsonHelper.WriteJsonToFile<Dictionary<string, ClientAccount>>(fileName, accounts);
        }
    }
}
