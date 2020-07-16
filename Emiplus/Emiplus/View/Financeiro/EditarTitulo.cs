using System;
using System.Linq;
using System.Windows.Forms;
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.View.Comercial;
using Emiplus.View.Common;
using Emiplus.View.Produtos;
using SqlKata.Execution;

namespace Emiplus.View.Financeiro
{
    public partial class EditarTitulo : Form
    {
        private Titulo _modelTitulo = new Titulo();
        private readonly Controller.Titulo _controllerTitulo = new Controller.Titulo();

        public EditarTitulo()
        {
            InitializeComponent();
            Eventos();
        }

        public static int IdTitulo { get; set; }

        private void LoadData()
        {
            _modelTitulo = _modelTitulo.FindById(IdTitulo).FirstOrDefault<Titulo>();

            emissao.Text = _modelTitulo.Emissao == null
                ? Validation.ConvertDateToForm(Validation.DateNowToSql())
                : Validation.ConvertDateToForm(_modelTitulo.Emissao);
            vencimento.Text = _modelTitulo.Vencimento == null
                ? ""
                : Validation.ConvertDateToForm(_modelTitulo.Vencimento);

            total.Text = Math.Abs(_modelTitulo.Total) < 0 ? "" : Validation.Price(_modelTitulo.Total);

            dataRecebido.Text = _modelTitulo.Baixa_data == null
                ? ""
                : Validation.ConvertDateToForm(_modelTitulo.Baixa_data);
            recebido.Text = Math.Abs(_modelTitulo.Recebido) < 0 ? "" : Validation.Price(_modelTitulo.Recebido);
            valorVenda.Text = Math.Abs(_controllerTitulo.GetTotalPedido(_modelTitulo.Id_Pedido)) < 0 ? Validation.FormatPrice(0, true) : Validation.FormatPrice(_controllerTitulo.GetTotalPedido(_modelTitulo.Id_Pedido), true);
            valorLiquido.Text = Math.Abs(_controllerTitulo.GetTotalPedido(_modelTitulo.Id_Pedido)) < 0
                ? Validation.FormatPrice(0, true)
                : Validation.FormatPrice(_controllerTitulo.GetTotalPedido(_modelTitulo.Id_Pedido), true);

            if (!string.IsNullOrEmpty(_modelTitulo.Taxas))
            {
                var taxas = _modelTitulo.Taxas.Split('|');
                if (taxas.Any())
                {
                    tarifaFixa.Text = $@"Tarifa fixa R$ {Validation.FormatPrice(Validation.ConvertToDouble(taxas[0]))}";
                    txtTarifaFixa.Text = $@"R$ {Validation.FormatPrice(Validation.ConvertToDouble(taxas[0]))}";

                    tarifaCartao.Text = $@"Taxa do cartão {taxas[1]}%";
                    var taxaCartao = _controllerTitulo.GetTotalPedido(_modelTitulo.Id_Pedido) / 100 * Validation.ConvertToDouble(taxas[1]);
                    txtTaxaCartao.Text = $@"{Validation.FormatPrice(taxaCartao, true)}";
                    
                    var taxaparcelas = _controllerTitulo.GetTotalPedido(_modelTitulo.Id_Pedido) / 100 * Validation.ConvertToDouble(taxas[2]);
                    var totalParcelas = taxaparcelas * Validation.ConvertToInt32(taxas[5]);
                    txtTaxaParcela.Text = taxas[6] != "0" ? $@"R$ {Validation.FormatPrice(Validation.ConvertToDouble(totalParcelas))}" : $@"R$ {Validation.FormatPrice(0)}";

                    tarifaParcelamento.Text = $@"Taxa de parcelamento {taxas[2]}% x {taxas[5]}";
                    txtTaxaAntecipacao.Text = $@"{Validation.FormatPrice(Validation.ConvertToDouble(taxas[3]), true)}";

                    if (string.IsNullOrEmpty(taxas[4])) prazoReceber.Visible = false;
                    prazoReceber.Text = $@"No prazo de {taxas[4]} dias.";

                    if (taxas[6] != "0")
                        valorLiquido.Text = Math.Abs(_controllerTitulo.GetTotalPedido(_modelTitulo.Id_Pedido)) < 0
                        ? Validation.FormatPrice(0, true)
                        : Validation.FormatPrice(_controllerTitulo.GetTotalPedido(_modelTitulo.Id_Pedido) - Validation.ConvertToDouble(taxas[0]) - taxaCartao - totalParcelas - Validation.ConvertToDouble(taxas[3]), true);
                    else
                        valorLiquido.Text = Math.Abs(_controllerTitulo.GetTotalPedido(_modelTitulo.Id_Pedido)) < 0
                            ? Validation.FormatPrice(0, true)
                            : Validation.FormatPrice(_controllerTitulo.GetTotalPedido(_modelTitulo.Id_Pedido) - Validation.ConvertToDouble(taxas[0]) - taxaCartao - Validation.ConvertToDouble(taxas[3]), true);
                }
            }

            if (_modelTitulo.Recebido > 0)
            {
                btnRecebidoPago.Checked = true;
                dataRecebido.Enabled = true;
                formaPgto.Enabled = true;
                recebido.Enabled = true;
            }

            cliente.SelectedValue = _modelTitulo.Id_Pessoa.ToString();
            formaPgto.SelectedValue = _modelTitulo.Id_FormaPgto.ToString();
            receita.SelectedValue = _modelTitulo.Id_Categoria.ToString();
            recorrente.SelectedValue = _modelTitulo.Tipo_Recorrencia.ToString();

            xRecorrente.Text = _modelTitulo.Qtd_Recorrencia.ToString();

            if (_modelTitulo.Id == 0)
                btnCancelar.Visible = false;
        }

        private void Save()
        {
            if (string.IsNullOrEmpty(emissao.Text))
            {
                Alert.Message("Atenção", "É necessário informar uma data de emissão", Alert.AlertType.warning);
                emissao.Focus();
                return;
            }

            if (string.IsNullOrEmpty(vencimento.Text))
            {
                Alert.Message("Atenção", "É necessário informar uma data de vencimento", Alert.AlertType.warning);
                return;
            }

            _modelTitulo.Id = IdTitulo;
            _modelTitulo.Tipo = Home.financeiroPage;
            _modelTitulo.Vencimento = Validation.ConvertDateToSql(vencimento.Text);
            _modelTitulo.Emissao = Validation.ConvertDateToSql(emissao.Text);
            _modelTitulo.Total = Validation.ConvertToDouble(total.Text);
            _modelTitulo.Baixa_data = string.IsNullOrEmpty(dataRecebido.Text)
                ? null
                : Validation.ConvertDateToSql(dataRecebido.Text);
            _modelTitulo.Recebido = Validation.ConvertToDouble(recebido.Text);
            //_modelTitulo.Valor_Liquido = Validation.ConvertToDouble(valorBruto.Text);
            _modelTitulo.Qtd_Recorrencia = Validation.ConvertToInt32(xRecorrente.Text);

            _modelTitulo.Id_Pessoa = Validation.ConvertToInt32(cliente.SelectedValue);
            _modelTitulo.Id_FormaPgto = Validation.ConvertToInt32(formaPgto.SelectedValue);
            _modelTitulo.Id_Categoria = Validation.ConvertToInt32(receita.SelectedValue);
            _modelTitulo.Tipo_Recorrencia = Validation.ConvertToInt32(recorrente.SelectedIndex);

            if (_modelTitulo.Save(_modelTitulo))
            {
                if (IdTitulo == 0)
                {
                    var idTituloPai = _modelTitulo.GetLastId();
                    _modelTitulo.Id = idTituloPai;
                    _modelTitulo.ID_Recorrencia_Pai = idTituloPai;
                    _modelTitulo.Nr_Recorrencia = 1;
                    _modelTitulo.Save(_modelTitulo, false);

                    if (xRecorrente.Text != "0")
                    {
                        var qtdRep = Validation.ConvertToInt32(xRecorrente.Text);
                        var nr = 1;
                        for (var i = 1; i < qtdRep; i++)
                        {
                            nr++;

                            _modelTitulo.Id = 0;
                            _modelTitulo.ID_Recorrencia_Pai = idTituloPai;
                            _modelTitulo.Tipo = Home.financeiroPage;

                            var dataVencimento = Convert.ToDateTime(vencimento.Text);
                            switch (recorrente.SelectedIndex)
                            {
                                case 1:
                                    dataVencimento = dataVencimento.AddDays(i);
                                    break;
                                case 2:
                                    dataVencimento = dataVencimento.AddDays(i * 7);
                                    break;
                                case 3:
                                    dataVencimento = dataVencimento.AddDays(i * 14);
                                    break;
                                case 4:
                                    dataVencimento = dataVencimento.AddMonths(i);
                                    break;
                                case 5:
                                    dataVencimento = dataVencimento.AddMonths(i * 3);
                                    break;
                                case 6:
                                    dataVencimento = dataVencimento.AddMonths(i * 6);
                                    break;
                                case 7:
                                    dataVencimento = dataVencimento.AddYears(i);
                                    break;
                            }

                            _modelTitulo.Vencimento = Validation.ConvertDateToSql(dataVencimento);

                            _modelTitulo.Emissao = Validation.ConvertDateToSql(emissao.Text);
                            _modelTitulo.Total = Validation.ConvertToDouble(total.Text);
                            _modelTitulo.Baixa_data = string.IsNullOrEmpty(dataRecebido.Text)
                                ? null
                                : Validation.ConvertDateToSql(dataRecebido.Text);
                            _modelTitulo.Recebido = Validation.ConvertToDouble(recebido.Text);
                            _modelTitulo.Qtd_Recorrencia = Validation.ConvertToInt32(xRecorrente.Text);

                            _modelTitulo.Id_Pessoa = Validation.ConvertToInt32(cliente.SelectedValue);
                            _modelTitulo.Id_FormaPgto = Validation.ConvertToInt32(formaPgto.SelectedValue);
                            _modelTitulo.Id_Categoria = Validation.ConvertToInt32(receita.SelectedValue);
                            _modelTitulo.Tipo_Recorrencia = Validation.ConvertToInt32(recorrente.SelectedIndex);

                            _modelTitulo.Nr_Recorrencia = nr;

                            _modelTitulo.Save(_modelTitulo, false);
                        }
                    }
                }

                Close();
            }
        }

        private void LoadFornecedores()
        {
            cliente.DataSource = new Pessoa().GetAll("Fornecedores");
            cliente.ValueMember = "Id";
            cliente.DisplayMember = "Nome";
        }

        private void LoadCategorias()
        {
            var categoriasdeContas = Home.financeiroPage == "Pagar" ? "Despesas" : "Receitas";

            receita.DataSource = new Categoria().GetAll(categoriasdeContas);
            receita.ValueMember = "Id";
            receita.DisplayMember = "Nome";
        }

        private void LoadRecorrencia()
        {
            var dadosRecorrencia = _modelTitulo.FindById(IdTitulo).Where("tipo_recorrencia", "!=", "0")
                .FirstOrDefault<Titulo>();
            if (dadosRecorrencia != null)
            {
                checkRepetir.Checked = true;
                recorrente.Enabled = true;
                xRecorrente.Enabled = true;
                panel1.Visible = true;
                var qtdTitulo = 0;

                if (dadosRecorrencia.ID_Recorrencia_Pai != 0)
                {
                    var qtdTitulos = _modelTitulo.Query().SelectRaw("COUNT (id) AS TOTAL")
                        .Where("id_recorrencia_pai", dadosRecorrencia.ID_Recorrencia_Pai)
                        .WhereNotNull("id_recorrencia_pai").FirstOrDefault();
                    qtdTitulo = Validation.ConvertToInt32(qtdTitulos.TOTAL);

                    var valorTitulos = _modelTitulo.Query().SelectRaw("SUM (total) AS TOTAL")
                        .Where("id_recorrencia_pai", dadosRecorrencia.ID_Recorrencia_Pai)
                        .WhereNotNull("id_recorrencia_pai").FirstOrDefault();
                    label22.Text = Validation.FormatPrice(Validation.ConvertToDouble(valorTitulos.TOTAL), true);
                }

                var nrParcela = $"{dadosRecorrencia.Nr_Recorrencia.ToString()} de {qtdTitulo}";
                label18.Text = nrParcela;

                xRecorrente.Enabled = false;
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

            Load += (s, e) =>
            {
                ToolHelp.Show("Defina a data de emissão do título!", pictureBox12, ToolHelp.ToolTipIcon.Info, "Ajuda!");
                ToolHelp.Show("Defina a data inicial do vencimento do título!", pictureBox4, ToolHelp.ToolTipIcon.Info,
                    "Ajuda!");
                ToolHelp.Show(
                    "Escolha a recorrência para esse título.\nObservação: O campo 'Quantas parcelas' irá criar os titulos conforme o número preenchido no momento que salvar, caso fique em branco os título só será gerado no prazo definido de antecedência do vencimento.",
                    pictureBox6, ToolHelp.ToolTipIcon.Info, "Ajuda!");
                ToolHelp.Show(
                    "Defina a quantidade de parcelas que deseja gerar com base no campo anterior, caso desejar 'deixe em branco' para o sistema gerar automaticamente as parcelas quando o prazo de vencimento estiver chegando.",
                    pictureBox7, ToolHelp.ToolTipIcon.Info, "Ajuda!");

                if (Home.financeiroPage == "Pagar")
                {
                    label23.Text = @"Pagar para";
                    label6.Text = @"Pagamentos";
                    label8.Text = @"Despesa";
                    label3.Text = @"Forma Pagar";
                    label12.Text = @"Esse pagamento se repete?";

                    label9.Text = @"Data Pagamento";
                    label10.Text = @"Valor Pagamento";
                    btnRecebidoPago.Text = @"Pago";
                }

                formaPgto.ValueMember = "Id";
                formaPgto.DisplayMember = "Nome";
                formaPgto.DataSource = new FormaPagamento().GetAll();

                LoadFornecedores();
                LoadCategorias();

                recorrente.DataSource = Support.GetTiposRecorrencia();
                recorrente.DisplayMember = "Nome";
                recorrente.ValueMember = "Id";

                if (IdTitulo > 0)
                {
                    LoadData();
                    LoadRecorrencia();
                }
                else
                {
                    emissao.Text = Validation.ConvertDateToForm(Validation.DateNowToSql());
                    vencimento.Text = Validation.ConvertDateToForm(Validation.DateNowToSql());
                }
            };

            btnSalvar.Click += (s, e) => Save();
            btnCancelar.Click += (s, e) =>
            {
                var data = _modelTitulo.Remove(IdTitulo);
                if (data)
                    Close();
            };

            xRecorrente.KeyPress += (s, e) => Masks.MaskOnlyNumbers(s, e);
            emissao.KeyPress += Masks.MaskBirthday;
            dataRecebido.KeyPress += Masks.MaskBirthday;
            vencimento.KeyPress += Masks.MaskBirthday;
            total.TextChanged += (s, e) =>
            {
                var txt = (TextBox) s;
                Masks.MaskPrice(ref txt);
            };

            recebido.TextChanged += (s, e) =>
            {
                var txt = (TextBox) s;
                Masks.MaskPrice(ref txt);
            };

            recorrente.SelectedIndexChanged += (s, e) => { xRecorrente.Enabled = recorrente.SelectedIndex != 0; };

            btnAddCliente.Click += (s, e) =>
            {
                Home.pessoaPage = "Fornecedores";
                AddClientes.Id = 0;
                var f = new AddClientes
                {
                    FormBorderStyle = FormBorderStyle.FixedSingle,
                    StartPosition = FormStartPosition.CenterScreen
                };
                if (f.ShowDialog() == DialogResult.OK)
                    LoadFornecedores();
            };

            btnAddCategoria.Click += (s, e) =>
            {
                var categoriasdeContas = Home.financeiroPage == "Pagar" ? "Despesas" : "Receitas";

                Home.CategoriaPage = categoriasdeContas;
                var f = new AddCategorias
                {
                    FormBorderStyle = FormBorderStyle.FixedSingle,
                    StartPosition = FormStartPosition.CenterScreen
                };
                if (f.ShowDialog() == DialogResult.OK)
                    LoadCategorias();
            };

            checkRepetir.CheckStateChanged += (s, e) =>
            {
                if (checkRepetir.Checked)
                {
                    recorrente.Enabled = true;
                    xRecorrente.Enabled = true;
                }
                else
                {
                    recorrente.Enabled = false;
                    xRecorrente.Enabled = false;
                }
            };

            btnRecebidoPago.CheckStateChanged += (s, e) =>
            {
                if (btnRecebidoPago.Checked)
                {
                    dataRecebido.Enabled = true;
                    formaPgto.Enabled = true;
                    recebido.Enabled = true;
                }
                else
                {
                    dataRecebido.Enabled = false;
                    formaPgto.Enabled = false;
                    recebido.Enabled = false;
                }
            };

            menuTaxas.Click += (s, e) => Support.DynamicPanel(flowLayoutPanel, panelTaxas, menuTaxas);
            label16.Click += (s, e) => Support.DynamicPanel(flowLayoutPanel, panelTaxas, menuTaxas);
            pictureBox9.Click += (s, e) => Support.DynamicPanel(flowLayoutPanel, panelTaxas, menuTaxas);

            menuBoleto.Click += (s, e) => Support.DynamicPanel(flowLayoutPanel, panelBoleto, menuBoleto);

            btnExit.Click += (s, e) => Close();
            label6.Click += (s, e) => Close();

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser(Configs.LinkAjuda);
        }
    }
}