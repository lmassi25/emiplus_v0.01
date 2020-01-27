using Emiplus.Data.Helpers;
using Emiplus.View.Common;
using SqlKata.Execution;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Emiplus.View.Fiscal.TelasNota
{
    public partial class CartaCorrecao : Form
    {
        private IEnumerable<dynamic> dataTable;
        private BackgroundWorker WorkerBackground = new BackgroundWorker();
        private BackgroundWorker WorkerBackground2 = new BackgroundWorker();

        private int p1 = 0;
        private string _msg;

        public static int idPedido { get; set; }

        private Model.Nota _mNota = new Model.Nota();
        private Controller.Nota _cNota = new Controller.Nota();

        public CartaCorrecao()
        {
            InitializeComponent();

            idPedido = OpcoesNfeRapida.idPedido;

            Eventos();
        }

        private void DataTableStart()
        {
            WorkerBackground.RunWorkerAsync();
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    Support.UpDownDataGrid(false, GridLista);
                    e.Handled = true;
                    break;

                case Keys.Down:
                    Support.UpDownDataGrid(true, GridLista);
                    e.Handled = true;
                    break;

                case Keys.Escape:
                    Close();
                    break;
            }
        }

        private void Eventos()
        {
            KeyDown += KeyDowns;
            KeyPreview = true;

            Load += (s, e) =>
            {
                DataTableStart();
            };

            //GridLista.DoubleClick += (s, e) => MessageBox.Show("");

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");

            btnAdicionar.Click += (s, e) =>
            {
                Model.Nota _notaCCe = new Model.Nota();
                _notaCCe = _notaCCe.Query().Where("status", "Transmitindo...").Where("id_pedido", idPedido).Where("excluir", 0).FirstOrDefault<Model.Nota>();

                if (_notaCCe != null)
                {
                    Alert.Message("Ação não permitida", "Existe outra CCe transmitindo", Alert.AlertType.warning);
                    return;
                }

                CartaCorrecaoAdd f = new CartaCorrecaoAdd();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    p1 = 1;
                    WorkerBackground2.RunWorkerAsync();
                }
            };

            btnRetransmitir.Click += (s, e) =>
            {
                //validação de registro com status Transmitindo...
                p1 = 1;
                WorkerBackground2.RunWorkerAsync();
            };

            imprimir.Click += (s, e) =>
            {
                imprimir.Text = "Imprimindo...";
                p1 = 2;
                WorkerBackground2.RunWorkerAsync();
            };

            btnRemover.Click += (s, e) =>
            {
                Model.Nota _notaCCe = new Model.Nota();
                _notaCCe = _notaCCe.Query().Where("status", "Transmitindo...").Where("id_pedido", idPedido).Where("excluir", 0).First<Model.Nota>();

                if (_notaCCe != null)
                {
                    Alert.Message("Ação não permitida", "Exclusão não realizada", Alert.AlertType.warning);
                    return;
                }

                var result = AlertOptions.Message("Atenção!", "Você está prestes a deletar uma carta de correção, continuar?", AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                if (result)
                {
                    _notaCCe.Excluir = 1;
                    _notaCCe.Save(_notaCCe);

                    DataTableStart();
                }
            };

            GridLista.CellFormatting += (s, e) =>
            {
            };

            using (var b = WorkerBackground)
            {
                b.DoWork += async (s, e) =>
                {
                    dataTable = await _cNota.GetDataTable(idPedido);
                };

                b.RunWorkerCompleted += async (s, e) =>
                {
                    await _cNota.SetTable(GridLista, idPedido);
                };
            }

            using (var b = WorkerBackground2)
            {
                b.DoWork += async (s, e) =>
                {
                    switch (p1)
                    {
                        case 1:
                            _msg = new Controller.Fiscal().EmitirCCe(idPedido);
                            break;

                        case 2:
                            var msg = new Controller.Fiscal().ImprimirCCe(idPedido);
                            if (!msg.Contains(".pdf"))
                                _msg = msg;
                            break;
                    }
                };

                b.RunWorkerCompleted += async (s, e) =>
                {
                    switch (p1)
                    {
                        case 1:

                            if (_msg.Contains("AUTORIZADA"))
                                Alert.Message("Tudo certo!", "Carta de correção autorizada", Alert.AlertType.success);//AlertOptions.Message("Tudo certo!", "Carta de correção autorizada", AlertBig.AlertType.success, AlertBig.AlertBtn.OK);
                            else
                                Alert.Message("Opss", _msg, Alert.AlertType.error);//AlertOptions.Message("Opss", _msg, AlertBig.AlertType.error, AlertBig.AlertBtn.OK);

                            break;

                        case 2:
                            imprimir.Text = "Imprimir";
                            break;
                    }

                    DataTableStart();

                    p1 = 0;
                };
            }
        }
    }
}