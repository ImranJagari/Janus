using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janus.Protocol.Enums
{
    public enum RoomModeEnum
    {
        Speed_Solo = 0,
        Item_Solo,
        Speed_Team,
        Item_Team,
        Random = 6
    }

    public enum RoomModeListEnum
    {
        Speed_Solo = 37,
        Speed_Solo_private = 38,
        Item_Solo = 41,
        Item_Solo_private = 42,
        Speed_Team = 69,
        Speed_Team_private = 70,
        Item_Team = 73,
        Item_Team_private = 74
    }
}
