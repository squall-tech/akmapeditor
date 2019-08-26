using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Xml.Linq;
using System.Xml;
using AKMapEditor.OtMapEditorServer;
using AKMapEditor.OtMapEditorServer.Classes;

namespace AKMapEditor.OtMapEditor
{
    public class GameMap
    {
        public enum OtMapNodeTypes
        {
            ROOTV1 = 0,
            ROOTV2 = 1,
            MAP_DATA = 2,
            ITEM_DEF = 3,
            TILE_AREA = 4,
            TILE = 5,
            ITEM = 6,
            TILE_SQUARE = 7,
            TILE_REF = 8,
            SPAWNS = 9,
            SPAWN_AREA = 10,
            MONSTER = 11,
            TOWNS = 12,
            TOWN = 13,
            HOUSETILE = 14,
            WAYPOINTS = 15,
            WAYPOINT = 16
        };

        public enum OtMapAttribute
        {
            NONE = 0,
            DESCRIPTION = 1,
            EXT_FILE = 2,
            TILE_FLAGS = 3,
            ACTION_ID = 4,
            UNIQUE_ID = 5,
            TEXT = 6,
            DESC = 7,
            TELE_DEST = 8,
            ITEM = 9,
            DEPOT_ID = 10,
            EXT_SPAWN_FILE = 11,
            RUNE_CHARGES = 12,
            EXT_HOUSE_FILE = 13,
            HOUSEDOORID = 14,
            COUNT = 15,
            DURATION = 16,
            DECAYING_STATE = 17,
            WRITTENDATE = 18,
            WRITTENBY = 19,
            SLEEPERGUID = 20,
            SLEEPSTART = 21,
            CHARGES = 22,
            CONTAINER_ITEMS = 23,
            ATTRIBUTE_MAP = 128
        };

        public UInt16 Width = 2000;
        public UInt16 Height = 2000;
        public Boolean ReadOnly = false;
        public Boolean MapOnSave = false;
        public UInt32 version = 3;
        public UInt32 majorVersionItems = 3;
        public UInt32 minorVersionItems = 40;
        public string spawnFile = "";
        public string houseFile = "";        
        public String MapName = "";
        public string FileName = "";
        public bool HasUpdates = false;
        public bool UpdateNeedRefresh = false;
        private bool replaceTiles;

        private ItemDatabase Items = null;

        private Dictionary<ulong, Tile> tiles;

        public List<Position> spawns;

        private object mapUpdateLock = new object();

        public GameMap()
        {
            tiles = new Dictionary<ulong, Tile>();
            spawns = new List<Position>();
        }

        public Dictionary<ulong, Tile> getTiles()
        {
            return tiles;
        }   

        public Tile getTile(int x, int y, int z)
        {
            ulong index = Position.ToIndex(x, y, z);
            if (tiles.ContainsKey(index)) return tiles[index];
            return null;
        }

        public Tile getTile(Position pos)
        {
            ulong index = pos.ToIndex();
            if (tiles.ContainsKey(index)) return tiles[index];
            return null;
        }

        public void setTile(int x, int y, int z, Tile newTile, bool remove = false)
        {
            ulong index = Position.ToIndex(x, y, z);
            tiles[index] = newTile;
            HasUpdates = true;
        }

        public void setTile(Position pos, Tile newTile, bool remove = false)
        {
            ulong index = pos.ToIndex();             
            tiles[index] = newTile;
            HasUpdates = true;
        }

        public Tile swapTile(Position pos, Tile newTile)
        {
            return swapTile(pos.x, pos.y, pos.z,newTile);
        }

        public Tile swapTile(int x, int y, int z, Tile newTile)
        {
            Tile tile = getTile(x, y, z);

            if (newTile != null)
            {
                if (tile == null)
                {
                    //++tilecount;
                }
                else
                {
                    newTile.stealExits(tile);
                    newTile.spawn_count = tile.spawn_count;
                    newTile.waypoint_count = tile.waypoint_count;
                    //newtile->spawn = tile->spawn;
                }
            }
            else if (tile != null)
            {
                if (tile.spawn_count > 0)
                {
                    newTile = new Tile(x, y, z);
                    newTile.stealExits(tile);
                    newTile.spawn_count = tile.spawn_count;
                    newTile.waypoint_count = tile.waypoint_count;
                    //newtile->spawn = tile->spawn;
                }
                else
                {
                    //--tilecount;
                }
            }


            ulong index = Position.ToIndex(x, y, z);
            tiles[index] = newTile;
            HasUpdates = true;
            return tile;
        }

        public void removeTile(Position pos)
        {
            removeTile(pos.x, pos.y, pos.z);
        }

        public void removeTile(int x, int y, int z)
        {
            HasUpdates = true;
            tiles.Remove(Position.ToIndex(x, y, z));
        }


        private void RemoveSpawnInternal(Tile tile)
        {
            Spawn spawn = tile.spawn;            

            int z = tile.getZ();
            int start_x = tile.getX() - spawn.getSize();
            int start_y = tile.getY() - spawn.getSize();
            int end_x = tile.getX() + spawn.getSize();
            int end_y = tile.getY() + spawn.getSize();

            for (int y = start_y; y <= end_y; ++y)
            {
                for (int x = start_x; x <= end_x; ++x)
                {                    
                    Tile ctile = getTile(x, y, z);
                    if (ctile != null && ctile.spawn_count > 0)
                    {
                        --ctile.spawn_count;
                    }
                }
            }
        }

        public void RemoveSpawn(Tile tile)
        {
            lock (mapUpdateLock)
            {
                if (tile.spawn != null)
                {
                    RemoveSpawnInternal(tile);
                    spawns.Remove(tile.Position);
                }
            }
            
        }


        public void AddSpawn(Tile tile)
        {
            lock (mapUpdateLock)
            {
                Spawn spawn = tile.spawn;
                if (spawn != null)
                {
                    int z = tile.getZ();
                    int start_x = tile.getX() - spawn.getSize();
                    int start_y = tile.getY() - spawn.getSize();
                    int end_x = tile.getX() + spawn.getSize();
                    int end_y = tile.getY() + spawn.getSize();

                    for (int y = start_y; y <= end_y; ++y)
                    {
                        for (int x = start_x; x <= end_x; ++x)
                        {
                            Tile ctile = getTile(x, y, z);
                            if (ctile == null)
                            {
                                ctile = new Tile(x, y, z);
                                setTile(x, y, z, ctile);
                            }
                            ++ctile.spawn_count;
                        }
                    }
                    spawns.Add(tile.Position);
                }    
            }        
        }

        public void UpdateMap(MapUpdate mapUpdate, MapEditor editor)
        {
            lock (mapUpdateLock) // não permitir salvar ao atualizar o mapa.
            {
                BatchAction batchAction = mapUpdate.batchAction;

                if (mapUpdate.updateType == UpdateType.UPDATE_TYPE_UNDO)
                {
                    foreach (Action action in batchAction.actions.Reverse<Action>())
                    {
                        action.SetMap(this);
                        action.SetEditor(editor);
                        action.undo();
                    }
                }
                else
                {
                    foreach (Action action in batchAction.actions)
                    {
                        action.SetMap(this);
                        action.SetEditor(editor);
                        switch (mapUpdate.updateType)
                        {
                            case UpdateType.UPDATE_TYPE_COMMIT:
                                action.commit();
                                break;
                            case UpdateType.UPDATE_TYPE_REDO:
                                action.redo();
                                break;
                        }
                    }
                }



            }            
        }

        public UInt64 size()
        {
            return (UInt64) tiles.Count();
        }

        public void clear()
        {
            tiles.Clear();
        }            

        public void Load(string fileName, bool replaceTiles)
        {
            this.FileName = fileName;
            this.replaceTiles = replaceTiles;
            //Thread t = new Thread(Load);
            //t.Start();
            try
            {
                LoadOtbm();
                LoadSpawns();
            }
            finally
            {
                HasUpdates = false; 
            }            
        }
        public void Save(String fileName)
        {
            try
            {                
                lock (mapUpdateLock)
                {

                    if (spawnFile == "")
                    {
                        spawnFile = Path.GetFileNameWithoutExtension(FileName) + ".xml";
                    }

                    bool makebackup = Settings.GetBoolean(Key.MAKE_BACKUP_ONSAVE);
                    this.FileName = fileName;
                    MapName = Path.GetFileName(FileName);

                    if (makebackup && File.Exists(FileName))
                    {
                        File.Copy(FileName, Path.GetDirectoryName(FileName) + "\\backup_" + MapName);
                    }
                    SaveOtbm(fileName);
                    if (makebackup && File.Exists(FileName))
                    {
                        File.Delete(Path.GetDirectoryName(FileName) + "\\backup_" + MapName);
                    }
                    SaveSpawns();
                }
            }
            catch (Exception ex)
            {
                ReadOnly = true;
                throw ex;
            }
        }        
      
        private void LoadOtbm()
        {
            if (!File.Exists(FileName))
                throw new Exception(string.Format("File not found {0}.", FileName));

           // FileName = fileName;
            MapName = Path.GetFileName(FileName);
            var tileLocations = new HashSet<ulong>();

            using (var reader = new FileReader(FileName))
            {
                BinaryNode node = reader.GetRootNode();

                PropertyReader props = reader.GetPropertyReader(node);

                props.ReadByte(); // junk?

                version = props.ReadUInt32();
                Width = props.ReadUInt16();
                Height = props.ReadUInt16();

                majorVersionItems = props.ReadUInt32();
                minorVersionItems = props.ReadUInt32();

                if (minorVersionItems != 40) 
                {                    
                    if (Settings.GetBoolean(Key.BLOCKING_VERSION))
                    {
                        MessageBox.Show("O mapa não esta na versão 9.6. favor converter o mesmo para 9.6 no RME");
                        ReadOnly = true;
                        this.FileName = "";
                        this.MapName = "";
                        Generic.Abort();
                    }                    
                }

                if (version <= 0)
                {
                    //In otbm version 1 the count variable after splashes/fluidcontainers and stackables
                    //are saved as attributes instead, this solves alot of problems with items
                    //that is changed (stackable/charges/fluidcontainer/splash) during an update.
                    throw new Exception(
                        "This map needs to be upgraded by using the latest map editor version to be able to load correctly.");
                }


                Items = Global.items;

                if (version > 3)
                {
                    throw new Exception("Unknown OTBM version detected.");
                }

                if (majorVersionItems < 3)
                {
                    throw new Exception(
                        "This map needs to be upgraded by using the latest map editor version to be able to load correctly.");
                }

                if (majorVersionItems > Items.MajorVersion)
                {
                    throw new Exception("The map was saved with a different items.otb version, an upgraded items.otb is required.");
                }

                if (minorVersionItems > Items.MinorVersion)
                {
                    //Trace.WriteLine("This map needs an updated items.otb.");
                }

                node = node.Child;

                reader.clearRootNode();


                if ((OtMapNodeTypes)node.Type != OtMapNodeTypes.MAP_DATA)
                {
                    throw new Exception("Could not read data node.");
                }

                props = reader.GetPropertyReader(node);

                while (props.PeekChar() != -1)
                {
                    byte attribute = props.ReadByte();
                    switch ((OtMapAttribute)attribute)
                    {
                        case OtMapAttribute.DESCRIPTION:
                            var description = props.GetString();
                            //Descriptions.Add(description);
                            break;
                        case OtMapAttribute.EXT_SPAWN_FILE:
                            spawnFile = props.GetString();
                            break;
                        case OtMapAttribute.EXT_HOUSE_FILE:
                            houseFile = props.GetString();
                            break;
                        default:
                            throw new Exception("Unknown header node.");
                    }
                }

                BinaryNode nodeMapData = node.Child;
                while (nodeMapData != null)
                {
                    switch ((OtMapNodeTypes)nodeMapData.Type)
                    {
                        case OtMapNodeTypes.TILE_AREA:
                            ParseTileArea(reader, nodeMapData, replaceTiles, tileLocations);
                            break;
                        case OtMapNodeTypes.TOWNS:
                            ParseTowns(reader, nodeMapData);
                            break;
                    }
                    nodeMapData = nodeMapData.Next;
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            //LoadSpawn(Path.Combine(Path.GetDirectoryName(fileName), spawnFile), tileLocations);
        }
        private void ParseTileArea(FileReader reader, BinaryNode otbNode, bool replaceTiles, ISet<ulong> tileLocations)
        {
            PropertyReader props = reader.GetPropertyReader(otbNode);

            int baseX = props.ReadUInt16();
            int baseY = props.ReadUInt16();
            int baseZ = props.ReadByte();

            BinaryNode nodeTile = otbNode.Child;

            while (nodeTile != null)
            {
                if (nodeTile.Type == (long)OtMapNodeTypes.TILE ||
                    nodeTile.Type == (long)OtMapNodeTypes.HOUSETILE)
                {
                    props = reader.GetPropertyReader(nodeTile);

                    var tileLocation = new Position(baseX + props.ReadByte(), baseY + props.ReadByte(), baseZ);

                    var tile = new Tile(tileLocation);

                    if (nodeTile.Type == (long)OtMapNodeTypes.HOUSETILE)
                    {
                        tile.HouseId = props.ReadUInt32();
                    }

                    while (props.PeekChar() != -1)
                    {
                        byte attribute = props.ReadByte();
                        switch ((OtMapAttribute)attribute)
                        {
                            case OtMapAttribute.TILE_FLAGS:
                                {
                                    tile.mapFlags = props.ReadUInt32();
                                    break;
                                }
                            case OtMapAttribute.ITEM:
                                {
                                    ushort itemId = props.ReadUInt16();

                                    var itemType = Items.items[itemId];
                                    if (itemType == null)
                                    {
                                        throw new Exception("Unkonw item type " + itemId + " in position " + tileLocation + ".");
                                    }

                                    var item = Item.Create(itemType);
                                    tile.addItem(item);
                                    break;
                                }
                            default:
                                throw new Exception(string.Format("{0} Unknown tile attribute.", tileLocation));
                        }
                    }

                    BinaryNode nodeItem = nodeTile.Child;

                    while (nodeItem != null)
                    {
                        if (nodeItem.Type == (long)OtMapNodeTypes.ITEM)
                        {
                            props = reader.GetPropertyReader(nodeItem);

                            ushort itemId = props.ReadUInt16();

                            var itemType = Items.items[itemId];
                            if (itemType == null)
                            {
                                throw new Exception("Unkonw item type " + itemId + " in position " + tileLocation + ".");
                            }

                            var item = Item.Create(itemType);
                            item.Deserialize(reader, nodeItem, props);

                            tile.addItem(item);
                        }
                        else
                        {
                            throw new Exception(string.Format("{0} Unknown node type.", tileLocation));
                        }
                        nodeItem = nodeItem.Next;
                    }

                    // adicionar o tile aqui.
                    setTile(tile.Position, tile, true);
                 }

                nodeTile = nodeTile.Next;
            }
        }
        private void ParseTowns(FileReader reader, BinaryNode otbNode)
        {
            BinaryNode nodeTown = otbNode.Child;

            while (nodeTown != null)
            {
                PropertyReader props = reader.GetPropertyReader(nodeTown);

                uint townid = props.ReadUInt32();
                string townName = props.GetString();
                var templeLocation = props.ReadPosition();

               // var town = new OtTown { Id = townid, Name = townName, TempleLocation = templeLocation };
                //towns[townid] = town;

                nodeTown = nodeTown.Next;
            }
        }        
        private void SaveOtbm(string fileName)
        {
            MapOnSave = true;
            try
            {
                using (var writer = new FileWriter(fileName))
                {
                    //Header                

                    writer.Write((uint)0, false);

                    writer.WriteNodeStart((byte)OtMapNodeTypes.ROOTV1);

                    writer.Write(version);
                    writer.Write(Width);
                    writer.Write(Height);
                    writer.Write(majorVersionItems);
                    writer.Write(minorVersionItems);

                    //Map Data
                    writer.WriteNodeStart((byte)OtMapNodeTypes.MAP_DATA);

                    writer.Write((byte)OtMapAttribute.DESCRIPTION);
                    writer.Write("AkMapEditor");

                    writer.Write((byte)OtMapAttribute.EXT_HOUSE_FILE);
                    writer.Write(houseFile);

                    writer.Write((byte)OtMapAttribute.EXT_SPAWN_FILE);
                    writer.Write(spawnFile);

                    bool first = true;
                    int local_x = -1, local_y = -1, local_z = -1;

                    foreach (var tile in tiles.Values)
                    {
                        if (tile.size() <= 0)
                        {
                            continue;
                        }

                        Position pos = tile.Position;

                        if (pos.x < local_x || pos.x >= local_x + 256 ||
                           pos.y < local_y || pos.y >= local_y + 256 ||
                           pos.z != local_z)
                        {                            
                            if (!first)
                            {
                                writer.WriteNodeEnd();
                            }

                            first = false;

                            writer.WriteNodeStart((byte)OtMapNodeTypes.TILE_AREA);
                            writer.Write((ushort)(local_x = pos.x & 0xFF00));
                            writer.Write((ushort)(local_y = pos.y & 0xFF00));
                            writer.Write((ushort)(local_z = pos.z));

                        }

                        if (tile.HouseId > 0)
                            writer.WriteNodeStart((byte)OtMapNodeTypes.HOUSETILE);
                        else
                            writer.WriteNodeStart((byte)OtMapNodeTypes.TILE);

                        writer.Write((byte)(tile.Position.X & 0xFF));
                        writer.Write((byte)(tile.Position.Y & 0xFF));

                        if (tile.HouseId > 0)
                            writer.Write(tile.HouseId);

                        if (tile.mapFlags > 0)
                        {
                            writer.Write((byte)OtMapAttribute.TILE_FLAGS);
                            writer.Write(tile.mapFlags);
                        }

                        if (tile.Ground != null)
                        {
                            if (tile.Ground.Type.isMetaItem())
                            {
                            }
                            else if (tile.Ground.hasBorderEquivalent())
                            {
                                bool found = false;
                                foreach (Item it in tile.Items)
                                {
                                    if ((it.getGroundEquivalent() != null) && (it.getGroundEquivalent().Id == tile.Ground.Type.Id))
                                    {
                                        found = true;
                                        break;
                                    }
                                }

                                if (!found)
                                {
                                    writer.WriteNodeStart((byte)OtMapNodeTypes.ITEM);
                                    writer.Write(tile.Ground.Type.Id);
                                    tile.Ground.Serialize(writer, writer.GetPropertyWriter());
                                    writer.WriteNodeEnd(); //Item
                                }
                            }
                            else
                            {
                                writer.WriteNodeStart((byte)OtMapNodeTypes.ITEM);
                                writer.Write(tile.Ground.Type.Id);
                                tile.Ground.Serialize(writer, writer.GetPropertyWriter());
                                writer.WriteNodeEnd(); //Item
                            }
                        }

                        foreach (var item in tile.Items)
                        {
                            writer.WriteNodeStart((byte)OtMapNodeTypes.ITEM);

                            writer.Write(item.Type.Id);
                            item.Serialize(writer, writer.GetPropertyWriter());

                            writer.WriteNodeEnd(); //Item
                        }

                        writer.WriteNodeEnd(); //End node Tile
                    }

                    // Only close the last node if one has actually been created
                    if (!first)
                    {
                        writer.WriteNodeEnd();
                    }

                    writer.WriteNodeStart((byte)OtMapNodeTypes.TOWNS);

                    /*
                    foreach (var town in towns.Values)
                    {
                        writer.WriteNodeStart((byte)OtMapNodeTypes.TOWN);
                        writer.Write(town.Id);
                        writer.Write(town.Name);
                        writer.Write(town.TempleLocation);
                        writer.WriteNodeEnd(); //Town
                    } */

                    writer.WriteNodeEnd(); //Towns

                    writer.WriteNodeEnd(); //Map Data
                    writer.WriteNodeEnd(); //Root
                }
            }
            finally
            {
                MapOnSave = false;
                HasUpdates = false;
            }
        }


        private void LoadSpawns()
        {
            String spawnFileName = Path.GetDirectoryName(FileName) + "\\" + spawnFile;
            if (!File.Exists(spawnFileName))
            {                
                return;
            }
            XElement spawnDoc = XElement.Load(spawnFileName);
		    int intVal = 0;
		    string strVal = "";

            foreach (XElement nodeSpawn in spawnDoc.Elements())
            {
                if (nodeSpawn.Name.LocalName == "spawn")
                {
                    bool posok = true;
                    Position spawnpos = new Position();
                    intVal = nodeSpawn.Attribute("centerx").GetInt32();
                    if (intVal > 0)
                    {
                        spawnpos.X = intVal;
                    }
                    else
                    {
                        posok = false;
                    }
                    intVal = nodeSpawn.Attribute("centery").GetInt32();
                    if (intVal > 0)
                    {
                        spawnpos.Y = intVal;
                    }
                    else
                    {
                        posok = false;
                    }
                    intVal = nodeSpawn.Attribute("centerz").GetInt32();
                    if (intVal > 0)
                    {
                        spawnpos.Z = intVal;
                    }
                    else
                    {
                        posok = false;
                    }

                    if (!posok)
                    {
                        //"Bad position data on one spawn, discarding...";
                        continue;
                    }
                    int radius = 1;
                    intVal = nodeSpawn.Attribute("radius").GetInt32();
                    if (intVal > 0)
                    {
                        radius = Math.Max(1, intVal);
                    }
                    else
                    {
                        //Couldn't read radius of spawn.. discarding spawn...
                        continue;
                    }

                    Tile tile = this.getTile(spawnpos);
                    if (tile != null && tile.spawn != null)
                    {
                        //Duplicate spawn on position 
                        continue;
                    }

                    Spawn spawn = new Spawn(radius);

                    if (tile == null)
                    {
                        tile = new Tile();
                        tile.Position = spawnpos;
                        this.setTile(spawnpos,tile);
                    }
                    tile.spawn = spawn;
                    this.AddSpawn(tile);

                    foreach (XElement creatureNode in nodeSpawn.Elements())
                    {
                        if ((creatureNode.Name.LocalName == "monster") || (creatureNode.Name.LocalName == "npc"))  
                        {
                            String name = "";
                            int spawntime = Settings.GetInteger(Key.DEFAULT_SPAWNTIME);
                            bool isNpc = (creatureNode.Name.LocalName == "npc");
                            name = creatureNode.Attribute("name").GetString();
                            if (name == "")
                            {
                                //No name of monster spawn at
                                continue;
                            }
                            intVal = creatureNode.Attribute("spawntime").GetInt32();
                            if (intVal > 0)
                            {
                                spawntime = intVal;
                            }
                            posok = true;
                            Position creaturepos = new Position(spawnpos);
                                                       
                            if (creatureNode.Attribute("x") != null) { creaturepos.x += creatureNode.Attribute("x").GetInt32(); } else { posok = false; }
                            if (creatureNode.Attribute("y") != null) { creaturepos.y += creatureNode.Attribute("y").GetInt32(); } else { posok = false; }

                            if (posok == false)
                            {
                                //Bad creature position data, discarding creature
                                continue;
                            }

                            if (Math.Abs(creaturepos.x - spawnpos.x) > radius)
                            {
                                radius = Math.Abs(creaturepos.x - spawnpos.x);
                            }
                            if (Math.Abs(creaturepos.y - spawnpos.y) > radius)
                            {
                                radius = Math.Abs(creaturepos.y - spawnpos.y);
                            }

                            radius = Math.Min(radius, Settings.GetInteger(Key.MAX_SPAWN_RADIUS));

                            Tile creature_tile = null;
                            if (creaturepos == spawnpos)
                            {
                                creature_tile = tile;
                            }
                            else
                            {
                                creature_tile = this.getTile(creaturepos);
                            }
                            if (creature_tile == null)
                            {
                                //Discarding creature
                                continue;
                            }

                            CreatureType type = CreatureDatabase.creatureDatabase[name];
                            if (type == null)
                            {
                                CreatureDatabase.creatureDatabase.addMissingCreatureType(name, isNpc);
                            }
                            Creature creature = new Creature(type);
                            creature.setSpawnTime(spawntime);
                            creature_tile.creature = creature;
                            if (creature_tile.spawn_count == 0)
                            {
                                Spawn creature_spawn = new Spawn(5);
                                creature_tile.spawn = creature_spawn;
                                AddSpawn(creature_tile);
                            }
                        }
                    }
                    
                } 
            }
        }

        private void SaveSpawns()
        {            
            String spawnFileName = Path.GetDirectoryName(FileName) + "\\" + spawnFile;

            XElement spawnDoc = new XElement("spawns");

            foreach (Position pos in spawns)
            {
                Tile tile = getTile(pos);
                Spawn s = tile.spawn;

                XElement spawn = new XElement("spawn");
                spawn.Add(new XAttribute("centerx", pos.X ));
                spawn.Add(new XAttribute("centery", pos.Y));
                spawn.Add(new XAttribute("centerz", pos.Z));
                spawn.Add(new XAttribute("radius", s.getSize()));

                for (int y = -tile.spawn.getSize(); y <= tile.spawn.getSize(); ++y)
                {
                    for (int x = -tile.spawn.getSize(); x <= tile.spawn.getSize(); ++x)
                    {
                        Tile creature_tile = getTile(pos + new Position(x, y, 0));
                        if (creature_tile != null)
                        {
                            Creature c = creature_tile.creature;
                            if (c != null)
                            {

                                XElement creatureSpawn = new XElement(c.isNpc() ? "npc" : "monster");
                                creatureSpawn.Add(new XAttribute("name", c.getName()));
                                creatureSpawn.Add(new XAttribute("x", x));
                                creatureSpawn.Add(new XAttribute("y", y));
                                creatureSpawn.Add(new XAttribute("z", pos.z));
                                creatureSpawn.Add(new XAttribute("spawntime", "60"));

                                spawn.Add(creatureSpawn);
                            }
                        }
                    }
                }
                spawnDoc.Add(spawn);
            }

            var xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Encoding = ASCIIEncoding.UTF8;
            xmlWriterSettings.Indent = true;
            using (var xmlWriter = XmlWriter.Create(spawnFileName, xmlWriterSettings))
            {
                spawnDoc.Save(xmlWriter);
            }            
        }
    }

}
