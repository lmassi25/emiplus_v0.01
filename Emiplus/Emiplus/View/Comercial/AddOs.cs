using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.Data.SobreEscrever;
using Emiplus.Properties;
using Emiplus.View.Common;
using Emiplus.View.Reports;
using SqlKata.Execution;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace Emiplus.View.Comercial
{
    public partial class AddOs : Form
    {
        #region V

        public static int Id { get; set; } // id pedido
        
        private Model.Pedido _mPedido = new Model.Pedido();
        private Model.Pessoa _mCliente = new Model.Pessoa();
        private Model.Usuarios _mUsuario = new Model.Usuarios();

        private KeyedAutoCompleteStringCollection collection = new KeyedAutoCompleteStringCollection();

        FullScreenMode fullScreenMode;

        private Timer timer = new Timer(Configs.TimeLoading);

        #endregion V

        public AddOs()
        {
            InitializeComponent();
            Eventos();
        }

        private void AutoComplete()
        {
            var data = _mPedido.Query();

            data.Select("campoa");
            data.Where("campoa", "<>", "");
            data.WhereNotNull("campoa");

            foreach (var itens in data.Get())
            {
                collection.Add(itens.CAMPOA);
            }

            aText.AutoCompleteCustomSource = collection;

            data.Select("campob");
            data.Where("campob", "<>", "");
            data.WhereNotNull("campob");

            foreach (var itens in data.Get())
            {
                collection.Add(itens.CAMPOB);
            }

            bText.AutoCompleteCustomSource = collection;

            data.Select("campoc");
            data.Where("campoc", "<>", "");
            data.WhereNotNull("campoc");

            foreach (var itens in data.Get())
            {
                collection.Add(itens.CAMPOC);
            }

            cText.AutoCompleteCustomSource = collection;

            data.Select("campod");
            data.Where("campod", "<>", "");
            data.WhereNotNull("campod");

            foreach (var itens in data.Get())
            {
                collection.Add(itens.CAMPOD);
            }

            dText.AutoCompleteCustomSource = collection;

            data.Select("campoe");
            data.Where("campoe", "<>", "");
            data.WhereNotNull("campoe");

            foreach (var itens in data.Get())
            {
                collection.Add(itens.CAMPOE);
            }

            eText.AutoCompleteCustomSource = collection;

            data.Select("campof");
            data.Where("campof", "<>", "");
            data.WhereNotNull("campof");

            foreach (var itens in data.Get())
            {
                collection.Add(itens.CAMPOF);
            }

            fText.AutoCompleteCustomSource = collection;
        }

        private void LoadData()
        {
            _mPedido = _mPedido.FindById(Id).FirstOrDefault<Model.Pedido>();
            if (_mPedido == null)
            {
                Alert.Message("Opps", "Não encontramos o registro.", Alert.AlertType.info);
                return;
            }

            osText.Text = "Dados da Ordem de Serviço: " + Id;
            aText.Text = _mPedido.campoa == null ? "" : _mPedido.campoa;
            bText.Text = _mPedido.campob == null ? "" : _mPedido.campob;
            cText.Text = _mPedido.campoc == null ? "" : _mPedido.campoc;
            dText.Text = _mPedido.campod == null ? "" : _mPedido.campod;
            eText.Text = _mPedido.campoe == null ? "" : _mPedido.campoe;
            fText.Text = _mPedido.campof == null ? "" : _mPedido.campof;
            problemaText.Text = _mPedido.problema == null ? "" : _mPedido.problema;
            solucaoText.Text = _mPedido.solucao == null ? "" : _mPedido.solucao;

            LoadCliente();
            LoadColaborador();
        }

        /// <summary>
        /// Carrega o cliente selecionado.
        /// </summary>
        private void LoadCliente()
        {
            if (_mPedido.Cliente > 0)
            {
                _mPedido.Save(_mPedido);
                var data = _mCliente.FindById(_mPedido.Cliente).FirstOrDefault<Model.Pessoa>();

                if (data == null)
                    return;

                if (data.Nome == "Consumidor Final")
                    return;

                nomeCliente.Text = data.Nome;
            }
        }

        /// <summary>
        /// Carrega o vendedor selecionado.
        /// </summary>
        private void LoadColaborador()
        {
            if (_mPedido.Colaborador > 0)
            {
                _mPedido.Save(_mPedido);

                var data = _mUsuario.FindByUserId(_mPedido.Colaborador).FirstOrDefault<Model.Usuarios>();

                if (data == null)
                    return;

                nomeVendedor.Text = data.Nome;
            }
        }

        /// <summary>
        /// Janela para selecionar Cliente no pedido.
        /// </summary>
        public void ModalClientes()
        {
            PedidoModalClientes form = new PedidoModalClientes();
            form.TopMost = true;
            if (form.ShowDialog() == DialogResult.OK)
            {
                _mPedido.Id = Id;
                _mPedido.Cliente = PedidoModalClientes.Id;
                _mPedido.Save(_mPedido);
                LoadData();
            }
        }

        /// <summary>
        /// Janela para selecionar vendedor no pedido.
        /// </summary>
        public void ModalColaborador()
        {
            PedidoModalVendedor form = new PedidoModalVendedor();
            form.TopMost = true;
            if (form.ShowDialog() == DialogResult.OK)
            {
                _mPedido.Id = Id;
                _mPedido.Colaborador = PedidoModalVendedor.Id;
                _mPedido.Save(_mPedido);
                LoadData();
            }
        }

        /// <summary>
        /// Salva todos os campos textbox
        /// </summary>
        private void Save()
        {
            if(aText.Text.Length > 255)
            {
                Alert.Message("Ação não permitida", aLabel.Text + " possui mais do que 255 caracteres", Alert.AlertType.warning);
                return;
            }

            if (bText.Text.Length > 255)
            {
                Alert.Message("Ação não permitida", bLabel.Text + " possui mais do que 255 caracteres", Alert.AlertType.warning);
                return;
            }

            if (cText.Text.Length > 255)
            {
                Alert.Message("Ação não permitida", cLabel.Text + " possui mais do que 255 caracteres", Alert.AlertType.warning);
                return;
            }

            if (dText.Text.Length > 255)
            {
                Alert.Message("Ação não permitida", dLabel.Text + " possui mais do que 255 caracteres", Alert.AlertType.warning);
                return;
            }

            if (eText.Text.Length > 255)
            {
                Alert.Message("Ação não permitida", eLabel.Text + " possui mais do que 255 caracteres", Alert.AlertType.warning);
                return;
            }

            if (fText.Text.Length > 255)
            {
                Alert.Message("Ação não permitida", fLabel.Text + " possui mais do que 255 caracteres", Alert.AlertType.warning);
                return;
            }

            if (problemaText.Text.Length > 500)
            {
                Alert.Message("Ação não permitida", label9.Text + " possui mais do que 500 caracteres", Alert.AlertType.warning);
                return;
            }

            if (solucaoText.Text.Length > 500)
            {
                Alert.Message("Ação não permitida", label10.Text + " possui mais do que 500 caracteres", Alert.AlertType.warning);
                return;
            }

            if (_mPedido == null)
            {
                Alert.Message("Opps", "Não encontramos o registro.", Alert.AlertType.info);
                return;
            }

            _mPedido.campoa = aText.Text;
            _mPedido.campob = bText.Text;
            _mPedido.campoc = cText.Text;
            _mPedido.campod = dText.Text;
            _mPedido.campoe = eText.Text;
            _mPedido.campof = fText.Text;
            _mPedido.problema = problemaText.Text;
            _mPedido.solucao = solucaoText.Text;

            if (!_mPedido.Save(_mPedido))
                Alert.Message("Ação não permitida", "Não foi possível salvar O.S.", Alert.AlertType.error);
        }

        /// <summary>
        /// Adiciona os eventos de 'KeyDown' a todos os controls com a function KeyDowns
        /// </summary>
        private void KeyDowns(object sender, KeyEventArgs e)
        {
            //BeginInvoke(new Action(() =>
            //{
            //}));
            //e.SuppressKeyPress = true;

            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    e.SuppressKeyPress = true;
                    break;

                case Keys.F2:
                    e.SuppressKeyPress = true;
                    break;

                case Keys.F5:
                    //OptionBobinaA4 f = new OptionBobinaA4();
                    //string tipo = "";
                    //f.TopMost = true;
                    //DialogResult formResult = f.ShowDialog();

                    //if (formResult == DialogResult.OK)
                    //{
                    //    tipo = "Folha A4";
                    //    new Controller.Pedido().Imprimir(Id, tipo);
                    //}
                    //else if (formResult == DialogResult.Cancel)
                    //{
                    //    tipo = "Bobina 80mm";
                    //    new Controller.Pedido().Imprimir(Id, tipo);
                    //}
                    break;

                case Keys.F7:
                    ModalClientes();
                    e.SuppressKeyPress = true;
                    break;

                case Keys.F8:
                    ModalColaborador();
                    e.SuppressKeyPress = true;
                    break;

                case Keys.F10:
                    
                    e.SuppressKeyPress = true;
                    break;

                case Keys.F11:
                    FullScreen fullScreen = new FullScreen();

                    if (fullScreenMode == FullScreenMode.No)
                    {
                        fullScreen.EnterFullScreenMode(this);
                        fullScreenMode = FullScreenMode.Yes;
                    }
                    else
                    {
                        fullScreen.LeaveFullScreenMode(this);
                        fullScreenMode = FullScreenMode.No;
                    }
                    break;
            }
        }

        enum FullScreenMode
        {
            Yes, No
        }

        /// <summary>
        /// Adiciona os eventos nos Controls do form.
        /// </summary>
        private void Eventos()
        {
            KeyDown += KeyDowns;
            panel1.KeyDown += KeyDowns;
            imprimir.KeyDown += KeyDowns;
            btnConcluir.KeyDown += KeyDowns;
            SelecionarCliente.KeyDown += KeyDowns;
            SelecionarColaborador.KeyDown += KeyDowns;
            Masks.SetToUpper(this);

            Load += (s, e) =>
            {   
                aLabel.Visible = !String.IsNullOrEmpty(IniFile.Read("Campo_1_Visible", "OS")) ? Convert.ToBoolean(IniFile.Read("Campo_1_Visible", "OS")) : false;
                aText.Visible = aLabel.Visible;
                aLabel.Text = !String.IsNullOrEmpty(IniFile.Read("Campo_1_Descr", "OS")) ? IniFile.Read("Campo_1_Descr", "OS") : "";

                bLabel.Visible = !String.IsNullOrEmpty(IniFile.Read("Campo_2_Visible", "OS")) ? Convert.ToBoolean(IniFile.Read("Campo_2_Visible", "OS")) : false;
                bText.Visible = bLabel.Visible;
                bLabel.Text = !String.IsNullOrEmpty(IniFile.Read("Campo_2_Descr", "OS")) ? IniFile.Read("Campo_2_Descr", "OS") : "";

                cLabel.Visible = !String.IsNullOrEmpty(IniFile.Read("Campo_3_Visible", "OS")) ? Convert.ToBoolean(IniFile.Read("Campo_3_Visible", "OS")) : false;
                cText.Visible = cLabel.Visible;
                cLabel.Text = !String.IsNullOrEmpty(IniFile.Read("Campo_3_Descr", "OS")) ? IniFile.Read("Campo_3_Descr", "OS") : "";

                dLabel.Visible = !String.IsNullOrEmpty(IniFile.Read("Campo_4_Visible", "OS")) ? Convert.ToBoolean(IniFile.Read("Campo_4_Visible", "OS")) : false;
                dText.Visible = dLabel.Visible;
                dLabel.Text = !String.IsNullOrEmpty(IniFile.Read("Campo_4_Descr", "OS")) ? IniFile.Read("Campo_4_Descr", "OS") : "";

                eLabel.Visible = !String.IsNullOrEmpty(IniFile.Read("Campo_5_Visible", "OS")) ? Convert.ToBoolean(IniFile.Read("Campo_5_Visible", "OS")) : false;
                eText.Visible = eLabel.Visible;
                eLabel.Text = !String.IsNullOrEmpty(IniFile.Read("Campo_5_Descr", "OS")) ? IniFile.Read("Campo_5_Descr", "OS") : "";

                fLabel.Visible = !String.IsNullOrEmpty(IniFile.Read("Campo_6_Visible", "OS")) ? Convert.ToBoolean(IniFile.Read("Campo_6_Visible", "OS")) : false;
                fText.Visible = fLabel.Visible;
                fLabel.Text = !String.IsNullOrEmpty(IniFile.Read("Campo_6_Descr", "OS")) ? IniFile.Read("Campo_6_Descr", "OS") : "";

                if (!dLabel.Visible && !eLabel.Visible && !eLabel.Visible)
                {
                    visualSeparator1.Location = new Point(visualSeparator1.Location.X, visualSeparator1.Location.Y - 70);
                    label9.Location = new Point(label9.Location.X, label9.Location.Y - 70);
                    problemaLen.Location = new Point(problemaLen.Location.X, problemaLen.Location.Y - 70);
                    problemaText.Location = new Point(problemaText.Location.X, problemaText.Location.Y - 70);

                    label10.Location = new Point(label10.Location.X, label10.Location.Y - 70);
                    solucaoLen.Location = new Point(solucaoLen.Location.X, solucaoLen.Location.Y - 70);
                    solucaoText.Location = new Point(solucaoText.Location.X, solucaoText.Location.Y - 70);
                }

                AutoComplete();
            };

            Shown += (s, e) =>
            {
                Refresh();

                Resolution.SetScreenMaximized(this);

                if (Id > 0)
                {
                    LoadData();
                }
                else
                {
                    _mPedido.Id = 0;
                    _mPedido.Cliente = 1;
                    _mPedido.Colaborador = Settings.Default.user_id;
                    _mPedido.Tipo = "Ordens de Servico";
                    if (_mPedido.Save(_mPedido))
                    {
                        Id = _mPedido.GetLastId();
                        _mPedido = _mPedido.FindById(Id).FirstOrDefault<Model.Pedido>();
                        LoadData();
                    }
                    else
                    {
                        Alert.Message("Opss", "Erro ao criar Pedido.", Alert.AlertType.error);
                        Close();
                    }
                }

                aText.Focus();
            };

            btnConcluir.Click += (s, e) =>
            {
                if(_mPedido == null)
                {
                    Alert.Message("Opps!", "Erro ao finalizar.", Alert.AlertType.error);
                    return;
                }

                _mPedido.status = 1;
                if (_mPedido.Save(_mPedido))
                {
                    Alert.Message("Tudo certo!", "Finalizado com sucesso.", Alert.AlertType.success);
                    Close();
                }                    
                else
                    Alert.Message("Opps!", "Erro ao finalizar.", Alert.AlertType.error);
            };

            btnGerarVenda.Click += (s, e) =>
            {
                //PedidoPagamentos f = new PedidoPagamentos();
                //f.TopMost = true;

                //_mPedido = _mPedido.FindById(Id).First<Model.Pedido>();
                //_mPedido.Id = Id;
                //_mPedido.Tipo = "Vendas";
                //if (_mPedido.Save(_mPedido))
                //{
                //    Alert.Message("Tudo certo!", "Venda gerada com sucesso.", Alert.AlertType.success);
                //    Home.pedidoPage = "Vendas";
                //    LoadData();
                //    return;
                //}
            };

            SelecionarCliente.Click += (s, e) => ModalClientes();
            SelecionarColaborador.Click += (s, e) => ModalColaborador();

            imprimir.Click += (s, e) =>
            {
                new Controller.Pedido().Imprimir(Id, "Ordens de Servico");
            };

            btnObs.Click += (s, e) =>
            {
                AddObservacao.idPedido = Id;
                AddObservacao f = new AddObservacao();
                f.TopMost = true;
                f.Show();
            };

            FormClosing += (s, e) =>
            {
                //if (!btnFinalizado)
                //{
                //    Home.pedidoPage = CachePage;
                //    var result = AlertOptions.Message("Atenção!", "Você está prestes a excluir!" + Environment.NewLine + "Deseja continuar?", AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                //    if (result)
                //    {
                //        if (Home.pedidoPage == "Compras" || Home.pedidoPage == "Devoluções")
                //            new Controller.Estoque(Id, Home.pedidoPage, "Fechamento de Tela").Remove().Pedido();
                //        else
                //            new Controller.Estoque(Id, Home.pedidoPage, "Fechamento de Tela").Add().Pedido();

                //        _mPedido.Remove(Id);
                //        return;
                //    }

                //    e.Cancel = true;
                //}
            };

            aText.TextChanged += (s, e) =>
            {
                timer.Stop();
                timer.Start();
            };

            bText.TextChanged += (s, e) =>
            {
                timer.Stop();
                timer.Start();
            };

            cText.TextChanged += (s, e) =>
            {
                timer.Stop();
                timer.Start();
            };

            dText.TextChanged += (s, e) =>
            {
                timer.Stop();
                timer.Start();
            };

            eText.TextChanged += (s, e) =>
            {
                timer.Stop();
                timer.Start();
            };

            fText.TextChanged += (s, e) =>
            {
                timer.Stop();
                timer.Start();
            };

            problemaText.TextChanged += (s, e) =>
            {
                problemaLen.Text = problemaText.Text.Length.ToString() + " caracteres";
                problemaLen.Visible = true;
                timer.Stop();
                timer.Start();
            };

            solucaoText.TextChanged += (s, e) =>
            {
                solucaoLen.Text = solucaoText.Text.Length.ToString() + " caracteres";
                solucaoLen.Visible = true;
                timer.Stop();
                timer.Start();
            };

            timer.AutoReset = false;
            timer.Elapsed += (s, e) =>
            {
                Save();
            };

            btnRemover.Click += (s, e) =>
            {
                var result = AlertOptions.Message("Atenção!", "Deseja realmente apagar?", AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                if (result)
                {
                    var remove = new Controller.Pedido();
                    remove.Remove(Id);
                    Close();
                }
            };

            btnExit.Click += (s, e) => Close();
        }
    }
}
