﻿using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.View.Comercial;
using Emiplus.View.Produtos;
using Emiplus.View.Reports;

namespace Emiplus.View.Common
{
    public partial class TelaProdutosInicial : Form
    {
        public TelaProdutosInicial()
        {
            InitializeComponent();
            Eventos();
        }

        private void Eventos()
        {
            Produtos.Click += (s, e) =>
            {
                if (UserPermission.SetControl(Produtos, pictureBox9, "pdt_pdt"))
                    return;

                OpenForm.Show<Produtos.Produtos>(this);
            };

            Servicos.Click += (s, e) =>
            {
                //if (UserPermission.SetControl(Produtos, pictureBox9, "pdt_pdt"))
                //    return;

                OpenForm.Show<Servicos>(this);
            };

            Etiquetas.Click += (s, e) =>
            {
                if (UserPermission.SetControl(Etiquetas, pictureBox2, "pdt_etiquetas"))
                    return;

                OpenForm.Show<Etiquetas>(this);
            };

            Categorias.Click += (s, e) =>
            {
                if (UserPermission.SetControl(Categorias, pictureBox1, "pdt_cats"))
                    return;

                Home.CategoriaPage = "Produtos";
                OpenForm.Show<Categorias>(this);
            };

            Impostos.Click += (s, e) =>
            {
                if (UserPermission.SetControl(Impostos, pictureBox3, "pdt_impostos"))
                    return;

                OpenForm.Show<Impostos>(this);
            };

            fornecedores.Click += (s, e) =>
            {
                if (UserPermission.SetControl(fornecedores, pictureBox5, "pdt_fornecedores"))
                    return;

                Home.pessoaPage = "Fornecedores";
                OpenForm.Show<Clientes>(this);
            };

            transportadoras.Click += (s, e) =>
            {
                if (UserPermission.SetControl(transportadoras, pictureBox6, "pdt_transportadoras"))
                    return;

                Home.pessoaPage = "Transportadoras";
                OpenForm.Show<Clientes>(this);
            };

            ReajusteProduto.Click += (s, e) =>
            {
                if (UserPermission.SetControl(ReajusteProduto, pictureBox7, "pdt_reajuste"))
                    return;

                OpenForm.Show<ReajusteDeProduto>(this);
            };

            btnCombo.Click += (s, e) =>
            {
                OpenForm.Show<ComboProdutos>(this);
            };

            Compras.Click += (s, e) =>
            {
                if (UserPermission.SetControl(Compras, pictureBox8, "pdt_novacompra"))
                    return;

                Home.pedidoPage = "Compras";
                OpenForm.Show<Pedido>(this);
            };

            btnRemessas.Click += (s, e) =>
            {
                Home.pedidoPage = "Remessas";
                OpenForm.Show<Pedido>(this);
            };

            btnRemessa.Click += (s, e) =>
            {
                Home.pedidoPage = "Remessas";
                AddPedidos.Id = 0;
                var novoPedido = new AddPedidos();
                novoPedido.ShowDialog();
            };

            HistoricoEntradaSaida.Click += (s, e) =>
            {
                if (UserPermission.SetControl(HistoricoEntradaSaida, pictureBox11, "pdt_entradassaidas"))
                    return;

                OpenForm.Show<EstoqueEntradaSaida>(this);
            };

            Estoque.Click += (s, e) =>
            {
                if (UserPermission.SetControl(Estoque, pictureBox12, "pdt_inventario"))
                    return;

                OpenForm.Show<Inventario>(this);
            };

            CompraNova.Click += (s, e) =>
            {
                if (UserPermission.SetControl(Compras, pictureBox13, "pdt_compras"))
                    return;

                Home.pedidoPage = "Compras";
                AddPedidos.Id = 0;
                var novoPedido = new AddPedidos();
                novoPedido.ShowDialog();
            };

            importarNfe.Click += (s, e) =>
            {
                if (UserPermission.SetControl(importarNfe, pictureBox14, "pdt_importarnfe"))
                    return;

                var f = new ImportarNfe();
                f.ShowDialog();
            };

            btnAdicionais.Click += (s, e) => { OpenForm.Show<Adicional>(this); };

            btnVariation.Click += (s, e) => OpenForm.Show<Variacoes>(this);
        }
    }
}