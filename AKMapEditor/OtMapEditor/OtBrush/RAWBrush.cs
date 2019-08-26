using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AKMapEditor.OtMapEditor.OtBrush
{
    public class RAWBrush : Brush
    {
        public ItemType itemtype;

        public RAWBrush(int itemId)
        {
            ItemType it = Global.items.items[itemId];
            if (it != null) itemtype = it;
        }
        
        public override int getLookID()
        {
            if (itemtype != null)
            {
                return itemtype.Id;
            }
            else
            {
                return base.getLookID();            
            }
        }
        public override string getName()
        {
            if ((itemtype != null) && (itemtype.Name != null))
            {                
                return itemtype.Name;
            }
            else
            {
                return base.getName();
            }
            
        }

        public override void undraw(GameMap map, Tile tile)
        {
            if ((tile.Ground != null) && 
                (tile.Ground.Type.Id == itemtype.Id))
            {
                tile.Ground = null;                
            }
            List<Item> itemsToRemove = new List<Item>();
            foreach(Item item in tile.Items)
            {
                if (item.Type.Id == itemtype.Id)
                {
                    itemsToRemove.Add(item);
                }
            }
            foreach (Item item in itemsToRemove)
            {
                tile.Items.Remove(item);
            }
        }

        public override void draw(GameMap map, Tile tile, object parameter = null)
        {
	        if(itemtype == null) {return;}

	        bool b = (bool) parameter;
            
	        if ((Settings.GetBoolean(Key.RAW_LIKE_SIMONE) && !b) && 
                (itemtype.alwaysOnBottom && itemtype.AlwaysOnTopOrder == 2)) {
                tile.startRemove();
		        foreach(Item item in tile.Items)                
                {
			        if(item.getTopOrder() == itemtype.AlwaysOnTopOrder) {
                        tile.AddItemToRemove(item);
				        break;
			        }
		        }
                tile.RemoveItems();
	        }            
	        tile.addItem(Item.Create(itemtype.Id));            
        }
    }
}
