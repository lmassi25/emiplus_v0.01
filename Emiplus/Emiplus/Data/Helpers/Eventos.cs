using System;
using System.Windows.Forms;
using Emiplus.Data.Helpers;

namespace Emiplus.Data.Helpers
{
    class Eventos
    {
        public static void MaskBirthday(object sender, KeyPressEventArgs e)
        {
            // 28/04/1997 - 10 caracateres
            TextBox t = sender as TextBox;
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

            if (Validation.IsNumber(txt))
                return;

            n = txt.Text.Replace(",", "").Replace(".", "");
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
    }
}
