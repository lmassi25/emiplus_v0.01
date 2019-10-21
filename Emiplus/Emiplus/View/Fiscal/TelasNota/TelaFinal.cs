using System.Windows.Forms;


namespace Emiplus.View.Fiscal.TelasNota
{
    public partial class TelaFinal : Form
    {
        private static int Id { get; set; } // id nota

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
                retorno.Text = new Controller.Fiscal().Issue(Id, "NFe");
            };
        }
    }
}