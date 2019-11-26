using Emiplus.Data.Helpers;
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

        public OpcoesNfeRapida()
        {
            InitializeComponent();
            Eventos();
        }

        public void Eventos()
        {
            btnDetalhes.Click += (s, e) =>
            {
                Nota.Id = idPedido;
                Nota nota = new Nota();
                nota.ShowDialog();
            };

            Emitir.Click += (s, e) =>
            {
                var checkNota = _modelNota.FindByIdPedido(idPedido).Where("status", null).FirstOrDefault();
                if (checkNota == null)
                {
                    Alert.Message("Atenção!", "Não é possível emitir uma nota Autorizada/Cancelada.", Alert.AlertType.warning);
                    return;
                }

                retorno.Text = "Emitindo NF-e .......................................... (1/2)";
                Emitir.Enabled = false;
                WorkerBackground.RunWorkerAsync();
            };

            Imprimir.Click += (s, e) =>
            {
                var checkNota = _modelNota.FindByIdPedido(idPedido).Where("status", null).FirstOrDefault();
                if (checkNota != null)
                {
                    Alert.Message("Opps!", "Emita a nota para imprimir.", Alert.AlertType.warning);
                    return;
                }

                retorno.Text = new Controller.Fiscal().Imprimir(idPedido, "NFe");
            };

            using (var b = WorkerBackground)
            {
                b.DoWork += async (s, e) =>
                {
                    _modelNota = _modelNota.FindByIdPedido(idPedido).FirstOrDefault<Model.Nota>();
                    if (_modelNota == null)
                    {
                        _modelNota.Id = 0;
                        _modelNota.id_pedido = idPedido;
                        _modelNota.Save(_modelNota);
                    }

                    _msg = new Controller.Fiscal().Emitir(idPedido, "NFe");
                };

                b.RunWorkerCompleted += async (s, e) =>
                {
                    retorno.Text = _msg;
                    Emitir.Enabled = true;
                };
            }
        }
    }
}
