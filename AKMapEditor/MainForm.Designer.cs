namespace AKMapEditor
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MainMS = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.new_TSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.open_TSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.selectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.currentFloorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lowerFloorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visibleFloorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.borderOptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.borderAutomagicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showShadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.floorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.actionTSSL = new System.Windows.Forms.ToolStripStatusLabel();
            this.itemTSSL = new System.Windows.Forms.ToolStripStatusLabel();
            this.positionTSSL = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelItem = new System.Windows.Forms.Panel();
            this.mapBrowser1 = new AKMapEditor.OtMapEditor.MapBrowser();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.jumpToBrushToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jumpToItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMS.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapBrowser1)).BeginInit();
            this.SuspendLayout();
            // 
            // MainMS
            // 
            this.MainMS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.mapToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.windowToolStripMenuItem,
            this.floorToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.MainMS.Location = new System.Drawing.Point(0, 0);
            this.MainMS.Name = "MainMS";
            this.MainMS.Size = new System.Drawing.Size(1182, 24);
            this.MainMS.TabIndex = 1;
            this.MainMS.Text = "menuStrip1";
            this.MainMS.MenuActivate += new System.EventHandler(this.MainMS_MenuActivate);
            this.MainMS.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.MainMS_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.new_TSMI,
            this.open_TSMI,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.connectToolStripMenuItem,
            this.toolStripMenuItem2,
            this.preferencesToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // new_TSMI
            // 
            this.new_TSMI.Name = "new_TSMI";
            this.new_TSMI.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.new_TSMI.Size = new System.Drawing.Size(195, 22);
            this.new_TSMI.Text = "New";
            this.new_TSMI.Click += new System.EventHandler(this.new_TSMI_Click);
            // 
            // open_TSMI
            // 
            this.open_TSMI.Name = "open_TSMI";
            this.open_TSMI.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.open_TSMI.Size = new System.Drawing.Size(195, 22);
            this.open_TSMI.Text = "Open";
            this.open_TSMI.Click += new System.EventHandler(this.open_TSMI_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.connectToolStripMenuItem.Text = "Connect";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(192, 6);
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.preferencesToolStripMenuItem.Text = "Preferences";
            this.preferencesToolStripMenuItem.Visible = false;
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripMenuItem1,
            this.selectionToolStripMenuItem,
            this.borderOptionToolStripMenuItem,
            this.toolStripMenuItem3,
            this.jumpToBrushToolStripMenuItem,
            this.jumpToItemToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Z)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(171, 6);
            // 
            // selectionToolStripMenuItem
            // 
            this.selectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.currentFloorToolStripMenuItem,
            this.lowerFloorsToolStripMenuItem,
            this.visibleFloorsToolStripMenuItem});
            this.selectionToolStripMenuItem.Name = "selectionToolStripMenuItem";
            this.selectionToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.selectionToolStripMenuItem.Text = "Selection";
            // 
            // currentFloorToolStripMenuItem
            // 
            this.currentFloorToolStripMenuItem.Name = "currentFloorToolStripMenuItem";
            this.currentFloorToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.currentFloorToolStripMenuItem.Text = "Current Floor";
            // 
            // lowerFloorsToolStripMenuItem
            // 
            this.lowerFloorsToolStripMenuItem.Name = "lowerFloorsToolStripMenuItem";
            this.lowerFloorsToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.lowerFloorsToolStripMenuItem.Text = "Lower Floors";
            // 
            // visibleFloorsToolStripMenuItem
            // 
            this.visibleFloorsToolStripMenuItem.Name = "visibleFloorsToolStripMenuItem";
            this.visibleFloorsToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.visibleFloorsToolStripMenuItem.Text = "Visible Floors";
            // 
            // borderOptionToolStripMenuItem
            // 
            this.borderOptionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.borderAutomagicToolStripMenuItem});
            this.borderOptionToolStripMenuItem.Name = "borderOptionToolStripMenuItem";
            this.borderOptionToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.borderOptionToolStripMenuItem.Text = "Border option";
            // 
            // borderAutomagicToolStripMenuItem
            // 
            this.borderAutomagicToolStripMenuItem.Name = "borderAutomagicToolStripMenuItem";
            this.borderAutomagicToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.borderAutomagicToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.borderAutomagicToolStripMenuItem.Text = "Border automagic";
            this.borderAutomagicToolStripMenuItem.Click += new System.EventHandler(this.borderAutomagicToolStripMenuItem_Click);
            // 
            // mapToolStripMenuItem
            // 
            this.mapToolStripMenuItem.Name = "mapToolStripMenuItem";
            this.mapToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.mapToolStripMenuItem.Text = "Map";
            this.mapToolStripMenuItem.Visible = false;
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showShadeToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // showShadeToolStripMenuItem
            // 
            this.showShadeToolStripMenuItem.Name = "showShadeToolStripMenuItem";
            this.showShadeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.showShadeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.showShadeToolStripMenuItem.Text = "Show shade";
            this.showShadeToolStripMenuItem.Click += new System.EventHandler(this.showShadeToolStripMenuItem_Click);
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.windowToolStripMenuItem.Text = "Window";
            this.windowToolStripMenuItem.Visible = false;
            // 
            // floorToolStripMenuItem
            // 
            this.floorToolStripMenuItem.Name = "floorToolStripMenuItem";
            this.floorToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.floorToolStripMenuItem.Text = "Floor";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Visible = false;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actionTSSL,
            this.itemTSSL,
            this.positionTSSL});
            this.statusStrip.Location = new System.Drawing.Point(0, 425);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1182, 22);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "mainSS";
            // 
            // actionTSSL
            // 
            this.actionTSSL.Name = "actionTSSL";
            this.actionTSSL.Size = new System.Drawing.Size(389, 17);
            this.actionTSSL.Spring = true;
            this.actionTSSL.Text = "Action";
            this.actionTSSL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // itemTSSL
            // 
            this.itemTSSL.Name = "itemTSSL";
            this.itemTSSL.Size = new System.Drawing.Size(389, 17);
            this.itemTSSL.Spring = true;
            this.itemTSSL.Text = "Nothing";
            this.itemTSSL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // positionTSSL
            // 
            this.positionTSSL.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.positionTSSL.Name = "positionTSSL";
            this.positionTSSL.Size = new System.Drawing.Size(389, 17);
            this.positionTSSL.Spring = true;
            this.positionTSSL.Text = "x: 0 y: 0 z: 0";
            // 
            // panelItem
            // 
            this.panelItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelItem.Location = new System.Drawing.Point(2, 28);
            this.panelItem.Name = "panelItem";
            this.panelItem.Size = new System.Drawing.Size(229, 392);
            this.panelItem.TabIndex = 4;
            // 
            // mapBrowser1
            // 
            this.mapBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapBrowser1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.mapBrowser1.Location = new System.Drawing.Point(236, 29);
            this.mapBrowser1.Name = "mapBrowser1";
            this.mapBrowser1.Size = new System.Drawing.Size(940, 391);
            this.mapBrowser1.TabIndex = 5;
            this.mapBrowser1.Text = "mapBrowser1";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(171, 6);
            // 
            // jumpToBrushToolStripMenuItem
            // 
            this.jumpToBrushToolStripMenuItem.Name = "jumpToBrushToolStripMenuItem";
            this.jumpToBrushToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.jumpToBrushToolStripMenuItem.Text = "Jump to Brush";
            this.jumpToBrushToolStripMenuItem.Click += new System.EventHandler(this.jumpToBrushToolStripMenuItem_Click);
            // 
            // jumpToItemToolStripMenuItem
            // 
            this.jumpToItemToolStripMenuItem.Name = "jumpToItemToolStripMenuItem";
            this.jumpToItemToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.jumpToItemToolStripMenuItem.Text = "Jump to Item";
            this.jumpToItemToolStripMenuItem.Click += new System.EventHandler(this.jumpToItemToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1182, 447);
            this.Controls.Add(this.mapBrowser1);
            this.Controls.Add(this.panelItem);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.MainMS);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "AK MapEditor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Leave += new System.EventHandler(this.MainForm_Leave);
            this.MainMS.ResumeLayout(false);
            this.MainMS.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapBrowser1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMS;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem new_TSMI;
        private System.Windows.Forms.ToolStripMenuItem open_TSMI;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;        
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel actionTSSL;
        private System.Windows.Forms.ToolStripStatusLabel itemTSSL;
        private System.Windows.Forms.ToolStripStatusLabel positionTSSL;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.Panel panelItem;
        private OtMapEditor.MapBrowser mapBrowser1;
        private System.Windows.Forms.ToolStripMenuItem mapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem floorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem borderOptionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem borderAutomagicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showShadeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem currentFloorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lowerFloorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem visibleFloorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem jumpToBrushToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jumpToItemToolStripMenuItem;













    }
}

