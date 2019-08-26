using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AKMapEditor.OtMapEditor;
using ProtoBuf;

namespace AKMapEditor.OtMapEditorServer.Classes
{
    [ProtoContract]
    public class MapUpdate
    {
        [ProtoMember(1)]
        public BatchAction batchAction;

        [ProtoMember(2)]
        public int updateType;
    }
}
