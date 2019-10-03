using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.View.Common;
using SqlKata.Execution;
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

            label6.Text = "Contas a " + Home.financeiroPage;
            if (Home.financeiroPage == "Pagar")
            {
                label4.Left = 350;
                pictureBox1.Left = 325;
            }

            if (IdTitulo > 0)
                LoadData();
        }

        private void LoadData()
        {
            _modelTitulo = _modelTitulo.FindById(IdTitulo).First<Titulo>();

            vencimento.Text = _modelTitulo.Vencimento == null ? "" : Validation.ConvertDateToForm(_modelTitulo.Vencimento);
            emissao.Text = _modelTitulo.Emissao == null ? "" : Validation.ConvertDateToForm(_modelTitulo.Emissao);
            total.Text = _modelTitulo.Total == null ? "" : Validation.Price(_modelTitulo.Total);
            dataRecebido.Text = _modelTitulo.Baixa_data == null ? "" : Validation.ConvertDateToForm(_modelTitulo.Baixa_data);
            recebido.Text = _modelTitulo.Recebido == null ? "" : Validation.Price(_modelTitulo.Recebido);
        }

        private void Start()
        {
            var formasPagamentos = new FormaPagamento().FindAll().Where("excluir", 0).OrderByDesc("nome").Get();
            if (formasPagamentos.Count() > 0)
            {
                formaPgto.DataSource = formasPagamentos;
                formaPgto.DisplayMember = "NOME";
                formaPgto.ValueMember = "ID";
                formaPgto.SelectedValue = _modelTitulo.Id_FormaPgto;
            }
            
            IEnumerable<dynamic> clientes;
            if (Home.financeiroPage == "Receber")
                clientes = new Pessoa().FindAll().Where("excluir", 0).Where("tipo", "Clientes").OrderByDesc("nome").Get();
            else
                clientes = new Pessoa().FindAll().Where("excluir", 0).OrderByDesc("nome").Get();

            if (clientes.Count() > 0)
            {
                cliente.DataSource = clientes;
                cliente.DisplayMember = "NOME";
                cliente.ValueMember = "ID";
                cliente.SelectedValue = _modelTitulo.Id_Pessoa;
            }
        }

        private void Save()
        {
            _modelTitulo.Id = IdTitulo;
            _modelTitulo.Vencimento = Validation.ConvertDateToSql(vencimento.Text);
            _modelTitulo.Emissao = Validation.ConvertDateToSql(emissao.Text);
            _modelTitulo.Total = Validation.ConvertToDouble(total.Text);
            _modelTitulo.Baixa_data = string.IsNullOrEmpty(dataRecebido.Text) ? null : Validation.ConvertDateToSql(dataRecebido.Text);
            _modelTitulo.Recebido = Validation.ConvertToDouble(recebido.Text);

            if (cliente.SelectedValue != null)
                _modelTitulo.Id_Pessoa = (int)cliente.SelectedValue;

            if (formaPgto.SelectedValue != null)
                _modelTitulo.Id_FormaPgto = (int)formaPgto.SelectedValue;

            if (_modelTitulo.Save(_modelTitulo))
                Close();
        }

        private void Eventos()
        {
            Load += (s, e) => Start();
            
            btnSalvar.Click += (s, e) => Save();
            btnRemover.Click += (s, e) =>
            {
                var data = _modelTitulo.Remove(IdTitulo);
                if (data)
                    Close();
            };

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

            btnExit.Click += (s, e) => Close();
            label6.Click += (s, e) => Close();

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");
        }
    }
}
