using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;
using System.IO;
namespace AssemblyCSharp.mod.data
{
    public class dataHandle
    {
        public static dataHandle gI()
        {
            if (gi == null)
            {
                gi = new dataHandle();
            }
            return gi;
        }
        public dataHandle()
        {
            if (!File.Exists(urlFile))
            {
                datajson = new Datajson
                {
                    IpServer = " VIP:fw.nrovip.top:6969:0,0,0",
                    speed = 5.0f,
                    width = 1024,
                    height = 600
                };
                string jsonString = JsonMapper.ToJson(datajson);
                File.WriteAllText(urlFile, jsonString);
            }
            updateData();
        }
        public void updateData()
        {
            string jsonString = File.ReadAllText(urlFile);
            datajson = JsonMapper.ToObject<Datajson>(jsonString);
        }

        public Datajson datajson;

        private static dataHandle gi;

        private string urlFile = "data.json";
    }
    public class Datajson
    {
        public string IpServer { get; set; }
        public float speed { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }
}
