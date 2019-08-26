using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;
using AKMapEditor.OtMapEditor;
using System.Xml.Linq;

namespace AKMapEditor
{    
    public partial class MainForm : Form
    {
        private ToolStripMenuItem[] floorItens;
        private ToolStripMenuItem selectedFloorItem;
        public MainForm()
        {            
            Global.mainForm = this;
            InitializeComponent();
            if (Program.devMode) Win32.AllocConsole();
           
            Global.selectedEditorPalette = new EditorPalette();
            Global.selectedEditorPalette.Dock = DockStyle.Fill;
            panelItem.Controls.Add(Global.selectedEditorPalette);
            floorItens = new ToolStripMenuItem[16];
            //BackColor = Color.Gray;

            this.LostFocus += new EventHandler(Form1_LostFocus);

            UpdateConfigs();
        }

        public void UpdateConfigs()
        {
            showShadeToolStripMenuItem.Checked = Settings.GetBoolean(Key.SHOW_SHADE);

            currentFloorToolStripMenuItem.Tag = SelectionType.SELECT_CURRENT_FLOOR;
            currentFloorToolStripMenuItem.Checked = false;
            currentFloorToolStripMenuItem.Click += SelectionTypeClick;

            visibleFloorsToolStripMenuItem.Tag = SelectionType.SELECT_VISIBLE_FLOORS;
            visibleFloorsToolStripMenuItem.Checked = false;
            visibleFloorsToolStripMenuItem.Click += SelectionTypeClick;
            lowerFloorsToolStripMenuItem.Tag = SelectionType.SELECT_ALL_FLOORS;
            lowerFloorsToolStripMenuItem.Checked = false;
            lowerFloorsToolStripMenuItem.Click += SelectionTypeClick;

            int selection = Settings.GetInteger(Key.SELECTION_TYPE);
            switch (selection)
            {
                case SelectionType.SELECT_CURRENT_FLOOR:
                    currentFloorToolStripMenuItem.Checked = true;
                    break;
                case SelectionType.SELECT_VISIBLE_FLOORS:
                    visibleFloorsToolStripMenuItem.Checked = true;
                    break;
                case SelectionType.SELECT_ALL_FLOORS:
                    lowerFloorsToolStripMenuItem.Checked = true;
                    break;
            }            

        }


        public void updateAction(String action)
        {
            actionTSSL.Text = action;
        }

        private void Form1_LostFocus(object sender, EventArgs e)
        {

//            Global.mapCanvas.drawing = false;
        }


        public void updateAction2(String action)
        {
            itemTSSL.Text = action;
        }

        public void UpdateScreenPosition(Position position)
        {
            positionTSSL.Text = "x: " + position.x + " y: "
                    + position.y + " z: " + position.z;
        }

        private void open_TSMI_Click(object sender, EventArgs e)
        {
            try
            {
                mapBrowser1.LoadMap(Generic.OpenMapFile());
            }
            catch (Exception ex)
            {
                Generic.DoException(ex);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                mapBrowser1.Start();

                if (!"".Equals(Global.inicialMap))
                {
                    mapBrowser1.Start(Global.inicialMap);
                }
                else
                {
                    mapBrowser1.Start();
                }
            }
            catch (Exception ex)
            {
                Generic.DoException(ex);
            }

            registerFloor();            
        }

        private void new_TSMI_Click(object sender, EventArgs e)
        {
            mapBrowser1.NewMap();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((Global.mapCanvas != null) && (Global.mapCanvas.getMapEditor() != null))
            {
                Global.mapCanvas.getMapEditor().Undo();
            }
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((Global.mapCanvas != null) && (Global.mapCanvas.getMapEditor() != null))
            {
                Global.mapCanvas.getMapEditor().Redo();
            }
        }

        private void registerFloor()
        {
            for (int x = 0; x < 16; x++)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                floorItens[x] = item;
                item.Text = "Floor " + x;                
                item.Tag = x;
                item.Click += floorItem_Click;
                floorToolStripMenuItem.DropDownItems.Add(item);
            }
            FloorChange(7);
        }

        public void FloorChange(int newFlor)
        {
            if (floorItens != null)
            {
                if (selectedFloorItem != null)
                {
                    selectedFloorItem.Checked = false;
                }
                selectedFloorItem = floorItens[newFlor];
                if (selectedFloorItem != null)
                {
                    selectedFloorItem.Checked = true;
                }
                
            }            
        }

        private void floorItem_Click(object sender, EventArgs e)
        {            
            if (selectedFloorItem != null)
            {
                selectedFloorItem.Checked = false;
            }

            int dif = (Global.mapCanvas.getMapEditor().MapZ - (int)((ToolStripMenuItem)sender).Tag);
            selectedFloorItem = (ToolStripMenuItem)sender;
            Global.mapCanvas.getMapEditor().UpdatePosScrollBar(32 * dif, 32 * dif);
            Global.mapCanvas.getMapEditor().MapZ = (int) ((ToolStripMenuItem)sender).Tag;
            Global.mapCanvas.Refresh();
            selectedFloorItem.Checked = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void borderAutomagicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool valor = Settings.GetBoolean(Key.USE_AUTOMAGIC);
            Settings.SetValue(Key.USE_AUTOMAGIC, !valor);
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectForm connectForm = new ConnectForm();
            connectForm.ShowDialog();
            //MessageBox.Show(connectForm.ip + " - " + connectForm.password);
            mapBrowser1.LoadMap(connectForm.ip, connectForm.password);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Global.mapCanvas.getMapEditor().MapReadOnly())
            {
                Global.mapCanvas.getMapEditor().Save();
            }
            else
            {
                MessageBox.Show("Não é possível salvar o mapa esta ReadOnly");
            }
            
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void MainMS_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {            
            
        }

        private void MainMS_MenuActivate(object sender, EventArgs e)
        {
            //            
        }

        private void MainForm_Leave(object sender, EventArgs e)
        {
            Global.mapCanvas.drawing = false;
            Global.mapCanvas.ctrlActive = false;
        }

        public void showShadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.SetValue(Key.SHOW_SHADE, !Settings.GetBoolean(Key.SHOW_SHADE));
            UpdateConfigs();
            Global.mapCanvas.Refresh();
        }

        private void SelectionTypeClick(object sender, EventArgs e)
        {
            Settings.SetValue(Key.SELECTION_TYPE,(int)((ToolStripMenuItem)sender).Tag);
            UpdateConfigs();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (!Global.mapCanvas.getMapEditor().MapReadOnly())
            {
                Global.mapCanvas.getMapEditor().SaveAs();
            }
            else
            {
                MessageBox.Show("Não é possível salvar o mapa esta ReadOnly");
            }            
        }

        public int selectedMenuFloor()
        {
            if (selectedFloorItem != null)
            {
                return (int)selectedFloorItem.Tag;
            }
            else
            {
                return 0;
            }
        }

        private void jumpToBrushToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Generic.JumpToBrush();
        }

        private void jumpToItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Generic.JumpToItem();
        }
    }

    public class Win32
    {
        /// <summary>
        /// Allocates a new console for current process.
        /// </summary>
        [DllImport("kernel32.dll")]
        public static extern Boolean AllocConsole();

        /// <summary>
        /// Frees the console.
        /// </summary>
        [DllImport("kernel32.dll")]
        public static extern Boolean FreeConsole();
    }
}
