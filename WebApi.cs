using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Text;

namespace EaseBase
{
    public class ВебАпи
    {
        HttpListener СерверАпиДляЗапросов = new HttpListener();
        ServerBase БазаДанных;
        public НастройкиБазы Настройки;
        public ВебАпи(ServerBase ДоступКБазеДанных)
        {
            this.Настройки = ДоступКБазеДанных.ВыгрузкаНастроекБазы();
            СерверАпиДляЗапросов.Prefixes.Add("http://" + Настройки.ИмяХостаБазы  + ':' + 
            Настройки.ПортБазыДанных + "/");
            СерверАпиДляЗапросов.Start();
            do
            {

            } while (СерверАпиДляЗапросов.IsListening == true);
            ЧтениеНовогоЗапроса(СерверАпиДляЗапросов.GetContextAsync());

        }
        public async void ЧтениеНовогоЗапроса(Task<System.Net.HttpListenerContext> ПолученныйЗАпрос)
        {
            Console.WriteLine("Debug");
            ПолученныйЗАпрос.Result.ToString();
        }
    }
}
