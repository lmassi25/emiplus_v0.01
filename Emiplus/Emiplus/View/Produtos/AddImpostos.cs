using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Produtos
{
    public partial class AddImpostos : Form
    {
        public AddImpostos()
        {
            InitializeComponent();
        }

        private void AddImpostos_Load(object sender, EventArgs e)
        {
            Icms.DataSource = new List<string> {
                "00 - Tributação integralmente",
                "10 - Tributação com cobrança do ICMS por substituição tributária",
                "20 - Tributação com redução de base de cálculo",
                "30 - Tributação Isenta ou não tributada e com cobrança do ICMS por substituição tributária ",
                "40 - Tributação Isenta",
                "41 - Não Tributada",
                "50 - Tributação Suspensa",
                "51 - Tributação com Diferimento",
                "60 - Tributação ICMS cobrado anteriormente por substituição tributária",
                "70 - Tributação ICMS com redução de base de cálculo e cobrança do ICMS por substituição tributária",
                "90 - Tributação ICMS: Outros",

                "101 - Tributada pelo Simples Nacional com permissão de crédito",
                "102 - Tributada pelo Simples Nacional sem permissão de crédito",
                "103 - Isenção do ICMS no Simples Nacional para faixa de receita bruta",
                "201 - Tributada pelo Simples Nacional com permissão de crédito e com cobrança do ICMS por Substituição Tributária",
                "202 - Tributada pelo Simples Nacional sem permissão de crédito e com cobrança do ICMS por Substituição Tributária",
                "203 - Isenção do ICMS nos Simples Nacional para faixa de receita bruta e com cobrança do ICMS por Substituição Tributária",
                "300 - Imune",
                "400 - Não tributada pelo Simples Nacional",
                "500 - ICMS cobrado anteriormente por substituição tributária (substituído) ou por antecipação",
                "900 - Outros"
            };

            //IPI.data
        }
    }
}
