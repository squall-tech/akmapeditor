using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AKMapEditor.OtMapEditor.OtBrush
{
    public class OptionalBorderBrush : Brush
    {

        public override bool canDraw(GameMap map, Position pos)
        {
            Tile tile = map.getTile(pos);

            // You can't do gravel on a mountain tile

            

            if (tile != null)
            {                
                GroundBrush bb = tile.getGroundBrush();
                if (bb != null)
                {
                    if (bb.hasOptionalBorder())
                    {
                        return false;
                    }
                }
                //tile.clearBorders();
            }

            int x = pos.x;
            int y = pos.y;
            int z = pos.z;


            tile = map.getTile(x - 1, y - 1, z); if (vTile(tile)) return true;
            tile = map.getTile(x, y - 1, z); if (vTile(tile)) return true;
            tile = map.getTile(x + 1, y - 1, z); if (vTile(tile)) return true;
            tile = map.getTile(x - 1, y, z); if (vTile(tile)) return true;
            tile = map.getTile(x + 1, y, z); if (vTile(tile)) return true;
            tile = map.getTile(x - 1, y + 1, z); if (vTile(tile)) return true;
            tile = map.getTile(x, y + 1, z); if (vTile(tile)) return true;
            tile = map.getTile(x + 1, y + 1, z); if (vTile(tile)) return true;

            return false;
        }

        private bool vTile(Tile tile)
        {
            if (tile != null)
            {
                GroundBrush bb = tile.getGroundBrush();
                if (bb != null)
                    if (bb.hasOptionalBorder())
                        return true;
            }
            return false;
        }


        public override void undraw(GameMap map, Tile tile)
        {
            tile.setOptionalBorder(false);
        }
        public override void draw(GameMap map, Tile tile, object param = null)
        {
            tile.setOptionalBorder(true);
        }

        public override string getName()
        {
            return "Optional Border Tool";
        }
    }
}
