using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;

namespace AKMapEditor.OtMapEditor.OtBrush
{
    public abstract class TerrainBrush : Brush
    {
        protected String name;
        protected UInt16 look_id;
        protected List<UInt32> friends = new List<UInt32>();
        protected bool hateFriends = false;
        
        public override bool needBorders()
        {
            return true;
        }
        public override bool canDrag()
        {
            return true;
        }
        public override string getName()
        {
            return name;
        }
        public override void setName(string name)
        {
            this.name = name;
        }

        public override int getLookID()
        {
            return look_id;
        }
        public override bool canDraw(GameMap map, Position pos)
        {
            return true;
        }

        public bool friendOf(TerrainBrush other)
        {
            UInt32 boderId = other.getID();

            foreach (UInt32 fit in friends)
            {
                if (boderId == fit)
                {
                    return !hateFriends;
                }
                else if (boderId == 0xFFFFFFFF)
                {
                    return !hateFriends;
                }
            }            
            return hateFriends;
        }

    }
}
