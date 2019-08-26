using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace AKMapEditor.OtMapEditor.OtBrush
{
    public class Materials
    {
        private static Materials materials;
        public static Dictionary<String, TileSet> tilesets;

        public static Materials getInstance()
        {
            if (materials == null)
            {
                materials = new Materials();
            }
            return materials;
        }

        public Materials()
        {
            tilesets = new Dictionary<string, TileSet>();
        }

        public List<TileSet> getTileSetList()
        {
            List<TileSet> list = new List<TileSet>();

            foreach (TileSet ts in tilesets.Values)
            {
                list.Add(ts);
            }
            list = list.OrderBy(x => x.name).ToList();
            return list;
        }
        public void createOtherTileset()
        {
            TileSet others = null;
            TileSet npc_tileset = null;

            if (tilesets.ContainsKey("Others"))
            {
                others = tilesets["Others"];                
            }
            else
            {
                others = new TileSet(Brushes.getInstance(), "Others");
                tilesets["Others"] = others;
            }

            if  (tilesets.ContainsKey("NPCs"))
            {
                npc_tileset = tilesets["NPCs"];
                npc_tileset.clear();
            }
            else
            {
                npc_tileset = new TileSet(Brushes.getInstance(), "NPCs");
                tilesets["NPCs"] = npc_tileset;
            }

            foreach (CreatureType type in CreatureDatabase.creatureDatabase)
            {
                if (type.in_other_tileset)
                {
                    if (type.isNpc)
                    {                        
                        npc_tileset.getCategory(TilesetCategoryType.TILESET_CREATURE).brushlist.Add(type.brush);
                    }
                    else
                    {
                        others.getCategory(TilesetCategoryType.TILESET_CREATURE).brushlist.Add(type.brush);
                    }
                }
                else if (type.brush == null)
                {
                    type.brush = new CreatureBrush(type);
                    Brushes.getInstance().addBrush(type.brush);
                    type.brush.flagAsVisible();
                    type.in_other_tileset = true;
                    if (type.isNpc)
                    {                        
                        npc_tileset.getCategory(TilesetCategoryType.TILESET_CREATURE).brushlist.Add(type.brush);
                    }
                    else
                    {
                        others.getCategory(TilesetCategoryType.TILESET_CREATURE).brushlist.Add(type.brush);
                    }
                }
            }
        }

        public void loadExtensions(String directoryName)
        {
            DirectoryInfo diretorio = new DirectoryInfo(directoryName);
            
            FileInfo[] Arquivos = diretorio.GetFiles("*.xml");

            //Começamos a listar os arquivos
            foreach (FileInfo fileinfo in Arquivos)
            {
                XElement doc = XElement.Load(fileinfo.FullName);
                loadMaterials(doc, fileinfo.FullName);              
            }            
        }

        public void loadMaterials(XElement doc, String dataPath)
        {
            try
            {
                foreach (var xml_node in doc.Elements("include"))
                {
                    loadMaterials(XElement.Load(dataPath + "\\" + xml_node.Attribute("file").GetString()), dataPath);
                }

                foreach (var metaitem_node in doc.Elements("metaitem"))
                {
                    //TODO
                }

                foreach (var border_node in doc.Elements("border"))
                {
                    Brushes.getInstance().unserializeBorder(border_node);
                }

                foreach (var brush_node in doc.Elements("brush"))
                {
                    Brushes.getInstance().unserializeBrush(brush_node);
                }

                foreach (var tileset_node in doc.Elements("tileset"))
                {
                    unserializeTileset(tileset_node);
                }            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + dataPath + "\n" + ex.StackTrace);
            }            
        }

        public TileSet getTileSet(String name)
        {
            TileSet ts = null;
            tilesets.TryGetValue(name, out ts);
            return ts;
        }

        private void unserializeTileset(XElement tileset_node)
        {
            String tileSetName = tileset_node.Attribute("name").GetString();
            if (!"".Equals(tileSetName))
            {
                TileSet ts = getTileSet(tileSetName);
                if (ts == null)
                {
                    ts = new TileSet(Brushes.getInstance(), tileSetName);
                    tilesets.Add(tileSetName, ts);
                }

                foreach (XElement element_tileset_node in tileset_node.Elements())
                {
                    ts.LoadCategory(element_tileset_node);
                }                
            }            
            //
        }
    }
}
