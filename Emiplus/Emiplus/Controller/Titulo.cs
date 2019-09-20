using Emiplus.Data.Helpers;
using SqlKata.Execution;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Emiplus.Controller
{
    class Titulo : Data.Core.Controller
    {
        public double GetTroco(int idPedido)
        {
            if (String.IsNullOrEmpty(idPedido.ToString()))
                return 0;

            var data = new Model.Titulo().Query().SelectRaw("SUM(recebido) as recebido, SUM(total) as total").Where("id_pedido", idPedido).Where("excluir", 0).First();
            var total = data.TOTAL;
            var recebido = data.RECEBIDO;

            return Validation.ConvertToDouble(total - recebido);
        }

        public double GetLancados(int idPedido)
        {
            if (String.IsNullOrEmpty(idPedido.ToString()))
                return 0;

            var data = new Model.Titulo().Query().SelectRaw("SUM(recebido) as recebido").Where("id_pedido", idPedido).Where("excluir", 0).First();
            return Validation.ConvertToDouble(data.RECEBIDO);
        }

        public double GetRestante(int idPedido)
        {
            if (String.IsNullOrEmpty(idPedido.ToString()))
                return 0;

            var data = new Model.Titulo().Query().SelectRaw("SUM(total) as total").Where("id_pedido", idPedido).Where("excluir", 0).First();
            var lancado = Validation.ConvertToDouble(data.TOTAL);

            double restante = 0;
            if (lancado < GetTotalPedido(idPedido))
            {
                restante = lancado - GetTotalPedido(idPedido);
            }

            return restante * -1;
        }

        public double GetTotalPedido(int idPedido)
        {
            if (String.IsNullOrEmpty(idPedido.ToString()))
                return 0;

            var data = new Model.Pedido().FindById(idPedido).Select("total").Where("excluir", 0).First();
            return Validation.ConvertToDouble(data.TOTAL);
        }

        public bool AddPagamento(int idPedido, int formaPgto, string valorS, string inicio, string parcela = "1")
        {
            var data = new Model.Titulo();
            double valor = Validation.ConvertToDouble(valorS);
            DateTime vencimento = DateTime.Now;

            if (idPedido > 0)
            {
                if (GetRestante(idPedido) <= 0)
                {
                    Alert.Message("Opss", "Valor total já recebido. Verifique os lançamentos!", Alert.AlertType.error);
                    return false;
                }

                data.Id_Pedido = idPedido;
                data.Id_Pessoa = new Model.Pedido().FindById(idPedido).Select("cliente").Where("excluir", 0).First().CLIENTE;
            }

            

            if (valor < 0)
            {
                return false;
            }

            if (!String.IsNullOrEmpty(inicio))
            {
                vencimento = Convert.ToDateTime(inicio);
            }

            //2 CHEQUE 4 CARTÃO DE CRÉDITO 5 CREDIÁRIO 6 BOLETO
            if (parcela.IndexOf("+") > 0)
            {   
                //15+20+30+50+70 / dias e parcelas

                int[] numeros = parcela.Split(new string[] { "+" }, StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToArray();

                data.Total = Validation.Round(valor / numeros.Count());

                for (int i = 0; i < numeros.Length; i++)
                {
                    vencimento = vencimento.AddDays(numeros[i]);

                    data.Id = 0;
                    data.Id_FormaPgto = formaPgto;
                    data.Emissao = Validation.DateNowToSql();
                    data.Vencimento = Validation.ConvertDateToSQL(vencimento);
                    data.Recebido = data.Total;
                    data.Save(data);
                }
            }
            else if (Validation.ConvertToInt32(parcela) > 0 && formaPgto != 1 && formaPgto != 3)
            {
                data.Total = Validation.Round(valor / Validation.ConvertToInt32(parcela));

                int count = 1;
                while(count <= Validation.ConvertToInt32(parcela))
                {    
                    data.Id = 0;
                    data.Id_FormaPgto = formaPgto;
                    data.Emissao = Validation.DateNowToSql();
                    data.Vencimento = Validation.ConvertDateToSQL(vencimento.AddMonths(count));
                    data.Recebido = data.Total;
                    data.Save(data);
                    count++;
                }
            }
            else
            {
                //1 DINHEIRO 3 CARTÃO DE DÉBITO
                
                data.Id = 0;
                data.Id_FormaPgto = formaPgto;
                data.Emissao = Validation.DateNowToSql();
                data.Vencimento = Validation.DateNowToSql();

                if (formaPgto == 1 && valor > GetRestante(idPedido))
                {
                    data.Total = GetRestante(idPedido);
                    data.Recebido = valor;
                }
                else
                {
                    data.Total = valor;
                    data.Recebido = valor;                    
                }

                if (data.Save(data))
                    return true;

                return false;
            }

            return false;
        }

        public void GetDataTableTitulos(DataGridView Table, int idPedido)
        {
            /*Table.ColumnCount = 5;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "Data";
            Table.Columns[1].Width = 100;

            Table.Columns[2].Name = "Forma de Pagamento";
            Table.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Table.Columns[2].Width = 100;

            Table.Columns[3].Name = "Valor";
            Table.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[3].Width = 100;

            Table.Columns[4].Name = "";
            Table.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[4].Width = 100;*/

            Table.Rows.Clear();

            var titulos = new Model.Titulo();

            var data = titulos.Query()
                .LeftJoin("formapgto", "formapgto.id", "titulo.id_formapgto")
                .Select("titulo.id", "titulo.recebido", "titulo.vencimento", "formapgto.nome as formapgto")
                .Where("titulo.excluir", 0)
                .Where("titulo.id_pedido", idPedido)
                .OrderByDesc("titulo.id")
                .Get();

            foreach (var item in data)
            {
                string pathIconEdit = $"{Support.BasePath()}\\Assets\\Images\\icons\\edit16x.png";
                string pathIconExcluir = $"{Support.BasePath()}\\Assets\\Images\\icons\\bin16x.png";

                Table.Rows.Add(
                    item.ID,
                    item.FORMAPGTO,
                    Validation.ConvertDateToForm(item.VENCIMENTO),  
                    Validation.FormatPrice(Validation.ConvertToDouble(item.RECEBIDO), true),
                    Bitmap.FromFile(pathIconEdit),
                    Bitmap.FromFile(pathIconExcluir)
                );
            }
        }
    }
}