using Emiplus.Data.Helpers;
using Emiplus.Data.SobreEscrever;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Produtos
{
    public partial class Etiquetas : Form
    {

        private Model.Item _mItem = new Model.Item();

        KeyedAutoCompleteStringCollection collection = new KeyedAutoCompleteStringCollection();

        public Etiquetas()
        {
            InitializeComponent();
            Eventos();
        }

        private void LoadItens()
        {

        }

        /// <summary>
        /// Autocomplete do campo de busca de produtos.
        /// </summary>
        private void AutoCompleteItens()
        {
            var item = _mItem.Query().Select("id", "nome").Where("excluir", 0).Where("tipo", "Produtos").Get();

            foreach (var itens in item)
            {
                collection.Add(itens.NOME, itens.ID);
            }

            BuscarProduto.AutoCompleteCustomSource = collection;
        }

        private void AddItem()
        {
            if (collection.Lookup(BuscarProduto.Text) > 0)
            {
                var itemId = collection.Lookup(BuscarProduto.Text);
                Model.Item item = _mItem.FindById(itemId).Where("excluir", 0).Where("tipo", "Produtos").First<Model.Item>();

                Model.Etiqueta etiqueta = new Model.Etiqueta();
                etiqueta.id_item = item.Id;
                etiqueta.linha = 0;
                etiqueta.quantidade = Validation.ConvertToInt32(Quantidade.Text);
                etiqueta.coluna = 0;
                etiqueta.Save(etiqueta);
            }
        }

        /// <summary>
        /// Limpa os input text.
        /// </summary>
        private void ClearForms()
        {
            BuscarProduto.Clear();
            Quantidade.Clear();
        }

        /// <summary>
        /// Adiciona os eventos nos Controls do form.
        /// </summary>
        private void Eventos()
        {

            Load += (s, e) =>
            {
                AutoCompleteItens();
            };

            addProduto.Click += (s, e) =>
            {
                AddItem();

                // Limpa os campos
                ClearForms();
            };

            imprimir.Click += (s, e) => {

            };

            label5.Click += (s, e) => Close();
            btnExit.Click += (s, e) => Close();
        }
    }
}