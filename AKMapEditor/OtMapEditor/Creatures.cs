using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using AKMapEditor.OtMapEditor.OtBrush;

namespace AKMapEditor.OtMapEditor
{

    public class CreatureDatabase
    {
        protected Dictionary<String, CreatureType> creature_map;

        public CreatureDatabase()
        {
            creature_map = new Dictionary<string, CreatureType>();
        }     

        public CreatureType this[String creatureName]
        {
            get 
            {
                if (creatureName == null)
                {
                    return null;
                }

                CreatureType ret = null;
                if (creature_map.TryGetValue(creatureName.ToLower(), out ret))
                {
                    return  ret;
                } else 
                {
                    return null;
                }                                
            }
            set
            {
                creature_map[creatureName] = value;
            }
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            foreach (CreatureType creature in creature_map.Values)
            {
                yield return creature;
            }            
        }

        public CreatureType addMissingCreatureType(string name, bool isNpc)
        {
            return null;
        }

        public bool hasMissing()
        {
            return false;
        }

        public void loadFromXML(String fileName, bool standard)
        {
            XElement doc = XElement.Load(fileName);

            foreach(XElement node in doc.Elements())
            {
                if (node.Name.LocalName == "creature")
                {
                    CreatureType ct = CreatureType.loadFromXML(node);
                    if (ct != null)
                    {
                        ct.standard = standard;
                        if (this[ct.name] != null)
                        {
                            Messages.AddWarning("Duplicate creature name ! " + ct.name + "  Discarding...");
                        }
                        else
                        {
                            creature_map.Add(ct.name.ToLower(), ct);
                        }
                    }
                }
            }            
        }

        public static CreatureDatabase creatureDatabase;
    }

    public class CreatureType
    {
	    public bool isNpc;
        public bool missing;
        public bool in_other_tileset;
        public bool standard;
        public string name;
        public Outfit outfit;
        public CreatureBrush brush;

        public CreatureType()
        {
            outfit = new Outfit();
        }

        public static CreatureType loadFromXML(XElement node)
        {            
            String tmp_type = node.Attribute("type").GetString();

            if (tmp_type == "")
            {
                Messages.AddWarning("Couldn't read type tag of creature node.");
                return null;
            }

            if (tmp_type != "monster" && tmp_type != "npc")
            {
                Messages.AddWarning("Invalid type tag of creature node " + tmp_type);
                return null;                   
            }
            String tmp_name = node.Attribute("name").GetString();
            if (tmp_name == "")
            {
                Messages.AddWarning("Couldn't read name tag of creature node.");
                return null;
            }

            CreatureType ct = new CreatureType();
            ct.name = tmp_name;
            ct.isNpc = (tmp_type == "npc");

            int intVal = node.Attribute("looktype").GetInt32();
            if (intVal > 0)
            {
                ct.outfit.lookType = intVal;

                /* VERIFICAR SERVER TODO
                if (gui.gfx.getCreatureSprite(intVal) == NULL)
                {
                    wxString war;
                    war << wxT("Invalid creature \"") << wxstr(tmp_name) << wxT("\" look type #");
                    war << intVal;
                    warnings.push_back(war);
                }
                 */
            }

            intVal = node.Attribute("lookitem").GetInt32();
            if (intVal > 0)
            {
                ct.outfit.lookItem = intVal;
            }


            intVal = node.Attribute("lookaddon").GetInt32();
            if (intVal > 0)
            {
                ct.outfit.lookAddon = intVal;
            }

            intVal = node.Attribute("lookhead").GetInt32();
            if (intVal > 0)
            {

                ct.outfit.lookHead = intVal;
            }
            intVal = node.Attribute("lookbody").GetInt32();
            if (intVal > 0)
            {
                ct.outfit.lookBody = intVal;
            }
            intVal = node.Attribute("looklegs").GetInt32();
            if (intVal > 0)
            {
                ct.outfit.lookLegs = intVal;
            }
            intVal = node.Attribute("lookfeet").GetInt32();
            if (intVal > 0)
            {
                ct.outfit.lookFeet = intVal;
            }            

            return ct;
        }
    }
}