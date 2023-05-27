using System;
using System.Collections;

namespace EaseBase
{
    public class Таблица
    {
        public ulong АЙДИ;
        public string ТипТаблицы;
        public ulong ИндексПопулярностиСтраницы = 0;
        string[] ДанныеИзЗапроса;

        public Таблица()
        {

        }
        public Таблица(string[] ПолученныеДанныеИзЗапроса)
        {
            ДанныеИзЗапроса = ПолученныеДанныеИзЗапроса;
            this.АЙДИ = System.Convert.ToUInt64(ПолученныеДанныеИзЗапроса[0]);
            this.ТипТаблицы = ПолученныеДанныеИзЗапроса[1];
        }
        public virtual byte[] ТаблицаВВидеМассиваБайн()
        {
            return new byte[0];
        }
    }



}
