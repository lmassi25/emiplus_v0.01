using System.IO;
using System.Windows.Forms;
using Emiplus.Controller;
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.View.Common;

namespace Emiplus.View.Configuracoes
{
    public partial class Cfesat : Form
    {
        public Cfesat()
        {
            InitializeComponent();

            Start();
            Eventos();
        }

        public void Start()
        {
            servidor.Items.Add("Homologacao");
            servidor.Items.Add("Producao");
            impressora.DataSource = Support.GetImpressoras();

            if (!string.IsNullOrEmpty(IniFile.Read("Servidor", "SAT")))
                servidor.SelectedItem = IniFile.Read("Servidor", "SAT");

            if (!string.IsNullOrEmpty(IniFile.Read("Printer", "SAT")))
                impressora.SelectedItem = IniFile.Read("Printer", "SAT");

            if (!string.IsNullOrEmpty(IniFile.Read("N_Serie", "SAT")))
                serie.Text = IniFile.Read("N_Serie", "SAT");
        }

        private void checkXml()
        {
            
        }

        private SqlKata.Query getListXml(string dataInicial, string dataFinal)
        {
            var query = new Model.Nota().Query();

            query
                .LeftJoin("pedido", "pedido.id", "nota.id_pedido")
                .LeftJoin("pessoa", "pessoa.id", "pedido.cliente")
                .LeftJoin("usuarios as colaborador", "colaborador.id_user", "pedido.colaborador")
                .LeftJoin("usuarios as usuario", "usuario.id_user", "pedido.id_usuario")
                .Select("pedido.id", "pedido.tipo", "pedido.emissao", "pedido.total", "pessoa.nome",
                    "colaborador.nome as colaborador", "usuario.nome as usuario", "pedido.criado", "pedido.excluir",
                    "pedido.status", "nota.nr_nota as nfe", "nota.serie", "nota.status as statusnfe",
                    "nota.tipo as tiponfe", "nota.id as idnota", "nota.criado as criadonota",
                    "nota.CHAVEDEACESSO as chavedeacesso");

            query.Where("nota.criado", ">=", Validation.ConvertDateToSql(dataInicial, true)).Where("nota.criado", "<=", Validation.ConvertDateToSql(dataFinal + " 23:59", true));

            query.Where("pedido.excluir", 0);

            query.Where("nota.tipo", "CFe");
            query.Where(
                q => q.Where("nota.status", "<>", "Pendente").Where("nota.status", "<>", "Falha")
            );

            query.OrderByDesc("nota.id");

            return query;
        }

        /// <summary>
        ///     Eventos do form
        /// </summary>
        public void Eventos()
        {
            logs.Click += (s, e) =>
            {
                var f = new Cfesat_logs();
                Cfesat_logs.tipo = 0;
                f.ShowDialog();
            };

            consultarsat.Click += (s, e) =>
            {
                if (!File.Exists("Sat.Dll"))
                {
                    Alert.Message("Opps", "Não encontramos a DLL do SAT", Alert.AlertType.warning);
                    return;
                }

                AlertOptions.Message("Retorno", new Controller.Fiscal().Consulta(), AlertBig.AlertType.info,
                    AlertBig.AlertBtn.OK);
            };

            consultarstatus.Click += (s, e) =>
            {
                var f = new Cfesat_logs();
                Cfesat_logs.tipo = 1;
                f.ShowDialog();
            };

            base64.Click += (s, e) =>
            {
                var f = new Cfesat_base64();
                f.ShowDialog();
            };

            xml.Click += (s, e) =>
            {
                checkXml();
            };

            servidor.Leave += (s, e) => IniFile.Write("Servidor", servidor.Text, "SAT");
            impressora.SelectedIndexChanged +=
                (s, e) => IniFile.Write("Printer", impressora.SelectedItem.ToString(), "SAT");
            serie.Leave += (s, e) => IniFile.Write("N_Serie", serie.Text, "SAT");

            btnExit.Click += (s, e) => Close();
        }
    }
}