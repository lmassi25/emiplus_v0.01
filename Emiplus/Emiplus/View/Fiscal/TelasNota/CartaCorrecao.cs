using Emiplus.Data.Helpers;
using Emiplus.View.Common;
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
                CartaCorrecaoAdd f = new CartaCorrecaoAdd();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    DataTableStart();

                    WorkerBackground2.RunWorkerAsync();
                }
            };

            btnRetransmitir.Click += (s, e) =>
            {
                //validação de registro com status Transmitindo...
                WorkerBackground2.RunWorkerAsync();
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
                    _msg = new Controller.Fiscal().EmitirCCe(idPedido);
                };

                b.RunWorkerCompleted += async (s, e) =>
                {
                    if (_msg.Contains("AUTORIZADA"))
                        Alert.Message("Tudo certo!", "Carta de correção autorizada", Alert.AlertType.success);//AlertOptions.Message("Tudo certo!", "Carta de correção autorizada", AlertBig.AlertType.success, AlertBig.AlertBtn.OK);
                    else
                        Alert.Message("Opss", _msg, Alert.AlertType.error);//AlertOptions.Message("Opss", _msg, AlertBig.AlertType.error, AlertBig.AlertBtn.OK);

                    DataTableStart();
                };
            }

            imprimir.Click += async (s, e) => await RenderizarAsync();
        }

        private async Task RenderizarAsync()
        {
            
        }
    }
}
