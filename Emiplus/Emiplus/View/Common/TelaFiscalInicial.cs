﻿using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.View.Comercial;
using Emiplus.View.Fiscal;
using Emiplus.View.Produtos;

namespace Emiplus.View.Common
{
    public partial class TelaFiscalInicial : Form
    {
        public TelaFiscalInicial()
        {
            InitializeComponent();
            Eventos();
        }

        private void Eventos()
        {
            novaNFE.Click += (s, e) =>
            {
                if (UserPermission.SetControl(novaNFE, pictureBox6, "fiscal_novanfe"))
                    return;

                Nota.disableCampos = false;
                Nota.Id = 0;
                var nota = new Nota();
                nota.ShowDialog();
            };

            nfe.Click += (s, e) =>
            {
                if (UserPermission.SetControl(nfe, pictureBox7, "fiscal_nfe"))
                    return;

                Home.pedidoPage = "Notas";
                OpenForm.Show<Pedido>(this);
            };

            naturezaOP.Click += (s, e) =>
            {
                if (UserPermission.SetControl(naturezaOP, pictureBox5, "fiscal_natop"))
                    return;

                OpenForm.Show<Natureza>(this);
            };

            clientes.Click += (s, e) =>
            {
                if (UserPermission.SetControl(clientes, pictureBox11, "fiscal_clientes"))
                    return;

                Home.pessoaPage = "Clientes";
                OpenForm.Show<Clientes>(this);
            };

            fornecedores.Click += (s, e) =>
            {
                if (UserPermission.SetControl(fornecedores, pictureBox2, "fiscal_fornecedores"))
                    return;

                Home.pessoaPage = "Fornecedores";
                OpenForm.Show<Clientes>(this);
            };

            transportadoras.Click += (s, e) =>
            {
                if (UserPermission.SetControl(transportadoras, pictureBox3, "fiscal_transportadoras"))
                    return;

                Home.pessoaPage = "Transportadoras";
                OpenForm.Show<Clientes>(this);
            };

            impostos.Click += (s, e) =>
            {
                if (UserPermission.SetControl(impostos, pictureBox4, "fiscal_impostos"))
                    return;

                OpenForm.Show<Impostos>(this);
            };

            InutilizarNFE.Click += (s, e) =>
            {
                if (UserPermission.SetControl(InutilizarNFE, pictureBox9, "fiscal_inutilizar"))
                    return;

                OpenForm.Show<InutilizarNotas>(this);
            };

            AlterarNFE.Click += (s, e) =>
            {
                if (UserPermission.SetControl(AlterarNFE, pictureBox10, "fiscal_alterar"))
                    return;

                AlertOptions.Message("", "Para alterar o últ. n° da NF-e acesse nossa área do Cliente online.",
                    AlertBig.AlertType.info, AlertBig.AlertBtn.OK);
            };

            EnviarXml.Click += (s, e) =>
            {
                if (UserPermission.SetControl(EnviarXml, pictureBox12, "fiscal_enviar"))
                    return;

                Alert.Message("Ação não permitida", "Função não disponível", Alert.AlertType.warning);
            };

            CFE.Click += (s, e) =>
            {
                if (UserPermission.SetControl(CFE, pictureBox8, "fiscal_cfe"))
                    return;

                Home.pedidoPage = "Cupons";
                OpenForm.Show<Pedido>(this);
            };
        }
    }
}