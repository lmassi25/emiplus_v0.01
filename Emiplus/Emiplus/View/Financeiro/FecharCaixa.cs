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

namespace Emiplus.View.Financeiro
{
    public partial class FecharCaixa : Form
    {
        public static int idCaixa { get; set; }
        private Controller.Caixa _controllerCaixa = new Controller.Caixa();
        private Model.Caixa _modelCaixa = new Model.Caixa();
        private Model.CaixaMovimentacao _modelCaixaMov = new Model.CaixaMovimentacao();

        public FecharCaixa()
        {
            InitializeComponent();
            Eventos();
        }

        private void LoadData()
        {
            _modelCaixa = _modelCaixa.FindById(idCaixa).FirstOrDefault<Model.Caixa>();

            //txtSaldoDinheiro.Text = Validation.FormatPrice(_controllerCaixa.SumDinheiro(idCaixa), true);
            //txtSaldoTotal.Text = Validation.FormatPrice(_controllerCaixa.SumTotal(idCaixa), true);
        }

        private void Eventos()
        {
            Load += (s, e) =>
            {
                LoadData();
            };

            btnFinalizar.Click += (s, e) =>
            {
                _modelCaixa.Tipo = "Fechado";
                _modelCaixa.Fechado = DateTime.Now;
                if (_modelCaixa.Save(_modelCaixa, false))
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            };

            btnFinalizarImprimir.Click += (s, e) =>
            {

            };

            btnCancelar.Click += (s, e) => Close();
        }
    }
}
