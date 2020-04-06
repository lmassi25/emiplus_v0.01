using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.Properties;
using RestSharp;
using SqlKata.Execution;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Configuracoes
{
    public partial class ImportProdutos : Form
    {
        BackgroundWorker backWorker = new BackgroundWorker();

        private string idEmpresa { get; set; }

        public ImportProdutos()
        {
            InitializeComponent();
            Eventos();
        }

        private void LoadEmpresas()
        {
            if (Support.CheckForInternetConnection())
            {
                var jo = new RequestApi().URL($"{Program.URL_BASE}/api/empresas/{Program.TOKEN}/{Settings.Default.user_id}").Content().Response();

                if (jo["error"] != null && jo["error"].ToString() != "")
                {
                    Alert.Message("Opss", jo["error"].ToString(), Alert.AlertType.error);
                    return;
                }

                var data = new ArrayList();
                foreach (dynamic item in jo)
                {
                    if (item.Value.id_unique != Settings.Default.empresa_unique_id) {
                        data.Add(new { Id = item.Value.id_unique, Nome = item.Value.razao_social });
                    }
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

                idEmpresa = Empresas.SelectedValue.ToString();

                backWorker.RunWorkerAsync();
            };

            backWorker.DoWork += (s, e) =>
            {
                string idEmpresa = this.idEmpresa;

                var response = new RequestApi().URL(Program.URL_BASE + $"/api/item/{Program.TOKEN}/{idEmpresa}/9999").Content().Response();
                if (response["error"] != null && response["error"].ToString() != "")
                {
                    Alert.Message("Opss", response["error"].ToString(), Alert.AlertType.error);
                    return;
                }

                int i = 0;
                foreach (dynamic item in response["data"])
                {
                    i++;
                    string codebarras = item.Value.codebarras;

                    Model.Item dataItem = new Model.Item().FindAll().WhereFalse("excluir").Where("codebarras", codebarras).Where("tipo", "Produtos").FirstOrDefault<Model.Item>();
                    if (dataItem != null)
                    {
                        Logs.Invoke((MethodInvoker)delegate
                        {
                            Logs.AppendText($"({i}) JÁ CADASTRADO: {dataItem.Nome} - Código de Barras: {dataItem.CodeBarras}" + Environment.NewLine);
                        });
                    }
                    else
                    {
                        Model.Item createItem = new Model.Item();
                        createItem.Id = 0;
                        createItem.Tipo = "Produtos";
                        createItem.Excluir = 0;
                        createItem.Nome = item.Value.nome;
                        createItem.Referencia = item.Value.referencia;
                        createItem.ValorCompra = item.Value.valorcompra;
                        createItem.ValorVenda = item.Value.valorvenda;
                        createItem.EstoqueMinimo = item.Value.estoqueminimo;
                        createItem.EstoqueAtual = item.Value.estoqueatual;
                        createItem.Medida = item.Value.medida;
                        createItem.Cest = item.Value.cest;
                        createItem.Ncm = item.Value.ncm;
                        createItem.CodeBarras = item.Value.codebarras;
                        createItem.Limite_Desconto = item.Value.limite_desconto;
                        createItem.Criado_por = Validation.ConvertToInt32(item.Value.criado_por);
                        createItem.Atualizado_por = Validation.ConvertToInt32(item.Value.atualizado_por);
                        createItem.Save(createItem, false);
                        
                        Logs.Invoke((MethodInvoker)delegate
                        {
                            Logs.AppendText($"({i}) CADASTRO REALIZADO: {createItem.Nome} - Código de Barras: {createItem.CodeBarras}" + Environment.NewLine);
                        });
                    }
                }

                Logs.Invoke((MethodInvoker)delegate
                {
                    Logs.AppendText($"Total de itens filtrado: ({i})");
                });
            };

            backWorker.RunWorkerCompleted += (s, e) =>
            {
                Alert.Message("Pronto", "Importação concluída.", Alert.AlertType.success);
            };

            btnExit.Click += (s, e) => Close();
        }
    }
}
