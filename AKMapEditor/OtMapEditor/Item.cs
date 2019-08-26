using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using AKMapEditor.OtMapEditor.OtBrush;
using ProtoBuf;

namespace AKMapEditor.OtMapEditor
{
   
    [ProtoContract]
    public class Item
    {
        [ProtoMember(1)]
        private UInt16 itemId;

        public ItemType Type { get { return Global.items.items[itemId]; } private set { itemId = value.Id; } }

        private Dictionary<OtItemAttribute, object> attributes;

        public UInt16 subtype;

        private bool selected;


        public bool isSelected()
        {
            return this.selected;
        }

        public void select()
        {
            selected = true;
        }

        public void deselect()
        {
            selected = false;
        }
        public void toggleSelection() 
        { 
            selected = !selected; 
        }


        public Item()
        {

        }

        protected Item(ItemType type)
        {
            selected = false;
            Type = type;
        }

        public static Item Create(ItemType type) {

            Item item = new Item(type);
            return item;
        }

        public static Item Create(UInt16 id)
        {
            if (Global.items.items[id] != null)
            {
                Item item = new Item(Global.items.items[id]);
                return item;
            }
            else
            {
                return null;
            }
            
        }

        public static Item transformItem(Item old_item, ushort new_id, Tile parent)
        {
            if (old_item == null)
            {
                return null;
            }
            old_item.setID(new_id);
            // Through the magic of deepCopy, this will now be a pointer to an item of the correct type.
            Item new_item = old_item.deepCopy();
            if (parent != null)
            {
                // Find the old item and remove it from the tile, insert this one instead!
                if (old_item == parent.Ground)
                {
                    old_item = null;
                    parent.Ground = new_item;
                    return new_item;
                }                

                int index = parent.Items.IndexOf(old_item);
                if (index > -1)
                {
                    parent.Items.RemoveAt(index);
                    parent.Items.Insert(index, new_item);
                    return new_item;
                }
            }
            return null;
        }        


        public bool hasBorderEquivalent()
        {
            return Global.items.items[itemId].HasEquivalent;
        }


        public ItemType getGroundEquivalent()
        {
            return Global.items.items[itemId].GroundEquivalent;
        }

        public Item deepCopy()
        {
            Item copy = new Item(this.Type);
            if (copy != null)
            {
                copy.subtype = this.subtype;
                copy.selected = this.selected;
                if (attributes != null)
                {
                    //copy.attributes = this.attributes;
                }
            }

            return copy;
        }

        public int getWallAlignment()
        {
            ItemType it = Global.items.items[itemId];
            if (!it.IsWall)
            {
                return 0;
            }
            return it.BorderAlignment;
        }

        public GroundBrush getGroundBrush() {
            ItemType it = Global.items.items[this.itemId];
	        if(!it.isGroundTile()) {
		        return null;
	        }
	        return (GroundBrush) it.brush;
        }

        public WallBrush getWallBrush()
        {
            ItemType it = Global.items.items[this.itemId];
            if ((it != null) && (it.IsWall))
            {
                return (WallBrush)it.brush;
            }
            return null;
        }

        public CarpetBrush getCarpetBrush()
        {
            ItemType it = Global.items.items[this.itemId];
            if ((it != null) && (it.IsCarpet))
            {
                return (CarpetBrush)it.brush;
            }
            return null;
        }

        public TableBrush getTableBrush()
        {
            ItemType it = Global.items.items[this.itemId];
            if ((it != null) && (it.IsTable))
            {
                return (TableBrush)it.brush;
            }
            return null;
        }

        public Brush getDoodadBrush()
        {
            if (Type.doodad_brush != null)
            {
                return Type.doodad_brush;
            }

            if ((Type.brush != null) && (Type.brush.inDoodadPalette))
            {
                return Type.brush;
            }

            if ((Type.raw_brush != null) && (Type.raw_brush.inDoodadPalette))
            {
                return Type.raw_brush;
            }

            return null;
            
        }

        public UInt16 getUniqueID()
        {
            return 0;
        }


        public void setID(UInt16 id)
        {
            this.itemId = id;
        }


        public bool isBorder()
        {
            return Type.IsBorder;
        }


        public int getSubtype()
        {
            return 0;
        }

        public int getTopOrder()
        {
            return this.Type.AlwaysOnTopOrder;
        }

        public void SetAttribute(OtItemAttribute attribute, object value)
        {
            if (attributes == null)
                attributes = new Dictionary<OtItemAttribute, object>();
            attributes[attribute] = value;
        }

        public object GetAttribute(OtItemAttribute attribute)
        {
            if (attributes == null || !attributes.ContainsKey(attribute))
                return null;

            return attributes[attribute];
        }

        public virtual void SerializeAttribute(OtItemAttribute attribute, PropertyWriter writer)
        {
            switch (attribute)
            {
                case OtItemAttribute.COUNT:
                    writer.Write((byte)GetAttribute(attribute));
                    break;
                case OtItemAttribute.ACTION_ID:
                    writer.Write((ushort)GetAttribute(attribute));
                    break;
                case OtItemAttribute.UNIQUE_ID:
                    writer.Write((ushort)GetAttribute(attribute));
                    break;
                case OtItemAttribute.NAME:
                    writer.Write((string)GetAttribute(attribute));
                    break;
                case OtItemAttribute.PLURALNAME:
                    writer.Write((string)GetAttribute(attribute));
                    break;
                case OtItemAttribute.ARTICLE:
                    writer.Write((string)GetAttribute(attribute));
                    break;
                case OtItemAttribute.ATTACK:
                    writer.Write((int)GetAttribute(attribute));
                    break;
                case OtItemAttribute.EXTRAATTACK:
                    writer.Write((int)GetAttribute(attribute));
                    break;
                case OtItemAttribute.DEFENSE:
                    writer.Write((int)GetAttribute(attribute));
                    break;
                case OtItemAttribute.EXTRADEFENSE:
                    writer.Write((int)GetAttribute(attribute));
                    break;
                case OtItemAttribute.ARMOR:
                    writer.Write((int)GetAttribute(attribute));
                    break;
                case OtItemAttribute.ATTACKSPEED:
                    writer.Write((int)GetAttribute(attribute));
                    break;
                case OtItemAttribute.HITCHANCE:
                    writer.Write((int)GetAttribute(attribute));
                    break;
                case OtItemAttribute.SCRIPTPROTECTED:
                    writer.Write((byte)((bool)GetAttribute(attribute) ? 1 : 0));
                    break;
                case OtItemAttribute.DUALWIELD:
                    writer.Write((byte)((bool)GetAttribute(attribute) ? 1 : 0));
                    break;
                case OtItemAttribute.TEXT:
                    writer.Write((string)GetAttribute(attribute));
                    break;
                case OtItemAttribute.WRITTENDATE:
                    writer.Write((int)GetAttribute(attribute));
                    break;
                case OtItemAttribute.WRITTENBY:
                    writer.Write((string)GetAttribute(attribute));
                    break;
                case OtItemAttribute.DESC:
                    writer.Write((string)GetAttribute(attribute));
                    break;
                case OtItemAttribute.RUNE_CHARGES:
                    writer.Write((byte)GetAttribute(attribute));
                    break;
                case OtItemAttribute.CHARGES:
                    writer.Write((ushort)GetAttribute(attribute));
                    break;
                case OtItemAttribute.DURATION:
                    writer.Write((int)GetAttribute(attribute));
                    break;
                case OtItemAttribute.DECAYING_STATE:
                    writer.Write((byte)GetAttribute(attribute));
                    break;
                default:
                    throw new Exception("Unkonw item attribute: " + attribute);
            }

        }

        public virtual void Serialize(PropertyWriter writer)
        {
            if (attributes == null)
                return;

            foreach (var attr in attributes)
            {
                writer.Write((byte)attr.Key);
                SerializeAttribute(attr.Key, writer);
            }
        }

        public virtual void Serialize(FileWriter fileWriter, PropertyWriter writer)
        {
            Serialize(writer);
        }

        public virtual void DeserializeAttribute(OtItemAttribute attribute, PropertyReader reader)
        {
            try
            {
                switch (attribute)
                {
                    case OtItemAttribute.COUNT:
                        SetAttribute(OtItemAttribute.COUNT, reader.ReadByte());
                        break;
                    case OtItemAttribute.ACTION_ID:
                        SetAttribute(OtItemAttribute.ACTION_ID, reader.ReadUInt16());
                        break;
                    case OtItemAttribute.UNIQUE_ID:
                        SetAttribute(OtItemAttribute.UNIQUE_ID, reader.ReadUInt16());
                        break;
                    case OtItemAttribute.NAME:
                        SetAttribute(OtItemAttribute.NAME, reader.ReadString());
                        break;
                    case OtItemAttribute.PLURALNAME:
                        SetAttribute(OtItemAttribute.PLURALNAME, reader.ReadString());
                        break;
                    case OtItemAttribute.ARTICLE:
                        SetAttribute(OtItemAttribute.ARTICLE, reader.ReadString());
                        break;
                    case OtItemAttribute.ATTACK:
                        SetAttribute(OtItemAttribute.ATTACK, reader.ReadInt32());
                        break;
                    case OtItemAttribute.EXTRAATTACK:
                        SetAttribute(OtItemAttribute.EXTRAATTACK, reader.ReadInt32());
                        break;
                    case OtItemAttribute.DEFENSE:
                        SetAttribute(OtItemAttribute.DEFENSE, reader.ReadInt32());
                        break;
                    case OtItemAttribute.EXTRADEFENSE:
                        SetAttribute(OtItemAttribute.EXTRADEFENSE, reader.ReadInt32());
                        break;
                    case OtItemAttribute.ARMOR:
                        SetAttribute(OtItemAttribute.ARMOR, reader.ReadInt32());
                        break;
                    case OtItemAttribute.ATTACKSPEED:
                        SetAttribute(OtItemAttribute.ATTACKSPEED, reader.ReadInt32());
                        break;
                    case OtItemAttribute.HITCHANCE:
                        SetAttribute(OtItemAttribute.HITCHANCE, reader.ReadInt32());
                        break;
                    case OtItemAttribute.SCRIPTPROTECTED:
                        SetAttribute(OtItemAttribute.SCRIPTPROTECTED, reader.ReadByte() != 0);
                        break;
                    case OtItemAttribute.DUALWIELD:
                        SetAttribute(OtItemAttribute.DUALWIELD, reader.ReadByte() != 0);
                        break;
                    case OtItemAttribute.TEXT:
                        SetAttribute(OtItemAttribute.TEXT, reader.ReadString());
                        break;
                    case OtItemAttribute.WRITTENDATE:
                        SetAttribute(OtItemAttribute.WRITTENDATE, reader.ReadInt32());
                        break;
                    case OtItemAttribute.WRITTENBY:
                        SetAttribute(OtItemAttribute.WRITTENBY, reader.ReadString());
                        break;
                    case OtItemAttribute.DESC:
                        SetAttribute(OtItemAttribute.DESC, reader.ReadString());
                        break;
                    case OtItemAttribute.RUNE_CHARGES:
                        SetAttribute(OtItemAttribute.RUNE_CHARGES, reader.ReadByte());
                        break;
                    case OtItemAttribute.CHARGES:
                        SetAttribute(OtItemAttribute.CHARGES, reader.ReadUInt16());
                        break;
                    case OtItemAttribute.DURATION:
                        SetAttribute(OtItemAttribute.DURATION, reader.ReadInt32());
                        break;
                    case OtItemAttribute.DECAYING_STATE:
                        SetAttribute(OtItemAttribute.DECAYING_STATE, reader.ReadByte());
                        break;
                    //specific item properties
                    //case OtItemAttribute.DEPOT_ID:
                    //    SetAttribute(OtItemAttribute.DEPOT_ID, reader.ReadUInt16());
                    //    break;
                    //case OtItemAttribute.HOUSEDOORID:
                    //    SetAttribute(OtItemAttribute.HOUSEDOORID, reader.ReadByte());
                    //    break;
                    //case OtItemAttribute.TELE_DEST:
                    //    SetAttribute(OtItemAttribute.TELE_DEST, reader.ReadLocation());
                    //    break;
                    //case OtItemAttribute.SLEEPERGUID:
                    //    SetAttribute(OtItemAttribute.SLEEPERGUID, reader.ReadUInt32());
                    //    break;
                    //case OtItemAttribute.SLEEPSTART:
                    //    SetAttribute(OtItemAttribute.SLEEPSTART, reader.ReadUInt32());
                    //    break;
                    default:
                        break;
                        throw new Exception("Unkonw item attribute: " + (byte)attribute + " ID: " + Type.Id);
                }
            }
            catch (Exception e)
            {
                Messages.AddWarning("[error] error on DeserializeAttribute" + e.Message);
            }
        }

        public virtual void Deserialize(PropertyReader reader)
        {
            try
            {
                while (reader.PeekChar() != -1)
                {
                    var attrType = (OtItemAttribute)reader.ReadByte();
                    DeserializeAttribute(attrType, reader);
                }
            }
            catch (Exception e)
            {
                Messages.AddWarning("[error] error on Deserialize" + e.Message);
            }
        }

        public virtual void Deserialize(FileReader fileReader, BinaryNode itemNode, PropertyReader reader)
        {
            Deserialize(reader);
        }
    }
}
