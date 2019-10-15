﻿using Emiplus.Data.Helpers;
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

        private void Eventos()
        {
            Load += (s, e) =>
            {
                if (Id > 0)
                {
                    // Id = 
                }
                else
                {
                    _mPedido.Id = Id;
                    _mPedido.Tipo = "Nota";
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
