using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AKMapEditor.OtMapEditor
{
    public class PropertyReader : BinaryReader
    {
        public PropertyReader(Stream stream)
            : base(stream) { }

        public string GetString()
        {
            var len = ReadUInt16();
            return Encoding.Default.GetString(ReadBytes(len));
        }

        public Position ReadPosition()
        {
            return new Position(ReadUInt16(), ReadUInt16(), ReadByte());
        }
    }
}
