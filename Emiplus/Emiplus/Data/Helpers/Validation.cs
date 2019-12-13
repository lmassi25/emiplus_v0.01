using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
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

        public static double RoundAliquotas(double value)
        {
            return Math.Round(value, 4);
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
                .Replace("|", "").Replace("Þ", "p").Replace("-", " ").Replace("  ", " ")
                .Replace("\\", "").Replace("~", "");

            return str.ToString().Trim();
        }

        public static string CleanStringForFiscal(string dirtyString)
        {
            StringBuilder str = new StringBuilder(CleanString(dirtyString));

            if(str.ToString().Replace(" ", "").All(char.IsDigit))
            {
                str = str.Replace(" ", "");
            }

            return (Encoding.ASCII.GetString(Encoding.GetEncoding("Cyrillic").GetBytes(str.ToString())).ToString().Trim()).ToUpper();
        }

        public static string OneSpaceString(string aux)
        {
            aux = aux.TrimStart();
            aux = aux.TrimEnd();
            
            Regex regex = new Regex(@"\s{2,}");
            aux = regex.Replace(aux, " ");

            return aux;
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
                res = obj.ToString("C", CultureInfo.GetCultureInfo("pt-br"));
            }
            else
            {
                res = string.Format("{0:N2}", obj);
            }

            return res;
        }

        public static string FormatDecimalPrice(decimal obj, bool cifrao = false)
        {
            string res;

            if (cifrao)
            {
                res = obj.ToString("C", CultureInfo.GetCultureInfo("pt-br"));
            }
            else
            {
                res = string.Format("{0:N2}", obj);
            }

            return res;
        }

        public static string Price(double price)
        {
            return string.Format("{0:N2}", price);
        }

        public static string FormatPriceWithDot(object obj, int decimais = 2)
        {
            string aux = "";

            switch (decimais)
            {
                case 3:
                    aux = string.Format("{0:N3}", Validation.ConvertToDouble(obj));
                    break;
                case 4:
                    aux = string.Format("{0:N4}", Validation.ConvertToDouble(obj));
                    break;
                case 5:
                    aux = string.Format("{0:N5}", Validation.ConvertToDouble(obj));
                    break;
                case 6:
                    aux = string.Format("{0:N6}", Validation.ConvertToDouble(obj));
                    break;
                default:
                    aux = string.Format("{0:N2}", Validation.ConvertToDouble(obj));
                    break;
            }
            
            aux = aux.Replace(".", "");
            aux = aux.Replace(",", ".");

            return aux;
        }

        public static double ConvertToDouble(object obj)
        {                        
            if (obj == null)
                return 0;

            if (obj.ToString() == string.Empty)
                return 0;            

            return Convert.ToDouble(obj.ToString().Replace("R$", "").Trim());
        }

        public static int ConvertToInt32(object obj)
        {
            if (obj == null)
                return 0;

            if (obj.ToString() == string.Empty)
                return 0;

            return Convert.ToInt32(obj);
        }

        public static string ConvertDateToForm(object date, bool large = false)
        {
            if (date == null)
                return "";

            if (String.IsNullOrEmpty(date.ToString()))
                return "";

            DateTime temp;
            if (!DateTime.TryParse(date.ToString(), out temp))
                return "";

            string data;
            if (large)
                data = Convert.ToDateTime(date).ToString("dd/MM/yyyy HH:mm");
            else
                data = Convert.ToDateTime(date).ToString("dd/MM/yyyy");

            if (data == "01/01/0001 00:00")
                data = "";

            return data;
        }

        public static string ConvertDateToSql(object date, bool large = false)
        {
            if (date == null)
                return "";

            if (String.IsNullOrEmpty(date.ToString()))
                return "";

            DateTime temp;
            if (!DateTime.TryParse(date.ToString(), out temp))
                return "";

            string data;
            if (large)
                data = Convert.ToDateTime(date).ToString("yyyy-MM-dd HH:mm");
            else
                data = Convert.ToDateTime(date).ToString("yyyy-MM-dd");

            return data;
        }

        public static string DateNowToSql()
        {
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

        public static string FormatNumberKilo(double num)
        {
            //long i = (long)Math.Pow(10, (int)Math.Max(0, Math.Log10(num) - 2));

            //if(i > 0)
            //{
            //    num = num / i * i;

            //    if (num >= 1000000)
            //        return (num / 1000000D).ToString("0.##") + " Ton";
            //    if (num >= 1000)
            //        return (num / 1000D).ToString("0.##") + " KG";
            //}

            //return num.ToString("#,0");
            return num.ToString();
        }

        public static string FormatNumberUnidade(double num)
        {
            //return num.ToString() + " UN";
            return num.ToString();
        }

        public static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))                
                return "";
            return input.First().ToString().ToUpper() + input.Substring(1);
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

        public static void BorderInput(VisualPlus.Toolkit.Controls.Editors.VisualTextBox input, BorderColor color)
        {
            switch (color)
            {
                case BorderColor.Vermelho:
                    input.Border.Color = Color.FromArgb(255, 128, 128);
                    input.Border.HoverColor = Color.FromArgb(255, 128, 128);
                    break;
                case BorderColor.Azul:
                    input.Border.Color = Color.FromArgb(128, 128, 255);
                    input.Border.HoverColor = Color.FromArgb(128, 128, 255);
                    break;
                default:
                    input.Border.Color = Color.Gainsboro;
                    input.Border.HoverColor = Color.Gainsboro;
                    break;
            }
        }

        public enum BorderColor {
            Vermelho, Azul
        }

        public static int digitoVerificador(string codbarras)
        {
            int digitoParaSomar = 0;

            double pares = 0, impares = 0, total = 0, multiplicador = 1;

            int item = 0;

            if (codbarras != "")
            {
                if (codbarras.Length == 12)
                {
                    item = 0;
                    while (item < 12)
                    {
                        string auxNum = "";
                        auxNum = codbarras.Substring(item, 1);
                        //MessageBox.Show(auxNum);

                        if ((ConvertToInt32(auxNum) % 2) == 0)
                            pares = pares + (ConvertToInt32(auxNum) * multiplicador);
                        else
                            impares = impares + (ConvertToInt32(auxNum) * multiplicador);

                        if (multiplicador == 1)
                            multiplicador = 3;
                        else
                            multiplicador = 1;

                        item++;
                    }
                }
            }

            //impares = impares * 3;
            total = impares + pares;

            digitoParaSomar = -1;

            int procurarMultiplo = 0;
            while (procurarMultiplo == 0)
            {
                digitoParaSomar++;
                if ((total + digitoParaSomar) % 10 == 0)
                    procurarMultiplo = 1;
            }

            return digitoParaSomar;
        }

        public static long RandomNumeric(int maxNumeric)
        {
            long x = 0;

            Random random = new Random();
            for (int i = 0; i < maxNumeric; i++)
            {
                x += (long)(Math.Pow(10, i) * random.Next(1, 10));
            }

            return x;
        }

        public static string CodeBarrasRandom()
        {
            return "789" + RandomNumeric(9) + digitoVerificador(RandomNumeric(9).ToString());
        }

        public static void KillEmiplus()
        {
            Process[] processes = Process.GetProcessesByName("Emiplus");
            foreach (Process process in processes)
                process.Kill();
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}
