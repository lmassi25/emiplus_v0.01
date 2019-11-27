using Emiplus.Data.Helpers;
using Emiplus.Model;
using SqlKata.Execution;
using System.Windows.Forms;

namespace Emiplus.View.Produtos
{
    public partial class AddEstoque : Form
    {
        private ItemEstoqueMovimentacao _modelItemEstoque = new ItemEstoqueMovimentacao();
        private Item _modelItem = new Item();
        private int IdItem = AddProduct.idPdtSelecionado;
        
        public AddEstoque()
        {
            InitializeComponent();
            Eventos();

            if (IdItem > 0)
            {
                var item = _modelItem.FindById(IdItem).First<Item>();
                tituloProduto.Text = item.Nome;
                estoqueAtual.Text = item.EstoqueAtual.ToString();
                custoAtual.Text = Validation.FormatPrice(item.ValorCompra);
            }
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

            btnCancelar.Click += (s, e) => Close();

            btnSalvar.Click += (s, e) =>
            {
                var item = _modelItem.FindById(IdItem).First<Item>();

                var tipo = btnRadioAddItem.Checked ? "A" : btnRadioRemoveItem.Checked ? "R" : "A";

                var data = _modelItemEstoque
                    .SetUsuario(0)
                    .SetQuantidade(Validation.ConvertToDouble(quantidade.Text))
                    .SetTipo(tipo)
                    .SetLocal("Cadastro de Produto")
                    .SetObs(obs.Text)
                    .SetItem(item)
                    .Save(_modelItemEstoque);

                if (data)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            };

            quantidade.KeyPress += (s, e) => Masks.MaskOnlyNumbers(s, e);
            obs.KeyPress += (s, e) => Masks.MaskOnlyNumberAndCharAndMore(s, e);

            quantidade.TextChanged += (s, e) =>
            {
                var item = _modelItem.FindById(IdItem).First<Item>();
                if (btnRadioAddItem.Checked)
                    novaQtd.Text = (Validation.ConvertToDouble(quantidade.Text) + item.EstoqueAtual).ToString();
                
                if (btnRadioRemoveItem.Checked)
                    novaQtd.Text = (Validation.ConvertToDouble(quantidade.Text) - item.EstoqueAtual).ToString();
            };

            btnRadioAddItem.Click += (s, e) =>
            {
                var item = _modelItem.FindById(IdItem).First<Item>();
                if (!string.IsNullOrEmpty(quantidade.Text))
                    novaQtd.Text = (item.EstoqueAtual + Validation.ConvertToDouble(quantidade.Text)).ToString();
            };

            btnRadioRemoveItem.Click += (s, e) =>
            {
                var item = _modelItem.FindById(IdItem).First<Item>();
                if (!string.IsNullOrEmpty(quantidade.Text))
                    novaQtd.Text = (item.EstoqueAtual - Validation.ConvertToDouble(quantidade.Text)).ToString();
            };
        }
    }
}
