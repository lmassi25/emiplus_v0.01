using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.Properties;
using SqlKata.Execution;

namespace Emiplus.View.Comercial
{
    public partial class OpcoesCfe : Form
    {
        private readonly Nota _modelNota = new Nota();
        private readonly BackgroundWorker workerBackground = new BackgroundWorker();
        private string _msg;
        private int p1;

        public OpcoesCfe()
        {
            InitializeComponent();
            Eventos();
        }

        public static int idPedido { get; set; } // id pedido
        public static int idNota { get; set; } // id nota
        public static int tipoTela { get; set; } = 0;
        public static string tipo { get; set; } //CFe NFCe

        private string CheckCupom()
        {
            var checkNota = idNota > 0
                ? _modelNota.FindById(idNota).FirstOrDefault<Nota>()
                : _modelNota.FindByIdPedidoUltReg(idPedido, "", "CFe").FirstOrDefault<Nota>();
            return checkNota?.Status;
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
        ///     Eventos do form
        /// </summary>
        public void Eventos()
        {
            //KeyPreview = true;
            //KeyDown += KeyDowns;

            Load += (s, e) =>
            {
                if (tipo == "NFCe")
                {
                    pictureBox1.Image = Resources.nfce;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }

                if (tipoTela == 0)
                    btnDetalhes.Visible = false;

                if (CheckCupom() == null || CheckCupom() == "Pendente")
                {
                    Emitir.Text = @"Emitir";
                }
                else if (CheckCupom() == "Autorizada" || CheckCupom() == "Autorizado")
                {
                    Emitir.Text = @"Cancelar";
                }
                else if (CheckCupom() == "Cancelada" || CheckCupom() == "Cancelado")
                {
                    Emitir.Visible = false;
                    btnDetalhes.Visible = false;
                    //Imprimir.Location = new Point(330, 303);
                }

                var nota = new Nota().FindById(idNota).FirstOrDefault<Nota>();
                if (nota == null)
                    return;

                nsefaz.Text = nota.nr_Nota;
                status.Text = nota.Status;
                chavedeacesso.Text = nota.ChaveDeAcesso;
            };

            //Emitir.KeyDown += KeyDowns;

            Emitir.Click += (s, e) =>
            {
                if (Emitir.Text == @"Cancelar")
                {
                    retorno.Text = "Cancelando cupom...";

                    p1 = 2;
                    workerBackground.RunWorkerAsync();
                }
                else
                {
                    retorno.Text = "Emitindo cupom...";

                    p1 = 1;
                    var checkNota = _modelNota.FindByIdPedidoAndTipo(idPedido, tipo == "NFCe" ? "NFCe" : "CFe")
                        .FirstOrDefault<Nota>();
                    if (checkNota == null)
                    {
                        _modelNota.Id = 0;
                        _modelNota.Tipo = tipo == "NFCe" ? "NFCe" : "CFe";
                        _modelNota.id_pedido = idPedido;
                        _modelNota.Save(_modelNota, false);
                    }

                    workerBackground.RunWorkerAsync();
                }
            };

            Imprimir.Click += (s, e) =>
            {
                if (CheckCupom() == null)
                {
                    Alert.Message("Opps!", "Emita o cupom para imprimir.", Alert.AlertType.warning);
                    return;
                }

                retorno.Text = "Imprimindo cupom...";

                var msg = new Controller.Fiscal().Imprimir(idPedido, "CFe");

                retorno.Text = "CF-e S@T impresso com sucesso!";
            };

            using (var b = workerBackground)
            {
                b.DoWork += async (s, e) =>
                {
                    switch (p1)
                    {
                        case 1:
                            _msg = new Controller.Fiscal().Emitir(idPedido, tipo == "NFCe" ? "NFCe" : "CFe");
                            break;

                        case 2:
                            _msg = new Controller.Fiscal().Cancelar(idPedido, tipo == "NFCe" ? "NFCe" : "CFe");
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
                var detailsPedido = new DetailsPedido {TopMost = true};
                detailsPedido.Show();

                Close();
            };

            FormClosing += (s, e) => { tipo = ""; };
        }
    }
}