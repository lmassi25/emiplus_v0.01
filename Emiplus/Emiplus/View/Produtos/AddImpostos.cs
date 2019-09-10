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

            Ipi.DataSource = new List<string> {
                
                //IPITrib

                "00 - Tributado: Entrada com recuperação de crédito",
                "49 - Tributado: Outras entradas",
                "50 - Tributado: Saída tributada",
                "99 - Tributado: Outras saídas",

                //IPINT

                "01 - Não Tributado: Entrada tributada com alíquota zero",
                "02 - Não Tributado: Entrada isenta",
                "03 - Não Tributado: Entrada não-tributada",
                "04 - Não Tributado: Entrada imune",
                "05 - Não Tributado: Entrada com suspensão",
                "51 - Não Tributado: Saída tributada com alíquota zero",
                "52 - Não Tributado: Saída isenta",
                "53 - Não Tributado: Saída não-tributada",
                "54 - Não Tributado: Saída imune",
                "55 - Não Tributado: Saída com suspensão"
            };

            Pis.DataSource = new List<string> {
                
                //PISAliq

                "01 - Tributado pela Alíquota: Operação Tributável (base de cálculo = valor da operação alíquota normal (cumulativo/não cumulativo))",
                "02 - Tributado pela Alíquota: Operação Tributável (base de cálculo = valor da operação (alíquota diferenciada))",

                //PISQtde

                "03 - Tributado pela Quantidade: Operação Tributável (base de cálculo = quantidade vendida x alíquota por unidade de produto)",

                //PISNT

                "04 - Não Tributado: Operação Tributável (tributação monofásica (alíquota zero))",
                "05 - Não Tributado: Operação Tributável (Substituição Tributária)",
                "06 - Não Tributado: Operação Tributável (alíquota zero)",
                "07 - Não Tributado: Operação Isenta da Contribuição",
                "08 - Não Tributado: Operação Sem Incidência da Contribuição",
                "09 - Não Tributado: Operação com Suspensão da Contribuição",
                
                //PISOutr

                "49 - Outras Operações: Outras Operações de Saída",
                "50 - Outras Operações: Operação com Direito a Crédito - Vinculada Exclusivamente a Receita Tributada no Mercado Interno",
                "51 - Outras Operações: Operação com Direito a Crédito - Vinculada Exclusivamente a Receita Não Tributada no Mercado Interno",
                "52 - Outras Operações: Operação com Direito a Crédito – Vinculada Exclusivamente a Receita de Exportação",
                "53 - Outras Operações: Operação com Direito a Crédito - Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno",
                "54 - Outras Operações: Operação com Direito a Crédito -Vinculada a Receitas Tributadas no Mercado Interno e de Exportação",
                "55 - Outras Operações: Operação com Direito a Crédito -Vinculada a Receitas NãoTributadas no Mercado Interno e de Exportação",
                "56 - Outras Operações: Operação com Direito a Crédito -Vinculada a Receitas Tributadas e Não - Tributadas no Mercado Interno, e de Exportação",
                "60 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita Tributada no Mercado Interno",
                "61 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita Não - Tributada no Mercado Interno",
                "62 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita de Exportação",
                "63 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno",
                "64 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas no Mercado Interno e de Exportação",
                "65 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada a Receitas Não-Tributadas no Mercado Interno e de Exportação",
                "66 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno, e de Exportação",
                "67 - Outras Operações: Crédito Presumido - Outras Operações",
                "70 - Outras Operações: Operação de Aquisição sem Direito a Crédito",
                "71 - Outras Operações: Operação de Aquisição com Isenção",
                "72 - Outras Operações: Operação de Aquisição com Suspensão",
                "73 - Outras Operações: Operação de Aquisição a Alíquota Zero",
                "74 - Outras Operações: Operação de Aquisição; sem Incidência da Contribuição",
                "75 - Outras Operações: Operação de Aquisição por Substituição Tributária",
                "98 - Outras Operações: Outras Operações de Entrada",
                "99 - Outras Operações: Outras Operações",
            };

            //PisSt.DataSource = new List<string>{ }

            Cofins.DataSource = new List<string> {
                
                //COFINSAliq

                "01 - Tributado pela Alíquota: Operação Tributável (base de cálculo = valor da operação alíquota normal (cumulativo/não cumulativo))",
                "02 - Tributado pela Alíquota: Operação Tributável (base de cálculo = valor da operação (alíquota diferenciada))",

                //COFINSQtde

                "03 - Tributado pela Quantidade: Operação Tributável (base de cálculo = quantidade vendida x alíquota por unidade de produto)",

                //COFINSNT

                "04 - Não Tributado: Operação Tributável (tributação monofásica (alíquota zero))",
                "05 - Não Tributado: Operação Tributável (Substituição Tributária)",
                "06 - Não Tributado: Operação Tributável (alíquota zero)",
                "07 - Não Tributado: Operação Isenta da Contribuição",
                "08 - Não Tributado: Operação Sem Incidência da Contribuição",
                "09 - Não Tributado: Operação com Suspensão da Contribuição",
                
                //COFINSOutr

                "49 - Outras Operações: Outras Operações de Saída",
                "50 - Outras Operações: Operação com Direito a Crédito - Vinculada Exclusivamente a Receita Tributada no Mercado Interno",
                "51 - Outras Operações: Operação com Direito a Crédito - Vinculada Exclusivamente a Receita Não Tributada no Mercado Interno",
                "52 - Outras Operações: Operação com Direito a Crédito – Vinculada Exclusivamente a Receita de Exportação",
                "53 - Outras Operações: Operação com Direito a Crédito - Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno",
                "54 - Outras Operações: Operação com Direito a Crédito -Vinculada a Receitas Tributadas no Mercado Interno e de Exportação",
                "55 - Outras Operações: Operação com Direito a Crédito -Vinculada a Receitas NãoTributadas no Mercado Interno e de Exportação",
                "56 - Outras Operações: Operação com Direito a Crédito -Vinculada a Receitas Tributadas e Não - Tributadas no Mercado Interno, e de Exportação",
                "60 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita Tributada no Mercado Interno",
                "61 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita Não - Tributada no Mercado Interno",
                "62 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita de Exportação",
                "63 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno",
                "64 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas no Mercado Interno e de Exportação",
                "65 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada a Receitas Não-Tributadas no Mercado Interno e de Exportação",
                "66 - Outras Operações: Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno, e de Exportação",
                "67 - Outras Operações: Crédito Presumido - Outras Operações",
                "70 - Outras Operações: Operação de Aquisição sem Direito a Crédito",
                "71 - Outras Operações: Operação de Aquisição com Isenção",
                "72 - Outras Operações: Operação de Aquisição com Suspensão",
                "73 - Outras Operações: Operação de Aquisição a Alíquota Zero",
                "74 - Outras Operações: Operação de Aquisição; sem Incidência da Contribuição",
                "75 - Outras Operações: Operação de Aquisição por Substituição Tributária",
                "98 - Outras Operações: Outras Operações de Entrada",
                "99 - Outras Operações: Outras Operações",
            };

            //CofinsSt.DataSource = new List<string>{ }

            icms_1.Visible = false;
            icms_2.Visible = false;
            icms_3.Visible = false;

            icms_1.Location = new Point(9, 62);
            icms_2.Location = new Point(9, 62);
            icms_3.Location = new Point(9, 62);

            icms_1.Size = new Size(196, 67);
            icms_2.Size = new Size(319, 158);
            icms_3.Size = new Size(214, 117);
        }


        private void SetIcms()
        {
            icms_1.Visible = false;
            icms_2.Visible = false;
            icms_3.Visible = false;

            if (Icms.Text.Contains("101"))
            {
                icms_1.Visible = true;
            }
            else if (Icms.Text.Contains("201"))
            {
                icms_2.Visible = true;
            }
            else if (Icms.Text.Contains("202"))
            {
                icms_2.Visible = true;
            }
            else if (Icms.Text.Contains("203"))
            {
                icms_2.Visible = true;
            }
            else if (Icms.Text.Contains("500"))
            {
                icms_3.Visible = true;
            }
            else if (Icms.Text.Contains("900"))
            {
                icms_2.Visible = true;
            }
        }

        private void Icms_SelectedValueChanged(object sender, EventArgs e)
        {
            SetIcms();
        }
    }
}
