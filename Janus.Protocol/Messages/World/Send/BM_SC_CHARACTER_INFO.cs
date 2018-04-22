using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;
using Janus.Protocol.Types;

namespace Janus.Protocol.Messages.World.Send
{
    public class BM_SC_CHARACTER_INFO : NetworkMessage
    {
        public const ushort Id = 2147;

        //16 bytes empty
        public string charName;
        public int unk4 = 0;
        public int unk5 = 0;

        public byte top;
        public byte charType;
        public byte bottom;
        public int unk8 = 1;
        public int unk9 = 1;
        public int unk10 = 1;
        public int unk11 = 1;
        public int unk12 = 1;
        public int unk13 = 1;
        public int unk14 = 1;
        public int unk15 = 1;
        public int charLevel;
        //20 bytes empty

        public int head;
        public int face;
        public int upper;
        public int lower;
        public int foot;
        public int hand;
        public int google;
        public int accesoire;
        public int theme;
        public int mantle;
        public int buckle;
        public int vent;
        public int nitro;
        public int wheels;
        public byte unk21 = 1;
        public int unk22 = 1;
        //public int trickSize = 13;
        public IEnumerable<TrickInfoType> trickInfoTypes;

        public BM_SC_CHARACTER_INFO()
        {
        }

        public BM_SC_CHARACTER_INFO(string charName, byte top, byte charType, byte bottom, int charLevel, int head, int face, int upper, int lower, int foot, int hand, int google, int accesoire, int theme, int mantle, int buckle, int vent, int nitro, int wheels, IEnumerable<TrickInfoType> trickInfoTypes)
        {
            this.charName = charName;
            this.top = top;
            this.charType = charType;
            this.bottom = bottom;
            this.charLevel = charLevel;
            this.head = head;
            this.face = face;
            this.upper = upper;
            this.lower = lower;
            this.foot = foot;
            this.hand = hand;
            this.google = google;
            this.accesoire = accesoire;
            this.theme = theme;
            this.mantle = mantle;
            this.buckle = buckle;
            this.vent = vent;
            this.nitro = nitro;
            this.wheels = wheels;
            this.trickInfoTypes = trickInfoTypes;
        }

        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.WorldServer };

        public override void Deserialize(IDataReader reader)
        {
            throw NetworkMessageException.DeserializeException;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTFBytes(successString);
            writer.WriteBytes(new byte[16]);
            writer.WriteUTFBytes(charName, 40);
            writer.WriteInt(unk4);
            writer.WriteInt(unk5);
            writer.WriteByte((byte)top);
            writer.WriteByte((byte)charType);
            writer.WriteByte((byte)bottom);
            writer.WriteInt(unk8);
            writer.WriteInt(unk9);
            writer.WriteInt(unk10);
            writer.WriteInt(unk11);
            writer.WriteInt(unk12);
            writer.WriteInt(unk13);
            writer.WriteInt(unk14);
            writer.WriteInt(unk15);
            writer.WriteInt(charLevel);
            writer.WriteBytes(new byte[20]);
            writer.WriteInt(head);
            writer.WriteInt(face);
            writer.WriteInt(upper);
            writer.WriteInt(lower);
            writer.WriteInt(foot);
            writer.WriteInt(hand);
            writer.WriteInt(google);
            writer.WriteInt(accesoire);
            writer.WriteInt(theme);
            writer.WriteInt(mantle);
            writer.WriteInt(buckle);
            writer.WriteInt(vent);
            writer.WriteInt(nitro);
            writer.WriteInt(wheels);
            writer.WriteByte((byte)unk21);
            writer.WriteInt(unk22);
            writer.WriteInt(trickInfoTypes.Count());

            foreach(TrickInfoType trick in trickInfoTypes)
            {
                trick.Serialize(writer);
            }
        }
    }
}
