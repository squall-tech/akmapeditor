using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarsiLibrary.Win;
using System.IO;
using System.Windows.Forms;
using Brush = AKMapEditor.OtMapEditor.OtBrush.Brush;
using AKMapEditor.OtMapEditor.OtBrush;
using AKMapEditor.OtMapEditorServer;
using AKMapEditor.OtMapEditorServer.Classes;
using System.Threading;

namespace AKMapEditor.OtMapEditor
{
    public class MapEditor : FATabStripItem
    {
        private static UInt16 genId;

        //--------Map-----------
        private String fileName;
        public GameMap gameMap;
        public ActionQueue queue;


        public Selection selection;

//        public CopyBuffer copybuffer;


        public CopyBuffer copybuffer { get { return canvas.copybuffer; } }


        //---------- online -----------
        public ClientConnection clientConnection;
        private String ip = "", password = "";        
        //public Dictionary<ulong, Position> updatedPosition;
        //public bool updating = false;


        //-----------general
        public Double Zoom = 1;
        public Int32 MapZ = 7;
//        private EditorMode editorMode;

        public GroundBrush replace_brush;
        public bool replace_dragging = false;

        //--------Controls--------
        private Panel glPanel;
        private MapCanvas canvas;
        private VScrollBar leftScrollBar;
        private HScrollBar bottomScrollBar;
        public System.Windows.Forms.Timer timerConnect;
        private System.ComponentModel.IContainer components;
        private Button editorModeButton;

        #region StartRegion
        public MapEditor(String fileName) 
            : base ("",null)
        {
            this.fileName = fileName;
            this.Title = ("".Equals(fileName)) ? "Untitled-" + getId() + ".otbm" : Path.GetFileName(fileName);
            this.Resize += new System.EventHandler(this.MapViewer_Resize);
            gameMap = new GameMap();          
            Global.editorMode = EditorMode.Selection;
            queue = new ActionQueue(this);
            selection = new Selection(this);            
        }


        public GameMap map { get { return gameMap; } }


        public MapEditor(String ip, String password) : base ("",null)
        {
            this.fileName = "";
            this.Title = "";
            this.ip = ip;
            this.password = password;            
            //this.updatedPosition = new Dictionary<ulong, Position>();
            this.Resize += new System.EventHandler(this.MapViewer_Resize);
            gameMap = new GameMap();
            gameMap.ReadOnly = true;
            Global.editorMode = EditorMode.Selection;
            queue = new ActionQueue(this);
            selection = new Selection(this);            
        }

        public void Start()
        {
            //CreateComponents(); incluido em outro lugar provisório            
            if (!"".Equals(fileName))
            {
                gameMap.Load(fileName, true);
            }

            if (!"".Equals(this.ip))
            {
                try
                {
                    clientConnection = new ClientConnection(this.ip, ServerForm.SERVER_PORT, this);
                    clientConnection.connect();
                    clientConnection.doLogin(password);
                    timerConnect.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Editor Start" + ex.Message);
                    ((FATabStrip)Parent).RemoveTab(this);
                }
            }

            UpdateScrollBar();

            bottomScrollBar.Value = bottomScrollBar.Maximum / 2;
            leftScrollBar.Value = leftScrollBar.Maximum / 2;


            canvas.Refresh();            
        }


        public void CloseMap()
        {            
            if (this.InvokeRequired)
                this.BeginInvoke((MethodInvoker)delegate
                {                    
                    if (this.Parent != null)
                    {                        
                        ((FATabStrip)Parent).RemoveTab(this);
                    }                    
                });           
        }

        public void updateViewMap()
        {

            if (this.InvokeRequired)
                this.BeginInvoke((MethodInvoker)delegate
                {
                    UpdateScrollBar();

                    bottomScrollBar.Value = bottomScrollBar.Maximum / 2;
                    leftScrollBar.Value = leftScrollBar.Maximum / 2;
                });
            canvasRefresh();
        }

        private void CreateComponents()
        {
            if (glPanel != null) return;
        //    this.SuspendLayout();

            glPanel = new Panel();
            glPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            glPanel.Location = new System.Drawing.Point(0, 0);
            glPanel.Size = new System.Drawing.Size(Size.Width - 17, Size.Height - 17);
            glPanel.Name = "glPanel";          
            glPanel.TabIndex = 0;

            this.Controls.Add(glPanel);

            leftScrollBar = new VScrollBar();

            leftScrollBar.Location = new System.Drawing.Point(Size.Width - 17, 0);
            leftScrollBar.Name = "vScrollBar1";
            leftScrollBar.TabIndex = 1;
            leftScrollBar.Size = new System.Drawing.Size(17, Size.Height - 17);
            leftScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.leftScrollBarScroll);

            this.Controls.Add(leftScrollBar);

            bottomScrollBar = new HScrollBar();
            bottomScrollBar.Location = new System.Drawing.Point(0, Size.Height - 17);
            bottomScrollBar.Name = "hScrollBar1";
            bottomScrollBar.TabIndex = 2;
            bottomScrollBar.Size = new System.Drawing.Size(Size.Width - 17, 17);

            bottomScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.bottomScrollBarScroll);

            this.Controls.Add(bottomScrollBar);


            editorModeButton = new Button();
            editorModeButton.Location = new System.Drawing.Point(Size.Width - 17, Size.Height - 17);
            editorModeButton.Size = new System.Drawing.Size(18, 18);
            editorModeButton.Font = new System.Drawing.Font("consolas", 7);
            editorModeButton.TabIndex = 3;
            editorModeButton.Click += editorModeButtonClick;
            editorModeButton.TextAlign = System.Drawing.ContentAlignment.TopLeft;

            //updateEditorButton();

            this.Controls.Add(editorModeButton);



            
            this.Refresh();

            InitializeComponent();

//            this.ResumeLayout(false);
        }
        #endregion

        #region MapRegion
        public void RemoveCanvas(MapCanvas canvas)
        {
            if (glPanel == null) return;
            if (glPanel.Controls.Contains(canvas))
            {
                glPanel.Controls.Remove(canvas);
            }
        }

        public Position getPosition()
        {
            return new Position(bottomScrollBar.Value / 32,
                leftScrollBar.Value / 32, MapZ);
        }

        public void UpdateCanvas(MapCanvas canvas)
        {
            this.canvas = canvas;
            if (glPanel == null) CreateComponents();
            glPanel.Controls.Add(canvas);
            canvas.BringToFront();
            canvas.Refresh();
        }


        public void UpdateScrollBar()
        {
            leftScrollBar.Minimum = 0;
            leftScrollBar.Maximum = (gameMap.Height * 32);

            bottomScrollBar.Minimum = 0;
            bottomScrollBar.Maximum = (gameMap.Width * 32);
        }

        public void UpdatePosScrollBar(int x, int y)
        {
            if (((bottomScrollBar.Value + x) < 0) ||
               ((leftScrollBar.Value + y) < 0))
            {
                return;
            }
            bottomScrollBar.Value = bottomScrollBar.Value + x;
            leftScrollBar.Value = leftScrollBar.Value + y;
        }

        public int getViewX()
        {
            return bottomScrollBar.Value;
        }

        public int getViewY()
        {
            return leftScrollBar.Value;
        }

        private void leftScrollBarScroll(object sender, ScrollEventArgs e)
        {            
            canvas.Refresh();            
        }

        private void bottomScrollBarScroll(object sender, ScrollEventArgs e)
        {            
            canvas.Refresh();
        }

        private UInt16 getId()
        {
            return ++genId;
        }

        private void MapViewer_Resize(object sender, EventArgs e)
        {
            if (leftScrollBar != null)
            {
                leftScrollBar.Location = new System.Drawing.Point(Size.Width - 17, 0);
                leftScrollBar.Size = new System.Drawing.Size(17, Size.Height - 17);

                bottomScrollBar.Location = new System.Drawing.Point(0, Size.Height - 17);
                bottomScrollBar.Size = new System.Drawing.Size(Size.Width - 17, 17);


                UpdateScrollBar();
            }
            
        }
        #endregion

        #region DrawRegion
        private Brush getSelectedBrush()
        {
            return EditorPalette.getSelectedPalette().GetSelectedBrush();
        }

        public void DrawSelection()
        {
            Position mousePos = canvas.getMousePosition();

            if (getSelectedBrush() as WallBrush != null)
            {
                List<Position> tilestodraw = new List<Position>();
                List<Position> tilestoborder = new List<Position>();

                int start_map_x = mousePos.X - EditorPalette.GetBrushSize();
                int start_map_y = mousePos.Y - EditorPalette.GetBrushSize();
                int end_map_x = mousePos.X + EditorPalette.GetBrushSize();
                int end_map_y = mousePos.Y + EditorPalette.GetBrushSize();

                for (int y = start_map_y - 1; y <= end_map_y + 1; ++y)
                {
                    for (int x = start_map_x - 1; x <= end_map_x + 1; ++x)
                    {
                        if (
                          (x <= start_map_x + 1 || x >= end_map_x - 1) ||
                          (y <= start_map_y + 1 || y >= end_map_y - 1)
                          )
                        {
                            tilestoborder.Add(new Position(x, y, mousePos.Z));
                        }
                        if (
                                (
                                    (x == start_map_x || x == end_map_x) ||
                                    (y == start_map_y || y == end_map_y)
                                ) && (
                                    (x >= start_map_x && x <= end_map_x) &&
                                    (y >= start_map_y && y <= end_map_y)
                                )
                            )
                        {
                            tilestodraw.Add(new Position(x, y, mousePos.Z));
                        }
                    }
                }
				if(canvas.ctrlActive ) {
					this.Draw(tilestodraw, tilestoborder, canvas.altActive,false);
				} else {
                    this.Draw(tilestodraw, tilestoborder, canvas.altActive, true);
				}
            }
            else if (getSelectedBrush() as DoorBrush != null)
            {
                List<Position> tilestodraw = new List<Position>();
                List<Position> tilestoborder = new List<Position>();

				tilestodraw.Add(new Position(mousePos.X, mousePos.Y, mousePos.Z));

				tilestoborder.Add(new Position(mousePos.X    , mousePos.Y - 1, mousePos.Z));
				tilestoborder.Add(new Position(mousePos.X - 1, mousePos.Y    , mousePos.Z));
				tilestoborder.Add(new Position(mousePos.X    , mousePos.Y + 1, mousePos.Z));
				tilestoborder.Add(new Position(mousePos.X + 1, mousePos.Y    , mousePos.Z));

				if(canvas.ctrlActive ) {					
                    this.Draw(tilestodraw, tilestoborder, canvas.altActive,false);
				} else {
                    this.Draw(tilestodraw, tilestoborder, canvas.altActive, true);
				}
            }
            else if ((getSelectedBrush() as DoodadBrush != null)  || (getSelectedBrush() as SpawnBrush != null) || (getSelectedBrush() as CreatureBrush != null))
            {
				if(canvas.ctrlActive ) {
					if (getSelectedBrush() as DoodadBrush != null) {
						List<Position> tilestodraw = new List<Position>();
						canvas.getTilesToDraw(mousePos, tilestodraw, null);
						this.Draw(tilestodraw, canvas.altActive,false);
					} else {
						this.Draw(new Position(mousePos.X, mousePos.Y, mousePos.Z), (canvas.shiftActive || canvas.altActive) , false);
					}
				} else {
                    this.Draw(new Position(mousePos.X, mousePos.Y, mousePos.Z), (canvas.shiftActive || canvas.altActive) ,  true);					
				}
            }
            else
            {
                GroundBrush gbrush = getSelectedBrush() as GroundBrush;

                if (gbrush != null && canvas.altActive)
                {
					replace_dragging = true;
                    Tile draw_tile = gameMap.getTile(mousePos);
					if(draw_tile != null) {
						replace_brush = draw_tile.getGroundBrush();
					} else {
						replace_brush = null;
					}
				}

				if(getSelectedBrush().needBorders()) {
                    List<Position> tilestodraw = new List<Position>();
                    List<Position> tilestoborder = new List<Position>();
					
					canvas.getTilesToDraw(mousePos, tilestodraw, tilestoborder);

                    if (canvas.ctrlActive)
                    {
						Draw(tilestodraw, tilestoborder, canvas.altActive, false);
					} 
                    else {						
                        Draw(tilestodraw, tilestoborder, canvas.altActive, true);
					}
                }
                else if (getSelectedBrush().oneSizeFitsAll())
                {
                    if ((getSelectedBrush() as HouseExitBrush != null) ||
                        (getSelectedBrush() as WaypointBrush != null) ||
                        (getSelectedBrush() as TemplePositionBrush != null))
                    {
                        Draw(new Position(mousePos.X, mousePos.Y, mousePos.Z), canvas.altActive, true);
                    }
                    else
                    {
                        List<Position> tilestodraw = new List<Position>();
                        tilestodraw.Add(new Position(mousePos.X, mousePos.Y, mousePos.Z));
                        if (canvas.ctrlActive)
                        {
                            Draw(tilestodraw, canvas.altActive, false);
                        }
                        else
                        {
                            Draw(tilestodraw, canvas.altActive, true);
                        }
                    }
                }
                else
                {
                    List<Position> tilestodraw = new List<Position>();									
                    canvas.getTilesToDraw(mousePos, tilestodraw, null);

					if (canvas.ctrlActive)
                    {
						Draw(tilestodraw, canvas.altActive, false);
					} 
                    else 
                    {
						Draw(tilestodraw, canvas.altActive, true);
					}
                }


            }           
        }

        private void removeDuplicateWalls(Tile buffer, Tile tile)
        {
            foreach(Item item in buffer.Items){            
                if (item.getWallBrush() != null)
                {
                    tile.cleanWalls(item.getWallBrush());
                }
            }
        }

        private void doSurroundingBorders(DoodadBrush doodad_brush, List<Position> tilestoborder, Tile buffer_tile, Tile new_tile)
        {
            if (doodad_brush.doNewBorders() && Settings.GetBoolean(Key.USE_AUTOMAGIC))
            {
                tilestoborder.Add(new Position(new_tile.getPosition().x, new_tile.getPosition().y, new_tile.getPosition().z));
                if (buffer_tile.hasGround())
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        for (int x = -1; x <= 1; x++)
                        {
                            tilestoborder.Add(new  Position(new_tile.getPosition().x + x, new_tile.getPosition().y + y, new_tile.getPosition().z));
                        }
                    }
                }
                else if (buffer_tile.hasWall())
                {
                    tilestoborder.Add(new Position(new_tile.getPosition().x, new_tile.getPosition().y - 1, new_tile.getPosition().z));
                    tilestoborder.Add(new Position(new_tile.getPosition().x - 1, new_tile.getPosition().y, new_tile.getPosition().z));
                    tilestoborder.Add(new Position(new_tile.getPosition().x + 1, new_tile.getPosition().y, new_tile.getPosition().z));
                    tilestoborder.Add(new Position(new_tile.getPosition().x, new_tile.getPosition().y + 1, new_tile.getPosition().z));
                }
            }
        }

        private void Draw(Position offset, bool alt, bool dodraw)
        {
            DoodadBrush doodad_brush = getSelectedBrush() as DoodadBrush;
            SpawnBrush spawn_brush = getSelectedBrush() as SpawnBrush;
            CreatureBrush creature_brush = getSelectedBrush() as CreatureBrush;
            Brush brush = EditorPalette.getSelectedPalette().GetSelectedBrush();

            BatchAction batch = new BatchAction(ActionIdentifier.ACTION_DRAW);


            if (doodad_brush != null)
            {
                Action action = new Action(this);
                GameMap buffer_map =  canvas.doodad_buffer_map;
                canvas.secondary_map = null;

                Position delta_pos = (offset - new Position(0x8000, 0x8000, 0x8));
                List<Position> tilestoborder = new List<Position>();

                
                foreach (Tile buffer_tile in buffer_map.getTiles().Values)
                {                    
                    Position pos = (buffer_tile.getPosition() + delta_pos);
                    if (pos.isValid() == false)
                    {
                        continue;
                    }

                    Tile tile = map.getTile(pos);

                    if (doodad_brush.placeOnBlocking() || alt)
                    {
                        if (tile != null)
                        {
                            bool place = true;
                            if (!doodad_brush.placeOnDuplicate() && !alt)
                            {
                                foreach(Item item in tile.Items)                                
                                {
                                    if (doodad_brush.ownsItem(item))
                                    {
                                        place = false;
                                        break;
                                    }
                                }
                            }
                            if (place)
                            {
                                Tile new_tile = tile.deepCopy();
                                removeDuplicateWalls(buffer_tile, new_tile);
                                doSurroundingBorders(doodad_brush, tilestoborder, buffer_tile, new_tile);
                                new_tile.merge(buffer_tile);
                                action.addChange(new Change(new_tile));
                            }
                        }
                        else
                        {
                            Tile new_tile = new Tile(pos);
                            removeDuplicateWalls(buffer_tile, new_tile);
                            doSurroundingBorders(doodad_brush, tilestoborder, buffer_tile, new_tile);
                            new_tile.merge(buffer_tile);
                            action.addChange(new Change(new_tile));
                        }
                    }
                    else
                    {
                        if (tile != null && tile.isBlocking() == false)
                        {
                            bool place = true;
                            if (!doodad_brush.placeOnDuplicate() && !alt)
                            {
                             foreach(Item item in tile.Items)                                
                                {
                                    if (doodad_brush.ownsItem(item))
                                    {
                                        place = false;
                                        break;
                                    }
                                }
                            }
                            if (place)
                            {
                                Tile new_tile = tile.deepCopy();
                                removeDuplicateWalls(buffer_tile, new_tile);
                                doSurroundingBorders(doodad_brush, tilestoborder, buffer_tile, new_tile);
                                new_tile.merge(buffer_tile);
                                action.addChange(new Change(new_tile));
                            }
                        }
                    }
                }
                batch.addAndCommitAction(action);

                if (tilestoborder.Count() > 0)
                {
                    action = new Action(this);

                    // Remove duplicates
                    // tilestoborder.sort();
                    //tilestoborder.unique();
                    
                    foreach(Position pos in tilestoborder)
                    {
                        Tile t = map.getTile(pos);
                        if (t != null)
                        {
                            Tile new_tile = t.deepCopy();
                            new_tile.borderize(map);
                            new_tile.wallize(map);
                            action.addChange(new Change(new_tile));
                        }
                    }

                    batch.addAndCommitAction(action);
                }
                Global.mapCanvas.secondary_map = null;
                DoodadBrush.FillDoodadPreviewBuffer();
                Global.mapCanvas.secondary_map = Global.mapCanvas.doodad_buffer_map;


            }
            else if (spawn_brush != null || creature_brush != null)
            {
                Action action = new Action(this);

                Tile t = map.getTile(offset);
                Tile new_tile = null;
                if (t != null)
                {
                    new_tile = t.deepCopy();
                }
                else
                {
                    new_tile = new Tile(offset);
                }
                int param;
                if (creature_brush != null)
                {
                    param = EditorPalette.getSelectedPalette().GetSpawnTime();
                }
                else
                {
                    param = EditorPalette.GetBrushSize();
                }
                if (dodraw)
                {
                    brush.draw(map, new_tile, param);
                }
                else
                {
                    brush.undraw(map, new_tile);
                }
                action.addChange(new Change(new_tile));
                batch.addAndCommitAction(action);
            }

            queue.addBatch(batch, 2);
                
        }

        private void Draw(List<Position> tilestodraw, bool alt, bool dodraw)
        {
            Brush brush = EditorPalette.getSelectedPalette().GetSelectedBrush();
            OptionalBorderBrush gravel_brush = brush as OptionalBorderBrush;


            GroundBrush border_brush = brush as GroundBrush;
            WallBrush wall_brush = brush as WallBrush;

            if (border_brush != null || wall_brush != null)
            {
                // Wrong function, end call
                return;
            }

            Action action = new Action(this);

            if (gravel_brush != null)
            {
                foreach (Position pos in tilestodraw)
                {
                    Tile tile = map.getTile(pos.x, pos.y, pos.z);

                    if (tile != null)
                    {
                        if (dodraw)
                        {
                            Tile new_tile = tile.deepCopy();
                            brush.draw(map, new_tile);
                            new_tile.borderize(map);
                            action.addChange(new Change(new_tile));
                        }
                        else if (dodraw == false && tile.hasOptionalBorder())
                        {
                            Tile new_tile = tile.deepCopy();
                            brush.undraw(map, new_tile);
                            new_tile.borderize(map);
                            action.addChange(new Change(new_tile));
                        }
                    }
                    else if (dodraw)
                    {
                        Tile new_tile = new Tile(pos);
                        brush.draw(map, new_tile);
                        new_tile.borderize(map);
                        if (new_tile.size() == 0)
                        {
                            new_tile = null;
                            continue;
                        }
                        action.addChange(new Change(new_tile));
                    }
                }
            }
            else
            {
                foreach (Position pos in tilestodraw)
                {
                    Tile tile = map.getTile(pos.x, pos.y, pos.z);

                    if (tile != null)
                    {
                        Tile new_tile = tile.deepCopy();
                        if (dodraw)
                        {
                            brush.draw(map, new_tile, alt);
                        }
                        else
                        {
                            brush.undraw(map, new_tile);
                        }
                        action.addChange(new Change(new_tile));
                    }
                    else if (dodraw)
                    {
                        Tile new_tile = new Tile(pos);
                        brush.draw(map, new_tile, alt);
                        action.addChange(new Change(new_tile));
                    }
                }
            }
            queue.addAction(ActionIdentifier.ACTION_DRAW, action, 2);            
        }

        private void Draw(List<Position> tilestodraw, List<Position> tilestoborder, bool alt, bool dodraw)
        {           
            GroundBrush border_brush = getSelectedBrush() as GroundBrush;
            WallBrush wall_brush = getSelectedBrush() as WallBrush;
            DoorBrush door_brush = getSelectedBrush() as DoorBrush;
            TableBrush table_brush = getSelectedBrush() as TableBrush;
            CarpetBrush carpet_brush = getSelectedBrush() as CarpetBrush;
            EraserBrush eraser = getSelectedBrush() as EraserBrush;

            if (border_brush != null || eraser != null)
            {                
                BatchAction batch = new BatchAction(ActionIdentifier.ACTION_DRAW);
                Action action = new Action(this);

                foreach (Position posToDraw in tilestodraw)
                {
                    Tile tile = gameMap.getTile(posToDraw);
                    if (tile != null)
                    {
                        Tile newTile = tile.deepCopy();
                        if (Settings.GetBoolean(Key.USE_AUTOMAGIC))
                        {
                            newTile.clearBorders();
                        }

                        if (dodraw)
                        {
                            if (border_brush != null && alt)
                            {
                                Pair<Boolean, GroundBrush> param = new Pair<bool, GroundBrush>();
                                if (replace_brush != null)
                                {
                                    param.first = false;
                                    param.second = replace_brush;
                                }
                                else
                                {
                                    param.first = true;
                                    param.second = null;
                                }
                                getSelectedBrush().draw(gameMap, newTile, param);
                            }
                            else
                            {
                                getSelectedBrush().draw(gameMap, newTile, null);
                            }
                        }
                        else
                        {
                            getSelectedBrush().undraw(gameMap, newTile);
                        }                        
                        action.addChange(new Change(newTile));

                    }
                    else if (dodraw)
                    {
                        Tile newTile = new Tile(posToDraw);
                        border_brush.draw(gameMap, newTile, null);
                        action.addChange(new Change(newTile));
                    }
                }
                batch.addAction(action);
                batch.commitActions();


                if (Settings.GetBoolean(Key.USE_AUTOMAGIC))
                {
                    Action borderAction = new Action(this);
                    foreach (Position posToborder in tilestoborder)
                    {
                        Tile tile = gameMap.getTile(posToborder);
                        if (tile != null)
                        {
                            Tile newTile = tile.deepCopy();
                            if (eraser != null)
                            {
                                newTile.wallize(gameMap);
                                newTile.tableize(gameMap);
                                newTile.carpetize(gameMap);
                            }
                            newTile.borderize(gameMap);
                            borderAction.addChange(new Change(newTile));
                        }
                        else
                        {
                            Tile newTile = new Tile(posToborder);
                            newTile.borderize(gameMap);
                            if (newTile.Items.Count > 0)
                            {
                                borderAction.addChange(new Change(newTile));
                            }
                        }
                    }
                    batch.addAction(borderAction);
                    batch.commitActions();
                }
                queue.addBatch(batch, 2);
            }
            else if (table_brush != null || carpet_brush != null)
            {
                BatchAction batch = new BatchAction(ActionIdentifier.ACTION_DRAW);
                Action action = new Action(this);
                
                foreach(Position pos in tilestodraw)    
                {
                    Tile tile = map.getTile(pos.x, pos.y, pos.z);

                    if (tile != null)
                    {
                        Tile new_tile = tile.deepCopy();
                        if (dodraw)
                        {
                            getSelectedBrush().draw(map, new_tile, null);
                        }
                        else
                        {
                            getSelectedBrush().undraw(map, new_tile);
                        }
                        action.addChange(new Change(new_tile));
                    }
                    else if (dodraw)
                    {
                        Tile new_tile = new Tile(pos);
                        getSelectedBrush().draw(map, new_tile, null);
                        action.addChange(new Change(new_tile));
                    }
                }

                // Commit changes to map
                batch.addAndCommitAction(action);

                // Do borders!
                action = new Action(this);                
                foreach(Position pos in tilestoborder)   
                {
                    Tile tile = map.getTile(pos.x, pos.y, pos.z);
                    if (table_brush != null)
                    {
                        if (tile != null && tile.hasTable())
                        {
                            Tile new_tile = tile.deepCopy();
                            new_tile.tableize(map);
                            action.addChange(new Change(new_tile));
                        }
                    }
                    else if (carpet_brush != null)
                    {
                        if (tile != null && tile.hasCarpet())
                        {
                            Tile new_tile = tile.deepCopy();
                            new_tile.carpetize(map);
                            action.addChange(new Change(new_tile));
                        }
                    }
                }
                batch.addAndCommitAction(action);

                queue.addBatch(batch, 2);
            }
            else if (wall_brush != null)
            {
                BatchAction batch = new BatchAction(ActionIdentifier.ACTION_DRAW);
                Action action = new Action(this);
                if (alt && dodraw)
                {
                    //gui.doodad_buffer_map->clear();
                    GameMap draw_map = new GameMap();
                    foreach (Position pos in tilestodraw)
                    {
                        Tile tile = gameMap.getTile(pos);
                        if (tile != null)
                        {
                            Tile new_tile = tile.deepCopy();
                            new_tile.cleanWalls(wall_brush);
                            getSelectedBrush().draw(draw_map, new_tile);
                            draw_map.setTile(pos, new_tile, true);
                        }
                        else if (dodraw)
                        {
                            Tile new_tile = new Tile(pos);
                            getSelectedBrush().draw(draw_map, new_tile);
                            draw_map.setTile(pos, new_tile, true);
                        }
                    }
                    foreach (Position pos in tilestodraw)
                    {
                        Tile tile = draw_map.getTile(pos);
                        if (tile != null)
                        {
                            tile.wallize(draw_map);
                            action.addChange(new Change(tile));
                        }
                    }                    
                    batch.addAction(action);
                    batch.commitActions();

                }
                else
                {
                    foreach (Position pos in tilestodraw)
                    {
                        Tile tile = gameMap.getTile(pos.x, pos.y, pos.z);
                        if (tile != null)
                        {
                            Tile new_tile = tile.deepCopy();
                            // Wall cleaning is exempt from automagic
                            new_tile.cleanWalls(wall_brush);
                            if (dodraw)
                                getSelectedBrush().draw(gameMap, new_tile);
                            else
                                getSelectedBrush().undraw(gameMap, new_tile);
                            action.addChange(new Change(new_tile));
                        }
                        else if (dodraw)
                        {
                            Tile new_tile = new Tile(pos);
                            getSelectedBrush().draw(gameMap, new_tile);
                            action.addChange(new Change(new_tile));
                        }

                    }
                    batch.addAction(action);
                    batch.commitActions();
                    if (Settings.GetBoolean(Key.USE_AUTOMAGIC))
                    {
                        action = new Action(this);
                        foreach (Position pos in tilestoborder)
                        {
                            Tile tile = gameMap.getTile(pos.x, pos.y, pos.z);

                            if (tile != null)
                            {
                                Tile new_tile = tile.deepCopy();
                                new_tile.wallize(gameMap);                                
                                action.addChange(new Change(new_tile));
                            }
                        }
                        batch.addAction(action);
                        batch.commitActions();
                    }
                }
                queue.addBatch(batch, 2);
            }
            else if (door_brush != null)
            {
                BatchAction batch = new BatchAction(ActionIdentifier.ACTION_DRAW);
                Action action = new Action(this);

                // Loop is kind of redundant since there will only ever be one index.
                foreach(Position pos in tilestodraw)                
                {                    
                    Tile tile = map.getTile(pos.x, pos.y, pos.z);
                    if (tile != null)
                    {
                        Tile new_tile = tile.deepCopy();
                        // Wall cleaning is exempt from automagic                        
                        if (dodraw)
                        {
                            door_brush.draw(map, new_tile, alt);
                        }
                        else
                        {
                            door_brush.undraw(map, new_tile);
                        }
                        //new_tile.cleanWalls(wall_brush);
                        action.addChange(new Change(new_tile));
                    }
                    else if (dodraw)
                    {
                        Tile new_tile = new Tile(pos);
                        door_brush.draw(map, new_tile, alt);
                        action.addChange(new Change(new_tile));
                    }
                }

                // Commit changes to map
                batch.addAndCommitAction(action);

                //if (Settings.GetBoolean(Key.USE_AUTOMAGIC))
                if (false)
                {
                    // Do borders!
                    action = new Action(this);
                    foreach (Position pos in tilestoborder)                     
                    {                    
                        Tile tile = map.getTile(pos.x, pos.y, pos.z);

                        if (tile != null)
                        {
                            Tile new_tile = tile.deepCopy();
                            new_tile.wallize(map);
                            //if(*tile == *new_tile) delete new_tile;
                            action.addChange(new Change(new_tile));
                        }
                    }
                    batch.addAndCommitAction(action);
                }

                addBatch(batch, 2);                
            }
            else
            {
                Action action = new Action(this);
                foreach (Position pos in tilestodraw)
                {
                    Tile tile = gameMap.getTile(pos.x, pos.y, pos.z);

                    if (tile != null)
                    {
                        Tile new_tile = tile.deepCopy();
                        if (dodraw)
                            getSelectedBrush().draw(gameMap, new_tile,null);
                        else
                            getSelectedBrush().undraw(gameMap, new_tile);
                        action.addChange(new Change(new_tile));
                    }
                    else if (dodraw)
                    {
                        Tile new_tile = new Tile(pos);
                        getSelectedBrush().draw(gameMap, new_tile,null);
                        action.addChange(new Change(new_tile));
                    }
                }
                queue.addAction(ActionIdentifier.ACTION_DRAW, action, 2);
            }
        }


        public void addBatch(BatchAction batchAction, int stacking_delay = 0)
        {
            queue.addBatch(batchAction, stacking_delay);
        }

        public void moveSelection(Position offset)
        {
            BatchAction batchAction = new BatchAction(ActionIdentifier.ACTION_MOVE); // Our saved action batch, for undo!
            Action action;
            // Remove tiles from the map
            action = new Action(this); // Our action!
            bool doborders = false;
            List<Tile> tmp_storage = new List<Tile>();
            // Update the tiles with the new positions
            foreach(Tile tile in selection.getTiles())
            {
                // First we get the old tile and it's position               
                Position pos = tile.getPosition();

                // Create the duplicate source tile, which will replace the old one later
                Tile old_src_tile = tile;
                Tile new_src_tile;

                new_src_tile = old_src_tile.deepCopy();

                Tile tmp_storage_tile = new Tile(pos);

                // Get all the selected items from the NEW source tile and iterate through them
                // This transfers ownership to the temporary tile
                List<Item> tile_selection = new_src_tile.popSelectedItems();
                foreach(Item item in tile_selection)
                {
                    // Add the copied item to the new destination tile,                    
                    tmp_storage_tile.addItem(item);
                }
                // Move spawns
                if ((new_src_tile.spawn != null) && new_src_tile.spawn.isSelected())
                {
                    tmp_storage_tile.spawn = new_src_tile.spawn;
                    new_src_tile.spawn = null;
                }
                // Move creatures
                if ((new_src_tile.creature != null) && new_src_tile.creature.isSelected())
                {
                    tmp_storage_tile.creature = new_src_tile.creature;
                    new_src_tile.creature = null;
                }
                // Move house data & tile status if ground is transferred
                if (tmp_storage_tile.Ground != null)
                {
                    tmp_storage_tile.house_id = new_src_tile.house_id;
                    new_src_tile.house_id = 0;
                    tmp_storage_tile.setMapFlags(new_src_tile.getMapFlags());
                    new_src_tile.setMapFlags(TileState.TILESTATE_NONE);
                    doborders = true;
                }
                tmp_storage.Add(tmp_storage_tile);
                // Add the tile copy to the action
                action.addChange(new Change(new_src_tile));
            }
            batchAction.addAndCommitAction(action);

            // Remove old borders (and create some new?)
            if (Settings.GetBoolean(Key.USE_AUTOMAGIC) && Settings.GetBoolean(Key.BORDERIZE_DRAG) && selection.size() < Settings.GetInteger(Key.BORDERIZE_DRAG_THRESHOLD))
            {
                action = new Action(this);
                List<Tile> borderize_tiles = new List<Tile>();
                // Go through all modified (selected) tiles (might be slow)
                foreach(Tile it in tmp_storage)                
                {
                    Position pos = it.getPosition();
                    // Go through all neighbours
                    Tile t;
                    t = map.getTile(pos.x, pos.y, pos.z);
                    if (t != null && !t.isSelected())
                    {
                        borderize_tiles.Add(t);
                    }
                    t = map.getTile(pos.x - 1, pos.y - 1, pos.z);
                    if (t != null && !t.isSelected())
                    {
                        borderize_tiles.Add(t);
                    }
                    t = map.getTile(pos.x, pos.y - 1, pos.z);
                    if (t != null && !t.isSelected())
                    {
                        borderize_tiles.Add(t);
                    }
                    t = map.getTile(pos.x + 1, pos.y - 1, pos.z);
                    if (t != null && !t.isSelected())
                    {
                        borderize_tiles.Add(t);
                    }
                    t = map.getTile(pos.x - 1, pos.y, pos.z);
                    if (t != null && !t.isSelected())
                    {
                        borderize_tiles.Add(t);
                    }
                    t = map.getTile(pos.x + 1, pos.y, pos.z);
                    if (t != null && !t.isSelected())
                    {
                        borderize_tiles.Add(t);
                    }
                    t = map.getTile(pos.x - 1, pos.y + 1, pos.z);
                    if (t != null && !t.isSelected())
                    {
                        borderize_tiles.Add(t);
                    }
                    t = map.getTile(pos.x, pos.y + 1, pos.z);
                    if (t != null && !t.isSelected())
                    {
                        borderize_tiles.Add(t);
                    }
                    t = map.getTile(pos.x + 1, pos.y + 1, pos.z);
                    if (t != null && !t.isSelected())
                    {
                        borderize_tiles.Add(t);
                    }
                }
                // Remove duplicates
            //    borderize_tiles.sort();
                //borderize_tiles.unique();
                // Do le borders!

                foreach(Tile tile in borderize_tiles)                
                {                    
                    Tile new_tile = tile.deepCopy();
                    if (doborders)
                    {
                        new_tile.borderize(map);
                    }
                    new_tile.wallize(map);
                    new_tile.tableize(map);
                    new_tile.carpetize(map);
                    if ((tile.Ground != null) && tile.Ground.isSelected())
                    {
                        new_tile.selectGround();
                    }
                    action.addChange(new Change(new_tile));
                }
                // Commit changes to map
                batchAction.addAndCommitAction(action);

                // New action for adding the destination tiles
                action = new Action(this);
                List<Tile> tilestoRemove = new List<Tile>();
                foreach(Tile tile in tmp_storage)                
                {                    
                    Position old_pos = tile.getPosition();
                    Position new_pos = new Position();

                    new_pos = old_pos - offset;

                    if (new_pos.z < 0 && new_pos.z > 15)
                    {                    
                        tilestoRemove.Add(tile);                        
                        continue;
                    }
                    // Create the duplicate dest tile, which will replace the old one later
                    Tile old_dest_tile = map.getTile(new_pos);
                    Tile new_dest_tile = null;

                    if (Settings.GetBoolean(Key.MERGE_MOVE) || (tile.Ground == null))
                    {
                        // Move items
                        if (old_dest_tile != null)
                        {
                            new_dest_tile = old_dest_tile.deepCopy();
                        }
                        else
                        {
                            new_dest_tile = new Tile(new_pos);
                        }
                        new_dest_tile.merge(tile);
                        tilestoRemove.Add(tile);
                    }
                    else
                    {
                        // Replace tile instead of just merge
                        tile.Position = new_pos;
                        new_dest_tile = tile;
                    }
                    action.addChange(new Change(new_dest_tile));
                }
                foreach (Tile tile in tilestoRemove)
                {
                    tmp_storage.Remove(tile);
                }
                // Commit changes to the map
                batchAction.addAndCommitAction(action);              
            }

            // Create borders
            if (Settings.GetBoolean(Key.USE_AUTOMAGIC) && Settings.GetBoolean(Key.BORDERIZE_DRAG) && selection.size() < Settings.GetInteger(Key.BORDERIZE_DRAG_THRESHOLD))
            {
                action = new Action(this);
                List<Tile> borderize_tiles = new List<Tile>();
                // Go through all modified (selected) tiles (might be slow)                    
                foreach (Tile it in selection.getTiles())
                {
                    bool add_me = false; // If this tile is touched
                    Position pos = it.getPosition();
                    // Go through all neighbours
                    Tile t;
                    t = map.getTile(pos.x - 1, pos.y - 1, pos.z);
                    if (t != null && !t.isSelected())
                    {
                        borderize_tiles.Add(t);
                        add_me = true;
                    }
                    t = map.getTile(pos.x - 1, pos.y - 1, pos.z);
                    if (t != null && !t.isSelected())
                    {
                        borderize_tiles.Add(t);
                        add_me = true;
                    }
                    t = map.getTile(pos.x, pos.y - 1, pos.z);
                    if (t != null && !t.isSelected())
                    {
                        borderize_tiles.Add(t);
                        add_me = true;
                    }
                    t = map.getTile(pos.x + 1, pos.y - 1, pos.z);
                    if (t != null && !t.isSelected())
                    {
                        borderize_tiles.Add(t);
                        add_me = true;
                    }
                    t = map.getTile(pos.x - 1, pos.y, pos.z);
                    if (t != null && !t.isSelected())
                    {
                        borderize_tiles.Add(t);
                        add_me = true;
                    }
                    t = map.getTile(pos.x + 1, pos.y, pos.z);
                    if (t != null && !t.isSelected())
                    {
                        borderize_tiles.Add(t);
                        add_me = true;
                    }
                    t = map.getTile(pos.x - 1, pos.y + 1, pos.z);
                    if (t != null && !t.isSelected())
                    {
                        borderize_tiles.Add(t);
                        add_me = true;
                    }
                    t = map.getTile(pos.x, pos.y + 1, pos.z);
                    if (t != null && !t.isSelected())
                    {
                        borderize_tiles.Add(t);
                        add_me = true;
                    }
                    t = map.getTile(pos.x + 1, pos.y + 1, pos.z);
                    if (t != null && !t.isSelected())
                    {
                        borderize_tiles.Add(t);
                        add_me = true;
                    }
                    if (add_me)
                    {
                        borderize_tiles.Add(it);
                    }
                }
                // Remove duplicates
                //borderize_tiles.sort();
                //borderize_tiles.unique();
                // Do le borders!
                foreach(Tile tile in borderize_tiles)                
                {                    
                    if (tile.Ground != null)
                    {
                        if (tile.Ground.getGroundBrush() != null)
                        {
                            Tile new_tile = tile.deepCopy();
                            if (doborders)
                            {
                                new_tile.borderize(map);
                            }
                            new_tile.wallize(map);
                            new_tile.tableize(map);
                            new_tile.carpetize(map);
                            if (tile.Ground.isSelected())
                            {
                                new_tile.selectGround();
                            }
                            action.addChange(new Change(new_tile));
                        }
                    }
                }
                // Commit changes to map
                batchAction.addAndCommitAction(action);
            }
            // Store the action for undo
            addBatch(batchAction);
            selection.updateSelectionCount();
        }




        public void destroySelection()
        {
            if (selection.size() == 0)
            {
                //gui.SetStatusText(wxT("No selected items to delete."));
            }
            else
            {
                int tile_count = 0;
                int item_count = 0;
                List<Position> tilestoborder = new List<Position>();

                BatchAction batch = new BatchAction(ActionIdentifier.ACTION_DELETE_TILES);
                Action action = new Action(this);
                
                foreach(Tile tile in selection.getTiles())
                {
                    tile_count++;

                    Tile newtile = tile.deepCopy();

                    List<Item> tile_selection = newtile.popSelectedItems();
                    List<Item> itemToRemove = new List<Item>();
                    foreach(Item it in tile_selection)
                    {
                        ++item_count;
                        itemToRemove.Add(it);
                    }

                    foreach (Item it in itemToRemove)
                    {
                        tile.Items.Remove(it);
                    }

                    if ((newtile.creature != null) && newtile.creature.isSelected())
                    {
                        newtile.creature = null;                       
                    }

                    if ((newtile.spawn != null) && newtile.spawn.isSelected())
                    {                        
                        newtile.spawn = null;
                    }

                    if (Settings.GetBoolean(Key.USE_AUTOMAGIC))
                    {
                        for (int y = -1; y <= 1; y++)
                        {
                            for (int x = -1; x <= 1; x++)
                            {
                                tilestoborder.Add(new Position(tile.getPosition().x + x, tile.getPosition().y + y, tile.getPosition().z));
                            }
                        }
                    }
                    action.addChange(new Change(newtile));
                }

                batch.addAndCommitAction(action);

                if (Settings.GetBoolean(Key.USE_AUTOMAGIC))
                {
                    // Remove duplicates
               //     tilestoborder.sort();
                    //tilestoborder.unique();

                    action = new Action(this);                    
                    foreach(Position pos in tilestoborder)                      
                    {
                        Tile tile = map.getTile(pos);
                        if (tile != null)
                        {
                            Tile new_tile = tile.deepCopy();
                            new_tile.borderize(map);
                            new_tile.wallize(map);
                            new_tile.tableize(map);
                            new_tile.carpetize(map);
                            action.addChange(new Change(new_tile));
                        }
                        else
                        {
                            Tile new_tile = new Tile(pos);
                            new_tile.borderize(map);
                            if (new_tile.size() != 0)
                            {
                                action.addChange(new Change(new_tile));
                            }
                            else
                            {
                                new_tile = null;
                            }
                        }
                    }
                    batch.addAndCommitAction(action);
                }

                addBatch(batch);
            }
        }


        public void Undo()
        {
            queue.undo();
            canvas.Refresh();
        }

        public void Redo()
        {
            queue.redo();
            canvas.Refresh();
        }

        public bool MapReadOnly()
        {
            return gameMap.ReadOnly;
        }

        public void Save()
        {
            if ("".Equals(gameMap.FileName))
            {
                gameMap.FileName = Generic.SaveMapFile();
                if ("".Equals(gameMap.FileName))
                {
                    canvas.Focus();
                    return;
                }
            }

            gameMap.Save(gameMap.FileName);
            Title = gameMap.MapName;
            canvas.Focus();
        }

        public void SaveAs()
        {
            gameMap.Save(Generic.SaveMapFile());
            Title = gameMap.MapName;
        }

        #endregion

        #region SelectionModes;
        public void setSelectionMode()
        {
            Global.editorMode = EditorMode.Selection;
            DoodadBrush brush = EditorPalette.getSelectedPalette().GetSelectedBrush() as DoodadBrush;
            if (brush != null)
            {
                canvas.secondary_map = null;
            }
        }

        public void setEditionMode()
        {
            Global.editorMode = EditorMode.Edition;
            selection.clear();
            DoodadBrush brush = EditorPalette.getSelectedPalette().GetSelectedBrush() as DoodadBrush;
            if (brush != null)
            {
                canvas.secondary_map = canvas.doodad_buffer_map;
            }
            else
            {
                canvas.secondary_map = null;
            }
        }

        private void editorModeButtonClick(object sender, EventArgs e)
        {
            revertEditionMode();
            canvas.Refresh();
        }

        public void revertEditionMode()
        {
            if (isSelectionMode())
            {
                setEditionMode();
            } 
            else if (isEditionMode())
            {
                setSelectionMode();
            }
        }

        public bool isSelectionMode()
        {
            return (Global.editorMode == EditorMode.Selection);
        }

        public bool isEditionMode()
        {
            return (Global.editorMode == EditorMode.Edition);
        }
        #endregion

        #region OnlineMode

        public static object lockRefresh = new object();


        public List<Position> refreshList = null;

        public List<Position> StartUpdate()
        {
            if (clientConnection != null)
            {
                /*
                if (refreshList == null)
                {
                    refreshList = new List<Position>();
                }
                else
                {
                    refreshList.Clear();
                }*/

                return new List<Position>();
            }
            return null;
        }

        public void AddPosition(Position pos, List<Position> positions)
        {
            if (positions != null)
            {
                positions.Add(pos);                
            }
            
        }

        public void DoUpdate(List<Position> positions)
        {
            if ((positions != null) && (positions.Count > 0))
            {
                Thread t = new Thread(() => RefreshMap(positions));
                t.Start();                
            }
        }

        public void RefreshMap(List<Position> positions)
        {
            try
            {
                if ((positions != null) && (positions.Count > 0))
                {
                    foreach (Position pos in positions)
                    {
                        Tile tile = new Tile();
                        tile.Position = pos;
                        gameMap.setTile(pos, tile);
                    }
                    clientConnection.RefreshMap(positions);
                }
            }
            catch (Exception ex)
            {
                Generic.AjustError(ex);
            }

            /*
            if ((positions != null) && (positions.Count > 0))
            {
                List<Position> posToUpdate = new List<Position>();

                foreach (Position pos in positions)
                {
                    if (!updatedPosition.ContainsKey(pos.ToIndex()))
                    {
                        posToUpdate.Add(pos);
                        updatedPosition.Add(pos.ToIndex(), pos);
                    }                               
                }
                if (posToUpdate.Count > 0)
                {
                    clientConnection.RefreshMap(posToUpdate);
                }                
            } */
        }

        public void UpdateServer(BatchAction batchAction, int updateType)
        {
            if ((clientConnection != null) && (batchAction != null))
            {
                BatchAction copyBatchAction = batchAction.DeepCopy();
                if (copyBatchAction != null)
                {
                    Thread t = new Thread(() => clientConnection.UpdateServer(copyBatchAction, updateType));
                    t.Start();
                }
                
            }
        }

        public void UpdateMap(MapUpdate mapUpdate)
        {
            map.UpdateMap(mapUpdate, this);
            canvasRefresh();

            //TODO
            if (map.UpdateNeedRefresh)
            {
                
                map.UpdateNeedRefresh = false;
            }
            
        }

        public void UpdateMap(List<Tile> tiles)
        {
            lock (lockRefresh)
            {
                if (tiles != null)
                {
                    foreach (Tile tile in tiles)
                    {
                        gameMap.setTile(tile.Position, tile);                        
                    }
                }
            }
            canvasRefresh();
        }

        private void canvasRefresh()
        {
            if (canvas.InvokeRequired)
                canvas.BeginInvoke((MethodInvoker)delegate
                {
                    canvas.Refresh();
                });
        }

        public void Close()
        {
            if (clientConnection != null)
            {
                clientConnection.close();
            }
        }
        #endregion

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timerConnect = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timerConnect
            // 
            this.timerConnect.Interval = 1000;
            this.timerConnect.Tick += new System.EventHandler(this.timerConnect_Tick);
            this.ResumeLayout(false);

        }

        private void timerConnect_Tick(object sender, EventArgs e)
        {
            if (clientConnection != null)
            {
                Thread t = new Thread(clientConnection.TestConnection);
                t.Start();
            }
        }

    }

    public enum EditorMode
    {
        Edition,
        Selection
    }
}
