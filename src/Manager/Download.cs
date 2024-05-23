using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Emit;
using System.Threading;
using System.IO;
using MCS;
#pragma warning disable 8600
#pragma warning disable 8604
#pragma warning disable 8629
namespace MCS
{
    
    public static class Downloader
    {
        private static long finished=0;
        public static long GetFileSize(HttpClient client,string url)
        {
            HttpResponseMessage response = client.GetAsync(url,HttpCompletionOption.ResponseHeadersRead).Result;
            return response.Content.Headers.ContentLength.Value;
        }
        ///<summary>
        /// 请使用<see cref="Task"/>,<see cref="Thread"/>或者其他多线程调用
        ///</summary>
        public static async void DownloadFile(string path,string url)
        {
            List<Task>tasks= new List<Task>();
            HttpClient client = new HttpClient(new HttpClientHandler(){AllowAutoRedirect=true});
            long size = GetFileSize(client,url);
            long oneThreadCount = size / Config.DownloadThreadCount;
            for (long i= 0; i < Config.DownloadThreadCount; i++)
            {
                tasks.Add(DownloadPart(client, path, url, i * oneThreadCount, (i == Config.DownloadThreadCount - 1) ? size - 1 : (i + 1) * oneThreadCount - 1));  
            }
            await Task.WhenAll(tasks);
            client.Dispose();   
        }
        public async static Task DownloadPart(HttpClient client,string path,string url,long start,long end)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get,url);
            HttpResponseMessage response=client.Send(request);
            byte[] buffer = await response.Content.ReadAsByteArrayAsync();
            FileStream fs = new FileStream(path,FileMode.OpenOrCreate,FileAccess.Write,FileShare.Write);
            fs.Position=start;
            await fs.WriteAsync(buffer,0,buffer.Length);
            fs.Flush();
            fs.Close();
            Interlocked.Increment(ref finished);
            if(finished==Config.DownloadThreadCount)client.Dispose();
        }
    }
    public class ServerFile
    {
        public static string CacheDir{get;set;}=Environment.CurrentDirectory+"/cache";
        public static string ServerDir{get;set;}=Environment.CurrentDirectory+"/server";
        public static string ConfigDir{get;set;}=Environment.CurrentDirectory+"/config";
        
    }
    public static class BMCLAPI
    {
        
        public static void Download_Vanilla(MCInfo.Version ver)
        {
            Downloader.DownloadFile("./vanilla_"+MCInfo.current[ver]+".jar",$"https://bmclapi2.bangbang93.com/version/{MCInfo.current[ver]}/server");
        }
    }
}