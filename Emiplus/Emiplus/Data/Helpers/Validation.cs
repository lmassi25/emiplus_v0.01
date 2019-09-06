using System;
using System.Collections.Generic;
using System.Globalization;

namespace Emiplus.Data.Helpers
{
    public static class Validation
    {
        /// <summary>
        /// Converte valor double pra ordem de dinheiro REAL
        /// </summary>
        /// <param name="obj">1234.95</param>
        /// <returns>1.234,95</returns>
        public static string FormatPrice(double obj, bool cifrao = false)
        {
            string res;

            if (cifrao) {
                res = obj.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("pt-br"));
            }
            else
            {
                var nfi = new NumberFormatInfo { NumberDecimalSeparator = ",", NumberGroupSeparator = "." };
                double d = obj;
                res = d.ToString("#,##.##", nfi);
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

            return Convert.ToDouble(obj);
        }

        public static int ConvertToInt32(object obj)
        {
            if (obj == null)
                return 0;

            if (obj.ToString() == "")
                return 0;

            return Convert.ToInt32(obj);
        }

        public static bool Event(object sender, dynamic control)
        {
            if (sender.GetType().Equals(control.GetType()) && ((dynamic)sender).Name == control.Name)
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
