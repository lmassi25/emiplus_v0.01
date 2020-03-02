using Emiplus.Data.Helpers;
using System.Windows.Forms;

namespace Emiplus.View.Common
{
    public partial class TelaConfigInicial : Form
    {
        public TelaConfigInicial()
        {
            InitializeComponent();
            Eventos();
        }

        public void Eventos()
        {
            dadosEmpresa.Click += (s, e) => OpenForm.Show<Configuracoes.InformacaoGeral>(this);
            email.Click += (s, e) => OpenForm.Show<Configuracoes.Email>(this);
            sat.Click += (s, e) => OpenForm.Show<Configuracoes.Cfesat>(this);
            comercial.Click += (s, e) => OpenForm.Show<Configuracoes.Comercial>(this);
            impressao.Click += (s, e) => OpenForm.Show<Configuracoes.Impressao>(this);
            system.Click += (s, e) => OpenForm.Show<Configuracoes.Sistema>(this);

            btnImportar.Click += (s, e) =>
            {
                ImportarDados f = new ImportarDados();
                f.ShowDialog();
            };
        }
    }
}