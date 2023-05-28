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
            БазаДанных = ДоступКБазеДанных;
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


                string[] ПреобразованныйЗапросВТекст = Encoding.UTF8.GetString(ОработанныйЗапрос).Split("\r\n");
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
                        case "Пользователь":
                            List<string> ДанныеДляЗаписи = new List<string>();
                            switch (ВыходныеДанные[1])
                            {
                                case "Список Пользователей":
                                    ДанныеДляЗаписи = new List<string>();

                                    byte[] ВыходнныеДанные2 = Encoding.UTF8.GetBytes(
                                        string.Join('\n', ДанныеДляЗаписи.ToArray())
                                    );
                                    ПолученныйЗАпрос.Result.Response.ContentLength64 = ВыходнныеДанные2.Length;
                                    ПолученныйЗАпрос.Result.Response.OutputStream.Write(ВыходнныеДанные2, 0, ВыходнныеДанные2.Length);
                                    break;
                                case "Регистрация пользователя":
                                    string СобщениеНаВыход;
                                    string[] ДанныеДляРегистрации = ВыходныеДанные[2].Split(' ');
                                    if (this.БазаДанных.ПоискПользователяВБазе(ДанныеДляРегистрации[0]))
                                    {
                                        СобщениеНаВыход = "Позьзователь с таким Логином, уже есть в базе";
                                    }
                                    else
                                    {
                                        this.БазаДанных.СозданиеПользователя(new Пользователь(ДанныеДляРегистрации[0], ДанныеДляРегистрации[1]));
                                        СобщениеНаВыход = "Позьзователь Успешно Зарегестрирован!";
                                        ПолученныйЗАпрос.Result.Response.ContentLength64 = Encoding.UTF8.GetBytes(СобщениеНаВыход).Length;
                                        ПолученныйЗАпрос.Result.Response.OutputStream.Write(Encoding.UTF8.GetBytes(СобщениеНаВыход), 0, Encoding.UTF8.GetBytes(СобщениеНаВыход).Length);
                                    }
                                    break;
                            }
                            // (логина, пароля, email и т.д.);

                            break;

                        case "Запрос":
                            List<string> ДанныеДляЗаписи0 = new List<string>();
                            switch (ВыходныеДанные[1])
                            {
                                case "Настройки Сервера Базы":
                                    ДанныеДляЗаписи = new List<string>();
                                    ДанныеДляЗаписи.Add(this.Настройки.ИмяХостаБазы);
                                    ДанныеДляЗаписи.Add(this.Настройки.ПортБазыДанных.ToString());
                                    ДанныеДляЗаписи.Add(this.Настройки.СозданиеЛогов.ToString());
                                    ДанныеДляЗаписи.Add(this.Настройки.ПутьДляЛогФайла);
                                    ДанныеДляЗаписи.Add(this.Настройки.РазмерХранилищаТаблиц.ToString());
                                    byte[] ВыходнныеДанные0 = Encoding.UTF8.GetBytes(
                                        string.Join('\n', ДанныеДляЗаписи.ToArray())
                                    );
                                    ПолученныйЗАпрос.Result.Response.ContentLength64 = ВыходнныеДанные0.Length;
                                    ПолученныйЗАпрос.Result.Response.OutputStream.Write(ВыходнныеДанные0, 0, ВыходнныеДанные0.Length);
                                    break;
                            }
                            break;
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
        }




            public byte[] ВыводДанныхВБайтовомМассиве(string ПолученныеДанные)
            {
                return Encoding.UTF8.GetBytes(ПолученныеДанные);
            }
        }
    }
