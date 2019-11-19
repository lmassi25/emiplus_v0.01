using Emiplus.Data.Helpers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Financeiro
{
    public partial class DetailsCaixa : Form
    {
        public static int idCaixa { get; set; }

        private Controller.Caixa _controllerCaixa = new Controller.Caixa();
        private Model.Caixa _modelCaixa = new Model.Caixa();
        private Model.CaixaMovimentacao _modelCaixaMov = new Model.CaixaMovimentacao();
        private Model.Usuarios _modelUsuarios = new Model.Usuarios();

        public DetailsCaixa()
        {
            InitializeComponent();
            Eventos();
        }

        private void LoadUsuario(int idUser)
        {
            var user = _modelUsuarios.FindByUserId(idUser).FirstOrDefault();
            if (user != null)
                colaborador.Text = user.NOME;
        }
        
        private void LoadData()
        {
            _modelCaixa = _modelCaixa.FindById(idCaixa).FirstOrDefault<Model.Caixa>();
            
            caixa.Text = _modelCaixa.Terminal;
            nrTerminal.Text = _modelCaixa.Terminal;
            aberto.Text = Validation.ConvertDateToForm(_modelCaixa.Criado, true);
            label7.Text = _modelCaixa.Tipo == "Aberto" ? "Caixa Aberto" : "Caixa Fechado";

            txtSaldoInicial.Text = Validation.FormatPrice(_modelCaixa.Saldo_Inicial, true);

            if (_modelCaixa.Tipo == "Fechado")
            {
                panel7.BackColor = Color.FromArgb(192, 0, 0);
                label9.Text = Validation.ConvertDateToForm(_modelCaixa.Fechado, true);
                btnFechar.Visible = false;
            }
                
            LoadUsuario(_modelCaixa.Usuario);

            txtEntradas.Text = Validation.FormatPrice(_controllerCaixa.SumEntradas(idCaixa), true);
            txtSaidas.Text = Validation.FormatPrice(_controllerCaixa.SumSaidas(idCaixa), true);
            txtDinheiro.Text = Validation.FormatPrice(_controllerCaixa.SumDinheiro(idCaixa), true);
            txtSaldoFinal.Text = Validation.FormatPrice(_controllerCaixa.SumTotal(idCaixa), true);
        }

        private void LoadTotais()
        {
            LoadData();
            DataTableAsync();
        }

        private async Task DataTableAsync() => await SetTable(GridLista);

        public Task<IEnumerable<dynamic>> GetDataTable()
        {
            var model = new Model.CaixaMovimentacao().Query();
            model.Where("id_caixa", idCaixa);
            model.LeftJoin("FORMAPGTO", "FORMAPGTO.id", "CAIXA_MOV.id_formapgto");
            model.Select("FORMAPGTO.nome as nome_pgto", "CAIXA_MOV.*");
            model.OrderByDesc("CAIXA_MOV.criado");
            return model.GetAsync<dynamic>();
        }

        public async Task SetTable(DataGridView Table, IEnumerable<dynamic> Data = null)
        {
            Table.ColumnCount = 5;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "Data/Hora";
            Table.Columns[0].Width = 100;

            Table.Columns[1].Name = "Descrição";
            Table.Columns[1].Width = 150;

            Table.Columns[2].Name = "Entrada";
            Table.Columns[2].Width = 80;

            Table.Columns[3].Name = "Saída";
            Table.Columns[3].Width = 80;

            Table.Columns[4].Name = "Forma Pagto.";
            Table.Columns[4].Width = 100;

            Table.Rows.Clear();

            if (Data == null)
            {
                IEnumerable<dynamic> dados = await GetDataTable();
                Data = dados;
            }

            for (int i = 0; i < Data.Count(); i++)
            {
                var item = Data.ElementAt(i);

                var valor = Validation.FormatPrice(Validation.ConvertToDouble(item.VALOR), true);

                Table.Rows.Add(
                    Validation.ConvertDateToForm(item.CRIADO, true),
                    item.DESCRICAO,
                    item.TIPO == "3" ? valor : "",
                    item.TIPO == "1" || item.TIPO == "2" ? valor : "",
                    item.NOME_PGTO
                );
            }

            Table.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void Eventos()
        {
            Load += async (s, e) =>
            {
                LoadData();
                await DataTableAsync();
            };
            
            btnLancamentos.Click += (s, e) =>
            {
                if (_modelCaixa.Tipo == "Fechado")
                {
                    Alert.Message("Oppss!", "Não é possível fazer lançamentos em um caixa fechado.", Alert.AlertType.warning);
                    return;
                }

                AddCaixaMov.idCaixa = idCaixa;
                var f = new AddCaixaMov();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadTotais();
                }
            };

            btnFechar.Click += (s, e) =>
            {
                FecharCaixa.idCaixa = idCaixa;
                FecharCaixa f = new FecharCaixa();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    label9.Text = DateTime.Now.ToString("dd/mm/YYYY HH:mm");
                    panel7.BackColor = Color.FromArgb(192, 0, 0);
                    label7.Text = "Caixa Fechado";
                    btnFechar.Visible = false;
                }
            };

            btnExit.Click += (s, e) => Close();

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser(Program.URL_BASE + "/ajuda");
        }
    }
}
