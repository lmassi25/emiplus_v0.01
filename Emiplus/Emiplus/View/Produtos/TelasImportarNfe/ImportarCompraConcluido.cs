using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.Properties;
using Emiplus.View.Common;
using Estoque = Emiplus.Controller.Estoque;

namespace Emiplus.View.Produtos.TelasImportarNfe
{
    public partial class ImportarCompraConcluido : Form
    {
        private readonly Item _mItem = new Item();
        private readonly Pedido _mPedido = new Pedido();
        private readonly PedidoItem _mPedidoItem = new PedidoItem();
        private readonly Titulo _mTitulo = new Titulo();
        private readonly ImportarNfe dataNfe = new ImportarNfe();
        private readonly ArrayList produtosID = new ArrayList();
        private readonly BackgroundWorker WorkerBackground = new BackgroundWorker();

        public ImportarCompraConcluido()
        {
            InitializeComponent();
            Eventos();
        }

        private int idFornecedor { get; set; }

        private void SetDataNota()
        {
            var dadosFornecedor = dataNfe.GetNotas();
            if (dadosFornecedor.Count > 0)
                foreach (Controller.ImportarNfe item in dadosFornecedor)
                {
                    cnpj.Text = item.GetFornecedor().CPFcnpj;
                    IE.Text = item.GetFornecedor().IE;
                    razaosocial.Text = item.GetFornecedor().razaoSocial;

                    rua.Text = item.GetFornecedor().Addr_Rua + @" " + item.GetFornecedor().Addr_Nr;
                    bairro.Text = item.GetFornecedor().Addr_Bairro;
                    cep.Text = item.GetFornecedor().Addr_CEP;
                    cidade.Text = item.GetFornecedor().Addr_Cidade;
                    estado.Text = item.GetFornecedor().Addr_UF;
                }

            var dataNotas = dataNfe.GetNotas();
            foreach (Controller.ImportarNfe item in dataNotas)
            {
                Id.Text = item.GetDados().Id;
                Emissao.Text = item.GetDados().Emissao;
                Nr.Text = item.GetDados().Nr;
            }
        }

        private void SetTableProdutos()
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

        private void SetTableTitulos()
        {
            GridListaTitulos.ColumnCount = 3;
            GridListaTitulos.Columns[0].Name = "Forma de Pagamento";
            GridListaTitulos.Columns[0].Width = 120;

            GridListaTitulos.Columns[1].Name = "Data";
            GridListaTitulos.Columns[1].Width = 120;

            GridListaTitulos.Columns[2].Name = "Valor";
            GridListaTitulos.Columns[2].Width = 120;
            GridListaTitulos.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            foreach (dynamic item in ImportarPagamentos.titulos)
                GridListaTitulos.Rows.Add(
                    item.FormaPgto,
                    item.Data,
                    item.Valor
                );

            GridListaTitulos.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void AddProdutos()
        {
            foreach (dynamic item in ImportarProdutos.produtos)
            {
                if (item.Id != 0)
                    produtosID.Add(new
                    {
                        item.Id,
                        item.Referencia,
                        item.CodeBarras,
                        item.Nome,
                        item.Medida,
                        item.Estoque,
                        item.CategoriaId,
                        item.ValorCompra,
                        item.ValorVenda,
                        item.Fornecedor,
                        item.NCM,
                        item.EstoqueCompra
                    });

                idFornecedor = Validation.ConvertToInt32(item.Fornecedor);

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
                _mItem.Ncm = item.NCM;
                _mItem.id_sync = Validation.RandomSecurity();
                _mItem.status_sync = "CREATE";
                if (!_mItem.Save(_mItem, false))
                    continue;

                if (item.Id == 0)
                    produtosID.Add(new
                    {
                        Id = _mItem.GetLastId(),
                        item.Referencia,
                        item.CodeBarras,
                        item.Nome,
                        item.Medida,
                        item.Estoque,
                        item.CategoriaId,
                        item.ValorCompra,
                        item.ValorVenda,
                        item.Fornecedor,
                        item.NCM,
                        item.EstoqueCompra
                    });

                foreach (DataGridViewRow gridData in GridLista.Rows)
                    if ((int) gridData.Cells["Ordem"].Value == (int) item.Ordem)
                        gridData.Cells["Importado"].Value = new Bitmap(Resources.success16x);
            }
        }

        private void AddCompra()
        {
            _mPedido.Id = 0;
            _mPedido.Tipo = "Compras";
            _mPedido.Emissao = DateTime.Parse(Emissao.Text);
            _mPedido.Chavedeacesso = Id.Text;
            _mPedido.Cliente = idFornecedor;
            _mPedido.Colaborador = Settings.Default.user_id;
            //_mPedido.status = 1;
            if (!_mPedido.Save(_mPedido))
                return;

            foreach (dynamic item in produtosID)
            {
                _mPedidoItem.Id = 0;
                _mPedidoItem.Tipo = "Produtos";
                _mPedidoItem.Pedido = _mPedido.GetLastId();
                _mPedidoItem.CProd = item.Referencia;
                _mPedidoItem.CEan = item.CodeBarras;
                _mPedidoItem.xProd = item.Nome;
                _mPedidoItem.Ncm = item.NCM;
                _mPedidoItem.Item = item.Id;
                _mPedidoItem.ValorCompra = 0;
                _mPedidoItem.ValorVenda = item.ValorCompra;
                _mPedidoItem.Medida = item.Medida;
                _mPedidoItem.Quantidade = item.EstoqueCompra;
                _mPedidoItem.Total = item.EstoqueCompra * item.ValorCompra;
                _mPedidoItem.TotalVenda = item.EstoqueCompra * item.ValorCompra;
                _mPedidoItem.Save(_mPedidoItem, false);
                new Estoque(_mPedidoItem.GetLastId(), Home.pedidoPage, $"Importação de compra").Add().Item();
            }

            foreach (dynamic item in ImportarPagamentos.titulos)
            {
                _mTitulo.Id = 0;
                _mTitulo.Tipo = "Pagar";
                _mTitulo.Emissao = Validation.ConvertDateToSql(Emissao.Text);
                _mTitulo.Id_FormaPgto = Validation.ConvertToInt32(item.FormaPgto) == 15 ? 6 : Validation.ConvertToInt32(item.FormaPgto);
                _mTitulo.Id_Pedido = _mPedido.GetLastId();
                _mTitulo.Vencimento = item.Data;
                _mTitulo.Total = Validation.ConvertToDouble(item.Valor.Replace(".", ","));
                _mTitulo.Recebido = Validation.ConvertToDouble(item.Valor.Replace(".", ","));
                _mTitulo.Id_Pessoa = idFornecedor;
                _mTitulo.Obs = $"Pagamento gerado a partir da importação de compra. Chave de acesso: {item.id} | Número da nota: {item.nr}";
                _mTitulo.Save(_mTitulo, false);

                if (_mPedido.GetLastId() > 0)
                {
                    _mPedido.Id = _mPedido.GetLastId();
                    _mPedido.status = 1;
                    _mPedido.Save(_mPedido);
                }
            }

            var data = _mPedido.SaveTotais(_mPedidoItem.SumTotais(_mPedido.GetLastId()));
            _mPedido.Save(data);
        }

        private void Aguarde()
        {
            pictureBox4.Visible = true;
            btnImportar.Text = @"Aguarde...";
        }

        private void Eventos()
        {
            Load += (s, e) =>
            {
                SetDataNota();
                SetTableProdutos();
                SetTableTitulos();
            };

            btnImportar.Click += (s, e) =>
            {
                WorkerBackground.RunWorkerAsync();
                Aguarde();
            };

            WorkerBackground.DoWork += (s, e) =>
            {
                AddProdutos();
                AddCompra();
            };

            WorkerBackground.RunWorkerCompleted += (s, e) =>
            {
                pictureBox4.Visible = false;
                btnImportar.Text = @"Pronto";

                var Msg = "Importação concluída com sucesso.";
                var Title = "Pronto!";

                var result = AlertOptions.Message(Title, Msg, AlertBig.AlertType.warning, AlertBig.AlertBtn.OK);
                if (!result) return;

                Close();
                Application.OpenForms["ImportarNfe"]?.Close();
            };

            Back.Click += (s, e) => Close();
        }
    }
}