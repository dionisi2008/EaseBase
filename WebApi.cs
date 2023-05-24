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
            if (ПолученныйЗАпрос.Result.Request.HttpMethod == "POST")
            {
                byte[] Запрос = new byte[1024];
                byte[] ОработанныйЗапрос = new byte[ПолученныйЗАпрос.Result.Request.InputStream.Read(Запрос, 0, 1024)];
                ОработанныйЗапрос = Запрос.ToList().GetRange(0, ОработанныйЗапрос.Length).ToArray();


                string[] ПреобразованныйЗапросВТекст = Encoding.UTF8.GetString(ОработанныйЗапрос).Split("\n");
                List<string> ВыходныеДанные = new List<string>();
                for (int shag = 0; shag <= ПреобразованныйЗапросВТекст.Length - 1; shag++)
                {
                    if (ПреобразованныйЗапросВТекст[shag] != "")
                    {
                        ВыходныеДанные.Add(ПреобразованныйЗапросВТекст[shag]);
                    }

                }
                // System.Console.WriteLine(DateTime.Now.ToString() + " "  + string.Join('\n', ВыходныеДанные.ToArray()));
                if (ВыходныеДанные.Count > 0)
                {
                    switch (ВыходныеДанные[0])
                    {
                        case "Запрос":
                            List<string> ДанныеДляЗаписи = new List<string>();
                            List<byte> ВыходнныеДанные = new List<byte>();
                            switch (ВыходныеДанные[1])
                            {
                                case "Настройки Сервера Базы":
                                    ВыходнныеДанные.
                                    ДанныеДляЗаписи.Add(this.Настройки.ИмяХостаБазы);
                                    ДанныеДляЗаписи.Add(this.Настройки.ПортБазыДанных.ToString());
                                    ДанныеДляЗаписи.Add(this.Настройки.СозданиеЛогов.ToString());
                                    ДанныеДляЗаписи.Add(this.Настройки.ПутьДляЛогФайла);
                                    ДанныеДляЗаписи.Add(this.Настройки.РазмерХранилищаТаблиц.ToString());
                                    ВыходнныеДанные = new byte[] Encoding.UTF8.GetBytes(
                                        string.Join('\n', ДанныеДляЗаписи.ToArray())
                                    );
                                    ПолученныйЗАпрос.Result.Response.ContentLength64 = ВыходнныеДанные.Length;
                                    ПолученныйЗАпрос.Result.Response.OutputStream.Write(ВыходнныеДанные, 0, ВыходнныеДанные.Length);
                                    break;
                                case "Список Пользователей":

                                    byte[] ВыходнныеДанные = Encoding.UTF8.GetBytes(
                                        string.Join('\n', ДанныеДляЗаписи.ToArray())
                                    );
                                    ПолученныйЗАпрос.Result.Response.ContentLength64 = ВыходнныеДанные.Length;
                                    ПолученныйЗАпрос.Result.Response.OutputStream.Write(ВыходнныеДанные, 0, ВыходнныеДанные.Length);
                                    break;
                            }
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Type " + ПолученныйЗАпрос.Result.Request.HttpMethod);
                ПолученныйЗАпрос.Result.Response.ContentLength64 = 1024;
                ПолученныйЗАпрос.Result.Response.OutputStream.Write(new ReadOnlySpan<byte>(new byte[1024]));
            }
            ПолученныйЗАпрос.Result.Response.Close();
        }
        public byte[] ВыводДанныхВБайтовомМассиве(string ПолученныеДанные)
        {
            return Encoding.UTF8.GetBytes(ПолученныеДанные);
        }
    }
}
