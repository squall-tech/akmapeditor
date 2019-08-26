using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Text;
using System.Threading;

namespace AKMapEditor.OtMapEditor.OtBrush
{
    public class GroundBrush : TerrainBrush
    {
        protected int z_order;
        protected bool has_zilch_outer_border;
        protected bool has_zilch_inner_border;
        protected bool has_outer_border;
        protected bool has_inner_border;
        protected AutoBorder optional_border;
        protected bool use_only_optional;
        protected bool randomize;
        protected List<ItemChanceBlock> border_items;
        protected List<BorderBlock> borders;
        public int total_chance;

        private static List<BorderBlock> specific_list = new List<BorderBlock>();

        public GroundBrush() : base()
        {
            border_items = new List<ItemChanceBlock>();
            borders = new List<BorderBlock>();
            total_chance = 0;            
        }

        public struct ItemChanceBlock
        {
            public int chance;
            public UInt16 id;
        };

        public class SpecificCaseBlock {
		    public List<UInt16> items_to_match;
            public UInt32 match_group;
            public int group_match_alignment;
            public UInt16 to_replace_id;
            public UInt16 with_id;
            public bool delete_all;

            public SpecificCaseBlock()
            {
                match_group = 0;
                group_match_alignment = BorderType.BORDER_NONE;
                to_replace_id = 0;
                with_id = 0;
                delete_all = false;
                items_to_match = new List<ushort>();
            }
	};

        public class BorderBlock
        {
            public bool outer;
            public bool super;
            public UInt32 to;

            public AutoBorder autoborder;
            public List<SpecificCaseBlock> specific_cases;

            public BorderBlock()
            {
                specific_cases = new List<SpecificCaseBlock>();
            }
        };

        public class BorderCluster
        {
            public UInt32 alignment;
            public Int32 z;
            public AutoBorder border;

            public static bool operator <(BorderCluster este, BorderCluster other)
            {
                return other.z > este.z;
            }
            public static bool operator >(BorderCluster este, BorderCluster other)
            {
                return other.z > este.z;
            }
        };


        public override void undraw(GameMap map, Tile tile)
        {
            if (tile.hasGround() && tile.Ground.getGroundBrush() == this)
            {
                tile.Ground = null;
            }
        }

        private Random random = new Random();

        public override void draw(GameMap map, Tile tile, object param)
        {
            if (param !=null)
            {
                Pair<bool, GroundBrush> ret = (Pair<bool, GroundBrush>)param;
                GroundBrush other = tile.getGroundBrush();

                if (ret.first)
                {
                    if (other != null) return;
                } else if ((other != null) && (ret.second.id != other.id))
                {
                    return;
                }
                
            }
            if (total_chance < 1)
            {
                total_chance = 1;
            }
            int chance = random.Next(1, total_chance + 1);
            UInt16 id = 0;
            foreach (ItemChanceBlock it in border_items)
            {
                if (chance < it.chance)
                {
                    id = it.id;
                    break;
                }
            }
            if ((id == 0) && (border_items.Count > 0))
            {
                id = border_items.First().id;
            }
            if (id == 0)
            {
                id = look_id;
            }
            if (id != 0)
            {
                tile.addItem(Item.Create(Global.items.items[id]));    
            }   
          
        }

        public static BorderBlock getBrushTo(GroundBrush first, GroundBrush second)
        {
            if (first != null)
            {
                if (second != null)
                {
                    //Console.WriteLine("Aqui ninguem é nulo");                    
                    if (first.getZ() < second.getZ() && second.hasOuterBorder())
                    {
                        //Console.WriteLine("getBrushTo 01");
                        if (first.hasInnerBorder())
                        {
                            //Console.WriteLine("getBrushTo 02");
                            foreach (BorderBlock bb in first.borders)
                            {
                                //Console.WriteLine("getBrushTo 03");
                                if (bb.outer == true)
                                {
                                    continue;
                                }
                                else if (bb.to == second.getID() || bb.to == 0xFFFFFFFF)
                                {
                                    return bb;
                                }
                            }
                        }
                        //Console.WriteLine("getBrushTo 04");
                        foreach (BorderBlock bb in second.borders)
                        {
                            //Console.WriteLine("getBrushTo 05");
                            if (!bb.outer)
                            {
                                continue;
                            }
                            else if (bb.to == first.getID())
                            {
                                //Console.WriteLine("getBrushTo 05 1");
                                return bb;
                            }
                            else if (bb.to == 0xFFFFFFFF)
                            {
                                //Console.WriteLine("getBrushTo 05 2");
                                return bb;
                            }
                        }
                    }            
                    else if (first.hasInnerBorder())
                    {
                        //Console.WriteLine("getBrushTo 06");
                        foreach (BorderBlock bb in first.borders)
                        {
                            //Console.WriteLine("getBrushTo 07");
                            if (bb.outer == true)
                            {
                                continue;
                            }
                            else if (bb.to == second.getID())
                            {
                                return bb;
                            }
                            else if (bb.to == 0xFFFFFFFF)
                            {
                                return bb;
                            }
                        }
                    }
                }
                else if (first.hasInnerZilchBorder())
                {
                    {
                        foreach (BorderBlock bb in first.borders)
                        {
                            if (bb.outer == true)
                            {
                                continue;
                            }
                            else if (bb.to == 0)
                            {
                                return bb;
                            }
                        }
                    }
                }
            }
            else if (second != null && second.hasOuterZilchBorder())
            {
                foreach (BorderBlock bb in second.borders)
                {
                    if (bb.outer == false)
                    {
                        continue;
                    }
                    else if (bb.to == 0)
                    {                      
                        return bb;
                    }
                }
            }
            return null;
        }
        

        public static GroundBrush extractGroundBrushFromTile(GameMap map, int x, int y, int z)
        {
            Tile t = map.getTile(x, y, z);
            if (t != null)
            {
                return t.getGroundBrush();
            }
            else
            {
                return null;
            }
            
        }

        public static void doBorders(GameMap map, Tile tile)
        {
            //Console.WriteLine("------------------------------");
            GroundBrush border_brush = tile.getGroundBrush();

           	int x = tile.Position.X;
            int y = tile.Position.Y;
            int z = tile.Position.Z;

            Pair<Boolean, GroundBrush>[] neighbours = new Pair<bool,GroundBrush>[8];            

            if(x == 0) 
            {
		        if(y == 0) 
                {
			        neighbours[0] = new Pair<bool,GroundBrush>(false,null);
			        neighbours[1] = new Pair<bool,GroundBrush>(false,null);
			        neighbours[2] = new Pair<bool,GroundBrush>(false,null);
			        neighbours[3] = new Pair<bool,GroundBrush>(false,null);
			        neighbours[4] = new Pair<bool,GroundBrush>(false,extractGroundBrushFromTile(map, x + 1, y, z));
			        neighbours[5] = new Pair<bool,GroundBrush>(false,null);
			        neighbours[6] = new Pair<bool,GroundBrush>(false,extractGroundBrushFromTile(map, x,     y + 1, z));
			        neighbours[7] = new Pair<bool,GroundBrush>(false,extractGroundBrushFromTile(map, x + 1, y + 1, z));
		        }
                else 
                {
			        neighbours[0] = new Pair<bool,GroundBrush>(false,null);
			        neighbours[1] = new Pair<bool,GroundBrush>(false,extractGroundBrushFromTile(map, x,     y - 1, z));
			        neighbours[2] = new Pair<bool,GroundBrush>(false,extractGroundBrushFromTile(map, x + 1, y - 1, z));
			        neighbours[3] = new Pair<bool,GroundBrush>(false,null);
			        neighbours[4] = new Pair<bool,GroundBrush>(false,extractGroundBrushFromTile(map, x + 1, y, z));
			        neighbours[5] = new Pair<bool,GroundBrush>(false,null);
			        neighbours[6] = new Pair<bool,GroundBrush>(false,extractGroundBrushFromTile(map, x,     y + 1, z));
			        neighbours[7] = new Pair<bool,GroundBrush>(false,extractGroundBrushFromTile(map, x + 1, y + 1, z));
		        }
	        } 
            else if(y == 0) 
            {
		        neighbours[0] = new Pair<bool,GroundBrush>(false,null);
		        neighbours[1] = new Pair<bool,GroundBrush>(false,null);
		        neighbours[2] = new Pair<bool,GroundBrush>(false,null);
		        neighbours[3] = new Pair<bool,GroundBrush>(false,extractGroundBrushFromTile(map, x - 1, y, z));
		        neighbours[4] = new Pair<bool,GroundBrush>(false,extractGroundBrushFromTile(map, x + 1, y, z));
		        neighbours[5] = new Pair<bool,GroundBrush>(false,extractGroundBrushFromTile(map, x - 1, y + 1, z));
		        neighbours[6] = new Pair<bool,GroundBrush>(false,extractGroundBrushFromTile(map, x,     y + 1, z));
		        neighbours[7] = new Pair<bool,GroundBrush>(false,extractGroundBrushFromTile(map, x + 1, y + 1, z));
	        } 
            else 
            {
		        neighbours[0] = new Pair<bool,GroundBrush>(false,extractGroundBrushFromTile(map, x - 1, y - 1, z));
		        neighbours[1] = new Pair<bool,GroundBrush>(false,extractGroundBrushFromTile(map, x,     y - 1, z));
		        neighbours[2] = new Pair<bool,GroundBrush>(false,extractGroundBrushFromTile(map, x + 1, y - 1, z));
		        neighbours[3] = new Pair<bool,GroundBrush>(false,extractGroundBrushFromTile(map, x - 1, y, z));
		        neighbours[4] = new Pair<bool,GroundBrush>(false,extractGroundBrushFromTile(map, x + 1, y, z));
		        neighbours[5] = new Pair<bool,GroundBrush>(false,extractGroundBrushFromTile(map, x - 1, y + 1, z));
		        neighbours[6] = new Pair<bool,GroundBrush>(false,extractGroundBrushFromTile(map, x,     y + 1, z));
		        neighbours[7] = new Pair<bool,GroundBrush>(false,extractGroundBrushFromTile(map, x + 1, y + 1, z));
	        }

            List<BorderCluster> border_list = new List<BorderCluster>();
            specific_list.Clear();

            for (int i = 0; i < 8; i++)
            {
                if (neighbours[i].first) continue;
                GroundBrush other = neighbours[i].second;
                if (border_brush != null)
                {                    
                    if (other != null)
                    {
                        if (other.getID() == border_brush.getID())
                        {
                            continue;
                        }
                        if (other.hasOuterBorder() || border_brush.hasInnerBorder())
                        {
                            bool only_mountain = false;
                            if ((other.friendOf(border_brush) || border_brush.friendOf(other)))
                            {
                                if (other.hasOptionalBorder())
                                {
                                    only_mountain = true;
                                }
                                else
                                {
                                    continue;
                                }
                            }

                            UInt32 tiledata = 0;
                            for (int j = i; j < 8; j++)
                            {                                
                                if (!neighbours[j].first && (neighbours[j].second != null) && neighbours[j].second.getID() == other.getID())
                                {
                                    neighbours[j].first = true;
                                    tiledata |= Convert.ToUInt32(1) << j;
                                }
                            }

                            if (tiledata != 0)
                            {
                                if (other.hasOptionalBorder() && tile.hasOptionalBorder())
                                {
                                    BorderCluster cluster = new BorderCluster();
                                    cluster.alignment = tiledata;
                                    cluster.z = 0x7FFFFFFF; // Above all other borders
                                    //cluster.z = 0;

                                    cluster.border = other.optional_border;
                                    //Console.WriteLine("Aqui 01");
                                    border_list.Add(cluster);
                                    if (other.useSoloOptionalBorder())
                                    {
                                        only_mountain = true;
                                    }
                                }
                                if (!only_mountain)
                                {
                                    BorderBlock bb = getBrushTo(border_brush, other);
                                    if (bb != null)
                                    {
                                        bool found = false;

                                        foreach (BorderCluster bc in border_list)
                                        {
                                            if (bc.border.Equals(bb.autoborder))
                                            {

                                                bc.alignment |= tiledata;
                                                if (bc.z < other.getZ())
                                                {
                                                    bc.z = other.getZ();
                                                }
                                                if (bb.specific_cases.Count() > 0)
                                                {
                                                    if (!specific_list.Contains(bb))
                                                    {
                                                        specific_list.Add(bb);
                                                    }
                                                }
                                                found = true;
                                                break;
                                            }
                                        }
                                        if (!found)
                                        {
                                            BorderCluster bc = new BorderCluster();
                                            bc.alignment = tiledata;
                                            bc.z = other.getZ();
                                            bc.border = bb.autoborder;
                                            //Console.WriteLine("Aqui 02");
                                            border_list.Add(bc);
                                            if (bb.specific_cases.Count() > 0)
                                            {
                                                if (!specific_list.Contains(bb))
                                                {
                                                    specific_list.Add(bb);
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                    else if (border_brush.hasInnerZilchBorder())
                    {
                        UInt32 tiledata = 0;
                        for (int j = i; j < 8; j++)
                        {
                            if (neighbours[j].first == false && neighbours[j].second == null)
                            {
                                neighbours[j].first = true;
                                tiledata |= Convert.ToUInt32(1) << j;
                            }
                        }
                        if (tiledata != 0)
                        {
                            BorderCluster cluster = new BorderCluster();
                            cluster.alignment = tiledata;
                            cluster.z = 5000;
                            BorderBlock bb = getBrushTo(border_brush, null);
                            if (bb == null)
                            {
                                continue;
                            }
                            cluster.border = bb.autoborder;
                            if (cluster.border != null)
                            {
                                //Console.WriteLine("Aqui 03");
                                border_list.Add(cluster);
                            }
                            if (bb.specific_cases.Count() > 0)
                            {
                                if (!specific_list.Contains(bb))
                                {
                                    specific_list.Add(bb);
                                }
                            }
                        }
                        continue;
                    }
                }
                else if (other != null && other.hasOuterZilchBorder())
                {
                    UInt32 tiledata = 0;
                    for (int j = i; j < 8; j++)
                    {
                        if (neighbours[j].first == false && (neighbours[j].second != null) && neighbours[j].second.getID() == other.getID())
                        {                            
                            neighbours[j].first = true;
                            tiledata |= Convert.ToUInt32(1) << j;
                        } 
                    }
                    if(tiledata != 0) {
				        BorderCluster cluster = new BorderCluster();
				        cluster.alignment = tiledata;
				        cluster.z = other.getZ();
				        BorderBlock bb = getBrushTo(null, other);
				        if(bb != null) {
					        cluster.border = bb.autoborder;
					        if(cluster.border != null) {
                                //Console.WriteLine("where 04 - " + i);
						        border_list.Add(cluster);
					        }
                            if (bb.specific_cases.Count() > 0)
                            {
                                if (!specific_list.Contains(bb))
                                {
                                    specific_list.Add(bb);
                                }
                            }
				        }
				        if(other.hasOptionalBorder() && tile.hasOptionalBorder()) {
					        BorderCluster _cluster = new BorderCluster();
					        _cluster.alignment = tiledata;
                             _cluster.z = 0x7FFFFFFF; // Above other zilch borders
                           // _cluster.z = 0;
					        _cluster.border = other.optional_border;
                            //Console.WriteLine("Aqui 05");
					        border_list.Add (_cluster);
				        } else {
					        tile.setOptionalBorder(false);
				        }			            
                    }
                }
                neighbours[i].first = true;
            }            
            tile.clearBorders();
            //Messages.AddLogMessage("Quantidade de border list: " + border_list.Count);

            //people.OrderBy(x => x.LastName).ToList(); 

            border_list = border_list.OrderByDescending(item => item.z).ToList();
            
            while (border_list.Count > 0)
            {
                //BorderCluster cluster = border_list.Last();

                BorderCluster cluster = border_list.First();

                if (cluster.border == null)
                {
                    border_list.Remove(cluster);
                    continue;                
                }

                //Console.WriteLine("id: " + cluster.border.Id + " alignment: " + cluster.alignment);                
                long[] i = new long[4];
                i[0] = ((Border_Types.border_types[cluster.alignment] & 0x000000FF) >> 0);
                i[1] = ((Border_Types.border_types[cluster.alignment] & 0x0000FF00) >> 8);
                i[2] = ((Border_Types.border_types[cluster.alignment] & 0x00FF0000) >> 16);
                i[3] = ((Border_Types.border_types[cluster.alignment] & 0xFF000000) >> 24);

                for (int iter = 0; iter < 4; ++iter)
                {
                    if (i[iter] != 0)
                    {
                        if(cluster.border.tiles[i[iter]] != 0) {                            
					        tile.addBorderItem(Item.Create(cluster.border.tiles[i[iter]]));
				        } else {
					        if(i[iter] == BorderType.NORTHWEST_DIAGONAL) 
                            {
                                tile.addBorderItem(Item.Create(cluster.border.tiles[BorderType.WEST_HORIZONTAL]));
                                tile.addBorderItem(Item.Create(cluster.border.tiles[BorderType.NORTH_HORIZONTAL]));
					        } 
                            else if(i[iter] == BorderType.NORTHEAST_DIAGONAL) 
                            {
                                tile.addBorderItem(Item.Create(cluster.border.tiles[BorderType.EAST_HORIZONTAL]));
                                tile.addBorderItem(Item.Create(cluster.border.tiles[BorderType.NORTH_HORIZONTAL]));
					        }
                            else if(i[iter] == BorderType.SOUTHWEST_DIAGONAL) 
                            {
                                tile.addBorderItem(Item.Create(cluster.border.tiles[BorderType.SOUTH_HORIZONTAL]));
                                tile.addBorderItem(Item.Create(cluster.border.tiles[BorderType.WEST_HORIZONTAL]));
                            }
                            else if (i[iter] == BorderType.SOUTHEAST_DIAGONAL)
                            {
                                tile.addBorderItem(Item.Create(cluster.border.tiles[BorderType.SOUTH_HORIZONTAL]));
                                tile.addBorderItem(Item.Create(cluster.border.tiles[BorderType.EAST_HORIZONTAL]));
					        }
				        }
                    }
                    else
                    {
                        break;
                    }
                }
                border_list.Remove(cluster);
            }
            if (specific_list.Count > 1)
            {
               // Global.mapCanvas.drawing = false;
              //  MessageBox.Show("Tem especifico");                
            }
        }

        public bool hasOuterZilchBorder()  
        {
            return has_zilch_outer_border || (optional_border != null);
        }

	    public bool hasInnerZilchBorder() 
        {
            return has_zilch_inner_border;
        }

        public bool hasOuterBorder()
        {
            return has_outer_border || (optional_border != null);
        }

        public bool hasInnerBorder()
        {
            return has_inner_border;
        }

        public bool hasOptionalBorder() 
        {
            return (optional_border != null);
        }

        public bool useSoloOptionalBorder()
        {
            return use_only_optional;
        }

        public int getZ() 
        {
            return z_order;
        }

        public override void load(XElement node) 
        {
            UInt16 intVal = 0;
            String strVal = "";
            look_id = node.Attribute("lookid").GetUInt16();
            if (node.Attribute("server_lookid").GetUInt16() != 0)
            {
                look_id = node.Attribute("server_lookid").GetUInt16();
            }
            z_order = node.Attribute("z-order").GetUInt16();
            use_only_optional = Generic.isTrueString(node.Attribute("solo_optional").GetString());
            randomize = Generic.isTrueString(node.Attribute("randomize").GetString());
            foreach (var item_node in node.Elements("item"))
            {
                UInt16 itemid = item_node.Attribute("id").GetUInt16();
                int chance = item_node.Attribute("chance").GetInt32();
                ItemType it = Global.items.items[itemid];
                if (it ==null)
                {
                    throw new Exception("Invalid item id brushId: " + this.id);
                }
                if (!it.isGroundTile())
                {
                    throw new Exception("is not ground item: " + itemid + " this is a " + it.Group.ToString());                    
                }

                if (it.brush !=null && !it.brush.Equals(this))
                {
                    throw new Exception("can not be member of two brushes id:" + itemid);
                }
                it.brush = this;
                ItemChanceBlock ci;
                ci.id = itemid;
                ci.chance = total_chance + chance;
                border_items.Add(ci);
                total_chance += chance;
            }
            foreach (var optional_node in node.Elements("optional"))
            {
                if (optional_border !=null)
                {
                    throw new Exception("Duplicate optional borders");
                   // continue;
                }                
                intVal = optional_node.Attribute("ground_equivalent").GetUInt16();
                if (intVal != 0)
                {
                    ItemType it = Global.items.items[intVal];
                    if (it ==null)
                    {
                        throw new Exception("Invalid id of ground dependency equivalent item");
                    }
                    if (!it.isGroundTile())
                    {
                        throw new Exception("Ground dependency equivalent is not a ground item");
                    }

                    if (!this.Equals(it.brush))
                    {
                        throw new Exception("Ground dependency equivalent does not use the same brush as ground border");
                    }

                    AutoBorder ab = new AutoBorder();
                    ab.Load(optional_node, this, intVal);
                    optional_border = ab;
                }
                else
                {
                    Int32 id = optional_node.Attribute("id").GetInt32();
                    if (id == 0)
                    {
                        throw new Exception("Missing tag id for border node");
                        //continue;
                    }
                    AutoBorder border_secound =null;

                    for (int x = 0; x <= Brushes.getInstance().maxBorderId; x++)
                    {
                        AutoBorder border = Brushes.getInstance().borders[x];
                        if ((border !=null) && 
                            (border.Id == id))
                        {
                            border_secound = border;
                        }
                    }                    
                    if (border_secound ==null)
                    {
                        throw new Exception("Could not find border id. Brush: " + name);
                    }
                    optional_border = border_secound;
                }
            }
            foreach (var border_node in node.Elements("border"))
            {
                AutoBorder ab;
                int id = border_node.Attribute("id").GetInt32();
                if (id == 0)
                {
                    intVal = border_node.Attribute("ground_equivalent").GetUInt16();
                    ItemType it = Global.items.items[intVal];
                    if (it ==null)
                    {
                        throw new Exception("Invalid id of ground dependency equivalent item");
                    }

                    if (!it.isGroundTile())
                    {
                        throw new Exception("Ground dependency equivalent is not a ground item");
                    }

                    if (!this.Equals(it.brush))
                    {
                        throw new Exception("Ground dependency equivalent does not use the same brush as ground border");
                    }
                    ab = new AutoBorder();
                    ab.Load(border_node, this, intVal);
                }
                else
                {
                    AutoBorder border_secound =null;

                    for (int x = 0; x <= Brushes.getInstance().maxBorderId; x++)
                    {
                        AutoBorder border = Brushes.getInstance().borders[x];
                        if ((border !=null) &&
                            (border.Id == id))
                        {
                            border_secound = border;
                        }
                    }
                    if (border_secound ==null)
                    {
                        throw new Exception("Could not find border id. Brush:" + name);
                    }
                    ab = border_secound;
                }
                BorderBlock bb = new BorderBlock();
                bb.super = false;
                bb.autoborder = ab;

                strVal = border_node.Attribute("to").GetString();
                if (!"".Equals(strVal))
                {
                    if ("all".Equals(strVal))
                    {
                        bb.to = 0xFFFFFFFF;
                    }
                    else if ("none".Equals(strVal))
                    {
                        bb.to = 0;
                    }
                    else
                    {
                        Brush toBrush = Brushes.getInstance().getBrush(strVal);
                        if (toBrush !=null)
                        {
                            bb.to = toBrush.getID();
                        }
                        else
                        {
                            throw new Exception("To brush " + strVal + " doesn't exist.");
                        }
                    }
                }
                else
                {
                    bb.to = 0xFFFFFFFF;
                }
                strVal = border_node.Attribute("super").GetString();
                if (!"".Equals(strVal))
                {
                    if (Generic.isTrueString(strVal))
                    {
                        bb.super = true;
                    }
                }
                strVal = border_node.Attribute("align").GetString();
                if (!"".Equals(strVal))
                {
                    if ("outer".Equals(strVal))
                    {
                        bb.outer = true;
                    }
                    else if ("inner".Equals(strVal))
                    {
                        bb.outer = false;
                    }
                    else
                    {
                        bb.outer = true;
                    }
                }

                if (bb.outer)
                {
                    if (bb.to == 0)
                    {
                        has_zilch_outer_border = true;
                    }
                    else
                    {
                        has_outer_border = true;
                    }
                }
                else
                {
                    if (bb.to == 0)
                    {
                        has_zilch_inner_border = true;
                    }
                    else
                    {
                        has_inner_border = true;
                    }
                }
                if (border_node.HasElements)
                {

                    foreach (var specific_border_node in border_node.Elements("specific"))
                    {
                        SpecificCaseBlock scb =null;

                        foreach (var conditions_specific in specific_border_node.Elements("conditions"))
                        {
                            foreach (var match_border_conditions in conditions_specific.Elements("match_border"))
                            {
                                int border_id = 0;
                                string edge = "";
                                border_id = match_border_conditions.Attribute("id").GetInt32();
                                edge = match_border_conditions.Attribute("edge").GetString();
                                if ((border_id == 0) || "".Equals(edge))
                                {
                                    continue;
                                }
                                int edge_id = AutoBorder.EdgeNameToEdge(edge);
                                AutoBorder bit_border = Brushes.getInstance().findBorder(edge_id);
                                if (bit_border ==null)
                                {
                                    throw new Exception("Unknown border id in specific case match block");
                                }

                                AutoBorder ab2 = bit_border;
                                if (ab2 ==null) throw new Exception("error ab2 ==null");
                                UInt16 match_itemid = ab.tiles[edge_id];
                                if (scb ==null) scb = new SpecificCaseBlock();
                                scb.items_to_match.Add(match_itemid);

                            }

                            foreach (var match_group_conditions in conditions_specific.Elements("match_group"))
                            {
                                uint group = 0;
                                String edge = "";
                                group = match_group_conditions.Attribute("group").GetUInt32();
                                edge = match_group_conditions.Attribute("edge").GetString();
                                if ((group == 0) || "".Equals(edge)) continue;

                                int edge_id = AutoBorder.EdgeNameToEdge(edge);
                                if (scb ==null) scb = new SpecificCaseBlock();
                                scb.match_group = group;
                                scb.group_match_alignment = edge_id;
                                scb.items_to_match.Add((UInt16) group);
                            }
                            foreach (var match_item_conditions in conditions_specific.Elements("match_item"))
                            {
                                ushort match_itemid = 0;
                                match_itemid = match_item_conditions.Attribute("id").GetUInt16();
                                if (match_itemid == 0) continue;
                                if (scb ==null) scb = new SpecificCaseBlock();
                                scb.match_group = 0;
                                scb.items_to_match.Add(match_itemid);
                            }
                        }

                        // aqui conditions_specific
                        foreach (var actions_specific in specific_border_node.Elements("actions"))
                        {
                            foreach (var actions_replace_border in actions_specific.Elements("replace_border"))
                            {
                                int border_id = 0;
								String edge = "";
								ushort with_id = 0;

                                border_id = actions_replace_border.Attribute("id").GetInt32();
                                edge = actions_replace_border.Attribute("edge").GetString();
                                with_id = actions_replace_border.Attribute("with").GetUInt16();

                                if ((border_id == 0) || ("".Equals(edge)) || (with_id == 0)) continue;
                                int edge_id = AutoBorder.EdgeNameToEdge(edge);
                                AutoBorder bit_border = Brushes.getInstance().findBorder(border_id);
                                if (bit_border ==null) throw new Exception("Unknown border id in specific case match block");                                

                                AutoBorder ab2 = bit_border;
                                if (ab2 ==null) throw new Exception("error ab2 ==null");
                                ItemType it = Global.items.items[with_id];
                                if (it ==null) throw new Exception("Unknown with_id in replace border");
                                it.IsBorder = true;
                                
                                if (scb ==null) scb = new SpecificCaseBlock();
                                scb.to_replace_id = ab2.tiles[edge_id];
                                scb.with_id = with_id;
                            }

                            foreach (var actions_replace_item in actions_specific.Elements("replace_item"))
                            {
                                ushort to_replace_id = actions_replace_item.Attribute("to_replace_id").GetUInt16();
                                ushort with_id = actions_replace_item.Attribute("with").GetUInt16(); ;
                                if ((to_replace_id == 0) || (with_id == 0)) continue;
                                ItemType it = Global.items.items[with_id];
                                if (it ==null) throw new Exception("Unknown with_id in replace item");
                                it.IsBorder = true;

                                if (scb ==null) scb = new SpecificCaseBlock();
                                scb.to_replace_id = to_replace_id;
                                scb.with_id = with_id;
                            }

                            foreach (var actions_delete_borders in actions_specific.Elements("delete_borders"))
                            {
                                if (scb ==null) scb = new SpecificCaseBlock();
                                scb.delete_all = true;
                            }
                        }
                        if (scb !=null)
                        {
                            bb.specific_cases.Add(scb);
                        }
                        
                    }
                }
                borders.Add(bb);                
            }
            foreach (var friend_node in node.Elements("friend"))
            {
                String friendName = friend_node.Attribute("name").GetString();
                if ("all".Equals(friendName))
                {
                    friends.Add(0xFFFFFFFF);
                }
                else
                {
                    Brush brush = Brushes.getInstance().getBrush(friendName);
                    if (brush !=null)
                    {
                        friends.Add(brush.getID());
                    }
                    else
                    {
                        throw new Exception("Brush" + friendName + " is not defined.");
                    }
                }
                hateFriends = false;
            }
            foreach (var friend_node in node.Elements("enemy"))
            {
                String enemyName = friend_node.Attribute("name").GetString();
                if ("all".Equals(enemyName))
                {
                    friends.Add(0xFFFFFFFF);
                }
                else
                {
                    Brush brush = Brushes.getInstance().getBrush(enemyName);
                    if (brush !=null)
                    {
                        friends.Add(brush.getID());
                    }
                    else
                    {
                        throw new Exception("Brush" + enemyName + " is not defined.");
                    }
                }
                hateFriends = true;
            }

            foreach (var friend_node in node.Elements("clear_borders"))
            {
                borders.Clear();
            }

            foreach (var friend_node in node.Elements("clear_friends"))
            {
                friends.Clear();
                hateFriends = false;
            }

            if (total_chance == 0)
            {
                randomize = false;
            }
        }
    }
}
