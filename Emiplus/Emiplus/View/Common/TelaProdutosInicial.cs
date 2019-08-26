using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emiplus.Data.Helpers;

namespace Emiplus.View.Common
{
    public partial class TelaProdutosInicial : Form
    {
        public TelaProdutosInicial()
        {
            InitializeComponent();
        }

        public void AbrirForm<MeuForm>() where MeuForm : Form, new()
        {
            Form formulario;
            formulario = Controls.OfType<MeuForm>().FirstOrDefault();

            if (formulario == null)
            {
                formulario = new MeuForm();
                formulario.TopLevel = false;
                formulario.FormBorderStyle = FormBorderStyle.None;
                formulario.Dock = DockStyle.Fill;
                formulario.Anchor = (AnchorStyles.Left | AnchorStyles.Top);
                formulario.Size = new Size(Width, Height);
                Controls.Add(formulario);
                Tag = formulario;
                formulario.Show();
                formulario.BringToFront();
            }
            else
            {
                formulario.BringToFront();
            }
        }
        
        private void Products_Click(object sender, EventArgs e)
        {
            AbrirForm<Produtos.Produtos>();
            //new OpenForm().Show<TelaComercialInicial>();

            //Home home = new Home();
            //home.AbrirForm<TelaComercialInicial>();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            AbrirForm<Produtos.Produtos>();
            //OpenForm.Show<Produtos.Produtos>(this);
        }
    }
}
