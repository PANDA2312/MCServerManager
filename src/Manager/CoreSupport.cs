using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace MCS
{
    public class MCInfo
    {
        public enum CoreType
        {
            Vanilla,Bukkit,Spigot,Paper,Purpur,Fabric,Forge,Bungeecord,Waterfall
        }
        public static string[] CoreTypeStr=[
            "Vanilla","Bukkit","Spigot","Paper","Purpur","Fabric","Forge","Bungeecore","Waterfall"
        ];
        public static MCInfo current=new MCInfo();
        private MCInfo()
        {
        }
        public enum Version
        {
            _1_8_8,_1_8_9,
            _1_9,_1_9_1,_1_9_2,_1_9_3,_1_9_4,
            _1_10,_1_10_1,_1_10_2,
            _1_11,_1_11_1,_1_11_2,
            _1_12,_1_12_1,_1_12_2,
            _1_13,_1_13_1,_1_13_2,
            _1_14,_1_14_1,_1_14_2,_1_14_3,_1_14_4,
            _1_15,_1_15_1,_1_15_2,
            _1_16,_1_16_1,_1_16_2,_1_16_3,_1_16_4,_1_16_5,
            _1_17,_1_17_1,
            _1_18,_1_18_1,_1_18_2,
            _1_19,_1_19_1,_1_19_2,_1_19_3,_1_19_4,
            _1_20,_1_20_1,_1_20_2
        }
        public string this[Version index]
        {
            get { return VersionStr[(int)index];}
        }
        public static string[] VersionStr=[
            "1.8.8","1.8.9",
            "1.9","1.9.1","1.9.2","1.9.3","1.9.4",
            "1.10","1.10.1","1.10.2","1.11","1.11.1","1.11.2",
            "1.12","1.12.1","1.12.2",
            "1.13","1.13.1","1.13.2",
            "1.14","1.14.1","1.14.2","1.14.3","1.14.4",
            "1.15","1.15.1","1.15.2",
            "1.16","1.16.1","1.16.2","1.16.3","1.16.4","1.16.5",
            "1.17","1.17.1",
            "1.18","1.18.1","1.18.2",
            "1.19","1.19.1","1.19.2","1.19.3","1.19.4",
            "1.20","1.20.1","1.20.2"
        ];
    }
    public static class Paper
    {
        public static string GetVersionUrl(MCInfo.Version version)
        {
            return "https://api.papermc.io/v2/projects/paper/versions/" + MCInfo.current[version];
        }
        public static int GetLatestBuild(MCInfo.Version version)
        {
            HttpClient client = new HttpClient();
            string jsonString = client.GetStringAsync(GetVersionUrl(version)).Result;
            JObject jsonObj = JObject.Parse(jsonString);
            int max=0;
            foreach(int val in jsonObj["builds"])
            {
                if(val>max)max=Convert.ToInt32(val);
            }
            return max;
        }
        public static string GetFileUrl(MCInfo.Version version,int build=-1)
        {
            if(build==-1)build=GetLatestBuild(version);
            return $"https://api.papermc.io/v2/projects/paper/versions/{MCInfo.current[version]}/builds/{build}/downloads/paper-{MCInfo.current[version]}-{build}.jar";
        }
        public static string GetFileName(MCInfo.Version version,int build=-1)
        {
            return $"paper-{version}-{build}.jar";
        }
        ///<summary>
        /// 请使用<see cref="Task"/>,<see cref="Thread"/>或者其他多线程调用
        ///</summary>
        public static void Download(MCInfo.Version version,string dir,int build=-1)
        {
            string url = string.Empty;
            if(build==-1)url=GetFileUrl(version);
            if(dir[dir.Length-1]=='/')dir=dir.Remove(dir.Length-1);
            Downloader.DownloadFile(dir+"/"+GetFileName(version,build),url);
        }
    }
}