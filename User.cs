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
        => (Логин, Пароль, base.ТипТаблицы) = (ПолученныйЛогин, ПолученныйПароль,
        "Пользователь");

        public override byte[] ТаблицаВВидеМассиваБайн()
        {
            List<string> ВыходнныеДанные = new List<string>();
            ВыходнныеДанные.Add(base.АЙДИ.ToString());
            ВыходнныеДанные.Add(base.ТипТаблицы);
            ВыходнныеДанные.Add(Логин);
            ВыходнныеДанные.Add(Пароль);
            ВыходнныеДанные.Add(Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join('\n', СписокСессий.ToArray()))));
            return Encoding.UTF8.GetBytes(string.Join('\n', ВыходнныеДанные));
        }
    }
}