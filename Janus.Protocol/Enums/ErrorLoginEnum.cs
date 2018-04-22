using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janus.Protocol.Enums
{
    public enum ErrorLoginEnum
    {
        SUCCESS_INTERNAL = 0, //THIS WILL BE USED FOR INTERNAL COMMUNICATION!
        MSG_SERVER_NOT_EXIST = 1,// wrong login information passed
        MSG_SERVER_DENIED = 6, // connection denied
        MSG_SERVER_ALREADY_EXIST = 9, // already connected. terminate actual connect, try again
        MSG_SERVER_RETRY = 50, // couldnt connect to server, try later
        MSG_FAIL_WEB_ID = 60, // go to www.gpotato.eu and login
        AURORA_RESULT_REPETITION = 70, // too many login tries, temporarly banned
    }
}
