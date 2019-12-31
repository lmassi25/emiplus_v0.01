using Emiplus.Data.Helpers;
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
    public partial class DocumentosReferenciados : Form
    {
        private IEnumerable<dynamic> dataTable;
        private BackgroundWorker WorkerBackground = new BackgroundWorker();
        private BackgroundWorker WorkerBackground2 = new BackgroundWorker();

        private int p1 = 0;
        private string _msg;

        public static int idPedido { get; set; }

        private Model.Nota _mNota = new Model.Nota();
        private Controller.Nota _cNota = new Controller.Nota();

        public DocumentosReferenciados()
        {
            InitializeComponent();

            idPedido = Nota.Id;

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

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser(Program.URL_BASE + "/ajuda");

            btnAdicionar.Click += (s, e) =>
            {
                //Model.Nota _notaCCe = new Model.Nota();
                ////_notaCCe = _notaCCe.Query().Where("status", "Transmitindo...").Where("id_pedido", idPedido).Where("excluir", 0).FirstOrDefault<Model.Nota>();

                //if (_notaCCe != null)
                //{
                //    Alert.Message("Ação não permitida", "Existe outra CCe transmitindo", Alert.AlertType.warning);
                //    return;
                //}

                //CartaCorrecaoAdd f = new CartaCorrecaoAdd();
                //if (f.ShowDialog() == DialogResult.OK)
                //{
                //    p1 = 1;
                //    WorkerBackground2.RunWorkerAsync();
                //}
            };

            btnRemover.Click += (s, e) =>
            {
                //Model.Nota _notaCCe = new Model.Nota();
                //_notaCCe = _notaCCe.Query().Where("status", "Transmitindo...").Where("id_pedido", idPedido).Where("excluir", 0).First<Model.Nota>();

                //if (_notaCCe != null)
                //{
                //    Alert.Message("Ação não permitida", "Exclusão não realizada", Alert.AlertType.warning);
                //    return;
                //}

                //var result = AlertOptions.Message("Atenção!", "Você está prestes a deletar uma carta de correção, continuar?", AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                //if (result)
                //{
                //    _notaCCe.Excluir = 1;
                //    _notaCCe.Save(_notaCCe);

                //    DataTableStart();
                //}
            };

            using (var b = WorkerBackground)
            {
                b.DoWork += async (s, e) =>
                {
                    dataTable = await _cNota.GetDataTableDoc(idPedido);
                };

                b.RunWorkerCompleted += async (s, e) =>
                {
                    await _cNota.SetTableDoc(GridLista, idPedido);
                };
            }            
        }
    }
}
