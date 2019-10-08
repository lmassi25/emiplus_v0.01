using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.View.Comercial;
using SqlKata.Execution;
using System;
using System.Windows.Forms;

namespace Emiplus.View.Fiscal.TelasNota
{
    public partial class DetailsClient : Form
    {
        public static int IdClient { get; set; } // id user
        public static int IdAddress { get; private set; }

        private Controller.Pessoa _controller = new Controller.Pessoa();
        private Pessoa _modelPessoa = new Pessoa();

        public DetailsClient()
        {
            InitializeComponent();
            Eventos();
        }

        private void LoadData()
        {
            _modelPessoa = _modelPessoa.FindById(IdClient).First<Pessoa>();

            label3.Text = _modelPessoa?.Nome ?? "";
            label4.Text = _modelPessoa?.Fantasia ?? "";
            label8.Text = _modelPessoa?.CPF ?? "";
            label10.Text = _modelPessoa?.RG ?? "";
            label6.Text = _modelPessoa?.Aniversario ?? "";
        }

        private void SetFocus() => Focus();

        private void DataTableAddress() => _controller.GetDataTableEnderecos(GridLista, IdClient);

        private void GetEndereco(bool create = false)
        {
            if (create)
            {
                AddClientes.Id = IdClient;
                IdAddress = 0;
                AddClienteEndereco addAddr = new AddClienteEndereco();
                if (addAddr.ShowDialog() == DialogResult.OK)
                {
                    SetFocus();
                    DataTableAddress();
                }
                return;
            }

            if (GridLista.SelectedCells.Count == 0)
            {
                Alert.Message("Opss", "Selecione um endereço para editar!", Alert.AlertType.info);
                return;
            }

            IdAddress = Convert.ToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
            AddClienteEndereco form = new AddClienteEndereco();
            if (form.ShowDialog() == DialogResult.OK)
                SetFocus();
        }

        private void SelectAddr()
        {
            if (Convert.ToInt32(GridLista.SelectedRows[0].Cells["ID"].Value) > 0)
            {
                IdAddress = Convert.ToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                Alert.Message("Oppss", "Selecione um endereço!", Alert.AlertType.info);
            }
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    Support.UpDownDataGrid(false, GridLista);
                    e.Handled = true;
                    break;
                case Keys.Down:
                    Support.UpDownDataGrid(true, GridLista);
                    e.Handled = true;
                    break;
                case Keys.Enter:
                    SelectAddr();
                    break;
            }
        }

        private void Eventos()
        {
            GridLista.KeyDown += KeyDowns;

            Load += (s, e) =>
            {
                DataTableAddress();
                LoadData();
            };

            newAddr.Click += (s, e) => GetEndereco(true);

            Selecionar.Click += (s, e) =>
            {
                SelectAddr();
            };

            Selecionar.Enter += (s, e) => DataTableAddress();
        }
    }
}
