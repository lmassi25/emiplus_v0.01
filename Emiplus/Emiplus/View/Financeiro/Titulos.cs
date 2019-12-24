using DotLiquid;
using Emiplus.Controller;
using Emiplus.Data.Helpers;
using Emiplus.Properties;
using Emiplus.View.Common;
using Emiplus.View.Reports;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Financeiro
{
    public partial class Titulos : Form
    {
        private Titulo _cTitulo = new Titulo();
        private int tipo;

        public Titulos()
        {
            InitializeComponent();
            Eventos();

            if (Home.financeiroPage == "Receber")
            {
                label1.Text = "Recebimentos";
                label6.Text = "Recebimentos";
                label2.Text = "Confira aqui todas os títulos a Receber/Recebidos da sua empresa.";
                status.DataSource = new List<String> { "Todos", "Pendentes", "Recebidos"};
            }
            else if (Home.financeiroPage == "Pagar")
            {
                label1.Text = "Pagamentos";
                label6.Text = "Pagamentos";
                label2.Text = "Confira aqui todas os títulos a Pagar/Pagos da sua empresa.";
                status.DataSource = new List<String> { "Todos", "Pendentes", "Pagos" };
            }
                
            data.DataSource = new List<String> { "Vencimento", "Emissão" };

            dataInicial.Text = Validation.DateNowToSql();
            dataFinal.Text = Validation.DateNowToSql();
        }

        private void FilterTypes()
        {
            tipo = data.Text == "Emissão" ? 1 : 0;

            if (status.Text != "Todos")
                Controller.Titulo.status = status.Text;
            else
                Controller.Titulo.status = "";
        }

        private void Filter()
        {
            FilterTypes();

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

            Load += (s, e) => Filter();
            search.TextChanged += (s, e) => Filter();
            filtrar.Click += (s, e) => Filter();

            btnAdicionar.Click += (s, e) => EditTitulo(true);
            btnEditar.Click += (s, e) => EditTitulo();
            GridLista.DoubleClick += (s, e) => EditTitulo();
            btnExit.Click += (s, e) => Close();

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");

            imprimir.Click += async (s, e) => await RenderizarAsync();
        }
        
        private async Task RenderizarAsync()
        {
            FilterTypes();

            IEnumerable<dynamic> dados = _cTitulo.GetDataTableTitulosGerados(Home.financeiroPage, search.Text, tipo, dataInicial.Text, dataFinal.Text);

            string formatipo = "", clientetipo = "";

            if (Home.financeiroPage == "Receber")
            {
                formatipo = "Forma Receber";
                clientetipo = "Receber de";
            }
            else
            {
                formatipo = "Forma Pagar";
                clientetipo = "Pagar para";
            }

            ArrayList data = new ArrayList();
            foreach (var item in dados)
            {
                data.Add(new
                {
                    ID = item.ID,                    
                    FORMAPGTO = item.FORMAPGTO,
                    EMISSAO = Validation.ConvertDateToForm(item.EMISSAO),
                    VENCIMENTO = Validation.ConvertDateToForm(item.VENCIMENTO),
                    CLIENTE = item.NOME,
                    TOTAL = Validation.FormatPrice(Validation.ConvertToDouble(item.TOTAL)),
                    BAIXA_DATA = item.BAIXA_DATA != null ? Validation.ConvertDateToForm(item.BAIXA_DATA) : "",
                    RECEBIDO = Validation.FormatPrice(Validation.ConvertToDouble(item.RECEBIDO))
                });
            }

            var html = Template.Parse(File.ReadAllText($@"{Program.PATH_BASE}\html\Titulos.html"));
            var render = html.Render(Hash.FromAnonymousObject(new
            {
                INCLUDE_PATH = Program.PATH_BASE,
                URL_BASE = Program.PATH_BASE,
                Data = data,
                NomeFantasia = Settings.Default.empresa_nome_fantasia,
                Logo = Settings.Default.empresa_logo,
                Emissao = DateTime.Now.ToString("dd/MM/yyyy"),
                dataInicial = dataInicial.Text,
                dataFinal = dataFinal.Text,
                Titulo = label1.Text,
                Formatipo = formatipo,
                Clientetipo = clientetipo
            }));

            Browser.htmlRender = render;
            var f = new Browser();
            f.ShowDialog();
        }
    }
}
