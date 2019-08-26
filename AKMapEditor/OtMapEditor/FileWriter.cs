using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AKMapEditor.OtMapEditor
{
    public class FileWriter : IDisposable
    {
        private FileStream fileStream;

        public FileWriter(string fileName)
        {
            fileStream = File.Open(fileName, FileMode.Create);
        }

        public void Close()
        {
            try
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                    fileStream = null;
                }
            }
            catch (Exception ex)
            {
                Messages.AddWarning("[Error] Unable to close file. Details: " + ex.Message);                
            }
        }

        public PropertyWriter GetPropertyWriter()
        {
            return new PropertyWriter(fileStream);
        }

        public void WriteNodeStart(byte type)
        {
            Write(BinaryNode.NODE_START, false);
            Write(type);
        }

        public void WriteNodeEnd()
        {
            Write(BinaryNode.NODE_END, false);
        }

        public void Write(byte[] data, bool unescape)
        {
            foreach (byte b in data)
            {
                if (unescape && (b == BinaryNode.NODE_START || b == BinaryNode.NODE_END || b == BinaryNode.ESCAPE))
                {
                    fileStream.WriteByte(BinaryNode.ESCAPE);
                }

                fileStream.WriteByte(b);
            }
        }

        public void Write(byte[] data)
        {
            Write(data, true);
        }

        public void Write(byte value)
        {
            Write(value, true);
        }

        public void Write(byte value, bool unescape)
        {
            Write(new byte[] { value }, unescape);
        }

        public void Write(Position location)
        {
            Write((ushort)location.X);
            Write((ushort)location.Y);
            Write((byte)location.Z);
        }

        public void Write(UInt16 value)
        {
            Write(BitConverter.GetBytes(value));
        }

        public void Write16(UInt16 value)
        {
            Write(BitConverter.GetBytes(value));
        }

        public void Write(UInt32 value)
        {
            Write(value, true);
        }

        public void Write(UInt32 value, bool unescape)
        {
            Write(BitConverter.GetBytes(value), unescape);
        }

        public void Write(string text)
        {
            Write((UInt16)text.Length);
            Write(Encoding.ASCII.GetBytes(text));
        }


        public void Dispose()
        {
            Close();
        }
    }
}
