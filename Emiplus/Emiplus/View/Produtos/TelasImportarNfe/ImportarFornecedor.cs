using Emiplus.Data.Helpers;
using System.Windows.Forms;

namespace Emiplus.View.Produtos.TelasImportarNfe
{
    public partial class ImportarFornecedor : Form
    {
        private ImportarNfe dataNfe = new ImportarNfe();

        public ImportarFornecedor()
        {
            InitializeComponent();
            Eventos();
        }

        private void Eventos()
        {
            Load += (s, e) =>
            {
                var dadosFornecedor = dataNfe.GetNotas();

                if (dadosFornecedor.Count > 0)
                {
                    foreach (Controller.ImportarNfe item in dadosFornecedor)
                    {
                        cnpj.Text = item.GetFornecedor().CPFcnpj;
                        IE.Text = item.GetFornecedor().IE;
                        razaosocial.Text = item.GetFornecedor().razaoSocial;

                        rua.Text = item.GetFornecedor().Addr_Rua + " " + item.GetFornecedor().Addr_Nr;
                        bairro.Text = item.GetFornecedor().Addr_Bairro;
                        cep.Text = item.GetFornecedor().Addr_CEP;
                        cidade.Text = item.GetFornecedor().Addr_Cidade;
                        estado.Text = item.GetFornecedor().Addr_UF;
                    }
                }
            };

            btnAvancar.Click += (s, e) =>
            {
                OpenForm.Show<TelasImportarNfe.ImportarProdutos>(this);
            };

            btnBack.Click += (s, e) =>
            {
                Close();
            };
        }
    }
}