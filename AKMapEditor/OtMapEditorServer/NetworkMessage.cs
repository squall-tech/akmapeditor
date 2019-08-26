using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using ProtoBuf;

namespace AKMapEditor.OtMapEditorServer
{
    public class MessageType
    {
        public const int LOGIN = 5;
        public const int SERVER_INFORMATION = 10;
        public const int MAP_REQUEST = 12;
        public const int MAP_RESPONSE = 15;
        public const int MAP_UPDATE = 20;
        public const int CHECK_CONNECTION = 25;        
    }

    [ProtoContract]
    public class EmptyMessage
    {

    }

    public class NetworkMessage
    {

        private NetworkStream stream = null;

        public NetworkMessage(NetworkStream stream)
        {
            this.stream = stream;
        }

        public int ReadType()
        {
            return stream.ReadByte();
        }
    }
}
