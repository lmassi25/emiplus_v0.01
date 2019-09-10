using Emiplus.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Emiplus.View.Testes
{
    public partial class Form1 : Form
    {
        private int Backspace = 0;
        public Form1()
        {
            InitializeComponent();

            pessoaJF.DataSource = new List<String> { "Física", "Jurídica" };
            pessoaJF.SelectedItem = "Física";
        }

        private void CpfCnpj_TextChanged(object sender, EventArgs e)
        {
            ChangeMask();
        }

        private void CpfCnpj_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                Backspace = 1;
            }
            else
            {
                Backspace = 0;
            }
        }

        private void ChangeMask()
        {
            if (cpfCnpj.Text != "")
            {
                if (Backspace == 0)
                {
                    cpfCnpj.Text = Validation.ChangeMaskCPFCNPJ(cpfCnpj.Text, pessoaJF.Text);
                    cpfCnpj.Select(cpfCnpj.Text.Length, 0);
                }
            }
        }
    }
}
