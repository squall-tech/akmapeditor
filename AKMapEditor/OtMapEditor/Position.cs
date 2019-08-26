using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace AKMapEditor.OtMapEditor
{

    [ProtoContract]
    public class Position
    {
        [ProtoMember(1)]
        public int x;
        [ProtoMember(2)]
        public int y;
        [ProtoMember(3)]
        public int z;

        public Position(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Position(Position oldpos)
        {
            this.x = oldpos.x;
            this.y = oldpos.y;
            this.z = oldpos.z;
        }
        public Position()
        {
            
        }
        public int X { get { return x; } set { x = value; } }
        public int Y { get { return y; } set { y = value; } }
        public int Z { get { return z; } set { z = value; } }


        public override string ToString()
        {
            return "x = " + X + ", y = " + y + ", z = " + Z;
        }
        public bool Equals(Position obj)
        {
            return ((obj.x == this.x) &&
                    (obj.y == this.y) &&
                    (obj.z == this.z));
        }

        public ulong ToIndex()
        {
            return (ulong)((uint)x & 0xFFFF) << 24 | ((uint)y & 0xFFFF) << 8 | ((uint)z & 0xFF);
        }

        public static ulong ToIndex(int x, int y, int z)
        {
            return (ulong)((uint)x & 0xFFFF) << 24 | ((uint)y & 0xFFFF) << 8 | ((uint)z & 0xFF);
        }


        public static Position operator - (Position este, Position other)
        {
            Position newpos = new Position();
            newpos.x = este.x - other.x;
            newpos.y = este.y - other.y;
            newpos.z = este.z - other.z;
            return newpos; 
        }

        public static Position operator + (Position este, Position other)
        {
            Position newpos = new Position();
            newpos.x = este.x + other.x;
            newpos.y = este.y + other.y;
            newpos.z = este.z + other.z;
            return newpos;         
        }


        public static bool operator == (Position este, Position other)
        {
            if ((este.X == other.X) && (este.Y == other.Y) && (este.Z == other.Z))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public static bool operator !=(Position este, Position other)
        {
            if ((este.X != other.X) || (este.Y != other.Y) || (este.Z != other.Z))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool isValid(){
            return x >= 0 && x <= 0xFFFF && y >= 0 && y <= 0xFFFF && z >= 0 && z <= 15;
        }

    }
}
