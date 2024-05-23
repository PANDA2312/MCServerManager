using System;
using System.Data;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml;
namespace MCS
{
    public static class ColorConsole
    {
        [DllImport("../../lib/liblibcolorprint.so",CharSet =CharSet.Unicode,CallingConvention=CallingConvention.Cdecl)]
        public static extern void _c_color_print_3(FormatControl format,ForeGroundColor foreGround,BackGroundColor backGround,string str);
        [DllImport("../../lib/liblibcolorprint.so",CharSet =CharSet.Unicode,CallingConvention=CallingConvention.Cdecl)]
        public static extern void _c_color_print_2(FormatControl format,ForeGroundColor foreGround,string str);
        [DllImport("../../lib/liblibcolorprint.so",CharSet =CharSet.Unicode,CallingConvention=CallingConvention.Cdecl)]
        public static extern void _c_color_print_1(ForeGroundColor foreGround,string str);
        
        public enum BackGroundColor
        {
            Black=40,Red,Green,Yellow,Blue,Fuchsine,Cyan
        }
        public enum ForeGroundColor
        {
            Black=30,Red,Green,Yellow,Blue,Fuchsine,Cyan
        }
        public enum FormatControl
        {
            Reset=0,High_or_Bold,Dim,Underline,Glow,Reverse,Hide
        }
        public static void Write(ForeGroundColor foreGroundColor,string str){_c_color_print_1(foreGroundColor,str);}
        public static void Write(FormatControl format,ForeGroundColor foreGroundColor,string str){_c_color_print_2(format,foreGroundColor,str);}
        public static void Write(FormatControl format,ForeGroundColor foreGroundColor,BackGroundColor backGroundColor,string str){_c_color_print_3(format,foreGroundColor,backGroundColor,str);}
    }
    public static class Render
    {

    }
    public class ConsoleInterface
    {
        
    }
    public abstract class Control
    {
        public abstract void Hide();
        public abstract void Show();
        public abstract void Remove();
    }
    public class TextInput
    {
        public string Text{get;set;}
        public TextInput()
        {

        }
    }
}