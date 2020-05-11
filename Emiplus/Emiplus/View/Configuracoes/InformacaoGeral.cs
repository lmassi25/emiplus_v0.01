using Emiplus.Data.Helpers;
using Emiplus.Properties;
using System.Windows.Forms;

namespace Emiplus.View.Configuracoes
{
    public partial class InformacaoGeral : Form
    {
        public InformacaoGeral()
        {
            InitializeComponent();
            Eventos();
        }

        private void Eventos()
        {
            Shown += (s, e) =>
            {
                idEmpresa.Text = Settings.Default.empresa_unique_id;
                cnpj.Text = Settings.Default.empresa_cnpj;
                razaosocial.Text = Settings.Default.empresa_razao_social;
                nomefantasia.Text = Settings.Default.empresa_nome_fantasia;
                inscricaoestadual.Text = Settings.Default.empresa_inscricao_estadual;
                inscricaomunicipal.Text = Settings.Default.empresa_inscricao_municipal;

                cep.Text = Settings.Default.empresa_cep;
                rua.Text = Settings.Default.empresa_rua;
                numero.Text = Settings.Default.empresa_nr;
                bairro.Text = Settings.Default.empresa_bairro;
                cidade.Text = Settings.Default.empresa_cidade;
                uf.Text = Settings.Default.empresa_estado;
                ibge.Text = Settings.Default.empresa_ibge;

                if (string.IsNullOrEmpty(Settings.Default.user_plan_id))
                {
                    plano.Text = "Contrate um Plano";
                    recorrencia.Text = "Contrate um Plano";
                    fatura.Visible = false;
                    label3.Visible = false;
                }
                else
                {
                    plano.Text = Validation.FirstCharToUpper(Settings.Default.user_plan_id);
                    recorrencia.Text = Validation.FirstCharToUpper(Settings.Default.user_plan_recorrencia);
                    fatura.Text = Settings.Default.user_plan_fatura;
                }
            };

            btnTrocarPlano.Click += (s, e) => Support.OpenLinkBrowser(Program.URL_BASE + "/admin");

            btnExit.Click += (s, e) => Close();
        }
    }
}