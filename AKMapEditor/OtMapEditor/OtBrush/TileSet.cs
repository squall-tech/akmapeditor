using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;

namespace AKMapEditor.OtMapEditor.OtBrush
{
    public enum TilesetCategoryType
    {
        TILESET_UNKNOWN,
        TILESET_TERRAIN,
        TILESET_CREATURE,
        TILESET_DOODAD,
        TILESET_ITEM,
        TILESET_RAW,
        TILESET_HOUSE,
        TILESET_WAYPOINT,
    };


    public class TileSet
    {
        public List<TilesetCategory> categories;
        public String name;
        public Brushes brushes;

        public TileSet(Brushes brushes, String name)
        {
            this.name = name;
            this.brushes = brushes;
            this.categories = new List<TilesetCategory>();
        }

        public override string ToString()
        {
            return name;
        }

        public void LoadCategory(XElement element_node )
        {
            TilesetCategory category = null;
            TilesetCategory category2 = null;

            switch (element_node.Name.ToString())
            {
                case "terrain":
                    category = getCategory(TilesetCategoryType.TILESET_TERRAIN);
                    break;
                case "doodad":
                    category = getCategory(TilesetCategoryType.TILESET_DOODAD);
                    break;
                case "items":
                    category = getCategory(TilesetCategoryType.TILESET_ITEM);
                    break;
                case "raw":
                    category = getCategory(TilesetCategoryType.TILESET_RAW);
                    break;
                case "terrain_and_raw":
                    category = getCategory(TilesetCategoryType.TILESET_TERRAIN);
                    category2 = getCategory(TilesetCategoryType.TILESET_RAW);
                    break;
                case "doodad_and_raw":
                    category = getCategory(TilesetCategoryType.TILESET_DOODAD);
                    category2 = getCategory(TilesetCategoryType.TILESET_RAW);
                    break;
                case "items_and_raw":
                    category = getCategory(TilesetCategoryType.TILESET_ITEM);
                    category2 = getCategory(TilesetCategoryType.TILESET_RAW);
                    break;
                case "creatures":
                    // ainda não
                    break;
            }
            if (category == null) return;

            foreach (XElement brush_element_node in element_node.Elements())
            {
                category.loadBrush(brush_element_node);
                if (category2 != null) 
                    category2.loadBrush(brush_element_node);
            }            
        }

        public void clear()
        {
            foreach (TilesetCategory category in categories)
            {
                category.brushlist.Clear();
            }
        }

        public TilesetCategory getCategory(TilesetCategoryType type)
        {            
            foreach (TilesetCategory it in categories)
            {
                if (it.getType().Equals(type))
                {
                    return it;
                }
            }
            TilesetCategory tsc = new TilesetCategory(this,type);
            categories.Add(tsc);
            return tsc;
        }

        public TilesetCategory getCategory2(TilesetCategoryType type)
        {            
            foreach (TilesetCategory it in categories)
            {
                if (it.getType().Equals(type))
                {
                    return it;
                }
            }
            return null;
        }
    }

    public class TilesetCategory
    {
        protected TilesetCategoryType type;
        public List<Brush> brushlist;
        public TileSet tileset;

        public TilesetCategory(TileSet parent, TilesetCategoryType type)
        {
            this.brushlist = new List<Brush>();
            this.tileset = parent;
            this.type = type;
        }

        public void loadBrush(XElement brushElement)
        {
            String brush_name = brushElement.Attribute("after").GetString();
            String strVal = "";
            int intVal = brushElement.Attribute("afteritem").GetInt32();
            if (intVal != 0)
            {
                ItemType it = Global.items.items[intVal];
                if (it != null)
                {
                    brush_name = (it.raw_brush != null ? it.raw_brush.getName() : "");
                }
            }


            if ("brush".Equals(brushElement.Name.ToString()))
            {
                strVal = brushElement.Attribute("name").GetString();
                Brush brush = tileset.brushes.getBrush(strVal);

                if (brush != null)
                {

                    if (!brush.inDoodadPalette)
                    {
                        brush.inDoodadPalette = (type == TilesetCategoryType.TILESET_DOODAD);
                    }

                    if (!brush.inRawPalette)
                    {
                        brush.inRawPalette = (type == TilesetCategoryType.TILESET_RAW);
                    }

                    if (!brush.inTerrainPalette)
                    {
                        brush.inTerrainPalette = (type == TilesetCategoryType.TILESET_TERRAIN);
                    }

                    if (!brush.inItemPalette)
                    {
                        brush.inItemPalette = (type == TilesetCategoryType.TILESET_ITEM);
                    }

                    int index = brushlist.Count();
                    if (!"".Equals(brush_name))
                    {
                        int i = 0;
                        foreach (Brush ite in brushlist)
                        {
                            if (ite.getName().Equals(brush_name))
                            {
                                index = i;
                                break;
                            }
                            i++;
                        }
                    }                    
                    brush.flagAsVisible();                    
                    brushlist.Insert(index, brush);
                }
            }
            else if ("item".Equals(brushElement.Name.ToString()))
            {
                int fromid = 0, toid = 0;
                fromid = brushElement.Attribute("id").GetInt32();
                if (fromid == 0)
                {
                    fromid = brushElement.Attribute("fromid").GetInt32();
                    toid = brushElement.Attribute("toid").GetInt32();
                }

                toid = Math.Max(toid, fromid);

                int index = brushlist.Count();
                if (!"".Equals(brush_name))
                {
                    int i = 0;
                    foreach (Brush ite in brushlist)
                    {
                        if (ite.getName().Equals(brush_name))
                        {
                            index = i;
                            break;
                        }
                        i++;
                    }
                }
                List<Brush> temp_vec = new List<Brush>();
                for (int id = fromid; id <= toid; ++id)
                {
                    ItemType it = Global.items.items[id];
                    if (it == null)
                    {
                        throw new Exception("Unknown item id " + id);
                    }
                    else
                    {
                        Brush brush = null;
                        if (it.raw_brush != null)
                        {
                            brush = it.raw_brush;
                        }
                        else
                        {
                            brush = new RAWBrush(it.Id);
                            it.raw_brush = brush;
                            it.has_raw = true;
                            tileset.brushes.addBrush(brush);
                        }
                        if ((it.doodad_brush == null) && (!isTrivial()))
                        {
                        //    it.doodad_brush = brush;
                        }

                        if (!brush.inDoodadPalette)
                        {
                            brush.inDoodadPalette = (type == TilesetCategoryType.TILESET_DOODAD);
                        }

                        if (!brush.inRawPalette)
                        {
                            brush.inRawPalette = (type == TilesetCategoryType.TILESET_RAW);
                        }

                        if (!brush.inTerrainPalette)
                        {
                            brush.inTerrainPalette = (type == TilesetCategoryType.TILESET_TERRAIN);
                        }

                        if (!brush.inItemPalette)
                        {
                            brush.inItemPalette = (type == TilesetCategoryType.TILESET_ITEM);
                        }

                        brush.flagAsVisible();                        
                        temp_vec.Add(brush);
                    }
                }
                brushlist.InsertRange(index, temp_vec);
            }
        }        

        public TilesetCategoryType getType()
        {
            return type;
        }

        public bool isTrivial()
        {
            if (type == TilesetCategoryType.TILESET_ITEM)
            {
                return true;
            }
            else if (type == TilesetCategoryType.TILESET_ITEM)
            {
                return true;
            }
            return false;
        }
    }
}
