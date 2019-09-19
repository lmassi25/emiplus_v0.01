using Emiplus.Data.Helpers;
using SqlKata.Execution;
using System;
using System.Windows.Forms;

namespace Emiplus.Controller
{
    class Titulo : Data.Core.Controller
    {
        public double GetLancados(int idPedido)
        {
            if (String.IsNullOrEmpty(idPedido.ToString()))
                return 0;

            var data = new Model.Titulo().Query().SelectRaw("SUM(total) as total").Where("id", idPedido).First();
            return Validation.ConvertToDouble(data.TOTAL);
        }

        public double GetRestante(int idPedido)
        {
            if (String.IsNullOrEmpty(idPedido.ToString()))
                return 0;

            var data = new Model.Titulo().Query().SelectRaw("SUM(total) as total").Where("id", idPedido).First();
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

            var data = new Model.Pedido().FindById(idPedido).Select("total").First();
            return Validation.ConvertToDouble(data.TOTAL);
        }

        public bool AddPagamento(int idPedido, int formaPgto, string valorS, string parcela = "1")
        {
            var data = new Model.Titulo();

            double valor = Validation.ConvertToDouble(valorS);

            if (idPedido > 0)
            {
                data.Id_Pedido = idPedido;
                data.Id_Pessoa = new Model.Pedido().FindById(idPedido).Select("cliente").First().CLIENTE;
            }
            
            switch (formaPgto)
            {
                case 1:
                    data.Vencimento = DateTime.Now;
                    data.Total = valor;
                    break;
                case 2:
                    //data.Total = Validation.ConvertToDouble(formaPgto);
                    break;
                case 3:
                    //data.Total = Validation.ConvertToDouble(formaPgto);
                    break;
                case 4:
                    //data.Total = Validation.ConvertToDouble(formaPgto);
                    break;
                case 5:
                    //data.Total = Validation.ConvertToDouble(formaPgto);
                    break;
                case 6:
                    //data.Total = Validation.ConvertToDouble(formaPgto);
                    break;
            }

            data.Id = 0;
            data.Id_FormaPgto = formaPgto;
            data.Emissao = DateTime.Now;

            if (data.Save(data))
                return true;

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
                .Select("titulo.id", "titulo.total", "titulo.emissao", "formapgto.nome as formapgto")
                .Where("titulo.excluir", 0)
                .Where("titulo.id_pedido", idPedido)
                .OrderByDesc("titulo.id")
                .Get();

            foreach (var item in data)
            {
                Table.Rows.Add(
                    item.ID,
                    item.FORMAPGTO,
                    Validation.ConvertDateToForm(item.EMISSAO),                    
                    Validation.FormatPrice(Validation.ConvertToDouble(item.TOTAL), true)
                );
            }
        }
    }
}
