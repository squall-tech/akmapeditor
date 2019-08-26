namespace AKMapEditor.OtMapEditor
{
    partial class EditorPalette
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.paletteType_CB = new System.Windows.Forms.ComboBox();
            this.tileSet_GB = new System.Windows.Forms.GroupBox();
            this.panelEditor = new System.Windows.Forms.Panel();
            this.tileSet_CB = new System.Windows.Forms.ComboBox();
            this.brushSizeGB = new System.Windows.Forms.GroupBox();
            this.brushPanel15 = new System.Windows.Forms.Panel();
            this.brushPanel9 = new System.Windows.Forms.Panel();
            this.brushPanel7 = new System.Windows.Forms.Panel();
            this.brushPanel5 = new System.Windows.Forms.Panel();
            this.brushPanel3 = new System.Windows.Forms.Panel();
            this.brushPanel0 = new System.Windows.Forms.Panel();
            this.brushPanel1 = new System.Windows.Forms.Panel();
            this.quadBrush = new System.Windows.Forms.Panel();
            this.circleBrush = new System.Windows.Forms.Panel();
            this.BrushThicknessP = new System.Windows.Forms.GroupBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.use_cf_button = new System.Windows.Forms.CheckBox();
            this.toolBoxGB = new System.Windows.Forms.GroupBox();
            this.BrushPanelWindowNormal = new System.Windows.Forms.Panel();
            this.BrushPanelWindowHatch = new System.Windows.Forms.Panel();
            this.BrushPanelDoorQuest = new System.Windows.Forms.Panel();
            this.BrushPanelDoorMagic = new System.Windows.Forms.Panel();
            this.BrushPanelDoorLocked = new System.Windows.Forms.Panel();
            this.BrushPanelDoorNormal = new System.Windows.Forms.Panel();
            this.BrushPanelPVP = new System.Windows.Forms.Panel();
            this.BrushPanelNoLogout = new System.Windows.Forms.Panel();
            this.BrushPanelNoPVP = new System.Windows.Forms.Panel();
            this.BrushPanelPZ = new System.Windows.Forms.Panel();
            this.BrushPanelErase = new System.Windows.Forms.Panel();
            this.BrushPanelOptionalBorder = new System.Windows.Forms.Panel();
            this.creatureBrush = new System.Windows.Forms.GroupBox();
            this.placeSpawnBT = new System.Windows.Forms.Button();
            this.placeCreatureBT = new System.Windows.Forms.Button();
            this.spawnSizeNUD = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.spawnTimeNUD = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.tileSet_GB.SuspendLayout();
            this.brushSizeGB.SuspendLayout();
            this.BrushThicknessP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.toolBoxGB.SuspendLayout();
            this.creatureBrush.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spawnSizeNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spawnTimeNUD)).BeginInit();
            this.SuspendLayout();
            // 
            // paletteType_CB
            // 
            this.paletteType_CB.Dock = System.Windows.Forms.DockStyle.Top;
            this.paletteType_CB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.paletteType_CB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paletteType_CB.FormattingEnabled = true;
            this.paletteType_CB.Items.AddRange(new object[] {
            "Terrain Palette",
            "Doodad Palette",
            "Item Palette",
            "Creature Palette",
            "Raw Palette"});
            this.paletteType_CB.Location = new System.Drawing.Point(0, 0);
            this.paletteType_CB.Name = "paletteType_CB";
            this.paletteType_CB.Size = new System.Drawing.Size(232, 21);
            this.paletteType_CB.TabIndex = 0;
            this.paletteType_CB.SelectedIndexChanged += new System.EventHandler(this.paletteType_CB_SelectedIndexChanged);
            // 
            // tileSet_GB
            // 
            this.tileSet_GB.Controls.Add(this.panelEditor);
            this.tileSet_GB.Controls.Add(this.tileSet_CB);
            this.tileSet_GB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tileSet_GB.Location = new System.Drawing.Point(0, 21);
            this.tileSet_GB.Margin = new System.Windows.Forms.Padding(1);
            this.tileSet_GB.Name = "tileSet_GB";
            this.tileSet_GB.Size = new System.Drawing.Size(232, 383);
            this.tileSet_GB.TabIndex = 4;
            this.tileSet_GB.TabStop = false;
            this.tileSet_GB.Text = "TileSet";
            // 
            // panelEditor
            // 
            this.panelEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEditor.Location = new System.Drawing.Point(6, 43);
            this.panelEditor.Name = "panelEditor";
            this.panelEditor.Size = new System.Drawing.Size(220, 334);
            this.panelEditor.TabIndex = 0;
            // 
            // tileSet_CB
            // 
            this.tileSet_CB.AccessibleDescription = "";
            this.tileSet_CB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tileSet_CB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tileSet_CB.FormattingEnabled = true;
            this.tileSet_CB.Location = new System.Drawing.Point(5, 16);
            this.tileSet_CB.Name = "tileSet_CB";
            this.tileSet_CB.Size = new System.Drawing.Size(221, 21);
            this.tileSet_CB.TabIndex = 1;
            this.tileSet_CB.SelectedIndexChanged += new System.EventHandler(this.tileSet_CB_SelectedIndexChanged);
            // 
            // brushSizeGB
            // 
            this.brushSizeGB.Controls.Add(this.brushPanel15);
            this.brushSizeGB.Controls.Add(this.brushPanel9);
            this.brushSizeGB.Controls.Add(this.brushPanel7);
            this.brushSizeGB.Controls.Add(this.brushPanel5);
            this.brushSizeGB.Controls.Add(this.brushPanel3);
            this.brushSizeGB.Controls.Add(this.brushPanel0);
            this.brushSizeGB.Controls.Add(this.brushPanel1);
            this.brushSizeGB.Controls.Add(this.quadBrush);
            this.brushSizeGB.Controls.Add(this.circleBrush);
            this.brushSizeGB.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.brushSizeGB.Location = new System.Drawing.Point(0, 650);
            this.brushSizeGB.Name = "brushSizeGB";
            this.brushSizeGB.Size = new System.Drawing.Size(232, 102);
            this.brushSizeGB.TabIndex = 5;
            this.brushSizeGB.TabStop = false;
            this.brushSizeGB.Text = "Brush Size";
            // 
            // brushPanel15
            // 
            this.brushPanel15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.brushPanel15.Location = new System.Drawing.Point(154, 56);
            this.brushPanel15.Name = "brushPanel15";
            this.brushPanel15.Size = new System.Drawing.Size(35, 35);
            this.brushPanel15.TabIndex = 9;
            this.brushPanel15.Tag = "11";
            this.brushPanel15.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BrushSizeChange);
            // 
            // brushPanel9
            // 
            this.brushPanel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.brushPanel9.Location = new System.Drawing.Point(117, 56);
            this.brushPanel9.Name = "brushPanel9";
            this.brushPanel9.Size = new System.Drawing.Size(35, 35);
            this.brushPanel9.TabIndex = 8;
            this.brushPanel9.Tag = "8";
            this.brushPanel9.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BrushSizeChange);
            // 
            // brushPanel7
            // 
            this.brushPanel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.brushPanel7.Location = new System.Drawing.Point(80, 56);
            this.brushPanel7.Name = "brushPanel7";
            this.brushPanel7.Size = new System.Drawing.Size(35, 35);
            this.brushPanel7.TabIndex = 7;
            this.brushPanel7.Tag = "6";
            this.brushPanel7.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BrushSizeChange);
            // 
            // brushPanel5
            // 
            this.brushPanel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.brushPanel5.Location = new System.Drawing.Point(43, 56);
            this.brushPanel5.Name = "brushPanel5";
            this.brushPanel5.Size = new System.Drawing.Size(35, 35);
            this.brushPanel5.TabIndex = 6;
            this.brushPanel5.Tag = "4";
            this.brushPanel5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BrushSizeChange);
            // 
            // brushPanel3
            // 
            this.brushPanel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.brushPanel3.Location = new System.Drawing.Point(6, 56);
            this.brushPanel3.Name = "brushPanel3";
            this.brushPanel3.Size = new System.Drawing.Size(35, 35);
            this.brushPanel3.TabIndex = 5;
            this.brushPanel3.Tag = "2";
            this.brushPanel3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BrushSizeChange);
            // 
            // brushPanel0
            // 
            this.brushPanel0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.brushPanel0.Location = new System.Drawing.Point(117, 19);
            this.brushPanel0.Name = "brushPanel0";
            this.brushPanel0.Size = new System.Drawing.Size(35, 35);
            this.brushPanel0.TabIndex = 4;
            this.brushPanel0.Tag = "0";
            this.brushPanel0.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BrushSizeChange);
            // 
            // brushPanel1
            // 
            this.brushPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.brushPanel1.Location = new System.Drawing.Point(154, 19);
            this.brushPanel1.Name = "brushPanel1";
            this.brushPanel1.Size = new System.Drawing.Size(35, 35);
            this.brushPanel1.TabIndex = 3;
            this.brushPanel1.Tag = "1";
            this.brushPanel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BrushSizeChange);
            // 
            // quadBrush
            // 
            this.quadBrush.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.quadBrush.Location = new System.Drawing.Point(6, 19);
            this.quadBrush.Name = "quadBrush";
            this.quadBrush.Size = new System.Drawing.Size(34, 34);
            this.quadBrush.TabIndex = 2;
            this.quadBrush.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BrushShapeChange);
            // 
            // circleBrush
            // 
            this.circleBrush.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.circleBrush.Location = new System.Drawing.Point(43, 19);
            this.circleBrush.Name = "circleBrush";
            this.circleBrush.Size = new System.Drawing.Size(35, 35);
            this.circleBrush.TabIndex = 1;
            this.circleBrush.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BrushShapeChange);
            // 
            // BrushThicknessP
            // 
            this.BrushThicknessP.Controls.Add(this.trackBar1);
            this.BrushThicknessP.Controls.Add(this.use_cf_button);
            this.BrushThicknessP.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BrushThicknessP.Location = new System.Drawing.Point(0, 580);
            this.BrushThicknessP.Name = "BrushThicknessP";
            this.BrushThicknessP.Size = new System.Drawing.Size(232, 70);
            this.BrushThicknessP.TabIndex = 6;
            this.BrushThicknessP.TabStop = false;
            this.BrushThicknessP.Text = "Brush Thickness";
            // 
            // trackBar1
            // 
            this.trackBar1.LargeChange = 10;
            this.trackBar1.Location = new System.Drawing.Point(7, 42);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(197, 45);
            this.trackBar1.TabIndex = 1;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar1.Value = 5;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // use_cf_button
            // 
            this.use_cf_button.AutoSize = true;
            this.use_cf_button.Location = new System.Drawing.Point(21, 20);
            this.use_cf_button.Name = "use_cf_button";
            this.use_cf_button.Size = new System.Drawing.Size(128, 17);
            this.use_cf_button.TabIndex = 0;
            this.use_cf_button.Text = "Use custom thickness";
            this.use_cf_button.UseVisualStyleBackColor = true;
            this.use_cf_button.CheckedChanged += new System.EventHandler(this.use_cf_button_CheckedChanged);
            // 
            // toolBoxGB
            // 
            this.toolBoxGB.Controls.Add(this.BrushPanelWindowNormal);
            this.toolBoxGB.Controls.Add(this.BrushPanelWindowHatch);
            this.toolBoxGB.Controls.Add(this.BrushPanelDoorQuest);
            this.toolBoxGB.Controls.Add(this.BrushPanelDoorMagic);
            this.toolBoxGB.Controls.Add(this.BrushPanelDoorLocked);
            this.toolBoxGB.Controls.Add(this.BrushPanelDoorNormal);
            this.toolBoxGB.Controls.Add(this.BrushPanelPVP);
            this.toolBoxGB.Controls.Add(this.BrushPanelNoLogout);
            this.toolBoxGB.Controls.Add(this.BrushPanelNoPVP);
            this.toolBoxGB.Controls.Add(this.BrushPanelPZ);
            this.toolBoxGB.Controls.Add(this.BrushPanelErase);
            this.toolBoxGB.Controls.Add(this.BrushPanelOptionalBorder);
            this.toolBoxGB.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolBoxGB.Location = new System.Drawing.Point(0, 478);
            this.toolBoxGB.Name = "toolBoxGB";
            this.toolBoxGB.Size = new System.Drawing.Size(232, 102);
            this.toolBoxGB.TabIndex = 7;
            this.toolBoxGB.TabStop = false;
            this.toolBoxGB.Text = "Tools";
            // 
            // BrushPanelWindowNormal
            // 
            this.BrushPanelWindowNormal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BrushPanelWindowNormal.Location = new System.Drawing.Point(191, 56);
            this.BrushPanelWindowNormal.Name = "BrushPanelWindowNormal";
            this.BrushPanelWindowNormal.Size = new System.Drawing.Size(35, 35);
            this.BrushPanelWindowNormal.TabIndex = 21;
            this.BrushPanelWindowNormal.Tag = "11";
            // 
            // BrushPanelWindowHatch
            // 
            this.BrushPanelWindowHatch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BrushPanelWindowHatch.Location = new System.Drawing.Point(154, 56);
            this.BrushPanelWindowHatch.Name = "BrushPanelWindowHatch";
            this.BrushPanelWindowHatch.Size = new System.Drawing.Size(35, 35);
            this.BrushPanelWindowHatch.TabIndex = 20;
            this.BrushPanelWindowHatch.Tag = "11";
            // 
            // BrushPanelDoorQuest
            // 
            this.BrushPanelDoorQuest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BrushPanelDoorQuest.Location = new System.Drawing.Point(117, 56);
            this.BrushPanelDoorQuest.Name = "BrushPanelDoorQuest";
            this.BrushPanelDoorQuest.Size = new System.Drawing.Size(35, 35);
            this.BrushPanelDoorQuest.TabIndex = 19;
            this.BrushPanelDoorQuest.Tag = "8";
            // 
            // BrushPanelDoorMagic
            // 
            this.BrushPanelDoorMagic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BrushPanelDoorMagic.Location = new System.Drawing.Point(80, 56);
            this.BrushPanelDoorMagic.Name = "BrushPanelDoorMagic";
            this.BrushPanelDoorMagic.Size = new System.Drawing.Size(35, 35);
            this.BrushPanelDoorMagic.TabIndex = 18;
            this.BrushPanelDoorMagic.Tag = "6";
            // 
            // BrushPanelDoorLocked
            // 
            this.BrushPanelDoorLocked.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BrushPanelDoorLocked.Location = new System.Drawing.Point(43, 56);
            this.BrushPanelDoorLocked.Name = "BrushPanelDoorLocked";
            this.BrushPanelDoorLocked.Size = new System.Drawing.Size(35, 35);
            this.BrushPanelDoorLocked.TabIndex = 17;
            this.BrushPanelDoorLocked.Tag = "4";
            // 
            // BrushPanelDoorNormal
            // 
            this.BrushPanelDoorNormal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BrushPanelDoorNormal.Location = new System.Drawing.Point(6, 56);
            this.BrushPanelDoorNormal.Name = "BrushPanelDoorNormal";
            this.BrushPanelDoorNormal.Size = new System.Drawing.Size(35, 35);
            this.BrushPanelDoorNormal.TabIndex = 16;
            this.BrushPanelDoorNormal.Tag = "2";
            // 
            // BrushPanelPVP
            // 
            this.BrushPanelPVP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BrushPanelPVP.Location = new System.Drawing.Point(191, 18);
            this.BrushPanelPVP.Name = "BrushPanelPVP";
            this.BrushPanelPVP.Size = new System.Drawing.Size(35, 35);
            this.BrushPanelPVP.TabIndex = 15;
            this.BrushPanelPVP.Tag = "11";
            // 
            // BrushPanelNoLogout
            // 
            this.BrushPanelNoLogout.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BrushPanelNoLogout.Location = new System.Drawing.Point(154, 18);
            this.BrushPanelNoLogout.Name = "BrushPanelNoLogout";
            this.BrushPanelNoLogout.Size = new System.Drawing.Size(35, 35);
            this.BrushPanelNoLogout.TabIndex = 14;
            this.BrushPanelNoLogout.Tag = "11";
            // 
            // BrushPanelNoPVP
            // 
            this.BrushPanelNoPVP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BrushPanelNoPVP.Location = new System.Drawing.Point(117, 18);
            this.BrushPanelNoPVP.Name = "BrushPanelNoPVP";
            this.BrushPanelNoPVP.Size = new System.Drawing.Size(35, 35);
            this.BrushPanelNoPVP.TabIndex = 13;
            this.BrushPanelNoPVP.Tag = "8";
            // 
            // BrushPanelPZ
            // 
            this.BrushPanelPZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BrushPanelPZ.Location = new System.Drawing.Point(80, 18);
            this.BrushPanelPZ.Name = "BrushPanelPZ";
            this.BrushPanelPZ.Size = new System.Drawing.Size(35, 35);
            this.BrushPanelPZ.TabIndex = 12;
            this.BrushPanelPZ.Tag = "6";
            // 
            // BrushPanelErase
            // 
            this.BrushPanelErase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BrushPanelErase.Location = new System.Drawing.Point(43, 18);
            this.BrushPanelErase.Name = "BrushPanelErase";
            this.BrushPanelErase.Size = new System.Drawing.Size(35, 35);
            this.BrushPanelErase.TabIndex = 11;
            this.BrushPanelErase.Tag = "4";
            // 
            // BrushPanelOptionalBorder
            // 
            this.BrushPanelOptionalBorder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BrushPanelOptionalBorder.Location = new System.Drawing.Point(6, 18);
            this.BrushPanelOptionalBorder.Name = "BrushPanelOptionalBorder";
            this.BrushPanelOptionalBorder.Size = new System.Drawing.Size(35, 35);
            this.BrushPanelOptionalBorder.TabIndex = 10;
            this.BrushPanelOptionalBorder.Tag = "2";
            // 
            // creatureBrush
            // 
            this.creatureBrush.Controls.Add(this.placeSpawnBT);
            this.creatureBrush.Controls.Add(this.placeCreatureBT);
            this.creatureBrush.Controls.Add(this.spawnSizeNUD);
            this.creatureBrush.Controls.Add(this.label2);
            this.creatureBrush.Controls.Add(this.spawnTimeNUD);
            this.creatureBrush.Controls.Add(this.label1);
            this.creatureBrush.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.creatureBrush.Location = new System.Drawing.Point(0, 404);
            this.creatureBrush.Name = "creatureBrush";
            this.creatureBrush.Size = new System.Drawing.Size(232, 74);
            this.creatureBrush.TabIndex = 23;
            this.creatureBrush.TabStop = false;
            this.creatureBrush.Text = "Brushes";
            // 
            // placeSpawnBT
            // 
            this.placeSpawnBT.Location = new System.Drawing.Point(128, 44);
            this.placeSpawnBT.Name = "placeSpawnBT";
            this.placeSpawnBT.Size = new System.Drawing.Size(95, 25);
            this.placeSpawnBT.TabIndex = 5;
            this.placeSpawnBT.Text = "Place Spawn";
            this.placeSpawnBT.UseVisualStyleBackColor = true;
            this.placeSpawnBT.Click += new System.EventHandler(this.placeSpawn_Click);
            // 
            // placeCreatureBT
            // 
            this.placeCreatureBT.Location = new System.Drawing.Point(128, 16);
            this.placeCreatureBT.Name = "placeCreatureBT";
            this.placeCreatureBT.Size = new System.Drawing.Size(95, 25);
            this.placeCreatureBT.TabIndex = 4;
            this.placeCreatureBT.Text = "Place Creature";
            this.placeCreatureBT.UseVisualStyleBackColor = true;
            this.placeCreatureBT.Click += new System.EventHandler(this.placeCreatureBT_Click);
            // 
            // spawnSizeNUD
            // 
            this.spawnSizeNUD.Location = new System.Drawing.Point(79, 45);
            this.spawnSizeNUD.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.spawnSizeNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spawnSizeNUD.Name = "spawnSizeNUD";
            this.spawnSizeNUD.Size = new System.Drawing.Size(44, 21);
            this.spawnSizeNUD.TabIndex = 3;
            this.spawnSizeNUD.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Spawn size";
            // 
            // spawnTimeNUD
            // 
            this.spawnTimeNUD.Location = new System.Drawing.Point(79, 18);
            this.spawnTimeNUD.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.spawnTimeNUD.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.spawnTimeNUD.Name = "spawnTimeNUD";
            this.spawnTimeNUD.Size = new System.Drawing.Size(44, 21);
            this.spawnTimeNUD.TabIndex = 1;
            this.spawnTimeNUD.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Spawntime";
            // 
            // EditorPalette
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tileSet_GB);
            this.Controls.Add(this.creatureBrush);
            this.Controls.Add(this.toolBoxGB);
            this.Controls.Add(this.BrushThicknessP);
            this.Controls.Add(this.brushSizeGB);
            this.Controls.Add(this.paletteType_CB);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "EditorPalette";
            this.Size = new System.Drawing.Size(232, 752);
            this.Enter += new System.EventHandler(this.EditorPalette_Enter);
            this.tileSet_GB.ResumeLayout(false);
            this.brushSizeGB.ResumeLayout(false);
            this.BrushThicknessP.ResumeLayout(false);
            this.BrushThicknessP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.toolBoxGB.ResumeLayout(false);
            this.creatureBrush.ResumeLayout(false);
            this.creatureBrush.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spawnSizeNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spawnTimeNUD)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox paletteType_CB;
        private System.Windows.Forms.GroupBox tileSet_GB;
        private System.Windows.Forms.ComboBox tileSet_CB;
        private System.Windows.Forms.Panel panelEditor;
        private System.Windows.Forms.GroupBox brushSizeGB;
        private System.Windows.Forms.Panel circleBrush;
        private System.Windows.Forms.Panel brushPanel15;
        private System.Windows.Forms.Panel brushPanel9;
        private System.Windows.Forms.Panel brushPanel7;
        private System.Windows.Forms.Panel brushPanel5;
        private System.Windows.Forms.Panel brushPanel3;
        private System.Windows.Forms.Panel brushPanel0;
        private System.Windows.Forms.Panel brushPanel1;
        private System.Windows.Forms.Panel quadBrush;
        private System.Windows.Forms.GroupBox BrushThicknessP;
        private System.Windows.Forms.CheckBox use_cf_button;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.GroupBox toolBoxGB;
        private System.Windows.Forms.Panel BrushPanelPVP;
        private System.Windows.Forms.Panel BrushPanelNoLogout;
        private System.Windows.Forms.Panel BrushPanelNoPVP;
        private System.Windows.Forms.Panel BrushPanelPZ;
        private System.Windows.Forms.Panel BrushPanelErase;
        private System.Windows.Forms.Panel BrushPanelOptionalBorder;
        private System.Windows.Forms.Panel BrushPanelWindowNormal;
        private System.Windows.Forms.Panel BrushPanelWindowHatch;
        private System.Windows.Forms.Panel BrushPanelDoorQuest;
        private System.Windows.Forms.Panel BrushPanelDoorMagic;
        private System.Windows.Forms.Panel BrushPanelDoorLocked;
        private System.Windows.Forms.Panel BrushPanelDoorNormal;
        private System.Windows.Forms.GroupBox creatureBrush;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button placeSpawnBT;
        private System.Windows.Forms.Button placeCreatureBT;
        private System.Windows.Forms.NumericUpDown spawnSizeNUD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown spawnTimeNUD;
    }
}
