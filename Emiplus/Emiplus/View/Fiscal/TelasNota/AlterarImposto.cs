using Emiplus.Data.Helpers;
using SqlKata.Execution;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Emiplus.View.Fiscal.TelasNota
{
    public partial class AlterarImposto : Form
    {
        private IEnumerable<dynamic> impostos { get; set; }

        public AlterarImposto()
        {
            InitializeComponent();

            impostos = new Model.Imposto().FindAll().WhereFalse("excluir").OrderByDesc("nome").Get();
            if (impostos.Count() > 0)
            {
                ImpostoNFE.DataSource = impostos;
                ImpostoNFE.DisplayMember = "NOME";
                ImpostoNFE.ValueMember = "ID";
            }

            ImpostoNFE.SelectedValue = 0;

            Eventos();

            ImpostoNFE.Select();
        }
        
        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (Validation.ConvertToInt32(ImpostoNFE.SelectedValue) > 0)
                    {
                        TelaProdutos.idImposto = Validation.ConvertToInt32(ImpostoNFE.SelectedValue);
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    e.SuppressKeyPress = true;
                    break;
                case Keys.Escape:
                    Close();
                    e.SuppressKeyPress = true;
                    break;
            }
        }

        private void Eventos()
        {
            KeyDown += KeyDowns;
            KeyPreview = true;

            btnSelecionar.Click += (s, e) => 
            { 
                if(Validation.ConvertToInt32(ImpostoNFE.SelectedValue) > 0)
                {
                    TelaProdutos.idImposto = Validation.ConvertToInt32(ImpostoNFE.SelectedValue);
                    DialogResult = DialogResult.OK;
                    Close();
                }
            };

            btnCancelar.Click += (s, e) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };
        }
    }
}
