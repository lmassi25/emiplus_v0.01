using Emiplus.Data.Helpers;
using Emiplus.Model;
using SqlKata.Execution;
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

        public AddCaixaMov()
        {
            InitializeComponent();
            Eventos();
        }

        private void Start()
        {
            var cat = new Categoria().FindAll().Where("tipo", "Financeiro").WhereFalse("excluir").OrderByDesc("nome").Get();
            if (cat.Count() > 0)
            {
                Categorias.DataSource = cat;
                Categorias.DisplayMember = "NOME";
                Categorias.ValueMember = "ID";
            }
            Categorias.SelectedValue = "";

            var fornecedor = new Pessoa().FindAll().Where("tipo", "Fornecedores").WhereFalse("excluir").OrderByDesc("nome").Get();
            if (fornecedor.Count() > 0)
            {
                Fornecedor.DataSource = fornecedor;
                Fornecedor.DisplayMember = "NOME";
                Fornecedor.ValueMember = "ID";
            }
            Fornecedor.SelectedValue = "";

            if (idMov > 0)
            {
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
                        Dinheiro.Enabled = false;
                        Cheque.Enabled = false;
                        Tipo1.Checked = true;
                        Tipo2.Enabled = false;
                        Tipo3.Enabled = false;
                        Valor.Enabled = false;
                        Categorias.Enabled = false;
                        Fornecedor.Enabled = false;
                        Obs.Enabled = false;
                        btnSalvar.Enabled = false;
                        break;
                    case 2:
                        Tipo2.Checked = true;
                        break;
                    case 3:
                        Tipo3.Checked = true;
                        break;
                }

                Valor.Text = Validation.FormatPrice(_modelCaixaMov.Valor);
                Categorias.SelectedValue = _modelCaixaMov.id_categoria;
                Fornecedor.SelectedValue = _modelCaixaMov.id_pessoa;
                Obs.Text = _modelCaixaMov.Obs;
            }
        }

        private void Eventos()
        {
            Load += (s, e) =>
            {
                Start();
            };

            Tipo1.Click += (s, e) =>
            {
                Categorias.Enabled = true;
                Fornecedor.Enabled = true;
            };
            Tipo2.Click += (s, e) =>
            {
                Categorias.Enabled = true;
                Fornecedor.Enabled = false;
            };

            Tipo3.Click += (s, e) =>
            {
                Categorias.Enabled = false;
                Fornecedor.Enabled = false;
            };

            btnSalvar.Click += (s, e) =>
            {
                _modelCaixaMov.id_caixa = idCaixa;
                _modelCaixaMov.id_formapgto = Dinheiro.Checked ? 1 : Cheque.Checked ? 2 : 1;

                if (Categorias.SelectedValue != null)
                    _modelCaixaMov.id_categoria = (int)Categorias.SelectedValue;

                if (Fornecedor.SelectedValue != null)
                    _modelCaixaMov.id_pessoa = (int)Fornecedor.SelectedValue;

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

            btnCancelar.Click += (s, e) => Close();
        }
    }
}
