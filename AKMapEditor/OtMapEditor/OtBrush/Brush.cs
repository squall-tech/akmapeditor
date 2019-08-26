using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace AKMapEditor.OtMapEditor.OtBrush
{
    public abstract class Brush
    {
        protected uint id;
        protected bool visible;
        protected static uint id_counter = 0;

        public virtual void load(XElement node){ }
        public virtual void draw(GameMap map, Tile tile, Object param = null){ }        
        public virtual void undraw(GameMap map, Tile tile){ }
        public virtual bool canDraw(GameMap map, Position pos){ return false;}

        public uint getID() { return id; }
        public virtual void setName(String name){ }
        public virtual String getName(){ return "";}
        public virtual int getLookID() { return 0; }
        public virtual uint getSpriteLookID() { return 0; }
        public virtual bool needBorders() { return false; }
        public virtual bool canDrag() { return false; }
        public virtual bool canSmear() { return false; }
        public virtual bool oneSizeFitsAll() { return false; }
        public virtual int getMaxVariation() { return 0; }
	    public bool visibleInPalette() {return visible;}
	    public void flagAsVisible() {visible = true;}

        public bool inTerrainPalette = false;
        public bool inRawPalette = false;
        public bool inDoodadPalette = false;
        public bool inItemPalette = false;
        public bool inCreaturePalette = false;


        public Brush()
        {
            id = ++id_counter;
        }
    }
}
