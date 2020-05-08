using System.ComponentModel;
using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.Model;
using SqlKata.Execution;

namespace Emiplus.View.Comercial
{
    public partial class OpcoesNfse : Form
    {
        private Nota _modelNota = new Nota();
        private string _msg, justificativa;
        private int p1;
        private readonly BackgroundWorker workerBackground = new BackgroundWorker();

        public OpcoesNfse()
        {
            InitializeComponent();

            if (idNota == 0)
            {
                var checkNota = new Nota().FindByIdPedidoUltReg(idPedido, "", "NFSe").FirstOrDefault<Nota>();

                if (checkNota != null) idNota = checkNota.Id;
            }

            Eventos();
        }

        public static int idPedido { get; set; }
        public static int idNota { get; set; }

        public void Eventos()
        {
            Emitir.Click += (s, e) =>
            {
                var checkNota = new Nota().FindById(idNota).FirstOrDefault<Nota>();
                if (checkNota == null)
                {
                    var _modelNotaNova = new Nota
                    {
                        Id = 0, 
                        Tipo = "NFSe", 
                        Status = "Pendente", 
                        id_pedido = idPedido
                    };
                    _modelNotaNova.Save(_modelNotaNova, false);

                    checkNota = new Nota().FindByIdPedidoUltReg(idPedido, "", "NFSe").FirstOrDefault<Nota>();
                }

                if (checkNota.Status == "Cancelada")
                {
                    //if (Home.pedidoPage == "Notas")
                    //{
                    //    Alert.Message("Atenção!", "Não é possível emitir uma nota Autorizada/Cancelada.", Alert.AlertType.warning);
                    //    return;
                    //}

                    //var result = AlertOptions.Message("Atenção!", "Existem registro(s) de nota(s) cancelada(s) a partir desta venda. Deseja gerar um nova nota?", AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                    //if (result)
                    //{
                    //    Model.Nota _modelNotaNova = new Model.Nota();

                    //    _modelNotaNova.Id = 0;
                    //    _modelNotaNova.Tipo = "NFe";
                    //    _modelNotaNova.Status = "Pendente";
                    //    _modelNotaNova.id_pedido = idPedido;
                    //    _modelNotaNova.Save(_modelNotaNova, false);

                    //    checkNota = new Model.Nota().FindByIdPedidoUltReg(idPedido, "", "NFe").FirstOrDefault<Model.Nota>();
                    //}
                }

                if (checkNota.Status != "Pendente")
                {
                    Alert.Message("Atenção!", "Não é possível emitir uma nota Autorizada/Cancelada.",
                        Alert.AlertType.warning);

                    return;
                }

                _modelNota = checkNota;

                retorno.Text = "Emitindo NFS-e .......................................... (1/2)";

                if (p1 == 0)
                {
                    p1 = 1;
                    workerBackground.RunWorkerAsync();
                }
                else
                {
                    Alert.Message("Ação não permitida", "Aguarde processo finalizar", Alert.AlertType.warning);
                }
            };

            using (var b = workerBackground)
            {
                b.DoWork += (s, e) =>
                {
                    switch (p1)
                    {
                        case 1:
                            _msg = new Controller.Fiscal().Emitir(idPedido, "NFSe", _modelNota.Id);
                            break;

                        case 2:
                            var msg = new Controller.Fiscal().Imprimir(idPedido, "NFSe", _modelNota.Id);
                            if (!msg.Contains(".pdf"))
                                _msg = msg;
                            break;

                        case 3:
                            //_msg = new Controller.Fiscal().EmitirCCe(idPedido, "Nota gerada com informacoes incorretas, por gentileza verificar as corretas");
                            break;

                        case 4:
                            if (justificativa.Length <= 15)
                                break;

                            _msg = new Controller.Fiscal().Cancelar(idPedido, "NFSe", justificativa, _modelNota.Id);
                            break;

                        case 5:
                            _msg = new Controller.Fiscal().EnviarEmail(idPedido, justificativa, "NFSe", _modelNota.Id);
                            break;
                    }
                };

                b.RunWorkerCompleted += (s, e) =>
                {
                    p1 = 0;
                    retorno.Text = _msg;
                };
            }

            FormClosing += (s, e) =>
            {
                idPedido = 0;
                idNota = 0;
            };
        }
    }
}