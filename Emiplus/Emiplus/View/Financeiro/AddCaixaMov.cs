using Emiplus.Data.Helpers;
using Emiplus.Model;
using SqlKata.Execution;
using System.Linq;
using System.Windows.Forms;

namespace Emiplus.View.Financeiro
{
    public partial class AddCaixaMov : Form
    {
        public static int idCaixa { get; set; }
        private CaixaMovimentacao _modelCaixaMov = new CaixaMovimentacao();
        
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
                Fornecedor.Enabled = true;
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
                    tipo = "Saída - Lançamento de Despesa";
                else if (Tipo2.Checked)
                    tipo = "Saída - Sangria";
                else if (Tipo3.Checked)
                    tipo = "Entrada - Acréscimo";

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
                    DialogResult = DialogResult.OK;
                    Close();
                }
            };

            Valor.TextChanged += (s, e) =>
            {
                TextBox txt = (TextBox)s;
                Masks.MaskPrice(ref txt);
            };

            btnCancelar.Click += (s, e) => Close();
        }
    }
}
