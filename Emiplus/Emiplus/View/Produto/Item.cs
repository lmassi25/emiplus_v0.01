using System;
using System.Windows.Forms;

namespace Emiplus.View.Item
{    
    public partial class Item : Form
    {
        private int _itemId;
                
        private Model.Item _item;
        private Controller.Item _controller;

        public Item(int id = 0)
        {
            InitializeComponent();

            _item = new Model.Item();
            _controller = new Controller.Item();

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

            _controller.bSalvar(_item);

            button1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;

            GetData();

            button2.Enabled = true;
        }
    }
}
