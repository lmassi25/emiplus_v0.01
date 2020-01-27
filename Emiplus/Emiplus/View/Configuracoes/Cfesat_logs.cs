using System.ComponentModel;
using System.Windows.Forms;

namespace Emiplus.View.Configuracoes
{
    public partial class Cfesat_logs : Form
    {
        private BackgroundWorker WorkerBackground = new BackgroundWorker();
        private string _msg;
        public static int tipo { get; set; }

        public Cfesat_logs()
        {
            InitializeComponent();

            Eventos();

            Start();
        }

        public void Start()
        {
            label12.Focus();
            retorno.Text = "Aguarde, carregando logs...";
            retorno.Refresh();
            WorkerBackground.RunWorkerAsync();
        }

        /// <summary>
        /// Eventos do form
        /// </summary>
        public void Eventos()
        {
            using (var b = WorkerBackground)
            {
                b.DoWork += async (s, e) =>
                {
                    if (tipo == 1)
                        _msg = new Controller.Fiscal().Logs(1);
                    else
                        _msg = new Controller.Fiscal().Logs(0);
                };

                b.RunWorkerCompleted += async (s, e) =>
                {
                    retorno.Text = _msg;
                };
            }
        }
    }
}