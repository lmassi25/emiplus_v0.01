using System.Collections;
using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.View.Produtos.TelasImportarNfe;

namespace Emiplus.View.Produtos
{
    public partial class ImportarNfe : Form
    {
        private Controller.ImportarNfe dataNfe;

        private readonly OpenFileDialog ofd = new OpenFileDialog();

        public ImportarNfe()
        {
            InitializeComponent();
            Eventos();
        }

        private bool MultipleImports { get; set; }
        private static ArrayList notas { get; set; }

        /// <summary>
        ///     1 - Importar produtos
        ///     2 - Maipular Estoque
        ///     3 - Importar Compra
        /// </summary>
        public static int optionSelected { get; set; }

        public ArrayList GetNotas()
        {
            return notas;
        }

        private void LoadDadosNota()
        {
            GridLista.Rows.Clear();

            var dataNotas = GetNotas();
            foreach (Controller.ImportarNfe item in dataNotas)
                GridLista.Rows.Add(
                    item.GetDados().Emissao,
                    item.GetDados().Nr,
                    item.GetDados().Id
                );

            dadosNota.Visible = true;
            btnAvancar.Visible = true;
        }

        private void Eventos()
        {
            Load += (s, e) =>
            {
                Resolution.SetScreenMaximized(this);

                MultipleImports = true;
                optionSelected = 1;
            };

            Shown += (s, e) =>
            {
                Refresh();

                ToolHelp.Show("Opção para importar todos os produtos da nota.", pictureBox7, ToolHelp.ToolTipIcon.Info,
                    "Ajuda!");
                ToolHelp.Show(
                    "Opção para manipular o estoque dos produtos contidos na nota que já estão cadastrado no sistema.",
                    pictureBox2, ToolHelp.ToolTipIcon.Info, "Ajuda!");
                ToolHelp.Show("Opção para importar toda a nota.", pictureBox4, ToolHelp.ToolTipIcon.Info, "Ajuda!");
            };

            btnSelecinarNfe.Click += (s, e) =>
            {
                ofd.RestoreDirectory = true;
                ofd.DefaultExt = "xml";
                ofd.Filter = @"XML|*.xml";
                ofd.CheckFileExists = true;
                ofd.CheckPathExists = true;
                ofd.Multiselect = MultipleImports;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    notas = new ArrayList();
                    foreach (var file in ofd.FileNames)
                    {
                        dataNfe = new Controller.ImportarNfe(file);
                        notas.Add(dataNfe);
                    }

                    LoadDadosNota();
                }
            };

            Op1.Click += (s, e) =>
            {
                MultipleImports = true;
                optionSelected = 1;
            };

            Op2.Click += (s, e) =>
            {
                MultipleImports = true;
                optionSelected = 2;
            };

            Op3.Click += (s, e) =>
            {
                MultipleImports = false;
                optionSelected = 3;
            };

            btnAvancar.Click += (s, e) =>
            {
                if (optionSelected == 1 || optionSelected == 2)
                {
                    OpenForm.Show<ImportarProdutos>(this);
                    return;
                }

                OpenForm.Show<ImportarFornecedor>(this);
            };
        }
    }
}