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
            СерверАпиДляЗапросов.Prefixes.Add("http://" + Настройки.ИмяХостаБазы + ':' +
            Настройки.ПортБазыДанных + "/");
            СерверАпиДляЗапросов.Start();
            do
            {
                ЧтениеНовогоЗапроса(СерверАпиДляЗапросов.GetContextAsync());
            } while (СерверАпиДляЗапросов.IsListening == true);


        }
        public async void ЧтениеНовогоЗапроса(Task<System.Net.HttpListenerContext> ПолученныйЗАпрос)
        {
            byte[] Запрос = new byte[1024];
            byte[] ОработанныйЗапрос = new byte[ПолученныйЗАпрос.Result.Request.InputStream.Read(Запрос, 0, 1024)];
            ОработанныйЗапрос = Запрос.ToList().GetRange(0, ОработанныйЗапрос.Length).ToArray();


            string[] ПреобразованныйЗапросВТекст = Encoding.UTF8.GetString(ОработанныйЗапрос).Split('\n');
            List<string> ВыходныеДанные = new List<string>();
            for (int shag = 0; shag <= ПреобразованныйЗапросВТекст.Length - 1; shag++)
            {
                if (ПреобразованныйЗапросВТекст[shag] != "")
                {
                    ВыходныеДанные.Add(ПреобразованныйЗапросВТекст[shag]);
                }

            }
            System.Console.WriteLine(DateTime.Now.ToString() + " "  + string.Join('\n', ВыходныеДанные.ToArray()));
            if (ВыходныеДанные.Count > 0)
            {
                switch (ВыходныеДанные[0])
                {
                    case "Запрос":
                        switch (ВыходныеДанные[1])
                        {
                            case "Настройки Сервера Базы":
                                List<string> ДанныеДляЗаписи = new List<string>();
                                ДанныеДляЗаписи.Add(this.Настройки.ИмяХостаБазы);
                                ДанныеДляЗаписи.Add(this.Настройки.ПортБазыДанных.ToString());
                                ДанныеДляЗаписи.Add(this.Настройки.СозданиеЛогов.ToString());
                                ДанныеДляЗаписи.Add(this.Настройки.ПутьДляЛогФайла);
                                ДанныеДляЗаписи.Add(this.Настройки.РазмерХранилищаТаблиц.ToString());
                                 await new StreamWriter(ПолученныйЗАпрос.Result.Response.OutputStream).WriteAsync(string.Join('\n', ДанныеДляЗаписи.ToArray()));

                                break;
                        }
                        break;
                }
            }
            ПолученныйЗАпрос.Result.Response.Close();
        }
    }
}
