using Emiplus.Data.Database;
using Emiplus.Data.Helpers;
using System.Windows.Forms;

namespace Emiplus.View.Configuracoes
{
    public partial class Database : Form
    {
        public Database()
        {
            InitializeComponent();
            Eventos();
        }
        
        /// <summary>
        /// Eventos do form
        /// </summary>
        public void Eventos()
        {
            Atualiza.Click += (s, e) =>
            {
                new CreateTables().CheckTables();

                Alert.Message("Pronto!", "Banco de dados atualizado com sucesso!", Alert.AlertType.success);
            };

            btnExit.Click += (s, e) => Close();
        }
    }
}
