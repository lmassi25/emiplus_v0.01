using System.Collections.Generic;
using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.View.Common;
using SqlKata.Execution;

namespace Emiplus.View.Comercial
{
    public partial class AddClienteEndereco : Form
    {
        private PessoaEndereco _modelAddress = new PessoaEndereco();
        private string cep_aux;
        private readonly int IdAddress = AddClientes.IdAddress;
        private readonly int IdPessoa = AddClientes.Id;
        private PessoaEndereco retorno = new PessoaEndereco();

        public AddClienteEndereco()
        {
            InitializeComponent();
            Eventos();
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
            Masks.SetToUpper(this);

            Shown += (s, e) =>
            {
                if (!IdPessoa.IsNumber() && IdPessoa == 0)
                {
                    Alert.Message("Opss", "Não foi possível, tente novamente.", Alert.AlertType.error);
                    Close();
                }

                pais.DataSource = new List<string> {"Brasil"};
                estado.DataSource = Support.GetEstados();

                if (IdAddress <= 0)
                    return;

                _modelAddress = _modelAddress.FindById(IdAddress).FirstOrDefault<PessoaEndereco>();

                cep.Text = _modelAddress.Cep ?? "";
                rua.Text = _modelAddress.Rua ?? "";
                bairro.Text = _modelAddress.Bairro ?? "";
                cidade.Text = _modelAddress.Cidade ?? "";
                nr.Text = _modelAddress.Nr ?? "";
                complemento.Text = _modelAddress.Complemento ?? "";
                estado.Text = _modelAddress.Estado ?? "";
                pais.Text = _modelAddress.Pais ?? "";
                ibge.Text = _modelAddress.IBGE ?? "";
            };

            btnAddrSalvar.Click += (s, e) =>
            {
                _modelAddress.Id = IdAddress;
                _modelAddress.Id_pessoa = IdPessoa;
                _modelAddress.Cep = cep.Text;
                _modelAddress.Rua = rua.Text;
                _modelAddress.Bairro = bairro.Text;
                _modelAddress.Nr = nr.Text;
                _modelAddress.Complemento = complemento.Text;
                _modelAddress.Estado = estado.Text;
                _modelAddress.Pais = pais.Text;
                _modelAddress.IBGE = ibge.Text;

                if (!_modelAddress.Save(_modelAddress))
                    return;

                DialogResult = DialogResult.OK;
                Close();
            };

            btnAddrDelete.Click += (s, e) =>
            {
                var result = AlertOptions.Message("Atenção!", "Deseja realmente excluir o endereço?", AlertBig.AlertType.info, AlertBig.AlertBtn.YesNo);
                if (!result)
                    return;

                if (!_modelAddress.Remove(IdAddress))
                    return;

                DialogResult = DialogResult.OK;
                Close();
            };

            cep.KeyPress += Masks.MaskCEP;
            buscarEndereco.Click += (s, e) =>
            {
                buscarEndereco.Enabled = false;

                rua.Text = "...";
                bairro.Text = "...";
                nr.Text = "...";
                complemento.Text = "...";
                ibge.Text = "...";

                rua.Enabled = false;
                bairro.Enabled = false;
                nr.Enabled = false;
                complemento.Enabled = false;
                cidade.Enabled = false;
                estado.Enabled = false;
                pais.Enabled = false;
                ibge.Enabled = false;

                if (string.IsNullOrEmpty(cep.Text))
                    return;

                if (cep.Text.Replace("-", "").Length != 8)
                    return;

                cep_aux = cep.Text.Replace("-", "");

                backgroundWorker1.RunWorkerAsync();
            };

            FormClosing += (s, e) => DialogResult = DialogResult.OK;

            backgroundWorker1.DoWork += (s, e) => retorno = _modelAddress.GetAddr(cep_aux);
            backgroundWorker1.RunWorkerCompleted += (s, e) =>
            {
                if (retorno != null)
                {
                    rua.Text = "";
                    bairro.Text = "";
                    nr.Text = "";
                    complemento.Text = "";
                    ibge.Text = "";

                    rua.Text = retorno.Rua;
                    bairro.Text = retorno.Bairro;
                    cidade.Text = retorno.Cidade;
                    estado.SelectedItem = retorno.Estado;
                    ibge.Text = retorno.IBGE;
                }

                rua.Enabled = true;
                bairro.Enabled = true;
                nr.Enabled = true;
                complemento.Enabled = true;
                cidade.Enabled = true;
                estado.Enabled = true;
                pais.Enabled = true;
                ibge.Enabled = true;

                rua.Select();

                buscarEndereco.Enabled = true;
            };

            rua.KeyPress += (s, e) => Masks.MaskOnlyNumberAndCharAndMore(s, e, 50);
            nr.KeyPress += (s, e) => Masks.MaskOnlyNumberAndCharAndMore(s, e, 10);
            bairro.KeyPress += (s, e) => Masks.MaskOnlyNumberAndCharAndMore(s, e, 30);
            complemento.KeyPress += (s, e) => Masks.MaskOnlyNumberAndCharAndMore(s, e, 50);
            cidade.KeyPress += (s, e) => Masks.MaskOnlyNumberAndCharAndMore(s, e, 50);
            ibge.KeyPress += (s, e) => Masks.MaskOnlyNumbers(s, e, 50);
        }
    }
}