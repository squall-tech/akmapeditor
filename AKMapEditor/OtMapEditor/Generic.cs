using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using Brush = AKMapEditor.OtMapEditor.OtBrush.Brush;
using AKMapEditor.OtMapEditor;
using AKMapEditor.OtMapEditor.OtBrush;

namespace AKMapEditor.OtMapEditor
{

    public class Global
    {
        public static ItemDatabase items;
        public static GraphicManager gfx;
        public static MainForm mainForm;
        public static MapCanvas mapCanvas;
        public static EditorPalette selectedEditorPalette; // paleta selecionada.        
        public static String inicialMap;
        public static Random random = new Random();
        public static EditorMode editorMode;
    }



    public class Generic
    {
        public static String getAppDir()
        {
            return Application.StartupPath;
        }

        public static string OpenMapFile()
        {
            if (Global.mapCanvas != null) Global.mapCanvas.ctrlActive = false;   
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "OpenTibia Binary Map (*.otbm)|*.otbm";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return openFileDialog.FileName;
            }
            else
            {
                return "";
            } 
        }


        public static string SaveMapFile()
        {
            if (Global.mapCanvas != null) Global.mapCanvas.ctrlActive = false;
            var openFileDialog = new SaveFileDialog();
            openFileDialog.Filter = "OpenTibia Binary Map (*.otbm)|*.otbm";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return openFileDialog.FileName;
            }
            else
            {
                return "";
            }
        }

        public static string OpenTibiaFolder()
        {
            var openFileDialog = new FolderBrowserDialog();//new OpenFileDialog();
            //openFileDialog.Filter = "OpenTibia Binary Map (*.otbm)|*.otbm";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return openFileDialog.SelectedPath;
            }
            else
            {
                return "";
            }
        }


        public static void JumpToBrush()
        {
            FindItemForm form = new FindItemForm();
            form.Text = "Jump to Brush";
            List<Brush> listBrush = new List<Brush>();
            foreach (TileSet tileSet in Materials.getInstance().getTileSetList())
            {

                TilesetCategory terrainTC = tileSet.getCategory2(TilesetCategoryType.TILESET_TERRAIN);
                if (terrainTC != null)
                {
                    listBrush.AddRange(terrainTC.brushlist);
                }
                TilesetCategory doodadTC = tileSet.getCategory2(TilesetCategoryType.TILESET_DOODAD);
                if (doodadTC != null)
                {
                    listBrush.AddRange(doodadTC.brushlist);
                }

                TilesetCategory itemTc = tileSet.getCategory2(TilesetCategoryType.TILESET_ITEM);
                if (itemTc != null)
                {
                    listBrush.AddRange(itemTc.brushlist);
                }

                TilesetCategory creatureTc = tileSet.getCategory2(TilesetCategoryType.TILESET_CREATURE);
                if (creatureTc != null)
                {
                    listBrush.AddRange(creatureTc.brushlist);
                }    
            }            
            form.setBrushList(listBrush);
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                Global.mapCanvas.SelectBrush(form.getSelectedBrush());
            }
        }

        public static void JumpToItem()
        {
            FindItemForm form = new FindItemForm();
            form.Text = "Jump to Item";
            List<Brush> listBrush = new List<Brush>();
            foreach (TileSet tileSet in Materials.getInstance().getTileSetList())
            {
                TilesetCategory rawTc = tileSet.getCategory2(TilesetCategoryType.TILESET_RAW);
                if (rawTc != null)
                {
                    listBrush.AddRange(rawTc.brushlist);
                }    
            }
            form.setBrushList(listBrush);
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                EditorPalette.SelectBrush(form.getSelectedBrush(), PaletteType.RAW);
            }                       
        }

        public static void AjustError(Exception ex, bool throw_ex = false, bool showmessage = true)
        {
            if (showmessage) MessageBox.Show("ERROR: " + "\n" + ex.Message + "\n\n\n" + "Stack:" + "\n\n" + ex.StackTrace);
            String [] lines = new string[3];
            lines[0] = ex.Message;
            lines[1] = "";
            lines[2] = ex.StackTrace;
            String fileName = Path.GetDirectoryName(Application.ExecutablePath) + "\\log_error_" + String.Format("{0:ddMMyyyyHHmmss}", DateTime.Now) + ".txt";
            File.WriteAllLines(@fileName, lines);
            if (throw_ex)
            {
                throw ex;
            }
        }

        public static String getMyIP()
        {
            return Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork).ToString();
        }

        public static String GetMapEditorVersion()
        {
            return Application.ProductVersion;
        }

        public static bool isFalseString(String str) {
	        if("false".Equals(str) || "0".Equals(str)  || "".Equals(str) || "no".Equals(str) ||  "not".Equals(str)) {
		        return true;
	        }
	        return false;
        }


        public static void Abort()
        {
            throw new AbortingException();
        }

        public static void DoException(Exception ex)
        {
            if (!ex.GetType().Equals(typeof(AbortingException)))
            {
                throw ex;
            }

        }

        public static bool isTrueString(String str) {
	        return !isFalseString(str);
        }


        public static List<Position> getTilesToBorder(Position pos)
        {
            List<Position> ret = new List<Position>();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Position newPos = new Position(pos.X + x, pos.Y + y, pos.z);
                    if (newPos != pos)
                    {
                        ret.Add(newPos);
                    }                    
                }
            }
            return ret;
        }

        public static List<Position> removeSamePosition(List<Position> positions)
        {
            List<Position> ret = new List<Position>();
            bool add = false;

            foreach (Position pos in positions)
            {
                foreach (Position other in positions)
                {
                    add = true;
                    if (!(pos.Equals(other)) && (pos == other))
                    {
                        add = false;
                        break;
                    }
                }

                if (add)
                {
                    ret.Add(pos);
                }
            }

            return ret;
        }

        public static List<Tile> removeSameTiles(List<Tile> tiles)
        {
            List<Tile> ret = new List<Tile>();
            bool add = false;

            foreach (Tile tile in tiles)
            {
                foreach (Tile other in tiles)
                {
                    add = true;
                    if (!(tile.Equals(other)) && (tile.Position == other.Position))
                    {
                        add = false;
                        break;
                    }
                }

                if (add)
                {
                    ret.Add(tile);
                }
            }

            return ret;
        }
    }

    public class AbortingException : Exception 
    {
        public AbortingException()
        {
        }        
    }
    
    public class Messages
    {
        public static String messageList = "";
        public static void AddWarning(String message)
        {
            messageList = messageList + message + "\n";             
        }
        
        public static void AddLogMessage(String message)
        {
            if (Program.devMode) Console.WriteLine(message);
        } 
    }

    public class Pair<T,Y>
    {
        public T first;
        public Y second;

        public Pair()
        {
            
        }

        public Pair(T a,Y b)
        {
            this.first = a;
            this.second = b;
        }
    }
}
