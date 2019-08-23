using System;
using System.Diagnostics;
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;
using Emiplus.Data.Database;
using Emiplus.Data.GenericRepository;
using Emiplus.Data.Helpers;

namespace Emiplus.View.Item
{
    public partial class Item : Form
    {
        private int _itemId;

        private Model.Item _item;
        private Controller.Item _controller;
        private BaseService<Model.Item> _camadaservico;


        public Item(int id = 0)
        {
            InitializeComponent();

            _item = new Model.Item();
            _controller = new Controller.Item();
            _camadaservico = new BaseService<Model.Item>();

            _itemId = id;

            if (_itemId > 0)
            {
                _item = _controller.GetItem(_itemId);
            }
        }

        private void GetData()
        {
            textBox1.Text = "";
            textBox1.Text = _item.Nome;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;

            _item.Nome = textBox1.Text;
            _controller.Salvar(_item);

            button1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;

            GetData();

            button2.Enabled = true;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            new Alert().Message("teste", "testeee", Alert.AlertType.error);
            //new Log().Adicionar("Item", "mensagem de log", Data.Helpers.Log.LogType.warning);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            button5.Enabled = false;

            _camadaservico.Add(new Model.Item { Nome = textBox1.Text});

            button5.Enabled = true;
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            button6.Enabled = false;

            var _item_teste = new Model.Item();
            _item_teste = _camadaservico.Find(3);

            MessageBox.Show(_item_teste.Nome);

            button6.Enabled = true;
        }
    }
}
