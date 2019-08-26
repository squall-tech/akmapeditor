using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AKMapEditor.OtMapEditor.OtBrush
{
    public class DoorBrush : Brush
    {
        protected EDoorType doortype;

        public DoorBrush(EDoorType doorType) : base()
        {
            this.doortype = doorType;
        }

        public override bool canDraw(GameMap map, Position pos)
        {
            Tile tile = map.getTile(pos);
            if (tile == null)
            {
                return false;
            }
            Item item = tile.getWall();
            if (item == null)
            {
                return false;
            }
            WallBrush wb = item.getWallBrush();
            if (wb == null)
            {
                return false;
            }

            int wall_alignment = item.getWallAlignment();

            ushort discarded_id = 0; // The id of a discarded match
            bool close_match = false;

            bool open = false;
            if (item.Type.IsBrushDoor)
            {
                open = item.Type.IsOpen;
            }

            WallBrush test_brush = wb;
            do
            {
                foreach (WallBrush.DoorType dt in test_brush.door_items[wall_alignment])                
                {                  
                    if (dt.type == doortype)
                    {
                        //Debug.Assert(dt.id);
                        ItemType it =  Global.items.items[dt.id];                        
                        if (it.IsOpen == open)
                        {
                            return true;
                        }
                        else if (close_match == false)
                        {
                            discarded_id = dt.id;
                            close_match = true;
                        }
                        if (!close_match && discarded_id == 0)
                        {
                            discarded_id = dt.id;
                        }
                    }
                }
                test_brush = test_brush.redirect_to;
            } while (test_brush != wb && test_brush != null);
            // If we've found no perfect match, use a close-to perfect
            if (discarded_id != 0)
            {
                return true;
            }

            return false;
                        
        }

        public override void draw(GameMap map, Tile tile, object param = null)
        {
            List<Pair<Item, Item>> replaceItems = new List<Pair<Item, Item>>();
            foreach(Item item in tile.Items)            
            {		            
		        if (item.Type.IsWall == false)
		        {			            
			        continue;
		        }
		        WallBrush wb = item.getWallBrush();
		        if (wb == null)
		        {			        
			        continue;
		        }

		        int wall_alignment = item.getWallAlignment();

		        ushort discarded_id = 0; // The id of a discarded match
		        bool close_match = false;
		        bool perfect_match = false;

		        bool open = false;
                if (param != null)
		        {
                    open = (Boolean)param;
		        }

		        if (item.Type.IsBrushDoor)
		        {
                    open = item.Type.IsOpen;
		        }

                WallBrush test_brush = wb;
		        do
		        {
                    foreach (WallBrush.DoorType dt in test_brush.door_items[wall_alignment]) 
			        {				        
				        if (dt.type == doortype)
				        {					 
					        ItemType it = Global.items.items[dt.id];					        

					        if (it.IsOpen == open)
					        {
						  //      item = Item.transformItem(item, dt.id, tile);
                                //replaceItems.Add(new Pair<Item,Item>(item,Item.transformItem(item, dt.id, tile)));
                                item.setID(dt.id);
						        perfect_match = true;
						        break;
					        }
					        else if (close_match == false)
					        {
						        discarded_id = dt.id;
						        close_match = true;
					        }
					        if (!close_match && discarded_id == 0)
					        {
						        discarded_id = dt.id;
					        }
				        }
			        }
			        test_brush = test_brush.redirect_to;
			        if (perfect_match)
			        {
				        break;
			        }
		        } while (test_brush != wb && test_brush != null);

		            // If we've found no perfect match, use a close-to perfect
		        if (perfect_match == false && discarded_id != 0)
		        {
			     //   item = Item.transformItem(item, discarded_id, tile);
                   // replaceItems.Add(new Pair<Item, Item>(item, Item.transformItem(item, discarded_id, tile)));
                    item.setID(discarded_id);
		        }

                /* HOUSE
		        if (Settings.getInteger(Config.AUTO_ASSIGN_DOORID) && tile.isHouseTile())
		        {
			        Map mmap = map as Map;
			        Door door = item as Door;
			        if (mmap != null && door != null)
			        {
				        House house = mmap.houses.getHouse(tile.getHouseID());
				        Debug.Assert(house);
				        Map real_map = map as Map;
				        if (real_map != null)
				        {
					        door.setDoorID(house.getEmptyDoorID());
				        }
			        }
		        }*/
                /*
		            // We need to consider decorations!
		        while (true)
		        {
			        // Vector has been modified, before we can use the iterator again we need to find the wall item again
			        item_iter = tile.items.begin();
			        while (true)
			        {
				        if (item_iter == tile.items.end())
				        {
					        return;
				        }
				        if (*item_iter == item)
				        {
					        ++item_iter;
					        if (item_iter == tile.items.end())
					        {
						        return;
					        }
					        break;
				        }
				        ++item_iter;
			        }
			        // Now it points to the correct item!

			        item = *item_iter;
			        if (item.isWall())
			        {
				        if (WallDecorationBrush * wdb = item.getWallBrush() as WallDecorationBrush)
				        {
					        // We got a decoration!
					        for (List<WallBrush.DoorType>.Enumerator iter = wdb.door_items[wall_alignment].begin(); iter.MoveNext();)
					        {
						        WallBrush.DoorType dt = iter.Current;
						        if (dt.type == doortype)
						        {
							        Debug.Assert(dt.id);
							        ItemType it = item_db[dt.id];
							        Debug.Assert(it.id != 0);

							        if (it.isOpen == open)
							        {
								        item = transformItem(item, dt.id, tile);
								        perfect_match = true;
								        break;
							        }
							        else if (close_match == false)
							        {
								        discarded_id = dt.id;
								        close_match = true;
							        }
							        if (!close_match && discarded_id == 0)
							        {
								        discarded_id = dt.id;
							        }
						        }
					        }
					        // If we've found no perfect match, use a close-to perfect
					        if (perfect_match == false && discarded_id != 0)
					        {
						        item = transformItem(item, discarded_id, tile);
					        }
					        continue;
				        }
			        }
			        break;
		        } */
                // If we get this far in the loop we should return                
            }

            foreach (Pair<Item, Item> replace in replaceItems)
            {
                int index = tile.Items.IndexOf(replace.first);
                if (index > -1)
                {
                    tile.Items.RemoveAt(index);
                    tile.Items.Insert(index, replace.second);
                }
            }
        }

        public override void undraw(GameMap map, Tile tile)
        {
            foreach(Item item in tile.Items)            
            {                
                if (item.Type.IsBrushDoor)
                {
                    item.getWallBrush().draw(map, tile, null);
                    if (Settings.GetBoolean(Key.USE_AUTOMAGIC))
                    {
                        tile.wallize(map);
                    }
                    return;
                }
            }            
        }

        public static void switchDoor(Item door)
        {
        }

        public override bool oneSizeFitsAll()
        {
            return true;
        }


    }
}