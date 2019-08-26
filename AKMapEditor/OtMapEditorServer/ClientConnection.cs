using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Net;
using System.Windows.Forms;
using AKMapEditor.OtMapEditorServer.Classes;
using AKMapEditor.OtMapEditor;
using ProtoBuf;

namespace AKMapEditor.OtMapEditorServer
{
    public class ClientConnection
    {
        private TcpClient tcpclnt;
        private NetworkStream stream;
        private String ip;
        private int port;
        private Login login = null;
        private ServerInformation serverInformation = null;
        private MapEditor editor;
        private object sendLock = new object();


        public ClientConnection(String ip, int port, MapEditor editor)
        {      
            tcpclnt = new TcpClient();
            this.ip = ip;
            this.port = port;
            this.editor = editor;
        }

        public void connect()
        {
            tcpclnt.Connect(this.ip, this.port);
            stream = tcpclnt.GetStream();

            Thread t = new Thread(ReadMessages);
            t.Start();
        }

        public void close()
        {
            if (stream != null) stream.Close();
            if (tcpclnt != null) tcpclnt.Close();
        }

        public void ReadMessages()
        {
            try
            {
                bool connected = true;
                do
                {
                    int recebimento = stream.ReadByte();
                    switch (recebimento)
                    {
                        case MessageType.SERVER_INFORMATION:
                            serverInformation = Serializer.DeserializeWithLengthPrefix<ServerInformation>(stream, PrefixStyle.Base128);
                            LoginReturn();
                            break;
                        case MessageType.MAP_RESPONSE:
                            UpdateMap(Serializer.DeserializeWithLengthPrefix<MapResponse>(stream, PrefixStyle.Base128));
                            break;
                        case MessageType.MAP_UPDATE:
                            UpdateMap(Serializer.DeserializeWithLengthPrefix<MapUpdate>(stream, PrefixStyle.Base128));
                            break;
                        case 1:
                            connected = false;
                            break;
                        case -1:
                            connected = false;
                            break;
                        default:
                            Messages.AddLogMessage("Mensagem não tratada. Código =" + recebimento);
                            EmptyMessage message = Serializer.DeserializeWithLengthPrefix<EmptyMessage>(stream, PrefixStyle.Base128);
                            break;
                    }

                } while (connected);
            }
            catch (IOException ex)
            {
                editor.timerConnect.Enabled = false;
                editor.CloseMap();
                MessageBox.Show("Você foi desconectado, seu mapa foi fechado!");
            }
            catch (Exception ex)
            {
                editor.timerConnect.Enabled = false;
                MessageBox.Show("ReadMessages: " + ex.Message);
                editor.CloseMap();
            }
        }

        private void SendMessage(byte messageType)
        {
            try
            {
                lock (sendLock)
                {
                    stream.WriteByte(messageType);
                    switch (messageType)
                    {
                        case MessageType.LOGIN:
                            Serializer.SerializeWithLengthPrefix<Login>(stream, login, PrefixStyle.Base128);
                            break;                        
                    }                    
                    stream.Flush();
                }
            }
            catch (Exception ex)
            {
                editor.timerConnect.Enabled = false;
                MessageBox.Show("SendMessage: " + ex.Message);
                editor.CloseMap();
            }            
        }


        public void TestConnection()
        {
            SendMessage(MessageType.CHECK_CONNECTION);
        }

        public void LoginReturn()
        {
            if ("X".Equals(serverInformation.ErrorLogin))
            {
                throw new Exception("Wrong password");
            }

            if ("V".Equals(serverInformation.ErrorLogin))
            {
                throw new Exception("Invalid version");
            }

            editor.Title = this.getServerInformation().MapName;
            editor.gameMap.Width = this.getServerInformation().MapWidth;
            editor.gameMap.Height = this.getServerInformation().MapHeight;

            editor.updateViewMap();
        }

        public void doLogin(String password)
        {
            login = new Login();
            login.Password = password;
            login.AppVersion = Application.ProductVersion;            
            SendMessage(MessageType.LOGIN);
        }

        private bool received = false;

        public void RefreshMap(List<Position> positions)
        {
            MapRequest mapRequest = new MapRequest();
            mapRequest.positions = positions;
            lock (sendLock)            
            {
                stream.WriteByte(MessageType.MAP_REQUEST);
                stream.Flush();
                Serializer.SerializeWithLengthPrefix<MapRequest>(stream, mapRequest, PrefixStyle.Base128);
            }
            while (!received)
            {
                Thread.Sleep(10);
            }
            received = false;           
        }


        public void UpdateMousePosition(Position pos)
        {
            //
        }

        private void UpdateMap(MapResponse mresp)
        {
            Thread t = new Thread(() => editor.UpdateMap(mresp.Tiles));
            t.Start();
            received = true;
        }

        private void UpdateMap(MapUpdate mapUpdate)
        {
            Thread t = new Thread(() => editor.UpdateMap(mapUpdate));            
            t.Start();         
        }

        public ServerInformation getServerInformation()
        {
            return serverInformation;
        }
        
        public void UpdateServer(BatchAction batchAction, int updateType)
        {
            try
            {
                if (batchAction.type != ActionIdentifier.ACTION_SELECT)
                {                    
                    lock (sendLock)
                    {
                        MapUpdate mapUpdate = new MapUpdate();
                        mapUpdate.batchAction = batchAction;
                        mapUpdate.updateType = updateType;

                        stream.WriteByte(MessageType.MAP_UPDATE);
                        stream.Flush();
                        Serializer.SerializeWithLengthPrefix<MapUpdate>(stream, mapUpdate, PrefixStyle.Base128);
                    }
                } 
            }
            catch (Exception ex)
            {
                Generic.AjustError(ex);
            }
                       
        }
    }
}
