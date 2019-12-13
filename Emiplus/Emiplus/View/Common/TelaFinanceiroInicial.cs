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
                if (UserPermission.SetControl(Clientes, pictureBox1, "fin_clientes"))
                    return;

                Home.pessoaPage = "Clientes";
                OpenForm.Show<Comercial.Clientes>(this);
            };

            Categorias.Click += (s, e) =>
            {
                if (UserPermission.SetControl(Categorias, pictureBox11, "fin_categorias"))
                    return;

                Home.CategoriaPage = "Financeiro";
                OpenForm.Show<Produtos.Categorias>(this);
            };

            fornecedores.Click += (s, e) =>
            {
                if (UserPermission.SetControl(fornecedores, pictureBox3, "fin_fornecedores"))
                    return;

                Home.pessoaPage = "Fornecedores";
                OpenForm.Show<Comercial.Clientes>(this);
            };

            aReceber.Click += (s, e) =>
            {
                if (UserPermission.SetControl(aReceber, pictureBox9, "fin_recebimentos"))
                    return;

                Home.financeiroPage = "Receber";
                OpenForm.Show<Titulos>(this);
            };

            aPagar.Click += (s, e) =>
            {
                if (UserPermission.SetControl(aPagar, pictureBox10, "fin_pagamentos"))
                    return;

                Home.financeiroPage = "Pagar";
                OpenForm.Show<Titulos>(this);
            };

            novoRecebimento.Click += (s, e) =>
            {
                if (UserPermission.SetControl(novoRecebimento, pictureBox4, "fin_novorecebimento"))
                    return;

                EditarTitulo.IdTitulo = 0;
                Home.financeiroPage = "Receber";
                OpenForm.Show<EditarTitulo>(this);
            };

            novoPagamento.Click += (s, e) =>
            {
                if (UserPermission.SetControl(novoPagamento, pictureBox5, "fin_novopag"))
                    return;

                EditarTitulo.IdTitulo = 0;
                Home.financeiroPage = "Pagar";
                OpenForm.Show<EditarTitulo>(this);
            };
            
            AbrirCaixa.Click += (s, e) =>
            {
                if (UserPermission.SetControl(AbrirCaixa, pictureBox6, "fin_abrircaixa"))
                    return;

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
                if (UserPermission.SetControl(Caixa, pictureBox12, "fin_caixa"))
                    return;

                Caixa f = new Caixa();
                f.ShowDialog();
            };

            EntradaSaidaCaixa.Click += (s, e) =>
            {
                if (UserPermission.SetControl(EntradaSaidaCaixa, pictureBox7, "fin_entradasaidacaixa"))
                    return;

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
                if (UserPermission.SetControl(FecharCaixa, pictureBox8, "fin_fecharcaixa"))
                    return;

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
