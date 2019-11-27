using SqlKata.Execution;
using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Emiplus.View.Fiscal.TelasNota
{
    public partial class TelaFinal : Form
    {
        private static int Id { get; set; } // id nota
        private BackgroundWorker WorkerBackground = new BackgroundWorker();
        private string _msg;

        private Model.Nota _mNota = new Model.Nota();

        public TelaFinal()
        {
            InitializeComponent();
            Id = Nota.Id;
            Eventos();
        }

        private void Eventos()
        {
            Back.Click += (s, e) => Close();

            Emitir.Click += (s, e) =>
            {
                retorno.Text = "Emitindo NF-e .......................................... (1/2)";
                Emitir.Enabled = false;
                WorkerBackground.RunWorkerAsync();
            };

            Imprimir.Click += (s, e) =>
            {
                retorno.Text = new Controller.Fiscal().Imprimir(Id, "NFe");
            };

            using (var b = WorkerBackground)
            {
                b.DoWork += async (s, e) =>
                {
                    _msg = new Controller.Fiscal().Emitir(Id, "NFe");
                };

                b.RunWorkerCompleted += async (s, e) =>
                {
                    retorno.Text = _msg;
                    Emitir.Enabled = true;
                };
            }
        }
    }
}