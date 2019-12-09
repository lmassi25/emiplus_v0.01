﻿using Emiplus.Data.Helpers;
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

namespace Emiplus.View.Fiscal.TelasNota
{
    public partial class OpcoesNfeRapida : Form
    {
        public static int idPedido { get; set; }

        private Model.Nota _modelNota = new Model.Nota();
        private BackgroundWorker WorkerBackground = new BackgroundWorker();
        private string _msg;
        private int p1 = 0;

        public OpcoesNfeRapida()
        {
            InitializeComponent();
            Eventos();
        }

        private void Campos(Boolean enabled)
        {
            Emitir.Enabled = enabled;
            Imprimir.Enabled = enabled;
            EnviarEmail.Enabled = enabled;
            btnDetalhes.Enabled = enabled;
            CartaCorrecao.Enabled = enabled;
            Cancelar.Enabled = enabled;
        }

        public void Eventos()
        {
            btnDetalhes.Click += (s, e) =>
            {
                Nota.disableCampos = true;
                Nota.Id = idPedido;
                Nota nota = new Nota();
                nota.ShowDialog();
            };

            Emitir.Click += (s, e) =>
            {
                var checkNota = _modelNota.FindByIdPedido(idPedido).WhereNotNull("status").Where("nota.tipo", "NFe").FirstOrDefault();
                if (checkNota != null)
                {                    
                    Alert.Message("Atenção!", "Não é possível emitir uma nota Autorizada/Cancelada.", Alert.AlertType.warning);
                    return;
                }

                retorno.Text = "Emitindo NF-e .......................................... (1/2)";
                
                p1 = 1;
                Campos(false);
                WorkerBackground.RunWorkerAsync();
            };

            Imprimir.Click += (s, e) =>
            {
                var checkNota = _modelNota.FindByIdPedido(idPedido).Where("status", null).Where("nota.tipo", "NFe").FirstOrDefault();
                if (checkNota != null)
                {
                    Alert.Message("Opps!", "Emita a nota para imprimir.", Alert.AlertType.warning);
                    return;
                }

                p1 = 2;
                Campos(false);
                WorkerBackground.RunWorkerAsync();
            };

            using (var b = WorkerBackground)
            {
                b.DoWork += async (s, e) =>
                {
                    switch (p1)
                    {
                        case 1:
                            _modelNota = _modelNota.FindByIdPedido(idPedido).FirstOrDefault<Model.Nota>();
                            if (_modelNota == null)
                            {
                                _modelNota.Id = 0;
                                _modelNota.id_pedido = idPedido;
                                _modelNota.Save(_modelNota);
                            }

                            _msg = new Controller.Fiscal().Emitir(idPedido, "NFe");
                            break;
                        case 2:
                            var msg = new Controller.Fiscal().Imprimir(idPedido, "NFe");
                            if (!msg.Contains(".pdf"))
                                _msg = msg;
                            break;
                    }
                };

                b.RunWorkerCompleted += async (s, e) =>
                {
                    p1 = 0;
                    retorno.Text = _msg;
                    Campos(true);
                };
            }
        }
    }
}
