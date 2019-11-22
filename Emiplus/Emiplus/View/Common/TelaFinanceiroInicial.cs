using Emiplus.Data.Helpers;
using Emiplus.Properties;
using Emiplus.View.Financeiro;
using SqlKata.Execution;
using System.Windows.Forms;

namespace Emiplus.View.Common
{
    public partial class TelaFinanceiroInicial : Form
    {
        public TelaFinanceiroInicial()
        {
            InitializeComponent();
            Eventos();
        }

        private void Eventos()
        {
            Clientes.Click += (s, e) =>
            {
                Home.pessoaPage = "Clientes";
                OpenForm.Show<Comercial.Clientes>(this);
            };

            Categorias.Click += (s, e) =>
            {
                Home.CategoriaPage = "Financeiro";
                OpenForm.Show<Produtos.Categorias>(this);
            };

            fornecedores.Click += (s, e) =>
            {
                Home.pessoaPage = "Fornecedores";
                OpenForm.Show<Comercial.Clientes>(this);
            };

            aReceber.Click += (s, e) =>
            {
                Home.financeiroPage = "Receber";
                OpenForm.Show<Titulos>(this);
            };

            aPagar.Click += (s, e) =>
            {
                Home.financeiroPage = "Pagar";
                OpenForm.Show<Titulos>(this);
            };

            novoRecebimento.Click += (s, e) =>
            {
                EditarTitulo.IdTitulo = 0;
                Home.financeiroPage = "Receber";
                OpenForm.Show<EditarTitulo>(this);
            };

            novoPagamento.Click += (s, e) =>
            {
                EditarTitulo.IdTitulo = 0;
                Home.financeiroPage = "Pagar";
                OpenForm.Show<EditarTitulo>(this);
            };
            
            AbrirCaixa.Click += (s, e) =>
            {
                if (Home.idCaixa == 0)
                {
                    AbrirCaixa f = new AbrirCaixa();
                    f.ShowDialog();
                }
                else
                {
                    Alert.Message("Oopps!", "Já existe um caixa aberto.", Alert.AlertType.warning);
                }
            };

            Caixa.Click += (s, e) =>
            {
                Caixa f = new Caixa();
                f.ShowDialog();
            };

            EntradaSaidaCaixa.Click += (s, e) =>
            {
                if (Home.idCaixa == 0)
                {
                    Alert.Message("Oopps!", "Você não possui um caixa aberto.", Alert.AlertType.warning);
                    return;
                }

                AddCaixaMov.idCaixa = Home.idCaixa;
                AddCaixaMov.idMov = 0;
                var f = new AddCaixaMov();
                f.ShowDialog();
            };

            FecharCaixa.Click += (s, e) =>
            {
                if (Home.idCaixa == 0)
                {
                    Alert.Message("Oopps!", "Você não possui um caixa aberto.", Alert.AlertType.warning);
                    return;
                }

                Financeiro.FecharCaixa.idCaixa = Home.idCaixa;
                FecharCaixa f = new FecharCaixa();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    Alert.Message("Pronto!", "Caixa fechado com sucesso.", Alert.AlertType.success);
                }
            };
        }
    }
}
