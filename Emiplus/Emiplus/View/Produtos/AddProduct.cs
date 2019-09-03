using Emiplus.Data.Helpers;
using Emiplus.Model;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Windows.Forms;
using SqlKata.Execution;

namespace Emiplus.View.Produtos
{
    public partial class AddProduct : Form
    {
        private int idPdtSelecionado = Produtos.idPdtSelecionado;
        private Item _modelItem = new Item();

        public AddProduct()
        {
            InitializeComponent();
            
            if (idPdtSelecionado > 0)
                LoadData();
        }

        private void Start()
        {
            ActiveControl = nome;

            ToolHelp.Show("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s", pictureBox6, ToolHelp.ToolTipIcon.Info, "Ajuda!");

            var cat = new Model.Categoria().FindAll().WhereFalse("excluir").OrderByDesc("nome").Get();

            Categorias.DataSource = cat;
            Categorias.DisplayMember = "NOME";
            Categorias.ValueMember = "ID";
            Categorias.SelectedValue = _modelItem.Categoriaid;
        }

        private void LoadData()
        {
            _modelItem = _modelItem.FindById(idPdtSelecionado).First<Item>();

            nome.Text = _modelItem.Nome;
            referencia.Text = _modelItem.Referencia;
        }

        private void Label6_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Console.WriteLine(Support.BasePath());
            new Alert().Message("Title", "Mesasge", Alert.AlertType.error);
        }

        private void BtnHelp_Click(object sender, EventArgs e)
        {
            Support.OpenLinkBrowser("http://google.com");
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            _modelItem.Id = idPdtSelecionado;
            _modelItem.Nome = nome.Text;
            _modelItem.Referencia = referencia.Text;
            _modelItem.Categoriaid = (int)Categorias.SelectedValue;

            _modelItem.Save(_modelItem);
        }

        private void BtnRemover_Click(object sender, EventArgs e)
        {
            var data = _modelItem.Remove(idPdtSelecionado);
            if (data)
                Close();
        }

        private void AddProduct_Load(object sender, EventArgs e)
        {
            Start();
        }

        private void BtnEstoque_Click(object sender, EventArgs e)
        {
            AddEstoque f = new AddEstoque();
            f.ShowDialog();
        }
    }
}