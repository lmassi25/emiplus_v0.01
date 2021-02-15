using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.Properties;
using Emiplus.View.Common;
using Newtonsoft.Json;
using SqlKata.Execution;
using System;
using System.ComponentModel;
using System.IO;
using System.Json;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace Emiplus.View.Fiscal.TelasNota
{
    public partial class OpcoesNfeRapida : Form
    {
        public static int idPedido { get; set; }
        public static int idNota { get; set; }

        private Model.Pedido _modelPedido = new Model.Pedido();
        private Model.Nota _modelNota = new Model.Nota();

        private BackgroundWorker WorkerBackground = new BackgroundWorker();
        private string _msg, justificativa;
        private int p1 = 0;

        public OpcoesNfeRapida()
        {
            InitializeComponent();

            if(idNota == 0)
            {
                var checkNota = new Model.Nota().FindByIdPedidoUltReg(idPedido, "", "NFe").FirstOrDefault<Model.Nota>();

                if(checkNota != null)
                {
                    idNota = checkNota.Id;
                }
            }

            Eventos();
        }

        private void Campos(Boolean enabled)
        {
            //if (enabled)
            //    label12.Focus = true;
            //else
            //    label12.Focus = false;

            Emitir.Enabled = enabled;
            Imprimir.Enabled = enabled;
            EnviarEmail.Enabled = enabled;
            btnDetalhes.Enabled = enabled;
            CartaCorrecao.Enabled = enabled;
            Cancelar.Enabled = enabled;
        }

        public void Eventos()
        {
            Load += (s, e) =>
            {
                var nota = new Model.Nota().FindById(idNota).FirstOrDefault<Model.Nota>();

                if (nota == null)
                    return;

                nsefaz.Text = (!String.IsNullOrEmpty(nota.nr_Nota)) ? nota.nr_Nota : "";
                serie.Text = (!String.IsNullOrEmpty(nota.Serie)) ? nota.Serie : "";
                status.Text = (!String.IsNullOrEmpty(nota.Status)) ? nota.Status : "";
                chavedeacesso.Text = (!String.IsNullOrEmpty(nota.ChaveDeAcesso)) ? nota.ChaveDeAcesso : "";

                Emitir.Visible = false;
            };

            btnDetalhes.Click += (s, e) =>
            {
                //Nota.disableCampos = true;
                Nota.Id = idNota;
                Nota nota = new Nota();
                nota.TopMost = true;
                nota.ShowDialog();
            };

            Emitir.Click += (s, e) =>
            {
                //var checkNota = _modelNota.FindByIdPedido(idPedido).WhereNotNull("status").Where("nota.tipo", "NFe").FirstOrDefault();
                //var checkNota = _modelNota.FindByIdPedidoUltReg(idPedido, "", "NFe").FirstOrDefault<Model.Nota>();
                var checkNota = new Model.Nota().FindById(idNota).FirstOrDefault<Model.Nota>();
                if (checkNota == null)
                {
                    Model.Nota _modelNotaNova = new Model.Nota();

                    _modelNotaNova.Id = 0;
                    _modelNotaNova.Tipo = "NFe";
                    _modelNotaNova.Status = "Pendente";
                    _modelNotaNova.id_pedido = idPedido;
                    _modelNotaNova.Save(_modelNotaNova, false);

                    checkNota = new Model.Nota().FindByIdPedidoUltReg(idPedido, "", "NFe").FirstOrDefault<Model.Nota>();
                }

                if (checkNota.Status == "Cancelada")
                {
                    if (Home.pedidoPage == "Notas")
                    {
                        Alert.Message("Atenção!", "Não é possível emitir uma nota Autorizada/Cancelada.", Alert.AlertType.warning);
                        return;
                    }

                    var result = AlertOptions.Message("Atenção!", "Existem registro(s) de nota(s) cancelada(s) a partir desta venda. Deseja gerar um nova nota?", AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                    if (result)
                    {
                        Model.Nota _modelNotaNova = new Model.Nota();

                        _modelNotaNova.Id = 0;
                        _modelNotaNova.Tipo = "NFe";
                        _modelNotaNova.Status = "Pendente";
                        _modelNotaNova.id_pedido = idPedido;
                        _modelNotaNova.Save(_modelNotaNova, false);

                        checkNota = new Model.Nota().FindByIdPedidoUltReg(idPedido, "", "NFe").FirstOrDefault<Model.Nota>();
                    }
                }

                if (checkNota.Status != "Pendente")
                {
                    Alert.Message("Atenção!", "Não é possível emitir uma nota Autorizada/Cancelada.", Alert.AlertType.warning);
                    return;
                }

                _modelNota = checkNota;

                retorno.Text = "Emitindo NF-e .......................................... (1/2)";

                if (p1 == 0)
                {
                    p1 = 1;
                    WorkerBackground.RunWorkerAsync();
                }
                else
                    Alert.Message("Ação não permitida", "Aguarde processo finalizar", Alert.AlertType.warning);
            };

            CartaCorrecao.Click += (s, e) =>
            {
                //var checkNota = new Model.Nota().FindByIdPedidoUltReg(idPedido, "", "NFe").FirstOrDefault<Model.Nota>();
                var checkNota = new Model.Nota().FindById(idNota).FirstOrDefault<Model.Nota>();
                if (checkNota == null || checkNota?.Status != "Autorizada")
                {
                    Alert.Message("Ação não permitida!", "Não é possível emitir uma Carta de Correção.", Alert.AlertType.warning);
                    return;
                }

                _modelNota = checkNota;

                CartaCorrecao cce = new CartaCorrecao();
                cce.TopMost = true;
                cce.Show();

                Application.OpenForms["OpcoesNfeRapida"].Close();
            };

            Cancelar.Click += (s, e) =>
            {
                //var checkNota = new Model.Nota().FindByIdPedidoUltReg(idPedido, "", "NFe").FirstOrDefault<Model.Nota>();
                var checkNota = new Model.Nota().FindById(idNota).FirstOrDefault<Model.Nota>();
                if (checkNota == null || checkNota?.Status != "Autorizada")
                {
                    Alert.Message("Ação não permitida!", "Não é possível cancelar uma nota Pendente/Cancelada.", Alert.AlertType.warning);
                    return;
                }
                
                _modelNota = checkNota;

                CartaCorrecaoAdd.tela = "Cancelar";
                CartaCorrecaoAdd f = new CartaCorrecaoAdd();
                f.TopMost = true;
                if (f.ShowDialog() == DialogResult.OK)
                {
                    CartaCorrecaoAdd.tela = "";
                    justificativa = CartaCorrecaoAdd.justificativa;

                    retorno.Text = "Cancelando NF-e .......................................... (1/2)";

                    p1 = 4;
                    WorkerBackground.RunWorkerAsync();
                }
            };

            EnviarEmail.Click += (s, e) =>
            {
                //var checkNota = new Model.Nota().FindByIdPedidoUltReg(idPedido, "", "NFe").FirstOrDefault<Model.Nota>();
                var checkNota = new Model.Nota().FindById(idNota).FirstOrDefault<Model.Nota>();
                if (checkNota == null || checkNota?.Status == "Pendente")
                {
                    Alert.Message("Ação não permitida!", "Não é possível enviar uma nota Pendente.", Alert.AlertType.warning);
                    return;
                }

                _modelNota = checkNota;

                CartaCorrecaoAdd.tela = "Email";
                CartaCorrecaoAdd.idNota = idNota;
                CartaCorrecaoAdd f = new CartaCorrecaoAdd();
                f.TopMost = true;
                if (f.ShowDialog() == DialogResult.OK)
                {
                    CartaCorrecaoAdd.tela = "";
                    justificativa = CartaCorrecaoAdd.justificativa;

                    retorno.Text = "Enviando NF-e .......................................... (1/2)";

                    p1 = 5;
                    WorkerBackground.RunWorkerAsync();
                }

            };

            Imprimir.Click += (s, e) =>
            {
                var checkNota = new Model.Nota().FindByIdPedidoUltReg(idPedido, "", "NFe").FirstOrDefault<Model.Nota>();
                if (checkNota == null || checkNota?.Status == "Pendente")
                {
                    Alert.Message("Opps!", "Emita a nota para imprimir.", Alert.AlertType.warning);
                    return;
                }

                _modelNota = checkNota;

                retorno.Text = "Imprimindo NF-e .......................................... (1/2)";

                if (p1 == 0)
                {
                    p1 = 2;
                    WorkerBackground.RunWorkerAsync();
                }
                else
                    Alert.Message("Ação não permitida", "Aguarde processo finalizar", Alert.AlertType.warning);
            };

            using (var b = WorkerBackground)
            {
                b.DoWork += async (s, e) =>
                {
                    switch (p1)
                    {
                        case 1:

                            //_modelNota = _modelNota.FindByIdPedido(idPedido).FirstOrDefault<Model.Nota>();
                            //if (_modelNota == null)
                            //{
                            //    _modelNota.Id = 0;
                            //    _modelNota.id_pedido = idPedido;
                            //    _modelNota.Save(_modelNota);
                            //}

                            _msg = new Controller.Fiscal().Emitir(idPedido, "NFe", _modelNota.Id);
                            break;

                        case 2:

                            if (IniFile.Read("NFe", "APP") != "Uninfe")
                            {
                                var msg = new Controller.Fiscal().Imprimir(idPedido, "NFe", _modelNota.Id);
                                if (!msg.Contains(".pdf"))
                                    _msg = msg;
                            }
                            else
                            {


                                EmissorImprimirDanfe();
                            }

                            break;

                        case 3:
                            //_msg = new Controller.Fiscal().EmitirCCe(idPedido, "Nota gerada com informacoes incorretas, por gentileza verificar as corretas");
                            break;

                        case 4:
                            if (justificativa.Length <= 15)
                                break;

                            _msg = new Controller.Fiscal().Cancelar(idPedido, "NFe", justificativa, _modelNota.Id);
                            break;

                        case 5:
                            _msg = new Controller.Fiscal().EnviarEmail(idPedido, justificativa, "NFe", _modelNota.Id);
                            break;
                    }
                };

                b.RunWorkerCompleted += async (s, e) =>
                {
                    p1 = 0;

                    if (!String.IsNullOrEmpty(_msg)) 
                    {
                        retorno.Text = _msg;
                    }                    
                };
            }

            FormClosing += (s, e) =>
            {
                OpcoesNfeRapida.idPedido = 0;
                OpcoesNfeRapida.idNota = 0;
            };
        }

        private async void EmissorImprimirDanfe()
        {
            _modelPedido = new Model.Pedido().FindById(idPedido).First<Model.Pedido>();

            string _pathUninfe = @"C:\Emiplus\Unimake\UniNFe\" + Validation.CleanStringForFiscal(Settings.Default.empresa_cnpj).Replace(" ", "").Replace(".", ""),
                _pathEnvio = _pathUninfe + @"\Envio", 
                _pathRetorno = _pathUninfe + @"\Retorno", 
                _pathEnviado = _pathUninfe + @"\Enviado\Autorizados";

            if (File.Exists(_pathEnviado + "\\" + _modelPedido.Emissao.Year.ToString("0000") + _modelPedido.Emissao.Month.ToString("00") + "\\" + _modelNota.ChaveDeAcesso + "-procNFe.xml"))
            {
                string xml = File.ReadAllText(_pathEnviado + "\\" + _modelPedido.Emissao.Year.ToString("0000") + _modelPedido.Emissao.Month.ToString("00") + "\\" + _modelNota.ChaveDeAcesso + "-procNFe.xml");

                string URI = "https://emiplus.com.br/emissor/api/nota/imprimir";

                var nota = new Model.Emissor();
                nota.chavedeacesso = _modelNota.ChaveDeAcesso;
                nota.emitente = Validation.CleanStringForFiscal(Settings.Default.empresa_cnpj).Replace(" ", "").Replace(".", "");
                nota.aaaamm = _modelPedido.Emissao.Year.ToString("0000") + _modelPedido.Emissao.Month.ToString("00");
                nota.xml = xml;

                using (var client = new HttpClient())
                {
                    var serializedProduto = JsonConvert.SerializeObject(nota);
                    var content = new StringContent(serializedProduto, Encoding.UTF8, "application/json");

                    client.DefaultRequestHeaders.Add("email", "leandro");
                    client.DefaultRequestHeaders.Add("password", "leandro");

                    HttpResponseMessage result = await client.PostAsync(URI, content);
                    //var result = await client.PostAsJsonAsync(URI, serializedProduto);

                    Console.WriteLine(result.ToString());
                    if (result.Content != null)
                    {
                        var responseContent = await result.Content.ReadAsStringAsync();
                        Console.WriteLine(responseContent.ToString());

                        /*dynamic jsonTratado = JsonValue.Parse(responseContent.ToString());

                        var json2 = jsonTratado[0];

                        if (json2[0] == "true")
                        {
                            System.Diagnostics.Process.Start("https://emiplus.com.br/emissor/pdf/" + nota.chavedeacesso + ".pdf");
                        }
                        else
                        {
                            _msg = "Arquivo pdf não encontrado!";
                        }*/

                        _msg = "";
                        System.Diagnostics.Process.Start("https://emiplus.com.br/emissor/pdf/" + nota.chavedeacesso + ".pdf");
                    }
                }                
            }
            else
            {
                _msg = "Arquivo xml não encontrado!";
            }
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}