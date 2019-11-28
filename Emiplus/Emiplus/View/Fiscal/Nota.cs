using Emiplus.Data.Helpers;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Emiplus.View.Fiscal
{
    public partial class Nota : Form
    {
        public static int Id { get; set; } // id nota
        public static int IdDetailsNota { get; set; }

        private Model.Pedido _mPedido = new Model.Pedido();
        private Model.Nota _mNota = new Model.Nota();

        public Nota()
        {
            InitializeComponent();
            Eventos();
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

            Load += (s, e) =>
            {
                if (Id > 0)
                {
                    // Id = 
                }
                else
                {
                    _mPedido.Id = Id;
                    _mPedido.Tipo = "Notas";
                    _mPedido.Emissao = DateTime.Now;
                    _mPedido.Saida = DateTime.Now;
                    if (_mPedido.Save(_mPedido))
                    {
                        Id = _mPedido.GetLastId();

                        _mNota.Id = 0;
                        _mNota.id_pedido = Id;
                        _mNota.Save(_mNota, false);

                        IdDetailsNota = _mNota.GetLastId();
                    }
                    else
                    {
                        Alert.Message("Opss", "Erro ao criar Nota.", Alert.AlertType.error);
                        Close();
                    }
                }

                OpenForm.ShowInPanel<TelasNota.TelaDados>(panelTelas);
            };
        }
    }
}
