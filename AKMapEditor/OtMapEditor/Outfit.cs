using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AKMapEditor.OtMapEditor
{
    public class Outfit
    {
        public int lookType;
        public int lookItem;
        public int lookAddon;
        public int lookHead;
        public int lookBody;
        public int lookLegs;
        public int lookFeet;

        public Outfit()
        {
            this.lookType = 0;
            this.lookItem = 0;
            this.lookAddon = 0;
            this.lookHead = 0;
            this.lookBody = 0;
            this.lookLegs = 0;
            this.lookFeet = 0;
        }

        public uint getColorHash()
        {
            return (uint) (lookHead << 24 | lookBody << 16 | lookLegs << 8 | lookFeet);
        }
    }
}
