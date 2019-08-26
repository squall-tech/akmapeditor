using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AKMapEditor.OtMapEditor;
using System.Net.Sockets;
using System.Threading;
using AKMapEditor.OtMapEditorServer;
using AKMapEditor.OtMapEditorServer.Classes;

namespace AKMapEditor
{
    public partial class ServerForm : Form
    {
        public const int SERVER_PORT = 17187;        

        public GameMap gameMap = new GameMap();
        public bool serverOnError = false;
        private TcpListener tcpListener;
        private bool terminated = false;
        private Thread listnterThead;
        private List<Connection> connections;
        private String password;        
        public ServerForm()
        {
            InitializeComponent();
          //  autoSaveTimer.Interval = (Settings.GetInteger(Key.AUTOSAVE_TIME) * 1000);
            autoSaveTimer.Interval = 100;
            try
            {
                ipTb.Text = Generic.getMyIP();
            }  catch
            {
                ipTb.Text = "";
            }
            

            //autoSaveTimer.Interval = 100;
            listnterThead = new Thread(ListeningClients);
            connections = new List<Connection>();
            password = "";
            if (!"".Equals(Global.inicialMap))
            {
                mapTb.Text = Global.inicialMap;
                startServer();
                this.notifyIcon.Visible = true;
                this.Hide();
            }            
        }

        private void BtStart_Click(object sender, EventArgs e)
        {
            startServer();
        }

        public bool verifyPassword(String password)
        {
            return (this.password.Equals(password));
        }

        private void startServer()
        {
            try
            {
                addLog("Initializing AKMapEditor Server 1.0");
                if (mapTb.Text != "")
                {
                    addLog("Loading map");
                    gameMap.Load(mapTb.Text, false);
                    addLog("Map Loaded");
                }

                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(ipTb.Text), SERVER_PORT);
                tcpListener = new TcpListener(ipEndPoint);
                tcpListener.Start();
                addLog("Server started at port: " + SERVER_PORT + " on ip: " + ipTb.Text);
                addLog("Listening for clients");
                listnterThead.Start();
                autoSaveTimer.Enabled = autoSaveCB.Checked;
            }
            catch (Exception ex)
            {
                //
            }            
        }

        private void ListeningClients()
        {
            try
            {
                while (!terminated)
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();
                    Connection connection = new Connection(tcpClient, this);
                    connections.Add(connection);
                    connection.init();
                }
            }
            catch (Exception ex)
            {
                addLogThread("ListeningClients(): " + ex.Message + "\n" + ex.StackTrace);
            }            
        }

        public void addLogThread(String log)
        {
            try
            {
                if (this.InvokeRequired)
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        logText.AppendText(log + "\n");
                        logText.Refresh();
                    });
            }
            catch { }
            
        }



        public void SaveLog()
        {
            try
            {
                if (this.InvokeRequired)
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        logText.SaveFile(Path.GetDirectoryName(Application.ExecutablePath) + "\file_log.txt");
                    });
            }
            catch (Exception ex)
            {
                //
            }
            
        }

        public void addLog(String log)
        {
            try
            {
                logText.AppendText(log + "\n");
                logText.Refresh();
            }
            catch { }
            
        }

        public void removeConnection(Connection connection)
        {
            try
            {
                connections.Remove(connection);
            }
            catch { }
            
        }

        public void UpdateMaps(MapUpdate mapUpdate, Connection sender)
        {
            try
            {
                foreach (Connection connection in connections)
                {
                    if (!sender.Equals(connection))
                    {
                        Thread t = new Thread(() => connection.SendMapUpdates(mapUpdate));
                        t.Start();
                    }
                }
            } catch (Exception ex)
            {  

            }
            
        }

        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Program.devMode)
            {
                Environment.Exit(0);
            }
            else
            {
                notifyIcon.Visible = true;
                this.Visible = false;
                e.Cancel = true;
            }                       
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            notifyIcon.Visible = false;
            this.Visible = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show("Deseja salvar o mapa?",
                "Save changes", MessageBoxButtons.YesNo);
            if (result1 == System.Windows.Forms.DialogResult.Yes)
            {
                if ("".Equals(gameMap.FileName))
                {
                    gameMap.FileName = Generic.SaveMapFile();
                    if ("".Equals(gameMap.FileName))
                    {
                        return;
                    }
                }

                gameMap.Save(gameMap.FileName);
            }

            notifyIcon.Visible = false;
            Environment.Exit(0);
        }

        private void ServerForm_Load(object sender, EventArgs e)
        {
            if (!"".Equals(Global.inicialMap))
            {
                BeginInvoke(new MethodInvoker(delegate
                {
                    Hide();
                }));              
            }            
        }

        private void autoSaveTimer_Tick(object sender, EventArgs e)
        {            
            Thread th = new Thread(SaveMap);
            th.Start();
        }

        private void SaveMap()
        {
            try
            {
                throw new Exception("Não deu pra salvar o mapa");

                if ((gameMap.FileName != "") && (!gameMap.MapOnSave) && File.Exists(gameMap.FileName) && (autoSaveTimer.Enabled) && (gameMap.HasUpdates))
                {                    
                    addLogThread("Saving map....");
                    gameMap.Save(gameMap.FileName);
                    addLogThread("Map autosave finished");                   
                }
            }
            catch (Exception ex)
            {                
                //ajustar se da exception
                autoSaveTimer.Enabled = false;
                addLogThread("On save map: " + ex.Message);
                Generic.AjustError(ex, false, !exit_on_error.Checked);
                if (exit_on_error.Checked)
                {
                    Thread.Sleep(3000);
                    Environment.Exit(0);
                }                
            }
        }

        private void openMap_Click(object sender, EventArgs e)
        {
            mapTb.Text = Generic.OpenMapFile();
        }

        private void forceSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ("".Equals(gameMap.FileName))
            {
                gameMap.FileName = Generic.SaveMapFile();
                if ("".Equals(gameMap.FileName))
                {                 
                    return;
                }
            }

            gameMap.Save(gameMap.FileName);            
        }

        private void ncontextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void saveMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show("Deseja salvar o mapa?",
                "Save changes", MessageBoxButtons.YesNo);
            if (result1 == System.Windows.Forms.DialogResult.Yes)
            {
                if ("".Equals(gameMap.FileName))
                {
                    gameMap.FileName = Generic.SaveMapFile();
                    if ("".Equals(gameMap.FileName))
                    {
                        return;
                    }
                }

                gameMap.Save(gameMap.FileName);
            }
        }
    }
}
