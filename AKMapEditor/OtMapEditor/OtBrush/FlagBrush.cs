using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AKMapEditor.OtMapEditor.OtBrush
{
    public class FlagBrush : Brush
    {
        protected uint flag;
        public FlagBrush(uint flag) : base()
        {
            this.flag = flag;
        }

        public override bool canDraw(GameMap map, Position pos)
        {
            Tile tile = map.getTile(pos);
            return ((tile != null) && tile.hasGround());
        }

        public override void draw(GameMap map, Tile tile, object param = null)
        {
            if (tile.hasGround())
            {
                tile.setMapFlags(flag);
            }
            
        }

        public override void undraw(GameMap map, Tile tile)
        {
            tile.unsetMapFlags(flag);
        }

        public override bool canDrag()
        {
            return true;
        }

        public override string getName()
        {
            switch (flag)
            {
                case TileState.TILESTATE_PROTECTIONZONE:
                    return "PZ brush (0x01)";
                case TileState.TILESTATE_NOPVP:
                    return "No combat zone brush (0x04)";
                case TileState.TILESTATE_NOLOGOUT:
                    return "No logout zone brush (0x08)";
                case TileState.TILESTATE_PVPZONE:
                    return "PVP Zone brush (0x10)";
            }
            return "Unknown flag brush";
        }
    }
}
