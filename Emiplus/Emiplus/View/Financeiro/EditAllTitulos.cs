using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.View.Common;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Financeiro
{
    public partial class EditAllTitulos : Form
    {
        private Titulo _modelTitulo = new Titulo();

        public static List<int> listTitulos = new List<int>();

        public EditAllTitulos()
        {
            InitializeComponent();
            Eventos();
        }

        private void SetHeadersTable(DataGridView Table)
        {
            Table.ColumnCount = 6;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            if (Home.financeiroPage == "Receber")
                Table.Columns[1].Name = "Receber de";
            else
                Table.Columns[1].Name = "Pagar para";

            Table.Columns[1].Width = 150;
            Table.Columns[1].Visible = true;

            Table.Columns[2].Name = "Forma de Pagamento";
            Table.Columns[2].Width = 160;

            Table.Columns[3].Name = "Vencimento";
            Table.Columns[3].Width = 100;

            Table.Columns[4].Name = "Total";
            Table.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[4].Width = 100;

            if (Home.financeiroPage == "Receber")
                Table.Columns[5].Name = "Recebido";
            else
                Table.Columns[5].Name = "Pago";

            Table.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[5].Width = 100;
        }

        private void SetContentTable(DataGridView Table)
        {
            Table.Rows.Clear();

            foreach (var item in listTitulos)
            {
                var mTitulo = new Model.Titulo();
                var DB = mTitulo.Query()
                .LeftJoin("formapgto", "formapgto.id", "titulo.id_formapgto")
                .LeftJoin("pessoa", "pessoa.id", "titulo.id_pessoa")
                .Select("titulo.id", "titulo.recebido", "titulo.vencimento", "titulo.emissao", "titulo.total", "titulo.id_pedido", "titulo.baixa_data", "titulo.baixa_total", "formapgto.nome as formapgto", "pessoa.nome", "pessoa.fantasia", "pessoa.rg", "pessoa.cpf")
                .Where("titulo.excluir", 0).Where("titulo.id", item);

                foreach (dynamic data in DB.Get())
                {
                    Table.Rows.Add(
                        data.ID,
                        data.NOME,
                        data.FORMAPGTO,
                        Validation.ConvertDateToForm(data.VENCIMENTO),
                        Validation.FormatPrice(Validation.ConvertToDouble(data.TOTAL), true),
                        Validation.FormatPrice(Validation.ConvertToDouble(data.RECEBIDO), true)
                    );
                }
            }

            Table.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void LoadFornecedores()
        {
            cliente.DataSource = new Pessoa().GetAll();
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

        private void Save()
        {
            foreach (var item in listTitulos)
            {
                _modelTitulo = _modelTitulo.FindById(item).FirstOrDefault<Model.Titulo>();
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
                    label6.Text = "Pagamentos";
                    label2.Text = "Pagamentoss a serem editados:";
                    label3.Text = "As alterações abaixo, será aplicado a todos os pagamentos listado acima.";

                    label23.Text = "Pagar para";
                    label8.Text = "Despesa";
                    visualGroupBox2.Text = "Pagamento";
                    label9.Text = "Data Pagamento";
                    label10.Text = "Valor Pagamento";
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
                var result = AlertOptions.Message("Atenção!", "Confirmar alterações?", AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                if (result)
                    Save();
            };

            dataRecebido.KeyPress += (s, e) => Masks.MaskBirthday(s, e);
            recebido.TextChanged += (s, e) =>
            {
                TextBox txt = (TextBox)s;
                Masks.MaskPrice(ref txt);
            };

            label6.Click += (s, e) => Close();
            btnExit.Click += (s, e) => Close();
        }
    }
}
