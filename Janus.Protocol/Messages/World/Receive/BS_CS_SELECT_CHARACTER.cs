﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.World.Receive
{
    public class BS_CS_SELECT_CHARACTER : NetworkMessage
    {
        public const ushort Id = 2066;

        public int charType;
        
        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.WorldServer };

        public override void Deserialize(IDataReader reader)
        {
            charType = reader.ReadByte();
        }

        public override void Serialize(IDataWriter writer)
        {
            throw NetworkMessageException.SerializeException;
        }
    }
}
