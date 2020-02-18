using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Emiplus.Data.Helpers
{
    public static class Validation
    {
        public static string AddSpaces(string valueF, string valueE)
        {
            if (string.IsNullOrEmpty(valueE))
                return "";

            if (string.IsNullOrEmpty(valueF))
                return "";

            if ((valueF + valueE).Length <= 48)
                return valueF + "".PadLeft(48 - (valueF.Length + valueE.Length)) + valueE;
            else
                return valueF + " " + valueE;
        }

        /// <summary>
        /// Arredondamento para 3 casas decimais
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double Round(double value)
        {
            return Math.Round(value, 3, MidpointRounding.AwayFromZero);
        }

        public static double RoundTwo(double value)
        {
            return Math.Round(value, 2, MidpointRounding.AwayFromZero);
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
            if (String.IsNullOrEmpty(dirtyString))
                return "";

            StringBuilder str = new StringBuilder(CleanString(dirtyString));

            if (str.ToString().Replace(" ", "").All(char.IsDigit))
            {
                str = str.Replace(" ", "");
            }

            return (Encoding.ASCII.GetString(Encoding.GetEncoding("Cyrillic").GetBytes(str.ToString())).ToString(Program.cultura).Trim()).ToUpper(Program.cultura);
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
        public static string FormatPrice(double value, bool cifrao = false)
        {
            string res;

            if (cifrao)
                res = value.ToString("C", Program.cultura);
            else
                res = string.Format(Program.cultura, "{0:N2}", value);

            return res;
        }

        public static string FormatDecimalPrice(decimal value, bool cifrao = false)
        {
            string res;

            if (cifrao)
                res = value.ToString("C", Program.cultura);
            else
                res = string.Format(Program.cultura, "{0:N2}", value);

            return res;
        }

        public static string Price(double price)
        {
            return string.Format(Program.cultura, "{0:N2}", price);
        }

        public static string FormatPriceWithDot(object value, int decimais = 2)
        {
            string aux = "";

            switch (decimais)
            {
                case 3:
                    aux = string.Format(Program.cultura, "{0:N3}", ConvertToDouble(value));
                    break;

                case 4:
                    aux = string.Format(Program.cultura, "{0:N4}", ConvertToDouble(value));
                    break;

                case 5:
                    aux = string.Format(Program.cultura, "{0:N5}", ConvertToDouble(value));
                    break;

                case 6:
                    aux = string.Format(Program.cultura, "{0:N6}", ConvertToDouble(value));
                    break;

                default:
                    aux = string.Format(Program.cultura, "{0:N2}", ConvertToDouble(value));
                    break;
            }

            aux = aux.Replace(".", "");
            aux = aux.Replace(",", ".");

            return aux;
        }

        public static double ConvertToDouble(object value)
        {
            if (value == null)
                return 0;

            if (string.IsNullOrEmpty(value.ToString()))
                return 0;

            return Convert.ToDouble(value.ToString().Replace("R$", "").Trim(), Program.cultura);
        }

        public static int ConvertToInt32(object value)
        {
            if (value == null)
                return 0;

            if (string.IsNullOrEmpty(value.ToString()))
                return 0;
            
            return Convert.ToInt32(value, Program.cultura);
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
                data = Convert.ToDateTime(date, Program.cultura).ToString("dd/MM/yyyy HH:mm", Program.cultura);
            else
                data = Convert.ToDateTime(date, Program.cultura).ToString("dd/MM/yyyy", Program.cultura);

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
                data = Convert.ToDateTime(date, Program.cultura).ToString("yyyy-MM-dd HH:mm", Program.cultura);
            else
                data = Convert.ToDateTime(date, Program.cultura).ToString("yyyy-MM-dd", Program.cultura);

            return data;
        }

        public static DateTime ConvertStringDateTime(object date)
        {
            if (date == null)
                return Convert.ToDateTime("01/01/0001");

            if (String.IsNullOrEmpty(date.ToString()))
                return Convert.ToDateTime("01/01/0001");

            if (date.ToString().IndexOf("/") > 0)
                try
                {
                    return Convert.ToDateTime(date);
                }
                catch (Exception)
                {
                    return Convert.ToDateTime("01/01/0001");
                }
            else
                return Convert.ToDateTime("01/01/0001");
        }

        public static string DateNowToSql()
        {
            return DateTime.Now.ToString("yyyy-MM-dd", Program.cultura);
        }

        /// <summary>
        /// Retorna o valor de acordo com a medida, Exemplo: UN retornará números inteiros (120), KG retornará número com 3 casas decimais
        /// </summary>
        public static string FormatMedidas(string Medida, double Valor)
        {
            switch (Medida)
            {
                case "UN":
                case "PC":
                case "MÇ":
                case "BD":
                case "DZ":
                case "CX":
                case "FD":
                case "PAR":
                case "PR":
                case "KIT":
                case "CNT":
                case "PCT":
                    string result = Valor.ToString(Program.cultura);

                    if (Valor.ToString(Program.cultura).Contains(","))
                        result = Valor.ToString(Program.cultura).Substring(0, Valor.ToString(Program.cultura).IndexOf(",", StringComparison.OrdinalIgnoreCase));

                    if (Valor.ToString(Program.cultura).Contains("."))
                        result = Valor.ToString(Program.cultura).Substring(0, Valor.ToString(Program.cultura).IndexOf(".", StringComparison.OrdinalIgnoreCase));

                    return result.ToString(Program.cultura);
                    break;

                case "KG":
                case "GR":
                case "L":
                case "ML":
                case "M2":
                    return Valor.ToString("0.000", Program.cultura);
                    break;
            }

            return "";
        }

        public static string FormatPriceXml(string value)
        {
            string p1 = "", p2 = ",00";

            if (!value.Contains("."))
                return value + ",00";

            p1 = value.Substring(0, value.IndexOf('.'));

            if (value.Substring(value.IndexOf('.'), (value.Length - value.IndexOf('.'))).Length >= 3)
                p2 = value.Substring(value.IndexOf('.'), 3).Replace(".", ",");

            return FormatPrice(ConvertToDouble(p1 + p2));
        }

        public static void WarningInput(TextBox textbox, PictureBox img)
        {
            if (String.IsNullOrEmpty(textbox.Text) || textbox.Text == "0,00")
            {
                img.Image = Properties.Resources.warning16x;
            }
            else
            {
                img.Image = Properties.Resources.success16x;
            }
        }

        public static string FirstCharToUpper(string input)
        {
            if (string.IsNullOrEmpty(input))
                return "";

            return input.First().ToString(Program.cultura).ToUpper(Program.cultura) + input.Substring(1);
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

        public enum BorderColor
        {
            Vermelho, Azul
        }

        public static int digitoVerificador(string codbarras)
        {
            int digitoParaSomar = 0;

            double pares = 0, impares = 0, total = 0, multiplicador = 1;

            int item = 0;

            if (!string.IsNullOrEmpty(codbarras))
            {
                if (codbarras.Length == 12)
                {
                    item = 0;
                    while (item < 12)
                    {
                        string auxNum = "";
                        auxNum = codbarras.Substring(item, 1);

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
            return "789" + RandomNumeric(9) + digitoVerificador(RandomNumeric(9).ToString(Program.cultura));
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

        public static bool validMail(string address)
        {
            EmailAddressAttribute e = new EmailAddressAttribute();
            if (e.IsValid(address))
                return true;

            return false;
        }

        private static Random rnd = new Random();
        public static int RandomSecurity()
        {
            DateTime foo = DateTime.UtcNow;
            long unixTime = ((DateTimeOffset)foo).ToUnixTimeSeconds();
            long random = rnd.Next(15, 999999);
            return (int)unixTime + (int)random;
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        private static readonly string PasswordHash = "P@@Sw0rd";
        private static readonly string SaltKey = "S@LT&KEY";
        private static readonly string VIKey = "@1B2c3D4e5F6g7H8";

        public static string Encrypt(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }

        public static string Decrypt(string encryptedText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }
    }
}