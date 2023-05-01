using System;
using System.Collections;

namespace EaseBase
{
    public class Таблица
    {
        public ulong АЙДИ;
        public string ТипТаблицы;

        public virtual byte[] ТаблицаВВидеМассиваБайн()
        {
            return new byte[0];
        }
    }



}
