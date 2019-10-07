using Emiplus.Data.Database;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Linq;
using System.Reflection;

namespace Emiplus.View.Testes
{
    public partial class cPure : Form
    {
        public cPure()
        {
            InitializeComponent();
        }

        private Dictionary<string, string> param;

        private void Button1_Click(object sender, EventArgs e)
        {

            //FbConnection SQLCon = ConnectPure.Connection();
            //if (SQLCon.State == ConnectionState.Closed)
            //{
            //    SQLCon.Open();
            //}

            //FbCommand cmd = new FbCommand("SELECT * FROM PESSOA", SQLCon);
            //FbDataReader res = cmd.ExecuteReader();
            //while (res.Read())
            //{
            //    Console.WriteLine(res["nome"].ToString());
            //}

            //if (SQLCon.State == ConnectionState.Open)
            //    SQLCon.Close();

            //SQLCon = null;

            Transaction.Open();
            var teste = new ModelPure("PESSOA");

            var data = teste.Find().Fetch();
            while (data.Read())
            {
                Console.WriteLine(data["nome"].ToString());
                Console.WriteLine(data["pessoatipo"].ToString());
            }

            //var id = teste.FindById(1);
            //while (id.Read())
            //{
            //    Console.WriteLine(id["nome"].ToString());
            //    Console.WriteLine(id["pessoatipo"].ToString());
            //}

            //var all = teste.Find().Fetch();
            //while (all.Read())
            //{
            //    Console.WriteLine(all["id"].ToString());
            //    Console.WriteLine(all["nome"].ToString());
            //    Console.WriteLine(all["pessoatipo"].ToString());
            //}

            //var inteiro = teste.Count();
            //while (inteiro.Read())
            //{
            //    Console.WriteLine(inteiro["id"].ToString());
            //}

            //var s = teste.Update("ID = @id", "tipo = @tipo, nome = @nome", "id=26&tipo=Clientes&nome=Maria91").FetchNonQuery();
            //Console.WriteLine(s);

            //var c = teste.Create("@tipo, @nome", "tipo=Clientes&nome=Maria93").FetchScalar();
            //Console.WriteLine(c);
            Transaction.Close();
        }
    }

}
