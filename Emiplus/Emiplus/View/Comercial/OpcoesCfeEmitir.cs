using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emiplus.Properties;

namespace Emiplus.View.Comercial
{
    public partial class OpcoesCfeEmitir : Form
    {
        private readonly BackgroundWorker _workerBackground = new BackgroundWorker();
        private string _msg;

        public OpcoesCfeEmitir()
        {
            InitializeComponent();
            Eventos();
        }

        public static int idPedido { get; set; } // id pedido
        public static bool fecharTelas { get; set; }

        public void Eventos()
        {
            Load += (s, e) =>
            {
                if (OpcoesCfe.tipo == "NFCe")
                {
                    pictureBox1.Image = Resources.nfce;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
            };

            Shown += (s, e) => _workerBackground.RunWorkerAsync();

            using (var b = _workerBackground)
            {
                b.DoWork += (s, e) =>
                {
                    _msg = new Controller.Fiscal().Emitir(idPedido, OpcoesCfe.tipo == "NFCe" ? "NFCe" : "CFe");
                };

                b.RunWorkerCompleted += (s, e) =>
                {
                    label10.Text = _msg;

                    if (_msg.Contains("Emitido com sucesso"))
                    {
                        label10.Text = @"Enviando impressão...";
                        Task.Delay(2000);

                        new Controller.Fiscal().Imprimir(idPedido, OpcoesCfe.tipo == "NFCe" ? "NFCe" : "CFe");

                        if (fecharTelas)
                        {
                            Application.OpenForms["PedidoPagamentos"]?.Close();
                            AddPedidos.BtnFinalizado = true;
                            Application.OpenForms["AddPedidos"]?.Close();
                        }

                        Close();
                    }
                };
            }
        }
    }
}