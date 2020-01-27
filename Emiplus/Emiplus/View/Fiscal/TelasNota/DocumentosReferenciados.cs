using Emiplus.Data.Helpers;
using Emiplus.View.Common;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Emiplus.View.Fiscal.TelasNota
{
    public partial class DocumentosReferenciados : Form
    {
        private IEnumerable<dynamic> dataTable;
        private BackgroundWorker WorkerBackground = new BackgroundWorker();

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
                DocumentosReferenciadosAdd f = new DocumentosReferenciadosAdd();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    DataTableStart();
                }
            };

            btnRemover.Click += (s, e) =>
            {
                var result = AlertOptions.Message("Atenção!", "Você está prestes a deletar uma chave de acesso, continuar?", AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                if (result)
                {
                    _mNota = _mNota.FindById(Convert.ToInt32(GridLista.SelectedRows[0].Cells["ID"].Value)).FirstOrDefault<Model.Nota>();
                    _mNota.Excluir = 1;
                    _mNota.Save(_mNota, false);

                    DataTableStart();
                }
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