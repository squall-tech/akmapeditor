using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace AKMapEditor.OtMapEditorServer.Classes
{
    [ProtoContract]
    public class Login
    {
        [ProtoMember(1)]
        public String Password { get; set; }
        [ProtoMember(2)]
        public String AppVersion { get; set; }
    }
}
