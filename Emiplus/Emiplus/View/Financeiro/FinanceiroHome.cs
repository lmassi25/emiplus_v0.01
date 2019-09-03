using SqlKata.Execution;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Emiplus.View.Financeiro
{
    public partial class FinanceiroHome : Form
    {
        public FinanceiroHome()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //var p = new Controller.Produto();
            //var cl = p.lista().Where("NOME", "William");

            // model Produto
            Model.Item m = new Model.Item();

            //var todosRegistros = m.FindAll();

            var ordenados = m.Query().OrderByDesc("NOME").Get();
            //foreach (var data in ordenados)
            //{
            //    Console.WriteLine($"Nome: {data.NOME}");
            //}

            var findID = m.FindById(81);
            //Console.WriteLine(findID.NOME);

            var buscar = m.Query().Where("ID", 10).WhereFalse("TIPO").First();
            //Console.WriteLine(buscar.NOME);

            if (m.Remove(82))
                Console.WriteLine("Removido com sucesso.");

            //m.Id = 82;
            m.Nome = "N";

            //var affected = m.Save(m);
            //Console.WriteLine(affected);

            //Console.WriteLine(buscar.NOME);

            //foreach (var data in todosRegistros)
            //{
            //    Console.WriteLine($"Nome: {data.NOME}");
            //}
        }
    }
}
