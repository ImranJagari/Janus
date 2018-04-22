using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janus.Protocol.Enums
{
    public class DailyQuestCoinResultEnum
    {
        public const string Success = "SUCCESS\0";
        public const string GettedAlready = "ALREADY_GET_COIN\0";
        public const string MaxCoinsReached = "HAVE_MAX_COIN\0";
    }
}
