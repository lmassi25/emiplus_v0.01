using Emiplus.Controller;
using Emiplus.Data.Helpers;
using Emiplus.View.Comercial;
using Emiplus.View.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Financeiro
{
    public partial class Titulos : Form
    {
        public static int IdTitulo { get; set; }

        private Titulo _cTitulo = new Titulo();

        public Titulos()
        {
            InitializeComponent();

            label1.Text = "Contas a " + Home.financeiroPage;
            label6.Text = "Contas a " + Home.financeiroPage;

            if (Home.financeiroPage == "Receber")
                label2.Text = "Confira aqui todas as contas a Receber da sua empresa.";
            else if (Home.financeiroPage == "Pagar")
                label2.Text = "Confira aqui todas as contas a Pagar da sua empresa.";

            Events();
        }

        public void LoadData() => _cTitulo.GetDataTableTitulosGerados(GridLista, search.Text);

        /// <summary>
        /// Eventos da tela
        /// </summary>
        private void Events()
        {
            this.Load += (s, e) => LoadData();

            btnExit.Click += (s, e) => { Close(); };

            btnEditar.Click += (s, e) =>
            {

            };

            btnAdicionar.Click += (s, e) =>
            {
                IdTitulo = 0;
                OpenForm.Show<AddPedidos>(this);
            };

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");

            search.TextChanged += (s, e) => LoadData();
        }
    }
}
