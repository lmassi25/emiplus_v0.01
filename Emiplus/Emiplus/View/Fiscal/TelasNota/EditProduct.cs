using Emiplus.Data.Helpers;
using SqlKata.Execution;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Fiscal.TelasNota
{
    public partial class EditProduct : Form
    {
        public static int idPdt { get; set; }
        public static int nrItem { get; set; }
        private Model.PedidoItem itemPedido;

        public EditProduct()
        {
            InitializeComponent();
            Eventos();
        }

        private void Impostos()
        {
            var icms = new ArrayList();
            icms.Add(new { Id = "0", Nome = "" });
            icms.Add(new { Id = "00", Nome = "00 - Tributação integralmente" });
            icms.Add(new { Id = "10", Nome = "10 - Tributação com cobrança do ICMS por S.T." });
            icms.Add(new { Id = "20", Nome = "20 - Tributação com redução de base de cálculo" });
            icms.Add(new { Id = "30", Nome = "30 - Tributação Isenta ou não tributada e com cobrança do ICMS por S.T." });
            icms.Add(new { Id = "40", Nome = "40 - Tributação Isenta" });
            icms.Add(new { Id = "41", Nome = "41 - Não Tributada" });
            icms.Add(new { Id = "50", Nome = "50 - Tributação Suspensa" });
            icms.Add(new { Id = "51", Nome = "51 - Tributação com Diferimento" });
            icms.Add(new { Id = "60", Nome = "60 - Tributação ICMS cobrado anteriormente por S.T." });
            icms.Add(new { Id = "70", Nome = "70 - Tributação ICMS com redução de base de cálculo e cobrança do ICMS por S.T." });
            icms.Add(new { Id = "90", Nome = "90 - Tributação ICMS: Outros" });

            icms.Add(new { Id = "101", Nome = "101 - Tributada pelo Simples Nacional com permissão de crédito" });
            icms.Add(new { Id = "102", Nome = "102 - Tributada pelo Simples Nacional sem permissão de crédito" });
            icms.Add(new { Id = "103", Nome = "103 - Isenção do ICMS no Simples Nacional para faixa de receita bruta" });
            icms.Add(new { Id = "201", Nome = "201 - Tributada pelo Simples Nacional com permissão de crédito e com cobrança do ICMS por S.T." });
            icms.Add(new { Id = "202", Nome = "202 - Tributada pelo Simples Nacional sem permissão de crédito e com cobrança do ICMS por S.T." });
            icms.Add(new { Id = "203", Nome = "203 - Isenção do ICMS nos Simples Nacional para faixa de receita bruta e com cobrança do ICMS por S.T." });
            icms.Add(new { Id = "300", Nome = "300 - Imune" });
            icms.Add(new { Id = "400", Nome = "400 - Não tributada pelo Simples Nacional" });
            icms.Add(new { Id = "500", Nome = "500 - ICMS cobrado anteriormente por S.T. (substituído) ou por antecipação" });
            icms.Add(new { Id = "900", Nome = "900 - Outros" });

            Icms.DataSource = icms;
            Icms.DisplayMember = "Nome";
            Icms.ValueMember = "Id";

            var ipi = new ArrayList();

            //IPITrib
            ipi.Add(new { Id = "0", Nome = "" });
            ipi.Add(new { Id = "00", Nome = "00 - Tributado: Entrada com recuperação de crédito" });
            ipi.Add(new { Id = "49", Nome = "49 - Tributado: Outras entradas" });
            ipi.Add(new { Id = "50", Nome = "50 - Tributado: Saída tributada" });
            ipi.Add(new { Id = "99", Nome = "99 - Tributado: Outras saídas" });

            //IPINT
            ipi.Add(new { Id = "01", Nome = "01 - Não Tributado: Entrada tributada com alíquota zero" });
            ipi.Add(new { Id = "02", Nome = "02 - Não Tributado: Entrada isenta" });
            ipi.Add(new { Id = "03", Nome = "03 - Não Tributado: Entrada não-tributada" });
            ipi.Add(new { Id = "04", Nome = "04 - Não Tributado: Entrada imune" });
            ipi.Add(new { Id = "05", Nome = "05 - Não Tributado: Entrada com suspensão" });
            ipi.Add(new { Id = "51", Nome = "51 - Não Tributado: Saída tributada com alíquota zero" });
            ipi.Add(new { Id = "52", Nome = "52 - Não Tributado: Saída isenta" });
            ipi.Add(new { Id = "53", Nome = "53 - Não Tributado: Saída não-tributada" });
            ipi.Add(new { Id = "54", Nome = "54 - Não Tributado: Saída imune" });
            ipi.Add(new { Id = "55", Nome = "55 - Não Tributado: Saída com suspensão" });

            Ipi.DataSource = ipi;
            Ipi.DisplayMember = "Nome";
            Ipi.ValueMember = "Id";

            var pis = new ArrayList();
            //PISAliq
            pis.Add(new { Id = "0", Nome = "" });
            pis.Add(new { Id = "01", Nome = "01 - Tributado pela Alíquota: Operação Tributável (base de cálculo = valor da operação alíquota normal (cumulativo/não cumulativo))" });
            pis.Add(new { Id = "02", Nome = "02 - Tributado pela Alíquota: Operação Tributável (base de cálculo = valor da operação (alíquota diferenciada))" });

            //PISQtde
            pis.Add(new { Id = "03", Nome = "03 - Tributado pela Quantidade: Operação Tributável (base de cálculo = quantidade vendida x alíquota por unidade de produto)" });

            //PISNT
            pis.Add(new { Id = "04", Nome = "04 - Não Tributado: Operação Tributável (tributação monofásica (alíquota zero))" });
            pis.Add(new { Id = "05", Nome = "05 - Não Tributado: Operação Tributável (Substituição Tributária)" });
            pis.Add(new { Id = "06", Nome = "06 - Não Tributado: Operação Tributável (alíquota zero)" });
            pis.Add(new { Id = "07", Nome = "07 - Não Tributado: Operação Isenta da Contribuição" });
            pis.Add(new { Id = "08", Nome = "08 - Não Tributado: Operação Sem Incidência da Contribuição" });
            pis.Add(new { Id = "09", Nome = "09 - Não Tributado: Operação com Suspensão da Contribuição" });

            //PISOutr
            pis.Add(new { Id = "49", Nome = "49 - Outras Operações: Outras Operações de Saída" });
            pis.Add(new { Id = "50", Nome = "50 - Outras Operações: Operação com Direito a Crédito - Vinculada Exclusivamente a Receita Tributada no Mercado Interno" });
            pis.Add(new { Id = "51", Nome = "51 - Outras Operações: Operação com Direito a Crédito - Vinculada Exclusivamente a Receita Não Tributada no Mercado Interno" });
            pis.Add(new { Id = "52", Nome = "52 - Outras Operações: Operação com Direito a Crédito – Vinculada Exclusivamente a Receita de Exportação" });
            pis.Add(new { Id = "53", Nome = "53 - Outras Operações: Operação com Direito a Crédito - Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno" });
            pis.Add(new { Id = "54", Nome = "54 - Outras Operações: Operação com Direito a Crédito -Vinculada a Receitas Tributadas no Mercado Interno e de Exportação" });
            pis.Add(new { Id = "55", Nome = "55 - Outras Operações: Operação com Direito a Crédito -Vinculada a Receitas NãoTributadas no Mercado Interno e de Exportação" });
            pis.Add(new { Id = "56", Nome = "56 - Outras Operações: Operação com Direito a Crédito -Vinculada a Receitas Tributadas e Não - Tributadas no Mercado Interno, e de Exportação" });
            pis.Add(new { Id = "60", Nome = "60 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita Tributada no Mercado Interno" });
            pis.Add(new { Id = "61", Nome = "61 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita Não - Tributada no Mercado Interno" });
            pis.Add(new { Id = "62", Nome = "62 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita de Exportação" });
            pis.Add(new { Id = "63", Nome = "63 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno" });
            pis.Add(new { Id = "64", Nome = "64 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas no Mercado Interno e de Exportação" });
            pis.Add(new { Id = "65", Nome = "65 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada a Receitas Não-Tributadas no Mercado Interno e de Exportação" });
            pis.Add(new { Id = "66", Nome = "66 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno, e de Exportação" });
            pis.Add(new { Id = "67", Nome = "67 - Outras Operações: Crédito Presumido - Outras Operações" });
            pis.Add(new { Id = "70", Nome = "70 - Outras Operações: Operação de Aquisição sem Direito a Crédito" });
            pis.Add(new { Id = "71", Nome = "71 - Outras Operações: Operação de Aquisição com Isenção" });
            pis.Add(new { Id = "72", Nome = "72 - Outras Operações: Operação de Aquisição com Suspensão" });
            pis.Add(new { Id = "73", Nome = "73 - Outras Operações: Operação de Aquisição a Alíquota Zero" });
            pis.Add(new { Id = "74", Nome = "74 - Outras Operações: Operação de Aquisição; sem Incidência da Contribuição" });
            pis.Add(new { Id = "75", Nome = "75 - Outras Operações: Operação de Aquisição por Substituição Tributária" });
            pis.Add(new { Id = "98", Nome = "98 - Outras Operações: Outras Operações de Entrada" });
            pis.Add(new { Id = "99", Nome = "99 - Outras Operações: Outras Operações" });

            Pis.DataSource = pis;
            Pis.DisplayMember = "Nome";
            Pis.ValueMember = "Id";

            var cofins = new ArrayList();

            //COFINSAliq
            cofins.Add(new { Id = "0", Nome = "" });
            cofins.Add(new { Id = "01", Nome = "01 - Tributado pela Alíquota: Operação Tributável (base de cálculo = valor da operação alíquota normal (cumulativo/não cumulativo))" });
            cofins.Add(new { Id = "02", Nome = "02 - Tributado pela Alíquota: Operação Tributável (base de cálculo = valor da operação (alíquota diferenciada))" });

            //COFINSQtde
            cofins.Add(new { Id = "03", Nome = "03 - Tributado pela Quantidade: Operação Tributável (base de cálculo = quantidade vendida x alíquota por unidade de produto)" });

            //COFINSNT
            cofins.Add(new { Id = "04", Nome = "04 - Não Tributado: Operação Tributável (tributação monofásica (alíquota zero))" });
            cofins.Add(new { Id = "05", Nome = "05 - Não Tributado: Operação Tributável (Substituição Tributária)" });
            cofins.Add(new { Id = "06", Nome = "06 - Não Tributado: Operação Tributável (alíquota zero)" });
            cofins.Add(new { Id = "07", Nome = "07 - Não Tributado: Operação Isenta da Contribuição" });
            cofins.Add(new { Id = "08", Nome = "08 - Não Tributado: Operação Sem Incidência da Contribuição" });
            cofins.Add(new { Id = "09", Nome = "09 - Não Tributado: Operação com Suspensão da Contribuição" });

            //COFINSOutr
            cofins.Add(new { Id = "49", Nome = "49 - Outras Operações: Outras Operações de Saída" });
            cofins.Add(new { Id = "50", Nome = "50 - Outras Operações: Operação com Direito a Crédito - Vinculada Exclusivamente a Receita Tributada no Mercado Interno" });
            cofins.Add(new { Id = "51", Nome = "51 - Outras Operações: Operação com Direito a Crédito - Vinculada Exclusivamente a Receita Não Tributada no Mercado Interno" });
            cofins.Add(new { Id = "52", Nome = "52 - Outras Operações: Operação com Direito a Crédito – Vinculada Exclusivamente a Receita de Exportação" });
            cofins.Add(new { Id = "53", Nome = "53 - Outras Operações: Operação com Direito a Crédito - Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno" });
            cofins.Add(new { Id = "54", Nome = "54 - Outras Operações: Operação com Direito a Crédito -Vinculada a Receitas Tributadas no Mercado Interno e de Exportação" });
            cofins.Add(new { Id = "55", Nome = "55 - Outras Operações: Operação com Direito a Crédito -Vinculada a Receitas NãoTributadas no Mercado Interno e de Exportação" });
            cofins.Add(new { Id = "56", Nome = "56 - Outras Operações: Operação com Direito a Crédito -Vinculada a Receitas Tributadas e Não - Tributadas no Mercado Interno, e de Exportação" });
            cofins.Add(new { Id = "60", Nome = "60 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita Tributada no Mercado Interno" });
            cofins.Add(new { Id = "61", Nome = "61 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita Não - Tributada no Mercado Interno" });
            cofins.Add(new { Id = "62", Nome = "62 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita de Exportação" });
            cofins.Add(new { Id = "63", Nome = "63 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno" });
            cofins.Add(new { Id = "64", Nome = "64 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas no Mercado Interno e de Exportação" });
            cofins.Add(new { Id = "65", Nome = "65 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada a Receitas Não-Tributadas no Mercado Interno e de Exportação" });
            cofins.Add(new { Id = "66", Nome = "66 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno, e de Exportação" });
            cofins.Add(new { Id = "67", Nome = "67 - Outras Operações: Crédito Presumido - Outras Operações" });
            cofins.Add(new { Id = "70", Nome = "70 - Outras Operações: Operação de Aquisição sem Direito a Crédito" });
            cofins.Add(new { Id = "71", Nome = "71 - Outras Operações: Operação de Aquisição com Isenção" });
            cofins.Add(new { Id = "72", Nome = "72 - Outras Operações: Operação de Aquisição com Suspensão" });
            cofins.Add(new { Id = "73", Nome = "73 - Outras Operações: Operação de Aquisição a Alíquota Zero" });
            cofins.Add(new { Id = "74", Nome = "74 - Outras Operações: Operação de Aquisição; sem Incidência da Contribuição" });
            cofins.Add(new { Id = "75", Nome = "75 - Outras Operações: Operação de Aquisição por Substituição Tributária" });
            cofins.Add(new { Id = "98", Nome = "98 - Outras Operações: Outras Operações de Entrada" });
            cofins.Add(new { Id = "99", Nome = "99 - Outras Operações: Outras Operações" });

            Cofins.DataSource = cofins;
            Cofins.DisplayMember = "Nome";
            Cofins.ValueMember = "Id";
        }

        private void LoadDados()
        {
            itemPedido = new Model.PedidoItem().Query().Where("pedido_item.id", idPdt).FirstOrDefault<Model.PedidoItem>();
            if (itemPedido != null)
            {
                item.Text = nrItem.ToString();
                referencia.Text = itemPedido?.CProd ?? "";
                codebarras.Text = itemPedido?.CEan ?? "";
                descricao.Text = itemPedido?.xProd ?? "";
                origem.Text = itemPedido?.Origem ?? "";
                ncm.Text = itemPedido?.Ncm ?? "";
                cest.Text = itemPedido?.Cest ?? "";
                cfop.Text = itemPedido?.Cfop ?? "";
                quantidade.Text = Validation.FormatMedidas(itemPedido.Medida, Validation.ConvertToDouble(itemPedido.Quantidade));

                valorUnitario.Text = Validation.FormatPrice(itemPedido.ValorVenda);
                valorDesconto.Text = Validation.FormatPrice(itemPedido.DescontoItem);
                valorFrete.Text = Validation.FormatPrice(itemPedido.Frete);
                valorTotal.Text = Validation.FormatPrice(itemPedido.Total);

                Icms.SelectedValue = itemPedido.Icms != null ? itemPedido.Icms : "";
                icmsaliq.Text = Validation.FormatPrice(itemPedido.IcmsAliq);
                icmsBase.Text = Validation.Price(Validation.ConvertToDouble(itemPedido.IcmsBase));
                icmsvlr.Text = Validation.FormatPrice(itemPedido.IcmsVlr);

                Pis.SelectedValue = itemPedido.Pis != null ? itemPedido.Pis : "";
                Cofins.SelectedValue = itemPedido.Cofins != null ? itemPedido.Cofins : "";
                Ipi.SelectedValue = itemPedido.Ipi != null ? itemPedido.Ipi : "";

                icmsstaliq.Text = Validation.Price(itemPedido.IcmsStAliq);
                icmsstbasecomreducao.Text = Validation.Price(itemPedido.IcmsStBaseComReducao);
                icmsstbase.Text = Validation.Price(itemPedido.IcmsStBase);
                icmsstvlr.Text = Validation.Price(itemPedido.Icmsstvlr);

                ipialiq.Text = Validation.Price(itemPedido.IpiAliq);
                pisaliq.Text = Validation.Price(itemPedido.PisAliq);
                cofinsaliq.Text = Validation.Price(itemPedido.CofinsAliq);

                pisvlr.Text = Validation.Price(itemPedido.PisVlr);
                cofinsvlr.Text = Validation.Price(itemPedido.CofinsVlr);
                ipivlr.Text = Validation.Price(itemPedido.IpiVlr);

                federal.Text = Validation.Price(itemPedido.Federal);
                estadual.Text = Validation.Price(itemPedido.Estadual);
                municipal.Text = Validation.Price(itemPedido.Municipal);

                infoAdicional.Text = itemPedido.Info_Adicional != null ? itemPedido.Info_Adicional : "";
                pedidoCompra.Text = itemPedido.Pedido_compra != null ? itemPedido.Pedido_compra : "";
                itemPedidoCompra.Text = itemPedido.Item_Pedido_Compra != null ? itemPedido.Item_Pedido_Compra : "";
            }

            medida.DataSource = Support.GetUnidades();

            origem.DataSource = Support.GetOrigens();
            origem.DisplayMember = "Nome";
            origem.ValueMember = "Id";

            if (itemPedido.Medida != null)
                medida.SelectedItem = itemPedido.Medida;

            if (itemPedido.Origem != null)
                origem.SelectedValue = itemPedido.Origem;
        }

        private void Save()
        {
            itemPedido.Id = idPdt;
            itemPedido.CProd = referencia.Text;
            itemPedido.CEan = codebarras.Text;
            itemPedido.xProd = descricao.Text;
            itemPedido.Origem = origem.Text;
            itemPedido.Ncm = ncm.Text;
            itemPedido.Cest = cest.Text;
            itemPedido.Cfop = cfop.Text;
            itemPedido.Quantidade = Validation.ConvertToDouble(quantidade.Text);

            itemPedido.ValorCompra = Validation.ConvertToDouble(valorUnitario.Text);
            itemPedido.DescontoItem = Validation.ConvertToDouble(valorDesconto.Text);
            itemPedido.Frete = Validation.ConvertToDouble(valorFrete.Text);
            itemPedido.Total = Validation.ConvertToDouble(valorTotal.Text);

            itemPedido.Icms = Icms.SelectedValue != null ? Icms.SelectedValue.ToString() : "";
            itemPedido.IcmsBase = Validation.ConvertToDouble(icmsBase.Text);
            itemPedido.IcmsAliq = Validation.ConvertToDouble(icmsaliq.Text);
            itemPedido.IcmsVlr = Validation.ConvertToDouble(icmsvlr.Text);

            itemPedido.IcmsStAliq = Validation.ConvertToDouble(icmsstaliq.Text);
            itemPedido.IcmsStBaseComReducao = Validation.ConvertToDouble(icmsstbasecomreducao.Text);
            itemPedido.IcmsStBase = Validation.ConvertToDouble(icmsstbase.Text);
            itemPedido.Icmsstvlr = Validation.ConvertToDouble(icmsstvlr.Text);

            itemPedido.Pis = Pis.SelectedValue != null ? Pis.SelectedValue.ToString() : "";
            itemPedido.PisAliq = Validation.ConvertToDouble(pisaliq.Text);
            itemPedido.PisVlr = Validation.ConvertToDouble(pisvlr.Text);

            itemPedido.Cofins = Cofins.SelectedValue != null ? Cofins.SelectedValue.ToString() : "";
            itemPedido.CofinsAliq = Validation.ConvertToDouble(cofinsaliq.Text);
            itemPedido.CofinsVlr = Validation.ConvertToDouble(cofinsvlr.Text);

            itemPedido.Ipi = Ipi.SelectedValue != null ? Ipi.SelectedValue.ToString() : "";
            itemPedido.IpiAliq = Validation.ConvertToDouble(ipialiq.Text);
            itemPedido.IpiVlr = Validation.ConvertToDouble(ipivlr.Text);

            itemPedido.Federal = Validation.ConvertToDouble(federal.Text);
            itemPedido.Estadual = Validation.ConvertToDouble(estadual.Text);
            itemPedido.Municipal = Validation.ConvertToDouble(municipal.Text);

            itemPedido.Info_Adicional = infoAdicional.Text;
            itemPedido.Pedido_compra = pedidoCompra.Text;
            itemPedido.Item_Pedido_Compra = itemPedidoCompra.Text;

            if (itemPedido.Save(itemPedido))
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void Eventos()
        {
            Shown += (s, e) =>
            {
                Impostos();
                LoadDados();
            };

            btnCancelar.Click += (s, e) => Close();

            btnSalvar.Click += (s, e) => {
                Save();
            };

            btnExcluir.Click += (s, e) => {
                itemPedido.Id = idPdt;
                itemPedido.Excluir = 1;
                if (itemPedido.Save(itemPedido))
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            };

            valorUnitario.TextChanged += new EventHandler(Masks.MaskPriceEvent);
            valorDesconto.TextChanged += new EventHandler(Masks.MaskPriceEvent);
            valorFrete.TextChanged += new EventHandler(Masks.MaskPriceEvent);
            valorTotal.TextChanged += new EventHandler(Masks.MaskPriceEvent);

            icmsaliq.TextChanged += new EventHandler(Masks.MaskPriceEvent);
            icmsBase.TextChanged += new EventHandler(Masks.MaskPriceEvent);
            icmsvlr.TextChanged += new EventHandler(Masks.MaskPriceEvent);

            icmsstaliq.TextChanged += new EventHandler(Masks.MaskPriceEvent);
            icmsstbasecomreducao.TextChanged += new EventHandler(Masks.MaskPriceEvent);
            icmsstbase.TextChanged += new EventHandler(Masks.MaskPriceEvent);
            icmsstvlr.TextChanged += new EventHandler(Masks.MaskPriceEvent);
            pisaliq.TextChanged += new EventHandler(Masks.MaskPriceEvent);
            pisvlr.TextChanged += new EventHandler(Masks.MaskPriceEvent);

            cofinsaliq.TextChanged += new EventHandler(Masks.MaskPriceEvent);
            cofinsvlr.TextChanged += new EventHandler(Masks.MaskPriceEvent);
            ipialiq.TextChanged += new EventHandler(Masks.MaskPriceEvent);
            ipivlr.TextChanged += new EventHandler(Masks.MaskPriceEvent);

            federal.TextChanged += new EventHandler(Masks.MaskPriceEvent);
            estadual.TextChanged += new EventHandler(Masks.MaskPriceEvent);
            municipal.TextChanged += new EventHandler(Masks.MaskPriceEvent);
        }
    }
}
