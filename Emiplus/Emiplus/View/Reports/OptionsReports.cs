using Emiplus.Data.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Reports
{
    public partial class OptionsReports : Form
    {
        public bool TodosRegistros { get; set; }
        public int NrRegistros { get; set; }
        public string OrdemBy { get; set; }

        public OptionsReports()
        {
            InitializeComponent();
            Eventos();
        }

        private void Eventos()
        {
            Load += (s, e) =>
            {
                ToolHelp.Show("Habilite essa opção para exibir todos os registros disponíveis.", pictureBox5, ToolHelp.ToolTipIcon.Info, "Ajuda!");
                ToolHelp.Show("Defina um limite de registros a serem exibidos, é necessário desativar a opção acima.", pictureBox1, ToolHelp.ToolTipIcon.Info, "Ajuda!");
            
                nrRegistros.Text = "50";
                Ordem.SelectedItem = "Z-A";
            };

            btnGerar.Click += (s, e) =>
            {
                if (!btnExibirTodos.Toggled) {
                    if (Validation.ConvertToDouble(nrRegistros.Text) <= 0)
                    {
                        Alert.Message("Opps", "Limite de registros inválido.", Alert.AlertType.error);
                        return;
                    }
                }

                TodosRegistros = btnExibirTodos.Toggled;
                NrRegistros = Validation.ConvertToInt32(nrRegistros.Text);
                OrdemBy = Ordem.Text;

                DialogResult = DialogResult.OK;
                Close();
            };

            btnExibirTodos.Click += (s, e) =>
            {
                if (!btnExibirTodos.Toggled)
                    nrRegistros.Enabled = false;
                else
                    nrRegistros.Enabled = true;
            };

            nrRegistros.KeyPress += (s, e) => Masks.MaskOnlyNumbers(s, e, 20);
        }
    }
}
