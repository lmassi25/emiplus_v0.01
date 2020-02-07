using DotLiquid;
using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.View.Common;
using Emiplus.View.Produtos;
using Emiplus.View.Reports;
using SqlKata.Execution;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Emiplus.View.Financeiro
{
    public partial class AddCaixaMov : Form
    {
        public static int idMov { get; set; }
        public static int idCaixa { get; set; }
        private CaixaMovimentacao _modelCaixaMov = new CaixaMovimentacao();
        private Titulo _modelTitulo = new Titulo();
        private Model.Caixa _modelCaixa = new Model.Caixa();
        private Model.Usuarios _modelUsuarios = new Model.Usuarios();

        public AddCaixaMov()
        {
            InitializeComponent();
            Eventos();
        }

        private void LoadFornecedores()
        {
            Fornecedor.DataSource = new Pessoa().GetAll();
            Fornecedor.ValueMember = "Id";
            Fornecedor.DisplayMember = "Nome";
        }

        private void Start()
        {
            LoadCategorias("Despesas");
            LoadFornecedores();

            if (idMov > 0)
            {
                imprimir.Visible = true;
                btnApagar.Visible = true;

                _modelCaixaMov = _modelCaixaMov.FindById(idMov).FirstOrDefault<CaixaMovimentacao>();
                idCaixa = _modelCaixaMov.id_caixa;

                if (_modelCaixaMov.id_formapgto == 1)
                    Dinheiro.Checked = true;

                if (_modelCaixaMov.id_formapgto == 2)
                    Cheque.Checked = true;

                switch (_modelCaixaMov.Tipo)
                {
                    case 1:
                        //Dinheiro.Enabled = false;
                        //Cheque.Enabled = false;
                        //Tipo1.Checked = true;
                        //Tipo2.Enabled = false;
                        //Tipo3.Enabled = false;
                        //Valor.Enabled = false;
                        //Categorias.Enabled = false;
                        //Fornecedor.Enabled = false;
                        //Obs.Enabled = false;
                        //btnSalvar.Enabled = false;

                        Categorias.Enabled = true;
                        Fornecedor.Enabled = true;
                        imprimir.Visible = false;
                        label4.Text = "Despesa:";
                        break;

                    case 2:
                        Tipo2.Checked = true;
                        Categorias.Enabled = true;
                        Fornecedor.Enabled = false;
                        imprimir.Visible = true;
                        label4.Text = "Despesa:";
                        break;

                    case 3:
                        Tipo3.Checked = true;
                        Categorias.Enabled = false;
                        Fornecedor.Enabled = false;
                        imprimir.Visible = false;
                        label4.Text = "Receita:";
                        break;
                }

                Valor.Text = Validation.FormatPrice(_modelCaixaMov.Valor);
                Categorias.SelectedValue = _modelCaixaMov.id_categoria.ToString();
                Fornecedor.SelectedValue = _modelCaixaMov.id_pessoa.ToString();
                Obs.Text = _modelCaixaMov.Obs;
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

        private void LoadCategorias(string tipo)
        {
            Categorias.DataSource = new Categoria().GetAll(tipo);
            Categorias.ValueMember = "Id";
            Categorias.DisplayMember = "Nome";

            Categorias.Refresh();
        }

        private void Eventos()
        {
            KeyDown += KeyDowns;
            KeyPreview = true;
            Masks.SetToUpper(this);

            Load += (s, e) =>
            {
                Start();
            };

            Tipo1.Click += (s, e) =>
            {
                Categorias.Enabled = true;
                Fornecedor.Enabled = true;
                imprimir.Visible = false;
                label4.Text = "Despesa:";
                LoadCategorias("Despesas");
            };
            Tipo2.Click += (s, e) =>
            {
                Categorias.Enabled = true;
                Fornecedor.Enabled = false;
                imprimir.Visible = true;
                label4.Text = "Despesa:";
                LoadCategorias("Despesas");
            };

            Tipo3.Click += (s, e) =>
            {
                Categorias.Enabled = false;
                Fornecedor.Enabled = false;
                imprimir.Visible = false;
                label4.Text = "Receita:";
                LoadCategorias("Receitas");
            };

            btnAddCategoria.Click += (s, e) =>
            {
                string CategoriasdeContas = "";
                if (Tipo1.Checked || Tipo2.Checked)
                    CategoriasdeContas = "Despesas";
                else
                    CategoriasdeContas = "Receitas";

                Home.CategoriaPage = CategoriasdeContas;
                AddCategorias f = new AddCategorias();
                f.FormBorderStyle = FormBorderStyle.FixedSingle;
                f.StartPosition = FormStartPosition.CenterScreen;
                if (f.ShowDialog() == DialogResult.OK)
                    LoadCategorias(CategoriasdeContas);
            };

            btnAddFornecedor.Click += (s, e) =>
            {
                Home.pessoaPage = "Fornecedores";
                Comercial.AddClientes.Id = 0;
                Comercial.AddClientes f = new Comercial.AddClientes();
                f.FormBorderStyle = FormBorderStyle.FixedSingle;
                f.StartPosition = FormStartPosition.CenterScreen;
                if (f.ShowDialog() == DialogResult.OK)
                    LoadFornecedores();
            };

            btnSalvar.Click += (s, e) =>
            {
                _modelCaixaMov.id_caixa = idCaixa;
                _modelCaixaMov.id_formapgto = Dinheiro.Checked ? 1 : Cheque.Checked ? 2 : 1;
                
                _modelCaixaMov.id_categoria = Validation.ConvertToInt32(Categorias.SelectedValue);
                _modelCaixaMov.id_pessoa = Validation.ConvertToInt32(Fornecedor.SelectedValue);

                _modelCaixaMov.Tipo = Tipo1.Checked ? 1 : Tipo2.Checked ? 2 : Tipo3.Checked ? 3 : 1;

                var tipo = "";
                if (Tipo1.Checked)
                {
                    tipo = "Saída - Lançamento de Despesa";
                }
                else if (Tipo2.Checked)
                {
                    tipo = "Saída - Sangria";
                }
                else if (Tipo3.Checked)
                {
                    tipo = "Entrada - Acréscimo";
                }

                var formaPgto = "";
                if (Dinheiro.Checked)
                    formaPgto = "Dinheiro";
                else if (Cheque.Checked)
                    formaPgto = "Cheque";

                _modelCaixaMov.Descricao = $"{formaPgto} - {tipo}";
                _modelCaixaMov.Valor = Validation.ConvertToDouble(Valor.Text);
                _modelCaixaMov.Obs = Obs.Text;
                if (_modelCaixaMov.Save(_modelCaixaMov))
                {
                    if (Tipo1.Checked)
                    {
                        if (_modelCaixaMov.Id != 0)
                            _modelTitulo = _modelTitulo.Query().Where("id_caixa_mov", _modelCaixaMov.Id).Where("excluir", 0).FirstOrDefault<Model.Titulo>();
                        
                        _modelTitulo.Tipo = "Pagar";
                        _modelTitulo.Emissao = Validation.DateNowToSql();
                        _modelTitulo.Id_Categoria = _modelCaixaMov.id_categoria;
                        _modelTitulo.Id_Pessoa = _modelCaixaMov.id_pessoa;
                        _modelTitulo.Total = _modelCaixaMov.Valor;
                        _modelTitulo.Id_FormaPgto = _modelCaixaMov.id_formapgto;
                        _modelTitulo.Vencimento = Validation.DateNowToSql();
                        _modelTitulo.Baixa_data = Validation.DateNowToSql();
                        _modelTitulo.Baixa_id_formapgto = _modelCaixaMov.id_formapgto;
                        _modelTitulo.Baixa_total = _modelCaixaMov.Valor;
                        _modelTitulo.Id_Caixa = idCaixa;
                        _modelTitulo.Id_Caixa_Mov = _modelCaixaMov.GetLastId();
                        _modelTitulo.Obs = $"Pagamento gerado a partir de um lançamento do caixa. {Obs.Text}";
                        _modelTitulo.Save(_modelTitulo, false);
                    }

                    DialogResult = DialogResult.OK;
                    Close();
                }
            };

            Valor.TextChanged += (s, e) =>
            {
                TextBox txt = (TextBox)s;
                Masks.MaskPrice(ref txt);
            };

            btnApagar.Click += (s, e) =>
            {
                if (_modelCaixaMov.Remove(idMov))
                {
                    var titulo = _modelTitulo.Query().Where("ID_CAIXA_MOV", idMov).FirstOrDefault();
                    if (titulo != null)
                        _modelTitulo.RemoveIdCaixaMov(idMov);

                    DialogResult = DialogResult.OK;
                    Close();
                }
            };

            imprimir.Click += (s, e) =>
            {
                _modelCaixa = _modelCaixa.FindById(idCaixa).FirstOrDefault<Model.Caixa>();

                var user = _modelUsuarios.FindByUserId(_modelCaixa.Usuario).FirstOrDefault();
                string userName = "";
                if (user != null)
                    userName = user.NOME;

                var html = Template.Parse(File.ReadAllText($@"{Program.PATH_BASE}\html\CupomAssinaturaCaixaMov.html"));
                var render = html.Render(Hash.FromAnonymousObject(new
                {
                    INCLUDE_PATH = Program.PATH_BASE,
                    URL_BASE = Program.PATH_BASE,
                    Emissao = DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                    nrTerminal = _modelCaixa.Terminal,
                    nrCaixa = _modelCaixa.Id.ToString(),
                    Responsavel = userName,
                    Valor = Valor.Text
                }));

                Browser.htmlRender = render;
                var f = new Browser();
                f.ShowDialog();
            };

            btnCancelar.Click += (s, e) => Close();
        }
    }
}