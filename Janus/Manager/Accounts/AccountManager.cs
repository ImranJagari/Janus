using Janus.Databases.Accounts;
using Janus.Game.Accounts;
using Janus.Server.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janus.Manager.Accounts
{
    public class AccountManager : DatabaseManager<AccountManager>
    {
        public Dictionary<string, Account> dictionaryTicketAccount = new Dictionary<string, Account>();
        public AccountRecord GetAccountRecordByLogin(string login)
        {
            return Database.Fetch<AccountRecord>(string.Format(AccountRecordRelator.FetchQueryByLogin, login)).FirstOrDefault();
        }
        public Account GetTicketAccount(string ticket)
        {
            if (dictionaryTicketAccount.ContainsKey(ticket))
                return dictionaryTicketAccount[ticket];
            return null;
        }
        public void AddTicketAccount(string ticket, Account account)
        {
            if (dictionaryTicketAccount.ContainsValue(account))
                return;
            dictionaryTicketAccount.Add(ticket, account);
        }
        public void RemoveTicketAccount(string ticket)
        {
            if (dictionaryTicketAccount.ContainsKey(ticket))
                dictionaryTicketAccount.Remove(ticket);
        }
    }
}
