#pragma warning disable 8618
namespace MCS
{
    public class Config
    {
        public static Config CurrentConfig{ get; set; }
        public static int DownloadThreadCount{get; set; }=20;
        public Config(int downloadThreadCount=20)
        {
            DownloadThreadCount = downloadThreadCount;
        }
    }
}