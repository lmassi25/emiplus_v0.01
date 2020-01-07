using Emiplus.Data.Database;
using Emiplus.Data.Helpers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Common
{
    public partial class Sync : Form
    {
        BackgroundWorker backWork = new BackgroundWorker();

        /// <summary>
        /// Acesso ao banco local
        /// </summary>
        private QueryFactory connect = new Connect().Open();

        /// <summary>
        /// Acesso ao banco online
        /// </summary>
        private QueryFactory connectOnline = new ConnectOnline().Open();

        public Sync()
        {
            InitializeComponent();
            Eventos();

            var cols = new[] { "id", "id_empresa", "tipo", "excluir", "criado", "atualizado", "deletado", "usuario", "saldo_inicial", "saldo_final", "saldo_final_informado", "observacao", "fechado", "terminal", "id_sync", "status_sync" };
            
            Console.WriteLine(Validation.RandomSecurity());

            GetDataAsync("CAIXA");
        }
        
        private async Task GetDataAsync(string Table)
        {
            var baseQuery = connect.Query().From(Table).Where("status_sync", "CREATE").OrWhereNull("status_sync");

            // CREATE = criar no banco online!
            var data = await baseQuery.Clone().From(Table).GetAsync();
            if (data != null)
            {
                foreach (dynamic item in data)
                {
                    Console.WriteLine(item.Keys());
                    Console.WriteLine(item.ID);
                }
            }
        }

        private void Eventos()
        {
            //GetCaixaAsync();

            Shown += (s, e) =>
            {
                backWork.RunWorkerAsync();
            };

            backWork.DoWork += (s, e) =>
            {

            };

            backWork.RunWorkerCompleted += (s, e) =>
            {

            };
        }
    }
}
