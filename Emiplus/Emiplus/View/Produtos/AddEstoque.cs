using Emiplus.Data.Helpers;
using Emiplus.Model;
using SqlKata.Execution;
using System;
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
            }
        }

        private void Eventos()
        {
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
        }
    }
}
