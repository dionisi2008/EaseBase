using System;

namespace EaseBase // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServerBase Сервер = new ServerBase(new EaseBaseApi("http://127.0.0.1:9090", "admin", "12345678", "Base1"), "Site", "Denis", "78987811");
            
            
            Сервер.СозданиеПользователя(new Пользователь("Deniska", "78987811"));
            Сервер.СозданиеПользователя(new Пользователь("Denisk", "78987811"));
            Сервер.СозданиеПользователя(new Пользователь("Deni", "78987811"));
            Сервер.СозданиеПользователя(new Пользователь("Den", "78987811"));
            System.Console.WriteLine(Сервер.ПоискПользователяВБазе("Deniska"));

        }
    }
}