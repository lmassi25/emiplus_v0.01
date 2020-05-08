using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.View.Common;
using SqlKata.Execution;

namespace Emiplus.View.Financeiro
{
    public partial class EditAllTitulos : Form
    {
        public static List<int> listTitulos = new List<int>();
        private Titulo _modelTitulo = new Titulo();

        public EditAllTitulos()
        {
            InitializeComponent();
            Eventos();
        }

        private void SetHeadersTable(DataGridView Table)
        {
            Table.ColumnCount = 6;

            typeof(DataGridView).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table,
                new object[] {true});
            Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = Home.financeiroPage == "Receber" ? "Receber de" : "Pagar para";
            Table.Columns[1].Width = 150;
            Table.Columns[1].Visible = true;

            Table.Columns[2].Name = "Forma de Pagamento";
            Table.Columns[2].Width = 160;

            Table.Columns[3].Name = "Vencimento";
            Table.Columns[3].Width = 100;

            Table.Columns[4].Name = "Total";
            Table.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[4].Width = 100;

            Table.Columns[5].Name = Home.financeiroPage == "Receber" ? "Recebido" : "Pago";
            Table.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[5].Width = 100;
        }

        private void SetContentTable(DataGridView Table)
        {
            Table.Rows.Clear();

            foreach (var item in listTitulos)
            {
                var mTitulo = new Titulo();
                var DB = mTitulo.Query()
                    .LeftJoin("formapgto", "formapgto.id", "titulo.id_formapgto")
                    .LeftJoin("pessoa", "pessoa.id", "titulo.id_pessoa")
                    .Select("titulo.id", "titulo.recebido", "titulo.vencimento", "titulo.emissao", "titulo.total",
                        "titulo.id_pedido", "titulo.baixa_data", "titulo.baixa_total", "formapgto.nome as formapgto",
                        "pessoa.nome", "pessoa.fantasia", "pessoa.rg", "pessoa.cpf")
                    .Where("titulo.excluir", 0).Where("titulo.id", item);

                foreach (var data in DB.Get())
                    Table.Rows.Add(
                        data.ID,
                        data.NOME,
                        data.FORMAPGTO,
                        Validation.ConvertDateToForm(data.VENCIMENTO),
                        Validation.FormatPrice(Validation.ConvertToDouble(data.TOTAL), true),
                        Validation.FormatPrice(Validation.ConvertToDouble(data.RECEBIDO), true)
                    );
            }

            Table.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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

        private void Save()
        {
            foreach (var item in listTitulos)
            {
                _modelTitulo = _modelTitulo.FindById(item).FirstOrDefault<Titulo>();
                _modelTitulo.Tipo = Home.financeiroPage;

                if (!string.IsNullOrEmpty(dataRecebido.Text))
                    _modelTitulo.Baixa_data = Validation.ConvertDateToSql(dataRecebido.Text);

                if (!string.IsNullOrEmpty(recebido.Text))
                    _modelTitulo.Recebido = Validation.ConvertToDouble(recebido.Text);

                if (Validation.ConvertToInt32(cliente.SelectedValue) != 0)
                    _modelTitulo.Id_Pessoa = Validation.ConvertToInt32(cliente.SelectedValue);

                if (Validation.ConvertToInt32(receita.SelectedValue) != 0)
                    _modelTitulo.Id_Categoria = Validation.ConvertToInt32(receita.SelectedValue);

                if (Validation.ConvertToInt32(formaPgto.SelectedValue) != 0)
                    _modelTitulo.Id_FormaPgto = Validation.ConvertToInt32(formaPgto.SelectedValue);

                _modelTitulo.Save(_modelTitulo, false);
            }

            listTitulos = null;
            Close();
        }

        private void Eventos()
        {
            Masks.SetToUpper(this);

            Shown += (s, e) =>
            {
                if (Home.financeiroPage == "Pagar")
                {
                    label6.Text = @"Pagamentos";
                    label2.Text = @"Pagamentoss a serem editados:";
                    label3.Text = @"As alterações abaixo, será aplicado a todos os pagamentos listado acima.";

                    label23.Text = @"Pagar para";
                    label8.Text = @"Despesa";
                    visualGroupBox2.Text = @"Pagamento";
                    label9.Text = @"Data Pagamento";
                    label10.Text = @"Valor Pagamento";
                }

                Refresh();

                formaPgto.ValueMember = "Id";
                formaPgto.DisplayMember = "Nome";
                formaPgto.DataSource = new FormaPagamento().GetAll();

                LoadFornecedores();
                LoadCategorias();

                SetHeadersTable(GridLista);
                SetContentTable(GridLista);
            };

            btnSalvar.Click += (s, e) =>
            {
                var result = AlertOptions.Message("Atenção!", "Confirmar alterações?", AlertBig.AlertType.warning,
                    AlertBig.AlertBtn.YesNo);
                if (result)
                    Save();
            };

            dataRecebido.KeyPress += Masks.MaskBirthday;
            recebido.TextChanged += (s, e) =>
            {
                var txt = (TextBox) s;
                Masks.MaskPrice(ref txt);
            };

            label6.Click += (s, e) => Close();
            btnExit.Click += (s, e) => Close();
        }
    }
}