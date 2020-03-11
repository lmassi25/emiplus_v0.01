using Emiplus.Data.Helpers;
using Emiplus.View.Comercial;
using SqlKata.Execution;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Emiplus.View.Fiscal.TelasNota
{
    public partial class TelaPagamento : Form
    {
        private int IdPedido { get; set; }

        //private Model.Item _mItem = new Model.Item();
        //private Model.PedidoItem _mPedidoItens = new Model.PedidoItem();
        //private Model.Pessoa _mCliente = new Model.Pessoa();
        private Model.Titulo _mTitulo = new Model.Titulo();
        private Model.Pedido _mPedido = new Model.Pedido();

        private Controller.Titulo _controllerTitulo = new Controller.Titulo();
        private Model.Nota _mNota = new Model.Nota();

        MaskedTextBox mtxt = new MaskedTextBox();
        TextBox mtxt2 = new TextBox();

        public TelaPagamento()
        {
            InitializeComponent();
            
            _mNota = new Model.Nota().FindById(Nota.Id).FirstOrDefault<Model.Nota>();
            if (_mNota == null)
            {
                Alert.Message("Ação não permitida", "Referência de Pedido não identificada", Alert.AlertType.warning);
                return;
            }

            IdPedido = _mNota.id_pedido;

            DisableCampos();
            Eventos();

            TelaReceber.Visible = false;
        }

        private void DisableCampos()
        {
            if (Nota.disableCampos)
            {
                Dinheiro.Enabled = false;
                Cheque.Enabled = false;
                Debito.Enabled = false;
                Credito.Enabled = false;
                Crediario.Enabled = false;
                Boleto.Enabled = false;
                Desconto.Enabled = false;
                Acrescimo.Enabled = false;
            }
        }

        public void AtualizarDados(Boolean grid = true)
        {
            Dinheiro.Select();

            _mPedido = _mPedido.FindById(IdPedido).FirstOrDefault<Model.Pedido>();

            if (grid)
                _controllerTitulo.GetDataTableTitulos(GridListaFormaPgtos, IdPedido);

            dynamic devolucoes = _mPedido.Query()
                .SelectRaw("SUM(total) as total")
                .Where("excluir", 0)
                .Where("tipo", "Devoluções")
                .Where("Venda", IdPedido)
                .FirstOrDefault<Model.Pedido>();

            acrescimos.Text = Validation.FormatPrice(_controllerTitulo.GetTotalFrete(IdPedido), true);
            discount.Text = Validation.FormatPrice((_controllerTitulo.GetTotalDesconto(IdPedido) + Validation.ConvertToDouble(devolucoes.Total ?? 0)), true);
            troco.Text = Validation.FormatPrice(_controllerTitulo.GetTroco(IdPedido), true).Replace("-", "");
            pagamentos.Text = Validation.FormatPrice(_controllerTitulo.GetLancados(IdPedido), true);
            total.Text = Validation.FormatPrice(_controllerTitulo.GetTotalPedido(IdPedido), true);

            var aPagar = Validation.RoundTwo(_controllerTitulo.GetTotalPedido(IdPedido) - _controllerTitulo.GetLancados(IdPedido));
            if (_controllerTitulo.GetLancados(IdPedido) < _controllerTitulo.GetTotalPedido(IdPedido))
            {
                aPagartxt.Text = Validation.FormatPrice(aPagar, true);
            }
            else
            {
                aPagartxt.Text = "R$ 0,00";
            }

            if (_controllerTitulo.GetLancados(IdPedido) > 0)
                Desconto.Enabled = false;
            else
                Desconto.Enabled = true;

            if (aPagar <= 0)
            {
                label15.BackColor = Color.FromArgb(46, 204, 113);
                aPagartxt.BackColor = Color.FromArgb(46, 204, 113);
                visualPanel1.BackColorState.Enabled = Color.FromArgb(46, 204, 113);
                visualPanel1.Border.Color = Color.FromArgb(39, 192, 104);
                Refresh();
            }
            else
            {
                label15.BackColor = Color.FromArgb(255, 40, 81);
                aPagartxt.BackColor = Color.FromArgb(255, 40, 81);
                visualPanel1.BackColorState.Enabled = Color.FromArgb(255, 40, 81);
                visualPanel1.Border.Color = Color.FromArgb(241, 33, 73);
                Refresh();
            }
        }

        private void bSalvar()
        {
            switch (lTipo.Text)
            {
                case "Dinheiro":
                    _controllerTitulo.AddPagamento(IdPedido, 1, valor.Text, iniciar.Text);
                    break;

                case "Cheque":
                    _controllerTitulo.AddPagamento(IdPedido, 2, valor.Text, iniciar.Text, parcelas.Text);
                    break;

                case "Cartão de Débito":
                    _controllerTitulo.AddPagamento(IdPedido, 3, valor.Text, iniciar.Text);
                    break;

                case "Cartão de Crédito":
                    _controllerTitulo.AddPagamento(IdPedido, 4, valor.Text, iniciar.Text, parcelas.Text);
                    break;

                case "Crediário":
                    _controllerTitulo.AddPagamento(IdPedido, 5, valor.Text, iniciar.Text, parcelas.Text);
                    break;

                case "Boleto":
                    _controllerTitulo.AddPagamento(IdPedido, 6, valor.Text, iniciar.Text, parcelas.Text);
                    break;
            }

            TelaReceber.Visible = false;

            AtualizarDados();
        }

        private void Campos(int tipo = 0)
        {
            valor.Text = "";
            parcelas.Text = "";
            iniciar.Text = "";

            if (tipo == 1)
            {
                label9.Visible = false;
                Info1.Visible = false;
                parcelas.Visible = false;

                label8.Visible = false;
                Info2.Visible = false;
                iniciar.Visible = false;
            }
            else
            {
                label9.Visible = true;
                Info1.Visible = true;
                parcelas.Visible = true;

                label8.Visible = true;
                Info2.Visible = true;
                iniciar.Visible = true;
            }

            valor.Text = Validation.FormatPrice(_controllerTitulo.GetRestante(IdPedido));
        }

        private void JanelasRecebimento(string formaPgto)
        {
            TelaReceber.Visible = true;
            lTipo.Text = formaPgto;
            valor.Select();

            if (formaPgto == "Cartão de Débito" || formaPgto == "Dinheiro")
            {
                Campos(1);
                return;
            }

            Campos(0);
        }

        private void JanelaDesconto()
        {
            PedidoPayDesconto.idPedido = IdPedido;
            using (PedidoPayDesconto Desconto = new PedidoPayDesconto())
            {
                Desconto.TopMost = true;
                if (Desconto.ShowDialog() == DialogResult.OK)
                    AtualizarDados();
            }
        }

        private void JanelaAcrescimo()
        {
            PedidoPayAcrescimo.idPedido = IdPedido;
            using (PedidoPayAcrescimo Acrescimo = new PedidoPayAcrescimo())
            {
                Acrescimo.TopMost = true;
                if (Acrescimo.ShowDialog() == DialogResult.OK)
                    AtualizarDados();
            }
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    bSalvar();
                    break;

                case Keys.F1:
                    JanelasRecebimento("Dinheiro");
                    break;

                case Keys.F2:
                    JanelasRecebimento("Cheque");
                    break;

                case Keys.F3:
                    JanelasRecebimento("Cartão de Débito");
                    break;

                case Keys.F4:
                    JanelasRecebimento("Cartão de Crédito");
                    break;

                case Keys.F5:
                    JanelasRecebimento("Crediário");
                    break;

                case Keys.F6:
                    JanelasRecebimento("Boleto");
                    break;

                case Keys.F7:
                    JanelaDesconto();
                    break;

                case Keys.F8:
                    JanelaAcrescimo();
                    break;

                case Keys.F9:
                    //TelaPagamentos();
                    break;

                case Keys.F10:
                    //TelaPagamentos();
                    break;

                case Keys.F11:
                    //TelaPagamentos();
                    break;

                case Keys.F12:
                    //TelaPagamentos();
                    break;

                case Keys.Escape:
                    TelaReceber.Visible = false;
                    break;
            }
        }

        /// <summary>
        /// Eventos do form
        /// </summary>
        public void Eventos()
        {
            KeyDown += KeyDowns;
            Dinheiro.KeyDown += KeyDowns;
            Cheque.KeyDown += KeyDowns;
            Debito.KeyDown += KeyDowns;
            Credito.KeyDown += KeyDowns;
            Crediario.KeyDown += KeyDowns;
            Boleto.KeyDown += KeyDowns;
            Desconto.KeyDown += KeyDowns;
            Acrescimo.KeyDown += KeyDowns;

            btnSalvar.KeyDown += KeyDowns;
            btnCancelar.KeyDown += KeyDowns;
            valor.KeyDown += KeyDowns;
            parcelas.KeyDown += KeyDowns;
            iniciar.KeyDown += KeyDowns;

            Load += (s, e) =>
            {
                mtxt.Visible = false;
                mtxt2.Visible = false;

                GridListaFormaPgtos.Controls.Add(mtxt);
                GridListaFormaPgtos.Controls.Add(mtxt2);

                AtualizarDados();

                if (_mNota.Status != "Pendente")
                {
                    progress5.Visible = false;
                    pictureBox1.Visible = false;
                    label13.Visible = false;
                    Next.Visible = false;
                }
            };

            Debito.Click += (s, e) => JanelasRecebimento("Cartão de Débito");
            Credito.Click += (s, e) => JanelasRecebimento("Cartão de Crédito");
            Dinheiro.Click += (s, e) => JanelasRecebimento("Dinheiro");
            Boleto.Click += (s, e) => JanelasRecebimento("Boleto");
            Crediario.Click += (s, e) => JanelasRecebimento("Crediário");
            Cheque.Click += (s, e) => JanelasRecebimento("Cheque");

            Desconto.Click += (s, e) => JanelaDesconto();
            Acrescimo.Click += (s, e) => JanelaAcrescimo();

            btnSalvar.Click += (s, e) => bSalvar();
            btnCancelar.Click += (s, e) => TelaReceber.Visible = false;

            iniciar.KeyPress += (s, e) => Masks.MaskBirthday(s, e);
            iniciar.KeyPress += (s, e) => Masks.MaskBirthday(s, e);
            valor.KeyPress += (s, e) => Masks.MaskDouble(s, e);

            valor.TextChanged += (s, e) =>
            {
                TextBox txt = (TextBox)s;
                Masks.MaskPrice(ref txt);
            };

            GridListaFormaPgtos.CellDoubleClick += (s, e) =>
            {
                if (GridListaFormaPgtos.Columns[e.ColumnIndex].Name == "colExcluir")
                {
                    Console.WriteLine(GridListaFormaPgtos.CurrentRow.Cells[4].Value);
                    if (Convert.ToString(GridListaFormaPgtos.CurrentRow.Cells[4].Value) != "")
                    {
                        int id = Validation.ConvertToInt32(GridListaFormaPgtos.CurrentRow.Cells[0].Value);
                        _mTitulo.Remove(id);
                        AtualizarDados();
                    }
                }
            };

            btnClearRecebimentos.Click += (s, e) =>
            {
                foreach (DataGridViewRow row in GridListaFormaPgtos.Rows)
                    if (Convert.ToString(row.Cells[0].Value) != "")
                        _mTitulo.Remove(Validation.ConvertToInt32(row.Cells[0].Value), "ID", false);

                AtualizarDados();
            };

            Next.Click += (s, e) => OpenForm.Show<TelaFinal>(this);

            Back.Click += (s, e) => Close();
            
            mtxt2.TextChanged += (s, e) =>
            {
                TextBox txt = (TextBox)s;
                Masks.MaskPrice(ref txt);
            };

            GridListaFormaPgtos.CellBeginEdit += (s, e) =>
            {
                if (e.ColumnIndex == 2)
                {
                    //-----mtxt

                    mtxt.Mask = "##/##/####";

                    Rectangle rec = GridListaFormaPgtos.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    mtxt.Location = rec.Location;
                    mtxt.Size = rec.Size;
                    mtxt.Text = "";

                    if (GridListaFormaPgtos[e.ColumnIndex, e.RowIndex].Value != null)
                        mtxt.Text = GridListaFormaPgtos[e.ColumnIndex, e.RowIndex].Value.ToString();

                    mtxt.Visible = true;
                }

                if (e.ColumnIndex == 3)
                {
                    //-----mtxt2

                    Rectangle rec = GridListaFormaPgtos.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    mtxt2.Location = rec.Location;
                    mtxt2.Size = rec.Size;
                    mtxt2.Text = "";

                    if (GridListaFormaPgtos[e.ColumnIndex, e.RowIndex].Value != null)
                        mtxt2.Text = GridListaFormaPgtos[e.ColumnIndex, e.RowIndex].Value.ToString();

                    mtxt2.Visible = true;
                }
            };

            GridListaFormaPgtos.CellEndEdit += (s, e) =>
            {
                if (mtxt.Visible)
                {
                    GridListaFormaPgtos.CurrentCell.Value = mtxt.Text;
                    mtxt.Visible = false;
                }

                if (mtxt2.Visible)
                {
                    GridListaFormaPgtos.CurrentCell.Value = mtxt2.Text;
                    mtxt2.Visible = false;
                }

                int ID = Validation.ConvertToInt32(GridListaFormaPgtos.Rows[e.RowIndex].Cells["colID"].Value);

                if (ID == 0)
                    return;

                var titulo = new Model.Titulo().FindById(ID).FirstOrDefault<Model.Titulo>();

                if (titulo == null)
                    return;

                DateTime parsed;
                if (DateTime.TryParse(GridListaFormaPgtos.Rows[e.RowIndex].Cells["Column1"].Value.ToString(), out parsed))
                    titulo.Vencimento = Validation.ConvertDateToSql(GridListaFormaPgtos.Rows[e.RowIndex].Cells["Column1"].Value);
                else
                    GridListaFormaPgtos.Rows[e.RowIndex].Cells["Column1"].Value = Validation.ConvertDateToForm(titulo.Vencimento);

                titulo.Total = Validation.ConvertToDouble(GridListaFormaPgtos.Rows[e.RowIndex].Cells["Column3"].Value);
                titulo.Recebido = Validation.ConvertToDouble(GridListaFormaPgtos.Rows[e.RowIndex].Cells["Column3"].Value);

                if (titulo.Save(titulo, false))
                {
                    //_controllerTitulo.GetDataTableTitulos(GridListaFormaPgtos, IdPedido); 
                    Alert.Message("Pronto!", "Recebimento atualizado com sucesso.", Alert.AlertType.success);
                    AtualizarDados(false);
                }
                else
                    Alert.Message("Opsss!", "Algo deu errado ao atualizar o recebimento.", Alert.AlertType.error);
            };

            GridListaFormaPgtos.Scroll += (s, e) =>
            {
                if (mtxt.Visible)
                {
                    Rectangle rec = GridListaFormaPgtos.GetCellDisplayRectangle(GridListaFormaPgtos.CurrentCell.ColumnIndex, GridListaFormaPgtos.CurrentCell.RowIndex, true);
                    mtxt.Location = rec.Location;
                }

                if (mtxt2.Visible)
                {
                    Rectangle rec = GridListaFormaPgtos.GetCellDisplayRectangle(GridListaFormaPgtos.CurrentCell.ColumnIndex, GridListaFormaPgtos.CurrentCell.RowIndex, true);
                    mtxt2.Location = rec.Location;
                }
            };
        }
    }
}