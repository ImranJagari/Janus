using Janus.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janus.Protocol.Types
{
    public class PlayerGameInfoType
    {
        public int uk1 = 1;
        public int uk2 = 1;
        public int uk3 = 1;
        public int uk4 = 1;
        public int uk5 = 1;
        public int uk6 = 1;
        public int uk7 = 1;
        public int uk8 = 5000;
        public byte uk9 = 1;
        public string name;//40
        public short uk10 = 15; //15
        public string remoteendpoint;//16
        public int uk11 = 1; //1
        public short uk12 = 5000; //1
        public short index_p;
        public byte uk14 = 1; //1
        public byte ismaster;

        public PlayerGameInfoType(string name, string remoteendpoint, short index_p, byte ismaster)
        {
            this.name = name;
            this.remoteendpoint = remoteendpoint;
            this.index_p = index_p;
            this.ismaster = ismaster;
        }

        public PlayerGameInfoType(int uk1, int uk2, int uk3, int uk4, int uk5, int uk6, int uk7, int uk8, byte uk9, string name, short uk10, string remoteendpoint, int uk11, short uk12, short index_p, byte uk14, byte ismaster)
        {
            this.uk1 = uk1;
            this.uk2 = uk2;
            this.uk3 = uk3;
            this.uk4 = uk4;
            this.uk5 = uk5;
            this.uk6 = uk6;
            this.uk7 = uk7;
            this.uk8 = uk8;
            this.uk9 = uk9;
            this.name = name;
            this.uk10 = uk10;
            this.remoteendpoint = remoteendpoint;
            this.uk11 = uk11;
            this.uk12 = uk12;
            this.index_p = index_p;
            this.uk14 = uk14;
            this.ismaster = ismaster;
        }

        //101 bytes not used but must be sended

        public void Serialize(IDataWriter writer)
        {
            writer.WriteInt(uk1);
            writer.WriteInt(uk2);
            writer.WriteInt(uk3);
            writer.WriteInt(uk4);
            writer.WriteInt(uk5);
            writer.WriteInt(uk6);
            writer.WriteInt(uk7);
            writer.WriteInt(uk8);
            writer.WriteByte(uk9);
            writer.WriteUTFBytes(name, 40);
            writer.WriteShort(uk10);
            writer.WriteUTFBytes(remoteendpoint, 16);
            writer.WriteInt(uk11);
            writer.WriteShort(uk12);
            writer.WriteShort(index_p);
            writer.WriteByte(uk14);
            writer.WriteByte(ismaster);
        }
    }
}
