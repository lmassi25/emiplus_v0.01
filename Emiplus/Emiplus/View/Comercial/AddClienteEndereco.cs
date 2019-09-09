using Emiplus.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emiplus.Data.Helpers;
using SqlKata.Execution;

namespace Emiplus.View.Comercial
{
    public partial class AddClienteEndereco : Form
    {
        private int IdAddress = AddClientes.IdAddress;
        private int IdPessoa = AddClientes.Id;
        private PessoaEndereco _modelAddress = new PessoaEndereco();

        public AddClienteEndereco()
        {
            InitializeComponent();

            if (!Validation.IsNumber(IdPessoa) && IdPessoa == 0)
            {
                Alert.Message("Opss", "Não foi possível, tente novamente.", Alert.AlertType.error);
                Close();
            }

            if (IdAddress > 0)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            _modelAddress = _modelAddress.FindById(IdAddress).First<PessoaEndereco>();

            titulo.Text = _modelAddress.Titulo;
            cep.Text = _modelAddress.Cep;
            rua.Text = _modelAddress.Rua;
            bairro.Text = _modelAddress.Bairro;
            nr.Text = _modelAddress.Nr;
            complemento.Text = _modelAddress.Complemento;
            estado.Text = _modelAddress.Estado;
            pais.Text = _modelAddress.Pais;
            ibge.Text = _modelAddress.IBGE;
        }

        private void BtnAddrCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnAddrSalvar_Click(object sender, EventArgs e)
        {
            _modelAddress.Id = IdAddress;
            _modelAddress.Id_pessoa = IdPessoa;
            _modelAddress.Titulo = titulo.Text;
            _modelAddress.Cep = cep.Text;
            _modelAddress.Rua = rua.Text;
            _modelAddress.Bairro = bairro.Text;
            _modelAddress.Nr = nr.Text;
            _modelAddress.Complemento = complemento.Text;
            _modelAddress.Estado = estado.Text;
            _modelAddress.Pais = pais.Text;
            _modelAddress.IBGE = ibge.Text;

            if (_modelAddress.Save(_modelAddress))
            {
                Close();
            }
        }
    }
}
