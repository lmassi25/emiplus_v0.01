using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class OpcoesCfeEmitir : Form
    {
        public static int idPedido { get; set; } // id pedido
        public static bool fecharTelas { get; set; }
        private Model.Nota _modelNota = new Model.Nota();
        private BackgroundWorker WorkerBackground = new BackgroundWorker();
        private string _msg;

        public OpcoesCfeEmitir()
        {
            InitializeComponent();
            Eventos();

            WorkerBackground.RunWorkerAsync();
        }

        /// <summary>
        /// Eventos do form
        /// </summary>
        public void Eventos()
        {
            using (var b = WorkerBackground)
            {
                b.DoWork += async (s, e) =>
                {
                    var checkNota = _modelNota.FindByIdPedidoAndTipo(idPedido, "CFe").FirstOrDefault<Model.Nota>();
                    if (checkNota == null)
                    {
                        _modelNota.Id = 0;
                        _modelNota.Tipo = "CFe";
                        _modelNota.Status = "Pendente";
                        _modelNota.id_pedido = idPedido;
                        _modelNota.Save(_modelNota, false);
                    }

                    _msg = new Controller.Fiscal().Emitir(idPedido, "CFe");
                };

                b.RunWorkerCompleted += async (s, e) =>
                {
                    label10.Text = _msg;

                    if (_msg.Contains("Emitido com sucesso"))
                    {
                        label10.Text = "Enviando impressão...";
                        Thread.Sleep(3000);

                        new Controller.Fiscal().Imprimir(idPedido, "CFe");
                        
                        if(fecharTelas)
                        {
                            Application.OpenForms["PedidoPagamentos"].Close();
                            AddPedidos.btnFinalizado = true;
                            Application.OpenForms["AddPedidos"].Close();
                            Close();
                        }
                    }
                };
            }
        }
    }
}
