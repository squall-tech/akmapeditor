using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AKMapEditor.OtMapEditor
{

    public class ClientVersion
    {
        private UInt16 verId;
        private String versionName;
        private String data_path;
       // private String client_path;
        private List<Tuple<UInt32, UInt32>> version_id_list;
        public static Dictionary<UInt16, ClientVersion> versions;

        public ClientVersion(UInt16 id, String versionName, String data_path, List<Tuple<UInt32, UInt32>> verIds)
        {
            version_id_list = new List<Tuple<uint, uint>>();
            verId = id;
            this.versionName = versionName;
            this.data_path = data_path;
            version_id_list.AddRange(verIds);
           // client_path = "";
        }



        public static void loadVersions()
        {

        }

        public static void addVersion(ClientVersion ver)
        {

        }

        public static ClientVersion get(UInt16 id)
        {
            ClientVersion clientVersion;
            versions.TryGetValue(id, out clientVersion);
            return clientVersion;
        }        
    }
}
