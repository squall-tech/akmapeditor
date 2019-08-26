using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace AKMapEditor.OtMapEditor
{
    public class SessionFlags
    {
        public const int NONE = 0;
        public const int INTERNAL = 1;
        public const int SUBTHREAD = 2;
    };

    public class Selection
    {
        private bool busy;
        private MapEditor editor;
        private BatchAction session;
        public Action subsession;

        private List<Tile> tiles = null;

        public Selection(MapEditor editor)
        {
            this.editor = editor;
            this.session = null;
            this.subsession = null;
            this.busy = false;
            tiles = new List<Tile>();
        }


        public int size() 
        {
            return tiles != null ? tiles.Count() : 0;
        }

        public List<Tile> getTiles()
        {
            return tiles;
        }

        public void add(Tile tile, Item item)
        {
            Debug.Assert(subsession != null);
            Debug.Assert(tile != null);
            Debug.Assert(item != null);

            if (item.isSelected())
                return;

            item.select();
            Tile new_tile = tile.deepCopy();
            item.deselect();

            //if (Settings.GetBoolean(Key.BORDER_IS_GROUND))
            if (Settings.GetBoolean(Key.USE_AUTOMAGIC))
            {
                if (item.isBorder())
                {
                    new_tile.selectGround();
                }
            }

            subsession.addChange(new Change(new_tile));
        }
        public void add(Tile tile, Spawn spawn)
        {
            if (spawn.isSelected())
                return;
            
            spawn.select();
            Tile new_tile = tile.deepCopy();
            spawn.deselect();

            subsession.addChange(new Change(new_tile));
        }
        public void add(Tile tile, Creature creature)
        {
            if (creature.isSelected())
                return;

            // Make a copy of the tile with the item selected
            creature.select();
            Tile new_tile = tile.deepCopy();
            creature.deselect();

            subsession.addChange(new Change(new_tile));
        }
        public void add(Tile tile)
        {
            Tile new_tile = tile.deepCopy();
            new_tile.select();

            subsession.addChange(new Change(new_tile));
        }
        public void remove(Tile tile, Item item)
        {
            bool tmp = item.isSelected();
            item.deselect();
            Tile new_tile = tile.deepCopy();
            if (tmp)
            {
                item.select();
            }
            //if (item.isBorder() && Settings.GetBoolean(Key.BORDER_IS_GROUND))
            if (item.isBorder() && Settings.GetBoolean(Key.USE_AUTOMAGIC))
            {
                new_tile.deselectGround();
            }

            subsession.addChange(new Change(new_tile));
        }
        public void remove(Tile tile, Spawn spawn)
        {
            bool tmp = spawn.isSelected();
            spawn.deselect();
            Tile new_tile = tile.deepCopy();
            if (tmp)
            {
                spawn.select();
            }

            subsession.addChange(new Change(new_tile));
        }
        public void remove(Tile tile, Creature creature)
        {
            bool tmp = creature.isSelected();
            creature.deselect();
            Tile new_tile = tile.deepCopy();
            if (tmp)
            {
                creature.select();
            }

            subsession.addChange(new Change(new_tile));
        }
        public void remove(Tile tile)
        {
            Tile new_tile = tile.deepCopy();
            new_tile.deselect();

            subsession.addChange(new Change(new_tile));
        }

        public void addInternal(Tile tile)
        {
            tiles.Add(tile);
        }
        public void removeInternal(Tile tile)
        {
            if (tiles.Count == 0)
                return;

            tiles.Remove(tile);
        }


        public void clear()
        {
            if (session != null)
            {                
                foreach(Tile it in tiles)
                {
                    Tile new_tile = it.deepCopy();
                    new_tile.deselect();
                    subsession.addChange(new Change(new_tile));
                }
            }
            else
            {
                foreach (Tile it in tiles)
                {
                    it.deselect();
                }
                tiles.Clear();
            }
        }

        public bool isBusy() 
        { 
            return busy; 
        }

        public void start(int flags = 0)
        {
            if ((flags & SessionFlags.INTERNAL) == 0)
            {
                if ((flags & SessionFlags.SUBTHREAD) != 0)
                {
                    ;
                }
                else
                {
                    session = new BatchAction(ActionIdentifier .ACTION_SELECT);
                }
                subsession = new Action(editor);
            }            
            busy = true;
        }

        public void commit()
        {
            if (session != null)
            {
                Debug.Assert(subsession != null);
                // We need to step out of the session before we do the action, else peril awaits us!
                BatchAction tmp = session;
                session = null;
                // Do the action
                tmp.addAndCommitAction(subsession);
                
                subsession = new Action(editor);
                session = tmp;
            }
        }
        public void finish(int flags = 0)
        {
            if ((flags & SessionFlags.INTERNAL) == 0)
            {
                if ((flags & SessionFlags.SUBTHREAD)!= 0)
                {
                    Debug.Assert(subsession != null);
                    subsession = null;
                }
                else
                {
                    Debug.Assert(session != null);
                    Debug.Assert(subsession != null);
                    // We need to exit the session before we do the action, else peril awaits us!
                    BatchAction tmp = session;
                    session = null;

                    tmp.addAndCommitAction(subsession);
                    editor.addBatch(tmp, 2);

                    session = null;
                    subsession = null;
                }
            }
            busy = false;
        }

        public void join(SelectionThread thread)
        {
            thread.Wait();
            session.addAction(thread.result);            
        }

        public Tile getSelectedTile()
        {
            if (tiles.Count > 0)
            {
                return tiles.First();
            }

            return null;            
        }

        public void updateSelectionCount()
        {
            //
        }
        
    }

    public class SelectionThread
    {
        private MapEditor editor;
        private Position start;
        private Position end;
        public Selection selection;
        public Action result;
        private bool finish = false;

        public SelectionThread(MapEditor editor, Position start, Position end)
        {
            this.editor = editor;
            this.start = start;
            this.end = end;
            this.selection = new Selection(editor);
            result = null;
        }

        public void Execute()
        {
            Thread t = new Thread(Entry);
            t.Start();            
        }

        public void Wait()
        {
            while(!finish) 
            {
                Thread.Sleep(10);
            }
        }

        public void Entry()
        {
            selection.start(SessionFlags.SUBTHREAD);
            for (int z = start.z; z >= end.z; --z)
            {
                for (int x = start.x; x <= end.x; ++x)
                {
                    for (int y = start.y; y <= end.y; ++y)
                    {
                        Tile tile = editor.map.getTile(x, y, z);
                        if (tile == null)
                            continue;

                        selection.add(tile);
                    }
                }
                if (z <= 7 && Settings.GetBoolean(Key.COMPENSATED_SELECT))
                {
                    ++start.x;
                    ++start.y;
                    ++end.x;
                    ++end.y;
                }
            }
            result = selection.subsession;
            selection.finish(SessionFlags.SUBTHREAD);
            finish = true;
        }
    }
}
