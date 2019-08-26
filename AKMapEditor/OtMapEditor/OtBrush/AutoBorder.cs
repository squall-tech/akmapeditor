using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;

namespace AKMapEditor.OtMapEditor.OtBrush
{
    public class AutoBorder
    {
        public uint Id;
        public UInt16[] tiles;
        public UInt16 Group;
        public bool IsOptionalBorder;

        public AutoBorder()
        {
            tiles = new UInt16[13];
            IsOptionalBorder = false;
        }

        public void Load(XElement border_node, GroundBrush owner = null, UInt16 groundEquivalent = 0)
        {
            String type = border_node.Attribute("type").GetString();
            if ("optional".Equals(type))
            {
                this.IsOptionalBorder = true;
            }

            this.Group = border_node.Attribute("type").GetUInt16();

            foreach (var borderitem_node in border_node.Elements("borderitem"))
            {
                UInt16 itemId =  borderitem_node.Attribute("item").GetUInt16();
                ItemType itemType = Global.items.items[itemId];
                if (itemType == null) continue;                

                if (owner != null)
                {
                    itemType.Group = ItemGroup.None;
                    itemType.GroundEquivalent = Global.items.items[groundEquivalent];
                    if (itemType.GroundEquivalent != null)
                    {
                        itemType.GroundEquivalent.HasEquivalent = true;
                    }
                }

                itemType.alwaysOnBottom = true;
                itemType.IsBorder = true;
                itemType.IsOptionalBorder = this.IsOptionalBorder;
                itemType.Group = (ItemGroup) this.Group;

                int edge = EdgeNameToEdge(borderitem_node.Attribute("edge").GetString());
                if (edge != BorderType.BORDER_NONE)
                {
                    tiles[edge] = itemId;
                    if (itemType.BorderAlignment == BorderType.BORDER_NONE)
                    {
                        itemType.BorderAlignment = edge;
                    }
                }
            }
        }

        public static int EdgeNameToEdge(String edgename)
        {
            if ("n".Equals(edgename))
            {
                return BorderType.NORTH_HORIZONTAL;
            }
            else if ("w".Equals(edgename))
            {
                return BorderType.WEST_HORIZONTAL;
            }
            else if ("s".Equals(edgename))
            {
                return BorderType.SOUTH_HORIZONTAL;
            }
            else if ("e".Equals(edgename))
            {
                return BorderType.EAST_HORIZONTAL;
            }
            else if ("cnw".Equals(edgename))
            {
                return BorderType.NORTHWEST_CORNER;
            }
            else if ("cne".Equals(edgename))
            {
                return BorderType.NORTHEAST_CORNER;
            }
            else if ("csw".Equals(edgename))
            {
                return BorderType.SOUTHWEST_CORNER;
            }
            else if ("cse".Equals(edgename))
            {
                return BorderType.SOUTHEAST_CORNER;
            }
            else if ("dnw".Equals(edgename))
            {
                return BorderType.NORTHWEST_DIAGONAL;
            }
            else if ("dne".Equals(edgename))
            {
                return BorderType.NORTHEAST_DIAGONAL;
            }
            else if ("dsw".Equals(edgename))
            {
                return BorderType.SOUTHWEST_DIAGONAL;
            }
            else if ("dse".Equals(edgename))
            {
                return BorderType.SOUTHEAST_DIAGONAL;
            }
            return BorderType.BORDER_NONE;
        }
    }
}
