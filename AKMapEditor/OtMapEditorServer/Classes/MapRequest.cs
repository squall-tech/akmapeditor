using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AKMapEditor.OtMapEditor;
using ProtoBuf;

namespace AKMapEditor.OtMapEditorServer.Classes
{
    [ProtoContract]
    public class MapRequest
    {
        [ProtoMember(1)]
        public List<Position> positions;

    }
}
