using Emiplus.Data.Helpers;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Produtos.TelasImportarNfe
{
    public partial class ImportarProdutosConcluido : Form
    {
        private Model.Item _mItem = new Model.Item();

        private BackgroundWorker WorkerBackground = new BackgroundWorker();

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

            DataGridViewImageColumn columnImg = new DataGridViewImageColumn();
            {
                columnImg.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                columnImg.HeaderText = "Importado";
                columnImg.Name = "Importado";
                columnImg.Width = 70;
            }
            GridLista.Columns.Insert(1, columnImg);

            GridLista.Columns[2].Name = "Ordem";
            GridLista.Columns[2].Visible = false;

            foreach (dynamic item in ImportarProdutos.produtos)
            {
                GridLista.Rows.Add(
                    item.Nome,
                    new Bitmap(Properties.Resources.error16x),
                    item.Ordem
                );
            }
        }

        private async Task Importar()
        {
            foreach (dynamic item in ImportarProdutos.produtos)
            {
                _mItem.Id = item.Id;
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
                _mItem.id_sync = Validation.RandomSecurity();
                _mItem.status_sync = "CREATE";
                if (_mItem.Save(_mItem, false))
                {
                    foreach (DataGridViewRow gridData in GridLista.Rows)
                    {
                        if ((int)gridData.Cells["Ordem"].Value == (int)item.Ordem)
                        {
                            gridData.Cells["Importado"].Value = new Bitmap(Properties.Resources.success16x);
                        }
                    }
                }
            }
        }

        private void Eventos()
        {
            Load += (s, e) =>
            {
                SetTable();
            };

            btnImportar.Click += (s, e) => WorkerBackground.RunWorkerAsync();

            btnClose.Click += (s, e) =>
            {
                Application.OpenForms["ImportarNfe"].Close();
                Application.OpenForms["ImportarProdutos"].Close();
                Close();
            };

            WorkerBackground.DoWork += (s, e) => GridLista.Invoke((MethodInvoker) delegate {
                Importar();
            });

            WorkerBackground.RunWorkerCompleted += async (s, e) =>
            {
                label1.Text = "Importação Concluída! :)";
                btnImportar.Visible = false;
                btnClose.Visible = true;
            };
        }
    }
}