using Emiplus.Properties;
using System;
using System.ComponentModel;
using System.Threading;
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

            if (OpcoesCfe.tipo == "NFCe")
            {
                pictureBox1.Image = Resources.nfce;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;                
            }

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
                    _msg = new Controller.Fiscal().Emitir(idPedido, OpcoesCfe.tipo == "NFCe" ? "NFCe" : "CFe");
                };

                b.RunWorkerCompleted += async (s, e) =>
                {
                    label10.Text = _msg;

                    if (_msg.Contains("Emitido com sucesso"))
                    {
                        label10.Text = "Enviando impressão...";
                        Thread.Sleep(3000);

                        new Controller.Fiscal().Imprimir(idPedido, OpcoesCfe.tipo == "NFCe" ? "NFCe" : "CFe");

                        if (fecharTelas)
                        {
                            try
                            {
                                Application.OpenForms["PedidoPagamentos"].Close();
                                AddPedidos.btnFinalizado = true;
                                Application.OpenForms["AddPedidos"].Close();
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                        }

                        Close();
                    }
                };
            }
        }
    }
}