using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarsiLibrary.Win;
using System.Windows.Forms;

namespace AKMapEditor.OtMapEditor
{
    public class MapBrowser : FATabStrip
    {
        private MapEditor activationMap;
        private MapEditor activeMapEditor;
        private MapCanvas mapCanvas;

        public MapBrowser()
        {
            mapCanvas = new MapCanvas();
            Global.mapCanvas = mapCanvas;
            mapCanvas.Dock = DockStyle.Fill;
            mapCanvas.mainForm = (MainForm)this.ParentForm;
            InitializeComponent();
        }

        public void Start()
        {                     
               activationMap = CreateMapEditor("");
        }

        public void Start(String map)
        {
            activationMap = CreateMapEditor(map);
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // MapBrowser
            // 
            this.TabStripItemClosing += new FarsiLibrary.Win.TabStripItemClosingHandler(this.MapBrowser_TabStripItemClosing);
            this.TabStripItemSelectionChanged += new FarsiLibrary.Win.TabStripItemChangedHandler(this.MapBrowser_TabStripItemSelectionChanged_1);
            this.TabStripItemClosed += new System.EventHandler(this.MapBrowser_TabStripItemClosed);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        public void LoadMap(String fileMap)
        {
            CreateMapEditor(fileMap);
        }

        public void LoadMap(String ip, String password)
        {
            CreateMapEditor(ip,password);
        }

        public void NewMap()
        {
            CreateMapEditor("");
        }


        private MapEditor CreateMapEditor(String fileMap)
        {
            if (activationMap != null)
            {
                this.RemoveTab(activationMap);
            }
            MapEditor mapViewer = new MapEditor(fileMap);
            activeMapEditor = mapViewer;            
            this.AddTab(mapViewer);
            this.SelectedItem = mapViewer;
            mapViewer.Start();            
            return mapViewer;
        }

        private MapEditor CreateMapEditor(String ip, String password)
        {
            if (activationMap != null)
            {
                this.RemoveTab(activationMap);
            }
            MapEditor mapViewer = new MapEditor(ip,password);
            activeMapEditor = mapViewer;
            this.AddTab(mapViewer);
            this.SelectedItem = mapViewer;
            mapViewer.Start();
            return mapViewer;
        }

        private void MapBrowser_TabStripItemClosing(TabStripItemClosingEventArgs e)
        {
            activeMapEditor.Close();
        }

        private void MapBrowser_TabStripItemSelectionChanged_1(TabStripItemChangedEventArgs e)
        {         
            if (e.ChangeType == FATabStripItemChangeTypes.SelectionChanged &&
               activeMapEditor != null)
            {
                activeMapEditor.RemoveCanvas(mapCanvas);
                activeMapEditor = (MapEditor)e.Item;
                activeMapEditor.UpdateCanvas(mapCanvas);
                mapCanvas.Focus();
            }

        }

        private void MapBrowser_TabStripItemClosed(object sender, EventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
