namespace AKMapEditor
{
    partial class ServerForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.openMap = new System.Windows.Forms.Button();
            this.autoSaveCB = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ipTb = new System.Windows.Forms.TextBox();
            this.mapTb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.BtStart = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.logText = new System.Windows.Forms.RichTextBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.ncontextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoSaveTimer = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.forceSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exit_on_error = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.ncontextMenuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.exit_on_error);
            this.panel1.Controls.Add(this.openMap);
            this.panel1.Controls.Add(this.autoSaveCB);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.ipTb);
            this.panel1.Controls.Add(this.mapTb);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.BtStart);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(831, 43);
            this.panel1.TabIndex = 0;
            // 
            // openMap
            // 
            this.openMap.Location = new System.Drawing.Point(294, 11);
            this.openMap.Name = "openMap";
            this.openMap.Size = new System.Drawing.Size(24, 22);
            this.openMap.TabIndex = 8;
            this.openMap.UseVisualStyleBackColor = true;
            this.openMap.Click += new System.EventHandler(this.openMap_Click);
            // 
            // autoSaveCB
            // 
            this.autoSaveCB.AutoSize = true;
            this.autoSaveCB.Location = new System.Drawing.Point(560, 5);
            this.autoSaveCB.Name = "autoSaveCB";
            this.autoSaveCB.Size = new System.Drawing.Size(107, 17);
            this.autoSaveCB.TabIndex = 7;
            this.autoSaveCB.Text = "Enable Autosave";
            this.autoSaveCB.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(320, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "IP:";
            // 
            // ipTb
            // 
            this.ipTb.Location = new System.Drawing.Point(346, 13);
            this.ipTb.Name = "ipTb";
            this.ipTb.Size = new System.Drawing.Size(201, 20);
            this.ipTb.TabIndex = 5;
            this.ipTb.Text = "192.168.1.100";
            // 
            // mapTb
            // 
            this.mapTb.Location = new System.Drawing.Point(49, 12);
            this.mapTb.Name = "mapTb";
            this.mapTb.Size = new System.Drawing.Size(243, 20);
            this.mapTb.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Map:";
            // 
            // BtStart
            // 
            this.BtStart.Location = new System.Drawing.Point(723, 10);
            this.BtStart.Name = "BtStart";
            this.BtStart.Size = new System.Drawing.Size(96, 23);
            this.BtStart.TabIndex = 0;
            this.BtStart.Text = "Start";
            this.BtStart.UseVisualStyleBackColor = true;
            this.BtStart.Click += new System.EventHandler(this.BtStart_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.logText);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 43);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(831, 369);
            this.panel2.TabIndex = 1;
            // 
            // logText
            // 
            this.logText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logText.Location = new System.Drawing.Point(0, 0);
            this.logText.Name = "logText";
            this.logText.ReadOnly = true;
            this.logText.Size = new System.Drawing.Size(831, 369);
            this.logText.TabIndex = 0;
            this.logText.Text = "";
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.ncontextMenuStrip1;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "AKMapEditor Server";
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // ncontextMenuStrip1
            // 
            this.ncontextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveMapToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.ncontextMenuStrip1.Name = "ncontextMenuStrip1";
            this.ncontextMenuStrip1.Size = new System.Drawing.Size(126, 48);
            this.ncontextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.ncontextMenuStrip1_Opening);
            // 
            // saveMapToolStripMenuItem
            // 
            this.saveMapToolStripMenuItem.Name = "saveMapToolStripMenuItem";
            this.saveMapToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.saveMapToolStripMenuItem.Text = "Save Map";
            this.saveMapToolStripMenuItem.Click += new System.EventHandler(this.saveMapToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // autoSaveTimer
            // 
            this.autoSaveTimer.Tick += new System.EventHandler(this.autoSaveTimer_Tick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.forceSaveToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(99, 26);
            // 
            // forceSaveToolStripMenuItem
            // 
            this.forceSaveToolStripMenuItem.Name = "forceSaveToolStripMenuItem";
            this.forceSaveToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.forceSaveToolStripMenuItem.Text = "Save";
            this.forceSaveToolStripMenuItem.Click += new System.EventHandler(this.forceSaveToolStripMenuItem_Click);
            // 
            // exit_on_error
            // 
            this.exit_on_error.AutoSize = true;
            this.exit_on_error.Checked = true;
            this.exit_on_error.CheckState = System.Windows.Forms.CheckState.Checked;
            this.exit_on_error.Location = new System.Drawing.Point(560, 21);
            this.exit_on_error.Name = "exit_on_error";
            this.exit_on_error.Size = new System.Drawing.Size(123, 17);
            this.exit_on_error.TabIndex = 9;
            this.exit_on_error.Text = "Close on server error";
            this.exit_on_error.UseVisualStyleBackColor = true;
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 412);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ServerForm";
            this.Text = "AKMapEditor Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerForm_FormClosing);
            this.Load += new System.EventHandler(this.ServerForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ncontextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtStart;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox logText;
        private System.Windows.Forms.TextBox mapTb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ipTb;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip ncontextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.CheckBox autoSaveCB;
        private System.Windows.Forms.Timer autoSaveTimer;
        private System.Windows.Forms.Button openMap;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem forceSaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveMapToolStripMenuItem;
        private System.Windows.Forms.CheckBox exit_on_error;
    }
}