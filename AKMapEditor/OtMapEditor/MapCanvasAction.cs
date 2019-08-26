using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AKMapEditor.OtMapEditor
{
    public class MapCanvasAction
    {
        private MapCanvas canvas;

        public MapCanvasAction(MapCanvas canvas)
        {
            this.canvas = canvas;
        }

        public MapEditor getMapEditor()
        {
            return canvas.getMapEditor();
        }

    }
}
