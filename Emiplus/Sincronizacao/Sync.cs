using System.ComponentModel;
using System.Windows.Forms;
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.View.Common;

namespace Sincronizacao
{
    public partial class Sync : Form
    {
        private readonly BackgroundWorker backWork = new BackgroundWorker();
        private readonly Timer timer1 = new Timer();
        private readonly Emiplus.View.Common.Sync f = new Emiplus.View.Common.Sync();
        
        public Sync()
        {
            InitializeComponent();
            Eventos();
        }
        
        private void Eventos()
        {
            Load += (s, e) =>
            {
                Hide();
            };

            Shown += (s, e) =>
            {
                timer1.Enabled = true;
                timer1.Interval = 600000;
                
                if (Support.CheckForInternetConnection())
                {
                    f.SendNota();
                
                    timer1.Start();
                }
            };
            
            timer1.Tick += (s, e) =>
            {
                if (Support.CheckForInternetConnection())
                    if (!Home.syncActive)
                    {
                        backWork.RunWorkerAsync();
                        IniFile.Write("Sync", "true", "APP");
                        Home.syncActive = true;
                    }
                
                timer1.Stop();
            };

            backWork.DoWork += async (s, e) =>
            {
                await f.StartSync();
            };
            
            backWork.RunWorkerCompleted += (s, e) =>
            {
                new Log().Add("SYNC", "Sincronização", Log.LogType.fatal);
                Home.syncActive = false;
                IniFile.Write("Sync", "false", "APP");
                timer1.Start();
            };
        }
    }
}
