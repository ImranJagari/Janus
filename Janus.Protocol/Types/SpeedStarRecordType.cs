using Janus.Core.Extensions;
using Janus.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janus.Protocol.Types
{
    public class SpeedStarRecordType
    {
        public string name;
        public int timeSpeedRecord;

        public SpeedStarRecordType()
        {
            name = "Server";
            timeSpeedRecord = DateTime.Now.GetUnixTimeStamp();
        }

        public SpeedStarRecordType(string name, int timeSpeedRecord)
        {
            this.name = name;
            this.timeSpeedRecord = timeSpeedRecord;
        }

        public void Serialize(IDataWriter writer)
        {
            writer.WriteUTFBytes(name.PadRight(35, '\0'), 35);
            writer.WriteInt(timeSpeedRecord);
            writer.WriteBytes(new byte[5]);
        }
    }
}
