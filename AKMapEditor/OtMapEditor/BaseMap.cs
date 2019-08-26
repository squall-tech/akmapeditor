using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AKMapEditor.OtMapEditor
{
    public class BaseMap
    {
        private UInt64 tilecount;
        private QTreeNode root;

        public BaseMap()
        {
            tilecount = 0;
            root = new QTreeNode();
        }
        public void clear(bool del = true)
        {
            //
        }
        public UInt64 size() {return tilecount;}

        public QTreeNode getRoot()
        {
            return root;
        }

        public List<Tile> getTiles()
        {
            List<Tile> ret = new List<Tile>();


            return ret;
        }

        
	    public Tile getTile(int x, int y, int z)
        {            
            QTreeNode leaf = root.getLeaf(x, y);
            if (leaf != null)
            {
                Floor floor = leaf.getFloor(z);
                if (floor != null)
                {
                    return floor.tiles[(x & 3) * 4 + (y & 3)];
                }
            }
            return null;
        }

	    public Tile getTile(Position pos)
        {
            return getTile(pos.x, pos.y, pos.z);
        }

	    public QTreeNode getLeaf(int x, int y) 
        {
            return root.getLeaf(x, y);
        }

        public void setTile(Position pos, Tile newtile, bool remove = false)
        {
            setTile(pos.x, pos.y, pos.z, newtile, remove);
        
        }

        public void removeTile(Position pos)
        {
            removeTile(pos.x, pos.y, pos.z);
        }

        public void removeTile(int x, int y, int z)
        {
            QTreeNode leaf = root.getLeafForce(x, y);
            Floor floor = leaf.createFloor(z);
            int offsetX = x & 3;
            int offsetY = y & 3;
            floor.tiles[offsetX * 4 + offsetY] = null;
        }

        public void setTile(int x, int y, int z, Tile newtile, bool remove = false)
        {
 
	        QTreeNode leaf = root.getLeafForce(x, y);
	        Floor floor = leaf.createFloor(z);
	        int offsetX = x & 3;
            int offsetY = y & 3;

	        Tile tile = floor.tiles[offsetX*4+offsetY];
	        if(newtile!=null) {
		        if(tile!=null) {
			        newtile.stealExits(tile);
			        //newtile->spawn_count = tile->spawn_count;
			        //newtile->spawn = tile->spawn;
		        } else {
			        tilecount++;
		        }
	        } else if(tile!=null) {
		        if((tile.spawn_count != 0) || tile.isHouseExit()) {
			        newtile = new Tile(x, y, z);
			        newtile.stealExits(tile);
			        newtile.spawn_count = tile.spawn_count;
			        newtile.waypoint_count = tile.waypoint_count;
			        //newtile->spawn = tile->spawn;
		        } else {
			        tilecount--;
		        }
	        }
	        if(remove) tile = null;
            floor.tiles[offsetX * 4 + offsetY] = newtile;

        }

        public Tile swapTile(Position pos, Tile newtile)
        {
            return swapTile(pos.x, pos.y, pos.z,newtile);
        }

        public Tile swapTile(int x, int y, int z, Tile newtile)
        {

            QTreeNode leaf = root.getLeafForce(x, y);
            Floor floor = leaf.createFloor(z);
            int offset_x = x & 3;
            int offset_y = y & 3;
            Tile tile = floor.tiles[offset_x * 4 + offset_y];
            if (newtile != null)
            {
                if (tile == null)
                {
                    ++tilecount;
                }
                else
                {
                    newtile.stealExits(tile);
                    newtile.spawn_count = tile.spawn_count;
                    newtile.waypoint_count = tile.waypoint_count;
                    //newtile->spawn = tile->spawn;
                }
            }
            else if (tile != null)
            {
                if ((tile.spawn_count != 0) || tile.isHouseExit())
                {
                    newtile = new Tile(x, y, z);
                    newtile.stealExits(tile);
                    newtile.spawn_count = tile.spawn_count;
                    newtile.waypoint_count = tile.waypoint_count;
                    //newtile->spawn = tile->spawn;
                }
                else
                {
                    --tilecount;
                }
            }
            Tile oldtile = tile;
            floor.tiles[offset_x * 4 + offset_y] = newtile;
            return oldtile;
        }
    }

    public class Floor
    {
        public Tile[] tiles;

        public Floor()
        {
            tiles = new Tile[16];
        }
    }

    public class QTreeNode
    {
        public Floor[] array;
        public QTreeNode[] child;
        protected bool isLeaf;

        public QTreeNode()
        {
            isLeaf = false;
            array = new Floor[16];
            child = new QTreeNode[16];
        }

        public bool IsLeaf()
        {
            return isLeaf;
        }

        public Tile getTile(int x, int y, int z)
        {
            if (!isLeaf) return null;
            Floor f = array[z];
            if (f == null) return null;
            return f.tiles[x * 4 + y];           
        }

        public Floor getFloor(int z)
        {
            if (!isLeaf) return null;
            if ((z > 15) || (z <= 0)) return null;
            return array[z];
        }

        public QTreeNode getLeaf(int x, int y)
        {
            QTreeNode node = this;
            int cx = x, cy = y;
            while (node != null)
            {
                if (node.isLeaf)
                {
                    return node;
                }
                else
                {
                    int index = ((cx & 0xC000) >> 14) | ((cy & 0xC000) >> 12);
                    if (node.child[index] != null)
                    {
                        node = node.child[index];
                        cx <<= 2;
                        cy <<= 2;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            return null;
        }

        public QTreeNode getLeafForce(int x, int y)
        {
            QTreeNode node = this;
            int cx = x, cy = y;
            int level = 6;
            while (node != null)
            {
                int index = ((cx & 0xC000) >> 14) | ((cy & 0xC000) >> 12);

                QTreeNode qt = node.child[index];
                if (qt!= null)
                {
                    if (qt.isLeaf)
                    {
                        return qt;
                    }
                }
                else
                {
                    if (level == 0)
                    {
                        qt = new QTreeNode();
                        qt.isLeaf = true;
                        node.child[index] = qt;
                        return qt;
                    }
                    else
                    {
                        qt = new QTreeNode();
                        node.child[index] = qt;
                    }
                }
                node = node.child[index];
                cx <<= 2;
                cy <<= 2;
                level -= 1;
            }
            return null;
        }
        public Floor createFloor(int z)
        {
            if (array[z] == null)
            {
                array[z] = new Floor();
            }
            return array[z];
        }
    }

    public class MapIterator : System.Collections.IEnumerable
    {
        private GameMap map;

        public MapIterator(GameMap map)
        {
            this.map = map;
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            /* Funciona mas é lento
            QTreeNode root = map.getRoot();
            int Height = map.Height;

            for (int z = 0; z < 16; z++)
            {
                for (int x = 0; x < Height; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        Tile tile = map.getTile(x, y, z);
                        if (tile != null)
                        {
                            yield return tile;
                        }
                    }
                }
            } 


            if (false)
            {
                yield return null;
            } */
            yield return null;
        }
    }
}
