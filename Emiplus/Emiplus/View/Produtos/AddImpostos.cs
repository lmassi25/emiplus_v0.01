using Emiplus.Data.Helpers;
using Emiplus.View.Produtos.Imposto.CFOP;
using SqlKata.Execution;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Emiplus.View.Produtos
{
    public partial class AddImpostos : Form
    {
        private int idImpSelected = Impostos.idImpSelected;
        private Model.Imposto _modelImposto = new Model.Imposto();

        public AddImpostos()
        {
            InitializeComponent();
            Eventos();
        }

        private void LoadData()
        {
            _modelImposto = _modelImposto.FindById(idImpSelected).First<Model.Imposto>();

            nome.Text = _modelImposto.Nome;
            cfop.Text = _modelImposto.Cfop;

            GetImpostos(1);
        }

        private class ImpostoTipo
        {
            public string Id { get; set; }
            public string Nome { get; set; }

            public ImpostoTipo(string Id, string Nome)
            {
                this.Id = Id;
                this.Nome = Nome;
            }
        }

        private void Carregar()
        {
            nome.Select();

            ToolHelp.Show("Título identificador da categoria.", pictureBox6, ToolHelp.ToolTipIcon.Info, "Ajuda!");

            #region COMBOBOX

            var icms = new ArrayList();

            icms.Add(new ImpostoTipo("0", ""));
            icms.Add(new ImpostoTipo("00", "00 - Tributação integralmente"));
            icms.Add(new ImpostoTipo("10", "10 - Tributação com cobrança do ICMS por S.T."));
            icms.Add(new ImpostoTipo("20", "20 - Tributação com redução de base de cálculo"));
            icms.Add(new ImpostoTipo("30", "30 - Tributação Isenta ou não tributada e com cobrança do ICMS por S.T."));
            icms.Add(new ImpostoTipo("40", "40 - Tributação Isenta"));
            icms.Add(new ImpostoTipo("41", "41 - Não Tributada"));
            icms.Add(new ImpostoTipo("50", "50 - Tributação Suspensa"));
            icms.Add(new ImpostoTipo("51", "51 - Tributação com Diferimento"));
            icms.Add(new ImpostoTipo("60", "60 - Tributação ICMS cobrado anteriormente por S.T."));
            icms.Add(new ImpostoTipo("70", "70 - Tributação ICMS com redução de base de cálculo e cobrança do ICMS por S.T."));
            icms.Add(new ImpostoTipo("90", "90 - Tributação ICMS: Outros"));

            icms.Add(new ImpostoTipo("101", "101 - Tributada pelo Simples Nacional com permissão de crédito"));
            icms.Add(new ImpostoTipo("102", "102 - Tributada pelo Simples Nacional sem permissão de crédito"));
            icms.Add(new ImpostoTipo("103", "103 - Isenção do ICMS no Simples Nacional para faixa de receita bruta"));
            icms.Add(new ImpostoTipo("201", "201 - Tributada pelo Simples Nacional com permissão de crédito e com cobrança do ICMS por S.T."));
            icms.Add(new ImpostoTipo("202", "202 - Tributada pelo Simples Nacional sem permissão de crédito e com cobrança do ICMS por S.T."));
            icms.Add(new ImpostoTipo("203", "203 - Isenção do ICMS nos Simples Nacional para faixa de receita bruta e com cobrança do ICMS por S.T."));
            icms.Add(new ImpostoTipo("300", "300 - Imune"));
            icms.Add(new ImpostoTipo("400", "400 - Não tributada pelo Simples Nacional"));
            icms.Add(new ImpostoTipo("500", "500 - ICMS cobrado anteriormente por S.T. (substituído) ou por antecipação"));
            icms.Add(new ImpostoTipo("900", "900 - Outros"));

            Icms.DataSource = icms;
            Icms.DisplayMember = "Nome";
            Icms.ValueMember = "Id";
            
            var ipi = new ArrayList();

            //IPITrib

            ipi.Add(new ImpostoTipo("0", ""));
            ipi.Add(new ImpostoTipo("00", "00 - Tributado: Entrada com recuperação de crédito"));
            ipi.Add(new ImpostoTipo("49", "49 - Tributado: Outras entradas"));
            ipi.Add(new ImpostoTipo("50", "50 - Tributado: Saída tributada"));
            ipi.Add(new ImpostoTipo("99", "99 - Tributado: Outras saídas"));

            //IPINT

            ipi.Add(new ImpostoTipo("01", "01 - Não Tributado: Entrada tributada com alíquota zero"));
            ipi.Add(new ImpostoTipo("02", "02 - Não Tributado: Entrada isenta"));
            ipi.Add(new ImpostoTipo("03", "03 - Não Tributado: Entrada não-tributada"));
            ipi.Add(new ImpostoTipo("04", "04 - Não Tributado: Entrada imune"));
            ipi.Add(new ImpostoTipo("05", "05 - Não Tributado: Entrada com suspensão"));
            ipi.Add(new ImpostoTipo("51", "51 - Não Tributado: Saída tributada com alíquota zero"));
            ipi.Add(new ImpostoTipo("52", "52 - Não Tributado: Saída isenta"));
            ipi.Add(new ImpostoTipo("53", "53 - Não Tributado: Saída não-tributada"));
            ipi.Add(new ImpostoTipo("54", "54 - Não Tributado: Saída imune"));
            ipi.Add(new ImpostoTipo("55", "55 - Não Tributado: Saída com suspensão"));

            Ipi.DataSource = ipi;
            Ipi.DisplayMember = "Nome";
            Ipi.ValueMember = "Id";

            var pis = new ArrayList();

            //PISAliq

            pis.Add(new ImpostoTipo("0", ""));
            pis.Add(new ImpostoTipo("01", "01 - Tributado pela Alíquota: Operação Tributável (base de cálculo = valor da operação alíquota normal (cumulativo/não cumulativo))"));
            pis.Add(new ImpostoTipo("02", "02 - Tributado pela Alíquota: Operação Tributável (base de cálculo = valor da operação (alíquota diferenciada))"));

            //PISQtde

            pis.Add(new ImpostoTipo("03", "03 - Tributado pela Quantidade: Operação Tributável (base de cálculo = quantidade vendida x alíquota por unidade de produto)"));

            //PISNT

            pis.Add(new ImpostoTipo("04", "04 - Não Tributado: Operação Tributável (tributação monofásica (alíquota zero))"));
            pis.Add(new ImpostoTipo("05", "05 - Não Tributado: Operação Tributável (Substituição Tributária)"));
            pis.Add(new ImpostoTipo("06", "06 - Não Tributado: Operação Tributável (alíquota zero)"));
            pis.Add(new ImpostoTipo("07", "07 - Não Tributado: Operação Isenta da Contribuição"));
            pis.Add(new ImpostoTipo("08", "08 - Não Tributado: Operação Sem Incidência da Contribuição"));
            pis.Add(new ImpostoTipo("09", "09 - Não Tributado: Operação com Suspensão da Contribuição"));

            //PISOutr

            pis.Add(new ImpostoTipo("49", "49 - Outras Operações: Outras Operações de Saída"));
            pis.Add(new ImpostoTipo("50", "50 - Outras Operações: Operação com Direito a Crédito - Vinculada Exclusivamente a Receita Tributada no Mercado Interno"));
            pis.Add(new ImpostoTipo("51", "51 - Outras Operações: Operação com Direito a Crédito - Vinculada Exclusivamente a Receita Não Tributada no Mercado Interno"));
            pis.Add(new ImpostoTipo("52", "52 - Outras Operações: Operação com Direito a Crédito – Vinculada Exclusivamente a Receita de Exportação"));
            pis.Add(new ImpostoTipo("53", "53 - Outras Operações: Operação com Direito a Crédito - Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno"));
            pis.Add(new ImpostoTipo("54", "54 - Outras Operações: Operação com Direito a Crédito -Vinculada a Receitas Tributadas no Mercado Interno e de Exportação"));
            pis.Add(new ImpostoTipo("55", "55 - Outras Operações: Operação com Direito a Crédito -Vinculada a Receitas NãoTributadas no Mercado Interno e de Exportação"));
            pis.Add(new ImpostoTipo("56", "56 - Outras Operações: Operação com Direito a Crédito -Vinculada a Receitas Tributadas e Não - Tributadas no Mercado Interno, e de Exportação"));
            pis.Add(new ImpostoTipo("60", "60 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita Tributada no Mercado Interno"));
            pis.Add(new ImpostoTipo("61", "61 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita Não - Tributada no Mercado Interno"));
            pis.Add(new ImpostoTipo("62", "62 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita de Exportação"));
            pis.Add(new ImpostoTipo("63", "63 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno"));
            pis.Add(new ImpostoTipo("64", "64 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas no Mercado Interno e de Exportação"));
            pis.Add(new ImpostoTipo("65", "65 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada a Receitas Não-Tributadas no Mercado Interno e de Exportação"));
            pis.Add(new ImpostoTipo("66", "66 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno, e de Exportação"));
            pis.Add(new ImpostoTipo("67", "67 - Outras Operações: Crédito Presumido - Outras Operações"));
            pis.Add(new ImpostoTipo("70", "70 - Outras Operações: Operação de Aquisição sem Direito a Crédito"));
            pis.Add(new ImpostoTipo("71", "71 - Outras Operações: Operação de Aquisição com Isenção"));
            pis.Add(new ImpostoTipo("72", "72 - Outras Operações: Operação de Aquisição com Suspensão"));
            pis.Add(new ImpostoTipo("73", "73 - Outras Operações: Operação de Aquisição a Alíquota Zero"));
            pis.Add(new ImpostoTipo("74", "74 - Outras Operações: Operação de Aquisição; sem Incidência da Contribuição"));
            pis.Add(new ImpostoTipo("75", "75 - Outras Operações: Operação de Aquisição por Substituição Tributária"));
            pis.Add(new ImpostoTipo("98", "98 - Outras Operações: Outras Operações de Entrada"));
            pis.Add(new ImpostoTipo("99", "99 - Outras Operações: Outras Operações"));

            Pis.DataSource = pis;
            Pis.DisplayMember = "Nome";
            Pis.ValueMember = "Id";
            
            var cofins = new ArrayList();

            //COFINSAliq

            cofins.Add(new ImpostoTipo("0", ""));
            cofins.Add(new ImpostoTipo("01", "01 - Tributado pela Alíquota: Operação Tributável (base de cálculo = valor da operação alíquota normal (cumulativo/não cumulativo))"));
            cofins.Add(new ImpostoTipo("02", "02 - Tributado pela Alíquota: Operação Tributável (base de cálculo = valor da operação (alíquota diferenciada))"));

            //COFINSQtde

            cofins.Add(new ImpostoTipo("03", "03 - Tributado pela Quantidade: Operação Tributável (base de cálculo = quantidade vendida x alíquota por unidade de produto)"));

            //COFINSNT

            cofins.Add(new ImpostoTipo("04", "04 - Não Tributado: Operação Tributável (tributação monofásica (alíquota zero))"));
            cofins.Add(new ImpostoTipo("05", "05 - Não Tributado: Operação Tributável (Substituição Tributária)"));
            cofins.Add(new ImpostoTipo("06", "06 - Não Tributado: Operação Tributável (alíquota zero)"));
            cofins.Add(new ImpostoTipo("07", "07 - Não Tributado: Operação Isenta da Contribuição"));
            cofins.Add(new ImpostoTipo("08", "08 - Não Tributado: Operação Sem Incidência da Contribuição"));
            cofins.Add(new ImpostoTipo("09", "09 - Não Tributado: Operação com Suspensão da Contribuição"));

            //COFINSOutr

            cofins.Add(new ImpostoTipo("49", "49 - Outras Operações: Outras Operações de Saída"));
            cofins.Add(new ImpostoTipo("50", "50 - Outras Operações: Operação com Direito a Crédito - Vinculada Exclusivamente a Receita Tributada no Mercado Interno"));
            cofins.Add(new ImpostoTipo("51", "51 - Outras Operações: Operação com Direito a Crédito - Vinculada Exclusivamente a Receita Não Tributada no Mercado Interno"));
            cofins.Add(new ImpostoTipo("52", "52 - Outras Operações: Operação com Direito a Crédito – Vinculada Exclusivamente a Receita de Exportação"));
            cofins.Add(new ImpostoTipo("53", "53 - Outras Operações: Operação com Direito a Crédito - Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno"));
            cofins.Add(new ImpostoTipo("54", "54 - Outras Operações: Operação com Direito a Crédito -Vinculada a Receitas Tributadas no Mercado Interno e de Exportação"));
            cofins.Add(new ImpostoTipo("55", "55 - Outras Operações: Operação com Direito a Crédito -Vinculada a Receitas NãoTributadas no Mercado Interno e de Exportação"));
            cofins.Add(new ImpostoTipo("56", "56 - Outras Operações: Operação com Direito a Crédito -Vinculada a Receitas Tributadas e Não - Tributadas no Mercado Interno, e de Exportação"));
            cofins.Add(new ImpostoTipo("60", "60 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita Tributada no Mercado Interno"));
            cofins.Add(new ImpostoTipo("61", "61 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita Não - Tributada no Mercado Interno"));
            cofins.Add(new ImpostoTipo("62", "62 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita de Exportação"));
            cofins.Add(new ImpostoTipo("63", "63 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno"));
            cofins.Add(new ImpostoTipo("64", "64 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas no Mercado Interno e de Exportação"));
            cofins.Add(new ImpostoTipo("65", "65 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada a Receitas Não-Tributadas no Mercado Interno e de Exportação"));
            cofins.Add(new ImpostoTipo("66", "66 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno, e de Exportação"));
            cofins.Add(new ImpostoTipo("67", "67 - Outras Operações: Crédito Presumido - Outras Operações"));
            cofins.Add(new ImpostoTipo("70", "70 - Outras Operações: Operação de Aquisição sem Direito a Crédito"));
            cofins.Add(new ImpostoTipo("71", "71 - Outras Operações: Operação de Aquisição com Isenção"));
            cofins.Add(new ImpostoTipo("72", "72 - Outras Operações: Operação de Aquisição com Suspensão"));
            cofins.Add(new ImpostoTipo("73", "73 - Outras Operações: Operação de Aquisição a Alíquota Zero"));
            cofins.Add(new ImpostoTipo("74", "74 - Outras Operações: Operação de Aquisição; sem Incidência da Contribuição"));
            cofins.Add(new ImpostoTipo("75", "75 - Outras Operações: Operação de Aquisição por Substituição Tributária"));
            cofins.Add(new ImpostoTipo("98", "98 - Outras Operações: Outras Operações de Entrada"));
            cofins.Add(new ImpostoTipo("99", "99 - Outras Operações: Outras Operações"));

            Cofins.DataSource = cofins;
            Cofins.DisplayMember = "Nome";
            Cofins.ValueMember = "Id";

            icms_1.Visible = false;
            icms_2.Visible = false;
            icms_3.Visible = false;

            icms_1.Location = new Point(9, 62);
            icms_2.Location = new Point(9, 62);
            icms_3.Location = new Point(9, 62);

            icms_1.Size = new Size(196, 67);
            icms_2.Size = new Size(474, 162);
            icms_3.Size = new Size(214, 117);

            #endregion

            if (idImpSelected > 0)
                LoadData();
        }
        
        private void GetImpostos_ICMS1(int tipo)
        {
            if(tipo == 0)
                _modelImposto.IcmsAliq = Validation.ConvertToDouble(icms_1.Aliq_Value);
            else
                icms_1.Aliq_Value = Validation.Price(_modelImposto.IcmsAliq);
        }

        private void GetImpostos_ICMS2(int tipo)
        {
            if (tipo == 0)
            {
                //_modelImposto. = Validation.ConvertToDouble(icms_2.Aliq_Value);
                _modelImposto.IcmsReducaoAliq = Validation.ConvertToDouble(icms_2.RedBase_Value);
                _modelImposto.IcmsIva = Validation.ConvertToDouble(icms_2.IVA_Value);
                _modelImposto.IcmsAliq = Validation.ConvertToDouble(icms_2.AliqICMS_Value);

                _modelImposto.IcmsStReducaoAliq = Validation.ConvertToDouble(icms_2.RedBaseST_Value);
                _modelImposto.IcmsStAliq = Validation.ConvertToDouble(icms_2.AliqST_Value);
            }
            else
            {
                icms_2.RedBase_Value = Validation.Price(_modelImposto.IcmsReducaoAliq);
                icms_2.IVA_Value = Validation.Price(_modelImposto.IcmsIva);
                icms_2.AliqICMS_Value = Validation.Price(_modelImposto.IcmsAliq);
                icms_2.RedBaseST_Value = Validation.Price(_modelImposto.IcmsStReducaoAliq);
                icms_2.AliqST_Value = Validation.Price(_modelImposto.IcmsStAliq);
            }            
        }

        private void GetImpostos_ICMS3(int tipo)
        {
            if (tipo == 0)
            {
                _modelImposto.IcmsIva = Validation.ConvertToDouble(icms_3.Base_Value);
                _modelImposto.IcmsAliq = Validation.ConvertToDouble(icms_3.Aliq_Value);
            }
            else
            {
                icms_3.Base_Value = Validation.Price(_modelImposto.IcmsIva);
                icms_3.Aliq_Value = Validation.Price(_modelImposto.IcmsAliq);
            }            
        }

        private void GetImpostos(int tipo)
        {
            if (tipo == 0)
            {
                _modelImposto.Icms = Icms.SelectedValue.ToString();
            }
            else
            {
                Icms.SelectedValue = _modelImposto.Icms;
            }

            if (Icms.Text.Contains("101"))
            {
                //icms_1.Visible = true;
                GetImpostos_ICMS1(tipo);
            }
            else if (Icms.Text.Contains("201"))
            {
                //icms_2.Visible = true;
                GetImpostos_ICMS2(tipo);
            }
            else if (Icms.Text.Contains("202"))
            {
                //icms_2.Visible = true;
                GetImpostos_ICMS2(tipo);
            }
            else if (Icms.Text.Contains("203"))
            {
                //icms_2.Visible = true;
                GetImpostos_ICMS2(tipo);
            }
            else if (Icms.Text.Contains("500"))
            {
                //icms_3.Visible = true;
                GetImpostos_ICMS3(tipo);
            }
            else if (Icms.Text.Contains("900"))
            {
                //icms_2.Visible = true;
                GetImpostos_ICMS2(tipo);
            }
            
            if(tipo == 0)
            {
                _modelImposto.Ipi = Ipi.SelectedValue.ToString();
                _modelImposto.IpiAliq = Validation.ConvertToDouble(Aliq_IPI.Text);

                _modelImposto.Pis = Pis.SelectedValue.ToString();
                _modelImposto.PisAliq = Validation.ConvertToDouble(Aliq_Pis.Text);

                _modelImposto.Cofins = Cofins.SelectedValue.ToString();
                _modelImposto.CofinsAliq = Validation.ConvertToDouble(Aliq_Cofins.Text);
            }
            else
            {
                Ipi.SelectedValue = _modelImposto.Ipi;
                Aliq_IPI.Text = Validation.Price(_modelImposto.IpiAliq);

                Pis.SelectedValue = _modelImposto.Pis;
                Aliq_Pis.Text = Validation.Price(_modelImposto.PisAliq);

                Cofins.SelectedValue = _modelImposto.Cofins;
                Aliq_Cofins.Text = Validation.Price(_modelImposto.CofinsAliq);
            }            
        }

        private void SetIcms()
        {            
            icms_1.Visible = false;
            icms_2.Visible = false;
            icms_3.Visible = false;

            if (Icms.Text.Contains("101"))
            {
                icms_1.Visible = true;
            }
            else if (Icms.Text.Contains("201"))
            {
                icms_2.Visible = true;
            }
            else if (Icms.Text.Contains("202"))
            {
                icms_2.Visible = true;
            }
            else if (Icms.Text.Contains("203"))
            {
                icms_2.Visible = true;
            }
            else if (Icms.Text.Contains("500"))
            {
                icms_3.Visible = true;
            }
            else if (Icms.Text.Contains("900"))
            {
                icms_2.Visible = true;
            }
        }

        private void Eventos()
        {
            Load += (s, e) => Carregar();

            label6.Click += (s, e) => Close();
            btnExit.Click += (s, e) => Close();

            btnSalvar.Click += (s, e) =>
            {
                _modelImposto.Id = idImpSelected;
                _modelImposto.Nome = nome.Text;
                _modelImposto.Cfop = cfop.Text;

                GetImpostos(0);

                if (_modelImposto.Save(_modelImposto))
                    Close();
            };
            btnRemover.Click += (s, e) =>
            {
                var data = _modelImposto.Remove(idImpSelected);
                if (data)
                    Close();
            };

            Icms.SelectedValueChanged += (s, e) => SetIcms();

            addCfop.Click += (s, e) =>
            {
                Cfops form = new Cfops();
                if (form.ShowDialog() == DialogResult.OK)
                {
                //    _mPedido.Id = Id;
                //    _mPedido.Colaborador = PedidoModalClientes.Id;
                //    _mPedido.Save(_mPedido);
                //    LoadData();
                }
            };

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");
        }
    }
}
