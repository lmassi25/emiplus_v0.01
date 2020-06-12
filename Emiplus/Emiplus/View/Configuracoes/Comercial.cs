using System.Windows.Forms;
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;

namespace Emiplus.View.Configuracoes
{
    public partial class Comercial : Form
    {
        public Comercial()
        {
            InitializeComponent();
            Eventos();
        }

        public void Eventos()
        {
            ToolHelp.Show(
                "Atribua um limite para lançar descontos a este item. O Valor irá influenciar nos descontos em reais e porcentagens.",
                pictureBox11, ToolHelp.ToolTipIcon.Info, "Ajuda!");
            ToolHelp.Show("Essa opção ativa o controle do estoque, não permitindo vender itens com o estoque zerado.",
                pictureBox1, ToolHelp.ToolTipIcon.Info, "Ajuda!");
            ToolHelp.Show("Ative essa opção para utilizar as mesas predefinidas no sistema.", pictureBox5,
                ToolHelp.ToolTipIcon.Info, "Ajuda!");

            Shown += (s, e) =>
            {
                if (!string.IsNullOrEmpty(IniFile.Read("RetomarVenda", "Comercial")))
                    retomarVendaInicio.Toggled = IniFile.Read("RetomarVenda", "Comercial") == "True";

                if (!string.IsNullOrEmpty(IniFile.Read("ControlarEstoque", "Comercial")))
                    btnControlarEstoque.Toggled = IniFile.Read("ControlarEstoque", "Comercial") == "True";

                if (!string.IsNullOrEmpty(IniFile.Read("LimiteDesconto", "Comercial")))
                    txtLimiteDesconto.Text =
                        Validation.Price(Validation.ConvertToDouble(IniFile.Read("LimiteDesconto", "Comercial")));

                if (!string.IsNullOrEmpty(IniFile.Read("TrocasCliente", "Comercial")))
                    Trocas_01.Toggled = IniFile.Read("TrocasCliente", "Comercial") == "True";

                if (!string.IsNullOrEmpty(IniFile.Read("UserNoDocument", "Comercial")))
                    UserNoDocument.Toggled = IniFile.Read("UserNoDocument", "Comercial") == "True";

                if (!string.IsNullOrEmpty(IniFile.Read("ShowImagePDV", "Comercial")))
                    imgPDV.Toggled = IniFile.Read("ShowImagePDV", "Comercial") == "True";

                if (!string.IsNullOrEmpty(IniFile.Read("Alimentacao", "Comercial")))
                    btnAlimentacao.Toggled = IniFile.Read("Alimentacao", "Comercial") == "True";

                if (!string.IsNullOrEmpty(IniFile.Read("MesasPreCadastrada", "Comercial")))
                    btnMesasPreCadastrada.Toggled = IniFile.Read("MesasPreCadastrada", "Comercial") == "True";

                if (!string.IsNullOrEmpty(IniFile.Read("BaixarEstoqueRemessas", "Comercial")))
                    btnBaixarEstoqueRemessas.Toggled = IniFile.Read("BaixarEstoqueRemessas", "Comercial") == "True";
            };

            retomarVendaInicio.Click += (s, e) =>
            {
                IniFile.Write("RetomarVenda", retomarVendaInicio.Toggled ? "False" : "True", "Comercial");
            };

            btnControlarEstoque.Click += (s, e) =>
            {
                IniFile.Write("ControlarEstoque", btnControlarEstoque.Toggled ? "False" : "True", "Comercial");
            };

            Trocas_01.Click += (s, e) =>
            {
                IniFile.Write("TrocasCliente", Trocas_01.Toggled ? "False" : "True", "Comercial");
            };

            UserNoDocument.Click += (s, e) =>
            {
                IniFile.Write("UserNoDocument", UserNoDocument.Toggled ? "False" : "True", "Comercial");
            };

            imgPDV.Click += (s, e) =>
            {
                IniFile.Write("ShowImagePDV", imgPDV.Toggled ? "False" : "True", "Comercial");
            };

            btnAlimentacao.Click += (s, e) =>
            {
                IniFile.Write("Alimentacao", btnAlimentacao.Toggled ? "False" : "True", "Comercial");
            };

            btnMesasPreCadastrada.Click += (s, e) =>
            {
                IniFile.Write("MesasPreCadastrada", btnMesasPreCadastrada.Toggled ? "False" : "True", "Comercial");
            };

            btnBaixarEstoqueRemessas.Click += (s, e) =>
            {
                IniFile.Write("BaixarEstoqueRemessas", btnBaixarEstoqueRemessas.Toggled ? "False" : "True", "Comercial");
            };

            txtLimiteDesconto.Leave += (s, e) => IniFile.Write("LimiteDesconto",
                Validation.ConvertToDouble(txtLimiteDesconto.Text).ToString(), "Comercial");
            txtLimiteDesconto.TextChanged += (s, e) =>
            {
                var txt = (TextBox) s;
                Masks.MaskPrice(ref txt);
            };

            btnExit.Click += (s, e) => Close();
        }
    }
}