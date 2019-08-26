using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brush = AKMapEditor.OtMapEditor.OtBrush.Brush;
using AKMapEditor.OtMapEditor.OtBrush;
using System.Windows.Forms;
using ProtoBuf;

namespace AKMapEditor.OtMapEditor
{    
    public class TileState
    {
	    public const uint TILESTATE_NONE             = 0x0000;
	    public const uint TILESTATE_PROTECTIONZONE   = 0x0001;
	    public const uint TILESTATE_DEPRECATED       = 0x0002; // Reserved
	    public const uint TILESTATE_NOPVP            = 0x0004;
	    public const uint TILESTATE_NOLOGOUT         = 0x0008;
	    public const uint TILESTATE_PVPZONE          = 0x0010;
	    public const uint TILESTATE_REFRESH          = 0x0020;
	    // Internal  u
	    public const uint TILESTATE_SELECTED         = 0x0001;
	    public const uint TILESTATE_UNIQUE           = 0x0002;
	    public const uint TILESTATE_BLOCKING         = 0x0004;
	    public const uint TILESTATE_OP_BORDER        = 0x0008; // If this is true, gravel will be placed on the tile!
	    public const uint TILESTATE_HAS_TABLE        = 0x0010;
	    public const uint TILESTATE_HAS_CARPET       = 0x0020;
        public const uint TILESTATE_MODIFIED         = 0x0040;
    };

    [ProtoContract]
    public class Tile
    {
        [ProtoMember(1)]
        private Position position;
        [ProtoMember(2)]
        private Item ground;

        [ProtoMember(3)]
        private List<Item> items_list;

        [ProtoMember(4)]
        public UInt32 mapFlags;

        [ProtoMember(5)]
        public uint spawn_count;

        [ProtoMember(6)]
        public Creature creature;

        [ProtoMember(7)]
        public Spawn spawn;

        

        public uint waypoint_count;
//        public UInt16 version;

        public UInt32 HouseId;        
        public UInt32 statFlags;
        public List<UInt16> house_exits;


        public UInt32 house_id;

        public Position Position { get { return position; } set { position = value; } }
        public Item Ground { get { return ground; } set { ground = value; } }      
        public int ItemCount { get { return items == null ? 0 : items.Count; } }

        #region TileRegion
        public Tile()
        {            
            items_list = new List<Item>();
        }
        public Tile(Position position)
        {
            this.position = position;            
            items_list = new List<Item>();
        }

        public Tile(int x, int y, int z)
        {
            this.position = new Position(x, y, z);            
            items = new List<Item>();
        }

        public List<Item> Items
        {
            get
            {                
                return items;
            }
        }

        public int getX() { return Position.X; }
        public int getY() { return Position.Y; }
        public int getZ() { return Position.Z; }

        public int size()  
        {            
            return (ground != null ? 1 : 0) + items.Count + (creature != null ? 1 : 0) + (spawn != null ? 1 : 0) + (int)spawn_count + (int)waypoint_count + (house_exits != null ? 1 : 0);
        }

        public Position getPosition()
        {
            return Position;
        }

        private List<Item> items
        {
            set
            {
                 items_list = value;
            }
            get
            {
                lock (items_list)
                {
                    return items_list;
                }                
            }
        }

        public bool isEmpty()
        {
            return (ground == null || items.Count < 1);
        }

        public bool hasProperty(ItemProperty prop)
        {
            return false;
        }

        public void addItem(Item item)
        {
         
            if (item == null) return;
            if (item.Type == null) return;

            if (item.Type.Group == ItemGroup.Ground)                
            {
                ground = item;               
            }
            else
            {
                ItemType GroundEquivalent = item.Type.GroundEquivalent;

                int it = 0;

                if (GroundEquivalent != null)
                {
                    ground = Item.Create(GroundEquivalent);
                }
                else
                {
                    if (item.Type.alwaysOnBottom)
                    {
                        while (true)
                        {
                            if (it == items.Count)
                            {
                                break;
                            }
                            else if (items[it].Type.alwaysOnBottom)
                            {
                                if (item.Type.AlwaysOnTopOrder < items[it].Type.AlwaysOnTopOrder)
                                {
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                            it++;
                        }
                    }
                    else
                    {
                        it = items.Count;
                    }
                }                
                items.Insert(it, item);
            }

            if (item.isSelected())
            {
                statFlags |= TileState.TILESTATE_SELECTED;
            }
        }

        public bool isPZ()
        {
            return false;
        }

        public bool isHouseExit()
        {
            return false;
        }

        public bool isHouseTile()
        {
            return false;
        }

        public int getHouseID()
        {
            return 0;
        }

        public void update()
        {
            statFlags &= TileState.TILESTATE_MODIFIED;
           
            if (spawn != null && spawn.isSelected())
            {
                statFlags |= TileState.TILESTATE_SELECTED;
            }
            if (creature != null && creature.isSelected())
            {
                statFlags |= TileState.TILESTATE_SELECTED;
            }

            if (ground != null)
            {
                if (ground.isSelected())
                {
                    statFlags |= TileState.TILESTATE_SELECTED;
                }
                if (ground.Type.BlockObject)
                {
                    statFlags |= TileState.TILESTATE_BLOCKING;
                }
                if (ground.getUniqueID() != 0)
                {
                    statFlags |= TileState.TILESTATE_UNIQUE;
                }
            }
            
            foreach(Item i in Items)            
            {                
                if (i.isSelected())
                {
                    statFlags |= TileState.TILESTATE_SELECTED;
                }
                if (i.getUniqueID() != 0)
                {
                    statFlags |= TileState.TILESTATE_UNIQUE;
                }

                ItemType it = Global.items.items[i.Type.Id];
                if (it.BlockObject)
                {
                    statFlags |= TileState.TILESTATE_BLOCKING;
                }
                if (it.IsOptionalBorder)
                {
                    statFlags |= TileState.TILESTATE_OP_BORDER;
                }
                if (it.IsTable)
                {
                    statFlags |= TileState.TILESTATE_HAS_TABLE;
                }
                if (it.IsCarpet)
                {
                    statFlags |= TileState.TILESTATE_HAS_CARPET;
                }                
            }

            if ((statFlags & TileState.TILESTATE_BLOCKING) == 0)
            {
                if (ground == null && items.Count() == 0)
                {
                    statFlags |= TileState.TILESTATE_BLOCKING;
                }
            }
        }

        
        public bool hasStatFlag(uint state)
        {
            return (this.statFlags & state) != 0;
        }

        public bool hasMapFlag(uint state)
        {
            return (this.mapFlags & state) != 0;
        } 

        public void stealExits(Tile from) 
        {
            //ASSERT(house_exits == NULL);
	        //house_exits = from->house_exits;
	        //from->house_exits = NULL;
        }


        public Tile deepCopy()
        {
            Tile copy = new Tile(Position.x, Position.y, Position.z);
            copy.mapFlags = this.mapFlags;
            copy.statFlags = this.statFlags;
            copy.house_id = house_id;
            if (spawn != null)
            {
                copy.spawn = spawn.deepCopy();
            }
            if (creature != null)
            {
                copy.creature = creature.deepCopy();
            }
            // Spawncount & exits are not transferred on copy!
            if (ground != null)
            {
                copy.ground = ground.deepCopy();
            }
            foreach (Item it in Items)
            {
                copy.addItem(it.deepCopy());
            }            

            return copy;
        }

        public bool isBlocking()
        {
            return (statFlags & TileState.TILESTATE_BLOCKING) != 0;
        }

        public void merge(Tile other)
        {
            /*
            if (other.isPZ())
            {
                setPZ(true);
            }
            if (other.house_id)
            {
                house_id = other.house_id;
            }

            if (other.creature)
            {
                creature = null;
                creature = other.creature;
                other.creature = null;
            }

            if (other.spawn)
            {
                spawn = null;
                spawn = other.spawn;
                other.spawn = null;
            }

            if (other.creature)
            {
                creature = null;
                creature = other.creature;
                other.creature = null;
            } */                


            if (other.Ground != null)
            {
                Ground = null;
                Ground = other.Ground;
                other.Ground = null;
            }

            foreach (Item item in other.Items)
            {
                addItem(item);
            }
            
            other.items.Clear();
        }

        public bool hasGround()
        {
            return getGroundBrush() != null;
        }

        public bool hasWall()
        {
            return false;
        }

        #endregion

        public bool hasCarpet()
        {
            return (statFlags & TileState.TILESTATE_HAS_CARPET) != 0;
        }

        public bool hasTable()
        {
            return (statFlags & TileState.TILESTATE_HAS_TABLE) != 0;            
        }

        public void setMapFlags(UInt32 _flags) 
        {
            mapFlags = _flags | mapFlags;
        }

        public void unsetMapFlags(UInt32 _flags)
        {
            mapFlags &= ~_flags;
        }

        public UInt32 getMapFlags()
        {
            return mapFlags;
        }

        #region GroundBrush
        public void clearBorders()
        {
            List<Item> deletedItens = new List<Item>();
            foreach (Item item in items)
            {
                if (item.Type.IsBorder)
                {
                    deletedItens.Add(item);
                }
            }
            foreach (Item item in deletedItens)
            {
                items.Remove(item);                
            }
        }
        
        public GroundBrush getGroundBrush()
        {
            if ((ground != null) && (ground.Type.brush != null))
            {
                if (typeof(GroundBrush).Equals(ground.Type.brush.GetType()))
                {
                    return (GroundBrush) ground.Type.brush;
                }
            }

            return null;
        }

        public bool hasOptionalBorder()
        {
            return (statFlags & TileState.TILESTATE_OP_BORDER) != 0;
        }

        public void setOptionalBorder(bool optionalBorder)
        {
            if (optionalBorder)
            {
                statFlags |= TileState.TILESTATE_OP_BORDER;
            }
            else
            {
                statFlags &= ~TileState.TILESTATE_OP_BORDER;
            }
        }

        public void addBorderItem(Item itemBorder)
        {
            if (itemBorder != null)
            {                
                if  (Settings.GetBoolean(Key.RAW_LIKE_SIMONE) &&
                    (itemBorder.Type.alwaysOnBottom && itemBorder.Type.AlwaysOnTopOrder == 2))
                {
                    foreach (Item item in this.Items)
                    {
                        if (item.getTopOrder() == itemBorder.Type.AlwaysOnTopOrder)
                        {                            
                            return;
                        }
                    }
                }
                Items.Insert(0, itemBorder);                
            }            
        }

        public void borderize(GameMap map)
        {
            GroundBrush.doBorders(map, this);
        }

        #endregion

        #region WallBrush

        public void addWallItem(Item wallItem)
        {
            if (wallItem == null)
            {
                return;
            }

            if (!wallItem.Type.IsWall) return;


            if (Settings.GetBoolean(Key.RAW_LIKE_SIMONE) &&
                (wallItem.Type.alwaysOnBottom && wallItem.Type.AlwaysOnTopOrder == 2))
            {
                startRemove();
                foreach (Item item in Items)
                {
                    if (item.getTopOrder() == wallItem.Type.AlwaysOnTopOrder)
                    {
                        AddItemToRemove(item);
                        break;
                    }
                }
                RemoveItems();
            }
            addItem(wallItem);
        }

        public void cleanWalls(bool dontdelete)
        {
            //
        }

        public void cleanWalls(WallBrush wb)
        {
            List<Item> deletedItens = new List<Item>();
            foreach (Item item in items)
            {
                if (wb != null)
                {
                    if ((item.Type.IsWall) && wb.hasWall(item))
                    {
                        deletedItens.Add(item);
                    }
                }
                else
                {
                    if (item.Type.IsWall)
                    {
                        deletedItens.Add(item);
                    }
                }

            }
            foreach (Item item in deletedItens)
            {
                items.Remove(item);
            }
        }

        public void wallize(GameMap map)
        {
            WallBrush.doWalls(map, this);
        }

        public bool containWallBrush()
        {
            return false;
        }
        #endregion

        #region TableBrush

        public void tableize(GameMap map)
        {
            TableBrush.doTables(map, this);
        }

        #endregion

        #region CarpetBrush

        public void carpetize(GameMap map)
        {
            CarpetBrush.doCarpets(map, this);
        }

        #endregion

        #region DoodadBrush
        public bool containDoodadBrush()
        {
            return false;
        }
        #endregion

        #region RawBrush
        public bool containRaw()
        {
            bool selected = false;

            if (ground != null)
            {
                selected = true;
            }

            foreach (Item item in items)
            {
                selected = true;
            }
            return selected;
        }
        #endregion



        // selection

        public void select()
        {
            if (size() == 0)
                return;

            if (ground != null)
            {
                ground.select();
            }
            if (spawn != null)
            {
                spawn.select();
            }
            if (creature != null)
            {
                creature.select();
            }

            foreach (Item it in Items)
            {
                it.select();
            }

            statFlags |= TileState.TILESTATE_SELECTED;
        }

        public void deselect()
        {
            if (ground != null)
            {
                ground.deselect();
            }
            if (spawn != null)
            {
                spawn.deselect();
            }
            if (creature != null)
            {
                creature.deselect();
            }


            foreach (Item it in Items)
            {
                it.deselect();
            }

            statFlags &= ~TileState.TILESTATE_SELECTED;            
        }

        public bool isSelected() 
        {
            return (statFlags & TileState.TILESTATE_SELECTED) != 0;
        }

        public void selectGround()
        {
            bool selected_ = false;
            if (ground != null)
            {
                ground.select();
                selected_ = true;
            }  
          
            if(Settings.GetBoolean(Key.USE_AUTOMAGIC))
            {
                foreach (Item it in Items)
                {
                    if (it.isBorder())
                    {
                        it.select();
                        selected_ = true;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (selected_)
            {
                statFlags |= TileState.TILESTATE_SELECTED;
            }
        }
        public void deselectGround()
        {
            if (ground!= null)
            {
                ground.deselect();
            }

            foreach (Item it in Items)
            {
                if (it.isBorder())
                {
                    it.deselect();
                }
            }
        }


        public Item getTopItem()
        {
            if (Items.Count > 0 && Items.Last().Type.isMetaItem() == false)
                return items.Last();
            if ((ground != null) && ground.Type.isMetaItem() == false)
            {
                return ground;
            }
            return null;
        }

        public List<Item> getSelectedItems()
        {
            List<Item> selected_items = new List<Item>();

            if (!isSelected()) return selected_items;

            if ((ground != null) && (ground.isSelected()))
            {
                selected_items.Add(ground);
            }

            foreach (Item it in Items)
            {
                if (it.isSelected())
                {
                    selected_items.Add(it);
                } 
            }            
            return selected_items;
        }

        public Item getTopSelectedItem()
        {
            Item ret = null;
            if ((ground != null) && ground.isSelected())
            {
                ret = ground;
            }
            foreach (Item it in Items)
            {
                if (it.isSelected())
                {
                    ret = it;
                }
            }
            return ret;
        }

        public List<Item> popSelectedItems()
        {
            List<Item> pop_items = new List<Item>();

            if (!isSelected())
            {
                return pop_items;
            }

            if ((Ground != null) && Ground.isSelected())
            {
                pop_items.Add(ground);
                ground = null;
            }

            List<Item> itemsToRemove = new List<Item>();

            foreach(Item it in Items)
            {            
                if (it.isSelected())
                {
                    pop_items.Add(it);
                    itemsToRemove.Add(it);
                }                
            }

            foreach (Item it in itemsToRemove)
            {
                this.Items.Remove(it);
            }

            statFlags &= ~TileState.TILESTATE_SELECTED;
            return pop_items;
        }

        public TableBrush getTableBrush()
        {
            TableBrush ret = null;
            if ((this.ground != null) && (ground.getTableBrush() != null))
            {
                ret = ground.getTableBrush();
            }

            foreach (Item it in Items)
            {
                if (it.getTableBrush() != null)
                {
                    ret = it.getTableBrush();
                }
            }
            return ret;
        }

        public CarpetBrush getCarpetBrush()
        {
            CarpetBrush ret = null;
            if ((this.ground != null) && (ground.getCarpetBrush() != null))
            {
                ret = ground.getCarpetBrush();
            }

            foreach (Item it in Items)
            {
                if (it.getCarpetBrush() != null)
                {
                    ret = it.getCarpetBrush();
                }
            }
            return ret;
        }

        public Brush getDoodadBrush()
        {
            Brush ret = null;
            if ((this.ground != null) && (ground.getDoodadBrush() != null))
            {
                ret = ground.getDoodadBrush();
            }

            foreach (Item it in Items)
            {
                if (it.getDoodadBrush() != null)
                {
                    ret = it.getDoodadBrush();
                }
            }
            return ret;
        }

        public WallBrush getWallBrush()
        {
            WallBrush ret = null;
            if ((this.ground != null) && (ground.getWallBrush() != null))
            {
                ret = ground.getWallBrush();
            }

            foreach (Item it in Items)
            {
                if (it.getWallBrush() != null)
                {
                    ret = it.getWallBrush();
                }
            }
            return ret;
        }

        public CreatureBrush getCreatureBrush()
        {
            if (creature != null)
            {
                return creature.getBrush();
            }
            return null;
        }

        public Item getWall()
        {
            foreach (Item it in Items)
            {
                if (it.Type.IsWall)
                {
                    return it;
                }
            }
            return null;
        }


        private List<Item> itemsToRemove = null;

        public void startRemove()
        {
            if (itemsToRemove == null) itemsToRemove = new List<Item>();
            itemsToRemove.Clear();
        }

        public void AddItemToRemove(Item item)
        {
            if (itemsToRemove != null) itemsToRemove.Add(item);
        }

        public void RemoveItems()
        {
            if (itemsToRemove != null)
            {
                foreach (Item it in itemsToRemove)
                {
                    Items.Remove(it);
                }
                itemsToRemove.Clear();
                itemsToRemove = null;
            }
            
        }


    }
}

