using Emiplus.Data.Helpers;
using Emiplus.Properties;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace Emiplus.View.Produtos.TelasImportarNfe
{
    public partial class ImportarCompraConcluido : Form
    {
        private ImportarNfe dataNfe = new ImportarNfe();
        private ArrayList produtosID = new ArrayList();

        private Model.Item _mItem = new Model.Item();
        private Model.Pedido _mPedido = new Model.Pedido();
        private Model.PedidoItem _mPedidoItem = new Model.PedidoItem();
        private Model.Titulo _mTitulo = new Model.Titulo();

        private int idFornecedor { get; set; }

        public ImportarCompraConcluido()
        {
            InitializeComponent();
            Eventos();
        }

        private void SetDataNota()
        {
            var dadosFornecedor = dataNfe.GetNotas();
            if (dadosFornecedor.Count > 0)
            {
                foreach (Controller.ImportarNfe item in dadosFornecedor)
                {
                    cnpj.Text = item.GetFornecedor().CPFcnpj;
                    IE.Text = item.GetFornecedor().IE;
                    razaosocial.Text = item.GetFornecedor().razaoSocial;

                    rua.Text = item.GetFornecedor().Addr_Rua + " " + item.GetFornecedor().Addr_Nr;
                    bairro.Text = item.GetFornecedor().Addr_Bairro;
                    cep.Text = item.GetFornecedor().Addr_CEP;
                    cidade.Text = item.GetFornecedor().Addr_Cidade;
                    estado.Text = item.GetFornecedor().Addr_UF;
                }
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
            {
                GridListaTitulos.Rows.Add(
                    item.FormaPgto,
                    item.Data,
                    item.Valor
                );
            }

            GridListaTitulos.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void AddProdutos()
        {
            bool success = false;
            foreach (dynamic item in ImportarProdutos.produtos)
            {
                if (item.Id != 0)
                {
                    produtosID.Add(new
                    {
                        Id = item.Id,
                        Referencia = item.Referencia,
                        CodeBarras = item.CodeBarras,
                        Nome = item.Nome,
                        Medida = item.Medida,
                        Estoque = item.Estoque,
                        CategoriaId = item.CategoriaId,
                        ValorCompra = item.ValorCompra,
                        ValorVenda = item.ValorVenda,
                        Fornecedor = item.Fornecedor
                    });
                }

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
                if (_mItem.Save(_mItem, false))
                {
                    success = true;
                    if (item.Id == 0)
                    {
                        produtosID.Add(new
                        {
                            Id = _mItem.GetLastId(),
                            Referencia = item.Referencia,
                            CodeBarras = item.CodeBarras,
                            Nome = item.Nome,
                            Medida = item.Medida,
                            Estoque = item.Estoque,
                            CategoriaId = item.CategoriaId,
                            ValorCompra = item.ValorCompra,
                            ValorVenda = item.ValorVenda,
                            Fornecedor = item.Fornecedor
                        });
                    }

                    foreach (DataGridViewRow gridData in GridLista.Rows)
                    {
                        if ((int)gridData.Cells["Ordem"].Value == (int)item.Ordem)
                        {
                            gridData.Cells["Importado"].Value = new Bitmap(Properties.Resources.success16x);
                        }
                    }
                }
                else
                    success = false;
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
            if (_mPedido.Save(_mPedido))
            {
                foreach (dynamic item in produtosID)
                {
                    _mPedidoItem.Id = 0;
                    _mPedidoItem.Tipo = "Produtos";
                    _mPedidoItem.Pedido = _mPedido.GetLastId();
                    _mPedidoItem.Item = item.Id;
                    _mPedidoItem.ValorCompra = 0;
                    _mPedidoItem.ValorVenda = item.ValorCompra;
                    _mPedidoItem.Medida = item.Medida;
                    _mPedidoItem.Quantidade = item.Estoque;
                    _mPedidoItem.Total = item.Estoque * item.ValorCompra;
                    _mPedidoItem.Save(_mPedidoItem, false);
                }

                foreach (dynamic item in ImportarPagamentos.titulos)
                {
                    _mTitulo.Id = 0;
                    _mTitulo.Tipo = "Pagar";
                    _mTitulo.Emissao = Validation.ConvertDateToSql(Emissao.Text);
                    _mTitulo.Id_FormaPgto = Validation.ConvertToInt32(item.FormaPgto);
                    _mTitulo.Id_Pedido = _mPedido.GetLastId();
                    _mTitulo.Vencimento = item.Data;
                    _mTitulo.Total = Validation.ConvertToDouble(item.Valor.Replace(".", ","));
                    _mTitulo.Id_Pessoa = idFornecedor;
                    _mTitulo.Obs = "Pagamento gerado a partir de uma importação de compra.";
                    _mTitulo.Save(_mTitulo, false);
                }

                var data = _mPedido.SaveTotais(_mPedidoItem.SumTotais(_mPedido.GetLastId()));
                _mPedido.Save(data);
            }
        }

        private void Eventos()
        {
            Load += (s, e) =>
            {
                SetDataNota();
                SetTableProdutos();
                SetTableTitulos();
                AddProdutos();
                AddCompra();
            };

            Back.Click += (s, e) => Close();
        }
    }
}