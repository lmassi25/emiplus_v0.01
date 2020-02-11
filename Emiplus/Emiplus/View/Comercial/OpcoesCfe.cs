using Emiplus.Data.Helpers;
using Emiplus.Properties;
using SqlKata.Execution;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class OpcoesCfe : Form
    {
        public static int idPedido { get; set; } // id pedido
        public static int idNota { get; set; } // id nota
        public static int tipoTela { get; set; } = 0;
        public static string tipo { get; set; } //CFe NFCe

        private Model.Nota _modelNota = new Model.Nota();
        private BackgroundWorker WorkerBackground = new BackgroundWorker();
        private int p1 = 0;
        private string _msg;

        public OpcoesCfe()
        {
            InitializeComponent();
            
            if (OpcoesCfe.tipo == "NFCe")
            {
                pictureBox1.Image = Resources.nfce;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            
            if (OpcoesCfe.tipoTela == 0)
                btnDetalhes.Visible = false;

            Eventos();
        }

        private string checkCupom()
        {
            Model.Nota checkNota = new Model.Nota();

            if (idNota > 0)
                checkNota = _modelNota.FindById(idNota).FirstOrDefault<Model.Nota>();
            else
                checkNota = _modelNota.FindByIdPedidoUltReg(idPedido, "", "CFe").FirstOrDefault<Model.Nota>();

            if (checkNota == null)
                return null;

            if (checkNota.Status == null)
                return null;

            return checkNota.Status;
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //case Keys.Escape:
                //    Close();
                //    break;
            }
        }

        /// <summary>
        /// Eventos do form
        /// </summary>
        public void Eventos()
        {
            //KeyPreview = true;
            //KeyDown += KeyDowns;

            Load += (s, e) =>
            {
                if (checkCupom() == null || checkCupom() == "Pendente")
                    Emitir.Text = "Emitir";
                else if (checkCupom() == "Autorizada" || checkCupom() == "Autorizado")
                    Emitir.Text = "Cancelar";
                else if (checkCupom() == "Cancelada" || checkCupom() == "Cancelado")
                {
                    Emitir.Visible = false;
                    btnDetalhes.Visible = false;
                    Imprimir.Location = new Point(330, 303);
                }
            };

            //Emitir.KeyDown += KeyDowns;

            Emitir.Click += (s, e) =>
            {
                if (Emitir.Text == "Cancelar")
                {
                    retorno.Text = "Cancelando cupom...";

                    p1 = 2;
                    WorkerBackground.RunWorkerAsync();
                }
                else
                {
                    retorno.Text = "Emitindo cupom...";

                    p1 = 1;
                    var checkNota = _modelNota.FindByIdPedidoAndTipo(idPedido, OpcoesCfe.tipo == "NFCe" ? "NFCe" : "CFe").FirstOrDefault<Model.Nota>();
                    if (checkNota == null)
                    {
                        _modelNota.Id = 0;
                        _modelNota.Tipo = OpcoesCfe.tipo == "NFCe" ? "NFCe" : "CFe";
                        _modelNota.id_pedido = idPedido;
                        _modelNota.Save(_modelNota, false);
                    }

                    WorkerBackground.RunWorkerAsync();
                }
            };

            Imprimir.Click += (s, e) =>
            {
                if (checkCupom() == null)
                {
                    Alert.Message("Opps!", "Emita o cupom para imprimir.", Alert.AlertType.warning);
                    return;
                }

                retorno.Text = "Imprimindo cupom...";

                var msg = new Controller.Fiscal().Imprimir(idPedido, "CFe");

                retorno.Text = "CF-e S@T impresso com sucesso!";
            };

            using (var b = WorkerBackground)
            {
                b.DoWork += async (s, e) =>
                {
                    switch (p1)
                    {
                        case 1:
                            _msg = new Controller.Fiscal().Emitir(idPedido, OpcoesCfe.tipo == "NFCe" ? "NFCe" : "CFe");
                            break;

                        case 2:
                            _msg = new Controller.Fiscal().Cancelar(idPedido, OpcoesCfe.tipo == "NFCe" ? "NFCe" : "CFe");
                            break;
                    }
                };

                b.RunWorkerCompleted += async (s, e) =>
                {
                    p1 = 0;
                    retorno.Text = _msg;
                    Emitir.Enabled = true;
                };
            }

            btnDetalhes.Click += (s, e) =>
            {
                DetailsPedido.idPedido = idPedido;
                DetailsPedido detailsPedido = new DetailsPedido();
                detailsPedido.Show();

                Close();
            };

            FormClosing += (s, e) =>
            {
                OpcoesCfe.tipo = "";
            };
        }
    }
}