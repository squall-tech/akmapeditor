using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace AKMapEditor.OtMapEditor
{
    [ProtoContract]
    public class Spawn
    {
        [ProtoMember(1)]
        protected int size;

        protected bool selected = false;

        public Spawn()
        {

        }

        public Spawn(int size = 3)
        {
            selected = false;
            setSize(size);
        }

        public bool isSelected()
        {
            return selected;
        }

        public void select()
        {
            selected = true;
        }

        public void deselect()
        {
            selected = false;
        }

        public void setSize(int newsize)
        {
            if (newsize < 100)
            {
                size = newsize;
            }
        }

        public int getSize() 
        {
            return size;
        }

        public Spawn deepCopy()
        {
            Spawn copy = new Spawn(size);
            copy.selected = selected;
            return copy;
        }
        
        /*
        public static bool operator ==(Spawn este, Spawn other)
        {
            if (este != null && other != null)
            {
                return este.size == other.size;
            }
            else
            {
                return false;                   
            }
            
        }

        public static bool operator !=(Spawn este, Spawn other)
        {
            if (este != null && other != null)
            {
                return este.size != other.size;
            }
            else
            {
                return false;
            }
        }*/
    }
}
