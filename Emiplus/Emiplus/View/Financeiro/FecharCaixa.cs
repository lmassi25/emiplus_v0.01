using System;
using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.View.Common;
using SqlKata.Execution;

namespace Emiplus.View.Financeiro
{
    public partial class FecharCaixa : Form
    {
        private readonly Controller.Caixa _controllerCaixa = new Controller.Caixa();
        private Model.Caixa _modelCaixa = new Model.Caixa();

        public FecharCaixa()
        {
            InitializeComponent();
            Eventos();
        }

        public static int idCaixa { get; set; }

        public static bool fecharImprimir { get; set; }

        private void LoadData()
        {
            _modelCaixa = _modelCaixa.FindById(idCaixa).FirstOrDefault<Model.Caixa>();

            var Dinheiro = Validation.ConvertToDouble(_controllerCaixa.SumEntradasDinheiro(_modelCaixa.Id) +
                                                      _modelCaixa.Saldo_Inicial -
                                                      _controllerCaixa.SumSaidas(_modelCaixa.Id));
            txtSaldoDinheiro.Text = Validation.FormatPrice(Dinheiro, true);
            txtSaldoTotal.Text = Validation.FormatPrice(_controllerCaixa.SumSaldoFinal(idCaixa), true);
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
            }
        }

        private void Eventos()
        {
            KeyDown += KeyDowns;
            KeyPreview = true;

            Load += (s, e) =>
            {
                LoadData();

                // Valor padrão 'false'
                fecharImprimir = false;
            };

            btnFinalizar.Click += (s, e) =>
            {
                _modelCaixa.Tipo = "Fechado";
                _modelCaixa.Fechado = DateTime.Now;
                if (_modelCaixa.Save(_modelCaixa, false))
                {
                    Home.idCaixa = 0;
                    DialogResult = DialogResult.OK;
                    Close();
                }
            };

            btnFinalizarImprimir.Click += (s, e) =>
            {
                _modelCaixa.Tipo = "Fechado";
                _modelCaixa.Fechado = DateTime.Now;
                if (_modelCaixa.Save(_modelCaixa, false))
                {
                    DialogResult = DialogResult.OK;
                    fecharImprimir = true;
                    Close();
                }
            };

            btnCancelar.Click += (s, e) => Close();
        }
    }
}