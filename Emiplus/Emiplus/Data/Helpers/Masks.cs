using System;
using System.Windows.Forms;
using Emiplus.Data.Helpers;

namespace Emiplus.Data.Helpers
{
    class Masks
    {
        public static void MaskHour(object sender, KeyPressEventArgs e)
        {
            // 10:00 - 9 caracateres
            TextBox t = sender as TextBox;
            t.MaxLength = 5;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            if (e.KeyChar >= 48 && e.KeyChar <= 57)
            {
                t.SelectionStart = t.Text.Length + 1;

                if (t.Text.Length == 2)
                    t.Text += ":";
                t.SelectionStart = t.Text.Length + 1;
            }
        }

        public static void MaskBirthday(object sender, KeyPressEventArgs e)
        {
            // 28/04/1997 - 10 caracateres
            TextBox t = sender as TextBox;
            t.MaxLength = 10;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            if (e.KeyChar >= 48 && e.KeyChar <= 57)
            {
                t.SelectionStart = t.Text.Length + 1;

                if (t.Text.Length == 2 || t.Text.Length == 5)
                    t.Text += "/";
                t.SelectionStart = t.Text.Length + 1;
            }
        }

        public static void MaskCPF(object sender, KeyPressEventArgs e)
        {
            // 410.405.698-70 - 14 caracateres
            TextBox t = sender as TextBox;
            t.MaxLength = 14;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            if (e.KeyChar >= 48 && e.KeyChar <= 57)
            {
                t.SelectionStart = t.Text.Length + 1;

                if (t.Text.Length == 3 || t.Text.Length == 7)
                    t.Text += ".";
                else if (t.Text.Length == 11)
                    t.Text += "-";
                t.SelectionStart = t.Text.Length + 1;
            }
        }
        
        public static void MaskCNPJ(object sender, KeyPressEventArgs e)
        {
            // 95.703.437/0001-06 - 18 caracateres
            TextBox t = sender as TextBox;
            t.MaxLength = 18;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            if (e.KeyChar >= 48 && e.KeyChar <= 57)
            {
                t.SelectionStart = t.Text.Length + 1;

                if (t.Text.Length == 2 || t.Text.Length == 6)
                    t.Text += ".";
                else if (t.Text.Length == 10)
                    t.Text += "/";
                else if (t.Text.Length == 15)
                    t.Text += "-";
                t.SelectionStart = t.Text.Length + 1;
            }
        }

        public static void MaskCEP(object sender, KeyPressEventArgs e)
        {
            // 15086-160 - 9 caracateres
            TextBox t = sender as TextBox;
            t.MaxLength = 9;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            if (e.KeyChar >= 48 && e.KeyChar <= 57)
            {
                t.SelectionStart = t.Text.Length + 1;

                if (t.Text.Length == 5)
                    t.Text += "-";
                t.SelectionStart = t.Text.Length + 1;
            }
        }

        public static void MaskPrice(ref TextBox txt)
        {
            string n = string.Empty;
            double v = 0;
            
            n = Validation.OnlyNumbers(txt.Text.Replace(",", "").Replace(".", ""));
            if (n.Equals(""))
            {
                n = "";
            }

            n = n.PadLeft(3, '0');
            if (n.Length > 3 && n.Substring(0, 1) == "0")
            {
                n = n.Substring(1, n.Length - 1);
            }

            v = Convert.ToDouble(n) / 100;
            txt.Text = string.Format("{0:N}", v);
            txt.SelectionStart = txt.Text.Length;
        }

        /// <summary>
        /// Permite apenas Letras, Numeros, max 255 caracteres
        /// </summary>
        public static void MaskOnlyNumberAndChar(object sender, KeyPressEventArgs e)
        {
            TextBox t = sender as TextBox;
            t.MaxLength = 255;

            if (!char.IsLetterOrDigit(e.KeyChar) && (e.KeyChar != 8) && (e.KeyChar != 32))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Permite apenas Letras, Numeros e Pontos, max 255 caracteres
        /// </summary>
        public static void MaskOnlyNumberAndCharAndMore(object sender, KeyPressEventArgs e)
        {
            TextBox t = sender as TextBox;
            t.MaxLength = 255;

            if (!char.IsLetterOrDigit(e.KeyChar) && (e.KeyChar != 8) && (e.KeyChar != 32) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Permite números e virgulas
        /// </summary>
        public static void MaskDouble(object sender, KeyPressEventArgs e)
        {
            TextBox t = sender as TextBox;
            t.MaxLength = 255;

            if (!char.IsNumber(e.KeyChar) && (e.KeyChar != 8) && (e.KeyChar != ','))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Permite apenas numeros, max 255 caracteres
        /// </summary>
        public static void MaskOnlyNumbers(object sender, KeyPressEventArgs e)
        {
            TextBox t = sender as TextBox;
            t.MaxLength = 255;

            if (!char.IsNumber(e.KeyChar) && (e.KeyChar != 8) && (e.KeyChar != 32))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Permite apenas letras, max 255 caracteres
        /// </summary>
        public static void MaskOnlyChars(object sender, KeyPressEventArgs e)
        {
            TextBox t = sender as TextBox;
            t.MaxLength = 255;

            if (!char.IsLetter(e.KeyChar) && (e.KeyChar != 8) && (e.KeyChar != 32))
            {
                e.Handled = true;
            }
        }

    }
}
