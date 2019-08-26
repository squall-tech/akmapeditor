using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AKMapEditor.OtMapEditor
{
    public class SelectionType
    {
        public const int SELECT_CURRENT_FLOOR = 1;
        public const int SELECT_ALL_FLOORS = 2;
        public const int SELECT_VISIBLE_FLOORS = 3;
    }

    public enum Key
    {
        NONE,
        DEFAULT_CLIENT_VERSION,
        DATA_PATCH,
        CHECK_FILE_SIGNATURES,
        VERSION_DATA_DIR,
        USE_AUTOMAGIC,
        UNDO_SIZE,
        GROUP_ACTIONS,
        SHOW_ALL_FLOORS,
        SHOW_SHADE,
        SHOW_SPECIAL_TILES,
        SHOW_BLOCKING,
        HIDE_ITEMS_WHEN_ZOOMED,
        SHOW_ITEMS,
        HIGHLIGHT_ITEMS,
        SHOW_CREATURES,
        SHOW_SPAWNS,
        SHOW_HOUSES,
        RAW_LIKE_SIMONE,
        BORDER_IS_GROUND,
        MERGE_PASTE,
        BORDERIZE_PASTE,
        SELECTION_TYPE,
        COMPENSATED_SELECT,
        WORKER_THREADS,
        BORDERIZE_DRAG,
        BORDERIZE_DRAG_THRESHOLD,
        MERGE_MOVE,
        BLOCKING_VERSION,
        AUTOSAVE_TIME,
        MAKE_BACKUP_ONSAVE,
        DEFAULT_SPAWNTIME,
        MAX_SPAWN_RADIUS,
        ALLOW_CREATURES_WITHOUT_SPAWN
    }


    public class Settings
    {        
        private static Dictionary<Key, object> settings = new Dictionary<Key, object>();

        public static void Load()
        {
            Generic.getAppDir();
        }

        public static void Save()
        {
        }

        public static void SetValue(Key key, Object value)
        {
            Object retorno = null;
            if (settings.TryGetValue(key, out retorno))
            {
                settings[key] = value;
            }
            else
            {
                settings.Add(key, value);
            }
            
        }

        public static String GetString(Key key)
        {
            return GetObject(key).ToString();
        }

        public static int GetInteger(Key key)
        {
            Object retorno = GetObject(key);
            if (retorno != null)
            {
                return Convert.ToInt32(retorno);
            }
            else
            {
                return 0;
            }
        }

        public static Boolean GetBoolean(Key key)
        {
            Object retorno = GetObject(key);
            if (retorno != null)
            {
                return Convert.ToBoolean(retorno);
            }
            else
            {
                return false;
            }
        }

        public static Object GetObject(Key key)
        {
            Object retorno = null;
            settings.TryGetValue(key, out retorno);
            return retorno;
        }

        public static void SetDefault()
        {
            settings.Add(Key.DEFAULT_CLIENT_VERSION, 40);
            settings.Add(Key.CHECK_FILE_SIGNATURES, false);
            settings.Add(Key.DATA_PATCH, "C:\\AKMapEditor\\AKMapEditor\\data");
            settings.Add(Key.USE_AUTOMAGIC, true);
            settings.Add(Key.UNDO_SIZE, 100);
            settings.Add(Key.GROUP_ACTIONS, true);
            settings.Add(Key.SHOW_ALL_FLOORS, true);
            settings.Add(Key.SHOW_SHADE, true);
            settings.Add(Key.SHOW_SPECIAL_TILES, true);
            settings.Add(Key.SHOW_BLOCKING, false);
            settings.Add(Key.HIDE_ITEMS_WHEN_ZOOMED, true);
            settings.Add(Key.SHOW_ITEMS, true);
            settings.Add(Key.HIGHLIGHT_ITEMS, false);
            settings.Add(Key.SHOW_CREATURES, true);
            settings.Add(Key.SHOW_SPAWNS, true);             
            settings.Add(Key.SHOW_HOUSES, true);
            settings.Add(Key.RAW_LIKE_SIMONE, true);
            settings.Add(Key.BORDER_IS_GROUND, false);
            settings.Add(Key.MERGE_PASTE, true);
            settings.Add(Key.BORDERIZE_PASTE, true);
            settings.Add(Key.SELECTION_TYPE, 1);
            settings.Add(Key.COMPENSATED_SELECT, true);
            settings.Add(Key.WORKER_THREADS, 1);
            settings.Add(Key.BORDERIZE_DRAG, true);
            settings.Add(Key.BORDERIZE_DRAG_THRESHOLD, 1000);
            settings.Add(Key.MERGE_MOVE, true);
            settings.Add(Key.BLOCKING_VERSION, true);
            settings.Add(Key.AUTOSAVE_TIME, 60);
            settings.Add(Key.MAKE_BACKUP_ONSAVE, true);
            settings.Add(Key.DEFAULT_SPAWNTIME, 60);
            settings.Add(Key.MAX_SPAWN_RADIUS, 100);
            settings.Add(Key.ALLOW_CREATURES_WITHOUT_SPAWN, false);
        }
    }
}

