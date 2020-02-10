using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.View.Common;
using Emiplus.View.Produtos;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Emiplus.View.Financeiro
{
    public partial class EditarTitulo : Form
    {
        public static int IdTitulo { get; set; }
        private Titulo _modelTitulo = new Titulo();

        public EditarTitulo()
        {
            InitializeComponent();
            Eventos();
        }

        private void LoadData()
        {
            _modelTitulo = _modelTitulo.FindById(IdTitulo).FirstOrDefault<Titulo>();

            emissao.Text = _modelTitulo.Emissao == null ? Validation.ConvertDateToForm(Validation.DateNowToSql()) : Validation.ConvertDateToForm(_modelTitulo.Emissao);
            vencimento.Text = _modelTitulo.Vencimento == null ? "" : Validation.ConvertDateToForm(_modelTitulo.Vencimento);

            total.Text = _modelTitulo.Total == 0 ? "" : Validation.Price(_modelTitulo.Total);

            dataRecebido.Text = _modelTitulo.Baixa_data == null ? "" : Validation.ConvertDateToForm(_modelTitulo.Baixa_data);
            recebido.Text = _modelTitulo.Recebido == 0 ? "" : Validation.Price(_modelTitulo.Recebido);

            cliente.SelectedValue = _modelTitulo.Id_Pessoa.ToString();
            formaPgto.SelectedValue = _modelTitulo.Id_FormaPgto.ToString();
            receita.SelectedValue = _modelTitulo.Id_Categoria.ToString();
            recorrente.SelectedValue = _modelTitulo.Tipo_Recorrencia.ToString();

            xRecorrente.Text = _modelTitulo.Qtd_Recorrencia.ToString();

            if (_modelTitulo.Id == 0)
                btnRemover.Visible = false;
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
            _modelTitulo.Baixa_data = string.IsNullOrEmpty(dataRecebido.Text) ? null : Validation.ConvertDateToSql(dataRecebido.Text);
            _modelTitulo.Recebido = Validation.ConvertToDouble(recebido.Text);
            _modelTitulo.Qtd_Recorrencia = Validation.ConvertToInt32(xRecorrente.Text);
           
            _modelTitulo.Id_Pessoa = Validation.ConvertToInt32(cliente.SelectedValue);
            _modelTitulo.Id_FormaPgto = Validation.ConvertToInt32(formaPgto.SelectedValue);
            _modelTitulo.Id_Categoria = Validation.ConvertToInt32(receita.SelectedValue);
            _modelTitulo.Tipo_Recorrencia = Validation.ConvertToInt32(recorrente.SelectedIndex);

            if (_modelTitulo.Save(_modelTitulo))
            {
                if (IdTitulo == 0) {

                    int idTituloPai = _modelTitulo.GetLastId();
                    _modelTitulo.Id = idTituloPai;
                    _modelTitulo.ID_Recorrencia_Pai = idTituloPai;
                    _modelTitulo.Nr_Recorrencia = 1;
                    _modelTitulo.Save(_modelTitulo, false);

                    if (xRecorrente.Text != "0")
                    {
                        int qtdRep = Validation.ConvertToInt32(xRecorrente.Text);
                        int nr = 1;
                        for (int i = 1; i < qtdRep; i++)
                        {
                            nr++;

                            _modelTitulo.Id = 0;
                            _modelTitulo.ID_Recorrencia_Pai = idTituloPai;
                            _modelTitulo.Tipo = Home.financeiroPage;

                            DateTime dataVencimento = Convert.ToDateTime(vencimento.Text);
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
                            _modelTitulo.Baixa_data = string.IsNullOrEmpty(dataRecebido.Text) ? null : Validation.ConvertDateToSql(dataRecebido.Text);
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
            var CategoriasdeContas = "";
            if (Home.financeiroPage == "Pagar")
                CategoriasdeContas = "Despesas";
            else
                CategoriasdeContas = "Receitas";

            receita.DataSource = new Categoria().GetAll(CategoriasdeContas);
            receita.ValueMember = "Id";
            receita.DisplayMember = "Nome";
        }

        private void LoadRecorrencia()
        {
            Titulo dadosRecorrencia = _modelTitulo.FindById(IdTitulo).Where("tipo_recorrencia" , "!=", "0").FirstOrDefault<Titulo>();
            if (dadosRecorrencia != null)
            {
                panel1.Visible = true;

                if (dadosRecorrencia.ID_Recorrencia_Pai != 0)
                {
                    var qtdTitulos = _modelTitulo.Query().SelectRaw("COUNT (id) AS TOTAL").Where("id_recorrencia_pai", dadosRecorrencia.ID_Recorrencia_Pai).WhereNotNull("id_recorrencia_pai").FirstOrDefault();
                    label19.Text = Validation.ConvertToInt32(qtdTitulos.TOTAL).ToString();

                    var ValorTitulos = _modelTitulo.Query().SelectRaw("SUM (total) AS TOTAL").Where("id_recorrencia_pai", dadosRecorrencia.ID_Recorrencia_Pai).WhereNotNull("id_recorrencia_pai").FirstOrDefault();
                    label22.Text = Validation.FormatPrice(Validation.ConvertToDouble(ValorTitulos.TOTAL), true);
                }

                label18.Text = dadosRecorrencia.Nr_Recorrencia.ToString();
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
                ToolHelp.Show("Defina a data inicial do vencimento do título!", pictureBox4, ToolHelp.ToolTipIcon.Info, "Ajuda!");
                ToolHelp.Show("Escolha a recorrência para esse título.\nObservação: O campo 'Quantas parcelas' irá criar os titulos conforme o número preenchido no momento que salvar, caso fique em branco os título só será gerado no prazo definido de antecedência do vencimento.", pictureBox6, ToolHelp.ToolTipIcon.Info, "Ajuda!");
                ToolHelp.Show("Defina a quantidade de parcelas que deseja gerar com base no campo anterior, caso desejar 'deixe em branco' para o sistema gerar automaticamente as parcelas quando o prazo de vencimento estiver chegando.", pictureBox7, ToolHelp.ToolTipIcon.Info, "Ajuda!");

                if (Home.financeiroPage == "Pagar")
                {
                    label23.Text = "Pagar para";
                    label6.Text = "Pagamentos";
                    label8.Text = "Despesa";
                    label3.Text = "Forma Pagar";
                    label12.Text = "Esse pagamento se repete?";

                    visualGroupBox2.Text = "Pagamento";
                    label9.Text = "Data Pagamento";
                    label10.Text = "Valor Pagamento";
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
            btnRemover.Click += (s, e) =>
            {
                var data = _modelTitulo.Remove(IdTitulo);
                if (data)
                    Close();
            };
            
            xRecorrente.KeyPress += (s, e) => Masks.MaskOnlyNumbers(s, e);
            emissao.KeyPress += (s, e) => Masks.MaskBirthday(s, e);
            dataRecebido.KeyPress += (s, e) => Masks.MaskBirthday(s, e);
            vencimento.KeyPress += (s, e) => Masks.MaskBirthday(s, e);
            total.TextChanged += (s, e) =>
            {
                TextBox txt = (TextBox)s;
                Masks.MaskPrice(ref txt);
            };

            recebido.TextChanged += (s, e) =>
            {
                TextBox txt = (TextBox)s;
                Masks.MaskPrice(ref txt);
            };

            recorrente.SelectedIndexChanged += (s, e) =>
            {
                if (recorrente.SelectedIndex == 0)
                    xRecorrente.Enabled = false;
                else
                    xRecorrente.Enabled = true;
            };

            btnAddCliente.Click += (s, e) =>
            {
                Home.pessoaPage = "Fornecedores";
                Comercial.AddClientes.Id = 0;
                Comercial.AddClientes f = new Comercial.AddClientes();
                f.FormBorderStyle = FormBorderStyle.FixedSingle;
                f.StartPosition = FormStartPosition.CenterScreen;
                if (f.ShowDialog() == DialogResult.OK)
                    LoadFornecedores();
            };

            btnAddCategoria.Click += (s, e) =>
            {
                string CategoriasdeContas = "";
                if (Home.financeiroPage == "Pagar")
                    CategoriasdeContas = "Despesas";
                else
                    CategoriasdeContas = "Receitas";

                Home.CategoriaPage = CategoriasdeContas;
                AddCategorias f = new AddCategorias();
                f.FormBorderStyle = FormBorderStyle.FixedSingle;
                f.StartPosition = FormStartPosition.CenterScreen;
                if (f.ShowDialog() == DialogResult.OK)
                    LoadCategorias();
            };

            btnExit.Click += (s, e) => Close();
            label6.Click += (s, e) => Close();

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");
        }
    }
}