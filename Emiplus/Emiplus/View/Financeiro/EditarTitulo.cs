using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.View.Common;
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
            _modelTitulo = _modelTitulo.FindById(IdTitulo).First<Titulo>();

            emissao.Text = _modelTitulo.Emissao == null ? Validation.ConvertDateToForm(Validation.DateNowToSql()) : Validation.ConvertDateToForm(_modelTitulo.Emissao);
            vencimento.Text = _modelTitulo.Vencimento == null ? "" : Validation.ConvertDateToForm(_modelTitulo.Vencimento);
            
            total.Text = _modelTitulo.Total == null ? "" : Validation.Price(_modelTitulo.Total);
            
            dataRecebido.Text = _modelTitulo.Baixa_data == null ? "" : Validation.ConvertDateToForm(_modelTitulo.Baixa_data);
            recebido.Text = _modelTitulo.Recebido == null ? "" : Validation.Price(_modelTitulo.Recebido);
        }

        private void Save()
        {
            if (String.IsNullOrEmpty(emissao.Text))
            {
                Alert.Message("Atenção", "É necessário informar uma data de emissão", Alert.AlertType.warning);
                emissao.Focus();
                return;
            }

            if (String.IsNullOrEmpty(vencimento.Text))
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

            if (cliente.SelectedValue != null)
                _modelTitulo.Id_Pessoa = (int)cliente.SelectedValue;

            if (formaPgto.SelectedValue != null)
                _modelTitulo.Id_FormaPgto = (int)formaPgto.SelectedValue;

            if (receita.SelectedValue != null)
                _modelTitulo.Id_Categoria = (int)receita.SelectedValue;
            
            if (_modelTitulo.Save(_modelTitulo))
                Close();
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
                if (Home.financeiroPage == "Pagar")
                {
                    label23.Text = "Pagar para";
                    label6.Text = "Pagamentos";
                    label8.Text = "Despesa";
                    label3.Text = "Forma Pagar";

                    visualGroupBox2.Text = "Pagamento";
                    label9.Text = "Data Pagamento";
                    label10.Text = "Valor Pagamento";

                    //label4.Left = 350;
                    //pictureBox1.Left = 325;
                }

                if (IdTitulo > 0)
                    LoadData();
                else
                {
                    emissao.Text = Validation.ConvertDateToForm(Validation.DateNowToSql());
                    vencimento.Text = Validation.ConvertDateToForm(Validation.DateNowToSql());
                }

                var formasPagamentos = new FormaPagamento().FindAll().Where("excluir", 0).OrderByDesc("nome").Get();
                if (formasPagamentos.Count() > 0)
                {
                    formaPgto.DataSource = formasPagamentos;
                    formaPgto.DisplayMember = "NOME";
                    formaPgto.ValueMember = "ID";
                    formaPgto.SelectedValue = _modelTitulo.Id_FormaPgto;
                }

                IEnumerable<dynamic> clientes;
                clientes = new Pessoa().FindAll().Where("excluir", 0).WhereNotLike("nome", "%Novo registro%").OrderByDesc("nome").Get();
                if (clientes.Count() > 0)
                {
                    cliente.DataSource = clientes;
                    cliente.DisplayMember = "NOME";
                    cliente.ValueMember = "ID";
                    cliente.SelectedValue = _modelTitulo.Id_Pessoa;
                }

                var CategoriasdeContas = "";
                if (Home.financeiroPage == "Pagar")
                    CategoriasdeContas = "Despesas";
                else
                    CategoriasdeContas = "Receitas";

                IEnumerable<dynamic> categorias;
                categorias = new Categoria().FindAll().Where("excluir", 0).Where("tipo", CategoriasdeContas).OrderByDesc("nome").Get();
                if (categorias.Count() > 0)
                {
                    receita.DataSource = categorias;
                    receita.DisplayMember = "NOME";
                    receita.ValueMember = "ID";
                    receita.SelectedValue = _modelTitulo.Id_Categoria;
                }
            };
            
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
