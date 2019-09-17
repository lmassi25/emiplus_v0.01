using FirebirdSql.Data.FirebirdClient;
using SqlKata.Execution;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Testes
{
    public partial class Form1 : Form
    {
        private const string _path = @"C:\emiplus_v0.01\EMIPLUS.FDB";
        private const string _user = "sysdba";
        private const string _pass = "masterkey";
        private const string _db = "sysdba";
        private const string _host = "localhost";

        public Form1()
        {
            InitializeComponent();
        }

        private void T1()
        {
            //dataGridView1.ColumnCount = 2;

            //dataGridView1.Columns[0].Name = "ID";
            //dataGridView1.Columns[0].Visible = false;

            //dataGridView1.Columns[1].Name = "Nome / Razão social";
            //dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dataGridView1.Rows.Clear();

            //int count = 0;

            //while (count <= 20000)
            //{
            //    dataGridView1.Rows.Add("B's Beverages " + count, "Victoria Ashworth " + count);
            //    count++;
            //}
        }

        private void T2()
        {
            
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var p = new Model.Pessoa();
            var select = p.FindAll().Get();

            foreach (var data in select)
            {
                var firstName = data.NOME;
                //Console.WriteLine(firstName);
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            Console.WriteLine("KATA: " + elapsedMs);
        }

        private void T3()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            FbConnection SQLCon = new FbConnection(
                $"character set=NONE;initial catalog={_path};user id={_user};data source={_host};user id={_db};Password={_pass};Pooling=true;Dialect=3"
            );

            FbCommand cmd;
            FbDataReader res;

            cmd = new FbCommand("SELECT * FROM pessoa", SQLCon);
            
            SQLCon.Open();
            using (var oReader = cmd.ExecuteReader())
            {
                while (oReader.Read())
                {
                    var firstName = oReader["nome"].ToString();
                    //Console.WriteLine(firstName);
                }


                //foreach (IDataRecord record in GetFromReader(oReader))
                // {
                //     var firstName = record["nome"].ToString();
                //     Console.WriteLine(firstName);
                // }

                SQLCon.Close();
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            Console.WriteLine("sql_1: " + elapsedMs);
        }

        private void T4()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            FbConnection SQLCon = new FbConnection(
                $"character set=NONE;initial catalog={_path};user id={_user};data source={_host};user id={_db};Password={_pass};Pooling=true;Dialect=3"
            );

            FbCommand cmd;
            FbDataReader res;

            cmd = new FbCommand("SELECT * FROM pessoa", SQLCon);

            SQLCon.Open();
            using (var oReader = cmd.ExecuteReader())
            {
                //while (oReader.Read())
                //{
                //    var firstName = oReader["nome"].ToString();
                //    Console.WriteLine(firstName);
                //}


                foreach (IDataRecord record in GetFromReader(oReader))
                {
                    var firstName = record["nome"].ToString();
                    //Console.WriteLine(firstName);
                }

                SQLCon.Close();
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            Console.WriteLine("sql_2: " + elapsedMs);
        }

        IEnumerable<IDataRecord> GetFromReader(IDataReader reader)
        {
            while (reader.Read()) yield return reader;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //T3();
            //T4();
            //T2();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            T2();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            T3();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            T4();
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            T2();
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            backgroundWorker2.RunWorkerAsync();
        }

        private void BackgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            T3();
        }
    }
}
