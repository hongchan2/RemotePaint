using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ShapeClass;

namespace PacketDefine
{
    public static class Constant
    {
        public const int PACKET_SIZE = 1024 * 40;
        public const int INIT_PACKET_SIZE = 1024 * 200;
    }

    public enum PacketType
    {
        init = 0,
        panelInfo,
        indexInfo,
        chatInfo
    }

    public enum PacketSendERROR
    {
        normal = 0,
        error
    }

    [Serializable]
    public class Packet
    {
        public int Length;
        public int Type;

        public Packet()
        {
            this.Length = 0;
            this.Type = 0;
        }

        public static byte[] Serialize(Object o)
        {
            MemoryStream ms = new MemoryStream(Constant.PACKET_SIZE);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, o);
            return ms.ToArray();
        }

        public static Object Desserialize(byte[] bt)
        {
            MemoryStream ms = new MemoryStream(Constant.PACKET_SIZE);
            foreach (byte b in bt)
            {
                ms.WriteByte(b);
            }

            ms.Position = 0;
            BinaryFormatter bf = new BinaryFormatter();
            Object obj = bf.Deserialize(ms);
            ms.Close();
            return obj;
        }
    }

    [Serializable]
    public class Initialize : Packet
    {
        public string clientID = "";
    }

    [Serializable]
    public class PanelInit : Packet
    {
        public MyPencil[] mypencil;
        public MyLines[] mylines;
        public MyRect[] myrect;
        public MyCircle[] mycircle;

        public int npencil;
        public int nline;
        public int nrect;
        public int ncircle;
        public int index;
    }

    [Serializable]
    public class PanelInfo : Packet
    {
        public MyPencil mypencil;
        public MyLines mylines;
        public MyRect myrect;
        public MyCircle mycircle;

        public int npencil;
        public int nline;
        public int nrect;
        public int ncircle;
        public int index;

        /*
         * 1 - pencil
         * 2 - line
         * 3 - rect
         * 4 - circle
         */
        public int changedValue;
    }

    [Serializable]
    public class IndexInfo : Packet
    {
        public int npencil;
        public int nline;
        public int nrect;
        public int ncircle;
        public int index;
    }

    [Serializable]
    public class ChatInfo : Packet
    {
        public string clientID;
        public string msg;
    }
}
