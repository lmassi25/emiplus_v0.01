﻿using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.View.Produtos.Imposto.CFOP;
using SqlKata.Execution;

namespace Emiplus.View.Produtos
{
    public partial class AddImpostos : Form
    {
        private readonly int idImpSelected = Impostos.idImpSelected;
        private Model.Imposto _modelImposto = new Model.Imposto();

        public AddImpostos()
        {
            InitializeComponent();
            Eventos();
        }

        private void LoadData()
        {
            _modelImposto = _modelImposto.FindById(idImpSelected).FirstOrDefault<Model.Imposto>();

            nome.Text = _modelImposto.Nome ?? "";
            cfop.Text = _modelImposto.Cfop ?? "";

            GetImpostos(1);
        }

        private void Carregar()
        {
            nome.Select();

            ToolHelp.Show(
                "Digite um nome para o imposto que está cadastrando." + Environment.NewLine +
                "Utilize uma descrição para a finalidade do imposto. ", pictureBox6, ToolHelp.ToolTipIcon.Info,
                "Ajuda!");

            #region COMBOBOX

            var icms = new ArrayList
            {
                new {Id = "0", Nome = ""},
                new {Id = "00", Nome = "00 - Tributação integralmente"},
                new {Id = "10", Nome = "10 - Tributação com cobrança do ICMS por S.T."},
                new {Id = "20", Nome = "20 - Tributação com redução de base de cálculo"},
                new {Id = "30", Nome = "30 - Tributação Isenta ou não tributada e com cobrança do ICMS por S.T."},
                new {Id = "40", Nome = "40 - Tributação Isenta"},
                new {Id = "41", Nome = "41 - Não Tributada"},
                new {Id = "50", Nome = "50 - Tributação Suspensa"},
                new {Id = "51", Nome = "51 - Tributação com Diferimento"},
                new {Id = "60", Nome = "60 - Tributação ICMS cobrado anteriormente por S.T."},
                new
                {
                    Id = "70",
                    Nome = "70 - Tributação ICMS com redução de base de cálculo e cobrança do ICMS por S.T."
                },
                new {Id = "90", Nome = "90 - Tributação ICMS: Outros"},
                new {Id = "101", Nome = "101 - Tributada pelo Simples Nacional com permissão de crédito"},
                new {Id = "102", Nome = "102 - Tributada pelo Simples Nacional sem permissão de crédito"},
                new {Id = "103", Nome = "103 - Isenção do ICMS no Simples Nacional para faixa de receita bruta"},
                new
                {
                    Id = "201",
                    Nome =
                        "201 - Tributada pelo Simples Nacional com permissão de crédito e com cobrança do ICMS por S.T."
                },
                new
                {
                    Id = "202",
                    Nome =
                        "202 - Tributada pelo Simples Nacional sem permissão de crédito e com cobrança do ICMS por S.T."
                },
                new
                {
                    Id = "203",
                    Nome =
                        "203 - Isenção do ICMS nos Simples Nacional para faixa de receita bruta e com cobrança do ICMS por S.T."
                },
                new {Id = "300", Nome = "300 - Imune"},
                new {Id = "400", Nome = "400 - Não tributada pelo Simples Nacional"},
                new {Id = "500", Nome = "500 - ICMS cobrado anteriormente por S.T. (substituído) ou por antecipação"},
                new {Id = "900", Nome = "900 - Outros"}
            };
            Icms.DataSource = icms;
            Icms.DisplayMember = "Nome";
            Icms.ValueMember = "Id";

            var ipi = new ArrayList
            {
                new {Id = "0", Nome = ""},
                new {Id = "00", Nome = "00 - Tributado: Entrada com recuperação de crédito"},
                new {Id = "49", Nome = "49 - Tributado: Outras entradas"},
                new {Id = "50", Nome = "50 - Tributado: Saída tributada"},
                new {Id = "99", Nome = "99 - Tributado: Outras saídas"},
                new {Id = "01", Nome = "01 - Não Tributado: Entrada tributada com alíquota zero"},
                new {Id = "02", Nome = "02 - Não Tributado: Entrada isenta"},
                new {Id = "03", Nome = "03 - Não Tributado: Entrada não-tributada"},
                new {Id = "04", Nome = "04 - Não Tributado: Entrada imune"},
                new {Id = "05", Nome = "05 - Não Tributado: Entrada com suspensão"},
                new {Id = "51", Nome = "51 - Não Tributado: Saída tributada com alíquota zero"},
                new {Id = "52", Nome = "52 - Não Tributado: Saída isenta"},
                new {Id = "53", Nome = "53 - Não Tributado: Saída não-tributada"},
                new {Id = "54", Nome = "54 - Não Tributado: Saída imune"},
                new {Id = "55", Nome = "55 - Não Tributado: Saída com suspensão"}
            };

            Ipi.DataSource = ipi;
            Ipi.DisplayMember = "Nome";
            Ipi.ValueMember = "Id";

            var pis = new ArrayList
            {
                new {Id = "0", Nome = ""},
                new
                {
                    Id = "01",
                    Nome =
                        "01 - Tributado pela Alíquota: Operação Tributável (base de cálculo = valor da operação alíquota normal (cumulativo/não cumulativo))"
                },
                new
                {
                    Id = "02",
                    Nome =
                        "02 - Tributado pela Alíquota: Operação Tributável (base de cálculo = valor da operação (alíquota diferenciada))"
                },
                new
                {
                    Id = "03",
                    Nome =
                        "03 - Tributado pela Quantidade: Operação Tributável (base de cálculo = quantidade vendida x alíquota por unidade de produto)"
                },
                new
                {
                    Id = "04",
                    Nome = "04 - Não Tributado: Operação Tributável (tributação monofásica (alíquota zero))"
                },
                new {Id = "05", Nome = "05 - Não Tributado: Operação Tributável (Substituição Tributária)"},
                new {Id = "06", Nome = "06 - Não Tributado: Operação Tributável (alíquota zero)"},
                new {Id = "07", Nome = "07 - Não Tributado: Operação Isenta da Contribuição"},
                new {Id = "08", Nome = "08 - Não Tributado: Operação Sem Incidência da Contribuição"},
                new {Id = "09", Nome = "09 - Não Tributado: Operação com Suspensão da Contribuição"},
                new {Id = "49", Nome = "49 - Outras Operações: Outras Operações de Saída"},
                new
                {
                    Id = "50",
                    Nome =
                        "50 - Outras Operações: Operação com Direito a Crédito - Vinculada Exclusivamente a Receita Tributada no Mercado Interno"
                },
                new
                {
                    Id = "51",
                    Nome =
                        "51 - Outras Operações: Operação com Direito a Crédito - Vinculada Exclusivamente a Receita Não Tributada no Mercado Interno"
                },
                new
                {
                    Id = "52",
                    Nome =
                        "52 - Outras Operações: Operação com Direito a Crédito – Vinculada Exclusivamente a Receita de Exportação"
                },
                new
                {
                    Id = "53",
                    Nome =
                        "53 - Outras Operações: Operação com Direito a Crédito - Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno"
                },
                new
                {
                    Id = "54",
                    Nome =
                        "54 - Outras Operações: Operação com Direito a Crédito -Vinculada a Receitas Tributadas no Mercado Interno e de Exportação"
                },
                new
                {
                    Id = "55",
                    Nome =
                        "55 - Outras Operações: Operação com Direito a Crédito -Vinculada a Receitas NãoTributadas no Mercado Interno e de Exportação"
                },
                new
                {
                    Id = "56",
                    Nome =
                        "56 - Outras Operações: Operação com Direito a Crédito -Vinculada a Receitas Tributadas e Não - Tributadas no Mercado Interno, e de Exportação"
                },
                new
                {
                    Id = "60",
                    Nome =
                        "60 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita Tributada no Mercado Interno"
                },
                new
                {
                    Id = "61",
                    Nome =
                        "61 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita Não - Tributada no Mercado Interno"
                },
                new
                {
                    Id = "62",
                    Nome =
                        "62 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita de Exportação"
                },
                new
                {
                    Id = "63",
                    Nome =
                        "63 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno"
                },
                new
                {
                    Id = "64",
                    Nome =
                        "64 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas no Mercado Interno e de Exportação"
                },
                new
                {
                    Id = "65",
                    Nome =
                        "65 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada a Receitas Não-Tributadas no Mercado Interno e de Exportação"
                },
                new
                {
                    Id = "66",
                    Nome =
                        "66 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno, e de Exportação"
                },
                new {Id = "67", Nome = "67 - Outras Operações: Crédito Presumido - Outras Operações"},
                new {Id = "70", Nome = "70 - Outras Operações: Operação de Aquisição sem Direito a Crédito"},
                new {Id = "71", Nome = "71 - Outras Operações: Operação de Aquisição com Isenção"},
                new {Id = "72", Nome = "72 - Outras Operações: Operação de Aquisição com Suspensão"},
                new {Id = "73", Nome = "73 - Outras Operações: Operação de Aquisição a Alíquota Zero"},
                new {Id = "74", Nome = "74 - Outras Operações: Operação de Aquisição; sem Incidência da Contribuição"},
                new {Id = "75", Nome = "75 - Outras Operações: Operação de Aquisição por Substituição Tributária"},
                new {Id = "98", Nome = "98 - Outras Operações: Outras Operações de Entrada"},
                new {Id = "99", Nome = "99 - Outras Operações: Outras Operações"}
            };

            Pis.DataSource = pis;
            Pis.DisplayMember = "Nome";
            Pis.ValueMember = "Id";

            var cofins = new ArrayList
            {
                new {Id = "0", Nome = ""},
                new
                {
                    Id = "01",
                    Nome =
                        "01 - Tributado pela Alíquota: Operação Tributável (base de cálculo = valor da operação alíquota normal (cumulativo/não cumulativo))"
                },
                new
                {
                    Id = "02",
                    Nome =
                        "02 - Tributado pela Alíquota: Operação Tributável (base de cálculo = valor da operação (alíquota diferenciada))"
                },
                new
                {
                    Id = "03",
                    Nome =
                        "03 - Tributado pela Quantidade: Operação Tributável (base de cálculo = quantidade vendida x alíquota por unidade de produto)"
                },
                new
                {
                    Id = "04",
                    Nome = "04 - Não Tributado: Operação Tributável (tributação monofásica (alíquota zero))"
                },
                new {Id = "05", Nome = "05 - Não Tributado: Operação Tributável (Substituição Tributária)"},
                new {Id = "06", Nome = "06 - Não Tributado: Operação Tributável (alíquota zero)"},
                new {Id = "07", Nome = "07 - Não Tributado: Operação Isenta da Contribuição"},
                new {Id = "08", Nome = "08 - Não Tributado: Operação Sem Incidência da Contribuição"},
                new {Id = "09", Nome = "09 - Não Tributado: Operação com Suspensão da Contribuição"},
                new {Id = "49", Nome = "49 - Outras Operações: Outras Operações de Saída"},
                new
                {
                    Id = "50",
                    Nome =
                        "50 - Outras Operações: Operação com Direito a Crédito - Vinculada Exclusivamente a Receita Tributada no Mercado Interno"
                },
                new
                {
                    Id = "51",
                    Nome =
                        "51 - Outras Operações: Operação com Direito a Crédito - Vinculada Exclusivamente a Receita Não Tributada no Mercado Interno"
                },
                new
                {
                    Id = "52",
                    Nome =
                        "52 - Outras Operações: Operação com Direito a Crédito – Vinculada Exclusivamente a Receita de Exportação"
                },
                new
                {
                    Id = "53",
                    Nome =
                        "53 - Outras Operações: Operação com Direito a Crédito - Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno"
                },
                new
                {
                    Id = "54",
                    Nome =
                        "54 - Outras Operações: Operação com Direito a Crédito -Vinculada a Receitas Tributadas no Mercado Interno e de Exportação"
                },
                new
                {
                    Id = "55",
                    Nome =
                        "55 - Outras Operações: Operação com Direito a Crédito -Vinculada a Receitas NãoTributadas no Mercado Interno e de Exportação"
                },
                new
                {
                    Id = "56",
                    Nome =
                        "56 - Outras Operações: Operação com Direito a Crédito -Vinculada a Receitas Tributadas e Não - Tributadas no Mercado Interno, e de Exportação"
                },
                new
                {
                    Id = "60",
                    Nome =
                        "60 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita Tributada no Mercado Interno"
                },
                new
                {
                    Id = "61",
                    Nome =
                        "61 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita Não - Tributada no Mercado Interno"
                },
                new
                {
                    Id = "62",
                    Nome =
                        "62 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita de Exportação"
                },
                new
                {
                    Id = "63",
                    Nome =
                        "63 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno"
                },
                new
                {
                    Id = "64",
                    Nome =
                        "64 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas no Mercado Interno e de Exportação"
                },
                new
                {
                    Id = "65",
                    Nome =
                        "65 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada a Receitas Não-Tributadas no Mercado Interno e de Exportação"
                },
                new
                {
                    Id = "66",
                    Nome =
                        "66 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno, e de Exportação"
                },
                new {Id = "67", Nome = "67 - Outras Operações: Crédito Presumido - Outras Operações"},
                new {Id = "70", Nome = "70 - Outras Operações: Operação de Aquisição sem Direito a Crédito"},
                new {Id = "71", Nome = "71 - Outras Operações: Operação de Aquisição com Isenção"},
                new {Id = "72", Nome = "72 - Outras Operações: Operação de Aquisição com Suspensão"},
                new {Id = "73", Nome = "73 - Outras Operações: Operação de Aquisição a Alíquota Zero"},
                new {Id = "74", Nome = "74 - Outras Operações: Operação de Aquisição; sem Incidência da Contribuição"},
                new {Id = "75", Nome = "75 - Outras Operações: Operação de Aquisição por Substituição Tributária"},
                new {Id = "98", Nome = "98 - Outras Operações: Outras Operações de Entrada"},
                new {Id = "99", Nome = "99 - Outras Operações: Outras Operações"}
            };

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
            icms_2.Size = new Size(474, 172);
            icms_3.Size = new Size(214, 117);

            #endregion COMBOBOX

            if (idImpSelected > 0)
                LoadData();
        }

        private void GetImpostos_ICMS1(int tipo)
        {
            if (tipo == 0)
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
                _modelImposto.Icms = Icms.SelectedValue.ToString();
            else
                Icms.SelectedValue = _modelImposto.Icms;

            if (Icms.Text.Contains("101"))
                //icms_1.Visible = true;
                GetImpostos_ICMS1(tipo);
            else if (Icms.Text.Contains("201"))
                //icms_2.Visible = true;
                GetImpostos_ICMS2(tipo);
            else if (Icms.Text.Contains("202"))
                //icms_2.Visible = true;
                GetImpostos_ICMS2(tipo);
            else if (Icms.Text.Contains("203"))
                //icms_2.Visible = true;
                GetImpostos_ICMS2(tipo);
            else if (Icms.Text.Contains("500"))
                //icms_3.Visible = true;
                GetImpostos_ICMS3(tipo);
            else if (Icms.Text.Contains("900"))
                //icms_2.Visible = true;
                GetImpostos_ICMS2(tipo);

            if (tipo == 0)
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
                icms_1.Visible = true;
            else if (Icms.Text.Contains("201"))
                icms_2.Visible = true;
            else if (Icms.Text.Contains("202"))
                icms_2.Visible = true;
            else if (Icms.Text.Contains("203"))
                icms_2.Visible = true;
            else if (Icms.Text.Contains("500"))
                icms_3.Visible = true;
            else if (Icms.Text.Contains("900")) icms_2.Visible = true;
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
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
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
                var form = new Cfops {TopMost = true};
                if (form.ShowDialog() == DialogResult.OK)
                {
                    //    _mPedido.Id = Id;
                    //    _mPedido.Colaborador = PedidoModalClientes.Id;
                    //    _mPedido.Save(_mPedido);
                    //    LoadData();
                }
            };

            cfop.KeyPress += (s, e) => Masks.MaskOnlyNumbers(s, e, 4);

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser(Configs.LinkAjuda);
        }
    }
}