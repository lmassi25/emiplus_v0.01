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

namespace Emiplus.View.Comercial
{
    public partial class PedidoModalCancelItem : Form
    {
        private int IdItem = AddPedidos.IdItem;
        public PedidoModalCancelItem()
        {
            InitializeComponent();
        }

        private void CancelItem()
        {
            if (IdItem > 0)
            {
                Model.PedidoItem item = new Model.PedidoItem();
                //var data = item.FindById(IdItem).Where("excluir", 0).First();

                item.Id = IdItem;
                item.Remove(IdItem);

                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
                case Keys.Enter:
                    CancelItem();
                    break;
            }
        }

        private void BtnContinuar_Click(object sender, EventArgs e)
        {
            CancelItem();
        }
    }
}
