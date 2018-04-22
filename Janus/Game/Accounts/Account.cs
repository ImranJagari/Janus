using Janus.Databases.Accounts;
using Janus.Game.Characters;
using Janus.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janus.Game.Accounts
{
    public class Account
    {
        public string SessionKey { get; }
        public AccountRecord Record { get; }

        public Account(AccountRecord record, string sessionKey)
        {
            Record = record;
            SessionKey = sessionKey;
        }

        public int Id
        {
            get
            {
                return Record.Id;
            }
        }
        public string Username
        {
            get
            {
                return Record.Username;
            }
            set
            {
                Record.Username = value;
            }
        }
        public string PasswordHash
        {
            get
            {
                return Record.PasswordHash;
            }
            set
            {
                Record.PasswordHash = value;
            }
        }
        public PermissionEnum Role
        {
            get
            {
                return (PermissionEnum)Record.Role;
            }
            set
            {
                Record.Role = (byte)value;
            }
        }

        public DateTime? BanTime
        {
            get
            {
                return Record.BanTime;
            }
            set
            {
                Record.BanTime = value;
            }
        }
        public Character Character
        {
            get;
            set;
        }
    }
}
