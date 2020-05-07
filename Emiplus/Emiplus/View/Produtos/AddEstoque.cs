using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.Properties;
using SqlKata.Execution;

namespace Emiplus.View.Produtos
{
    public partial class AddEstoque : Form
    {
        private readonly Item _modelItem = new Item();
        private readonly ItemEstoqueMovimentacao _modelItemEstoque = new ItemEstoqueMovimentacao();
        private readonly int IdItem = AddProduct.idPdtSelecionado;

        public AddEstoque()
        {
            InitializeComponent();
            Eventos();

            if (IdItem > 0)
            {
                var item = _modelItem.FindById(IdItem).First<Item>();

                tituloProduto.Text = item.Nome;
                estoqueAtual.Text =
                    Validation.FormatMedidas(item.Medida, Validation.ConvertToDouble(item.EstoqueAtual));
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
            Masks.SetToUpper(this);

            btnSalvar.Click += (s, e) =>
            {
                var item = _modelItem.FindById(IdItem).FirstOrDefault<Item>();
                if (item != null)
                {
                    var tipo = btnRadioAddItem.Checked ? "A" : btnRadioRemoveItem.Checked ? "R" : "A";

                    var data = _modelItemEstoque
                        .SetUsuario(Settings.Default.user_id)
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
                }
            };

            quantidade.KeyPress += (s, e) => Masks.MaskDouble(s, e);
            obs.KeyPress += (s, e) => Masks.MaskOnlyNumberAndCharAndMore(s, e);

            quantidade.TextChanged += (s, e) =>
            {
                var item = _modelItem.FindById(IdItem).First<Item>();
                if (btnRadioAddItem.Checked)
                    novaQtd.Text = Validation.FormatMedidas(item.Medida,
                        item.EstoqueAtual + Validation.ConvertToDouble(quantidade.Text));

                if (btnRadioRemoveItem.Checked)
                    novaQtd.Text = Validation.FormatMedidas(item.Medida,
                        item.EstoqueAtual - Validation.ConvertToDouble(quantidade.Text));
            };

            btnRadioAddItem.Click += (s, e) =>
            {
                var item = _modelItem.FindById(IdItem).First<Item>();
                if (!string.IsNullOrEmpty(quantidade.Text))
                    novaQtd.Text = Validation.FormatMedidas(item.Medida,
                        item.EstoqueAtual + Validation.ConvertToDouble(quantidade.Text));
            };

            btnRadioRemoveItem.Click += (s, e) =>
            {
                var item = _modelItem.FindById(IdItem).First<Item>();
                if (!string.IsNullOrEmpty(quantidade.Text))
                    novaQtd.Text = Validation.FormatMedidas(item.Medida,
                        item.EstoqueAtual - Validation.ConvertToDouble(quantidade.Text));
            };
        }
    }
}