using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AKMapEditor.OtMapEditor.OtBrush
{
    public class CreatureBrush : Brush
    {
        private CreatureType creature_type;
        public CreatureBrush(CreatureType creature_type)
        {
            this.creature_type = creature_type;
        }


        public override string getName()
        {
            if (creature_type != null)
            {
                return creature_type.name;
            }
            return "Creature Brush";
        }

        public override bool canDraw(GameMap map, Position pos)
        {
            Tile tile = map.getTile(pos);
            if (creature_type != null && tile != null && !tile.isBlocking())
            {
                if (tile.spawn_count == 0)
                {
                    if (Settings.GetBoolean(Key.ALLOW_CREATURES_WITHOUT_SPAWN))
                    {
                        if (tile.isPZ())
                        {
                            if (creature_type.isNpc)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    if (tile.isPZ())
                    {
                        if (creature_type.isNpc)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override void undraw(GameMap map, Tile tile)
        {
            tile.creature = null;
        }

        public override void draw(GameMap map, Tile tile, object param = null)
        {
            if (canDraw(map, tile.getPosition()))
            {
                undraw(map, tile);
                if (creature_type != null)
                {
                    tile.creature = new Creature(creature_type);
                    tile.creature.setSpawnTime((int)param);
                }
            }
        }


        public CreatureType getType()
        {
            return creature_type;
        }

        public override uint getSpriteLookID()
        {
            /*
            if (creature_type != null && (creature_type.outfit.lookItem != 0))
            {
                return Global.gfx.getCreatureSprite(creature_type.outfit.lookItem).getSpriteID;
            }
             */
            if (creature_type != null)
            {
                if (creature_type.outfit.lookItem != 0)
                {                    
                    return (uint) creature_type.outfit.lookItem;
                }
                if (creature_type.outfit.lookType != 0)
                {
                    return (uint) creature_type.outfit.lookType;
                    //GameSprite spr = Global.gfx.getCreatureSprite(creature_type.outfit.lookType);
                    /*
                    if (spr == null)
                        return 0;

                    int tme = 0; //GetTime() % itype->FPA;
                    for (int cx = 0; cx != spr.width; ++cx)
                    {
                        for (int cy = 0; cy != spr.height; ++cy)
                        {
                           // int texnum = spr.getHardwareID(cx, cy, 2, creature_type.outfit, tme); //south
                           // glBlitTexture(screenx - cx * 32, screeny - cy * 32, texnum, red, green, blue, alpha);
                        }
                    }
                     * */
                }    
            } 
                     
            return 0;
        }

    }
}
