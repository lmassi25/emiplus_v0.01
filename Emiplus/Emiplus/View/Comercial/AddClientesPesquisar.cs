using Emiplus.Data.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class AddClientesPesquisar : Form
    {
        public AddClientesPesquisar()
        {
            InitializeComponent();
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
            }
        }

        private void Eventos()
        {
            KeyDown += KeyDowns;
            KeyPreview = true;
            Masks.SetToUpper(this);

            Load += (s, e) =>
            {
                Tipo.DataSource = new List<String> { "Cadastro Manual", "Cadastro automático por CNPJ" };
            };

            cpfCnpj.KeyPress += (s, e) =>
            {
                Masks.MaskCNPJ(s, e);
            };

            btnGerar.Click += (s, e) =>
            {
                
            };

            btnCancelar.Click += (s, e) => Close();
        }
    }
}
