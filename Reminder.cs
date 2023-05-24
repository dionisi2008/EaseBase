using System;
using System.Collections;
using System.Text;

namespace EaseBase
{
    public class Напоминание : Таблица
    {
        public ulong АЙДИАвтораНапоминаия;
        public string ТекстНапоминания;
        public string ДатаСозданияПоминания;
        public string ДатаИзмененияПоминания;
        public string ПриложенныйФайлВБэйс64;
        public Напоминание()
        {

        }
        public override byte[] ТаблицаВВидеМассиваБайн()
        {
            List<string> ВыходнныеДанные = new List<string>();
            ВыходнныеДанные.Add(base.АЙДИ.ToString());
            ВыходнныеДанные.Add(base.ТипТаблицы);
            return Encoding.UTF8.GetBytes(string.Join('\n', ВыходнныеДанные.ToArray()));
        }
    }
}