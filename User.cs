using System;
using System.Collections;
using System.Text;

namespace EaseBase
{
    public class Пользователь : Таблица
    {
        public string Логин, Пароль;
        public List<string> СписокСессий;

        public Пользователь(string ПолученныйЛогин, string ПолученныйПароль, List<string> ПолученныйСписокСессий)
        {
            this.СписокСессий = new List<string>();
            this.Логин = ПолученныйЛогин;
            this.Пароль = ПолученныйПароль;
            this.СписокСессий = ПолученныйСписокСессий;
        }
        public Пользователь(string ПолученныйЛогин, string ПолученныйПароль)
        {
            Логин = ПолученныйЛогин;
            Пароль = ПолученныйПароль;
            base.ТипТаблицы = "Пользователь";
            this.СписокСессий = new List<string>();
        }


        public override byte[] ТаблицаВВидеМассиваБайн()
        {
            List<string> ВыходнныеДанные = new List<string>();
            ВыходнныеДанные.Add(base.АЙДИ.ToString());
            ВыходнныеДанные.Add(base.ТипТаблицы);
            ВыходнныеДанные.Add(Логин);
            ВыходнныеДанные.Add(Пароль);
            if (СписокСессий.ToArray().Length >= 1)
            {
                ВыходнныеДанные.Add(Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join('\n', СписокСессий.ToArray()))));
            }
            else
            {
                ВыходнныеДанные.Add(Convert.ToBase64String(Encoding.UTF8.GetBytes("not")));
                Console.WriteLine("Нет Сессий к записи");
            }

            return Encoding.UTF8.GetBytes(string.Join('\n', ВыходнныеДанные));
        }
    }
}