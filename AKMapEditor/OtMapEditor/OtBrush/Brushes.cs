using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Windows.Forms;

namespace AKMapEditor.OtMapEditor.OtBrush
{
    public class Brushes
    {

        private static Brushes brushes;

        public Dictionary<String,Brush> brushList;
        public AutoBorder[] borders;
        public uint maxBorderId;

        public static Brushes getInstance()
        {
            if (brushes == null)
            {
                brushes = new Brushes();
            }
            return brushes;
        }        
        
        public Brushes()
        {
            borders = new AutoBorder[100000];
            brushList = new Dictionary<string, Brush>();
            maxBorderId = 0;
        }

        public void unserializeBorder(XElement border_node) 
        {
            uint id = border_node.Attribute("id").GetUInt32();
            if (borders[id] != null) return;

            AutoBorder border = new AutoBorder();
            border.Id = id;
            border.Load(border_node);
            borders[id] = border;
            if (id > maxBorderId)
            {
                maxBorderId = id;
            }
        }

        public void unserializeBrush(XElement border_node)
        {
            Brush brush = null;
            String name = border_node.Attribute("name").GetString();
            if (name == "")
            {
                //
            }
            brush = getBrush(name);

            if (brush == null)
            {
                String type = border_node.Attribute("type").GetString();

                switch (type)
                {
                    case "border":
                        brush = new GroundBrush();
                        break;
                    case "ground":
                        brush = new GroundBrush();
                        break;
                    case "wall":
                        brush = new WallBrush();
                        break;
                    case "wall decoration":
                        brush = new WallDecorationBrush();
                        break;
                    case "carpet":
                        brush = new CarpetBrush();
                        break;
                    case "table":
                        brush = new TableBrush();
                        break;
                    case "doodad":
                        brush = new DoodadBrush();
                        break;
                    default:
                        Messages.AddWarning("Unknown brush type type:" + type);
                        break;
                }
                brush.setName(name);
            }

            if (!border_node.HasElements)
            {
                brushList.Add(name, brush);
            }
            else
            {
                brush.load(border_node);

                if (getBrush(name) != null) 
                {
                    if (getBrush(name).getLookID() != brush.getLookID())
                    {
                        //erro
                    }
                    else
                    {
                        return;
                    }
                }
                brushList.Add(name, brush);
            }            
        }

        public void init()
        {
            Border_Types.init();
        }
        public void clear()
        {

        }

        public AutoBorder findBorder(int border_Id)
        {
            return borders[border_Id];
        }

        public Brush getBrush(String name)
        {
            Brush retorno = null;
            brushList.TryGetValue(name, out retorno);
            return retorno;
        }

        public void addBrush(Brush brush)
        {
            try
            {
                if (typeof(RAWBrush).Equals(brush.GetType()))
                {
                    brushList["" + ((RAWBrush)brush).itemtype.Id] = brush;
                }
                else
                {
                    brushList[brush.getName()] = brush;
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + brush.getLookID());
                throw ex;
            }              
        }
    }
}
