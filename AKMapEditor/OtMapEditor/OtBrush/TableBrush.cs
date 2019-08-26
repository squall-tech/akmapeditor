using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace AKMapEditor.OtMapEditor.OtBrush
{
    public class TableBrush : Brush
    {
        protected UInt16 look_id;
        protected String name;
        protected TableNode[] table_items;


        public class TableType
        {
            public TableType()
            {
                this.chance = 0;
                this.item_id = 0;
            }
            public int chance;
            public ushort item_id;
        }

        public class TableNode
        {
            public TableNode()
            {
                this.total_chance = 0;
            }
            public int total_chance;
            public List<TableType> items = new List<TableType>();
        }

        public TableBrush() : base()
        {
            table_items = new TableNode[7];
            for (int x = 0; x < 7; x++)
            {
                table_items[x] = new TableNode();
            }
        }

        public override int getLookID()
        {
            return look_id;
        }

        public override void setName(string name)
        {
            this.name = name;
        }


        public override string getName()
        {
            return this.name;
        }

        public override bool canDraw(GameMap map, Position pos)
        {
            return true;
        }

        public override bool needBorders()
        {
            return true;
        }

        public override void undraw(GameMap map, Tile tile)
        {
            List<Item> itemsToRemove = new List<Item>();
            foreach (Item it in tile.Items)
            {
                if (it.Type.IsTable)
                {
                    TableBrush tb = it.getTableBrush();
                    if (this.Equals(tb))
                    {
                        itemsToRemove.Add(it);
                    }
                }
            }

            foreach (Item it in itemsToRemove)
            {
                tile.Items.Remove(it);
            }
        }

        public override void draw(GameMap map, Tile tile, object param = null)
        {
            undraw(map, tile); // Remove old

            TableNode tn = table_items[0];
            if (tn.total_chance <= 0)
            {
                return;
            }
            int chance = Global.random.Next(1, tn.total_chance + 1);
            ushort type = 0;
            
            foreach(TableType table_iter in tn.items)
            {
                if (chance <= table_iter.chance)
                {
                    type = table_iter.item_id;
                    break;
                }
                chance -= table_iter.chance;
            }

            if (type != 0)
            {
                tile.addItem(Item.Create(type));
            }
        }

        private static bool hasMatchingTableBrushAtTile(GameMap map, TableBrush table_brush, int x, int y, int z)
        {
            Tile t = map.getTile(x, y, z);
            if (t == null)
            {
                return false;
            }
            
            foreach(Item it in t.Items)            
            {
                TableBrush tb = it.getTableBrush();
                if (tb == table_brush)
                {
                    return true;
                }
            }

            return false;
        }        

        public static void doTables(GameMap map, Tile tile)
        {
            if (tile.hasTable() == false)
            {
                return;
            }

            int x = tile.getPosition().x;
            int y = tile.getPosition().y;
            int z = tile.getPosition().z;

            foreach(Item item in tile.Items)
            {                
                TableBrush table_brush = item.getTableBrush();
                if (table_brush == null)
                {
                    continue;
                }

                bool[] neighbours = new bool[8];

                if (x == 0)
                {
                    if (y == 0)
                    {
                        neighbours[0] = false;
                        neighbours[1] = false;
                        neighbours[2] = false;
                        neighbours[3] = false;
                        neighbours[4] = hasMatchingTableBrushAtTile(map, table_brush, x + 1, y, z);
                        neighbours[5] = false;
                        neighbours[6] = hasMatchingTableBrushAtTile(map, table_brush, x, y + 1, z);
                        neighbours[7] = hasMatchingTableBrushAtTile(map, table_brush, x + 1, y + 1, z);
                    }
                    else
                    {
                        neighbours[0] = false;
                        neighbours[1] = hasMatchingTableBrushAtTile(map, table_brush, x, y - 1, z);
                        neighbours[2] = hasMatchingTableBrushAtTile(map, table_brush, x + 1, y - 1, z);
                        neighbours[3] = false;
                        neighbours[4] = hasMatchingTableBrushAtTile(map, table_brush, x + 1, y, z);
                        neighbours[5] = false;
                        neighbours[6] = hasMatchingTableBrushAtTile(map, table_brush, x, y + 1, z);
                        neighbours[7] = hasMatchingTableBrushAtTile(map, table_brush, x + 1, y + 1, z);
                    }
                }
                else if (y == 0)
                {
                    neighbours[0] = false;
                    neighbours[1] = false;
                    neighbours[2] = false;
                    neighbours[3] = hasMatchingTableBrushAtTile(map, table_brush, x - 1, y, z);
                    neighbours[4] = hasMatchingTableBrushAtTile(map, table_brush, x + 1, y, z);
                    neighbours[5] = hasMatchingTableBrushAtTile(map, table_brush, x - 1, y + 1, z);
                    neighbours[6] = hasMatchingTableBrushAtTile(map, table_brush, x, y + 1, z);
                    neighbours[7] = hasMatchingTableBrushAtTile(map, table_brush, x + 1, y + 1, z);
                }
                else
                {
                    neighbours[0] = hasMatchingTableBrushAtTile(map, table_brush, x - 1, y - 1, z);
                    neighbours[1] = hasMatchingTableBrushAtTile(map, table_brush, x, y - 1, z);
                    neighbours[2] = hasMatchingTableBrushAtTile(map, table_brush, x + 1, y - 1, z);
                    neighbours[3] = hasMatchingTableBrushAtTile(map, table_brush, x - 1, y, z);
                    neighbours[4] = hasMatchingTableBrushAtTile(map, table_brush, x + 1, y, z);
                    neighbours[5] = hasMatchingTableBrushAtTile(map, table_brush, x - 1, y + 1, z);
                    neighbours[6] = hasMatchingTableBrushAtTile(map, table_brush, x, y + 1, z);
                    neighbours[7] = hasMatchingTableBrushAtTile(map, table_brush, x + 1, y + 1, z);
                }

                uint tiledata = 0;
                for (int i = 0; i < 8; i++)
                {
                    if (neighbours[i])
                    {                        
                        tiledata |= Convert.ToUInt32(1) << i;
                    }
                }
                int bt = Border_Types.table_types[tiledata];                

                TableNode tn = table_brush.table_items[bt];
                if (tn.total_chance == 0)
                {
                    return;
                }
                int chance = Global.random.Next(1, tn.total_chance + 1);
                ushort id = 0;                
                foreach(TableType node_iter in tn.items)
                {
                    if (chance <= node_iter.chance)
                    {
                        id = node_iter.item_id;
                        break;
                    }
                    chance -= node_iter.chance;
                }
                if (id != 0)
                {
                    item.setID(id);
                }
            }
        }

        public override void load(XElement node)
        {
            string strVal = "";
            look_id = node.Attribute("lookid").GetUInt16();
            if (node.Attribute("server_lookid").GetUInt16() != 0)
            {
                ushort id = node.Attribute("server_lookid").GetUInt16();
                look_id = id;
            }

            foreach (XElement child in node.Elements())
            {
                if ("table".Equals(child.Name.LocalName))
                {
                    uint alignment = 0;
                    strVal = child.Attribute("align").GetString();
                    if (strVal != "")
                    {
                        if (strVal == "vertical")
                        {
                            alignment = BorderType.TABLE_VERTICAL;
                        }
                        else if (strVal == "horizontal")
                        {
                            alignment = BorderType.TABLE_HORIZONTAL;
                        }
                        else if (strVal == "south")
                        {
                            alignment = BorderType.TABLE_SOUTH_END;
                        }
                        else if (strVal == "east")
                        {
                            alignment = BorderType.TABLE_EAST_END;
                        }
                        else if (strVal == "north")
                        {
                            alignment = BorderType.TABLE_NORTH_END;
                        }
                        else if (strVal == "west")
                        {
                            alignment = BorderType.TABLE_WEST_END;
                        }
                        else if (strVal == "alone")
                        {
                            alignment = BorderType.TABLE_ALONE;
                        }
                        else
                        {
                            Messages.AddWarning("Unknown table alignment '" + strVal);
                            continue;
                        }
                    }
                    else
                    {
                        Messages.AddWarning("Could not read type tag of table node");
                        continue;
                    }

                    foreach (XElement subchild_node in child.Elements())
                    {
                        if ("item".Equals(subchild_node.Name.LocalName))
                        {
                            int id = subchild_node.Attribute("id").GetInt32();
                            int chance = subchild_node.Attribute("chance").GetInt32();

                            if (id == 0)
                            {
                                Messages.AddWarning("Could not read id tag of item node");
                                continue;
                            }

                            if (chance == 0)
                            {
                                Messages.AddWarning("Could not read chance tag of item node");
                                continue;
                            }
                            ItemType it = Global.items.items[id];
                            if (it == null)
                            {
                                Messages.AddWarning("There is no itemtype with id " + id);
                                continue;
                            }

                            if ((it.brush != null) && (it.brush != this))
                            {
                                Messages.AddWarning("itemId " + id + "already has a brush");
                                continue;
                            }
                            it.IsTable = true;
                            it.brush = this;

                            TableType tt = new TableType();
                            table_items[alignment].total_chance += chance;
                            tt.chance = chance;

                            tt.item_id = (UInt16) id;
                            table_items[alignment].items.Add(tt);
                        }
                    }
                    
                }

            }
        }
    }
}
