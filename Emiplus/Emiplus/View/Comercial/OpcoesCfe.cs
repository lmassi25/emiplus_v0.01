using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class OpcoesCfe : Form
    {
        public static int idPedido { get; set; } // id pedido

        private BackgroundWorker WorkerBackground = new BackgroundWorker();

        private string _msg;

        public OpcoesCfe()
        {
            InitializeComponent();

            Eventos();
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {                
                //case Keys.Escape:
                //    Close();
                //    break;
            }
        }

        /// <summary>
        /// Eventos do form
        /// </summary>
        public void Eventos()
        {
            //KeyPreview = true;
            KeyDown += KeyDowns;

            Load += (s, e) =>
            {
                
            };

            Emitir.KeyDown += KeyDowns;

            Emitir.Click += (s, e) =>
            {
                WorkerBackground.RunWorkerAsync();
            };

            using (var b = WorkerBackground)
            {
                b.DoWork += async (s, e) =>
                {                    
                    _msg = new Controller.Fiscal().Emitir(idPedido, "CFe");
                };

                b.RunWorkerCompleted += async (s, e) =>
                {
                    retorno.Text = _msg;
                    Emitir.Enabled = true;
                };
            }
        }
    }
}
