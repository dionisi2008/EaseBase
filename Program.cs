using System;

namespace EaseBase // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServerBase Сервер = new ServerBase(new EaseBaseApi("http://10.1.2.2:100", "admin", "12345678", "Base1"), "Site", "Denis", "78987811");
            // Сервер.СозданиеПользователя(new Пользователь("denis", "1234"));
            // for (int shag = 0; shag <= 1024; shag++)
            // {
            //     Сервер.СозданиеПользователя(new Пользователь(shag.ToString(), "1234"));
            // }
            // Console.WriteLine("Hello World!");
            
        }
    }
}