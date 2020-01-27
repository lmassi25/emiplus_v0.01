using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.View.Common;
using SqlKata.Execution;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Emiplus.View.Produtos
{
    public partial class AddServicos : Form
    {
        #region V

        public static int idPdtSelecionado { get; set; }
        private Item _modelItem = new Item();
        private Controller.Item _controllerItem = new Controller.Item();

        private BackgroundWorker backOn = new BackgroundWorker();

        #endregion V

        public AddServicos()
        {
            InitializeComponent();
            Eventos();

            nome.Select();
        }

        private void Start()
        {
            ToolHelp.Show("Descreva seu serviço... Lembre-se de utilizar as características do serviço.", pictureBox5, ToolHelp.ToolTipIcon.Info, "Ajuda!");
        }

        private void LoadEstoque()
        {
            _modelItem = _modelItem.FindById(idPdtSelecionado).FirstOrDefault<Item>();
        }

        private void LoadData()
        {
            _modelItem = _modelItem.FindById(idPdtSelecionado).FirstOrDefault<Item>();

            nome.Text = _modelItem?.Nome ?? "";
            referencia.Text = _modelItem?.Referencia ?? "";
            valorcompra.Text = Validation.Price(_modelItem.ValorCompra);
            valorvenda.Text = Validation.Price(_modelItem.ValorVenda);
        }

        private void Save()
        {
            if (!string.IsNullOrEmpty(nome.Text))
            {
                var data = _modelItem.Query().Where("id", "!=", idPdtSelecionado).Where("tipo", "Servicos").Where("nome", nome.Text).Where("excluir", 0).FirstOrDefault();
                if (data != null)
                {
                    Alert.Message("Oppss", "Já existe um serviço cadastrado com esse NOME.", Alert.AlertType.error);
                    return;
                }
            }

            if (!string.IsNullOrEmpty(referencia.Text))
            {
                var data = _modelItem.Query().Where("id", "!=", idPdtSelecionado).Where("referencia", referencia.Text).Where("excluir", 0).FirstOrDefault();
                if (data != null)
                {
                    Alert.Message("Oppss", "Já existe um serviço cadastrado com essa referência.", Alert.AlertType.error);
                    return;
                }
            }

            _modelItem.Id = idPdtSelecionado;
            _modelItem.Tipo = "Servicos";
            _modelItem.Nome = nome.Text;
            _modelItem.Referencia = referencia.Text;
            _modelItem.ValorCompra = Validation.ConvertToDouble(valorcompra.Text);
            _modelItem.ValorVenda = Validation.ConvertToDouble(valorvenda.Text);

            if (_modelItem.Save(_modelItem))
                Close();
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
                this.BeginInvoke((MethodInvoker)delegate
                {
                    idPdtSelecionado = AddServicos.idPdtSelecionado;
                    backOn.RunWorkerAsync();
                });
            };

            label6.Click += (s, e) => Close();
            btnExit.Click += (s, e) =>
            {
                var dataProd = _modelItem.Query().Where("id", idPdtSelecionado).Where("atualizado", "01.01.0001, 00:00:00.000").FirstOrDefault();
                if (dataProd != null)
                {
                    var result = AlertOptions.Message("Atenção!", "Esse serviço não foi editado, deseja deletar?", AlertBig.AlertType.info, AlertBig.AlertBtn.YesNo);
                    if (result)
                    {
                        var data = _modelItem.Remove(idPdtSelecionado);
                        if (data)
                            Close();
                    }

                    nome.Focus();
                }

                Close();
            };

            btnSalvar.Click += (s, e) => Save();
            btnRemover.Click += (s, e) =>
            {
                var result = AlertOptions.Message("Atenção!", "Você está prestes a deletar um serviço, continuar?", AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                if (result)
                {
                    var data = _modelItem.Remove(idPdtSelecionado);
                    if (data)
                        Close();
                }
            };

            valorcompra.TextChanged += (s, e) =>
            {
                TextBox txt = (TextBox)s;
                Masks.MaskPrice(ref txt);
            };

            valorvenda.TextChanged += (s, e) =>
            {
                TextBox txt = (TextBox)s;
                Masks.MaskPrice(ref txt);
            };

            nome.KeyPress += (s, e) => Masks.MaskMaxLength(s, e, 100);

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");

            backOn.DoWork += (s, e) =>
            {
                _modelItem = _modelItem.FindById(idPdtSelecionado).FirstOrDefault<Item>();
            };

            backOn.RunWorkerCompleted += (s, e) =>
            {
                Start();

                if (idPdtSelecionado > 0)
                {
                    LoadData();
                }
                else
                {
                    _modelItem = new Model.Item();
                    _modelItem.Tipo = "Servicos";
                    _modelItem.Id = idPdtSelecionado;
                    if (_modelItem.Save(_modelItem, false))
                    {
                        idPdtSelecionado = _modelItem.GetLastId();
                        LoadData();
                    }
                    else
                    {
                        Alert.Message("Opss", "Erro ao criar.", Alert.AlertType.error);
                        Close();
                    }
                }
            };
        }
    }
}