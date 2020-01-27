using Emiplus.Data.Helpers;
using Emiplus.View.Common;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Emiplus.View.Fiscal
{
    public partial class InutilizarNotas : Form
    {
        private Controller.Nota _cNota = new Controller.Nota();
        private BackgroundWorker WorkerBackground = new BackgroundWorker();
        private string _msg;
        private int p1 = 0;

        public InutilizarNotas()
        {
            InitializeComponent();

            status.DataSource = new List<String> { "Todos", "Transmitidos", "Autorizadas" };

            Eventos();
        }

        private void Filter()
        {
            _cNota.GetDataTableInutilizar(GridLista, status.Text, dataInicial.Text, dataFinal.Text);
        }

        private void Edit(bool create = false)
        {
            if (create)
            {
                var _nota = new Model.Nota().Query().Where("status", "Transmitindo...").Where("tipo", "Inutiliza").Where("excluir", 0).FirstOrDefault<Model.Nota>();

                if (_nota != null)
                {
                    Alert.Message("Ação não permitida", "Existe um registro pendente para transmissão!", Alert.AlertType.warning);
                    return;
                }

                InutilizarNotasAdd.idNota = 0;
                InutilizarNotasAdd f = new InutilizarNotasAdd();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    Filter();

                    if (p1 == 0)
                    {
                        p1 = 1;
                        WorkerBackground.RunWorkerAsync();
                    }
                }
                return;
            }

            if (GridLista.SelectedRows.Count > 0)
            {
                InutilizarNotasAdd.idNota = Convert.ToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
                InutilizarNotasAdd f = new InutilizarNotasAdd();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    Filter();

                    if (p1 == 0)
                    {
                        p1 = 1;
                        WorkerBackground.RunWorkerAsync();
                    }
                }
            }
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
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
                Filter();

                dataInicial.Text = Validation.DateNowToSql();
                dataFinal.Text = Validation.DateNowToSql();
            };

            btnAdicionar.Click += (s, e) => Edit(true);
            btnEditar.Click += (s, e) => Edit();
            GridLista.DoubleClick += (s, e) => Edit();
            btnExit.Click += (s, e) => Close();

            dataInicial.ValueChanged += (s, e) => Filter();
            dataFinal.ValueChanged += (s, e) => Filter();
            status.SelectedValueChanged += (s, e) => Filter();

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");

            btnRetransmitir.Click += (s, e) =>
            {
                if (p1 == 0)
                {
                    p1 = 1;
                    WorkerBackground.RunWorkerAsync();
                }
            };

            btnRemover.Click += (s, e) =>
            {
                Model.Nota _nota = new Model.Nota();
                _nota = _nota.Query().Where("status", "Transmitindo...").Where("excluir", 0).First<Model.Nota>();

                if (_nota != null)
                {
                    Alert.Message("Ação não permitida", "Exclusão não realizada", Alert.AlertType.warning);
                    return;
                }

                var result = AlertOptions.Message("Atenção!", "Você está prestes a deletar, deseja continuar?", AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                if (result)
                {
                    _nota.Excluir = 1;
                    _nota.Save(_nota);

                    Filter();
                }
            };

            WorkerBackground.RunWorkerAsync();

            //imprimir.Click += async (s, e) => await RenderizarAsync();

            using (var b = WorkerBackground)
            {
                b.DoWork += async (s, e) =>
                {
                    _msg = new Controller.Fiscal().EmitirInutiliza();
                };

                b.RunWorkerCompleted += async (s, e) =>
                {
                    p1 = 0;

                    if (String.IsNullOrEmpty(_msg))
                        return;
                    else if (_msg.Contains("Inutilização de número homologado"))
                        Alert.Message("Tudo certo!", "Inutilização de número homologado", Alert.AlertType.success);//AlertOptions.Message("Tudo certo!", "Carta de correção autorizada", AlertBig.AlertType.success, AlertBig.AlertBtn.OK);
                    else
                        Alert.Message("Opss", _msg, Alert.AlertType.error);//AlertOptions.Message("Opss", _msg, AlertBig.AlertType.error, AlertBig.AlertBtn.OK);

                    Filter();
                };
            }
        }
    }
}