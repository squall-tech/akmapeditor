using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace AKMapEditor.OtMapEditorServer.Classes
{
    [ProtoContract]
    public class ServerInformation
    {
        [ProtoMember(1)]
        public String MapName { get; set; }
        [ProtoMember(2)]
        public String ErrorLogin { get; set; }
        [ProtoMember(3)]
        public UInt16 MapHeight { get; set; }
        [ProtoMember(4)]
        public UInt16 MapWidth { get; set; }
    }
}
