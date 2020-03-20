using Emiplus.Data.Core;
using System;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace Emiplus.View.Configuracoes
{
    public partial class OS : Form
    {
        private Timer timer = new Timer(Configs.TimeLoading);

        public OS()
        {
            InitializeComponent();
            Eventos();
        }

        private void Save()
        {
            if(!String.IsNullOrEmpty(label1Descr.Text))
                IniFile.Write("Campo_1_Descr", label1Descr.Text, "OS");

            if (!String.IsNullOrEmpty(label2Descr.Text))
                IniFile.Write("Campo_2_Descr", label2Descr.Text, "OS");

            if (!String.IsNullOrEmpty(label3Descr.Text))
                IniFile.Write("Campo_3_Descr", label3Descr.Text, "OS");

            if (!String.IsNullOrEmpty(label4Descr.Text))
                IniFile.Write("Campo_4_Descr", label4Descr.Text, "OS");

            if (!String.IsNullOrEmpty(label5Descr.Text))
                IniFile.Write("Campo_5_Descr", label5Descr.Text, "OS");

            if (!String.IsNullOrEmpty(label6Descr.Text))
                IniFile.Write("Campo_6_Descr", label6Descr.Text, "OS");
        }

        private void Eventos()
        {
            Shown += (s, e) =>
            {
                label1Visible.Checked = true;//!String.IsNullOrEmpty(IniFile.Read("Campo_1_Visible", "OS")) ? Convert.ToBoolean(IniFile.Read("Campo_1_Visible", "OS")) : false;
                label1Pesquisa.Checked = true;//!String.IsNullOrEmpty(IniFile.Read("Campo_1_Pesquisa", "OS")) ? Convert.ToBoolean(IniFile.Read("Campo_1_Pesquisa", "OS")) : false;
                label1Descr.Text = IniFile.Read("Campo_1_Descr", "OS");

                label2Visible.Checked = true;//!String.IsNullOrEmpty(IniFile.Read("Campo_2_Visible", "OS")) ? Convert.ToBoolean(IniFile.Read("Campo_2_Visible", "OS")) : false;
                label2Pesquisa.Checked = true;//!String.IsNullOrEmpty(IniFile.Read("Campo_2_Pesquisa", "OS")) ? Convert.ToBoolean(IniFile.Read("Campo_2_Pesquisa", "OS")) : false;
                label2Descr.Text = IniFile.Read("Campo_2_Descr", "OS");

                label3Visible.Checked = true;//!String.IsNullOrEmpty(IniFile.Read("Campo_3_Visible", "OS")) ? Convert.ToBoolean(IniFile.Read("Campo_3_Visible", "OS")) : false;
                label3Pesquisa.Checked = true;//!String.IsNullOrEmpty(IniFile.Read("Campo_3_Pesquisa", "OS")) ? Convert.ToBoolean(IniFile.Read("Campo_3_Pesquisa", "OS")) : false;
                label3Descr.Text = IniFile.Read("Campo_3_Descr", "OS");

                label4Visible.Checked = !String.IsNullOrEmpty(IniFile.Read("Campo_4_Visible", "OS")) ? Convert.ToBoolean(IniFile.Read("Campo_4_Visible", "OS")) : false;
                label4Pesquisa.Checked = !String.IsNullOrEmpty(IniFile.Read("Campo_4_Pesquisa", "OS")) ? Convert.ToBoolean(IniFile.Read("Campo_4_Pesquisa", "OS")) : false;
                label4Descr.Text = IniFile.Read("Campo_4_Descr", "OS");

                label5Visible.Checked = !String.IsNullOrEmpty(IniFile.Read("Campo_5_Visible", "OS")) ? Convert.ToBoolean(IniFile.Read("Campo_5_Visible", "OS")) : false;
                label5Pesquisa.Checked = !String.IsNullOrEmpty(IniFile.Read("Campo_5_Pesquisa", "OS")) ? Convert.ToBoolean(IniFile.Read("Campo_5_Pesquisa", "OS")) : false;
                label5Descr.Text = IniFile.Read("Campo_5_Descr", "OS");

                label6Visible.Checked = !String.IsNullOrEmpty(IniFile.Read("Campo_6_Visible", "OS")) ? Convert.ToBoolean(IniFile.Read("Campo_6_Visible", "OS")) : false;
                label6Pesquisa.Checked = !String.IsNullOrEmpty(IniFile.Read("Campo_6_Pesquisa", "OS")) ? Convert.ToBoolean(IniFile.Read("Campo_6_Pesquisa", "OS")) : false;
                label6Descr.Text = IniFile.Read("Campo_6_Descr", "OS");
            };

            label1Visible.Click += (s, e) =>
            {
                if (label1Visible.Checked)
                    IniFile.Write("Campo_1_Visible", "True", "OS");
                else
                    IniFile.Write("Campo_1_Visible", "False", "OS");
            };

            label1Pesquisa.Click += (s, e) =>
            {
                if (label1Pesquisa.Checked)
                    IniFile.Write("Campo_1_Pesquisa", "True", "OS");
                else
                    IniFile.Write("Campo_1_Pesquisa", "False", "OS");
            };

            label2Visible.Click += (s, e) =>
            {
                if (label2Visible.Checked)
                    IniFile.Write("Campo_2_Visible", "True", "OS");
                else
                    IniFile.Write("Campo_2_Visible", "False", "OS");
            };

            label2Pesquisa.Click += (s, e) =>
            {
                if (label2Pesquisa.Checked)
                    IniFile.Write("Campo_2_Pesquisa", "True", "OS");
                else
                    IniFile.Write("Campo_2_Pesquisa", "False", "OS");
            };

            label3Visible.Click += (s, e) =>
            {
                if (label3Visible.Checked)
                    IniFile.Write("Campo_3_Visible", "True", "OS");
                else
                    IniFile.Write("Campo_3_Visible", "False", "OS");
            };

            label3Pesquisa.Click += (s, e) =>
            {
                if (label3Pesquisa.Checked)
                    IniFile.Write("Campo_3_Pesquisa", "True", "OS");
                else
                    IniFile.Write("Campo_3_Pesquisa", "False", "OS");
            };

            label4Visible.Click += (s, e) =>
            {
                if (label4Visible.Checked)
                    IniFile.Write("Campo_4_Visible", "True", "OS");
                else
                    IniFile.Write("Campo_4_Visible", "False", "OS");
            };

            label4Pesquisa.Click += (s, e) =>
            {
                if (label4Pesquisa.Checked)
                    IniFile.Write("Campo_4_Pesquisa", "True", "OS");
                else
                    IniFile.Write("Campo_4_Pesquisa", "False", "OS");
            };

            label5Visible.Click += (s, e) =>
            {
                if (label5Visible.Checked)
                    IniFile.Write("Campo_5_Visible", "True", "OS");
                else
                    IniFile.Write("Campo_5_Visible", "False", "OS");
            };

            label5Pesquisa.Click += (s, e) =>
            {
                if (label5Pesquisa.Checked)
                    IniFile.Write("Campo_5_Pesquisa", "True", "OS");
                else
                    IniFile.Write("Campo_5_Pesquisa", "False", "OS");
            };

            label6Visible.Click += (s, e) =>
            {
                if (label6Visible.Checked)
                    IniFile.Write("Campo_6_Visible", "True", "OS");
                else
                    IniFile.Write("Campo_6_Visible", "False", "OS");
            };

            label6Pesquisa.Click += (s, e) =>
            {
                if (label6Pesquisa.Checked)
                    IniFile.Write("Campo_6_Pesquisa", "True", "OS");
                else
                    IniFile.Write("Campo_6_Pesquisa", "False", "OS");
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
            timer.Elapsed += (s, e) =>
            {
                Save();
            };

            btnExit.Click += (s, e) => Close();
        }
    }
}