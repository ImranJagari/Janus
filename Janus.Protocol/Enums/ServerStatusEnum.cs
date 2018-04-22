using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janus.Protocol.Enums
{
    public enum ServerStatusEnum
    {
        CONNECTION_SUCCESS = 0, // continue to login screen
        CONNECTION_BROKEN = 1, // connection to server broken/lost. please login again
    }
}
