using Emiplus.Data.Helpers;
using Emiplus.View.Common;
using Emiplus.View.Fiscal.TelasNota;
using SqlKata.Execution;
using System;
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
                if (_mPedido.Cliente == 0)
                    TelaDados.telaDados = false;

                Resolution.SetScreenMaximized(this);

                if (Id > 0)
                {
                    _mNota = _mNota.FindById(Id).FirstOrDefault<Model.Nota>();
                    _mPedido = _mPedido.FindById(_mNota.id_pedido).FirstOrDefault<Model.Pedido>();
                }
                else
                {
                    if (OpcoesNfeRapida.idPedido > 0)
                    {
                        _mNota.Id = 0;
                        _mNota.id_pedido = OpcoesNfeRapida.idPedido;
                        _mNota.Tipo = "NFe";
                        _mNota.Status = "Pendente";
                        _mNota.Save(_mNota, false);
                        Id = _mNota.GetLastId();
                    }
                    else
                    {
                        _mPedido.Id = 0;
                        _mPedido.Tipo = "NFe";
                        _mPedido.Save(_mPedido);

                        _mNota.Id = 0;
                        _mNota.id_pedido = _mPedido.GetLastId();
                        _mNota.Tipo = "NFe";
                        _mNota.Status = "Pendente";
                        _mNota.Save(_mNota, false);
                        Id = _mNota.GetLastId();
                    }
                }

                OpenForm.ShowInPanel<TelasNota.TelaDados>(panelTelas);
            };

            FormClosing += (s, e) =>
            {
                OpcoesNfeRapida.idPedido = 0;

                _mNota = new Model.Nota().FindById(Id).FirstOrDefault<Model.Nota>();

                if (_mNota == null)
                    Close();

                if (_mPedido != null)
                {
                    if (_mNota.id_pedido > 0)
                    {
                        _mPedido = _mPedido.FindById(_mNota.id_pedido).FirstOrDefault<Model.Pedido>();
                    }
                }
                    
                if (!TelaDados.telaDados)
                {
                    var result = AlertOptions.Message("Atenção!", "Você está prestes a excluir! Deseja continuar?", AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                    if (result)
                    {
                        //if (Id > 0)
                        //{
                        //    _mPedido.Excluir = 1;
                        //    if (_mPedido.Save(_mPedido))
                        //    {
                        //        _mNota.Excluir = 1;
                        //        _mNota.Save(_mNota, false);
                        //    }

                        //    TelaDados.telaDados = true;                            
                        //    Close();
                        //}

                        if (_mPedido != null)
                        {
                            if (_mPedido.Tipo == "NFe")
                            {
                                _mPedido.Excluir = 1;
                                _mPedido.Save(_mPedido);
                            }
                        }

                        if (_mNota != null)
                        {
                            _mNota.Excluir = 1;
                            _mNota.id_pedido = 0;
                            _mNota.Save(_mNota, false);
                        }
                    }
                }
            };
        }
    }
}