using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AKMapEditor.OtMapEditor.OtBrush
{
    public class SpawnBrush : Brush
    {
        public override void draw(GameMap map, Tile tile, object param = null)
        {
            if (tile.spawn == null)
            {
                int value = 0;
                if (param != null && typeof(int).Equals(param.GetType()))
                {
                    value = (int)param;
                }

                tile.spawn = new Spawn(Math.Max(1,value));
            }
        }

        public override void undraw(GameMap map, Tile tile)
        {
            tile.spawn = null;
        }

        public override bool canDraw(GameMap map, Position pos)
        {
            Tile tile = map.getTile(pos);
            if (tile != null && tile.spawn != null)
            {
                return false;
            }

            return true;
        }

        public override bool canSmear()
        {
            return false;
        }

        public override bool canDrag()
        {
            return true;
        }

        public override bool oneSizeFitsAll()
        {
            return true;
        }
    }

}