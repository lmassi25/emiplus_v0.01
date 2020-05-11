using System;
using System.Windows.Forms;
using Emiplus.Data.Core;
using Timer = System.Timers.Timer;

namespace Emiplus.View.Configuracoes
{
    public partial class OS : Form
    {
        private readonly Timer timer = new Timer(Configs.TimeLoading);

        public OS()
        {
            InitializeComponent();
            Eventos();
        }

        private void Save()
        {
            if (!string.IsNullOrEmpty(label1Descr.Text))
                IniFile.Write("Campo_1_Descr", label1Descr.Text, "OS");

            if (!string.IsNullOrEmpty(label2Descr.Text))
                IniFile.Write("Campo_2_Descr", label2Descr.Text, "OS");

            if (!string.IsNullOrEmpty(label3Descr.Text))
                IniFile.Write("Campo_3_Descr", label3Descr.Text, "OS");

            if (!string.IsNullOrEmpty(label4Descr.Text))
                IniFile.Write("Campo_4_Descr", label4Descr.Text, "OS");

            if (!string.IsNullOrEmpty(label5Descr.Text))
                IniFile.Write("Campo_5_Descr", label5Descr.Text, "OS");

            if (!string.IsNullOrEmpty(label6Descr.Text))
                IniFile.Write("Campo_6_Descr", label6Descr.Text, "OS");
        }

        private void Eventos()
        {
            Shown += (s, e) =>
            {
                label1Visible.Checked =
                    true; //!String.IsNullOrEmpty(IniFile.Read("Campo_1_Visible", "OS")) ? Convert.ToBoolean(IniFile.Read("Campo_1_Visible", "OS")) : false;
                label1Pesquisa.Checked =
                    true; //!String.IsNullOrEmpty(IniFile.Read("Campo_1_Pesquisa", "OS")) ? Convert.ToBoolean(IniFile.Read("Campo_1_Pesquisa", "OS")) : false;
                label1Descr.Text = IniFile.Read("Campo_1_Descr", "OS");

                label2Visible.Checked =
                    true; //!String.IsNullOrEmpty(IniFile.Read("Campo_2_Visible", "OS")) ? Convert.ToBoolean(IniFile.Read("Campo_2_Visible", "OS")) : false;
                label2Pesquisa.Checked =
                    true; //!String.IsNullOrEmpty(IniFile.Read("Campo_2_Pesquisa", "OS")) ? Convert.ToBoolean(IniFile.Read("Campo_2_Pesquisa", "OS")) : false;
                label2Descr.Text = IniFile.Read("Campo_2_Descr", "OS");

                label3Visible.Checked =
                    true; //!String.IsNullOrEmpty(IniFile.Read("Campo_3_Visible", "OS")) ? Convert.ToBoolean(IniFile.Read("Campo_3_Visible", "OS")) : false;
                label3Pesquisa.Checked =
                    true; //!String.IsNullOrEmpty(IniFile.Read("Campo_3_Pesquisa", "OS")) ? Convert.ToBoolean(IniFile.Read("Campo_3_Pesquisa", "OS")) : false;
                label3Descr.Text = IniFile.Read("Campo_3_Descr", "OS");

                label4Visible.Checked = !string.IsNullOrEmpty(IniFile.Read("Campo_4_Visible", "OS")) && Convert.ToBoolean(IniFile.Read("Campo_4_Visible", "OS"));
                label4Pesquisa.Checked = !string.IsNullOrEmpty(IniFile.Read("Campo_4_Pesquisa", "OS")) && Convert.ToBoolean(IniFile.Read("Campo_4_Pesquisa", "OS"));
                label4Descr.Text = IniFile.Read("Campo_4_Descr", "OS");

                label5Visible.Checked = !string.IsNullOrEmpty(IniFile.Read("Campo_5_Visible", "OS")) && Convert.ToBoolean(IniFile.Read("Campo_5_Visible", "OS"));
                label5Pesquisa.Checked = !string.IsNullOrEmpty(IniFile.Read("Campo_5_Pesquisa", "OS")) && Convert.ToBoolean(IniFile.Read("Campo_5_Pesquisa", "OS"));
                label5Descr.Text = IniFile.Read("Campo_5_Descr", "OS");

                label6Visible.Checked = !string.IsNullOrEmpty(IniFile.Read("Campo_6_Visible", "OS")) && Convert.ToBoolean(IniFile.Read("Campo_6_Visible", "OS"));
                label6Pesquisa.Checked = !string.IsNullOrEmpty(IniFile.Read("Campo_6_Pesquisa", "OS")) && Convert.ToBoolean(IniFile.Read("Campo_6_Pesquisa", "OS"));
                label6Descr.Text = IniFile.Read("Campo_6_Descr", "OS");
            };

            label1Visible.Click += (s, e) =>
            {
                IniFile.Write("Campo_1_Visible", label1Visible.Checked ? "True" : "False", "OS");
            };

            label1Pesquisa.Click += (s, e) =>
            {
                IniFile.Write("Campo_1_Pesquisa", label1Pesquisa.Checked ? "True" : "False", "OS");
            };

            label2Visible.Click += (s, e) =>
            {
                IniFile.Write("Campo_2_Visible", label2Visible.Checked ? "True" : "False", "OS");
            };

            label2Pesquisa.Click += (s, e) =>
            {
                IniFile.Write("Campo_2_Pesquisa", label2Pesquisa.Checked ? "True" : "False", "OS");
            };

            label3Visible.Click += (s, e) =>
            {
                IniFile.Write("Campo_3_Visible", label3Visible.Checked ? "True" : "False", "OS");
            };

            label3Pesquisa.Click += (s, e) =>
            {
                IniFile.Write("Campo_3_Pesquisa", label3Pesquisa.Checked ? "True" : "False", "OS");
            };

            label4Visible.Click += (s, e) =>
            {
                IniFile.Write("Campo_4_Visible", label4Visible.Checked ? "True" : "False", "OS");
            };

            label4Pesquisa.Click += (s, e) =>
            {
                IniFile.Write("Campo_4_Pesquisa", label4Pesquisa.Checked ? "True" : "False", "OS");
            };

            label5Visible.Click += (s, e) =>
            {
                IniFile.Write("Campo_5_Visible", label5Visible.Checked ? "True" : "False", "OS");
            };

            label5Pesquisa.Click += (s, e) =>
            {
                IniFile.Write("Campo_5_Pesquisa", label5Pesquisa.Checked ? "True" : "False", "OS");
            };

            label6Visible.Click += (s, e) =>
            {
                IniFile.Write("Campo_6_Visible", label6Visible.Checked ? "True" : "False", "OS");
            };

            label6Pesquisa.Click += (s, e) =>
            {
                IniFile.Write("Campo_6_Pesquisa", label6Pesquisa.Checked ? "True" : "False", "OS");
            };

            label1Descr.TextChanged += (s, e) =>
            {
                timer.Stop();
                timer.Start();
            };

            label2Descr.TextChanged += (s, e) =>
            {
                timer.Stop();
                timer.Start();
            };

            label3Descr.TextChanged += (s, e) =>
            {
                timer.Stop();
                timer.Start();
            };

            label4Descr.TextChanged += (s, e) =>
            {
                timer.Stop();
                timer.Start();
            };

            label5Descr.TextChanged += (s, e) =>
            {
                timer.Stop();
                timer.Start();
            };

            label6Descr.TextChanged += (s, e) =>
            {
                timer.Stop();
                timer.Start();
            };

            timer.AutoReset = false;
            timer.Elapsed += (s, e) => { Save(); };

            btnExit.Click += (s, e) => Close();
        }
    }
}