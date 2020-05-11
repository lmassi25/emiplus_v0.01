using System.ComponentModel;
using System.Windows.Forms;

namespace Emiplus.View.Configuracoes
{
    public partial class Cfesat_logs : Form
    {
        private string _msg;
        private readonly BackgroundWorker WorkerBackground = new BackgroundWorker();

        public Cfesat_logs()
        {
            InitializeComponent();

            Eventos();

            Start();
        }

        public static int tipo { get; set; }

        public void Start()
        {
            label12.Focus();
            retorno.Text = "Aguarde, carregando logs...";
            retorno.Refresh();
            WorkerBackground.RunWorkerAsync();
        }

        /// <summary>
        ///     Eventos do form
        /// </summary>
        public void Eventos()
        {
            using (var b = WorkerBackground)
            {
                b.DoWork += async (s, e) =>
                {
                    _msg = tipo == 1 ? new Controller.Fiscal().Logs(1) : new Controller.Fiscal().Logs();
                };

                b.RunWorkerCompleted += async (s, e) => { retorno.Text = _msg; };
            }
        }
    }
}