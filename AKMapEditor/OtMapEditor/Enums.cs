using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AKMapEditor.OtMapEditor
{
    public enum ItemProperty
    {
        BLOCKSOLID,
        HASHEIGHT,
        BLOCKPROJECTILE,
        BLOCKPATHFIND,
        PROTECTIONZONE,
        ISVERTICAL,
        ISHORIZONTAL,
        MOVEABLE,
        BLOCKINGANDNOTMOVEABLE,
    };

    public enum OtItemAttribute
    {
        NONE = 0,
        //DESCRIPTION = 1,
        //EXT_FILE = 2,
        TILE_FLAGS = 3,
        ACTION_ID = 4,
        UNIQUE_ID = 5,
        TEXT = 6,
        DESC = 7,
        TELE_DEST = 8,
        ITEM = 9,
        DEPOT_ID = 10,
        //EXT_SPAWN_FILE = 11,
        RUNE_CHARGES = 12,
        //EXT_HOUSE_FILE = 13,
        HOUSEDOORID = 14,
        COUNT = 15,
        DURATION = 16,
        DECAYING_STATE = 17,
        WRITTENDATE = 18,
        WRITTENBY = 19,
        SLEEPERGUID = 20,
        SLEEPSTART = 21,
        CHARGES = 22,
        CONTAINER_ITEMS = 23,
        NAME = 30,
        PLURALNAME = 31,
        ATTACK = 33,
        EXTRAATTACK = 34,
        DEFENSE = 35,
        EXTRADEFENSE = 36,
        ARMOR = 37,
        ATTACKSPEED = 38,
        HITCHANCE = 39,
        SHOOTRANGE = 40,
        ARTICLE = 41,
        SCRIPTPROTECTED = 42,
        DUALWIELD = 43,
        QUATRO6 = 46,
        CENTOE1 = 101,
        CINTO7 = 57,
        ATTRIBUTE_MAP = 128
    };

    public enum EditorSprite
    {
        EDITOR_SPRITE_SELECTION_MARKER = -1000,
        EDITOR_SPRITE_BRUSH_CD_1x1,
        EDITOR_SPRITE_BRUSH_CD_3x3,
        EDITOR_SPRITE_BRUSH_CD_5x5,
        EDITOR_SPRITE_BRUSH_CD_7x7,
        EDITOR_SPRITE_BRUSH_CD_9x9,
        EDITOR_SPRITE_BRUSH_CD_15x15,
        EDITOR_SPRITE_BRUSH_CD_19x19,
        EDITOR_SPRITE_BRUSH_SD_1x1,
        EDITOR_SPRITE_BRUSH_SD_3x3,
        EDITOR_SPRITE_BRUSH_SD_5x5,
        EDITOR_SPRITE_BRUSH_SD_7x7,
        EDITOR_SPRITE_BRUSH_SD_9x9,
        EDITOR_SPRITE_BRUSH_SD_15x15,
        EDITOR_SPRITE_BRUSH_SD_19x19,
        EDITOR_SPRITE_OPTIONAL_BORDER_TOOL,
        EDITOR_SPRITE_ERASER,
        EDITOR_SPRITE_PZ_TOOL,
        EDITOR_SPRITE_PVPZ_TOOL,
        EDITOR_SPRITE_NOLOG_TOOL,
        EDITOR_SPRITE_NOPVP_TOOL,
        EDITOR_SPRITE_DOOR_NORMAL,
        EDITOR_SPRITE_DOOR_LOCKED,
        EDITOR_SPRITE_DOOR_MAGIC,
        EDITOR_SPRITE_DOOR_QUEST,
        EDITOR_SPRITE_WINDOW_NORMAL,
        EDITOR_SPRITE_WINDOW_HATCH,
        EDITOR_SPRITE_SELECTION_GEM,
        EDITOR_SPRITE_DRAWING_GEM,
        EDITOR_SPRITE_LAST
    };

    public class SpriteEnum{
	    public const int SPRITE_FLAG_GREEN = 2020;
	    public const int SPRITE_FLAG_GREY = 2019;
	    public const int SPRITE_FLAME_BLUE = 1397;
        public const int SPRITE_SPAWN = 1507;
    };

    public class UpdateType
    {
        public const int UPDATE_TYPE_COMMIT = 1;
        public const int UPDATE_TYPE_UNDO = 2;
        public const int UPDATE_TYPE_REDO = 3;
    }
}
