using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AKMapEditor.OtMapEditor.OtBrush
{
    public class EraserBrush : Brush
    {

        public override bool canDraw(GameMap map, Position pos)
        {
            return true;
        }

        public override void draw(GameMap map, Tile tile, object param = null)
        {
            // Draw is undraw, undraw is super-undraw!
            List<Item> itemToRemove = new List<Item>();

            foreach (Item it in tile.Items)
            {
                itemToRemove.Add(it);
            }

            foreach(Item it in itemToRemove)
            {
                tile.Items.Remove(it);
            }            
        }

        public override void undraw(GameMap map, Tile tile)
        {
            this.draw(map, tile, null);

            if (tile.Ground != null)
            {
                tile.Ground = null;
            }
        }

        public override bool needBorders()
        {
            return true;
        }

        public override string getName()
        {
            return "";
        }

        public override bool canDrag()
        {
            return true;
        }
    }
}