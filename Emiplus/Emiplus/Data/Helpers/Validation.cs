using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Emiplus.Data.Helpers
{
    public static class Validation
    {
        public static double Round(double value)
        {
            return Math.Round(value, 2);
        }

        public static string CleanString(string dirtyString)
        {
            StringBuilder str = new StringBuilder(dirtyString);

            str.Replace('Ä', 'A').Replace('Å', 'A').Replace('Æ', 'A')
                .Replace('Ë', 'E').Replace('Ï', 'I').Replace('Ð', 'D').Replace('Ö', 'O')
                .Replace('Ø', 'O').Replace('Ü', 'U').Replace('ü', 'u').Replace('Ý', 'Y')
                .Replace('þ', 'b').Replace('ß', 'B').Replace('ä', 'a').Replace('å', 'a')
                .Replace('æ', 'a').Replace('ë', 'e').Replace('ï', 'i').Replace('ð', 'o')
                .Replace('ö', 'o').Replace('ø', 'o').Replace('ý', 'y').Replace('ÿ', 'y')
                .Replace('€', 'E').Replace('§', 'S').Replace("°", "").Replace("º", "")
                .Replace("ª", "").Replace("^", "").Replace("+", "").Replace("@", "")
                .Replace("#", "").Replace("$", "").Replace("%", "").Replace("¨", "")
                .Replace("\"", "").Replace("'", "").Replace("_", "").Replace("{", "")
                .Replace("}", "").Replace("[", "").Replace("]", "").Replace(";", "")
                .Replace(":", "").Replace("=", "").Replace("?", "").Replace(",", ".")
                .Replace("<", "").Replace(">", "").Replace("!", "").Replace("&", "")
                .Replace("*", "").Replace("(", "").Replace(")", "").Replace("/", "")
                .Replace("|", "").Replace("Þ", "p").Replace("-", " ").Replace("  ", "")
                .Replace("\\", "").Replace("~", "");

            return str.ToString().Trim();
        }

        /// <summary>
        /// Converte valor double pra ordem de dinheiro REAL
        /// </summary>
        /// <param name="obj">1234.95</param>
        /// <returns>1.234,95</returns>
        public static string FormatPrice(double obj, bool cifrao = false)
        {
            string res;

            if (cifrao)
            {
                res = obj.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("pt-br"));
            }
            else
            {
                //var nfi = new NumberFormatInfo { NumberDecimalSeparator = ",", NumberGroupSeparator = "." };
                //double d = obj;
                //res = d.ToString("#,##.##", nfi);
                res = string.Format("{0:N2}", obj);
            }

            return res;
        }

        public static string Price(double price)
        {
            return string.Format("{0:N2}", price);
        }

        public static double ConvertToDouble(object obj)
        {                        
            if (obj == null)
                return 0;

            if (obj.ToString() == "")
                return 0;            

            return Convert.ToDouble(obj.ToString().Replace("R$", "").Trim());
        }

        public static int ConvertToInt32(object obj)
        {
            if (obj == null)
                return 0;

            if (obj.ToString() == "")
                return 0;

            return Convert.ToInt32(obj);
        }

        public static string ConvertDateToForm(object date)
        {
            if (date == null)
                return "";

            if (String.IsNullOrEmpty(date.ToString()))
                return "";

            return Convert.ToDateTime(date).Day.ToString("00") + "/" + (Convert.ToDateTime(date).Month).ToString("00") + "/" + (Convert.ToDateTime(date).Year);
        }

        public static string ConvertDateToSQL(object date)
        {
            if (date == null)
                return "";

            if (String.IsNullOrEmpty(date.ToString()))
                return "";

            return Convert.ToDateTime(date).Year + "-" + (Convert.ToDateTime(date).Month).ToString("00") + "-" + (Convert.ToDateTime(date).Day).ToString("00");
        }

        public static string DateNowToSql()
        {
            //return DateTime.Now.Year + "-" + (DateTime.Now.Month).ToString("00") + "-" + (DateTime.Now.Day).ToString("00");
            return DateTime.Now.ToString("yyyy-MM-dd");
        }

        public static bool Event(object sender, dynamic control)
        {
            if (((Control)sender).Name == control.Name)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determines if a specific value is a number.
        /// </summary>
        /// <returns><c>true</c> if the value is a number; otherwise, <c>false</c>.</returns>
        /// <param name="value">Value.</param>
        /// <typeparam name="T">The Type of value.</typeparam>
        public static bool IsNumber<T>(this T value)
        {
            if (value is sbyte) return true;
            if (value is byte) return true;
            if (value is short) return true;
            if (value is ushort) return true;
            if (value is int) return true;
            if (value is uint) return true;
            if (value is long) return true;
            if (value is ulong) return true;
            if (value is float) return true;
            if (value is double) return true;
            if (value is decimal) return true;
            return false;
        }

        public static string OnlyNumbers(string toNormalize)
        {
            string resultString = string.Empty;
            Regex regexObj = new Regex(@"[^\d\d.\,]");
            resultString = regexObj.Replace(toNormalize, "");
            return resultString;
        }

        public static bool IsEqualTo<T>(this T firstValue, T secondValue) where T : IComparable<T>
        {
            return firstValue.Equals(secondValue);
        }

        public static bool IsGreaterThan<T>(this T firstValue, T secondValue) where T : IComparable<T>
        {
            return firstValue.CompareTo(secondValue) > 0;
        }

        public static bool IsLessThan<T>(this T firstValue, T secondValue) where T : IComparable<T>
        {
            return firstValue.CompareTo(secondValue) < 0;
        }

        public static bool IsGreaterThanOrEqualTo<T>(this T firstValue, T secondValue) where T : IComparable<T>
        {
            return (firstValue.IsEqualTo(secondValue) || firstValue.IsGreaterThan(secondValue));
        }

        public static bool IsLessThanOrEqualTo<T>(this T firstValue, T secondValue) where T : IComparable<T>
        {
            return (firstValue.IsEqualTo(secondValue) || firstValue.IsLessThan(secondValue));
        }
    }
}
