using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.Properties;
using SqlKata.Execution;

namespace Emiplus.View.Produtos.TelasImportarNfe
{
    public partial class ImportarProdutosConcluido : Form
    {
        private Item _mItem = new Item();
        private ItemEstoqueMovimentacao _mItemEstoqueMovimentacao = new ItemEstoqueMovimentacao();

        private readonly BackgroundWorker workerBackground = new BackgroundWorker();

        public ImportarProdutosConcluido()
        {
            InitializeComponent();
            Eventos();
        }

        private void SetTable()
        {
            GridLista.ColumnCount = 2;

            GridLista.Columns[0].Name = "Produto";
            GridLista.Columns[0].Width = 150;
            GridLista.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            var columnImg = new DataGridViewImageColumn();
            {
                columnImg.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                columnImg.HeaderText = @"Importado";
                columnImg.Name = "Importado";
                columnImg.Width = 70;
            }
            GridLista.Columns.Insert(1, columnImg);

            GridLista.Columns[2].Name = "Ordem";
            GridLista.Columns[2].Visible = false;

            foreach (dynamic item in ImportarProdutos.produtos)
                GridLista.Rows.Add(
                    item.Nome,
                    new Bitmap(Resources.error16x),
                    item.Ordem
                );
        }

        private async Task Importar()
        {
            foreach (dynamic item in ImportarProdutos.produtos)
            {
                int id = item.Id;
                string nome = item.Nome;
                string codeBarras = item.CodeBarras;

                _mItem = _mItem.Query()
                    .Where(q => q.Where("id", id).OrWhere("nome", nome).OrWhere("codebarras", codeBarras))
                    .FirstOrDefault<Item>();

                _mItem.Tipo = "Produtos";
                _mItem.Excluir = 0;
                _mItem.Referencia = item.Referencia;
                _mItem.CodeBarras = item.CodeBarras;
                _mItem.Nome = item.Nome;
                _mItem.Medida = item.Medida;
                _mItem.EstoqueAtual = item.Estoque;
                _mItem.Categoriaid = item.CategoriaId;
                _mItem.ValorCompra = item.ValorCompra;
                _mItem.ValorVenda = item.ValorVenda;
                _mItem.Fornecedor = Validation.ConvertToInt32(item.Fornecedor);
                _mItem.Ncm = item.NCM ?? "";
                _mItem.id_sync = item.idSync == 0 ? Validation.RandomSecurity() : item.idSync;
                if (_mItem.Save(_mItem, false))
                {
                    var data = _mItemEstoqueMovimentacao
                        .SetUsuario(Settings.Default.user_id)
                        .SetQuantidade(Validation.ConvertToDouble(item.EstoqueCompra))
                        .SetTipo("A")
                        .SetLocal("")
                        .SetObs("Importação de compra")
                        .SetItem(_mItem)
                        .Save(_mItemEstoqueMovimentacao);

                    foreach (DataGridViewRow gridData in GridLista.Rows)
                        if ((int) gridData.Cells["Ordem"].Value == (int) item.Ordem)
                            gridData.Cells["Importado"].Value = new Bitmap(Resources.success16x);
                }
            }
        }

        private void Eventos()
        {
            Load += (s, e) => { SetTable(); };

            btnImportar.Click += (s, e) => workerBackground.RunWorkerAsync();

            btnClose.Click += (s, e) =>
            {
                Application.OpenForms["ImportarNfe"]?.Close();
                Application.OpenForms["ImportarProdutos"]?.Close();
                Close();
            };

            workerBackground.DoWork += (s, e) => GridLista.Invoke((MethodInvoker) delegate { Importar(); });

            workerBackground.RunWorkerCompleted += (s, e) =>
            {
                label1.Text = @"Importação Concluída! :)";
                btnImportar.Visible = false;
                btnClose.Visible = true;
            };
        }
    }
}