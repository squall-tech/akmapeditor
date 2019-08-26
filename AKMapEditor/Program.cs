using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using AKMapEditor.OtMapEditor;
using System.Xml.Linq;
using System.IO;
using AKMapEditor.OtMapEditor.OtBrush;
using Brush = AKMapEditor.OtMapEditor.OtBrush.Brush;



namespace AKMapEditor
{
    static class Program
    {        
        public static bool devMode = true;
        public static bool errorOnVersion = false;

        [STAThread]
        static void Main(string[] args)
        {            
            bool ieserver = false;
            bool aborted = false;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Starting app.
            try{
                Settings.SetDefault();
                String dataDir = Generic.getAppDir() + "\\data\\";
                String tibiaDir = "";
                Global.inicialMap = "";

                if (args.Length >= 1)
                {
                    if ("dev".Equals(args[0]))
                    {
                        //devMode = true;
                        dataDir = "C:\\dev\\otserv\\mapeditor\\data\\";
                    }
                    else if ("dev_serv".Equals(args[0]))
                    {
                    //    devMode = true;
                        dataDir = "C:\\dev\\otserv\\mapeditor\\data\\";
                        ieserver = true;
                    }
                    else if ("serv".Equals(args[0]))
                    {                     
                        ieserver = true;
                        if ((args.Length >=2) && (!"".Equals(args[1])))
                        {
                            Global.inicialMap = args[1];
                        }
                    }
                    else
                    {
                        Global.inicialMap = args[0];
                    }
                }               

                Global.items = new ItemDatabase();
                Global.items.Load(dataDir + "960\\items.otb");                

                if (!ieserver)
                {

                    var client_file = XElement.Load(dataDir + "\\client.xml");
                    foreach (var client in client_file.Elements("client"))
                    {
                        tibiaDir = client.Attribute("tibia_dir").GetString();
                        if (!File.Exists(tibiaDir + "\\Tibia.spr"))
                        {
                            tibiaDir = Generic.OpenTibiaFolder();
                            if (!File.Exists(tibiaDir + "\\Tibia.spr"))
                            {
                                MessageBox.Show("Diretório inválido");
                                Environment.Exit(1);
                            }
                            client.Attribute("tibia_dir").SetValue(tibiaDir);
                            client_file.Save(dataDir + "\\client.xml");
                        }
                    } 

                    try
                    {
                        Global.gfx = new GraphicManager(Global.items);
                        Global.gfx.loadSpriteMetadata(tibiaDir + "\\Tibia.dat");
                        Global.gfx.loadSpriteData(tibiaDir + "\\Tibia.spr");
                    } 
                    catch (Exception ex2)
                    {
                        client_file = XElement.Load(dataDir + "\\client.xml");
                        foreach (var client in client_file.Elements("client"))
                        {
                            client.Attribute("tibia_dir").SetValue("");
                            client_file.Save(dataDir + "\\client.xml");
                        }
                        throw ex2;
                    }
                    

                    if (errorOnVersion)
                    {
                        client_file = XElement.Load(dataDir + "\\client.xml");
                        foreach (var client in client_file.Elements("client"))
                        {
                            client.Attribute("tibia_dir").SetValue("");
                            client_file.Save(dataDir + "\\client.xml");
                        }  
                    }

                }

                CreatureDatabase.creatureDatabase = new CreatureDatabase();
                CreatureDatabase.creatureDatabase.loadFromXML(dataDir + "960\\creatures.xml", false);

                if (!ieserver)
                {
                    Materials.getInstance().loadMaterials(XElement.Load(dataDir + "960\\materials.xml"), dataDir + "960\\");
                    Materials.getInstance().loadExtensions(dataDir + "\\extensions");
                    Materials.getInstance().createOtherTileset();
                    Brushes.getInstance().init();
                }

                if (Messages.messageList != "")
                {
                //    MessageBox.Show(Messages.messageList);
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message + "\n trace" + ex.StackTrace);
                aborted = true;
                Application.Exit();
            }
            if (!aborted)
            {
                if (!ieserver)
                {
                    Application.Run(new MainForm());
                }
                else
                {
                    Application.Run(new ServerForm());
                }
            }                      

        }
    }
}
