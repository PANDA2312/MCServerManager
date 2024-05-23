#include <stdio.h>
void _c_color_printf_3(int format,int foreground,int background,char* text)
{
    printf("\033[0m\033[%d;%d;%dm%s\033[0m",format,foreground,background,text);
}
void _c_color_printf_2(int format,int foreground,char* text)
{
    printf("\033[0m\033[%d;%dm%s\033[0m",format,foreground,text);
}
void _c_color_printf_1(int foreground,char* text)
{
    printf("\033[0m\033[0;%dm%s\033[0m",foreground,text);
}
void _c_reset_color()
{
    printf("\033[0m");
}