using Emiplus.Data.Helpers;
using System.ComponentModel;
using System.Windows.Forms;

namespace Emiplus.View.Common
{
    public partial class TelaConfigInicial : Form
    {
        private BackgroundWorker backWork = new BackgroundWorker();

        public TelaConfigInicial()
        {
            InitializeComponent();
            Eventos();
        }

        public void Eventos()
        {
            ToolHelp.Show("Utilize essa função para sincronizar todos os dados do programa para consulta no sistema web do seus dados.", pictureBox11, ToolHelp.ToolTipIcon.Info, "Ajuda!");

            dadosEmpresa.Click += (s, e) => OpenForm.Show<Configuracoes.InformacaoGeral>(this);
            email.Click += (s, e) => OpenForm.Show<Configuracoes.Email>(this);
            sat.Click += (s, e) => OpenForm.Show<Configuracoes.Cfesat>(this);
            comercial.Click += (s, e) => OpenForm.Show<Configuracoes.Comercial>(this);
            os.Click += (s, e) => OpenForm.Show<Configuracoes.OS>(this);
            impressao.Click += (s, e) => OpenForm.Show<Configuracoes.Impressao>(this);
            system.Click += (s, e) => OpenForm.Show<Configuracoes.Sistema>(this);

            btnImportar.Click += (s, e) =>
            {
                ImportarDados f = new ImportarDados();
                f.ShowDialog();
            };

            btnSincronizar.Click += async (s, e) =>
            {
                if (Support.CheckForInternetConnection())
                {
                    if (!Home.syncActive) {
                        Home.syncActive = true;
                        pictureBox10.Image = Properties.Resources.loader_page;
                        btnSincronizar.Text = "Sincronizando..";
                        backWork.RunWorkerAsync();
                    }
                    else
                    {
                        Alert.Message("Opps", "Sincronização já está rodando.", Alert.AlertType.info);
                    }
                }
                else
                {
                    Alert.Message("Opps", "Parece que você não possui conexão com a internet.", Alert.AlertType.error);
                }
            };

            backWork.DoWork += async (s, e) =>
            {
                Sync f = new Sync();
                await f.StartSync();
            };

            backWork.RunWorkerCompleted += (s, e) =>
            {
                new Log().Add("SYNC", "Sincronização", Log.LogType.fatal);
                pictureBox10.Image = Properties.Resources.refresh;
                btnSincronizar.Text = "Sincronizar";
                Home.syncActive = false;
            };
        }
    }
}