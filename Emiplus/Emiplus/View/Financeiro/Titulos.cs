using Emiplus.Controller;
using Emiplus.Data.Helpers;
using Emiplus.View.Common;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Emiplus.View.Financeiro
{
    public partial class Titulos : Form
    {
        private Titulo _cTitulo = new Titulo();

        public Titulos()
        {
            InitializeComponent();
            Eventos();

            label1.Text = "Contas a " + Home.financeiroPage;
            label6.Text = "Contas a " + Home.financeiroPage;

            if (Home.financeiroPage == "Receber")
                label2.Text = "Confira aqui todas as contas a Receber da sua empresa.";
            else if (Home.financeiroPage == "Pagar")
                label2.Text = "Confira aqui todas as contas a Pagar da sua empresa.";

            data.DataSource = new List<String> { "Vencimento", "Emissão" };

            dataInicial.Text = Validation.DateNowToSql();
            dataFinal.Text = Validation.DateNowToSql();
        }

        private void Filter()
        {
            var tipo = data.Text == "Emissão" ? 1 : 0;
            _cTitulo.GetDataTableTitulosGeradosFilter(GridLista, Home.financeiroPage, search.Text, tipo, dataInicial.Text, dataFinal.Text);
        }

        private void EditTitulo(bool create = false)
        {
            if (create)
            {
                EditarTitulo.IdTitulo = 0;
                OpenForm.Show<EditarTitulo>(this);
                return;
            }

            if (GridLista.SelectedRows.Count > 0)
            {
                EditarTitulo.IdTitulo = Convert.ToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
                OpenForm.Show<EditarTitulo>(this);
            }
        }

        private void Eventos()
        {
            Load += (s, e) => Filter();
            search.TextChanged += (s, e) => Filter();
            filtrar.Click += (s, e) => Filter();

            btnAdicionar.Click += (s, e) => EditTitulo(true);
            btnEditar.Click += (s, e) => EditTitulo();

            btnExit.Click += (s, e) => Close();

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");
        }
    }
}
