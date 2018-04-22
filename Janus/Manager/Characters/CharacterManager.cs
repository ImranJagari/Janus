using Janus.Databases.Characters;
using Janus.Server.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janus.Manager.Characters
{
    public class CharacterManager : DatabaseManager<CharacterManager>
    {
        public CharacterRecord GetCharacterByAccountId(int accountId)
        {
            return Database.Fetch<CharacterRecord>(string.Format(CharacterRecordRelator.FetchQueryByAccountId, accountId)).FirstOrDefault();
        }
    }
}
