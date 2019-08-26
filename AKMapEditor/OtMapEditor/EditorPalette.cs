using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Brush = AKMapEditor.OtMapEditor.OtBrush.Brush;
using AKMapEditor.OtMapEditor.OtBrush;
using AGraphics = AKMapEditor.OtMapEditor.GraphicManager;

namespace AKMapEditor.OtMapEditor
{
    public enum PaletteType
    {
        TERRAIN,
        DOODAD,
        ITEM,
        CREATURE,
        RAW
    }

    public enum BrushShape
    {
        BRUSHSHAPE_SQUARE,
        BRUSHSHAPE_CIRCLE
    }
      

    public partial class EditorPalette : UserControl
    {
        private static EditorPalette activePalette;

        private ListView ItemListView;
        private FlowLayoutPanel brushListItems;
        private ColumnHeader columnHeader;
        private Brush selectedBrush;
        private Panel selectedPanelBrush;        
        private Panel selectedBrushShape;
        private Panel selectedBrushSize;
        private int panelEditorTop;
        ToolTip toolTip1 = new ToolTip();
        private bool updateEditor = true;


        public double custom_thickness_mod;
        public bool use_custom_thickness = false;
        public int brush_variation = 0;

        
        public EditorPalette()
        {
            InitializeComponent();
            CreateBrushList();
            CreateListView();
            paletteType_CB.SelectedIndex = 0;
            ItemListView.HideSelection = false;

            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 100;
            toolTip1.ReshowDelay = 50;
            toolTip1.ShowAlways = true;

            quadBrush.Tag = BrushShape.BRUSHSHAPE_SQUARE;
            circleBrush.Tag = BrushShape.BRUSHSHAPE_CIRCLE;
            BrushShapeChange(quadBrush, null);
            BrushSizeChange(brushPanel0, null);

            trackBar1_Scroll(null, null);

            Global.gfx.LoadImgFiles(EditorSprite.EDITOR_SPRITE_BRUSH_SD_9x9, quadBrush);
            Global.gfx.LoadImgFiles(EditorSprite.EDITOR_SPRITE_BRUSH_CD_9x9, circleBrush);
            LoadToolBarImg();
            LoadToolBarBrush();

            placeSpawnBT.Tag = new SpawnBrush();
        }


        private void LoadBrushImgQuad()
        {
            Global.gfx.LoadImgFiles(EditorSprite.EDITOR_SPRITE_BRUSH_SD_1x1, brushPanel0);
            Global.gfx.LoadImgFiles(EditorSprite.EDITOR_SPRITE_BRUSH_SD_3x3, brushPanel1);
            Global.gfx.LoadImgFiles(EditorSprite.EDITOR_SPRITE_BRUSH_SD_5x5, brushPanel3);
            Global.gfx.LoadImgFiles(EditorSprite.EDITOR_SPRITE_BRUSH_SD_7x7, brushPanel5);
            Global.gfx.LoadImgFiles(EditorSprite.EDITOR_SPRITE_BRUSH_SD_9x9, brushPanel7);
            Global.gfx.LoadImgFiles(EditorSprite.EDITOR_SPRITE_BRUSH_SD_15x15, brushPanel9);
            Global.gfx.LoadImgFiles(EditorSprite.EDITOR_SPRITE_BRUSH_SD_19x19, brushPanel15);           
        }


        private void LoadBrushImgCircle()
        {
            Global.gfx.LoadImgFiles(EditorSprite.EDITOR_SPRITE_BRUSH_CD_1x1, brushPanel0);
            Global.gfx.LoadImgFiles(EditorSprite.EDITOR_SPRITE_BRUSH_CD_3x3, brushPanel1);
            Global.gfx.LoadImgFiles(EditorSprite.EDITOR_SPRITE_BRUSH_CD_5x5, brushPanel3);
            Global.gfx.LoadImgFiles(EditorSprite.EDITOR_SPRITE_BRUSH_CD_7x7, brushPanel5);
            Global.gfx.LoadImgFiles(EditorSprite.EDITOR_SPRITE_BRUSH_CD_9x9, brushPanel7);
            Global.gfx.LoadImgFiles(EditorSprite.EDITOR_SPRITE_BRUSH_CD_15x15, brushPanel9);
            Global.gfx.LoadImgFiles(EditorSprite.EDITOR_SPRITE_BRUSH_CD_19x19, brushPanel15);
        }


        private void LoadToolBarImg()
        {
            Global.gfx.LoadImgFiles(EditorSprite.EDITOR_SPRITE_OPTIONAL_BORDER_TOOL, BrushPanelOptionalBorder);
            Global.gfx.LoadImgFiles(EditorSprite.EDITOR_SPRITE_ERASER, BrushPanelErase);
            Global.gfx.LoadImgFiles(EditorSprite.EDITOR_SPRITE_PZ_TOOL, BrushPanelPZ);
            Global.gfx.LoadImgFiles(EditorSprite.EDITOR_SPRITE_PVPZ_TOOL, BrushPanelPVP);
            Global.gfx.LoadImgFiles(EditorSprite.EDITOR_SPRITE_NOLOG_TOOL, BrushPanelNoLogout);
            Global.gfx.LoadImgFiles(EditorSprite.EDITOR_SPRITE_NOPVP_TOOL, BrushPanelNoPVP);
            Global.gfx.LoadImgFiles(EditorSprite.EDITOR_SPRITE_DOOR_NORMAL, BrushPanelDoorNormal);
            Global.gfx.LoadImgFiles(EditorSprite.EDITOR_SPRITE_DOOR_LOCKED, BrushPanelDoorLocked);
            Global.gfx.LoadImgFiles(EditorSprite.EDITOR_SPRITE_DOOR_MAGIC, BrushPanelDoorMagic);
            Global.gfx.LoadImgFiles(EditorSprite.EDITOR_SPRITE_DOOR_QUEST, BrushPanelDoorQuest);
            Global.gfx.LoadImgFiles(EditorSprite.EDITOR_SPRITE_WINDOW_HATCH, BrushPanelWindowNormal);
            Global.gfx.LoadImgFiles(EditorSprite.EDITOR_SPRITE_WINDOW_NORMAL, BrushPanelWindowHatch);
        }

        private void LoadToolBarBrush()
        {
            
            RegistrerBrush(BrushPanelOptionalBorder, new OptionalBorderBrush());
            RegistrerBrush(BrushPanelErase, new EraserBrush());
            RegistrerBrush(BrushPanelPZ, new FlagBrush(TileState.TILESTATE_PROTECTIONZONE));
            RegistrerBrush(BrushPanelPVP, new FlagBrush(TileState.TILESTATE_PVPZONE));
            RegistrerBrush(BrushPanelNoLogout, new FlagBrush(TileState.TILESTATE_NOLOGOUT));
            RegistrerBrush(BrushPanelNoPVP, new FlagBrush(TileState.TILESTATE_NOPVP));
            RegistrerBrush(BrushPanelDoorNormal, new DoorBrush(EDoorType.WALL_DOOR_NORMAL));
            RegistrerBrush(BrushPanelDoorLocked, new DoorBrush(EDoorType.WALL_DOOR_LOCKED));
            RegistrerBrush(BrushPanelDoorMagic, new DoorBrush(EDoorType.WALL_DOOR_MAGIC));
            RegistrerBrush(BrushPanelDoorQuest, new DoorBrush(EDoorType.WALL_DOOR_QUEST));
            RegistrerBrush(BrushPanelWindowNormal, new DoorBrush(EDoorType.WALL_WINDOW));
            RegistrerBrush(BrushPanelWindowHatch, new DoorBrush(EDoorType.WALL_HATCH_WINDOW));
        }

        private void RegistrerBrush(Panel brushPanel, Brush brush)
        {
            brushPanel.MouseDown += new MouseEventHandler(brushClick);
            brushPanel.Tag = brush;            
        }

        private void paletteType_CB_SelectedIndexChanged(object sender, EventArgs e)
        {            
            try
            {
                updateEditor = false;
                selectedBrush = null;
                BrushShapeChange(quadBrush, null);
                BrushSizeChange(brushPanel0, null);
            }
            finally
            {
                updateEditor = true;
            }

            panelEditor.Controls.Clear();
            tileSet_CB.Items.Clear();
            brushListItems.Controls.Clear();
            ItemListView.Clear();
            BrushThicknessP.Visible = false;
            toolBoxGB.Visible = false;
            creatureBrush.Visible = false;
            brushSizeGB.Visible = true;
            tileSet_CB.Visible = true;
            if (panelEditorTop != 0)
            {
                panelEditor.Top = panelEditorTop;
                panelEditorTop = 0;                
            }
            
            if (IsTerrainSelected())
            {
                toolBoxGB.Visible = true;
                panelEditor.Controls.Add(brushListItems);                
                foreach (TileSet tileSet in  Materials.getInstance().getTileSetList())
                {
                    if (tileSet.getCategory2(TilesetCategoryType.TILESET_TERRAIN) != null)
                    {
                        tileSet_CB.Items.Add(tileSet);
                    }
                }
                if (tileSet_CB.Items.Count > 0) tileSet_CB.SelectedIndex = 0;

            }
            else if (IsDoodadSelected())
            {
                BrushThicknessP.Visible = true;
                panelEditor.Controls.Add(brushListItems);
                foreach (TileSet tileSet in Materials.getInstance().getTileSetList())
                {
                    if (tileSet.getCategory2(TilesetCategoryType.TILESET_DOODAD) != null)
                    {
                        tileSet_CB.Items.Add(tileSet);
                    }
                }
                if (tileSet_CB.Items.Count > 0) tileSet_CB.SelectedIndex = 0;
            }
            else if (IsItemSelected())
            {
                panelEditor.Controls.Add(ItemListView);

                foreach (TileSet tileSet in Materials.getInstance().getTileSetList())
                {
                    if (tileSet.getCategory2(TilesetCategoryType.TILESET_ITEM) != null)
                    {
                        tileSet_CB.Items.Add(tileSet);
                    }
                }
                if (tileSet_CB.Items.Count > 0) tileSet_CB.SelectedIndex = 0;
            }

            else if (IsCreatureSelected())
            {
                creatureBrush.Visible = true;
                brushSizeGB.Visible = false;
                panelEditor.Controls.Add(ItemListView);

                tileSet_CB.Visible = false;
                panelEditorTop = panelEditor.Top;
                panelEditor.Top = tileSet_CB.Top;

                foreach (TileSet tileSet in Materials.getInstance().getTileSetList())
                {
                    if (tileSet.getCategory2(TilesetCategoryType.TILESET_CREATURE) != null)
                    {
                        tileSet_CB.Items.Add(tileSet);
                    }
                }
                if (tileSet_CB.Items.Count > 0) tileSet_CB.SelectedIndex = 0;
            }
            else if (IsRawSelected())
            {
                panelEditor.Controls.Add(ItemListView);

                foreach (TileSet tileSet in Materials.getInstance().getTileSetList())
                {
                    if (tileSet.getCategory2(TilesetCategoryType.TILESET_RAW) != null)
                    {
                        tileSet_CB.Items.Add(tileSet);
                    }
                }
                if (tileSet_CB.Items.Count > 0) tileSet_CB.SelectedIndex = 0;
            }
        }

        private void tileSet_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            TileSet selectedTileSet = (TileSet)tileSet_CB.SelectedItem;
            if (IsRawSelected())
            {
                ClearListView();
                ItemListView.BeginUpdate();
                TilesetCategory tsc = selectedTileSet.getCategory2(TilesetCategoryType.TILESET_RAW);
                foreach (Brush brush in tsc.brushlist)
                {                                
                    ItemListView.Items.Add(CreateListViewItem(brush));
                }
                ItemListView.EndUpdate();
            } 
            else if (IsDoodadSelected())
            {
                brushListItems.Controls.Clear();
                brushListItems.Visible = false;
                TilesetCategory tsc = selectedTileSet.getCategory2(TilesetCategoryType.TILESET_DOODAD);
                foreach (Brush brush in tsc.brushlist)
                {
                    CreateBrushPanel(brush);

                }
                brushListItems.Visible = true;
            
            } 
            else if (IsItemSelected())
            {
                ClearListView();
                ItemListView.BeginUpdate();
                TilesetCategory tsc = selectedTileSet.getCategory2(TilesetCategoryType.TILESET_ITEM);
                foreach (Brush brush in tsc.brushlist)
                {
                    ItemListView.Items.Add(CreateListViewItem(brush));
                }
                ItemListView.EndUpdate();
            }
            else if (IsCreatureSelected())
            {
                ClearListView();
                ItemListView.BeginUpdate();
                TilesetCategory tsc = selectedTileSet.getCategory2(TilesetCategoryType.TILESET_CREATURE);
                foreach (Brush brush in tsc.brushlist)
                {
                    ItemListView.Items.Add(CreateListViewItem(brush));
                }
                ItemListView.EndUpdate();
            }
            else if (IsTerrainSelected())
            {
                brushListItems.Controls.Clear();
                brushListItems.Visible = false;
                TilesetCategory tsc = selectedTileSet.getCategory2(TilesetCategoryType.TILESET_TERRAIN);
                foreach (Brush brush in tsc.brushlist)
                {
                    CreateBrushPanel(brush);

                }
                brushListItems.Visible = true;
            }
        }

        private bool IsRawSelected()
        {
            return ("Raw Palette".Equals(paletteType_CB.SelectedItem.ToString()));
        }
        private bool IsTerrainSelected()
        {
            return ("Terrain Palette".Equals(paletteType_CB.SelectedItem.ToString()));
        }

        private bool IsDoodadSelected()
        {
            return ("Doodad Palette".Equals(paletteType_CB.SelectedItem.ToString()));
        }

        private bool IsItemSelected()
        {
            return ("Item Palette".Equals(paletteType_CB.SelectedItem.ToString()));
        }

        private bool IsCreatureSelected()
        {
            return ("Creature Palette".Equals(paletteType_CB.SelectedItem.ToString()));
        }

        private void ClearListView()
        {
            ItemListView.Clear();
            ItemListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { columnHeader });
        }

        private void CreateListView()
        {
            columnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeader.Width = 165;
            ItemListView = new ListView();
            ItemListView.SmallImageList = Global.gfx.imageItemList;
            ItemListView.Dock = DockStyle.Fill;
            ItemListView.HeaderStyle = ColumnHeaderStyle.None;
            ItemListView.FullRowSelect = true;
            ItemListView.HideSelection = false;
            ItemListView.MultiSelect = false;
            ItemListView.Name = "ItemListView";
            ItemListView.View = System.Windows.Forms.View.Details;
            ItemListView.SelectedIndexChanged += new System.EventHandler(ListView_SelectedIndexChanged);
        }

        public ListViewItem CreateListViewItem(Brush brush)
        {
            ListViewItem lvItem = new ListViewItem();
            lvItem.Text = brush.getName() + ":" + brush.getLookID();
            if (brush.getLookID() != 0)
            {
                int lookid = brush.getLookID();
                lvItem.ImageIndex = Global.gfx.getImageIndex(Global.items.items[lookid].SpriteId);
            } 
            if (brush.getSpriteLookID() != 0)
            {
                lvItem.ImageIndex = Global.gfx.getCreatureImageIndex((int) brush.getSpriteLookID());
            }

            lvItem.Tag = brush;
            return lvItem;
        }

        private void CreateBrushList()
        {
            brushListItems = new FlowLayoutPanel();
            brushListItems.AutoScroll = true;
            brushListItems.Dock = DockStyle.Fill;            
        }

        private void CreateBrushPanel(Brush brush)
        {
            Panel brushPanel = new  DCButton();
            toolTip1.SetToolTip(brushPanel, brush.getName() + " - " + brush.GetType().Name);
            brushPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;         
            brushPanel.Margin = new System.Windows.Forms.Padding(1);
            brushPanel.BackgroundImageLayout = ImageLayout.Zoom;
            brushPanel.Name = brush.getName();
            //brushPanel.Tooltip
            brushPanel.Size = new System.Drawing.Size(34, 34);
            if (brush.getLookID() != 0)
            {                
                int lookid = brush.getLookID();
           //     brushPanel.BackColor = Color.White;
                brushPanel.BackgroundImage = Global.gfx.spriteItems[Global.items.items[lookid].SpriteId].getBitmap();                
            }
            else
            {
                brushPanel.BackColor = Color.Black;
            }

            brushPanel.MouseDown += new MouseEventHandler(brushClick);
            brushPanel.Tag = brush;            
            brushListItems.Controls.Add(brushPanel);

            if (selectedBrush == null)
            {
                brushClick(brushPanel, null);
            }
        }

        private void brushClick(object sender, MouseEventArgs e)
        {
            activePalette = this;
            if (selectedPanelBrush != null)
            {
                selectedPanelBrush.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            }
            selectedPanelBrush = (Panel) sender;
            selectedPanelBrush.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            selectedBrush = (Brush) selectedPanelBrush.Tag;


            UpdateEditor();
            UpdateDoodadBrush();                        
        }

        private void ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            activePalette = this;
            if (ItemListView.SelectedItems.Count > 0)
            {
                selectedBrush = (Brush)ItemListView.SelectedItems[0].Tag;
                UpdateEditor();
            }
            
        }

        private void SelectTileSet(TileSet tileSet)
        {
            int index = 0;
            foreach (TileSet itemCB in tileSet_CB.Items)
            {
                if (itemCB.Equals(tileSet))
                {
                    break;
                }

                index++;
            }
            tileSet_CB.SelectedIndex = index;
        }

        public int getQtTiles()
        {
            if (selectedBrush == placeSpawnBT.Tag)
            {
                return Convert.ToInt32(spawnSizeNUD.Value);
            }
            else
            {
                var test = selectedBrushSize.Tag;
                return Convert.ToInt32(test);                
            }            
        }

        public Brush GetSelectedBrush()
        {
            if (Global.mapCanvas.getMapEditor().isEditionMode())
            {
                return selectedBrush;
            }
            else
            {
                return null;
            }
            
        }

        private void UpdateEditor()
        {
            if (Global.mapCanvas.hasMapEditor() && updateEditor)
            {
                Global.mapCanvas.getMapEditor().setEditionMode();
                Global.mapCanvas.Refresh();
            }
            
        }

        private int getPaletteType_CBIndex(PaletteType paletteType)
        {
            switch (paletteType)
            {
                case PaletteType.TERRAIN:
                    return 0;
                case PaletteType.DOODAD:
                    return 1;
                case PaletteType.ITEM:
                    return 2;
                case PaletteType.CREATURE:
                    return 3;
                case PaletteType.RAW:
                    return 4;
                default:
                    return -1;
            }
        }
        public static EditorPalette getSelectedPalette()
        {
            return activePalette;
        }

        private void EditorPalette_Enter(object sender, EventArgs e)
        {
            activePalette = this;
        }

        public static int GetBrushSize()
        {
            return getSelectedPalette().getQtTiles();
        }

        private BrushShape InternalGetBrushShape()
        {
            return (BrushShape) selectedBrushShape.Tag;
        }

        public static BrushShape GetBrushShape()
        {
            return getSelectedPalette().InternalGetBrushShape();            
        }

        public static Brush GetCurrentBrush()
        {
            return getSelectedPalette().GetSelectedBrush();
        }


        public static bool SelectBrush(Brush brush, PaletteType palette)
        {
            return getSelectedPalette().SelectBrushInternal(brush, palette);
        }


        private bool SelectBrushInternal(Brush brush, PaletteType palette)
        {
            paletteType_CB.SelectedIndex = getPaletteType_CBIndex(palette);
            foreach (var item in tileSet_CB.Items)
            {
                TileSet tileset = (TileSet) item;

                TilesetCategory category = null;

                switch (palette)
                {
                    case PaletteType.TERRAIN:
                        category = tileset.getCategory(TilesetCategoryType.TILESET_TERRAIN);
                        break;
                    case PaletteType.DOODAD:
                        category = tileset.getCategory(TilesetCategoryType.TILESET_DOODAD);
                        break;
                    case PaletteType.RAW:
                        category = tileset.getCategory(TilesetCategoryType.TILESET_RAW);
                        break;
                    case PaletteType.ITEM:
                        category = tileset.getCategory(TilesetCategoryType.TILESET_ITEM);
                        break;
                    case PaletteType.CREATURE:
                        category = tileset.getCategory(TilesetCategoryType.TILESET_CREATURE);
                        break;
                }

                foreach (Brush ibrush in category.brushlist)
                {
                    if (ibrush.Equals(brush))
                    {
                        tileSet_CB.SelectedItem = item;
                        if (panelEditor.Controls.Contains(brushListItems))
                        {
                            foreach (Control control in brushListItems.Controls)
                            {
                                if (control.Tag.Equals(brush))
                                {
                                    Panel panel = (Panel)control;
                                    brushClick(panel, null);
                                    return true;
                                }
                            }
                        }
                        else if (panelEditor.Controls.Contains(ItemListView))
                        {
                            foreach (ListViewItem lvitem in ItemListView.Items)
                            {
                                if (lvitem.Tag.Equals(brush))
                                {
                                    lvitem.Selected = true;
                                    lvitem.Focused = true;                                    
                                    ItemListView.Select();
                                    ItemListView.EnsureVisible(lvitem.Index);
                                    return true;
                                }
                            }                            
                        }
                                        
                    }
                }

            }
            return false;
        }

        private void panel1_Paint(object sender, System.Windows.Forms.PaintEventArgs
        e)
        {           
            Panel panel1 = (Panel) sender;

            Graphics g = panel1.CreateGraphics();

            Rectangle panelRect = panel1.ClientRectangle;

            Point p1 = new Point(panelRect.Left, panelRect.Top); //top left
            Point p2 = new Point(panelRect.Right-2, panelRect.Top); //Top Right
            Point p3 = new Point(panelRect.Left, panelRect.Bottom-2); //Bottom Left
            Point p4 = new Point(panelRect.Right - 2, panelRect.Bottom -2); //Bottom Right

            Pen pen1 = new Pen(System.Drawing.Color.Black);
            Pen pen2 = new Pen(System.Drawing.Color.White);

            g.DrawLine(pen1, p1, p2);
            g.DrawLine(pen1, p1, p3);
            g.DrawLine(pen2, p2, p4);
            g.DrawLine(pen2, p3, p4);
        }

        private void BrushShapeChange(object sender, MouseEventArgs e)
        {
            Panel brushPanel = (Panel)sender;
            if (selectedBrushShape != null)
            {
                selectedBrushShape.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            }
            if (quadBrush.Equals(brushPanel))
            {
                LoadBrushImgQuad();
            }
            else
            {
                LoadBrushImgCircle();
            }


            brushPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            selectedBrushShape = brushPanel;
            UpdateEditor();
        }

        private void BrushSizeChange(object sender, MouseEventArgs e)
        {            
            Panel brushPanel = (Panel)sender;
            if (brushPanel.Equals(selectedBrushSize)) return;

            brushPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            if (selectedBrushSize != null)
            {
                selectedBrushSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;               
            }            
            selectedBrushSize = brushPanel;

            UpdateDoodadBrush();
            UpdateEditor();
        }


        public void UpdateDoodadBrush()
        {
            DoodadBrush dbrush = selectedBrush as DoodadBrush;
            if (dbrush != null)
            {
                Global.mapCanvas.secondary_map = null;
                DoodadBrush.FillDoodadPreviewBuffer();
                Global.mapCanvas.secondary_map = Global.mapCanvas.doodad_buffer_map;
                Global.mapCanvas.Refresh();
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int[] lookup_table= {1,2,3,5,8,13,23,35,50,80};
            use_cf_button.Checked = true;

            SetBrushThickness(true, lookup_table[trackBar1 .Value-1], 100);
        }

        private void use_cf_button_CheckedChanged(object sender, EventArgs e)
        {
            SetBrushThickness(use_cf_button.Checked);
        }


        public void SetBrushThickness(int x, int y)
        {
            if (x != -1 || y != -1)
            {
                custom_thickness_mod = (double)Math.Max(x, 1) / (double)Math.Max(y, 1);
            }

            UpdateDoodadBrush();

            UpdateEditor();
        }
        public void SetBrushThickness(bool on, int x = -1, int y = -1)
        {
            use_custom_thickness = on;

            if (x != -1 || y != -1)
            {
                custom_thickness_mod = (double)Math.Max(x, 1) / (double)Math.Max(y, 1);
            }

            UpdateDoodadBrush();

            UpdateEditor();
        }

        public int GetBrushVariation()
        {
            return brush_variation;
        }

        public void SetBrushVariation(int nz)
        {
            DoodadBrush dbrush = selectedBrush as DoodadBrush;
            if (nz != brush_variation && dbrush != null)
            {
                // Monkey!
                brush_variation = nz;
                Global.mapCanvas.secondary_map = null;
                DoodadBrush.FillDoodadPreviewBuffer();
                Global.mapCanvas.secondary_map = Global.mapCanvas.doodad_buffer_map;
            }

        }

        private void placeCreatureBT_Click(object sender, EventArgs e)
        {
            if (ItemListView.SelectedItems.Count > 0)
            {
                selectedBrush = (Brush)ItemListView.SelectedItems[0].Tag;
                UpdateEditor();
            } 
        }

        private void placeSpawn_Click(object sender, EventArgs e)
        {
            selectedBrush = (Brush)placeSpawnBT.Tag;
            UpdateEditor();
        }

        public int GetSpawnTime()
        {
            return Convert.ToInt32(spawnTimeNUD.Value);
        }      
    }
}
