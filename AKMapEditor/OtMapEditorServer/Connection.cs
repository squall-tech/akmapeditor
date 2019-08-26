using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Net;
using System.Windows.Forms;
using ProtoBuf;
using AKMapEditor.OtMapEditorServer.Classes;
using AKMapEditor.OtMapEditor;
using Action = AKMapEditor.OtMapEditor.Action;

namespace AKMapEditor.OtMapEditorServer
{
    public class Connection
    {
        private static int id = 0;

        private TcpClient tcpClient;
        private NetworkStream stream = null;       
        private Thread thread;
        private ServerForm form;
        private Login login = null;
        private String ip = "";
        private GameMap clientMap = new GameMap();

        private Object lockMessage = new object();

        public Connection(TcpClient tcpClient, ServerForm form)
        {
            this.tcpClient = tcpClient;
            this.form = form;
            this.thread = new Thread(doConnection);
            this.stream = tcpClient.GetStream();            
        }

        public void init()
        {
            thread.Start();
        }

        public void doConnection()            
        {
            try
            {
                try
                {
                    try
                    {
                        this.ip = tcpClient.Client.RemoteEndPoint.ToString();
                        addLog("Connected with " + this.ip);
                        bool connected = false;
                        do
                        {
                            connected = tcpClient.Connected;
                            int recebimento = stream.ReadByte();
                            switch (recebimento)
                            {
                                case MessageType.LOGIN:
                                    login = Serializer.DeserializeWithLengthPrefix<Login>(stream, PrefixStyle.Base128);
                                    WriteMessage(MessageType.SERVER_INFORMATION);
                                    break;
                                case MessageType.MAP_REQUEST:
                                    WriteMessage(MessageType.MAP_REQUEST);
                                    break;
                                case MessageType.MAP_UPDATE:
                                    MapUpdate();
                                    break;
                                case MessageType.CHECK_CONNECTION:
                                    break;
                                case 1:
                                    connected = false;
                                    break;
                                case -1:
                                    connected = false;
                                    break;
                                default:
                                    addLog("Mensagem não tratada. Código =" + recebimento);
                                    EmptyMessage message = Serializer.DeserializeWithLengthPrefix<EmptyMessage>(stream, PrefixStyle.Base128);
                                    break;
                            }
                        } while (connected);
                    }
                    catch (Exception ex)
                    {
                        addLog(ex.Message + "\n" + ex.StackTrace);
                    }
                }
                finally
                {
                    stream.Close();
                    tcpClient.Close();
                    addLog("conexão finalizada para " + ip);
                    form.removeConnection(this);
                }
            }
            catch { }
        }


        private void WriteMessage(int type, Object obj = null)
        {
            try
            {
                lock (lockMessage)
                {
                    switch (type)
                    {
                        case MessageType.SERVER_INFORMATION:
                            ServerInformation si = new ServerInformation();
                            if (!Application.ProductVersion.Equals(login.AppVersion))
                            {
                                si.ErrorLogin = "V";
                            }
                            else if (form.verifyPassword(login.Password))
                            {
                                si.MapName = getMap().MapName;
                                si.MapWidth = getMap().Width;
                                si.MapHeight = getMap().Height;
                            }
                            else
                            {
                                si.ErrorLogin = "X";
                            }
                            stream.WriteByte(MessageType.SERVER_INFORMATION);
                            stream.Flush();
                            Serializer.SerializeWithLengthPrefix<ServerInformation>(stream, si, PrefixStyle.Base128);
                            break;
                        case MessageType.MAP_REQUEST:
                            UpdateMap();
                            break;
                        case MessageType.MAP_UPDATE:
                            stream.WriteByte(MessageType.MAP_UPDATE);
                            stream.Flush();
                            Serializer.SerializeWithLengthPrefix<MapUpdate>(stream, (MapUpdate)obj, PrefixStyle.Base128);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                try
                {
                    addLog("WriteMessage - " + ex.Message + "\n" + ex.StackTrace);
                }
                catch { }                
            }            
        }

        private void addLog(String log)
        {
            try
            {
                form.addLogThread(log);
            }
            catch { }
            
        }

        private void MapUpdate()
        {
            try
            {
                MapUpdate mapUpdate = Serializer.DeserializeWithLengthPrefix<MapUpdate>(stream, PrefixStyle.Base128);
                Thread t = new Thread(() => MapUpdate(mapUpdate));
                t.Start();
            }
            catch (Exception ex)
            {
                addLog("MapUpdate()" + ex.Message + "\n" + ex.StackTrace);
            }

        }

        public void MapUpdate(MapUpdate mapUpdate)
        {
            try
            {
                getMap().UpdateMap(mapUpdate,null);
                form.UpdateMaps(mapUpdate, this);                
            }
            catch (Exception ex)
            {
                addLog("MapUpdate - " + ex.Message + "trace: " +  ex.StackTrace);
            }
            
        }

        public void SendMapUpdates(MapUpdate mapUpdate)
        {
            try
            {
                WriteMessage(MessageType.MAP_UPDATE, mapUpdate);
            }
            catch (Exception ex)
            {
                addLog("SendMapUpdates" + ex.Message + ex.StackTrace);
            }
            
        }

        private void UpdateMap()
        {
            try
            {
                MapRequest mapRequest = Serializer.DeserializeWithLengthPrefix<MapRequest>(stream, PrefixStyle.Base128);

                List<Tile> tiles = new List<Tile>();
                GameMap map = getMap();
                foreach (Position pos in mapRequest.positions)
                {
                    Tile tile = map.getTile(pos);
                    if (tile != null)
                    {
                        tiles.Add(tile);
                    }
                }

                MapResponse mapResponse = new MapResponse();
                mapResponse.Tiles = tiles;
                stream.WriteByte(MessageType.MAP_RESPONSE);
                stream.Flush();
                Serializer.SerializeWithLengthPrefix<MapResponse>(stream, mapResponse, PrefixStyle.Base128);
            }
            catch(Exception ex)
            {
                addLog("UpdateMap()" + ex.Message + "\n" + ex.StackTrace);
            }
        }        

        private GameMap getMap()
        {
            return form.gameMap;
        }
    }
}

