using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Helpers
{
    public static class GeneralHelper
    {
        public static SelectList SelectListForEnum(Type enumType)
        {
            List<object> values = new List<object>();
            foreach (var e in Enum.GetValues(enumType))
            {
                var memInfo = enumType.GetMember(e.ToString());
                var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                var description = ((DescriptionAttribute)attributes[0]).Description;
                values.Add(new { Id = e, Name = description });
            }
            return new SelectList(values, "Id", "Name");
        }

        public static SelectList SelectListForEnum<EnumType>(EnumType selectedValue)
        {
            Type enumType = typeof(EnumType);
            List<object> values = new List<object>();
            foreach (var e in Enum.GetValues(enumType))
            {
                var memInfo = enumType.GetMember(e.ToString());
                var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                var description = ((DescriptionAttribute)attributes[0]).Description;
                values.Add(new { Id = e, Name = description });
            }
            return new SelectList(values, "Id", "Name", selectedValue);
        }

        public static string ToDisplayableDate(this DateTime date)
        {
            string month;
            switch (date.Month)
            {
                case 1: month = " stycznia "; break;
                case 2: month = " lutego "; break;
                case 3: month = " marca "; break;
                case 4: month = " kwietnia "; break;
                case 5: month = " maja "; break;
                case 6: month = " czerwca "; break;
                case 7: month = " lipca "; break;
                case 8: month = " sierpnia "; break;
                case 9: month = " września "; break;
                case 10: month = " października "; break;
                case 11: month = " listopada "; break;
                case 12: month = " grudnia "; break;
                default: month = " miesiąca "; break;
            }
            return date.Day.ToString() + month + date.Year.ToString();
        }
    }
}