using System;
namespace MCS
{
    public static class Utlils
    {
        public static bool IsDirectoryValid(string path)
        {
            if(path[0]!='/')return false;
            char[] InValidChars=Path.GetInvalidPathChars();
            for(int i=0;i<InValidChars.Length;i++)
            {
                if(path.IndexOf(InValidChars[i])!=-1)return false;
            }
            for(int i=0;i<path.Length;i++)
            {
                if(i>=1&&path[i]=='/')
                {
                    if(path[i-1]=='/')return false;
                }
            }
            return true;
        }
        public static string GetDirectory()
        {
            string path=Console.ReadLine();
            while(!IsDirectoryValid(path))
            {
                path=Console.ReadLine();
            }
            return path;
        }
    }
}