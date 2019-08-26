using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace AKMapEditor.OtMapEditor.OtBrush
{
    public class DoodadBrush : Brush
    {
        protected UInt16 look_id;
        protected String name;

        protected int thickness;
        protected int thickness_ceiling;

        protected bool draggable;
        protected bool on_blocking;
        protected bool one_size;
        protected bool do_new_borders;
        protected bool on_duplicate;
        protected UInt16 clear_mapflags;
        protected UInt32 clear_statflags;
        protected List<AlternativeBlock> alternatives;

        private static Random random = new Random();


        #region Classes

        public class SingleBlock {
		    public int chance;
            public Item item;
	    };

	    public class CompositeBlock {
		    public int chance;
            public List<Tile> tiles;
            public CompositeBlock()
            {
                tiles = new List<Tile>();
            }
	    };

	    public class AlternativeBlock {
		    
		    public List<SingleBlock> single_items;
            public List<CompositeBlock> composite_items;

            public int composite_chance; // Total chance of a composite
            public int single_chance; // Total chance of a single object

            public bool ownsItem(UInt16 id)
            {
                foreach(SingleBlock sb in single_items)
                {
                    if (sb.item.Type.Id == id)
                    {
                        return true;
                    }
                }

                foreach (CompositeBlock cb in composite_items)
                {
                    List<Tile> tiles = cb.tiles;
                    foreach(Tile t in tiles)
                    {
                        if (t.Ground != null && t.Ground.Type.Id == id)
                        {
                            return true;
                        }
                        foreach (Item item in t.Items)
                        {
                            if (item.Type.Id == id)
                            {
                                return true;
                            }
                        }
                    }
                }
                return false;
            }

		    public AlternativeBlock()
            {
                single_items = new List<SingleBlock>();
                composite_items = new List<CompositeBlock>();
            }
	    };


        #endregion

        public DoodadBrush() : base()
        {
            alternatives = new List<AlternativeBlock>();
            one_size = false;
        }

        public override string getName()
        {
            return this.name;
        }

        public override void setName(string name)
        {
            this.name = name;
        }

        public override int getLookID()
        {
            return look_id;
        }

        public bool placeOnBlocking()
        {
            return on_blocking;
        }
        public bool placeOnDuplicate()
        {
            return on_duplicate;
        }
        public bool doNewBorders()
        {
            return do_new_borders;
        }

        public int getThickness()
        {
            return thickness;
        }
        public int getThicknessCeiling()
        {
            return thickness_ceiling;
        }

        public override int getMaxVariation()
        {
            return alternatives.Count();
        }

        public override bool oneSizeFitsAll()
        {
            return one_size;
        }

        public bool ownsItem(Item item)
        {
            return false;
        }

        public int getTotalChance(int ab)
        {
            if (alternatives.Count() <= 0)
            {
                return 0;
            }
            ab %= alternatives.Count();
            AlternativeBlock ab_ptr = alternatives[ab];            
            return ab_ptr.composite_chance + ab_ptr.single_chance;
        }

        public int getSingleChance(int ab) 
        {
            if (alternatives.Count() <= 0)
            {
                return 0;
            }
            ab %= alternatives.Count();
            AlternativeBlock ab_ptr = alternatives[ab];
            return ab_ptr.single_chance;
        }

        public int getCompositeChance(int ab)
        {
            if (alternatives.Count() <= 0)
            {
                return 0;
            }
            ab %= alternatives.Count();
            AlternativeBlock ab_ptr = alternatives[ab];
            return ab_ptr.composite_chance;
        }

        public bool hasSingleObjects(int ab)
        {
            if (alternatives.Count() <= 0)
            {
                return false;
            }
            ab %= alternatives.Count();
            AlternativeBlock ab_ptr = alternatives[ab];
            return ab_ptr.single_chance > 0;
        }
        public bool hasCompositeObjects(int ab)
        {
            if (alternatives.Count() <= 0)
            {
                return false;
            }
            ab %= alternatives.Count();
            AlternativeBlock ab_ptr = alternatives[ab];
            return ab_ptr.composite_chance > 0;
        }

        public List<Tile> getComposite(int variation)
        {
            List<Tile> empty = new List<Tile>();

            if (alternatives.Count() <= 0)
            {
                return empty;
            }
            variation %= alternatives.Count();
            AlternativeBlock ab_ptr = alternatives[variation];

            //if (ab_ptr.composite_chance < 1) ab_ptr.composite_chance = 1;
            int chance = ab_ptr.composite_chance;
            if (chance < 1) chance = 1;

            int roll = random.Next(1, chance + 1);
            foreach (CompositeBlock cb in ab_ptr.composite_items)
            {                
                if (roll <= cb.chance)
                {
                    return cb.tiles;
                }        
            }

            return empty;
        }

        public bool isEmpty(int variation)
        {
            if (hasCompositeObjects(variation))
            {
                return false;
            }
            if (hasSingleObjects(variation))
            {
                return false;
            }
            if (thickness <= 0)
            {
                return false;
            }
            return true;
        }

        public override void draw(GameMap map, Tile tile, object param = null)
        {
            int variation = 0;
            if (param != null)
            {
                variation = (int)param;
            }

            if (alternatives.Count() < 0)
                return;

            variation %= alternatives.Count();
            AlternativeBlock ab_ptr = alternatives[variation];
            

            int roll = random.Next(1, ab_ptr.single_chance + 1);
            foreach (SingleBlock sb in ab_ptr.single_items)            
            {                
                if (roll <= sb.chance)
                {
                    // Use this!
                    /*
                    tile.startRemove();
                    if  (Settings.GetBoolean(Key.RAW_LIKE_SIMONE) &&
                        (sb.item.Type.alwaysOnBottom && sb.item.Type.AlwaysOnTopOrder == 2))
                    {
                        foreach (Item item in tile.Items)
                        {
                            if (item.getTopOrder() == sb.item.Type.AlwaysOnTopOrder)
                            {
                                tile.AddItemToRemove(item);
                                break;
                            }
                        }
                    }*/
                    tile.RemoveItems();
                    tile.addItem(sb.item.deepCopy());
                    break;
                }
                roll -= sb.chance;
            }
            /* 
            if (clear_mapflags || clear_statflags)
            {
                tile.setMapFlags(tile.getMapFlags() & (~clear_mapflags));
                tile.setMapFlags(tile.getStatFlags() & (~clear_statflags));
            }   */          
        }

        private void loadAlternative(XElement node)
        {

            AlternativeBlock ab = null;
            foreach (XElement item_node in node.Elements("item"))
            {
                int chance;

                if ("".Equals(item_node.Attribute("chance").GetString()))
                {
                    Messages.AddWarning("Can't read chance tag of doodad item node.");
                    continue;
                }

                chance = item_node.Attribute("chance").GetInt32();

                Item item = Item.Create(Global.items.items[item_node.Attribute("id").GetInt32()]);
                if (item.Type == null)
                {
                    Messages.AddWarning("Can't read chance tag of doodad item node.");
                    continue;
                }
                ItemType it = item.Type;
                if (it.Id != 0)
                {
                    it.doodad_brush = this;
                }
                SingleBlock sb = new SingleBlock();
                sb.chance = chance;
                sb.item = item;
                if (ab == null) ab = new AlternativeBlock();                
                ab.single_items.Add(sb);
                ab.single_chance += chance;
            }
            foreach (XElement composite_node in node.Elements("composite"))
            {
                int chance;
                CompositeBlock cb = new CompositeBlock();
                if ("".Equals(composite_node.Attribute("chance").GetString()))
                {
                    Messages.AddWarning("Can't read chance tag of doodad item node.");
                    continue;
                }

                chance = composite_node.Attribute("chance").GetInt32();
                if (chance == 0)
                {
                    Messages.AddWarning("Can't read chance tag of doodad composite node.");
                    continue;
                }
                cb.chance = chance;
                if (ab == null) ab = new AlternativeBlock();                
                ab.composite_chance += cb.chance;
                cb.chance = ab.composite_chance;

                foreach (XElement tile_composite_node in composite_node.Elements("tile"))
                {
                    int x = 0, y = 0, z = 0;

                    x = tile_composite_node.Attribute("x").GetInt32();
                    y = tile_composite_node.Attribute("y").GetInt32();
                    z = tile_composite_node.Attribute("z").GetInt32();

                    /*

                    if (!readXMLValue(composite_child, "x", x))
                    {
                        wxString warning;
                        warning = wxT("Couldn't read positionX values of composite tile node.");
                        warnings.push_back(warning);
                        composite_child = composite_child->next;
                        continue;
                    }
                    if (!readXMLValue(composite_child, "y", y))
                    {
                        wxString warning;
                        warning = wxT("Couldn't read positionY values of composite tile node.");
                        warnings.push_back(warning);
                        composite_child = composite_child->next;
                        continue;
                    }
                    readXMLValue(composite_child, "z", z); // Don't halt on error

                    if (x <= -0x8000 || x >= +0x8000)
                    {
                        wxString warning;
                        warning = wxT("Invalid range of x value on composite tile node.");
                        warnings.push_back(warning);
                        composite_child = composite_child->next;
                        continue;
                    }
                    if (y <= -0x8000 || y >= +0x8000)
                    {
                        wxString warning;
                        warning = wxT("Invalid range of y value on composite tile node.");
                        warnings.push_back(warning);
                        composite_child = composite_child->next;
                        continue;
                    }
                    if (z <= -0x8 || z >= +0x8)
                    {
                        wxString warning;
                        warning = wxT("Invalid range of z value on composite tile node.");
                        warnings.push_back(warning);
                        composite_child = composite_child->next;
                        continue;
                    } */

                    Tile t = new Tile(x, y, z);
                    foreach (XElement item_tile_composite_node in tile_composite_node.Elements("item"))
                    {
                        Item item = Item.Create(Global.items.items[item_tile_composite_node.Attribute("id").GetInt16()]);
                        if (item.Type != null)
                        {
                            t.addItem(item);
                            ItemType it = item.Type;
                            if (it.Id != 0)
                            {
                                it.doodad_brush = this;
                            }

                        }                        
                    }
                    if (t.size() > 0)
                    {
                        cb.tiles.Add(t);
                    }
                }
                ab.composite_items.Add(cb);


            }
            if (ab != null)
            {
                alternatives.Add(ab);
            }
            
        }


        public override void load(XElement node)
        {            
            look_id = node.Attribute("lookid").GetUInt16();
            if (node.Attribute("server_lookid").GetUInt16() != 0)
            {
                look_id = node.Attribute("server_lookid").GetUInt16();
            }

            if (!"".Equals(node.Attribute("on_blocking").GetString()))
            {
                on_blocking =  Generic.isTrueString(node.Attribute("on_blocking").GetString());
            }

            if (!"".Equals(node.Attribute("on_duplicate").GetString()))
            {
                on_duplicate = Generic.isTrueString(node.Attribute("on_duplicate").GetString());
            }

            if (!"".Equals(node.Attribute("redo_borders").GetString()))
            {
                do_new_borders = Generic.isTrueString(node.Attribute("redo_borders").GetString());
            }

            if (!"".Equals(node.Attribute("reborder").GetString()))
            {
                do_new_borders = Generic.isTrueString(node.Attribute("reborder").GetString());
            }

            if (!"".Equals(node.Attribute("one_size").GetString()))
            {
                one_size = Generic.isTrueString(node.Attribute("one_size").GetString());
            }

            if (!"".Equals(node.Attribute("draggable").GetString()))
            {
                draggable = Generic.isTrueString(node.Attribute("draggable").GetString());
            }

            if (!"".Equals(node.Attribute("remove_optional_border").GetString()))
            {
                if (do_new_borders == false)
                {
                    Messages.AddWarning("remove_optional_border will not work without redo_borders");
                }
                clear_statflags |= TileState.TILESTATE_OP_BORDER;
            }
            if (!"".Equals(node.Attribute("thickness").GetString()))
            {
                int slash = node.Attribute("thickness").GetString().IndexOf("/");
                if (slash != -1)
                {
                    thickness = Convert.ToInt32(node.Attribute("thickness").GetString().Substring(0, (int) Math.Max(0ul, (float) slash)   )  );
                }
            }

            foreach (XElement node_alternative in node.Elements("alternate"))
            {
                loadAlternative(node_alternative);
            }

            loadAlternative(node);
        }


        public static void FillDoodadPreviewBuffer()
        {            
            DoodadBrush brush =  EditorPalette.getSelectedPalette().GetSelectedBrush() as DoodadBrush;
            if (brush == null) return;
            if (Global.mapCanvas.secondary_map != null) return;
            

            GameMap doodad_buffer_map = Global.mapCanvas.doodad_buffer_map;
            Global.mapCanvas.secondary_map = doodad_buffer_map;

            doodad_buffer_map.clear();


            if (brush.isEmpty(EditorPalette.getSelectedPalette().GetBrushVariation()))
            {
                return;
            } 

            int object_count = 0;
            int area;
            if (EditorPalette.GetBrushShape() == BrushShape.BRUSHSHAPE_SQUARE)
            {
                area = 2 * EditorPalette.GetBrushSize();
                area = area * area + 1;
            }
            else
            {
                if (EditorPalette.GetBrushSize() == 1)
                {
                    // There is a huge deviation here with the other formula.
                    area = 5;
                }
                else
                {
                    area = (int)(0.5 + EditorPalette.GetBrushSize() * EditorPalette.GetBrushSize() * Math.PI);
                }
            }
            EditorPalette ed = EditorPalette.getSelectedPalette();

            int object_range = (ed.use_custom_thickness ? (int)(area * ed.custom_thickness_mod) : brush.getThickness() * area /  Math.Max (1, brush.getThicknessCeiling()));
            int final_object_count = Math.Max(1, object_range + random.Next(object_range + 1));

            Position center_pos = new Position(0x8000, 0x8000, 0x8);

            int brush_size = EditorPalette.GetBrushSize();

            if (EditorPalette.GetBrushSize() > 0 && brush.oneSizeFitsAll() == false)
            {
                while (object_count < final_object_count)
                {
                    int retries = 0;
                    bool exit = false;

                    while (retries < 5 && exit == false) // Try to place objects 5 times
                    {
                        int pos_retries = 0;
                        int xpos = 0;
                        int ypos = 0;
                        bool found_pos = false;
                        if (EditorPalette.GetBrushShape() == BrushShape.BRUSHSHAPE_CIRCLE)
                        {
                            while (pos_retries < 5 && found_pos == false)
                            {
                                xpos = random.Next(-brush_size, brush_size + 1);
                                ypos = random.Next(-brush_size, brush_size + 1);
                                double distance = Math.Sqrt((double)(xpos * xpos) + (double)(ypos * ypos));
                                if (distance < EditorPalette.GetBrushSize() + 0.005)
                                {
                                    found_pos = true;
                                }
                            }
                        }
                        else
                        {
                            found_pos = true;
                            xpos = random.Next(-brush_size, brush_size + 1);
                            ypos = random.Next(-brush_size, brush_size + 1);  
                        }
                        if (found_pos == false)
                        {
                            ++retries;
                            continue;
                        }
                        // Decide whether the zone should have a composite or several single objects.

                        bool fail = false;
                        if (random.Next(brush.getTotalChance(ed.GetBrushVariation()) + 1) <= brush.getCompositeChance(ed.GetBrushVariation()))
                        {
                            // Composite
                            List<Tile> tiles = brush.getComposite(ed.GetBrushVariation());

                            // Figure out if the placement is valid
                            foreach (Tile buffer_tile in tiles)
                            {
                                Position pos = center_pos + buffer_tile.getPosition() + new Position(xpos, ypos, 0);
                                Tile tile = doodad_buffer_map.getTile(pos);
                                if (tile != null)
                                {
                                    if (tile.size() > 0)
                                    {
                                        fail = true;
                                        break;
                                    }
                                }
                            }
                            if (fail)
                            {
                                ++retries;
                                break;
                            }

                            // Transfer items to the stack
                            foreach (Tile buffer_tile in tiles)
                            {
                                Position pos = center_pos + buffer_tile.getPosition() + new Position(xpos, ypos, 0);
                                Tile tile;
                                if ((tile = doodad_buffer_map.getTile(pos)) == null)
                                {
                                    tile = new Tile(pos);
                                }
                                if (buffer_tile.Ground != null)
                                {
                                    tile.addItem(buffer_tile.Ground.deepCopy());
                                }
                                foreach (Item item_iter in buffer_tile.Items)
                                {
                                    tile.addItem(item_iter.deepCopy());
                                }
                                doodad_buffer_map.setTile(tile.getPosition(), tile);
                            }
                            exit = true;
                        }
                        else if (brush.hasSingleObjects(ed.GetBrushVariation()))
                        {
                            Position pos = center_pos + new Position(xpos, ypos, 0);
                            Tile tile = doodad_buffer_map.getTile(pos);
                            if (tile != null)
                            {
                                if (tile.size() > 0)
                                {
                                    fail = true;
                                    break;
                                }
                            }
                            else
                            {
                                tile = new Tile(pos);
                            }
                            int variation = ed.GetBrushVariation();
                            brush.draw(doodad_buffer_map, tile, variation);
                            //std::cout << "\tpos: " << tile->getPosition() << std::endl;
                            doodad_buffer_map.setTile(tile.getPosition(), tile);
                            exit = true;
                        }
                        if (fail)
                        {
                            ++retries;
                            break;
                        }
                    }
                    ++object_count;
                }
            }
            else
            {
                if (brush.hasCompositeObjects(ed.GetBrushVariation()) && random.Next(brush.getTotalChance(ed.GetBrushVariation()) + 1) <= brush.getCompositeChance(ed.GetBrushVariation()))
                {
                    // Composite
                    List<Tile> tiles = brush.getComposite(ed.GetBrushVariation());

                    // All placement is valid...

                    // Transfer items to the buffer                    
                    foreach(Tile buffer_tile in tiles)
                    {                        
                        Position pos = center_pos + buffer_tile.getPosition();
                        Tile tile = new Tile(pos);
                        //std::cout << pos << " = " << center_pos << " + " << buffer_tile->getPosition() << std::endl;

                        if (buffer_tile.Ground != null)
                        {
                            tile.addItem(buffer_tile.Ground.deepCopy());
                        }
                        foreach(Item item_iter in buffer_tile.Items)                        
                        {
                            tile.addItem(item_iter.deepCopy());
                        }
                        doodad_buffer_map.setTile(tile.getPosition(), tile);
                    }
                }
                else if (brush.hasSingleObjects(ed.GetBrushVariation()))
                {
                    Tile tile = new Tile(center_pos);
                    int variation = ed.GetBrushVariation();
                    brush.draw(doodad_buffer_map, tile, variation);
                    doodad_buffer_map.setTile(center_pos, tile);
                }
            }

        }
    }
}
