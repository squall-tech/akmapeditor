using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace AKMapEditor.OtMapEditor.OtBrush
{
    public class CarpetBrush : Brush
    {
        protected UInt16 look_id;
        protected CarpetNode[] carpet_items;
        protected String name;

        private static Random random = new Random();

        public class CarpetType
        {
            public ushort id;
            public int chance;
        }

        public class CarpetNode
        {
            public CarpetNode()
            {
                this.total_chance = 0;
            }
            public int total_chance;
            public List<CarpetType> items = new List<CarpetType>();
        }

        public CarpetBrush() : base()
        {
            carpet_items = new CarpetNode[14];
            for (int x = 0; x < 14; x++)
            {
                carpet_items[x] = new CarpetNode();
            }
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

        public override bool needBorders()
        {
            return true;
        }

        public override bool canDraw(GameMap map, Position pos)
        {
            return true;
        }

        public override void undraw(GameMap map, Tile tile)
        {
            List<Item> itemToRemove = new List<Item>();
            foreach (Item it in tile.Items)
            {
                if (it.Type.IsCarpet)
                {
                    CarpetBrush cb = it.getCarpetBrush();
                    if (cb == this)
                    {

                        itemToRemove.Add(it);
                    }                    
                }
            }
            foreach (Item it in itemToRemove)
            {
                tile.Items.Remove(it);
            }

        }
       
        public override void draw(GameMap map, Tile tile, object param = null)
        {
         	undraw(map, tile); // Remove old
	        tile.addItem(Item.Create(getRandomCarpet(BorderType.CARPET_CENTER)));   
        }


        public static void doCarpets(GameMap map, Tile tile)
        {

            if (tile.hasCarpet() == false)
            {
		            return;
            }

	        int x = tile.getPosition().x;
	        int y = tile.getPosition().y;
	        int z = tile.getPosition().z;
                            
            foreach(Item item in tile.Items)	        
	        {		        		     
		        CarpetBrush carpet_brush = item.getCarpetBrush();
		        if (carpet_brush == null)
		        {
			        continue;
		        }

		        bool[] neighbours = new bool[8];

		        if (x == 0)
		        {
			        if (y == 0)
			        {
				        neighbours[0] = false;
				        neighbours[1] = false;
				        neighbours[2] = false;
				        neighbours[3] = false;
				        neighbours[4] = hasMatchingCarpetBrushAtTile(map, carpet_brush, x + 1, y, z);
				        neighbours[5] = false;
				        neighbours[6] = hasMatchingCarpetBrushAtTile(map, carpet_brush, x, y + 1, z);
				        neighbours[7] = hasMatchingCarpetBrushAtTile(map, carpet_brush, x + 1, y + 1, z);
			        }
			        else
			        {
				        neighbours[0] = false;
				        neighbours[1] = hasMatchingCarpetBrushAtTile(map, carpet_brush, x, y - 1, z);
				        neighbours[2] = hasMatchingCarpetBrushAtTile(map, carpet_brush, x + 1, y - 1, z);
				        neighbours[3] = false;
				        neighbours[4] = hasMatchingCarpetBrushAtTile(map, carpet_brush, x + 1, y, z);
				        neighbours[5] = false;
				        neighbours[6] = hasMatchingCarpetBrushAtTile(map, carpet_brush, x, y + 1, z);
				        neighbours[7] = hasMatchingCarpetBrushAtTile(map, carpet_brush, x + 1, y + 1, z);
			        }
		        }
		        else if (y == 0)
		        {
			        neighbours[0] = false;
			        neighbours[1] = false;
			        neighbours[2] = false;
			        neighbours[3] = hasMatchingCarpetBrushAtTile(map, carpet_brush, x - 1, y, z);
			        neighbours[4] = hasMatchingCarpetBrushAtTile(map, carpet_brush, x + 1, y, z);
			        neighbours[5] = hasMatchingCarpetBrushAtTile(map, carpet_brush, x - 1, y + 1, z);
			        neighbours[6] = hasMatchingCarpetBrushAtTile(map, carpet_brush, x, y + 1, z);
			        neighbours[7] = hasMatchingCarpetBrushAtTile(map, carpet_brush, x + 1, y + 1, z);
		        }
		        else
		        {
			        neighbours[0] = hasMatchingCarpetBrushAtTile(map, carpet_brush, x - 1, y - 1, z);
			        neighbours[1] = hasMatchingCarpetBrushAtTile(map, carpet_brush, x, y - 1, z);
			        neighbours[2] = hasMatchingCarpetBrushAtTile(map, carpet_brush, x + 1, y - 1, z);
			        neighbours[3] = hasMatchingCarpetBrushAtTile(map, carpet_brush, x - 1, y, z);
			        neighbours[4] = hasMatchingCarpetBrushAtTile(map, carpet_brush, x + 1, y, z);
			        neighbours[5] = hasMatchingCarpetBrushAtTile(map, carpet_brush, x - 1, y + 1, z);
			        neighbours[6] = hasMatchingCarpetBrushAtTile(map, carpet_brush, x, y + 1, z);
			        neighbours[7] = hasMatchingCarpetBrushAtTile(map, carpet_brush, x + 1, y + 1, z);
		        }

		        uint tiledata = 0;
		        for (int i = 0; i < 8; i++)
		        {
			        if (neighbours[i])
			        {
				        // Same table as this one, calculate what border
                        tiledata |= Convert.ToUInt32(1) << i;
			        }
		        }
		        // bt is always valid.
                int id = carpet_brush.getRandomCarpet(Border_Types.carpet_types[tiledata]);
		        if (id != 0)
		        {
                    item.setID((ushort)id);
		        }
	        }
        }

        private static bool hasMatchingCarpetBrushAtTile(GameMap map, CarpetBrush carpet_brush, int x, int y, int z)
        {
            Tile t = map.getTile(x, y, z);
            if (t == null)
            {
                return false;
            }
            foreach(Item item in t.Items)            
            {
                CarpetBrush cb = item.getCarpetBrush();
                if (cb == carpet_brush)
                {
                    return true;
                }
            }

            return false;
        }

        public ushort getRandomCarpet(int alignment)
        {
            if (carpet_items[alignment].total_chance > 0)
            {
                int chance = random.Next(1, carpet_items[alignment].total_chance + 1);
                foreach (CarpetType carpet_iter in carpet_items[alignment].items)
                {
                    if (chance <= carpet_iter.chance)
                    {
                        return carpet_iter.id;
                    }
                    chance -= carpet_iter.chance;
                }
            }
            else
            {
                if (alignment != BorderType.CARPET_CENTER && carpet_items[BorderType.CARPET_CENTER].total_chance > 0)
                {
                    int chance = random.Next(1, carpet_items[BorderType.CARPET_CENTER].total_chance + 1);
                    foreach (CarpetType carpet_iter in carpet_items[alignment].items)
                    {
                        if (chance <= carpet_iter.chance)
                        {
                            return carpet_iter.id;
                        }
                        chance -= carpet_iter.chance;
                    }
                }
                // Find an item to place on the tile, first center, then the rest.
                for (int i = 0; i < 12; ++i)
                {
                    if (carpet_items[i].total_chance > 0)
                    {
                        int chance = random.Next(1, carpet_items[i].total_chance + 1);
                        foreach (CarpetType carpet_iter in carpet_items[alignment].items)
                        {
                            if (chance <= carpet_iter.chance)
                            {
                                return carpet_iter.id;
                            }
                            chance -= carpet_iter.chance;
                        }
                    }
                }
            }
            return 0;
        }

        public override void load(XElement node)
        {
            string strVal = "";
            look_id = node.Attribute("lookid").GetUInt16();
            if (node.Attribute("server_lookid").GetUInt16() != 0)
            {
                look_id = node.Attribute("server_lookid").GetUInt16();
            }

            foreach (XElement child_node in node.Elements())
            {
                if ("carpet".Equals(child_node.Name.LocalName))
                {
                    int alignment;
                    strVal = child_node.Attribute("align").GetString();
                    if (!"".Equals(strVal))
                    {
                        alignment = AutoBorder.EdgeNameToEdge(strVal);
                        if (alignment == BorderType.BORDER_NONE)
                        {
                            if ("center".Equals(strVal))
                            {
                                alignment = BorderType.CARPET_CENTER;
                            }
                            else
                            {
                                Messages.AddWarning("Invalid alignment of carpet node");
                                continue;
                            }
                        }
                    }
                    else
                    {
                        Messages.AddWarning("Could not read alignment tag of carpet node");
                        continue;
                    }
                    bool use_local_id = true;

                    foreach (XElement subchild_node in child_node.Elements())
                    {
                        if ("item".Equals(subchild_node.Name.LocalName))
                        {
                            use_local_id = false;

                            int id = subchild_node.Attribute("id").GetInt32();
                            int chance = subchild_node.Attribute("chance").GetInt32();

                            if (id == 0)
                            {
                                Messages.AddWarning("Could not read id tag of item node");
                                continue;
                            }

                            if (chance == 0)
                            {
                                Messages.AddWarning("Could not read chance tag of item node");
                                continue;
                            }
                            ItemType it = Global.items.items[id];
                            if (it == null)
                            {
                                Messages.AddWarning("There is no itemtype with id " + id);
                                continue;
                            }

                            if ((it.brush != null) && (it.brush != this))
                            {
                                Messages.AddWarning("itemId " + id + "already has a brush");
                                continue;
                            }
                            it.IsCarpet = true;
                            it.brush = this;

                            carpet_items[alignment].total_chance += chance;
                            CarpetType t = new CarpetType();
                            t.chance = chance;
                            t.id = (ushort) id;
                            carpet_items[alignment].items.Add(t);
                        }

                    }

                    if (use_local_id)
                    {
                        int id = child_node.Attribute("id").GetInt32();
                        if (id == 0)
                        {
                            Messages.AddWarning("Could not read id tag of item node");
                            continue;
                        }
                        ItemType it = Global.items.items[id];
                        if (it == null)
                        {
                            Messages.AddWarning("There is no itemtype with id " + id);
                            continue;
                        }
                        if ((it.brush != null) && (it.brush != this))
                        {
                            Messages.AddWarning("itemId " + id + "already has a brush");
                            continue;
                        }
                        it.IsCarpet = true;
                        it.brush = this;

                        carpet_items[alignment].total_chance += 1;
                        CarpetType t = new CarpetType();
                        t.chance = 1;
                        t.id = (ushort)id;
                        carpet_items[alignment].items.Add(t);

                    }
                }
            }
        }
    }
}
