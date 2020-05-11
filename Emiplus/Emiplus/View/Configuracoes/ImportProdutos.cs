using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.Properties;
using SqlKata.Execution;

namespace Emiplus.View.Configuracoes
{
    public partial class ImportProdutos : Form
    {
        private readonly BackgroundWorker backWorker = new BackgroundWorker();

        public ImportProdutos()
        {
            InitializeComponent();
            Eventos();
        }

        private string IdEmpresa { get; set; }

        private void LoadEmpresas()
        {
            if (Support.CheckForInternetConnection())
            {
                var idUser = Settings.Default.user_sub_user != 0
                    ? Settings.Default.user_sub_user
                    : Settings.Default.user_id;
                var jo = new RequestApi().URL($"{Program.URL_BASE}/api/empresas/{Program.TOKEN}/{idUser}").Content()
                    .Response();

                if (jo["error"] != null && jo["error"].ToString() != "")
                {
                    Alert.Message("Opss", jo["error"].ToString(), Alert.AlertType.error);
                    return;
                }

                var data = new ArrayList {new {Id = "0", Nome = "SELECIONE A EMPRESA"}};
                foreach (dynamic item in jo)
                    if (item.Value.id_unique != Settings.Default.empresa_unique_id)
                    {
                        string id = item.Value.id_unique.ToString();
                        string nome = item.Value.razao_social.ToString();
                        data.Add(new {Id = id, Nome = nome});
                    }

                Empresas.DataSource = data;
                Empresas.DisplayMember = "Nome";
                Empresas.ValueMember = "Id";
            }
            else
            {
                Alert.Message("Opps", "Você precisa estar conectado a internet.", Alert.AlertType.error);
            }
        }

        private void Eventos()
        {
            Shown += (s, e) =>
            {
                Refresh();
                LoadEmpresas();
            };

            btnImport.Click += (s, e) =>
            {
                if (!Support.CheckForInternetConnection())
                {
                    Alert.Message("Opps", "Você precisa estar conectado a internet.", Alert.AlertType.error);
                    return;
                }

                Logs.Clear();

                IdEmpresa = Empresas.SelectedValue.ToString();

                if (Empresas.SelectedValue.ToString() != "0")
                    backWorker.RunWorkerAsync();
            };

            backWorker.DoWork += (s, e) =>
            {
                var idEmpresa = IdEmpresa;

                var response = new RequestApi().URL(Program.URL_BASE + $"/api/item/{Program.TOKEN}/{idEmpresa}/9999")
                    .Content().Response();
                if (response["error"] != null && response["error"].ToString() != "")
                {
                    Alert.Message("Opss", response["error"].ToString(), Alert.AlertType.error);
                    return;
                }

                var i = 0;
                foreach (dynamic item in response["data"])
                {
                    i++;
                    string codebarras = item.Value.codebarras;

                    var dataItem = new Item().FindAll().WhereFalse("excluir").Where("codebarras", codebarras)
                        .Where("tipo", "Produtos").FirstOrDefault<Item>();
                    if (dataItem != null)
                    {
                        Logs.Invoke((MethodInvoker) delegate
                        {
                            Logs.AppendText(
                                $"({i}) JÁ CADASTRADO: {dataItem.Nome} - Código de Barras: {dataItem.CodeBarras}" +
                                Environment.NewLine);
                        });
                    }
                    else
                    {
                        var createItem = new Item
                        {
                            Id = 0,
                            Tipo = "Produtos",
                            Excluir = 0,
                            Nome = item.Value.nome,
                            Referencia = item.Value.referencia,
                            ValorCompra = item.Value.valorcompra,
                            ValorVenda = item.Value.valorvenda,
                            EstoqueMinimo = item.Value.estoqueminimo,
                            EstoqueAtual = item.Value.estoqueatual,
                            Medida = item.Value.medida,
                            Cest = item.Value.cest,
                            Ncm = item.Value.ncm,
                            CodeBarras = item.Value.codebarras,
                            Limite_Desconto = Validation.ConvertToDouble(item.Value.limite_desconto),
                            Criado_por = Validation.ConvertToInt32(item.Value.criado_por),
                            Atualizado_por = Validation.ConvertToInt32(item.Value.atualizado_por)
                        };
                        createItem.Save(createItem, false);

                        Logs.Invoke((MethodInvoker) delegate
                        {
                            Logs.AppendText(
                                $"({i}) CADASTRO REALIZADO: {createItem.Nome} - Código de Barras: {createItem.CodeBarras}" +
                                Environment.NewLine);
                        });
                    }
                }

                Logs.Invoke((MethodInvoker) delegate { Logs.AppendText($"Total de itens filtrado: ({i})"); });
            };

            backWorker.RunWorkerCompleted += (s, e) =>
            {
                Alert.Message("Pronto", "Importação concluída.", Alert.AlertType.success);
            };

            btnExit.Click += (s, e) => Close();
        }
    }
}