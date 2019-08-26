using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AKMapEditor.OtMapEditor
{
    public class CopyBuffer
    {

        private Position copyPos;
        private GameMap tiles;
        private byte serialized;
        private UInt64 serialized_size;

        public CopyBuffer()
        {
            tiles = new GameMap();
        }

        public void copy(MapEditor editor, int floor)
        {
            if (editor.selection.size() == 0)
            {
                //gui.SetStatusText(wxT("No tiles to copy."));
                return;
            }

            clear();
            tiles = new GameMap();

            int tile_count = 0;
            int item_count = 0;
            copyPos = new Position(0xFFFF, 0xFFFF, floor);
            
            foreach(Tile tile in editor.selection.getTiles())
            {            
                ++tile_count;

                Tile copied_tile = new Tile(tile.getPosition());

                if ((tile.Ground != null) && tile.Ground.isSelected())
                {
                    copied_tile.house_id = tile.house_id;
                    copied_tile.setMapFlags(tile.getMapFlags());
                }

                List<Item> tile_selection = tile.getSelectedItems();                
                foreach (Item iit in tile_selection)
                {
                    ++item_count;
                    // Copy items to copybuffer
                    copied_tile.addItem(iit.deepCopy());
                }

                if ((tile.creature != null) && tile.creature.isSelected())
                {
                    copied_tile.creature = tile.creature.deepCopy();
                }
                if ((tile.spawn != null) && tile.spawn.isSelected())
                {
                    copied_tile.spawn = tile.spawn.deepCopy();
                }

                tiles.setTile(copied_tile.getPosition(), copied_tile);
                if (copied_tile.Position.X < copyPos.x)
                {
                    copyPos.x = copied_tile.Position.X;
                }

                if (copied_tile.Position.Y < copyPos.y)
                {
                    copyPos.y = copied_tile.Position.Y;
                }
            }
        }

        public void cut(MapEditor editor, int floor)
        {
            if (editor.selection.size() == 0)
            {
               // gui.SetStatusText(wxT("No tiles to cut."));
                return;
            }

            clear();
            tiles = new GameMap();

            int tile_count = 0;
            int item_count = 0;
            copyPos = new Position(0xFFFF, 0xFFFF, floor);

            BatchAction batch = new BatchAction(ActionIdentifier.ACTION_CUT_TILES);
            Action action = new Action(editor);

            List<Position> tilestoborder = new List<Position>();

            foreach (Tile tile in editor.selection.getTiles())
            {       
                tile_count++;
                
                Tile newtile = tile.deepCopy();
                Tile copied_tile = new Tile(tile.getPosition());

                if ((tile.Ground != null) && tile.Ground.isSelected())
                {
                    copied_tile.house_id = newtile.house_id;
                    newtile.house_id = 0;
                    copied_tile.setMapFlags(tile.getMapFlags());
                    newtile.setMapFlags(TileState.TILESTATE_NONE);
                }

                List<Item> tile_selection = newtile.popSelectedItems();
                foreach(Item iit in tile_selection)
                {
                    item_count++;
                    // Add items to copybuffer
                    copied_tile.addItem(iit);
                }

                if ((newtile.creature != null) && newtile.creature.isSelected())
                {
                    copied_tile.creature = newtile.creature;
                    newtile.creature = null;
                }

                if ((newtile.spawn != null) && newtile.spawn.isSelected())
                {
                    copied_tile.spawn = newtile.spawn;
                    newtile.spawn = null;
                }

                tiles.setTile(copied_tile.getPosition(), copied_tile);

                if (copied_tile.Position.X < copyPos.x)
                {
                    copyPos.x = copied_tile.Position.X;
                }

                if (copied_tile.Position.Y < copyPos.y)
                {
                    copyPos.y = copied_tile.Position.Y;
                }

                if (Settings.GetBoolean(Key.USE_AUTOMAGIC))
                {
                    for (int y = -2; y <= 2; y++)
                    {
                        for (int x = -2; x <= 2; x++)
                        {
                            tilestoborder.Add(new Position(tile.getX() + x, tile.getY() + y, tile.getZ()));
                        }
                    }
                }
                action.addChange(new Change(newtile));
            }

            batch.addAndCommitAction(action);

            // Remove duplicates
      //      tilestoborder.sort();
            //tilestoborder.unique();

            if (Settings.GetBoolean(Key.USE_AUTOMAGIC))
            {
                action = new Action(editor);
                foreach(Position it in tilestoborder)
                {
                    Tile tile = editor.map.getTile(it);
                    if (tile != null)
                    {
                        Tile new_tile = tile.deepCopy();
                        new_tile.borderize(editor.map);
                        new_tile.wallize(editor.map);
                        action.addChange(new Change(new_tile));
                    }
                    else
                    {
                        Tile new_tile = new Tile(it);
                        new_tile.borderize(editor.map);
                        if (new_tile.size() != 0)
                        {
                            action.addChange(new Change(new_tile));
                        }
                        else
                        {
                            new_tile = null;
                        }
                    }
                }

                batch.addAndCommitAction(action);
            }

            editor.addBatch(batch);
         //   std.stringstream ss = new std.stringstream();
            //ss << "Cut out " << tile_count << " tile" << (tile_count > 1 ? "s" : "") << " (" << item_count << " item" << (item_count > 1 ? "s" : "") << ")";
            //gui.SetStatusText(wxstr(ss.str()));
        }
        public void paste(MapEditor editor, Position topos)
        {
            if (tiles == null)
                return;

            BatchAction batchAction = new BatchAction(ActionIdentifier.ACTION_PASTE_TILES);

            Action action = new Action(editor);
            
            foreach(Tile buffer_tile in tiles.getTiles().Values)
            {            
                Position pos = buffer_tile.getPosition() - copyPos + topos;

                if (pos.isValid() == false)
                {
                    continue;
                }

                Tile copy_tile = buffer_tile.deepCopy();
                Tile old_dest_tile = editor.map.getTile(pos);
                Tile new_dest_tile = null;

                if (Settings.GetBoolean(Key.MERGE_PASTE) || !(copy_tile.Ground != null))
                {
                    if (old_dest_tile != null)
                    {
                        new_dest_tile = old_dest_tile.deepCopy();
                    }
                    else
                    {
                        new_dest_tile = new Tile(pos);
                    }
                    new_dest_tile.merge(copy_tile);
                    copy_tile = null;
                }
                else
                {
                    // If the copied tile has ground, replace target tile
                    new_dest_tile = copy_tile;
                    copy_tile.Position = pos;
                }

                action.addChange(new Change(new_dest_tile));
            }
            batchAction.addAndCommitAction(action);

            if (Settings.GetBoolean(Key.USE_AUTOMAGIC) && Settings.GetBoolean(Key.BORDERIZE_PASTE))
            {
                /*
                action = new Action(editor);

                List<Position> posToBorder = new List<Position>();
                foreach (Tile it in tiles.getTiles().Values)
                {
                     posToBorder.AddRange(Generic.getTilesToBorder(it.Position));
                    

                }

                posToBorder = Generic.removeSamePosition(posToBorder);

                foreach (Position pos in posToBorder)
                {
                    Tile tile = editor.map.getTile(pos);
                    if (tile != null)
                    {
                        Tile newTile = tile.deepCopy();
                        newTile.borderize(editor.map);
                        action.addChange(new Change(newTile));
                    }
                    else
                    {
                        Tile newTile = new Tile(pos);
                        newTile.borderize(editor.map);
                        if (newTile.Items.Count > 0)
                        {
                            action.addChange(new Change(newTile));
                        }
                    }
                } */

                /*
                List<Tile> borderize_tiles = new List<Tile>();
                // Go through all modified (selected) tiles (might be slow)                    
                foreach(Tile it in tiles.getTiles().Values)
                {
                    bool add_me = false; // If this tile is touched
                    Position pos = it.getPosition() - copyPos + topos;
                    if (pos.z < 0 || pos.z >= 16)
                    {
                        continue;
                    }

                    // Go through all neighbours
                    Tile t;
                    t = editor.map.getTile(pos.x - 1, pos.y - 1, pos.z);
                    if (t != null && !t.isSelected())
                    {
                        borderize_tiles.Add(t);
                        add_me = true;
                    }
                    t = editor.map.getTile(pos.x, pos.y - 1, pos.z);
                    if (t != null && !t.isSelected())
                    {
                        borderize_tiles.Add(t);
                        add_me = true;
                    }
                    t = editor.map.getTile(pos.x + 1, pos.y - 1, pos.z);
                    if (t != null && !t.isSelected())
                    {
                        borderize_tiles.Add(t);
                        add_me = true;
                    }
                    t = editor.map.getTile(pos.x - 1, pos.y, pos.z);
                    if (t != null && !t.isSelected())
                    {
                        borderize_tiles.Add(t);
                        add_me = true;
                    }
                    t = editor.map.getTile(pos.x + 1, pos.y, pos.z);
                    if (t != null && !t.isSelected())
                    {
                        borderize_tiles.Add(t);
                        add_me = true;
                    }
                    t = editor.map.getTile(pos.x - 1, pos.y + 1, pos.z);
                    if (t != null && !t.isSelected())
                    {
                        borderize_tiles.Add(t);
                        add_me = true;
                    }
                    t = editor.map.getTile(pos.x, pos.y + 1, pos.z);
                    if (t != null && !t.isSelected())
                    {
                        borderize_tiles.Add(t);
                        add_me = true;
                    }
                    t = editor.map.getTile(pos.x + 1, pos.y + 1, pos.z);
                    if (t != null && !t.isSelected())
                    {
                        borderize_tiles.Add(t);
                        add_me = true;
                    }
                    if (add_me)
                    {
                        borderize_tiles.Add(editor.map.getTile(pos));
                    }
                }
                // Remove duplicates
           //     borderize_tiles.sort();
                //borderize_tiles.unique();

                // Do le borders!
                foreach(Tile tile in borderize_tiles)
                {                    
                    if (tile.Ground != null)
                    {
                        if (tile.Ground.getGroundBrush() != null)
                        {
                            Tile new_tile = tile.deepCopy();
                            new_tile.borderize(editor.map);
                            if (tile.Ground.isSelected())
                            {
                                new_tile.selectGround();
                            }
                            new_tile.wallize(editor.map);
                            action.addChange(new Change(new_tile));
                        }
                    }
                }
                // Commit changes to map
                 */
             //   batchAction.addAndCommitAction(action);
            }

            editor.addBatch(batchAction);
        }

        public bool canPaste()
        {
            return false;
        }

        public Position getPosition()
        {
            return copyPos;
        }

        public void clear()
        {
            tiles = null;               
        }

        public UInt64 GetTileCount()
        {
            return tiles != null? tiles.size() : 0;
        }

        public GameMap getBufferMap()
        {
            return tiles;
        }


    }
}