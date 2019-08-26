using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Windows.Forms;
using System.Drawing;
using Brush = AKMapEditor.OtMapEditor.OtBrush.Brush;
using AKMapEditor.OtMapEditor.OtBrush;
using System.Diagnostics;


namespace AKMapEditor.OtMapEditor
{
    public class MapCanvas : GLControl
    {

        public GameMap doodad_buffer_map;
        public GameMap secondary_map;
        public CopyBuffer copybuffer;

        public MainForm mainForm;
        private bool loaded = false;
        private Position oldMousePos;

        //--------keys
        public bool shiftActive = false;
        public bool altActive = false;
        public bool ctrlActive = false;

        public bool drawing = false;


        private bool dragging  = false;
        private int drag_start_x = -1;
        private int drag_start_y = -1;
        private int drag_start_z = -1;


        private int last_click_map_x = 0;
        private int last_click_map_y = 0;
        private int last_click_map_z = 0;
        private int last_click_x = 0;
        private int last_click_y = 0;


        public bool pasting = false;

        public bool isBrush = false;
        
        public bool dragging_draw = false;

        public bool boundbox_selection = false;

        private int cursor_x, cursor_y;
        private int current_house_id = 0;

        #region ComponentesTela        
        private ContextMenuStrip contextMenuStrip;
        private System.ComponentModel.IContainer components;
        private ToolStripMenuItem cutToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem pasteToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripSeparator tssProp;
        private ToolStripMenuItem propertiesToolStripMenuItem;
        private ToolStripMenuItem selectRawTSMI;
        private ToolStripSeparator tssBrush;
        private ToolStripMenuItem selectGroundBrushTSMI;
        private ToolStripMenuItem selectDoodadbrushTSMI;
        private ToolStripMenuItem selectWallbrushTSMI;
        private ToolStripMenuItem selectCarpetBrushTSMI;
        private ToolStripMenuItem selectTableBrushTSMI;
        private ToolStripMenuItem selectDoorBrushTSMI;
        private ToolStripSeparator tssOption;
        private ToolStripMenuItem openDoorTSMI;
        private ToolStripMenuItem closeDoorTSMI;
        private ToolStripMenuItem gotoDestinationTSMI;
        private ToolStripMenuItem rotateItemTSMI;
        private ToolStripMenuItem selectCreatureTSMI;
        private ToolStripMenuItem copyPositionToolStripMenuItem;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyPositionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tssOption = new System.Windows.Forms.ToolStripSeparator();
            this.rotateItemTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.gotoDestinationTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.openDoorTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.closeDoorTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.tssBrush = new System.Windows.Forms.ToolStripSeparator();
            this.selectRawTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.selectWallbrushTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.selectCarpetBrushTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.selectTableBrushTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.selectDoorBrushTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.selectDoodadbrushTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.selectGroundBrushTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.tssProp = new System.Windows.Forms.ToolStripSeparator();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectCreatureTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.copyPositionToolStripMenuItem,
            this.tssOption,
            this.rotateItemTSMI,
            this.gotoDestinationTSMI,
            this.openDoorTSMI,
            this.closeDoorTSMI,
            this.tssBrush,
            this.selectRawTSMI,
            this.selectWallbrushTSMI,
            this.selectCarpetBrushTSMI,
            this.selectTableBrushTSMI,
            this.selectDoorBrushTSMI,
            this.selectDoodadbrushTSMI,
            this.selectGroundBrushTSMI,
            this.selectCreatureTSMI,
            this.tssProp,
            this.propertiesToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(181, 440);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // copyPositionToolStripMenuItem
            // 
            this.copyPositionToolStripMenuItem.Name = "copyPositionToolStripMenuItem";
            this.copyPositionToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.copyPositionToolStripMenuItem.Text = "Copy Position";
            this.copyPositionToolStripMenuItem.Click += new System.EventHandler(this.copyPositionToolStripMenuItem_Click);
            // 
            // tssOption
            // 
            this.tssOption.Name = "tssOption";
            this.tssOption.Size = new System.Drawing.Size(177, 6);
            // 
            // rotateItemTSMI
            // 
            this.rotateItemTSMI.Name = "rotateItemTSMI";
            this.rotateItemTSMI.Size = new System.Drawing.Size(180, 22);
            this.rotateItemTSMI.Text = "Rotate item";
            // 
            // gotoDestinationTSMI
            // 
            this.gotoDestinationTSMI.Name = "gotoDestinationTSMI";
            this.gotoDestinationTSMI.Size = new System.Drawing.Size(180, 22);
            this.gotoDestinationTSMI.Text = "Goto Destination";
            // 
            // openDoorTSMI
            // 
            this.openDoorTSMI.Name = "openDoorTSMI";
            this.openDoorTSMI.Size = new System.Drawing.Size(180, 22);
            this.openDoorTSMI.Text = "Open door";
            // 
            // closeDoorTSMI
            // 
            this.closeDoorTSMI.Name = "closeDoorTSMI";
            this.closeDoorTSMI.Size = new System.Drawing.Size(180, 22);
            this.closeDoorTSMI.Text = "Close door";
            // 
            // tssBrush
            // 
            this.tssBrush.Name = "tssBrush";
            this.tssBrush.Size = new System.Drawing.Size(177, 6);
            // 
            // selectRawTSMI
            // 
            this.selectRawTSMI.Name = "selectRawTSMI";
            this.selectRawTSMI.Size = new System.Drawing.Size(180, 22);
            this.selectRawTSMI.Text = "Select RAW";
            this.selectRawTSMI.Click += new System.EventHandler(this.selectRawTSMI_Click);
            // 
            // selectWallbrushTSMI
            // 
            this.selectWallbrushTSMI.Name = "selectWallbrushTSMI";
            this.selectWallbrushTSMI.Size = new System.Drawing.Size(180, 22);
            this.selectWallbrushTSMI.Text = "Select Wallbrush";
            this.selectWallbrushTSMI.Click += new System.EventHandler(this.selectWallbrushTSMI_Click);
            // 
            // selectCarpetBrushTSMI
            // 
            this.selectCarpetBrushTSMI.Name = "selectCarpetBrushTSMI";
            this.selectCarpetBrushTSMI.Size = new System.Drawing.Size(180, 22);
            this.selectCarpetBrushTSMI.Text = "Select Carpetbrush";
            this.selectCarpetBrushTSMI.Click += new System.EventHandler(this.selectCarpetBrushTSMI_Click);
            // 
            // selectTableBrushTSMI
            // 
            this.selectTableBrushTSMI.Name = "selectTableBrushTSMI";
            this.selectTableBrushTSMI.Size = new System.Drawing.Size(180, 22);
            this.selectTableBrushTSMI.Text = "Select Tablebrush";
            this.selectTableBrushTSMI.Click += new System.EventHandler(this.selectTableBrushTSMI_Click);
            // 
            // selectDoorBrushTSMI
            // 
            this.selectDoorBrushTSMI.Name = "selectDoorBrushTSMI";
            this.selectDoorBrushTSMI.Size = new System.Drawing.Size(180, 22);
            this.selectDoorBrushTSMI.Text = "Select Doorbrush";
            this.selectDoorBrushTSMI.Click += new System.EventHandler(this.selectDoorBrushTSMI_Click);
            // 
            // selectDoodadbrushTSMI
            // 
            this.selectDoodadbrushTSMI.Name = "selectDoodadbrushTSMI";
            this.selectDoodadbrushTSMI.Size = new System.Drawing.Size(180, 22);
            this.selectDoodadbrushTSMI.Text = "Select Doodadbrush";
            this.selectDoodadbrushTSMI.Click += new System.EventHandler(this.selectDoodadbrushTSMI_Click);
            // 
            // selectGroundBrushTSMI
            // 
            this.selectGroundBrushTSMI.Name = "selectGroundBrushTSMI";
            this.selectGroundBrushTSMI.Size = new System.Drawing.Size(180, 22);
            this.selectGroundBrushTSMI.Text = "Select Groundbrush";
            this.selectGroundBrushTSMI.Click += new System.EventHandler(this.selectGroundBrushTSMI_Click);
            // 
            // tssProp
            // 
            this.tssProp.Name = "tssProp";
            this.tssProp.Size = new System.Drawing.Size(177, 6);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.propertiesToolStripMenuItem.Text = "Properties";
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
            // 
            // selectCreatureTSMI
            // 
            this.selectCreatureTSMI.Name = "selectCreatureTSMI";
            this.selectCreatureTSMI.Size = new System.Drawing.Size(180, 22);
            this.selectCreatureTSMI.Text = "Select Creature";
            this.selectCreatureTSMI.Click += new System.EventHandler(this.selectCreatureTSMI_Click);
            // 
            // MapCanvas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ContextMenuStrip = this.contextMenuStrip;
            this.Name = "MapCanvas";
            this.Size = new System.Drawing.Size(609, 457);
            this.Load += new System.EventHandler(this.MapCanvas_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MapCanvas_Paint);
            this.Enter += new System.EventHandler(this.MapCanvas_Enter);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MapCanvas_KeyUp);
            this.Leave += new System.EventHandler(this.MapCanvas_Leave);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MapCanvas_MouseDown);
            this.MouseEnter += new System.EventHandler(this.MapCanvas_MouseEnter);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MapCanvas_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MapCanvas_MouseUp);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.MapCanvas_MouseWheel);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        public MapCanvas()
        {
            InitializeComponent();
            oldMousePos = new Position(0, 0, 0);
            doodad_buffer_map = new GameMap();
            copybuffer = new CopyBuffer();
        }

        #endregion


        #region DrawRegion
        private void MapCanvas_Load(object sender, EventArgs e)
        {
            int w = this.Width;
            int h = this.Height;
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, w, h, 0, -1, 1);
            GL.Viewport(0, 0, w, h);

            GL.Disable(EnableCap.CullFace);
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1);

            loaded = true;

            this.Focus();
        }

        public void BlitSpriteType(int screenx, int screeny, uint spriteid, int red = 255, int green = 255, int blue = 255, int alpha = 255)
        {
            GameSprite spr = Global.gfx.item_db.items[spriteid].gameSprite;

            BlitSpriteType(screenx, screeny, spr, red, green, blue, alpha);
        }
        public void BlitSpriteType(int screenx, int screeny, GameSprite spr, int red, int green, int blue, int alpha)
        {
            if (spr == null)
                return;
            screenx -= spr.drawoffset_x;
            screeny -= spr.drawoffset_y;

            int tme = 0; //GetTime() % itype->FPA;
            for (int cx = 0; cx != spr.width; ++cx)
            {
                for (int cy = 0; cy != spr.height; ++cy)
                {
                    for (int cf = 0; cf != spr.frames; ++cf)
                    {
                        int texnum = spr.getHardwareID(cx, cy, cf, -1, 0, 0, 0, tme);
                        //printf("CF: %d\tTexturenum: %d\n", cf, texnum);
                        glBlitTexture(screenx - cx * 32, screeny - cy * 32, texnum, red, green, blue, alpha);
                    }
                }
            }
        }

        public void BlitCreature(int screenx, int screeny, Outfit outfit, int red = 255, int green = 255, int blue = 255, int alpha = 255)
        {
            if (outfit.lookItem != 0)
            {
                ItemType it = Global.items.clientItems[outfit.lookItem];                
                BlitSpriteType(screenx, screeny, it.gameSprite, red, green, blue, alpha);
                return;
            }
            if (outfit.lookType != 0)
            {
                GameSprite spr = Global.gfx.getCreatureSprite(outfit.lookType);
                if (spr == null)
                    return;
                int tme = 0; //GetTime() % itype->FPA;
                for (int cx = 0; cx != spr.width; ++cx)
                {
                    for (int cy = 0; cy != spr.height; ++cy)
                    {
                        int texnum = spr.getHardwareID(cx, cy, 2, outfit, tme); //south
                        glBlitTexture(screenx - cx * 32, screeny - cy * 32, texnum, red, green, blue, alpha);
                    }
                }
            }
        }

        public void BlitCreature(int screenx, int screeny, Creature c, int red = 255, int green = 255, int blue = 255, int alpha = 255)
        {
            if (c.isSelected())
            {
                red /= 2;
                green /= 2;
                blue /= 2;
            }
            BlitCreature(screenx, screeny, c.getLookType(), red, green, blue, alpha);
        }


        private void glBlitTexture(int screenX, int screenY, int texture_number, int red = 255, int green = 255, int blue = 255, int alpha = 255)
        {
            GL.BindTexture(TextureTarget.Texture2D, texture_number);            
            GL.Color4(Color.FromArgb(alpha,red, green, blue));
            GL.Begin(BeginMode.Quads);            
            GL.TexCoord2(0, 0); GL.Vertex2(screenX, screenY);
            GL.TexCoord2(1, 0); GL.Vertex2(screenX + 32, screenY);
            GL.TexCoord2(1, 1); GL.Vertex2(screenX + 32, screenY + 32);
            GL.TexCoord2(0, 1); GL.Vertex2(screenX, screenY + 32);
            GL.End(); 
        }

        private void glBlitQuad(int screenX, int screenY, int red = 255, int green = 255, int blue = 255, int alpha = 255)
        {
            GL.Disable(EnableCap.Texture2D);
            GL.Color4(Color.FromArgb(alpha, red, green, blue));
            GL.Begin(BeginMode.Quads);
            GL.TexCoord2(0, 0); GL.Vertex2(screenX, screenY);
            GL.TexCoord2(1, 0); GL.Vertex2(screenX + 32, screenY);
            GL.TexCoord2(1, 1); GL.Vertex2(screenX + 32, screenY + 32);
            GL.TexCoord2(0, 1); GL.Vertex2(screenX, screenY + 32);
            GL.End();
            GL.Enable(EnableCap.Texture2D);
        }
        
        private void glBlitSelectionQuad()
        {
            if (boundbox_selection)
            {
                double zoom = getMapEditor().Zoom;

                GL.Disable(EnableCap.Texture2D);
                GL.Begin(BeginMode.LineLoop);
                GL.Color4(Color.FromArgb(255, 255, 255, 255));
                GL.TexCoord2(0, 0); GL.Vertex2(last_click_x / zoom, last_click_y / zoom);
                GL.TexCoord2(1, 0); GL.Vertex2(cursor_x / zoom, last_click_y / zoom);
                GL.TexCoord2(1, 1); GL.Vertex2(cursor_x / zoom, cursor_y / zoom);
                GL.TexCoord2(0, 1); GL.Vertex2(last_click_x / zoom, cursor_y / zoom);
                GL.End();
                GL.Enable(EnableCap.Texture2D);
            }

        }

        private int getFloorAdjustment(int floor)
        {
	        if (floor > 7) // Underground
	        {
		        return 0; //32*-(8-floor); // No adjustment
	        }
	        else
	        {
                return 0; // 32 * (7 - floor);
	        }
        }


        public void DrawItem(Item item, Position pos, int red = 255, int green = 255, int blue = 255, int alpha = 255)
        {
            try
            {

                if (item == null) return;
                if (item.isSelected())
                {
                    red /= 2;
                    green /= 2;
                    blue /= 2;
                }
                //Debug.Assert(item.Type != null);

                GameSprite spr = item.Type.gameSprite;

                if (spr == null)
                {
                    spr = Global.gfx.spriteItems[item.Type.SpriteId];
                    if (spr != null)
                    {
                        Global.items.items[item.Type.Id].gameSprite = spr;
                    }
                    else
                    {
                        return;
                    }

                }
                int draw_x = (pos.X * (32)) - getMapEditor().getViewX();
                int draw_y = (pos.Y * (32)) - getMapEditor().getViewY();


                draw_x -= ((getMapEditor().MapZ - pos.Z) * 32);
                draw_y -= ((getMapEditor().MapZ - pos.Z) * 32);


                draw_x -= spr.drawoffset_x;
                draw_y -= spr.drawoffset_x;

                //draw_x -= (count*8); work, but need ajusts
                //draw_y -= (count*8);

                int subtype = -1;

                int xdiv = pos.X % spr.xdiv;
                int ydiv = pos.Y % spr.ydiv;
                int zdiv = pos.Z % spr.zdiv;


                int tme = 0;
                for (int cx = 0; cx != spr.width; cx++)
                {
                    for (int cy = 0; cy != spr.height; cy++)
                    {
                        for (int cf = 0; cf != spr.frames; cf++)
                        {
                            int texnum = spr.getHardwareID(cx, cy, cf,
                                subtype,
                                xdiv,
                                ydiv,
                                zdiv,
                                tme
                            );
                            glBlitTexture(draw_x - (cx * 32), draw_y - (cy * 32), texnum, red, green, blue, alpha);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BlitItem(ref int draw_x, ref int draw_y, Position pos,  Item item, bool ephemeral = false, int red = 255, int green = 255, int blue = 255, int alpha = 255)
        {
            ItemType it = item.Type;

            if (!ephemeral && item.isSelected())
            {
                red /= 2;
                blue /= 2;
                green /= 2;
            }


            if (it.Id == 0)
            {
                glBlitQuad(draw_x, draw_y, 255, 0, 0, alpha);
                return;
            }
            else if (it.Id == 459)
            {
                glBlitQuad(draw_x, draw_y, red, green, 0, alpha / 3 * 2);
                return;
            }
            else if (it.Id == 460)
            {
                glBlitQuad(draw_x, draw_y, red, 0, 0, alpha / 3 * 2);
                return;
            }

            GameSprite spr = item.Type.gameSprite;

            if (spr == null)
            {
                spr = Global.gfx.spriteItems[item.Type.SpriteId];
                if (spr != null)
                {
                    Global.items.items[item.Type.Id].gameSprite = spr;
                }
                else
                {
                    return;
                }

            }
            if (it.isMetaItem()) return;
            if (!ephemeral && it.IsPickupable && Settings.GetBoolean(Key.SHOW_ITEMS) == false) return;
            int screenx = draw_x - spr.drawoffset_x;
            int screeny = draw_y - spr.drawoffset_y;

            draw_x -= spr.getDrawHeight();
            draw_y -= spr.getDrawHeight();

            int subtype = -1;

            int xdiv = pos.x % spr.xdiv;
            int ydiv = pos.y % spr.ydiv;
            int zdiv = pos.z % spr.zdiv;

            if (it.Group == ItemGroup.Splash || it.Group == ItemGroup.FluidContainer)
            {
                subtype = item.getSubtype();
            }
            else if (it.IsHangable)
            {
                xdiv = 0;
                /*
                if (tile.hasProperty(ItemProperty.ISVERTICAL))
                {
                    xdiv = 2;
                }
                else if (tile.hasProperty(ItemProperty.ISHORIZONTAL))
                {
                    xdiv = 1;
                }
                else
                {
                    xdiv = -0;
                } */
            }

            else if (it.IsStackable)
            {
                if (item.getSubtype() <= 1)
                    subtype = 0;
                else if (item.getSubtype() <= 2)
                    subtype = 1;
                else if (item.getSubtype() <= 3)
                    subtype = 2;
                else if (item.getSubtype() <= 4)
                    subtype = 3;
                else if (item.getSubtype() < 10)
                    subtype = 4;
                else if (item.getSubtype() < 25)
                    subtype = 5;
                else if (item.getSubtype() < 50)
                    subtype = 6;
                else
                    subtype = 7;
            }

            int tme = 0; //GetTime() % itype->FPA;
            for (int cx = 0; cx != spr.width; cx++)
            {
                for (int cy = 0; cy != spr.height; cy++)
                {
                    for (int cf = 0; cf != spr.frames; cf++)
                    {
                        int texnum = spr.getHardwareID(cx, cy, cf,
                            subtype,
                            xdiv,
                            ydiv,
                            zdiv,
                            tme
                        );
                        glBlitTexture(screenx - cx * 32, screeny - cy * 32, texnum, red, green, blue, alpha);
                    }
                }
            }

        }



        public void BlitItem(ref int draw_x, ref int draw_y, Tile tile, Item item, bool ephemeral = false, int red = 255, int green = 255, int blue = 255, int alpha = 255)
        {
            ItemType it = item.Type;

            if (!ephemeral && item.isSelected())
            {
                red /= 2;
                blue /= 2;
                green /= 2;
            }


            if (it.Id == 0)
            {                
                glBlitQuad(draw_x, draw_y, 255, 0, 0, alpha);                
                return;
            }
            else if (it.Id == 459)
            {               
                glBlitQuad(draw_x, draw_y, red, green, 0, alpha / 3 * 2);             
                return;
            }
            else if (it.Id == 460)
            {
                glBlitQuad(draw_x, draw_y, red, 0, 0, alpha / 3 * 2);             
                return;
            }

            GameSprite spr = item.Type.gameSprite;

            if (spr == null)
            {
                spr = Global.gfx.spriteItems[item.Type.SpriteId];
                if (spr != null)
                {
                    Global.items.items[item.Type.Id].gameSprite = spr;
                }
                else
                {
                    return;
                }

            }
            if (it.isMetaItem()) return;            
            if(!ephemeral && it.IsPickupable && Settings.GetBoolean(Key.SHOW_ITEMS) == false) return;
            int screenx = draw_x - spr.drawoffset_x;
	        int screeny = draw_y - spr.drawoffset_y;

            Position pos = tile.Position;

            draw_x -= spr.getDrawHeight();
            draw_y -= spr.getDrawHeight();

            int subtype = -1;

            int xdiv = pos.x % spr.xdiv;
            int ydiv = pos.y % spr.ydiv;
            int zdiv = pos.z % spr.zdiv;

            if (it.Group == ItemGroup.Splash || it.Group == ItemGroup.FluidContainer)
            {
                subtype = item.getSubtype();
            }
            else if (it.IsHangable)
            {
                if (tile.hasProperty(ItemProperty.ISVERTICAL))
                {
                    xdiv = 2;
                }
                else if (tile.hasProperty(ItemProperty.ISHORIZONTAL))
                {
                    xdiv = 1;
                }
                else
                {
                    xdiv = -0;
                }
            }

            else if (it.IsStackable)
            {
                if (item.getSubtype() <= 1)
                    subtype = 0;
                else if (item.getSubtype() <= 2)
                    subtype = 1;
                else if (item.getSubtype() <= 3)
                    subtype = 2;
                else if (item.getSubtype() <= 4)
                    subtype = 3;
                else if (item.getSubtype() < 10)
                    subtype = 4;
                else if (item.getSubtype() < 25)
                    subtype = 5;
                else if (item.getSubtype() < 50)
                    subtype = 6;
                else
                    subtype = 7;
            }

            int tme = 0; //GetTime() % itype->FPA;
            for (int cx = 0; cx != spr.width; cx++)
            {
                for (int cy = 0; cy != spr.height; cy++)
                {
                    for (int cf = 0; cf != spr.frames; cf++)
                    {
                        int texnum = spr.getHardwareID(cx, cy, cf,
                            subtype,
                            xdiv,
                            ydiv,
                            zdiv,
                            tme
                        );
                        glBlitTexture(screenx - cx * 32, screeny - cy * 32, texnum, red, green, blue, alpha);
                    }
                }
            }

        }

        private void DrawTile(Tile tile)
        {
            if (tile == null) return;

            int map_x = tile.Position.X;
            int map_y = tile.Position.Y;
            int map_z = tile.Position.Z;


            int view_scroll_x = getMapEditor().getViewX();
            int view_scroll_y = getMapEditor().getViewY();
            int floor = getMapEditor().MapZ;

            int draw_x = map_x * 32 - view_scroll_x;
            int draw_y = map_y * 32 - view_scroll_y;

            Position pos = tile.Position;

            draw_x -= ((getMapEditor().MapZ - pos.Z) * 32);
            draw_y -= ((getMapEditor().MapZ - pos.Z) * 32);

            int r = 255;
            int g = 255;
            int b = 255;

            if (tile.Ground != null)
            {
                bool showspecial = Settings.GetBoolean(Key.SHOW_SPECIAL_TILES);

                if ((tile.isBlocking() && Settings.GetBoolean(Key.SHOW_BLOCKING)))
                {
                    g = g / 3 * 2;
                    b = b / 3 * 2;
                }

                if (Settings.GetBoolean(Key.HIGHLIGHT_ITEMS) && tile.Items.Count() > 0 && tile.Items[0].isBorder() == false)
                {
                    g = g / 3 * 2;
                    r = r / 3 * 2;
                }

                if (tile.spawn_count > 0 && Settings.GetBoolean(Key.SHOW_SPAWNS))
                {
                    float f = 1.0f;
                    for (uint i = 0; i < tile.spawn_count; ++i)
                    {
                        f *= 0.7f;
                    }
                    g = (int)(g * f);
                    b = (int)(b * f);
                }

                if (tile.isHouseTile() && Settings.GetBoolean(Key.SHOW_HOUSES))
                {
                    if (tile.getHouseID() == current_house_id)
                    {
                        r /= 2;
                    }
                    else
                    {
                        r /= 2;
                        g /= 2;
                    }
                }
                else if (showspecial && tile.hasMapFlag(TileState.TILESTATE_PROTECTIONZONE))
                {
                    r /= 2;
                    b /= 2;
                }

                if (showspecial && tile.hasMapFlag(TileState.TILESTATE_PVPZONE))
                {
			        g = r/4;
			        b = b/3*2;
		        }

                if (showspecial && tile.hasMapFlag(TileState.TILESTATE_NOLOGOUT))
                {
                    b /= 2;
                }
                if (showspecial && tile.hasMapFlag(TileState.TILESTATE_NOPVP))
                {
                    g /= 2;
                }

                BlitItem(ref draw_x, ref draw_y, tile, tile.Ground, false, r, g, b);
            }
            if ((getMapEditor().Zoom < 10.0) || Settings.GetBoolean(Key.HIDE_ITEMS_WHEN_ZOOMED) == false)
            {

                foreach (Item it in tile.Items)
                {
                    //BlitItem(ref draw_x, ref draw_y, tile, it);                    
                    if(it.isBorder()) {
                        BlitItem(ref draw_x, ref draw_y, tile, it, false, r, g, b);
                    } else {
                        BlitItem(ref draw_x, ref draw_y, tile, it);
                    } 
                }
                if((tile.creature != null) && Settings.GetBoolean(Key.SHOW_CREATURES)) 
                {
                    BlitCreature(draw_x, draw_y, tile.creature);
                }

		    if(tile.spawn != null && Settings.GetBoolean(Key.SHOW_SPAWNS)) {
			    if(tile.spawn.isSelected()) {
                    BlitSpriteType(draw_x, draw_y, SpriteEnum.SPRITE_SPAWN, 128, 128, 128);
			    } else {
                    BlitSpriteType(draw_x, draw_y, SpriteEnum.SPRITE_SPAWN, 255, 255, 255);
			    }
		    }

            }
        }

        private int qt = 0;

        private void MapCanvas_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            try
            {
                if (!loaded) return;

                MapEditor editor = getMapEditor();

                if (editor.MapZ != Global.mainForm.selectedMenuFloor())
                {
                    Global.mainForm.FloorChange(editor.MapZ);
                }

                int screensize_x = this.Width;
                int screensize_y = this.Height;

              //  qt++;

             //   getMainForm().updateAction("" + qt);

                GL.MatrixMode(MatrixMode.Projection);
                GL.LoadIdentity();
                GL.Viewport(0, 0, screensize_x, screensize_y);
                GL.Ortho(0, screensize_x / editor.Zoom, screensize_y / editor.Zoom, 0, -1, 1);

                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                GL.MatrixMode(MatrixMode.Modelview);
                GL.LoadIdentity();

                GL.Enable(EnableCap.Blend);
                //----------- draw logic---------------

                // draw map
                int view_scroll_x = editor.getViewX();
                int view_scroll_y = editor.getViewY();
                int screen_size_x = (int)(screensize_x / editor.Zoom);
                int screen_size_y = (int)(screensize_x / editor.Zoom);

                int start_x = (view_scroll_x / 32) - 2;
                int end_x = ((view_scroll_x + screen_size_x) / 32) + 2;
                int start_y = (view_scroll_y / 32);
                int end_y = ((view_scroll_y + screen_size_y) / 32) + 2;                


                int start_z = (editor.MapZ <= 7 ? editor.MapZ : editor.MapZ);
                int end_z = (editor.MapZ <= 7 ? 7 : editor.MapZ + 3);

                int floor = editor.MapZ;
                int mouse_map_x = getMousePosition().X;
                int mouse_map_y = getMousePosition().Y;

                List<Position> posRefresh = editor.StartUpdate();

                for (int az = end_z; az >= start_z; az--)
                {
                    int map_z = az;
                    if ((getMapEditor().MapZ == az) && Settings.GetBoolean(Key.SHOW_SHADE))
                    {
                        GL.Disable(EnableCap.Texture2D);
                        GL.Color4(Color.FromArgb(128, 0, 0, 0));
                        GL.Begin(BeginMode.Quads);
                        GL.Vertex2(0, (int)screensize_y / editor.Zoom);
                        GL.Vertex2((int)screensize_x / editor.Zoom, (int)screensize_y / editor.Zoom);
                        GL.Vertex2((int)screensize_x / editor.Zoom, 0);
                        GL.Vertex2(0, 0);
                        GL.End();

                        GL.Enable(EnableCap.Texture2D);
                    }


                    int dif = (editor.MapZ - az);
                    for (int ax = (start_x + dif); ax <= (end_x + dif); ax++)
                    {
                        for (int ay = (start_y + dif); ay <= (end_y + dif); ay++)
                        {
                            Position posTile = new Position(ax, ay, az);

                            Tile tile = editor.gameMap.getTile(posTile);
                            if (tile != null)
                            {
                                if (tile.size() > 0)
                                {
                                    DrawTile(tile);
                                }
                            }
                            else
                            {
                                editor.AddPosition(posTile, posRefresh);
                            }
                        }
                    }

                    if (secondary_map != null)
                    {
                        Position normalPos = new Position();
                        Position to = getMousePosition();
                        if (isPasting() == true)
                        {
                            normalPos = editor.copybuffer.getPosition();
                        }
                        else if (EditorPalette.getSelectedPalette().GetSelectedBrush() as DoodadBrush != null)
                        {
                            normalPos = new Position(0x8000, 0x8000, 0x8);
                        }
                        for (int map_x = start_x; map_x <= end_x; map_x++)
                        {
                            for (int map_y = start_y; map_y <= end_y; map_y++)
                            {
                                Position final = new Position(map_x, map_y, map_z);
                                Position pos = normalPos + final - to;
                                //Position pos = topos + copypos - Position(map_x, map_y, map_z);
                                if (pos.z >= 15 || pos.z < 0)
                                {
                                    continue;
                                }
                                Tile tile = secondary_map.getTile(pos);

                                if (tile != null)
                                {
                                     //int draw_x = (map_x * 32) - view_scroll_x; //(15-map_z)*32-(15-floor)*32;
                                     //int draw_y = (map_y * 32) - view_scroll_y; //(15-map_z)*32-(15-floor)*32;

                                     int draw_x = ((map_x * 32) - view_scroll_x) - ((floor - map_z) * 32); //(15-map_z)*32-(15-floor)*32;
                                     int draw_y = ((map_y * 32) - view_scroll_y) - ((floor - map_z) * 32); //(15-map_z)*32-(15-floor)*32;

                                    int r = 160;
                                    int g = 160;
                                    int b = 160;
                                    if (tile.Ground != null)
                                    {
                                        bool showspecial = Settings.GetBoolean(Key.SHOW_SPECIAL_TILES);

                                        if (tile.isBlocking() && Settings.GetBoolean(Key.SHOW_BLOCKING))
                                        {
                                            g = g / 3 * 2;
                                            b = b / 3 * 2;
                                        }

                                        if (Settings.GetBoolean(Key.HIGHLIGHT_ITEMS) && tile.Items.Count() > 0 && tile.Items[0].isBorder() == false)
                                        {
                                            g = g / 3 * 2;
                                            r = r / 3 * 2;
                                        }

                                        if (tile.spawn_count > 0 && Settings.GetBoolean(Key.SHOW_SPAWNS))
                                        {
                                            float f = 1.0f;
                                            for (uint i = 0; i < tile.spawn_count; ++i)
                                            {
                                                f *= 0.7f;
                                            }
                                            g = (int)(g * f);
                                            b = (int)(b * f);
                                        }

                                        if (tile.isHouseTile() && Settings.GetBoolean(Key.SHOW_HOUSES))
                                        {
                                            if (tile.getHouseID() == current_house_id)
                                            {
                                                r /= 2;
                                            }
                                            else
                                            {
                                                r /= 2;
                                                g /= 2;
                                            }
                                        }
                                        else if (showspecial && tile.hasMapFlag(TileState.TILESTATE_PROTECTIONZONE))
                                        {
                                            r /= 2;
                                            b /= 2;
                                        }

                                        if (showspecial && tile.hasMapFlag(TileState.TILESTATE_PVPZONE))
                                        {
                                            g = r / 4;
                                            b = b / 3 * 2;
                                        }

                                        if (showspecial && tile.hasMapFlag(TileState.TILESTATE_NOLOGOUT))
                                        {
                                            b /= 2;
                                        }
                                        if (showspecial && tile.hasMapFlag(TileState.TILESTATE_NOPVP))
                                        {
                                            g /= 2;
                                        }


                                        BlitItem(ref draw_x, ref draw_y, tile, tile.Ground, true, r, g, b, 160);
                                    }

                                    if (editor.Zoom < 10.0 || Settings.GetBoolean(Key.HIDE_ITEMS_WHEN_ZOOMED) == false)
                                    {
                                        foreach (Item it in tile.Items)
                                            if (it.isBorder())
                                            {
                                                BlitItem(ref draw_x, ref draw_y, tile, it, true, 160, r, g, b);
                                            }
                                            else
                                            {
                                                BlitItem(ref draw_x, ref draw_y, tile, it, true, 160, 160, 160, 160);
                                            }
                                    }
                                    /*
                                    if (tile.creature && settings.getInteger(Config.SHOW_CREATURES))
                                    {
                                        BlitCreature(draw_x, draw_y, tile.creature);
                                    } */
                                }
                            }
                        }
                    }
                }


                for (int az = start_z; az >= 0; az--)
                {
                    int map_z = az;
                    if (secondary_map != null)
                    {
                        Position normalPos = new Position();
                        Position to = getMousePosition();
                        if (isPasting() == true)
                        {
                            normalPos = editor.copybuffer.getPosition();
                        }
                        else if (EditorPalette.getSelectedPalette().GetSelectedBrush() as DoodadBrush != null)
                        {
                            normalPos = new Position(0x8000, 0x8000, 0x8);
                        }
                        for (int map_x = start_x; map_x <= end_x; map_x++)
                        {
                            for (int map_y = start_y; map_y <= end_y; map_y++)
                            {
                                Position final = new Position(map_x, map_y, map_z);
                                Position pos = normalPos + final - to;
                                //Position pos = topos + copypos - Position(map_x, map_y, map_z);
                                if (pos.z >= 15 || pos.z < 0)
                                {
                                    continue;
                                }
                                Tile tile = secondary_map.getTile(pos);

                                if (tile != null)
                                {
                                    int draw_x = ((map_x * 32) - view_scroll_x) - ((floor - map_z) * 32); //(15-map_z)*32-(15-floor)*32;
                                    int draw_y = ((map_y * 32) - view_scroll_y) - ((floor - map_z) * 32); //(15-map_z)*32-(15-floor)*32;

                                    int r = 160;
                                    int g = 160;
                                    int b = 160;
                                    if (tile.Ground != null)
                                    {
                                        bool showspecial = Settings.GetBoolean(Key.SHOW_SPECIAL_TILES);

                                        if (tile.isBlocking() && Settings.GetBoolean(Key.SHOW_BLOCKING))
                                        {
                                            g = g / 3 * 2;
                                            b = b / 3 * 2;
                                        }

                                        if (Settings.GetBoolean(Key.HIGHLIGHT_ITEMS) && tile.Items.Count() > 0 && tile.Items[0].isBorder() == false)
                                        {
                                            g = g / 3 * 2;
                                            r = r / 3 * 2;
                                        }

                                        if (tile.spawn_count > 0 && Settings.GetBoolean(Key.SHOW_SPAWNS))
                                        {
                                            float f = 1.0f;
                                            for (uint i = 0; i < tile.spawn_count; ++i)
                                            {
                                                f *= 0.7f;
                                            }
                                            g = (int)(g * f);
                                            b = (int)(b * f);
                                        }

                                        if (tile.isHouseTile() && Settings.GetBoolean(Key.SHOW_HOUSES))
                                        {
                                            if (tile.getHouseID() == current_house_id)
                                            {
                                                r /= 2;
                                            }
                                            else
                                            {
                                                r /= 2;
                                                g /= 2;
                                            }
                                        }
                                        else if (showspecial && tile.hasMapFlag(TileState.TILESTATE_PROTECTIONZONE))
                                        {
                                            r /= 2;
                                            b /= 2;
                                        }

                                        if (showspecial && tile.hasMapFlag(TileState.TILESTATE_PVPZONE))
                                        {
                                            g = r / 4;
                                            b = b / 3 * 2;
                                        }

                                        if (showspecial && tile.hasMapFlag(TileState.TILESTATE_NOLOGOUT))
                                        {
                                            b /= 2;
                                        }
                                        if (showspecial && tile.hasMapFlag(TileState.TILESTATE_NOPVP))
                                        {
                                            g /= 2;
                                        }


                                        BlitItem(ref draw_x, ref draw_y, tile, tile.Ground, true, r, g, b, 160);
                                    }

                                    if (editor.Zoom < 10.0 || Settings.GetBoolean(Key.HIDE_ITEMS_WHEN_ZOOMED) == false)
                                    {
                                        foreach (Item it in tile.Items)
                                            if (it.isBorder())
                                            {
                                                BlitItem(ref draw_x, ref draw_y, tile, it, true, 160, r, g, b);
                                            }
                                            else
                                            {
                                                BlitItem(ref draw_x, ref draw_y, tile, it, true, 160, 160, 160, 160);
                                            }
                                    }
                                    /*
                                    if (tile.creature && settings.getInteger(Config.SHOW_CREATURES))
                                    {
                                        BlitCreature(draw_x, draw_y, tile.creature);
                                    } */
                                }
                            }
                        }
                    }
                }
                editor.DoUpdate(posRefresh);

                // Draw dragging shadow
                if (!editor.selection.isBusy() && dragging)
                {
                    foreach (Tile tile in editor.selection.getTiles())
                    {
                        Position pos = tile.getPosition();

                        int move_x;
                        int move_y;
                        int move_z;
                        move_x = drag_start_x - mouse_map_x;
                        move_y = drag_start_y - mouse_map_y;
                        move_z = drag_start_z - floor;

                        pos.x -= move_x;
                        pos.y -= move_y;
                        pos.z -= move_z;

                        if (pos.z < 0 || pos.z >= 16)
                        {
                            continue;
                        }

                        // On screen and dragging?
                        if (pos.x + 2 > start_x && pos.x < end_x && pos.y + 2 > start_y && pos.y < end_y && (move_x != 0 || move_y != 0 || move_z != 0))
                        {
                            int draw_x = pos.x * 32 - view_scroll_x;
                            if (pos.z <= 7)
                            {
                                draw_x -= (7 - pos.z) * 32;
                            }
                            else
                            {
                                draw_x -= 32 * (floor - pos.z);
                            }
                            int draw_y = pos.y * 32 - view_scroll_y;
                            if (pos.z <= 7)
                            {
                                draw_y -= (7 - pos.z) * 32;
                            }
                            else
                            {
                                draw_y -= 32 * (floor - pos.z);
                            }
                            List<Item> toRender = tile.getSelectedItems();
                            Tile desttile = editor.map.getTile(pos);
                            foreach (Item iit in toRender)
                            {
                                if (desttile != null)
                                {
                                    BlitItem(ref draw_x, ref draw_y, desttile, iit, true, 160, 160, 160, 160);
                                }
                                else
                                {
                                    BlitItem(ref draw_x, ref draw_y, pos, iit, true, 160, 160, 160, 160);
                                }
                            }
                            /*
                            if (tile.creature && tile.creature.isSelected() && settings.getInteger(Config.SHOW_CREATURES))
                            {
                                //BlitCreature(draw_x, draw_y, tile.creature);
                            }
                            if (tile.spawn && tile.spawn.isSelected())
                            {
                                BlitSpriteType(draw_x, draw_y, SPRITE_SPAWN, 160, 160, 160, 160);
                            } */
                        }
                    }
                }



                GL.Disable(EnableCap.Texture2D);

                

                if (editor.isEditionMode())
                {

                    Brush brush = EditorPalette.getSelectedPalette().GetSelectedBrush();

                    RAWBrush rawbrush = brush as RAWBrush;
                    TerrainBrush terrainbrush = brush as TerrainBrush;
                    WallBrush wall_brush = brush as WallBrush;
                    TableBrush table_brush = brush as TableBrush;
                    CarpetBrush carpet_brush = brush as CarpetBrush;
                    DoorBrush door_brush = brush as DoorBrush;
                    OptionalBorderBrush optional_brush = brush as OptionalBorderBrush;
                    CreatureBrush creature_brush = brush as CreatureBrush;
                    SpawnBrush spawn_brush = brush as SpawnBrush;
                    HouseBrush house_brush = brush as HouseBrush;
                    HouseExitBrush house_exit_brush = brush as HouseExitBrush;
                    WaypointBrush waypoint_brush = brush as WaypointBrush;
                    FlagBrush flag_brush = brush as FlagBrush;
                    EraserBrush eraser = brush as EraserBrush;

                    Position mousePos = getMousePosition();
                    int brush_size = EditorPalette.GetBrushSize();                    

                    if (dragging_draw)
                    {
                        if (wall_brush != null)
                        {
                            int last_click_start_map_x = Math.Min(last_click_map_x, mouse_map_x);
                            int last_click_start_map_y = Math.Min(last_click_map_y, mouse_map_y);
                            int last_click_end_map_x = Math.Max(last_click_map_x, mouse_map_x) + 1;
                            int last_click_end_map_y = Math.Max(last_click_map_y, mouse_map_y) + 1;

                            int last_click_start_sx = last_click_start_map_x * 32 - view_scroll_x - getFloorAdjustment(floor);
                            int last_click_start_sy = last_click_start_map_y * 32 - view_scroll_y - getFloorAdjustment(floor);
                            int last_click_end_sx = last_click_end_map_x * 32 - view_scroll_x - getFloorAdjustment(floor);
                            int last_click_end_sy = last_click_end_map_y * 32 - view_scroll_y - getFloorAdjustment(floor);

                            int delta_x = last_click_end_sx - last_click_start_sx;
                            int delta_y = last_click_end_sy - last_click_start_sy;

                            GL.Color4(Color.FromArgb(128, 0, 166, 0));
                            GL.Begin(BeginMode.Quads);

                            if (true)
                            {
                                GL.Vertex2(last_click_start_sx, last_click_start_sy + 32);
                                GL.Vertex2(last_click_end_sx, last_click_start_sy + 32);
                                GL.Vertex2(last_click_end_sx, last_click_start_sy);
                                GL.Vertex2(last_click_start_sx, last_click_start_sy);
                            }

                            if (delta_y > 32)
                            {
                                GL.Vertex2(last_click_start_sx, last_click_end_sy - 32);
                                GL.Vertex2(last_click_start_sx + 32, last_click_end_sy - 32);
                                GL.Vertex2(last_click_start_sx + 32, last_click_start_sy + 32);
                                GL.Vertex2(last_click_start_sx, last_click_start_sy + 32);
                            }

                            if (delta_x > 32 && delta_y > 32)
                            {
                                GL.Vertex2(last_click_end_sx - 32, last_click_start_sy + 32);
                                GL.Vertex2(last_click_end_sx, last_click_start_sy + 32);
                                GL.Vertex2(last_click_end_sx, last_click_end_sy - 32);
                                GL.Vertex2(last_click_end_sx - 32, last_click_end_sy - 32);
                            }

                            if (delta_y > 32)
                            {
                                GL.Vertex2(last_click_start_sx, last_click_end_sy - 32);
                                GL.Vertex2(last_click_end_sx, last_click_end_sy - 32);
                                GL.Vertex2(last_click_end_sx, last_click_end_sy);
                                GL.Vertex2(last_click_start_sx, last_click_end_sy);
                            }
                            GL.End();
                        }
                        else
                        {
                            if (rawbrush != null) // Textured brush
                            {
                                GL.Enable(EnableCap.Texture2D);
                            }
                        }
                    }
                    else
                    {
                        if (wall_brush != null)
                        {

                            int start_map_x = mouse_map_x - brush_size;
                            int start_map_y = mouse_map_y - brush_size;
                            int end_map_x = mouse_map_x + brush_size + 1;
                            int end_map_y = mouse_map_y + brush_size + 1;

                            int start_sx = start_map_x * 32 - view_scroll_x - getFloorAdjustment(floor);
                            int start_sy = start_map_y * 32 - view_scroll_y - getFloorAdjustment(floor);
                            int end_sx = end_map_x * 32 - view_scroll_x - getFloorAdjustment(floor);
                            int end_sy = end_map_y * 32 - view_scroll_y - getFloorAdjustment(floor);

                            int delta_x = end_sx - start_sx;
                            int delta_y = end_sy - start_sy;

                            GL.Color4(Color.FromArgb(128, 0, 166, 0));
                            GL.Begin(BeginMode.Quads);

                            if (true)
                            {
                                GL.Vertex2(start_sx, start_sy + 32);
                                GL.Vertex2(end_sx, start_sy + 32);
                                GL.Vertex2(end_sx, start_sy);
                                GL.Vertex2(start_sx, start_sy);
                            }
                            if (delta_y > 32)
                            {
                                GL.Vertex2(start_sx, end_sy - 32);
                                GL.Vertex2(start_sx + 32, end_sy - 32);
                                GL.Vertex2(start_sx + 32, start_sy + 32);
                                GL.Vertex2(start_sx, start_sy + 32);
                            }

                            if (delta_x > 32 && delta_y > 32)
                            {
                                GL.Vertex2(end_sx - 32, start_sy + 32);
                                GL.Vertex2(end_sx, start_sy + 32);
                                GL.Vertex2(end_sx, end_sy - 32);
                                GL.Vertex2(end_sx - 32, end_sy - 32);
                            }

                            if (delta_y > 32)
                            {
                                GL.Vertex2(start_sx, end_sy - 32);
                                GL.Vertex2(end_sx, end_sy - 32);
                                GL.Vertex2(end_sx, end_sy);
                                GL.Vertex2(start_sx, end_sy);
                            }
                            GL.End();
                        }
                        else if (door_brush != null)
                        {
                            int cx = (mouse_map_x) * 32 - view_scroll_x - getFloorAdjustment(floor);
                            int cy = (mouse_map_y) * 32 - view_scroll_y - getFloorAdjustment(floor);

                            if (door_brush.canDraw(editor.map, new Position(mouse_map_x, mouse_map_y, floor)))
                            {
                                GL.Color4(Color.FromArgb(128, 0, 166, 0));
                            }
                            else
                            {
                                GL.Color4(Color.FromArgb(128, 166, 0, 0));
                            }
                            GL.Begin(BeginMode.Quads);
                            GL.Vertex2(cx, cy + 32);
                            GL.Vertex2(cx + 32, cy + 32);
                            GL.Vertex2(cx + 32, cy);
                            GL.Vertex2(cx, cy);
                            GL.End();
                        }
                        else if (creature_brush != null)
                        {
                            GL.Enable(EnableCap.Texture2D);
                            int cy = (mouse_map_y) * 32 - view_scroll_y - getFloorAdjustment(floor);
                            int cx = (mouse_map_x) * 32 - view_scroll_x - getFloorAdjustment(floor);
                            if (creature_brush.canDraw(editor.map, new Position(mouse_map_x, mouse_map_y, floor)))
                            {
                                BlitCreature(cx, cy, creature_brush.getType().outfit, 255, 255, 255, 160);
                            }
                            else
                            {
                                BlitCreature(cx, cy, creature_brush.getType().outfit, 255, 64, 64, 160);
                            }
                            GL.Disable(EnableCap.Texture2D);                            
                        }
                        else if ((brush != null)  && (!brush.GetType().Equals(typeof(DoodadBrush))))
                        {
                            if (rawbrush != null) // Textured brush
                            {
                                GL.Enable(EnableCap.Texture2D);
                            }
                            for (int y = -brush_size - 1; y <= brush_size + 1; y++)
                            {
                                int cy = (mouse_map_y + y) * 32 - view_scroll_y - getFloorAdjustment(floor);
                                for (int x = -brush_size - 1; x <= brush_size + 1; x++)
                                {
                                    int cx = (mouse_map_x + x) * 32 - view_scroll_x - getFloorAdjustment(floor);
                                    if (EditorPalette.GetBrushShape() == BrushShape.BRUSHSHAPE_SQUARE)
                                    {
                                        if (x >= -brush_size && x <= brush_size && y >= -brush_size && y <= brush_size)
                                        {
                                            if (rawbrush != null)
                                            {
                                                BlitSpriteType(cx, cy, rawbrush.itemtype.gameSprite, 160, 160, 160, 160);
                                            }
                                            else
                                            {
                                                if ((terrainbrush != null) || (table_brush != null) || (carpet_brush != null))
                                                {
                                                    GL.Color4(Color.FromArgb(128, 0, 166, 0));
                                                }
                                                else if ((spawn_brush != null) || (eraser != null))
                                                {
                                                    GL.Color4(Color.FromArgb(128, 166, 0, 0));
                                                }
                                                else if (flag_brush != null)
                                                {
                                                    GL.Color4(Color.FromArgb(128, 0, 166, 0));
                                                }
                                                else if ((house_brush != null) || (waypoint_brush != null) || (house_exit_brush != null))
                                                {
                                                    if (house_exit_brush == null)
                                                    {
                                                        GL.Color4(Color.FromArgb(128, 0, 166, 166));
                                                    }
                                                    else if (brush.canDraw(editor.map, new Position(mouse_map_x + x, mouse_map_y + y, floor)))
                                                    {
                                                        // Can draw exit/waypoint                                                    
                                                        GL.Color4(Color.FromArgb(128, 0, 166, 166));
                                                    }
                                                    else
                                                    {
                                                        // Can't draw exit/waypoint                                                    
                                                        GL.Color4(Color.FromArgb(128, 166, 0, 0));
                                                    }
                                                }
                                                else if (optional_brush != null)
                                                {
                                                    if (optional_brush.canDraw(editor.map, new Position(mouse_map_x + x, mouse_map_y + y, floor)))
                                                    {
                                                        GL.Color4(Color.FromArgb(128, 0, 166, 0));
                                                    }
                                                    else
                                                    {
                                                        GL.Color4(Color.FromArgb(128, 166, 0, 0));
                                                    }
                                                }
                                                GL.Begin(BeginMode.Quads);
                                                GL.Vertex2(cx, cy + 32);
                                                GL.Vertex2(cx + 32, cy + 32);
                                                GL.Vertex2(cx + 32, cy);
                                                GL.Vertex2(cx, cy);
                                                GL.End();
                                            }
                                        }
                                    }
                                    else if (EditorPalette.GetBrushShape() == BrushShape.BRUSHSHAPE_CIRCLE)
                                    {
                                        double distance = Math.Sqrt((double)(x * x) + (double)(y * y));
                                        if (distance < brush_size + 0.005)
                                        {
                                            if (rawbrush != null)
                                            {
                                                BlitSpriteType(cx, cy, rawbrush.itemtype.gameSprite, 160, 160, 160, 160);
                                            }
                                            else
                                            {
                                                if ((terrainbrush != null) || (table_brush != null) || (carpet_brush != null))
                                                {
                                                    GL.Color4(Color.FromArgb(128, 0, 166, 0));
                                                }
                                                else if ((spawn_brush != null) || (eraser != null))
                                                {
                                                    GL.Color4(Color.FromArgb(128, 166, 0, 0));
                                                }
                                                else if (flag_brush != null)
                                                {
                                                    GL.Color4(Color.FromArgb(128, 166, 166, 0));
                                                }
                                                else if ((house_brush != null) || (waypoint_brush != null) || (house_exit_brush != null))
                                                {
                                                    if (house_exit_brush == null)
                                                    {
                                                        GL.Color4(Color.FromArgb(128, 0, 166, 166));
                                                    }
                                                    else if (brush.canDraw(editor.map, new Position(mouse_map_x + x, mouse_map_y + y, floor)))
                                                    {
                                                        // Can draw exit/waypoint                                                    
                                                        GL.Color4(Color.FromArgb(128, 0, 166, 166));
                                                    }
                                                    else
                                                    {
                                                        // Can't draw exit/waypoint                                                    
                                                        GL.Color4(Color.FromArgb(128, 166, 0, 0));
                                                    }
                                                }
                                                else if (optional_brush != null)
                                                {
                                                    if (optional_brush.canDraw(editor.map, new Position(mouse_map_x + x, mouse_map_y + y, floor)))
                                                    {
                                                        GL.Color4(Color.FromArgb(128, 0, 166, 0));
                                                    }
                                                    else
                                                    {
                                                        GL.Color4(Color.FromArgb(128, 0, 0, 166));
                                                    }
                                                }
                                                GL.Begin(BeginMode.Quads);
                                                GL.Vertex2(cx, cy + 32);
                                                GL.Vertex2(cx + 32, cy + 32);
                                                GL.Vertex2(cx + 32, cy);
                                                GL.Vertex2(cx, cy);
                                                GL.End();
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }
                }


                glBlitSelectionQuad();

                GL.Enable(EnableCap.Texture2D);
                GL.MatrixMode(MatrixMode.Projection);
                GL.PopMatrix();
                GL.MatrixMode(MatrixMode.Modelview);
                GL.PopMatrix();

                this.SwapBuffers();
            }
            catch (Exception ex)
            {
                Generic.AjustError(ex);
            }
        }


        #endregion


        public void UpdateTilePosition(Tile tile, Position pos)
        {
            Position newPosition = new Position(
                tile.Position.X + pos.X,
                tile.Position.Y + pos.Y,
                tile.Position.Z);
            tile.Position = newPosition;
        }


        public Position GetTilePosition(Tile tile, Position pos)
        {
            Position newPosition = new Position(
                tile.Position.X + pos.X,
                tile.Position.Y + pos.Y,
                tile.Position.Z);
            return newPosition;
        }

        public void getTilesToDraw(Position mousePos, List<Position> tilestodraw, List<Position> tilestoborder)
        {
            int size = EditorPalette.getSelectedPalette().getQtTiles();

            for (int y = -size - 1; y <= size + 1; y++)
            {
                for (int x = -size - 1; x <= size + 1; x++)
                {
                    if (EditorPalette.GetBrushShape() == BrushShape.BRUSHSHAPE_SQUARE)
                    {
                        if(x >= -size && x <= size && y >= -size && y <= size) 
                        {
					        if(tilestodraw != null) 
                                tilestodraw.Add(new Position(mousePos.X+x,mousePos.Y+y, mousePos.Z));
				        }
                        if ((Math.Abs(x) - size < 2) && (Math.Abs(y) - size < 2)) 
                        {
					        if(tilestoborder != null)
                                tilestoborder.Add(new Position(mousePos.X + x, mousePos.Y + y, mousePos.Z));
				        }
                    }
                    else if (EditorPalette.GetBrushShape() == BrushShape.BRUSHSHAPE_CIRCLE)
                    {
                        double distance =  Math.Sqrt((x*x) + (y*y));
                        if(distance < size+0.005) {
					        if(tilestodraw != null)
						        tilestodraw.Add(new Position(mousePos.X+x,mousePos.Y+y, mousePos.Z));
				        }
                        if (Math.Abs(distance - size) < 1.5)
                        {
					        if(tilestoborder != null)
                                tilestoborder.Add(new Position(mousePos.X + x, mousePos.Y + y, mousePos.Z));
				        }
                    }
                }
            }
        }

        public Position getMousePosition()
        {
            double zoom = getMapEditor().Zoom;
            int map_x = (getMapEditor().getViewX() + (int)(cursor_x / zoom)) / 32;
            int map_y = (getMapEditor().getViewY() + (int)(cursor_y / zoom)) / 32;
            return new Position(map_x, map_y, getMapEditor().MapZ);
        }

        public MapEditor getMapEditor()
        {            
            return (MapEditor)this.Parent.Parent;
        }

        public bool hasMapEditor()
        {
            return (this.Parent != null) && (this.Parent.Parent != null);
        }

        private MainForm getMainForm()
        {
            return Global.mainForm;
        }

        private void MapCanvas_Leave(object sender, EventArgs e)
        {
            drawing = false;
            shiftActive = false;
            ctrlActive = false;
            altActive = false;
        }

        private void MapCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            cursor_x = e.X;
            cursor_y = e.Y;
            UpdateMouse();
            if ((boundbox_selection) && (getMapEditor().Zoom > 0.5))
            {
                Refresh();
            }
        }

        private void UpdateMouse()
        {
            if (!oldMousePos.Equals(getMousePosition()))
            {                
                oldMousePos = getMousePosition();
                getMainForm().UpdateScreenPosition(getMousePosition());

                //if (getMapEditor().

                if (getMapEditor().isSelectionMode())
                {
                    if (isPasting())
                    {
                        Refresh();
                    }
                    else if (dragging)
                    {
                        // atualizar a action
                        int move_x = drag_start_x - getMousePosition().X;
                        int move_y = drag_start_y - getMousePosition().Y;
                        int move_z = drag_start_z - getMapEditor().MapZ;

                    }
                    else if (boundbox_selection)
                    {
                        int move_x = Math.Abs(last_click_map_x - getMousePosition().X);
                        int move_y = Math.Abs(last_click_map_y - getMousePosition().Y);
                        if (getMapEditor().Zoom < 0.5)
                        {
                            Refresh();
                        }
                    }
                } 
                else if (getMapEditor().isEditionMode())
                {
                    if (drawing)
                    {
                        getMapEditor().DrawSelection();
                    }
                    Refresh();
                }

               DoodadBrush.FillDoodadPreviewBuffer();
            }
        }

        private void MapCanvas_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                int screensize_x = this.Width;
                int screensize_y = this.Height;
                double zoom = getMapEditor().Zoom;

                double pos_x_old = cursor_x / zoom;
                double pos_y_old = cursor_y / zoom;

                double diff = (e.Delta * 2) * (zoom / 4) / 640.0;
                double oldzoom = zoom;
                if (zoom < 0.125)
                {
                    diff = 0.125 - oldzoom;
                    zoom = 0.125;
                }
                if (zoom > 25.00) { diff = 25.00 - oldzoom; zoom = 25.0; }


                getMapEditor().Zoom += diff;

                //(
                double pos_x_new = cursor_x / getMapEditor().Zoom;
                double pos_y_new = cursor_y / getMapEditor().Zoom;

                getMapEditor().UpdatePosScrollBar(
                    Convert.ToInt32(pos_x_old - pos_x_new),
                    Convert.ToInt32(pos_y_old - pos_y_new));

                Refresh();
            }
            catch (Exception ex)
            {
                Generic.AjustError(ex);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                contextMenuStrip_Opening(null, null);
                double zoom = getMapEditor().Zoom;
                int moveSpeed = Convert.ToInt32(4 / zoom);

                if (keyData == (Keys.ShiftKey | Keys.Shift | Keys.Control | Keys.ControlKey))
                {
                    ctrlActive = true;
                    shiftActive = true;
                }


                if (keyData == (Keys.ShiftKey | Keys.Shift))
                {
                    shiftActive = true;
                }

                if (keyData == (Keys.RButton | Keys.ShiftKey | Keys.Alt))
                {

                    altActive = true;
                }

                if (keyData == (Keys.Control | Keys.ControlKey))
                {
                    ctrlActive = true;
                }

                if (keyData == Keys.Right)
                {
                    getMapEditor().UpdatePosScrollBar(32 * moveSpeed, 0);
                    Refresh();
                    return true;
                }
                else if (keyData == Keys.Left)
                {
                    getMapEditor().UpdatePosScrollBar(-32 * moveSpeed, 0);
                    Refresh();
                    return true;
                }
                else if (keyData == Keys.Up)
                {
                    getMapEditor().UpdatePosScrollBar(0, -32 * moveSpeed);
                    Refresh();
                    return true;
                }
                else if (keyData == Keys.Down)
                {
                    getMapEditor().UpdatePosScrollBar(0, 32 * moveSpeed);
                    Refresh();
                    return true;
                }
                else if (keyData == Keys.PageUp)
                {
                    FloorUp();
                    return true;
                }
                else if (keyData == Keys.PageDown)
                {
                    FloorDown();
                    return true;
                }
                else if (keyData == Keys.A)
                {
                    bool valor = Settings.GetBoolean(Key.USE_AUTOMAGIC);
                    Settings.SetValue(Key.USE_AUTOMAGIC, !valor);
                    return true;
                }
                else if (keyData == Keys.E)
                {
                    getMapEditor().revertEditionMode();
                    Refresh();
                    return true;
                }
                else if (keyData == Keys.Z)
                {
                    EditorPalette ed = EditorPalette.getSelectedPalette();
                    int nv = ed.GetBrushVariation();
                    --nv;
                    if (nv < 0)
                    {
                        nv = Math.Max(0, (ed.GetSelectedBrush() != null ? ed.GetSelectedBrush().getMaxVariation() - 1 : 0));
                    }
                    ed.SetBrushVariation(nv);
                    Refresh();
                }
                else if (keyData == Keys.X)
                {
                    EditorPalette ed = EditorPalette.getSelectedPalette();
                    int nv = ed.GetBrushVariation();
                    ++nv;
                    if (nv < 0)
                    {
                        nv = Math.Max(0, (ed.GetSelectedBrush() != null ? ed.GetSelectedBrush().getMaxVariation() - 1 : 0));
                    }
                    ed.SetBrushVariation(nv);
                    Refresh();
                }
                else if (keyData == Keys.Add)
                {
                    FloorUp();
                    return true;
                }
                else if (keyData == Keys.Subtract)
                {
                    FloorDown();
                    return true;
                }
                else if (keyData == Keys.Q)
                {
                    Global.mainForm.showShadeToolStripMenuItem_Click(null, null);
                    return true;
                }

                else if (keyData == Keys.J)
                {
                    Generic.JumpToBrush();
                    return true;
                }

                else if (keyData == (Keys.J | Keys.Control))
                {
                    Generic.JumpToItem();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Generic.AjustError(ex);
            }
            return base.ProcessCmdKey(ref msg, keyData);

        }

        private void FloorUp()
        {
            if (getMapEditor().MapZ > 0)
            {
                getMapEditor().MapZ = getMapEditor().MapZ - 1;
                getMapEditor().UpdatePosScrollBar(32, 32);
                Global.mainForm.FloorChange(getMapEditor().MapZ);
                getMainForm().UpdateScreenPosition(getMousePosition());
                Refresh();
            }
        }

        private void FloorDown()
        {
            if (getMapEditor().MapZ < 15)
            {
                getMapEditor().MapZ = getMapEditor().MapZ + 1;
                getMapEditor().UpdatePosScrollBar(-32, -32);
                Global.mainForm.FloorChange(getMapEditor().MapZ);
                getMainForm().UpdateScreenPosition(getMousePosition());
                Refresh();
            }          
        }

        private void copyPositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(getMousePosition().ToString());
        }


        private void MapCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                cursor_x = e.X;
                cursor_y = e.Y;
                UpdateMouse();

                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    MapCanvas_MouseDownLeft(sender, e);
                }
                else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    MapCanvas_MouseDownRight(sender, e);
                }
            }
            catch (Exception ex)
            {
                Generic.AjustError(ex);
            }

        }

        private void MapCanvas_MouseDownLeft(object sender, MouseEventArgs e)
        {
            MapEditor editor = getMapEditor();

            int mouse_map_x = getMousePosition().X; 
            int mouse_map_y = getMousePosition().Y;
            int floor = getMapEditor().MapZ;

            if (getMapEditor().isSelectionMode())
            {
                if (isPasting())
                {
                    // Set paste to false (no rendering etc.)
                    EndPasting();

                    // Paste to the map
                    editor.copybuffer.paste(editor, new Position(mouse_map_x, mouse_map_y, floor));

                    // Start dragging
                    dragging = true;
                    drag_start_x = mouse_map_x;
                    drag_start_y = mouse_map_y;
                    drag_start_z = floor;
                }
                else
                {
                    do
                    {
                        boundbox_selection = false;
                        if (shiftActive)
                        {
                            getMainForm().updateAction("");
                            boundbox_selection = true;

                            if (!ctrlActive)
                            {
                                editor.selection.start(); // Start selection session
                                editor.selection.clear(); // Clear out selection
                                editor.selection.finish(); // End selection session
                                editor.selection.updateSelectionCount();                                
                            } 
                        }
                        else if (ctrlActive)
                        {
                            Tile tile = editor.map.getTile(mouse_map_x, mouse_map_y, floor);
                            if (tile != null)
                            {
                                if ((tile.spawn != null) && Settings.GetBoolean(Key.SHOW_SPAWNS))
                                {
                                    editor.selection.start(); // Start selection session
                                    if (tile.spawn.isSelected())
                                    {
                                        editor.selection.remove(tile, tile.spawn);
                                    }
                                    else
                                    {
                                        editor.selection.add(tile, tile.spawn);
                                    }
                                    editor.selection.finish(); // Finish selection session
                                    editor.selection.updateSelectionCount();
                                }
                                else if ((tile.creature != null) && Settings.GetBoolean(Key.SHOW_CREATURES))
                                {
                                    editor.selection.start(); // Start selection session
                                    if (tile.creature.isSelected())
                                    {
                                        editor.selection.remove(tile, tile.creature);
                                    }
                                    else
                                    {
                                        editor.selection.add(tile, tile.creature);
                                    }
                                    editor.selection.finish(); // Finish selection session
                                    editor.selection.updateSelectionCount();
                                }
                                else
                                {
                                    Item item = tile.getTopItem();
                                    if (item != null)
                                    {
                                        editor.selection.start(); // Start selection session
                                        if (item.isSelected())
                                        {
                                            editor.selection.remove(tile, item);
                                        }
                                        else
                                        {
                                            editor.selection.add(tile, item);
                                        }
                                        editor.selection.finish(); // Finish selection session
                                        editor.selection.updateSelectionCount();
                                    }
                                }
                            }
                        }
                        else
                        {
                            Tile tile = editor.map.getTile(mouse_map_x, mouse_map_y, floor);
                            if (tile == null)
                            {
                                editor.selection.start(); // Start selection session
                                editor.selection.clear(); // Clear out selection
                                editor.selection.finish(); // End selection session
                                editor.selection.updateSelectionCount();
                            }
                            else if (tile.isSelected())
                            {
                                dragging = true;
                                drag_start_x = mouse_map_x;
                                drag_start_y = mouse_map_y;
                                drag_start_z = floor;
                            }
                            else
                            {
                                editor.selection.start(); // Start a selection session
                                editor.selection.clear();
                                editor.selection.commit();
                                if ((tile.spawn != null) && Settings.GetBoolean(Key.SHOW_SPAWNS))
                                {
                                    editor.selection.add(tile, tile.spawn);
                                    dragging = true;
                                    drag_start_x = mouse_map_x;
                                    drag_start_y = mouse_map_y;
                                    drag_start_z = floor;
                                }
                                else if ((tile.creature != null) && Settings.GetBoolean(Key.SHOW_CREATURES))
                                {
                                    editor.selection.add(tile, tile.creature);
                                    dragging = true;
                                    drag_start_x = mouse_map_x;
                                    drag_start_y = mouse_map_y;
                                    drag_start_z = floor;
                                }
                                else
                                {
                                    Item item = tile.getTopItem();
                                    if (item != null)
                                    {
                                        editor.selection.add(tile, item);
                                        dragging = true;
                                        drag_start_x = mouse_map_x;
                                        drag_start_y = mouse_map_y;
                                        drag_start_z = floor;
                                    }
                                }
                                editor.selection.finish(); // Finish the selection session
                                editor.selection.updateSelectionCount();
                            }
                        }

                    } while (false);
                }
            } 
            else if (getMapEditor().isEditionMode())
            {
                // TODO ajustar os métodos oneSizeFitsAll e canSmear
                /*
                if (EditorPalette.GetBrushSize() == 0 && (!EditorPalette.GetCurrentBrush().oneSizeFitsAll()))
                {
                    drawing = true;
                }
                else
                {
                    drawing = EditorPalette.GetCurrentBrush().canSmear();
                } */
                drawing = true;
                getMapEditor().DrawSelection();
            }

            last_click_x = cursor_x;
            last_click_y = cursor_y;

            last_click_map_x = mouse_map_x;
            last_click_map_y = mouse_map_y;
            last_click_map_z = editor.MapZ;
            Refresh();
        }

        private void MapCanvas_MouseDownRight(object sender, MouseEventArgs e)
        {
            getMapEditor().setSelectionMode();

            MapEditor editor = getMapEditor();
            int mouse_map_x = getMousePosition().X;
            int mouse_map_y = getMousePosition().Y;
            int floor = getMapEditor().MapZ;

            Tile tile = editor.map.getTile(mouse_map_x, mouse_map_y, floor);
            if (tile == null)
            {
                editor.selection.start(); // Start selection session
                editor.selection.clear(); // Clear out selection
                editor.selection.finish(); // End selection session
                editor.selection.updateSelectionCount();
            }
            else if (!tile.isSelected())
            {
                editor.selection.start(); // Start a selection session
                editor.selection.clear();
                editor.selection.commit();
                if ((tile.spawn != null) && Settings.GetBoolean(Key.SHOW_SPAWNS))
                {
                    editor.selection.add(tile, tile.spawn);

                }
                else if ((tile.creature != null) && Settings.GetBoolean(Key.SHOW_CREATURES))
                {
                    editor.selection.add(tile, tile.creature);

                }
                else
                {
                    Item item = tile.getTopItem();
                    if (item != null)
                    {
                        editor.selection.add(tile, item);
                    }
                }
                editor.selection.finish(); // Finish the selection session
                editor.selection.updateSelectionCount();
            }          
            Refresh();            
        }

        private void MapCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {

                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    return;
                }

                int mouse_map_x = getMousePosition().X;
                int mouse_map_y = getMousePosition().Y;
                int floor = getMapEditor().MapZ;

                int move_x = last_click_map_x - mouse_map_x;
                int move_y = last_click_map_y - mouse_map_y;
                int move_z = last_click_map_z - floor;

                MapEditor editor = getMapEditor();

                if (getMapEditor().isSelectionMode())
                {
                    if (dragging && (move_x != 0 || move_y != 0 || move_z != 0))
                    {
                        editor.moveSelection(new Position(move_x, move_y, move_z));
                    }
                    else
                    {
                        if (boundbox_selection)
                        {
                            if (mouse_map_x == last_click_map_x && mouse_map_y == last_click_map_y && ctrlActive)
                            {
                                // Mouse hasn't moved, do control+shift thingy!
                                Tile tile = editor.gameMap.getTile(mouse_map_x, mouse_map_y, floor);
                                if (tile != null)
                                {
                                    editor.selection.start(); // Start a selection session
                                    if (tile.isSelected())
                                    {
                                        editor.selection.remove(tile);
                                    }
                                    else
                                    {
                                        editor.selection.add(tile);
                                    }
                                    editor.selection.finish(); // Finish the selection session
                                    editor.selection.updateSelectionCount();
                                }
                            }
                            else
                            {
                                // The cursor has moved, do some boundboxing!
                                if (last_click_map_x > mouse_map_x)
                                {
                                    int tmp = mouse_map_x;
                                    mouse_map_x = last_click_map_x;
                                    last_click_map_x = tmp;
                                }
                                if (last_click_map_y > mouse_map_y)
                                {
                                    int tmp = mouse_map_y;
                                    mouse_map_y = last_click_map_y;
                                    last_click_map_y = tmp;
                                }

                                int numtiles = 0;
                                int threadcount = Math.Max(Settings.GetInteger(Key.WORKER_THREADS), 1);

                                int start_x = 0;
                                int start_y = 0;
                                int start_z = 0;
                                int end_x = 0;
                                int end_y = 0;
                                int end_z = 0;

                                switch (Settings.GetInteger(Key.SELECTION_TYPE))
                                {
                                    case SelectionType.SELECT_CURRENT_FLOOR:
                                        {
                                            start_z = end_z = floor;
                                            start_x = last_click_map_x;
                                            start_y = last_click_map_y;
                                            end_x = mouse_map_x;
                                            end_y = mouse_map_y;
                                        }
                                        break;
                                    case SelectionType.SELECT_ALL_FLOORS:
                                        {
                                            start_x = last_click_map_x;
                                            start_y = last_click_map_y;
                                            start_z = 16 - 1;
                                            end_x = mouse_map_x;
                                            end_y = mouse_map_y;
                                            end_z = floor;

                                            if (Settings.GetBoolean(Key.COMPENSATED_SELECT))
                                            {
                                                start_x -= (floor < 7 ? 7 - floor : 0);
                                                start_y -= (floor < 7 ? 7 - floor : 0);

                                                end_x -= (floor < 7 ? 7 - floor : 0);
                                                end_y -= (floor < 7 ? 7 - floor : 0);
                                            }

                                            numtiles = (start_z - end_z) * (end_x - start_x) * (end_y - start_y);
                                        }
                                        break;
                                    case SelectionType.SELECT_VISIBLE_FLOORS:
                                        {
                                            start_x = last_click_map_x;
                                            start_y = last_click_map_y;
                                            if (floor < 8)
                                            {
                                                start_z = 7;
                                            }
                                            else
                                            {
                                                start_z = Math.Min(15, floor + 2);
                                            }
                                            end_x = mouse_map_x;
                                            end_y = mouse_map_y;
                                            end_z = floor;

                                            if (Settings.GetBoolean(Key.COMPENSATED_SELECT))
                                            {
                                                start_x -= (floor < 7 ? 7 - floor : 0);
                                                start_y -= (floor < 7 ? 7 - floor : 0);

                                                end_x -= (floor < 7 ? 7 - floor : 0);
                                                end_y -= (floor < 7 ? 7 - floor : 0);
                                            }
                                        }
                                        break;
                                }
                                if (numtiles < 500)
                                {
                                    // No point in threading for such a small set.
                                    threadcount = 1;
                                }
                                // Subdivide the selection area
                                // We know it's a square, just split it into several areas
                                int width = end_x - start_x;
                                if (width < threadcount)
                                {
                                    threadcount = Math.Min(1, width);
                                }
                                // Let's divide!
                                int remainder = width;
                                int cleared = 0;
                                List<SelectionThread> threads = new List<SelectionThread>();
                                if (width == 0)
                                {
                                    threads.Add(new SelectionThread(editor, new Position(start_x, start_y, start_z), new Position(start_x, end_y, end_z)));
                                }
                                else
                                {
                                    for (int i = 0; i < threadcount; ++i)
                                    {
                                        int chunksize = width / threadcount;
                                        // The last threads takes all the remainder
                                        if (i == threadcount - 1)
                                        {
                                            chunksize = remainder;
                                        }
                                        threads.Add(new SelectionThread(editor, new Position(start_x + cleared, start_y, start_z), new Position(start_x + cleared + chunksize, end_y, end_z)));
                                        cleared += chunksize;
                                        remainder -= chunksize;
                                    }
                                }
                                Debug.Assert(cleared == width);
                                Debug.Assert(remainder == 0);

                                editor.selection.start(); // Start a selection session
                                for (List<SelectionThread>.Enumerator iter = threads.GetEnumerator(); iter.MoveNext(); )
                                {
                                    (iter.Current).Execute();
                                }
                                for (List<SelectionThread>.Enumerator iter = threads.GetEnumerator(); iter.MoveNext(); )
                                {
                                    editor.selection.join(iter.Current);
                                }
                                editor.selection.finish(); // Finish the selection session
                                editor.selection.updateSelectionCount();
                            }
                        }
                        else if (ctrlActive)
                        {
                        }
                        else
                        {
                            Tile tile = editor.map.getTile(mouse_map_x, mouse_map_y, floor);
                            if (tile == null)
                            {
                                editor.selection.start(); // Start selection session
                                editor.selection.clear(); // Clear out selection
                                editor.selection.finish(); // End selection session
                                editor.selection.updateSelectionCount();
                            }
                            else if (tile.isSelected())
                            {
                                dragging = true;
                                drag_start_x = mouse_map_x;
                                drag_start_y = mouse_map_y;
                                drag_start_z = floor;
                            }
                            else
                            {
                                editor.selection.start(); // Start a selection session
                                editor.selection.clear();
                                editor.selection.commit();
                                if ((tile.spawn != null) && Settings.GetBoolean(Key.SHOW_SPAWNS))
                                {
                                    editor.selection.add(tile, tile.spawn);
                                    dragging = true;
                                    drag_start_x = mouse_map_x;
                                    drag_start_y = mouse_map_y;
                                    drag_start_z = floor;
                                }
                                else if ((tile.creature != null) && Settings.GetBoolean(Key.SHOW_CREATURES))
                                {
                                    editor.selection.add(tile, tile.creature);
                                    dragging = true;
                                    drag_start_x = mouse_map_x;
                                    drag_start_y = mouse_map_y;
                                    drag_start_z = floor;
                                }
                                else
                                {
                                    Item item = tile.getTopItem();
                                    if (item != null)
                                    {
                                        editor.selection.add(tile, item);
                                        dragging = true;
                                        drag_start_x = mouse_map_x;
                                        drag_start_y = mouse_map_y;
                                        drag_start_z = floor;
                                    }
                                }
                                editor.selection.finish(); // Finish the selection session
                                editor.selection.updateSelectionCount();
                            }
                        }
                    }
                }
                boundbox_selection = false;
                dragging = false;
                drawing = false;
                getMapEditor().queue.resetTimer();
                Refresh();
            }
            catch (Exception ex)
            {
                Generic.AjustError(ex);
            }
        }

        private void MapCanvas_KeyUp(object sender, KeyEventArgs e)
        {
            shiftActive = false;
            ctrlActive = false;
            altActive = false;
        }


        private bool isPasting()
        {
            return pasting;
        }

        private void EndPasting()
        {
            if(pasting) 
            {
		        pasting = false;
		        secondary_map = null;
	        }
        }

        private void StartPasting() {
	        if(getMapEditor() != null) 
            {
		        pasting = true;
		        secondary_map = getMapEditor().copybuffer.getBufferMap();
	        }
        }

        private void contextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                foreach (var it in contextMenuStrip.Items)
                {
                    if (typeof(ToolStripMenuItem).Equals(it.GetType()))
                    {
                        ToolStripMenuItem ts = (ToolStripMenuItem)it;
                        ts.Visible = false;
                        ts.Enabled = false;
                    }
                    else if (typeof(ToolStripSeparator).Equals(it.GetType()))
                    {
                        ToolStripSeparator ss = (ToolStripSeparator)it;
                        ss.Visible = false;
                    }
                }

                copyPositionToolStripMenuItem.Enabled = true;
                pasteToolStripMenuItem.Enabled = getMapEditor().copybuffer.GetTileCount() > 0;

                bool anything_selected = (getMapEditor().selection.getTiles().Count() != 0);
                if (anything_selected)
                {
                    copyToolStripMenuItem.Enabled = true;
                    deleteToolStripMenuItem.Enabled = true;
                    cutToolStripMenuItem.Enabled = true;
                    propertiesToolStripMenuItem.Enabled = true;
                    tssProp.Visible = true;
                    if (getMapEditor().selection.getTiles().Count() == 1)
                    {
                        Tile selectedTile = getMapEditor().selection.getSelectedTile();
                        Item topItem = selectedTile.getTopItem();
                        if (topItem != null)
                        {
                            tssBrush.Visible = true;
                            selectRawTSMI.Enabled = true;
                            selectCarpetBrushTSMI.Enabled = (selectedTile.getCarpetBrush() != null);
                            selectGroundBrushTSMI.Enabled = (selectedTile.getGroundBrush() != null);
                            selectWallbrushTSMI.Enabled = (selectedTile.getWallBrush() != null);
                            selectDoodadbrushTSMI.Enabled = (selectedTile.getDoodadBrush() != null);
                            selectTableBrushTSMI.Enabled = (selectedTile.getTableBrush() != null);
                        }
                        selectCreatureTSMI.Enabled = (selectedTile.getCreatureBrush() != null);
                    }
                }

                foreach (var it in contextMenuStrip.Items)
                {
                    if (typeof(ToolStripMenuItem).Equals(it.GetType()))
                    {
                        ToolStripMenuItem ts = (ToolStripMenuItem)it;
                        ts.Visible = ts.Enabled;
                    }
                }
            }
            catch (Exception ex)
            {
                Generic.AjustError(ex);
            }

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getMapEditor().destroySelection();
            Refresh();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (getMapEditor().isSelectionMode())
            {
                getMapEditor().copybuffer.copy(getMapEditor(), getMapEditor().MapZ);
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getMapEditor().setSelectionMode();

            getMapEditor().selection.start();
            getMapEditor().selection.clear();
            getMapEditor().selection.finish();
            
            StartPasting();
            Refresh();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (getMapEditor().isSelectionMode())
            {
                getMapEditor().copybuffer.cut(getMapEditor(), getMapEditor().MapZ);
                Refresh();
            }
        }

        public void SelectBrush(Brush brush)
        {            
            if (brush.inTerrainPalette)
            {
                if (EditorPalette.SelectBrush(brush, PaletteType.TERRAIN)) return;
            }

            if (brush.inDoodadPalette) 
            {
                if (EditorPalette.SelectBrush(brush, PaletteType.DOODAD)) return;
            }

            if (brush.inItemPalette)
            {
                if (EditorPalette.SelectBrush(brush, PaletteType.ITEM)) return;
            }

            if (typeof(CreatureBrush).Equals(brush.GetType()))
            {
                if (EditorPalette.SelectBrush(brush, PaletteType.CREATURE)) return;
            }
        }


        private void selectRawTSMI_Click(object sender, EventArgs e)
        {
            Tile selectedTile = getMapEditor().selection.getSelectedTile();
            if (selectedTile == null) return;
            if (selectedTile.getTopSelectedItem() == null) return;
            Item it = selectedTile.getTopSelectedItem();
            if (it.Type.raw_brush == null) return;
            if (EditorPalette.SelectBrush(it.Type.raw_brush, PaletteType.RAW)) return;
            
        }

        private void selectGroundBrushTSMI_Click(object sender, EventArgs e)
        {
            Tile selectedTile = getMapEditor().selection.getSelectedTile();
            if (selectedTile == null) return;

            GroundBrush gbrush = selectedTile.getGroundBrush();
            if (gbrush == null) return;

            SelectBrush(gbrush);
        }

        private void selectDoodadbrushTSMI_Click(object sender, EventArgs e)
        {
            Tile selectedTile = getMapEditor().selection.getSelectedTile();
            if (selectedTile == null) return;

            Brush dbrush = selectedTile.getDoodadBrush();
            if (dbrush == null) return;

            SelectBrush(dbrush);
            

        }

        private void selectWallbrushTSMI_Click(object sender, EventArgs e)
        {
            Tile selectedTile = getMapEditor().selection.getSelectedTile();
            if (selectedTile == null) return;

            WallBrush wbrush = selectedTile.getWallBrush();
            if (wbrush == null) return;

            SelectBrush(wbrush);
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //
        }

        private void selectCarpetBrushTSMI_Click(object sender, EventArgs e)
        {
            Tile selectedTile = getMapEditor().selection.getSelectedTile();
            if (selectedTile == null) return;

            CarpetBrush cbrush = selectedTile.getCarpetBrush();
            if (cbrush == null) return;

            SelectBrush(cbrush);
        }

        private void selectTableBrushTSMI_Click(object sender, EventArgs e)
        {
            Tile selectedTile = getMapEditor().selection.getSelectedTile();
            if (selectedTile == null) return;

            TableBrush tbrush = selectedTile.getTableBrush();
            if (tbrush == null) return;

            SelectBrush(tbrush);
        }

        private void selectDoorBrushTSMI_Click(object sender, EventArgs e)
        {

        }

        private void MapCanvas_MouseEnter(object sender, EventArgs e)
        {
            Focus();
        }

        private void MapCanvas_Enter(object sender, EventArgs e)
        {
            //
        }

        private void selectCreatureTSMI_Click(object sender, EventArgs e)
        {
            Tile selectedTile = getMapEditor().selection.getSelectedTile();

            if (selectedTile == null) return;

            CreatureBrush tbrush = selectedTile.getCreatureBrush();
            if (tbrush == null) return;

            if (EditorPalette.SelectBrush(tbrush, PaletteType.CREATURE)) return;            
        }
    }
}
