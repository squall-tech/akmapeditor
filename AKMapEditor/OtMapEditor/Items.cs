using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Windows.Forms;
using Brush = AKMapEditor.OtMapEditor.OtBrush.Brush;
using AKMapEditor.OtMapEditor.OtBrush;

namespace AKMapEditor.OtMapEditor
{
    public enum OtbItemGroup
    {
        NONE = 0,
        GROUND,
        CONTAINER,
        WEAPON,
        AMMUNITION,
        ARMOR,
        CHARGES,
        TELEPORT,
        MAGICFIELD,
        WRITEABLE,
        KEY,
        SPLASH,
        FLUID,
        DOOR,
        DEPRECATED,
        LAST
    };

    public enum ItemGroup
    {
        //Otb Groups
        None,
        Ground,
        Container,
        FluidContainer,
        Splash,
        Deprecated,

        //Xml Groups
        Rune,
        Bed,
        Door,
        Teleport,
        //TrashHolder,
        MailBox,
        Depot,
        MagicField,
        Key
    };

    public enum ItemTypes
    {
        ITEM_TYPE_NONE = 0,
        ITEM_TYPE_DEPOT,
        ITEM_TYPE_MAILBOX,
        ITEM_TYPE_TRASHHOLDER,
        ITEM_TYPE_CONTAINER,
        ITEM_TYPE_DOOR,
        ITEM_TYPE_MAGICFIELD,
        ITEM_TYPE_TELEPORT,
        ITEM_TYPE_BED,
        ITEM_TYPE_KEY,
        ITEM_TYPE_LAST
    };

    public enum OtbItemFlags
    {
        BLOCK_SOLID = 1,
        BLOCK_PROJECTILE = 2,
        BLOCK_PATHFIND = 4,
        HAS_HEIGHT = 8,
        USEABLE = 16,
        PICKUPABLE = 32,
        MOVEABLE = 64,
        STACKABLE = 128,
        FLOORCHANGEDOWN = 256,
        FLOORCHANGENORTH = 512,
        FLOORCHANGEEAST = 1024,
        FLOORCHANGESOUTH = 2048,
        FLOORCHANGEWEST = 4096,
        ALWAYSONTOP = 8192,
        READABLE = 16384,
        ROTABLE = 32768,
        HANGABLE = 65536,
        VERTICAL = 131072,
        HORIZONTAL = 262144,
        CANNOTDECAY = 524288,		/*deprecated*/
        ALLOWDISTREAD = 1048576,
        CORPSE = 2097152,			/*deprecated*/
        CLIENTCHARGES = 4194304,	/*deprecated*/
        LOOKTHROUGH = 8388608,
        ANIMATION = 16777216,
        WALKSTACK = 33554432
    };

        public enum OtbItemAttr
        {
            ITEM_ATTR_FIRST = 0x10,
            ITEM_ATTR_SERVERID = ITEM_ATTR_FIRST,
            ITEM_ATTR_CLIENTID,
            ITEM_ATTR_NAME,
            ITEM_ATTR_DESCR,			/*deprecated*/
            ITEM_ATTR_SPEED,
            ITEM_ATTR_SLOT,				/*deprecated*/
            ITEM_ATTR_MAXITEMS,			/*deprecated*/
            ITEM_ATTR_WEIGHT,			/*deprecated*/
            ITEM_ATTR_WEAPON,			/*deprecated*/
            ITEM_ATTR_AMU,				/*deprecated*/
            ITEM_ATTR_ARMOR,			/*deprecated*/
            ITEM_ATTR_MAGLEVEL,			/*deprecated*/
            ITEM_ATTR_MAGFIELDTYPE,		/*deprecated*/
            ITEM_ATTR_WRITEABLE,		/*deprecated*/
            ITEM_ATTR_ROTATETO,			/*deprecated*/
            ITEM_ATTR_DECAY,			/*deprecated*/
            ITEM_ATTR_SPRITEHASH,
            ITEM_ATTR_MINIMAPCOLOR,
            ITEM_ATTR_07,
            ITEM_ATTR_08,
            ITEM_ATTR_LIGHT,			/*deprecated*/

            //1-byte aligned
            ITEM_ATTR_DECAY2,			/*deprecated*/
            ITEM_ATTR_WEAPON2,			/*deprecated*/
            ITEM_ATTR_AMU2,				/*deprecated*/
            ITEM_ATTR_ARMOR2,			/*deprecated*/
            ITEM_ATTR_WRITEABLE2,		/*deprecated*/
            ITEM_ATTR_LIGHT2,
            ITEM_ATTR_TOPORDER,
            ITEM_ATTR_WRITEABLE3,		/*deprecated*/
            ITEM_ATTR_WAREID,

            ITEM_ATTR_LAST
        };


    public enum RootAttr
    {
        ROOT_ATTR_VERSION = 0x01
    };

    public class ItemType 
    {
        public UInt16 Id;
        public UInt16 SpriteId;
        public UInt16 GroundSpeed;
        public ItemGroup Group;
        public bool alwaysOnBottom;
        public UInt16 AlwaysOnTopOrder;
        public bool HasUseWith;
        public UInt16 MaxReadChars;
        public UInt16 MaxReadWriteChars;
        public bool HasHeight;
        public UInt16 MinimapColor;
        public bool LookThrough;
        public UInt16 LightLevel;
        public UInt16 LightColor;
        public bool IsStackable;
        public bool IsReadable;
        public bool IsMoveable;
        public bool IsPickupable;
        public bool IsHangable;
        public bool IsHorizontal;
        public bool IsVertical;
        public bool IsRotatable;
        public bool BlockObject;
        public bool BlockProjectile;
        public bool BlockPathFind;
        public bool AllowDistRead;
        public bool IsAnimation;
        public bool WalkStack;
        public UInt16 WareId;
        public string Name;
        public byte[] SpriteHash;
        public string Article;
        public string Plural;
        public string Description;

        public bool FloorChangeDown;
        public bool FloorChangeNorth;
        public bool FloorChangeSouth;
        public bool FloorChangeEast;
        public bool FloorChangeWest;

        public bool IsBorder = false;        
        public bool IsOptionalBorder = false;

        //------ wallBrush
        public bool IsWall = false;
        public bool IsBrushDoor = false;
        public bool WallHateMe = false;
        public bool IsOpen = false;
        public bool IsCarpet = false;
        public bool IsTable = false;

        public ItemType GroundEquivalent;
        public bool HasEquivalent = false;
        public GameSprite gameSprite;
        public int BorderAlignment = 0;
        
        public bool has_raw = false;
        public Brush brush; //arrumar depois da limpeza
        public Brush raw_brush;
        public Brush doodad_brush;
                
        public bool isMetaItem()
        {
            return false;
        }
        public bool isGroundTile()
        {
            return (Group == ItemGroup.Ground);
        }
    }

    public class ItemDatabase
    {
        public UInt32 MajorVersion;
        public UInt32 MinorVersion;
        public UInt32 BuildNumber;

        protected int item_count;
        protected int effect_count;
        protected int monster_count;
        protected int distance_count;

        protected int minclientID;
        protected int maxclientID;
        protected UInt16 max_item_id;

        public ItemType[] items;
        public ItemType[] clientItems;        
        public int MaxId;

        public ItemDatabase()
        {
            MaxId = 0;
            items = new ItemType[UInt16.MaxValue];
            clientItems = new ItemType[UInt16.MaxValue];

            MajorVersion = 0;
            MinorVersion = 0;
            BuildNumber = 0;

            item_count = 0;
            effect_count = 0;
            monster_count = 0;
            distance_count = 0;

            minclientID = 0;
            maxclientID = 0;
            max_item_id = 0;
        }

        public void Load(string fileName)
        {
            LoadFromOtb(fileName);
            LoadFromXml(Path.ChangeExtension(fileName, "xml"));
        }

        public bool LoadFromOtb(string fileName)
        {
            if (!File.Exists(fileName))
                throw new Exception("Couldn't open file " + fileName);
            
            using (FileReader reader = new FileReader(fileName)) 
            {
                BinaryNode node = reader.GetRootNode();

                PropertyReader props = reader.GetPropertyReader(node);

                props.ReadByte();
                props.ReadUInt32();

                byte attr = props.ReadByte();
                if ((RootAttr)attr == RootAttr.ROOT_ATTR_VERSION)
                {
                    var datalen = props.ReadUInt16();

                    if (datalen != 140)
                        throw new Exception("Size of version header is invalid.");

                    MajorVersion = props.ReadUInt32();
                    MinorVersion = props.ReadUInt32();
                    BuildNumber = props.ReadUInt32();
                }
                node = node.Child;

                switch (MajorVersion)
                {
                    case 1: throw new Exception("Old version of items.otb detected, a newer version of items.otb is required. Version: 1");
                    case 2: throw new Exception("Old version of items.otb detected, a newer version of items.otb is required. Version: 2");
                    case 3: return LoadFromOtbVer3(node, reader);
                    default: throw new Exception("Unsupported items.otb version (version " + MajorVersion);
                }
            }
            

        }
        private bool LoadFromOtbVer3(BinaryNode node, FileReader reader)
        {
            PropertyReader props = null;
            while (node != null)
            {
                props = reader.GetPropertyReader(node);

                ItemType item = new ItemType();
                byte itemGroup = (byte)node.Type;

                switch ((OtbItemGroup)itemGroup)
                {
                    case OtbItemGroup.NONE: item.Group = ItemGroup.None; break;
                    case OtbItemGroup.GROUND: item.Group = ItemGroup.Ground; break;
                    case OtbItemGroup.SPLASH: item.Group = ItemGroup.Splash; break;
                    case OtbItemGroup.FLUID: item.Group = ItemGroup.FluidContainer; break;
                    case OtbItemGroup.CONTAINER: item.Group = ItemGroup.Container; break;
                    case OtbItemGroup.DEPRECATED: item.Group = ItemGroup.Deprecated; break;
                    default: Messages.AddWarning("Unknown item group declaration"); break;
                }

                OtbItemFlags flags = (OtbItemFlags)props.ReadUInt32();

                item.BlockObject = ((flags & OtbItemFlags.BLOCK_SOLID) == OtbItemFlags.BLOCK_SOLID);
                item.BlockProjectile = ((flags & OtbItemFlags.BLOCK_PROJECTILE) == OtbItemFlags.BLOCK_PROJECTILE);
                item.BlockPathFind = ((flags & OtbItemFlags.BLOCK_PATHFIND) == OtbItemFlags.BLOCK_PATHFIND);
                item.IsPickupable = ((flags & OtbItemFlags.PICKUPABLE) == OtbItemFlags.PICKUPABLE);
                item.IsMoveable = ((flags & OtbItemFlags.MOVEABLE) == OtbItemFlags.MOVEABLE);
                item.IsStackable = ((flags & OtbItemFlags.STACKABLE) == OtbItemFlags.STACKABLE);
                item.FloorChangeDown = ((flags & OtbItemFlags.FLOORCHANGEDOWN) == OtbItemFlags.FLOORCHANGEDOWN);
                item.FloorChangeNorth = ((flags & OtbItemFlags.FLOORCHANGENORTH) == OtbItemFlags.FLOORCHANGENORTH);
                item.FloorChangeEast = ((flags & OtbItemFlags.FLOORCHANGEEAST) == OtbItemFlags.FLOORCHANGEEAST);
                item.FloorChangeSouth = ((flags & OtbItemFlags.FLOORCHANGESOUTH) == OtbItemFlags.FLOORCHANGESOUTH);
                item.FloorChangeWest = ((flags & OtbItemFlags.FLOORCHANGEWEST) == OtbItemFlags.FLOORCHANGEWEST);
                item.alwaysOnBottom = ((flags & OtbItemFlags.ALWAYSONTOP) == OtbItemFlags.ALWAYSONTOP);
                item.IsVertical = ((flags & OtbItemFlags.VERTICAL) == OtbItemFlags.VERTICAL);
                item.IsHorizontal = ((flags & OtbItemFlags.HORIZONTAL) == OtbItemFlags.HORIZONTAL);
                item.IsHangable = ((flags & OtbItemFlags.HANGABLE) == OtbItemFlags.HANGABLE);
                item.IsRotatable = ((flags & OtbItemFlags.ROTABLE) == OtbItemFlags.ROTABLE);
                item.IsReadable = ((flags & OtbItemFlags.READABLE) == OtbItemFlags.READABLE);
                item.HasUseWith = ((flags & OtbItemFlags.USEABLE) == OtbItemFlags.USEABLE);
                item.HasHeight = ((flags & OtbItemFlags.HAS_HEIGHT) == OtbItemFlags.HAS_HEIGHT);
                item.LookThrough = ((flags & OtbItemFlags.LOOKTHROUGH) == OtbItemFlags.LOOKTHROUGH);
                item.AllowDistRead = ((flags & OtbItemFlags.ALLOWDISTREAD) == OtbItemFlags.ALLOWDISTREAD);
                item.IsAnimation = ((flags & OtbItemFlags.ANIMATION) == OtbItemFlags.ANIMATION);
                item.WalkStack = ((flags & OtbItemFlags.WALKSTACK) == OtbItemFlags.WALKSTACK);
                while (props.PeekChar() != -1)
                {
                    byte attribute = props.ReadByte();
                    UInt16 datalen = props.ReadUInt16();

                    switch ((OtbItemAttr)attribute)
                    {
                        case OtbItemAttr.ITEM_ATTR_SERVERID:
                            if (datalen != sizeof(UInt16))
                                throw new Exception("Unexpected data length of server id block (Should be 2 bytes)");

                            item.Id = props.ReadUInt16();
                            break;

                        case OtbItemAttr.ITEM_ATTR_CLIENTID:
                            if (datalen != sizeof(UInt16))
                                throw new Exception("Unexpected data length of client id block (Should be 2 bytes)");

                            item.SpriteId = props.ReadUInt16();
                            break;

                        case OtbItemAttr.ITEM_ATTR_WAREID:
                            if (datalen != sizeof(UInt16))
                                throw new Exception("Unexpected data length of ware id block (Should be 2 bytes)");

                            item.WareId = props.ReadUInt16();
                            break;

                        case OtbItemAttr.ITEM_ATTR_SPEED:
                            if (datalen != sizeof(UInt16))
                                throw new Exception("Unexpected data length of speed block (Should be 2 bytes)");

                            item.GroundSpeed = props.ReadUInt16();
                            break;

                        case OtbItemAttr.ITEM_ATTR_NAME:
                            item.Name = new string(props.ReadChars(datalen));
                            break;

                        case OtbItemAttr.ITEM_ATTR_SPRITEHASH:
                            if (datalen != 16)
                                throw new Exception("Unexpected data length of sprite hash (Should be 16 bytes)");

                            item.SpriteHash = props.ReadBytes(16);
                            break;

                        case OtbItemAttr.ITEM_ATTR_MINIMAPCOLOR:
                            if (datalen != 2)
                                throw new Exception("Unexpected data length of minimap color (Should be 2 bytes)");

                            item.MinimapColor = props.ReadUInt16();
                            break;

                        case OtbItemAttr.ITEM_ATTR_07:
                            //read/write-able
                            if (datalen != 2)
                                throw new Exception("Unexpected data length of attr 07 (Should be 2 bytes)");

                            item.MaxReadWriteChars = props.ReadUInt16();
                            break;

                        case OtbItemAttr.ITEM_ATTR_08:
                            //readable
                            if (datalen != 2)
                                throw new Exception("Unexpected data length of attr 08 (Should be 2 bytes)");

                            item.MaxReadChars = props.ReadUInt16();
                            break;

                        case OtbItemAttr.ITEM_ATTR_LIGHT2:
                            if (datalen != sizeof(UInt16) * 2)
                                throw new Exception("Unexpected data length of item light (2) block");

                            item.LightLevel = props.ReadUInt16();
                            item.LightColor = props.ReadUInt16();
                            break;

                        case OtbItemAttr.ITEM_ATTR_TOPORDER:
                            if (datalen != sizeof(byte))
                                throw new Exception("Unexpected data length of item toporder block (Should be 1 byte)");

                            item.AlwaysOnTopOrder = props.ReadByte();
                            break;

                        default:
                            //skip unknown attributes
                            props.ReadBytes(datalen);
                            break;
                    }
                }
                if (MaxId < item.Id)
                {
                    MaxId = item.Id;
                }

                items[item.Id] = item;
                clientItems[item.SpriteId] = item;
                node = node.Next;
            }
            return true;      
        }

        public void clear()
        {
            for (int x = 0; x <= MaxId; x++)
            {
                items[x] = null;
            }
            MaxId = 0;
        }

        public void LoadFromXml(string fileName)
        {
            if (!File.Exists(fileName))
                throw new Exception(string.Format("File not found {0}.", fileName));

            try
            {
                var xml = XElement.Load(fileName);

                foreach (var item in xml.Elements("item"))
                {
                    if (item.Attribute("id") != null)
                    {
                        var id = item.Attribute("id").GetUInt16();
                        LoadItem(id, item);
                    }
                    else if (item.Attribute("fromid") != null && item.Attribute("toid") != null)
                    {
                        var fromid = item.Attribute("fromid").GetUInt16();
                        var toid = item.Attribute("toid").GetUInt16();

                        for (ushort i = fromid; i <= toid; i++)
                            LoadItem(i, item);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Can not load items.xml", e);
            }
        }

        private void LoadItem(ushort id, XElement item)
        {
            if (id > 20000 && id < 20100)
            {
                id = (ushort)(id - 20000);
                //AddItem(new OtItemType { Id = id });

                if (MaxId < id)
                {
                    MaxId = id;
                }
                items[id] = new ItemType { Id = id };
            }

            var itemType = items[id];

            if (itemType == null)
            {
                Messages.AddWarning("[Warning] Item " + id + " in item.xml not found in items.otb");
                return;
            }

            itemType.Name = item.Attribute("name").GetString();
            itemType.Article = item.Attribute("article").GetString();
            itemType.Plural = item.Attribute("plural").GetString();

            LoadAttributes(itemType, item);
        }

        private void LoadAttributes(ItemType itemType, XElement element)
        {
            foreach (var property in element.Elements("attribute"))
            {
                switch (property.Attribute("key").GetString())
                {
                    case "description":
                        itemType.Description = property.Attribute("value").GetString();
                        break;
                    case "type":
                        /*
                        switch (property.Attribute("value").GetString())
                        {
                            case "container":
                                itemType.Group = ItemGroup.Container;
                                break;
                            case "key":
                                itemType.Group = ItemGroup.Key;
                                break;
                            case "magicfield":
                                itemType.Group = ItemGroup.MagicField;
                                break;
                            case "depot":
                                itemType.Group = ItemGroup.Depot;
                                break;
                            case "mailbox":
                                itemType.Group = ItemGroup.MailBox;
                                break;
                            case "trashholder":
                                if (itemType.Group != ItemGroup.Ground)
                                {
                                    itemType.Group = ItemGroup.TrashHolder;
                                }                                
                                break;
                            case "teleport":
                                itemType.Group = ItemGroup.Teleport;
                                break;
                            case "door":
                                itemType.Group = ItemGroup.Door;
                                break;
                            case "bed":
                                itemType.Group = ItemGroup.Bed;
                                break;
                            case "rune":
                                itemType.Group = ItemGroup.Rune;
                                break;
                        }*/
                        break;
                }
            }
        }

    }
}
