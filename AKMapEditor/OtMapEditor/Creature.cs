using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AKMapEditor.OtMapEditor.OtBrush;
using ProtoBuf;

namespace AKMapEditor.OtMapEditor
{
    [ProtoContract]
    public class Creature
    {
        [ProtoMember(1)]
        protected string type_name;

        [ProtoMember(2)]
        protected int spawntime;

        protected bool saved;
        protected bool selected;


        public Creature() { }
        public Creature(CreatureType ctype)
        {
            if (ctype != null)
            {
                type_name = ctype.name;
            }
        }

        public Creature(string type_name)
        {
            this.type_name = type_name;
        }


        public Outfit getLookType()
        {
            CreatureType type = CreatureDatabase.creatureDatabase[type_name];
            if (type != null)
            {
                return type.outfit;
            }
            return new Outfit();
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

        public Creature deepCopy()
        {
            Creature copy = new Creature(type_name);
            copy.spawntime = spawntime;
            copy.selected = selected;
            copy.saved = saved;
            return copy;            
        }

	    public bool isNpc() 
        {
            CreatureType type = CreatureDatabase.creatureDatabase[type_name];
            if (type != null)
            {
                return type.isNpc;
            }
            return false;
        }

	    public string getName()  
        {
            CreatureType type = CreatureDatabase.creatureDatabase[type_name];
            if (type != null)
            {
                return type.name;
            }
            return "";
               
        }
        public CreatureBrush getBrush()
        {
            CreatureType type = CreatureDatabase.creatureDatabase[type_name];
            if (type != null)
            {
                return type.brush;
            }
            return null;
        }

        public int getSpawnTime()
        {
            return spawntime;
        }
        public void setSpawnTime(int spawntime)
        {
            this.spawntime = spawntime;
        }
    }
}
