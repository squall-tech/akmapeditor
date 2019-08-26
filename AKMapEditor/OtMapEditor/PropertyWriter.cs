using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AKMapEditor.OtMapEditor
{
    public class PropertyWriter : BinaryWriter
    {
        public PropertyWriter(Stream stream)
            : base(stream) { }

        public override void Write(string value)
        {
            Write((ushort)value.Length);
            Write(Encoding.Default.GetBytes(value));
        }

        public void Write(Position position)
        {
            Write((ushort)position.X);
            Write((ushort)position.Y);
            Write((byte)position.Z);
        }
    }
}
