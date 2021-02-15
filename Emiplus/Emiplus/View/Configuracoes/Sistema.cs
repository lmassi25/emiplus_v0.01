using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Emiplus.Data.Core;
using Emiplus.Data.Database;
using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.View.Common;

using SqlKata.Execution;

namespace Emiplus.View.Configuracoes
{
    public partial class Sistema : Form
    {
        private Model.PedidoItem _pedidoItem = new PedidoItem();
        
        public Sistema()
        {
            InitializeComponent();
            Eventos();
        }

        private void Eventos()
        {
            Shown += (s, e) =>
            {
                if (File.Exists(Program.PATH_BASE + "\\logs\\EXCEPTIONS.txt"))
                    erros.Text = File.ReadAllText(Program.PATH_BASE + "\\logs\\EXCEPTIONS.txt");

                if (!string.IsNullOrEmpty(IniFile.Read("Remoto", "LOCAL")))
                    ip.Text = IniFile.Read("Remoto", "LOCAL");

                if (!string.IsNullOrEmpty(IniFile.Read("syncAuto", "APP")))
                    syncAuto.Toggled = IniFile.Read("syncAuto", "APP") == "True";
            };

            syncAuto.Click += (s, e) =>
            {
                IniFile.Write("syncAuto", syncAuto.Toggled ? "False" : "True", "APP");
            };

            AtualizaDb.Click += (s, e) =>
            {
                new CreateTables().CheckTables();

                Alert.Message("Pronto!", "Banco de dados atualizado com sucesso!", Alert.AlertType.success);
            };

            btnClearErroLog.Click += (s, e) =>
            {
                var result = AlertOptions.Message("Atenção!",
                    $"Você está prestes a limpar o log de erros.{Environment.NewLine}Deseja continuar?",
                    AlertBig.AlertType.info, AlertBig.AlertBtn.YesNo);
                if (result)
                {
                    File.Delete(Program.PATH_BASE + "\\logs\\EXCEPTIONS.txt");
                    erros.Text = "";
                }
            };

            ip.Leave += (s, e) => IniFile.Write("Remoto", ip.Text, "LOCAL");

            margemSincronizar.Click += (s, e) =>
            {
                margem();
            };

            btnExit.Click += (s, e) => Close();
        }

        public void margem()
        {
            var itens = GetDataTable();

            foreach (var item in itens)
            {                
                GetId(Validation.ConvertToInt32(item.ID));
                if (_pedidoItem != null)
                {
                    if (_pedidoItem.ValorCompra > 0)
                    {
                        _pedidoItem.TotalCompra = Validation.Round(_pedidoItem.ValorCompra * _pedidoItem.Quantidade);
                        _pedidoItem.Save(_pedidoItem);
                    }                    
                }
            }     
        }
        
        public void GetId(int id)
        {
            //_pedidoItem = _pedidoItem.Query().Where("Id", id).FirstOrDefault<Model.PedidoItem>();
            _pedidoItem = _pedidoItem.FindById(id).FirstOrDefault<Model.PedidoItem>();
        }
        
        public IEnumerable<dynamic> GetDataTable()
        {
            var model = new Model.PedidoItem().Query();
            
            model.Where("totalcompra", "=", 0);
            model.Where("valorcompra", ">", 0);
            model.Where("excluir", "=", 0);

            return model.Get();
        }
    }
}