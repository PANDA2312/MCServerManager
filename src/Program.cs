using System;
namespace MCS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Test.Start();
            Config config= new Config();
            Config.CurrentConfig = config;
        }
    }
}