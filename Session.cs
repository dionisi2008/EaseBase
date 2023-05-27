using System;
using System.Collections;
using System.Text;

namespace EaseBase
{
    public class Сессия : Таблица
    {
        public ulong АЙДИПользователя;
        public DateTime НачалоСессии;
        public DateTime КонецСессии;
        public List<string> СписокАпиВзаимодействий;
        public bool БылаЛиСессияЗащищена;
        public DateTime ПоследняяАктивность;
        public const int ДопустимоеКолличествоМинутВБездействии = 120;
        public Сессия(string[] ПолученныйДанныеДляКонструктора) : base(ПолученныйДанныеДляКонструктора)
        {
            АЙДИПользователя = System.Convert.ToUInt64(ПолученныйДанныеДляКонструктора[2]);
            НачалоСессии = Convert.ToDateTime(ПолученныйДанныеДляКонструктора[3]);
            КонецСессии = Convert.ToDateTime(ПолученныйДанныеДляКонструктора[4]);
            СписокАпиВзаимодействий = new List<string>(Encoding.UTF8.GetString(Convert.FromBase64String(ПолученныйДанныеДляКонструктора[5])).Split('\n'));
            БылаЛиСессияЗащищена = Convert.ToBoolean(ПолученныйДанныеДляКонструктора[6]);
            ПоследняяАктивность = Convert.ToDateTime(ПолученныйДанныеДляКонструктора[7]);
        }

        public override byte[] ТаблицаВВидеМассиваБайн()
        {
            List<string> ВыходнныеДанные = new List<string>();
            ВыходнныеДанные.Add(base.АЙДИ.ToString());
            ВыходнныеДанные.Add(base.ТипТаблицы);
            ВыходнныеДанные.Add(АЙДИПользователя.ToString());
            ВыходнныеДанные.Add(НачалоСессии.ToString());
            ВыходнныеДанные.Add(КонецСессии.ToString());
            ВыходнныеДанные.Add(Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join('\n', СписокАпиВзаимодействий))));
            ВыходнныеДанные.Add(БылаЛиСессияЗащищена.ToString());
            ВыходнныеДанные.Add(ПоследняяАктивность.ToString());
            return Encoding.UTF8.GetBytes(string.Join('\n', ВыходнныеДанные));
        }
    }
}