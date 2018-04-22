using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.World.Send
{
    public class BM_SC_PLAYER_INFO : NetworkMessage
    {
        public const ushort Id = 2314;

        public static Int32 unk0 = 4;
        public static UInt16 packetSize = 0x2d; //ushort maxvalue
        public static String successCmd = "SUCCESS\0";
        public static Int32 unk1;
        public static Int32 unk2;
        public static Int32 unk3;
        public static Int32 unk4;
        public static Int32 unk5;
        public static Int16 unk6;
        public static Int32 unk7 = 5;

        //Sub Packet One
        public static Int16 subPacketId = 1;
        public static Int16 subPacketSize = 50;
        public static String mapName = "ID1_Testo_2";
        public static Int16 unk8;
        public static Int32 unk9;
        public static Int32 unk10;
        public static Int32 unk11;
        public static Int32 unk12;
        public static Int32 unk13;
        public static Int32 unk14;
        public static Int32 unk15;
        public static String mapName2 = "ID1_Test";

        //Sub Packet Two
        public static Int16 subPacketSecondId = 2;
        public static Int16 subPacketSecondSize = 12;
        public static Int16 unk16 = 5;
        public static Int16 unk17 = 5;
        public static Int32 unk18 = 45;
        public static Int32 playerLevel;

        //Sub Packet Three
        public static Int16 subPacketThirdId = 4;
        public static Int16 subPacketThirdSize = 12;
        public static Int32 unk19 = 5;
        public static Int32 unk20 = 6;
        public static Int32 unk21 = 7;

        //Sub Packet Four
        public static Int16 subPacketFourthId = 64;
        public static Int16 subPacketFourthSize = 16;
        public static Int32 unk22 = 7;
        public static Int32 unk23 = 8;
        public static Int32 unk24 = 9;
        public static Int32 unk25 = 10;

        //Sub Packet Five
        public static Int16 subPacketFifthId = 512;
        public static Int16 subPacketFifthSize = 44;
        public static float speed;
        public static float accel;
        public static float turn;
        public static float brake;
        public static float boostSpeed;
        public static float overSpeed;
        public static float boosterC;
        public static float trick;
        public static float unk26;
        public static float unk27;

        //Sub Packet Six
        public static Int16 subPacketSixthId = 1024;
        public static Int16 subPacketSixthSize = 12;
        public static Int32 count1 = 2;
        public static Int32 count2 = 3;
        public static Int32 count3 = 3;
        public static Int32 count4 = 0;
        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.WorldServer };

        public BM_SC_PLAYER_INFO(Int32 _playerLevel)
        {
            playerLevel = _playerLevel;
        }
        public BM_SC_PLAYER_INFO()
        {
        }
        public override void Deserialize(IDataReader reader)
        {
            throw NetworkMessageException.DeserializeException;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(unk0);
            writer.WriteUShort(packetSize);
            writer.WriteUTFBytes(successCmd);
            writer.WriteInt(unk1);
            writer.WriteInt(unk2);
            writer.WriteInt(unk3);
            writer.WriteInt(unk4);
            writer.WriteInt(unk5);
            writer.WriteShort(unk6);
            writer.WriteInt(unk7);

            writer.WriteShort(subPacketId);
            writer.WriteShort(subPacketSize);
            writer.WriteUTFBytes(mapName.PadRight(11, '\0'));
            writer.WriteShort(unk8);
            writer.WriteInt(unk9);
            writer.WriteInt(unk10);
            writer.WriteInt(unk11);
            writer.WriteInt(unk12);
            writer.WriteInt(unk13);
            writer.WriteInt(unk14);
            writer.WriteInt(unk15);
            writer.WriteUTFBytes(mapName2.PadRight(9, '\0'));

            writer.WriteShort(subPacketSecondId);
            writer.WriteShort(subPacketSecondSize);
            writer.WriteShort(unk16);
            writer.WriteShort(unk17);
            writer.WriteInt(unk18);
            writer.WriteInt(playerLevel);

            writer.WriteShort(subPacketThirdId);
            writer.WriteShort(subPacketThirdSize);
            writer.WriteInt(unk19);
            writer.WriteInt(unk20);
            writer.WriteInt(unk21);

            writer.WriteShort(subPacketFourthId);
            writer.WriteShort(subPacketFourthSize);
            writer.WriteInt(unk22);
            writer.WriteInt(unk23);
            writer.WriteInt(unk24);
            writer.WriteInt(unk25);

            writer.WriteShort(subPacketFifthId);
            writer.WriteShort(subPacketFifthSize);
            writer.WriteFloat(speed);
            writer.WriteFloat(accel);
            writer.WriteFloat(turn);
            writer.WriteFloat(brake);
            writer.WriteFloat(boostSpeed);
            writer.WriteFloat(overSpeed);
            writer.WriteFloat(boosterC);
            writer.WriteFloat(trick);
            writer.WriteFloat(unk26);
            writer.WriteFloat(unk27);
            writer.WriteInt(0);

            writer.WriteShort(subPacketSixthId);
            writer.WriteShort(subPacketSixthSize);
            writer.WriteInt(count1);
            writer.WriteInt(count2);
            writer.WriteInt(count3);
            writer.WriteInt(count4);
        }
    }
}
