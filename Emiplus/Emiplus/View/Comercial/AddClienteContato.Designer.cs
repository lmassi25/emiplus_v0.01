namespace Emiplus.View.Comercial
{
    partial class AddClienteContato
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddClienteContato));
            this.label3 = new System.Windows.Forms.Label();
            this.email = new System.Windows.Forms.TextBox();
            this.contato = new System.Windows.Forms.TextBox();
            this.telefone = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnContatoSalvar = new System.Windows.Forms.Button();
            this.btnContatoCancelar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.celular = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Open Sans SemiBold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(12, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 17);
            this.label3.TabIndex = 28;
            this.label3.Text = "E-mail";
            // 
            // email
            // 
            this.email.Location = new System.Drawing.Point(15, 156);
            this.email.Name = "email";
            this.email.Size = new System.Drawing.Size(236, 24);
            this.email.TabIndex = 27;
            // 
            // contato
            // 
            this.contato.Location = new System.Drawing.Point(15, 100);
            this.contato.Name = "contato";
            this.contato.Size = new System.Drawing.Size(236, 24);
            this.contato.TabIndex = 25;
            // 
            // telefone
            // 
            this.telefone.Location = new System.Drawing.Point(257, 100);
            this.telefone.Mask = "(99) 0000-0000";
            this.telefone.Name = "telefone";
            this.telefone.Size = new System.Drawing.Size(197, 24);
            this.telefone.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Open Sans SemiBold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(253, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 17);
            this.label1.TabIndex = 23;
            this.label1.Text = "Telefone";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Open Sans Light", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(10, 20);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(131, 26);
            this.label11.TabIndex = 5;
            this.label11.Text = "Novo contato:";
            // 
            // btnContatoSalvar
            // 
            this.btnContatoSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnContatoSalvar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnContatoSalvar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnContatoSalvar.FlatAppearance.BorderSize = 0;
            this.btnContatoSalvar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnContatoSalvar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnContatoSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContatoSalvar.Font = new System.Drawing.Font("Open Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnContatoSalvar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnContatoSalvar.Image = ((System.Drawing.Image)(resources.GetObject("btnContatoSalvar.Image")));
            this.btnContatoSalvar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnContatoSalvar.Location = new System.Drawing.Point(516, 5);
            this.btnContatoSalvar.Name = "btnContatoSalvar";
            this.btnContatoSalvar.Size = new System.Drawing.Size(65, 60);
            this.btnContatoSalvar.TabIndex = 4;
            this.btnContatoSalvar.Text = "Salvar";
            this.btnContatoSalvar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnContatoSalvar.UseVisualStyleBackColor = true;
            this.btnContatoSalvar.Click += new System.EventHandler(this.BtnContatoSalvar_Click);
            // 
            // btnContatoCancelar
            // 
            this.btnContatoCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnContatoCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnContatoCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnContatoCancelar.FlatAppearance.BorderSize = 0;
            this.btnContatoCancelar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnContatoCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnContatoCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContatoCancelar.Font = new System.Drawing.Font("Open Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnContatoCancelar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnContatoCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnContatoCancelar.Image")));
            this.btnContatoCancelar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnContatoCancelar.Location = new System.Drawing.Point(582, 5);
            this.btnContatoCancelar.Name = "btnContatoCancelar";
            this.btnContatoCancelar.Size = new System.Drawing.Size(69, 60);
            this.btnContatoCancelar.TabIndex = 3;
            this.btnContatoCancelar.Text = "Cancelar";
            this.btnContatoCancelar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnContatoCancelar.UseVisualStyleBackColor = true;
            this.btnContatoCancelar.Click += new System.EventHandler(this.BtnContatoCancelar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Open Sans SemiBold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(12, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 17);
            this.label2.TabIndex = 26;
            this.label2.Text = "Contato";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.btnContatoSalvar);
            this.panel1.Controls.Add(this.btnContatoCancelar);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(670, 68);
            this.panel1.TabIndex = 22;
            // 
            // celular
            // 
            this.celular.Location = new System.Drawing.Point(460, 100);
            this.celular.Mask = "(99) 90000-0000";
            this.celular.Name = "celular";
            this.celular.Size = new System.Drawing.Size(197, 24);
            this.celular.TabIndex = 30;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Open Sans SemiBold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(456, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 17);
            this.label4.TabIndex = 29;
            this.label4.Text = "Celular";
            // 
            // AddClienteContato
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(670, 200);
            this.Controls.Add(this.celular);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.email);
            this.Controls.Add(this.contato);
            this.Controls.Add(this.telefone);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Open Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "AddClienteContato";
            this.Text = "AddClienteContato";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox email;
        private System.Windows.Forms.TextBox contato;
        private System.Windows.Forms.MaskedTextBox telefone;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnContatoSalvar;
        private System.Windows.Forms.Button btnContatoCancelar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MaskedTextBox celular;
        private System.Windows.Forms.Label label4;
    }
}