using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using System;
using System.Windows.Forms;

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
            ToolHelp.Show("Atribua um limite para lançar descontos a este item. O Valor irá influenciar nos descontos em reais e porcentagens.", pictureBox11, ToolHelp.ToolTipIcon.Info, "Ajuda!");

            Shown += (s, e) =>
            {
                if (!String.IsNullOrEmpty(IniFile.Read("RetomarVenda", "Comercial")))
                {
                    if (IniFile.Read("RetomarVenda", "Comercial") == "True")
                        retomarVendaInicio.Toggled = true;
                    else
                        retomarVendaInicio.Toggled = false;
                }

                if (!String.IsNullOrEmpty(IniFile.Read("LimiteDesconto", "Comercial")))
                    txtLimiteDesconto.Text = Validation.Price(Validation.ConvertToDouble(IniFile.Read("LimiteDesconto", "Comercial")));
            };

            retomarVendaInicio.Click += (s, e) =>
            {
                if (retomarVendaInicio.Toggled)
                    IniFile.Write("RetomarVenda", "False", "Comercial");
                else
                    IniFile.Write("RetomarVenda", "True", "Comercial");
            };

            txtLimiteDesconto.Leave += (s, e) => IniFile.Write("LimiteDesconto", Validation.ConvertToDouble(txtLimiteDesconto.Text).ToString(), "Comercial");
            txtLimiteDesconto.TextChanged += (s, e) =>
            {
                TextBox txt = (TextBox)s;
                Masks.MaskPrice(ref txt);
            };

            btnExit.Click += (s, e) => Close();
        }
    }
}