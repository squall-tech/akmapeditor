using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace AKMapEditor.OtMapEditor.OtBrush
{
    public enum EDoorType
    {
        WALL_UNDEFINED,
        WALL_ARCHWAY,
        WALL_DOOR_NORMAL,
        WALL_DOOR_LOCKED,
        WALL_DOOR_QUEST,
        WALL_DOOR_MAGIC,
        WALL_WINDOW,
        WALL_HATCH_WINDOW,
    };

    public class WallBrush : TerrainBrush
    {

        protected WallNode[] wall_items;
        public List<DoorType>[] door_items;
        public WallBrush redirect_to;



        public class WallNode {
            public int total_chance;
            public List<WallType> items;

            public WallNode() 
            {
                items = new List<WallType>();                
                total_chance = 0;
            }
	    };

        public class WallType
        {
            public int chance;
            public UInt16 id;
        };

        public class DoorType {
		    public EDoorType type;
            public UInt16 id;
	    };

        public WallBrush() : base()
        {
            wall_items = new WallNode[17];
            door_items = new List<DoorType>[17];
            for (int x = 0; x < 17; x++)
            {
                door_items[x] = new List<DoorType>();
            }
            for (int x = 0; x < 17; x++)
            {
                wall_items[x] = new WallNode();
            }
        }

        private static Random random = new Random();

        public override void draw(GameMap map, Tile tile, object param)
        {
            if (false)
            {

            }
            tile.cleanWalls(this);
            UInt16 id = 0;
            WallBrush try_brush = this;

            while (true)
            {
                if (id != 0) break;
                if (try_brush == null) return;

                for (int i = 0; i < 16; ++i)
                {
                    WallNode wn = try_brush.wall_items[i];
                    if (wn.total_chance <= 0)
                    {
                        continue;
                    }
                    int chance = random.Next(1, wn.total_chance + 1);

                    foreach (WallType wt in wn.items)
                    {
                        if (chance <= wt.chance)
                        {
                            id = wt.id;
                            break;
                        }
                    }
                    if (id != 0)
                    {
                        break;
                    }
                }
            }
            tile.addWallItem(Item.Create(id));
        }

        public static bool hasMatchingWallBrushAtTile(GameMap map, WallBrush wall_brush, int x, int y, int z)
        {

            Tile t = map.getTile(x, y, z);
            if (t == null) return false;

            foreach (Item item in t.Items)  
            {
                if (item.Type.IsWall)
                {
                    WallBrush wb = item.getWallBrush();
                    if (wb == wall_brush)
                    {
                        return !item.Type.WallHateMe;
                    }
                    else if (wall_brush.friendOf(wb) || wb.friendOf(wall_brush))
                    {
                        return !item.Type.WallHateMe;
                    }
                }
            }

            return false;
        }

        public static void doWalls(GameMap map, Tile tile) 
        {
            int x = tile.Position.X;
            int y = tile.Position.Y;
            int z = tile.Position.Z;

            List<Item> items_to_add = new List<Item>();
            List<Item> items_to_erase = new List<Item>();

            foreach (Item wall in tile.Items)
            {
                if (!wall.Type.IsWall)
                {
                    continue;
                }
                WallBrush wall_brush = wall.getWallBrush();
                if (wall_brush == null)
                {
                    continue;
                }


                if (wall_brush as WallDecorationBrush != null)
                {
                    items_to_add.Add(wall);                    
                    continue;
                }


                bool[] neighbours = new bool[4];
                if (x == 0)
                {
                    if (y == 0)
                    {
                        neighbours[0] = false;
                        neighbours[1] = false;
                        neighbours[2] = hasMatchingWallBrushAtTile(map, wall_brush, x + 1, y, z);
                        neighbours[3] = hasMatchingWallBrushAtTile(map, wall_brush, x, y + 1, z);
                    }
                    else
                    {
                        neighbours[0] = hasMatchingWallBrushAtTile(map, wall_brush, x, y - 1, z);
                        neighbours[1] = false;
                        neighbours[2] = hasMatchingWallBrushAtTile(map, wall_brush, x + 1, y, z);
                        neighbours[3] = hasMatchingWallBrushAtTile(map, wall_brush, x, y + 1, z);
                    }
                }
                else if (y == 0)
                {
                    neighbours[0] = false;
                    neighbours[1] = hasMatchingWallBrushAtTile(map, wall_brush, x - 1, y, z);
                    neighbours[2] = hasMatchingWallBrushAtTile(map, wall_brush, x + 1, y, z);
                    neighbours[3] = hasMatchingWallBrushAtTile(map, wall_brush, x, y + 1, z);
                }
                else
                {
                    neighbours[0] = hasMatchingWallBrushAtTile(map, wall_brush, x, y - 1, z);
                    neighbours[1] = hasMatchingWallBrushAtTile(map, wall_brush, x - 1, y, z);
                    neighbours[2] = hasMatchingWallBrushAtTile(map, wall_brush, x + 1, y, z);
                    neighbours[3] = hasMatchingWallBrushAtTile(map, wall_brush, x, y + 1, z);
                }
                UInt32 tiledata = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (neighbours[i])
                    {                        
                        tiledata |= (UInt32) 1 << i;
                    }
                }
                bool exit = false;
                for (int i = 0; i < 2; ++i)
                { 
                    if (exit)
                    {
                        break;
                    }

                    int bt;
                    if (i == 0)
                    {
                        bt = Border_Types.full_border_types[tiledata];
                    }
                    else
                    {
                        bt = Border_Types.half_border_types[tiledata];
                    }
                    if (wall.getWallAlignment() == BorderType.WALL_UNTOUCHABLE)
                    {
                        items_to_add.Add(wall);
                       // items_to_remove.Add(wall);                        
                        exit = true;
                    }
                    else if (wall.getWallAlignment() == bt)
                    {
                        items_to_add.Add(wall);
                        //it = tile->items.erase(it);
                        exit = true;
                        //while(it != tile->items.end()) {
                        //  todo
                        // }
                    }
                    else
                    {
                        UInt16 id = 0;
				        WallBrush try_brush = wall_brush;

				        while(true) {
					        if(try_brush == null) break;
					        if(id != 0) break;

					        WallNode wn = try_brush.wall_items[(int) bt];
					        if(wn.total_chance <= 0) {
						        if(wn.items.Count() == 0) {
							        try_brush = try_brush.redirect_to;
							        if(try_brush == wall_brush) break; // To prevent infinite loop
							        continue;
						        } else {
							        id = wn.items[0].id;
						        }
					        } else {
						        int chance = random.Next(1, wn.total_chance + 1);
                                foreach (WallType node_iter in wn.items)
                                {
                                    if(chance <= node_iter.chance) {
								        id = node_iter.id;
								        break;
							        }
                                }
					        }
					        // Propagate down the chain
					        try_brush = try_brush.redirect_to;
					        if(try_brush == wall_brush) break; // To prevent infinite loop
				        }
				        if(try_brush == null && id == 0) {
					        if(i == 1) {
						        //++it;
					        }
					        continue;
				        } else {
					        // If there is such an item, add it to the tile
					        Item new_wall = Item.Create(id);
					        if(wall.isSelected()) {
//						        new_wall.isSelected = true;
					        }
					        items_to_add.Add(new_wall);
					        exit = true;
					        //++it;
				        }
                    }

                }
            }
            tile.cleanWalls(null);
            foreach (Item item in items_to_add)
            {
                tile.addWallItem(item);
            }

        }

        public bool hasWall(Item item)
        {
            int bt = item.getWallAlignment();

            WallBrush test_wall = this;
            while (test_wall != null)
            {
                foreach (WallType wt in test_wall.wall_items[bt].items)
                {
                    if (wt.id == item.Type.Id)
                    {
                        return true;
                    }
                }
                foreach (DoorType dt in test_wall.door_items[bt])
                {
                    if (dt.id == item.Type.Id)
                    {
                        return true;
                    }
                }
                test_wall = test_wall.redirect_to;
                if (test_wall == this) return false;
            }
            return false;                
        }

        public override void load(XElement brush)
        {            
            look_id = brush.Attribute("lookid").GetUInt16();
            if (brush.Attribute("server_lookid").GetUInt16() != 0)
            {
                look_id = brush.Attribute("server_lookid").GetUInt16();
            }

            foreach (XElement wall_brush in brush.Elements("wall"))
            {
                int alignment = 0;
                String type = wall_brush.Attribute("type").GetString();
                if (!"".Equals(type))
                {
                    if (type == "vertical")
                    {
                        alignment = BorderType.WALL_VERTICAL;
                    }
                    else if (type == "horizontal")
                    {
                        alignment = BorderType.WALL_HORIZONTAL;
                    }
                    else if (type == "corner")
                    {
                        alignment = BorderType.WALL_NORTHWEST_DIAGONAL;
                    }
                    else if (type == "pole")
                    {
                        alignment = BorderType.WALL_POLE;
                    }
                    else if (type == "south end")
                    {
                        alignment = BorderType.WALL_SOUTH_END;
                    }
                    else if (type == "east end")
                    {
                        alignment = BorderType.WALL_EAST_END;
                    }
                    else if (type == "north end")
                    {
                        alignment = BorderType.WALL_NORTH_END;
                    }
                    else if (type == "west end")
                    {
                        alignment = BorderType.WALL_WEST_END;
                    }
                    else if (type == "south T")
                    {
                        alignment = BorderType.WALL_SOUTH_T;
                    }
                    else if (type == "east T")
                    {
                        alignment = BorderType.WALL_EAST_T;
                    }
                    else if (type == "west T")
                    {
                        alignment = BorderType.WALL_WEST_T;
                    }
                    else if (type == "north T")
                    {
                        alignment = BorderType.WALL_NORTH_T;
                    }
                    else if (type == "northwest diagonal")
                    {
                        alignment = BorderType.WALL_NORTHWEST_DIAGONAL;
                    }
                    else if (type == "northeast diagonal")
                    {
                        alignment = BorderType.WALL_NORTHEAST_DIAGONAL;
                    }
                    else if (type == "southwest diagonal")
                    {
                        alignment = BorderType.WALL_SOUTHWEST_DIAGONAL;
                    }
                    else if (type == "southeast diagonal")
                    {
                        alignment = BorderType.WALL_SOUTHEAST_DIAGONAL;
                    }
                    else if (type == "intersection")
                    {
                        alignment = BorderType.WALL_INTERSECTION;
                    }
                    else if (type == "untouchable")
                    {
                        alignment = BorderType.WALL_UNTOUCHABLE;
                    }
                    else
                    {
                        Messages.AddWarning("Unknown wall alignment '" + type + "'");
                        continue;
                    }

                }
                else
                {
                    Messages.AddWarning("Could not read type tag of wall node");
                    continue;
                }
                foreach (XElement item_wall_brush in wall_brush.Elements("item"))
                {
                    int id = 0;
                    int chance = 0;

                    id = item_wall_brush.Attribute("id").GetInt32();

                    if (id == 0)
                    {
                        Messages.AddWarning("Could not read id tag of item node");
                        break;
                    }

                    chance = item_wall_brush.Attribute("chance").GetInt32();
                    ItemType it = Global.items.items[id];
                    if (it == null)
                    {

                        Messages.AddWarning("There is no itemtype with id " + id);                        
                        continue;
                    }

                    if ((it.brush != null) && (it.brush != this))
                    {
                        Messages.AddWarning("Itemtype id " + id + " already has a brush");
                        continue;
                    }
                    it.IsWall = true;
                    it.brush = this;
                    it.BorderAlignment = alignment;

                    WallType wt = new WallType();
                    //issi é la do tipo
                    wall_items[alignment].total_chance += chance;
                    wt.chance = wall_items[alignment].total_chance;
                    wt.id = (UInt16) id;
                    wall_items[alignment].items.Add(wt);


                }
                foreach (XElement door_wall_brush in wall_brush.Elements("door"))
                {
                    String type_door = "";					
					int chance = 0;
					bool isOpen;
					bool hate = false;
                    int id = door_wall_brush.Attribute("id").GetInt32();
                    if (id == 0)
                    {
                        Messages.AddWarning("Could not read id tag of item node");
                        break;
                    }
                    chance = door_wall_brush.Attribute("chance").GetInt32();
                    type_door = door_wall_brush.Attribute("type").GetString();
                    if (!"".Equals(type_door))
                    {
                        String strVal = door_wall_brush.Attribute("open").GetString();
                        if (!"".Equals(strVal))
                        {
                            isOpen = Generic.isTrueString(strVal);
                        }
                        else
                        {
                            isOpen = true;
                            if ((!type_door.Equals("window")) && (!type_door.Equals("any window")) && (!type_door.Equals("hatch window")))
                            {
                                Messages.AddWarning("Could not read open tag of item node");
                                break;
                            }
                        }
                    }
                    else
                    {
                        Messages.AddWarning("Could not read type tag of item node");
                        break;
                    }

                    if (!"".Equals(door_wall_brush.Attribute("type").GetString()))
                    {
                        hate = Generic.isTrueString(door_wall_brush.Attribute("type").GetString());
                    }

                    ItemType it = Global.items.items[id];
                    if (it == null)
                    {
                        Messages.AddWarning("There is no itemtype with id " + id);
                        continue;
                    }
                    it.IsWall = true;
                    it.brush = this;
                    it.IsBrushDoor = true;
                    it.WallHateMe = hate;
                    it.IsOpen = isOpen;
                    it.BorderAlignment = alignment;

                    DoorType dt = new DoorType();
                    bool all_windows = false;
                    bool all_doors = false;
                    if (type_door == "normal")
                    {
                        dt.type = EDoorType.WALL_DOOR_NORMAL;
                    }
                    else if (type_door == "locked")
                    {
                        dt.type = EDoorType.WALL_DOOR_LOCKED;
                    }
                    else if (type_door == "quest")
                    {
                        dt.type = EDoorType.WALL_DOOR_QUEST;
                    }
                    else if (type_door == "magic")
                    {
                        dt.type = EDoorType.WALL_DOOR_MAGIC;
                    }
                    else if (type_door == "archway")
                    {
                        dt.type = EDoorType.WALL_ARCHWAY;
                    }
                    else if (type_door == "window")
                    {
                        dt.type = EDoorType.WALL_WINDOW;
                    }
                    else if (type_door == "hatch_window" || type_door == "hatch window")
                    {
                        dt.type = EDoorType.WALL_HATCH_WINDOW;
                    }
                    else if (type_door == "any door")
                    {
                        all_doors = true;
                    }
                    else if (type_door == "any window")
                    {
                        all_windows = true;
                    }
                    else if (type_door == "any")
                    {
                        all_windows = true;
                        all_doors = true;
                    }
                    else
                    {
                        Messages.AddWarning("Unknown door type '" + type_door + "'");
                        break;
                    }
                    dt.id = (UInt16) id;
                    if (all_windows)
                    {                        
                        dt.type = EDoorType.WALL_WINDOW; door_items[alignment].Add(dt);
                        dt.type = EDoorType.WALL_HATCH_WINDOW; door_items[alignment].Add(dt);
                    }

                    if (all_doors)
                    {
                        dt.type = EDoorType.WALL_ARCHWAY; door_items[alignment].Add(dt);
                        dt.type = EDoorType.WALL_DOOR_NORMAL; door_items[alignment].Add(dt);
                        dt.type = EDoorType.WALL_DOOR_LOCKED; door_items[alignment].Add(dt);
                        dt.type = EDoorType.WALL_DOOR_QUEST; door_items[alignment].Add(dt);
                        dt.type = EDoorType.WALL_DOOR_MAGIC; door_items[alignment].Add(dt);
                    }
                    if (!all_doors && !all_windows)
                    {
                        door_items[alignment].Add(dt);
                    }                    
                }
            }
            foreach (XElement efriend_brush in brush.Elements("friend"))
            {
                String friendName = efriend_brush.Attribute("name").GetString();
                if (friendName != "")
                {
                    if ("all".Equals(friendName))
                    {
                        //nada
                    }
                    else
                    {
                        Brush friend_brush = Brushes.getInstance().getBrush(friendName);
                        if (friend_brush != null)
                        {
                            friends.Add(friend_brush.getID());
                        }
                        else
                        {
                            Messages.AddWarning("Brush '" + friendName + "' is not defined.");
                        }
                        String redirect = efriend_brush.Attribute("redirect").GetString();
                        if ((!"".Equals(redirect)) && Generic.isTrueString(redirect))
                        {
                            WallBrush rd = null;
                            try
                            {
                                rd = (WallBrush)friend_brush;
                            }
                            catch// (Exception ex)
                            {
                                Messages.AddWarning("Wall brush redirect link: '" + redirect + "' is not a wall brush.");
                            }
                            if (redirect_to == null)
                            {
                                redirect_to = rd;
                            }
                        }
                    }
                }                
            }
        }
    }
}
