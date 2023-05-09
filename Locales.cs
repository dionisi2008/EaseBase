using System;
using System.Collections;
using System.Text;

namespace EaseBase
{
    public class Локаль : Таблица
    {
        public string РасположениеЮрл = "";
        public Dictionary<string, string> СписокКлючейИЗначений = new Dictionary<string, string>();

        public Локаль(string ПолученныйЮРЛ)
        => (РасположениеЮрл, base.ТипТаблицы) = (ПолученныйЮРЛ, "Локаль");


        public override byte[] ТаблицаВВидеМассиваБайн()
        {
            List<string> ВыходнныеДанные = new List<string>();
            ВыходнныеДанные.Add(base.АЙДИ.ToString());
            ВыходнныеДанные.Add(base.ТипТаблицы);
            ВыходнныеДанные.Add(РасположениеЮрл);
            for (int shag = 0; shag <= this.СписокКлючейИЗначений.Count - 1; shag++)
            {
                ВыходнныеДанные.Add(СписокКлючейИЗначений.Keys.ToArray<String>()[shag]);
                ВыходнныеДанные.Add(СписокКлючейИЗначений[СписокКлючейИЗначений.Keys.ToArray<String>()[shag]]);
            }
            return Encoding.UTF8.GetBytes(string.Join('\n', ВыходнныеДанные));
        }
    }
}