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

namespace Emiplus.View.Comercial
{
    public partial class OpcoesNfse : Form
    {
        public static int idPedido { get; set; }
        public static int idNota { get; set; }

        private Model.Nota _modelNota = new Model.Nota();
        private BackgroundWorker WorkerBackground = new BackgroundWorker();
        private string _msg, justificativa;
        private int p1 = 0;

        public OpcoesNfse()
        {
            InitializeComponent();

            if (idNota == 0)
            {
                var checkNota = new Model.Nota().FindByIdPedidoUltReg(idPedido, "", "NFSe").FirstOrDefault<Model.Nota>();

                if (checkNota != null)
                {
                    idNota = checkNota.Id;
                }
            }

            Eventos();
        }
        
        public void Eventos()
        {
            Emitir.Click += (s, e) =>
            {
                var checkNota = new Model.Nota().FindById(idNota).FirstOrDefault<Model.Nota>();
                if (checkNota == null)
                {
                    Model.Nota _modelNotaNova = new Model.Nota();

                    _modelNotaNova.Id = 0;
                    _modelNotaNova.Tipo = "NFSe";
                    _modelNotaNova.Status = "Pendente";
                    _modelNotaNova.id_pedido = idPedido;
                    _modelNotaNova.Save(_modelNotaNova, false);

                    checkNota = new Model.Nota().FindByIdPedidoUltReg(idPedido, "", "NFSe").FirstOrDefault<Model.Nota>();
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
                    Alert.Message("Atenção!", "Não é possível emitir uma nota Autorizada/Cancelada.", Alert.AlertType.warning);
                    return;
                }

                _modelNota = checkNota;

                retorno.Text = "Emitindo NFS-e .......................................... (1/2)";

                if (p1 == 0)
                {
                    p1 = 1;
                    WorkerBackground.RunWorkerAsync();
                }
                else
                    Alert.Message("Ação não permitida", "Aguarde processo finalizar", Alert.AlertType.warning);
            };

            using (var b = WorkerBackground)
            {
                b.DoWork += async (s, e) =>
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

                b.RunWorkerCompleted += async (s, e) =>
                {
                    p1 = 0;
                    retorno.Text = _msg;
                };
            }

            FormClosing += (s, e) =>
            {
                OpcoesNfse.idPedido = 0;
                OpcoesNfse.idNota = 0;
            };
        }
    }
}
