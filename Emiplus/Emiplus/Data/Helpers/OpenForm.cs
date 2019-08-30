﻿using System.Linq;
using System.Windows.Forms;

namespace Emiplus.Data.Helpers
{
    public static class OpenForm
    {
        /// <summary>
        /// Adiciona um Form dentro de outro Form
        /// </summary>
        /// <typeparam name="MeuForm">Form externo</typeparam>
        /// <param name="Principal">Form atual</param>
        public static void Show<MeuForm>(Form Principal) where MeuForm : Form, new()
        {
            Form formulario;
            formulario = Principal.Controls.OfType<MeuForm>().FirstOrDefault();

            if (formulario == null)
            {
                formulario = new MeuForm();
                formulario.TopLevel = false;
                formulario.FormBorderStyle = FormBorderStyle.None;
                formulario.Dock = DockStyle.Fill;
                //formulario.Anchor = (AnchorStyles.Left | AnchorStyles.Top);
                //formulario.Size = new Size(Principal.Width, Principal.Height);
                Principal.Controls.Add(formulario);
                Principal.Tag = formulario;
                formulario.Show();
                formulario.BringToFront();
            }
            else
            {
                formulario.BringToFront();
            }
        }

        public static void ShowInPanel<MeuForm>(Panel Principal) where MeuForm : Form, new()
        {
            Form formulario;
            formulario = Principal.Controls.OfType<MeuForm>().FirstOrDefault();

            if (formulario == null)
            {
                formulario = new MeuForm();
                formulario.TopLevel = false;
                formulario.FormBorderStyle = FormBorderStyle.None;
                formulario.Dock = DockStyle.Fill;
                //formulario.Anchor = (AnchorStyles.Left | AnchorStyles.Top);
                //formulario.Size = new Size(Principal.Width, Principal.Height);
                Principal.Controls.Add(formulario);
                Principal.Tag = formulario;
                formulario.Show();
                formulario.BringToFront();
            }
            else
            {
                formulario.BringToFront();
            }
        }
    }
}