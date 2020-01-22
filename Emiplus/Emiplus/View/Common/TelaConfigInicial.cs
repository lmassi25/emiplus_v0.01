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
            //menu.NodeMouseClick += (s, e) =>
            //{
            //    switch (e.Node.Name)
            //    {
            //        case "comercial":
            //            OpenForm.ShowInPanel<Configuracoes.Comercial>(tela);
            //            break;
            //        case "cfesat":
            //            OpenForm.ShowInPanel<Configuracoes.Cfesat>(tela);
            //            break;
            //    }
            //};

            comercial.Click += (s, e) =>
            {
                OpenForm.ShowInPanel<Configuracoes.Comercial>(panel);
            };

            impressao.Click += (s, e) => OpenForm.ShowInPanel<Configuracoes.Impressao>(panel);
        }
    }
}
