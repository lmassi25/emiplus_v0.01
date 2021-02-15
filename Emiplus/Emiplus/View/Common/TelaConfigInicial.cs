using System.ComponentModel;
using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.Properties;
using Emiplus.View.Configuracoes;

namespace Emiplus.View.Common
{
    public partial class TelaConfigInicial : Form
    {        
        private readonly BackgroundWorker backWork = new BackgroundWorker();
        private readonly BackgroundWorker backWorkRemessa = new BackgroundWorker();

        public TelaConfigInicial()
        {
            InitializeComponent();
            Eventos();
        }

        public void Eventos()
        {
            ToolHelp.Show(
                "Utilize essa função para sincronizar todos os dados do programa para consulta no sistema web do seus dados.",
                pictureBox11, ToolHelp.ToolTipIcon.Info, "Ajuda!");

            Load += (s, e) => Refresh();

            dadosEmpresa.Click += (s, e) => OpenForm.Show<InformacaoGeral>(this);
            email.Click += (s, e) => OpenForm.Show<Email>(this);
            sat.Click += (s, e) => OpenForm.Show<Cfesat>(this);
            comercial.Click += (s, e) => OpenForm.Show<Configuracoes.Comercial>(this);
            os.Click += (s, e) => OpenForm.Show<OS>(this);
            impressao.Click += (s, e) => OpenForm.Show<Impressao>(this);
            system.Click += (s, e) => OpenForm.Show<Sistema>(this);

            btnBalancas.Click += (s, e) => OpenForm.Show<Balanças>(this);

            btnImportar.Click += (s, e) =>
            {
                var f = new ImportarDados();
                f.ShowDialog();
            };

            btnSincronizar.Click += (s, e) =>
            {
                if (Support.CheckForInternetConnection())
                {
                    if (!Home.syncActive)
                    {
                        Home.syncActive = true;
                        pictureBox10.Image = Resources.loader_page;
                        btnSincronizar.Text = @"Sincronizando..";
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
                if (Sync.sync_start == 0)
                {
                    var f = new Sync();
                    await f.StartSync();
                }                
            };

            backWork.RunWorkerCompleted += (s, e) =>
            {
                new Log().Add("SYNC", "Sincronização", Log.LogType.fatal);
                pictureBox10.Image = Resources.refresh;
                btnSincronizar.Text = @"Sincronizar";
                Home.syncActive = false;
            };

            /*
             *  Enviar Remessas
             */
            btnRemessaEnviar.Click += (s, e) =>
            {
                if (Support.CheckForInternetConnection())
                {
                    if (!Home.syncActive)
                    {
                        Home.syncActive = true;
                        pictureBox14.Image = Resources.loader_page;
                        label13.Text = @"Enviando..";
                        backWorkRemessa.RunWorkerAsync();
                    }
                    else
                    {
                        Alert.Message("Opps", "Sincronização já está rodando.", Alert.AlertType.info);
                    }
                }
            };

            backWorkRemessa.DoWork += async (s, e) =>
            {
                var f = new Sync();
                Sync.Remessa = true;
                await f.SendRemessa();
                Sync.Remessa = false;
            };

            backWorkRemessa.RunWorkerCompleted += (s, e) =>
            {
                new Log().Add("SYNC", "Remessa enviada", Log.LogType.fatal);
                pictureBox14.Image = Resources.caja;
                label13.Text = @"Remessas";
                Home.syncActive = false;
            };

            btnRemessaReceber.Click += async (s, e) =>
            {
                var f = new Sync();
                await f.ReceberRemessa();
            };

            btnImportProdutos.Click += (s, e) => OpenForm.Show<ImportProdutos>(this);
        }
    }
}