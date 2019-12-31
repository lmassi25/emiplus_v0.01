using Emiplus.Data.Helpers;
using Emiplus.View.Common;
using Emiplus.View.Fiscal.TelasNota;
using SqlKata.Execution;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Emiplus.View.Fiscal
{
    public partial class Nota : Form
    {
        public static int Id { get; set; } // id nota
        public static int IdDetailsNota { get; set; }
        
        /// <summary>
        /// True - Desabilita os campos na tela da Nota
        /// </summary>
        public static bool disableCampos { get; set; }

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
                if(_mPedido.Cliente == 0)
                    TelaDados.telaDados = false;

                Resolution.SetScreenMaximized(this);

                if (Id > 0)
                {
                    //_mPedido = _mPedido.FindById(Id).FirstOrDefault<Model.Pedido>();
                    //_mNota = _mNota.FindByIdPedidoAndTipo(Id, "NFe").FirstOrDefault<Model.Nota>();
                }
                else
                {
                    _mPedido.Id = Id;
                    _mPedido.Tipo = "NFe";
                    _mPedido.Emissao = DateTime.Now;
                    _mPedido.Saida = DateTime.Now;
                    if (_mPedido.Save(_mPedido))
                    {
                        Id = _mPedido.GetLastId();

                        _mNota.Id = 0;
                        _mNota.id_pedido = Id;
                        _mNota.Tipo = "NFe";
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

            FormClosing += (s, e) =>
            {
                _mPedido = _mPedido.FindById(Id).FirstOrDefault<Model.Pedido>();
                _mNota = _mNota.FindByIdPedidoAndTipo(Id, "NFe").FirstOrDefault<Model.Nota>();

                if (_mPedido.Cliente == 0 && TelaDados.telaDados != true)
                    TelaDados.telaDados = false;
                else
                    TelaDados.telaDados = true;

                if (!TelaDados.telaDados)
                {
                    var result = AlertOptions.Message("Atenção!", "Você está prestes a excluir! Deseja continuar?", AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                    if (result)
                    {
                        if(Id > 0)
                        {
                            _mPedido.Excluir = 1;
                            if (_mPedido.Save(_mPedido))
                            {
                                _mNota.Excluir = 1;
                                _mNota.Save(_mNota, false);                               
                            }

                            TelaDados.telaDados = true;
                            Close();
                        }
                    }
                }
            };
        }
    }
}
