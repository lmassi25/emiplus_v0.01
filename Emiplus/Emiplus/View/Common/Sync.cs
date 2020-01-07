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

        public Sync()
        {
            InitializeComponent();
            Eventos();

            var teste = new ConnectOnline().Open();
            //var users = teste.Query().From("sys_caixa").Get();

            var cols = new[] { "id", "id_empresa", "tipo", "excluir", "criado", "atualizado", "deletado", "usuario", "saldo_inicial", "saldo_final", "saldo_final_informado", "observacao", "fechado", "terminal" };
            //teste.Query("sys_caixa").AsInsert(cols, new Model.Caixa().FindAll().Get());
            //var co = new Model.Caixa().Query().Limit(100);
            var cos2 = new Model.Caixa().Query().Limit(100).Get();
            //var s = ConvertListToObject(cos2);
            //var result = cos2.Cast<IEnumerable<object>>().ToList();
            //var affected = teste.Query("caixa").Insert(cols, co);
            //var affected2 = teste.Query("caixa").AsInsert(cols, co);

            //var cols = new[] { "id", "id_empresa" };

            var data = new[] {
    new object[] { 1000, "A", "", "", "", "", "", "", "", "", "", "", "", "" },
    new object[] { 1000, "B", "", "", "", "", "", "", "", "", "", "", "", "" },
    new object[] { 1000, "C", "", "", "", "", "", "", "", "", "", "", "", "" },
    new object[] { 1000, "D", "", "", "", "", "", "", "", "", "", "", "", "" },
};

            //teste.Query("caixa").Insert(cols, cos2);
            Console.WriteLine(Validation.RandomSecurity());
        }

        private void Eventos()
        {
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
