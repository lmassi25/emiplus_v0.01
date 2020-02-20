using Emiplus.Data.Helpers;
using SqlKata.Execution;
using System.ComponentModel;
using System.Windows.Forms;

namespace Emiplus.View.Fiscal.TelasNota
{
    public partial class TelaFinal : Form
    {
        private static int Id { get; set; } // id nota
        private BackgroundWorker WorkerBackground = new BackgroundWorker();
        private string _msg, justificativa;
        private int p1 = 0;
        private Model.Nota _mNota = new Model.Nota();

        public TelaFinal()
        {
            InitializeComponent();
            
            _mNota = new Model.Nota().FindById(Nota.Id).FirstOrDefault<Model.Nota>();
            
            if (_mNota == null)
            {
                Alert.Message("Ação não permitida", "Referência de Pedido não identificada", Alert.AlertType.warning);
                return;
            }

            Id = _mNota.id_pedido;

            Eventos();
        }

        private void Eventos()
        {
            Back.Click += (s, e) => Close();

            Emitir.Click += (s, e) =>
            {
                //_mNota = new Model.Nota().FindByIdPedido(Id).FirstOrDefault<Model.Nota>();
                if (_mNota.Status != "Pendente")
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
                retorno.Text = new Controller.Fiscal().Imprimir(Id, "NFe", _mNota.Id);
            };

            EnviarEmail.Click += (s, e) =>
            {
                var checkNota = _mNota.FindByIdPedidoUltReg(Id, "", "NFe").FirstOrDefault<Model.Nota>();
                if (checkNota.Status != "Autorizada")
                {
                    Alert.Message("Ação não permitida!", "Não é possível enviar uma nota Pendente.", Alert.AlertType.warning);
                    return;
                }

                _mNota = checkNota;

                CartaCorrecaoAdd.tela = "Email";
                CartaCorrecaoAdd f = new CartaCorrecaoAdd();
                f.TopMost = true;
                if (f.ShowDialog() == DialogResult.OK)
                {
                    CartaCorrecaoAdd.tela = "";
                    justificativa = CartaCorrecaoAdd.justificativa;

                    retorno.Text = "Enviando NF-e .......................................... (1/2)";

                    p1 = 5;
                    WorkerBackground.RunWorkerAsync();
                }
            };

            using (var b = WorkerBackground)
            {
                b.DoWork += async (s, e) =>
                {
                    if (p1 == 5)
                        _msg = new Controller.Fiscal().EnviarEmail(Id, justificativa, "NFe", _mNota.Id);
                    else
                        _msg = new Controller.Fiscal().Emitir(Id, "NFe", _mNota.Id, false);
                };

                b.RunWorkerCompleted += async (s, e) =>
                {
                    p1 = 0;

                    retorno.Text = _msg;
                    Emitir.Enabled = true;
                };
            }
        }
    }
}