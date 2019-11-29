namespace Emiplus.View.Common
{
    partial class TelaFinanceiroInicial
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TelaFinanceiroInicial));
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.novoPagamento = new System.Windows.Forms.Button();
            this.aPagar = new System.Windows.Forms.Button();
            this.aReceber = new System.Windows.Forms.Button();
            this.novoRecebimento = new System.Windows.Forms.Button();
            this.visualSeparator4 = new VisualPlus.Toolkit.Controls.Layout.VisualSeparator();
            this.label2 = new System.Windows.Forms.Label();
            this.visualSeparator6 = new VisualPlus.Toolkit.Controls.Layout.VisualSeparator();
            this.label7 = new System.Windows.Forms.Label();
            this.AbrirCaixa = new System.Windows.Forms.Button();
            this.visualSeparator5 = new VisualPlus.Toolkit.Controls.Layout.VisualSeparator();
            this.label4 = new System.Windows.Forms.Label();
            this.Caixa = new System.Windows.Forms.Button();
            this.Clientes = new System.Windows.Forms.Button();
            this.fornecedores = new System.Windows.Forms.Button();
            this.Categorias = new System.Windows.Forms.Button();
            this.FecharCaixa = new System.Windows.Forms.Button();
            this.EntradaSaidaCaixa = new System.Windows.Forms.Button();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(10, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "Você está em:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label8.Location = new System.Drawing.Point(134, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 17);
            this.label8.TabIndex = 3;
            this.label8.Text = "Financeiro";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.pictureBox2);
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(733, 40);
            this.panel4.TabIndex = 93;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(110, 11);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(20, 20);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // novoPagamento
            // 
            this.novoPagamento.BackColor = System.Drawing.Color.Transparent;
            this.novoPagamento.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.novoPagamento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.novoPagamento.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.novoPagamento.FlatAppearance.BorderSize = 0;
            this.novoPagamento.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.novoPagamento.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.novoPagamento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.novoPagamento.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.novoPagamento.ForeColor = System.Drawing.Color.DimGray;
            this.novoPagamento.Image = ((System.Drawing.Image)(resources.GetObject("novoPagamento.Image")));
            this.novoPagamento.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.novoPagamento.Location = new System.Drawing.Point(256, 140);
            this.novoPagamento.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.novoPagamento.Name = "novoPagamento";
            this.novoPagamento.Size = new System.Drawing.Size(219, 41);
            this.novoPagamento.TabIndex = 90;
            this.novoPagamento.Text = "          Novo Pagamento";
            this.novoPagamento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.novoPagamento.UseVisualStyleBackColor = false;
            // 
            // aPagar
            // 
            this.aPagar.BackColor = System.Drawing.Color.Transparent;
            this.aPagar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.aPagar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.aPagar.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.aPagar.FlatAppearance.BorderSize = 0;
            this.aPagar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.aPagar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.aPagar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.aPagar.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aPagar.ForeColor = System.Drawing.Color.DimGray;
            this.aPagar.Image = ((System.Drawing.Image)(resources.GetObject("aPagar.Image")));
            this.aPagar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.aPagar.Location = new System.Drawing.Point(491, 140);
            this.aPagar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.aPagar.Name = "aPagar";
            this.aPagar.Size = new System.Drawing.Size(219, 41);
            this.aPagar.TabIndex = 89;
            this.aPagar.Text = "          Pagamentos";
            this.aPagar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.aPagar.UseVisualStyleBackColor = false;
            // 
            // aReceber
            // 
            this.aReceber.BackColor = System.Drawing.Color.Transparent;
            this.aReceber.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.aReceber.Cursor = System.Windows.Forms.Cursors.Hand;
            this.aReceber.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.aReceber.FlatAppearance.BorderSize = 0;
            this.aReceber.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.aReceber.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.aReceber.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.aReceber.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aReceber.ForeColor = System.Drawing.Color.DimGray;
            this.aReceber.Image = ((System.Drawing.Image)(resources.GetObject("aReceber.Image")));
            this.aReceber.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.aReceber.Location = new System.Drawing.Point(491, 94);
            this.aReceber.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.aReceber.Name = "aReceber";
            this.aReceber.Size = new System.Drawing.Size(219, 41);
            this.aReceber.TabIndex = 85;
            this.aReceber.Text = "          Recebimentos";
            this.aReceber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.aReceber.UseVisualStyleBackColor = false;
            // 
            // novoRecebimento
            // 
            this.novoRecebimento.BackColor = System.Drawing.Color.Transparent;
            this.novoRecebimento.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.novoRecebimento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.novoRecebimento.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.novoRecebimento.FlatAppearance.BorderSize = 0;
            this.novoRecebimento.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.novoRecebimento.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.novoRecebimento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.novoRecebimento.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.novoRecebimento.ForeColor = System.Drawing.Color.DimGray;
            this.novoRecebimento.Image = ((System.Drawing.Image)(resources.GetObject("novoRecebimento.Image")));
            this.novoRecebimento.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.novoRecebimento.Location = new System.Drawing.Point(256, 94);
            this.novoRecebimento.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.novoRecebimento.Name = "novoRecebimento";
            this.novoRecebimento.Size = new System.Drawing.Size(219, 41);
            this.novoRecebimento.TabIndex = 95;
            this.novoRecebimento.Text = "          Novo Recebimento";
            this.novoRecebimento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.novoRecebimento.UseVisualStyleBackColor = false;
            // 
            // visualSeparator4
            // 
            this.visualSeparator4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.visualSeparator4.Line = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(222)))), ((int)(((byte)(220)))));
            this.visualSeparator4.Location = new System.Drawing.Point(256, 83);
            this.visualSeparator4.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.visualSeparator4.Name = "visualSeparator4";
            this.visualSeparator4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.visualSeparator4.Shadow = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.visualSeparator4.ShadowVisible = true;
            this.visualSeparator4.Size = new System.Drawing.Size(220, 4);
            this.visualSeparator4.TabIndex = 102;
            this.visualSeparator4.Text = "visualSeparator4";
            this.visualSeparator4.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.visualSeparator4.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualSeparator4.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualSeparator4.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualSeparator4.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.visualSeparator4.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.visualSeparator4.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Light", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(252, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 25);
            this.label2.TabIndex = 101;
            this.label2.Text = "Movimentações";
            // 
            // visualSeparator6
            // 
            this.visualSeparator6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.visualSeparator6.Line = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(222)))), ((int)(((byte)(220)))));
            this.visualSeparator6.Location = new System.Drawing.Point(21, 83);
            this.visualSeparator6.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.visualSeparator6.Name = "visualSeparator6";
            this.visualSeparator6.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.visualSeparator6.Shadow = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.visualSeparator6.ShadowVisible = true;
            this.visualSeparator6.Size = new System.Drawing.Size(220, 4);
            this.visualSeparator6.TabIndex = 100;
            this.visualSeparator6.Text = "visualSeparator6";
            this.visualSeparator6.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.visualSeparator6.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualSeparator6.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualSeparator6.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualSeparator6.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.visualSeparator6.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.visualSeparator6.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Light", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Gray;
            this.label7.Location = new System.Drawing.Point(17, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 25);
            this.label7.TabIndex = 99;
            this.label7.Text = "Cadastros";
            // 
            // AbrirCaixa
            // 
            this.AbrirCaixa.BackColor = System.Drawing.Color.Transparent;
            this.AbrirCaixa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AbrirCaixa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AbrirCaixa.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.AbrirCaixa.FlatAppearance.BorderSize = 0;
            this.AbrirCaixa.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.AbrirCaixa.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.AbrirCaixa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AbrirCaixa.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AbrirCaixa.ForeColor = System.Drawing.Color.DimGray;
            this.AbrirCaixa.Image = ((System.Drawing.Image)(resources.GetObject("AbrirCaixa.Image")));
            this.AbrirCaixa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.AbrirCaixa.Location = new System.Drawing.Point(256, 186);
            this.AbrirCaixa.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.AbrirCaixa.Name = "AbrirCaixa";
            this.AbrirCaixa.Size = new System.Drawing.Size(219, 41);
            this.AbrirCaixa.TabIndex = 103;
            this.AbrirCaixa.Text = "          Abrir Caixa";
            this.AbrirCaixa.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.AbrirCaixa.UseVisualStyleBackColor = false;
            // 
            // visualSeparator5
            // 
            this.visualSeparator5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.visualSeparator5.Line = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(222)))), ((int)(((byte)(220)))));
            this.visualSeparator5.Location = new System.Drawing.Point(491, 83);
            this.visualSeparator5.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.visualSeparator5.Name = "visualSeparator5";
            this.visualSeparator5.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.visualSeparator5.Shadow = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.visualSeparator5.ShadowVisible = true;
            this.visualSeparator5.Size = new System.Drawing.Size(220, 4);
            this.visualSeparator5.TabIndex = 105;
            this.visualSeparator5.Text = "visualSeparator5";
            this.visualSeparator5.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.visualSeparator5.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualSeparator5.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualSeparator5.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualSeparator5.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.visualSeparator5.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.visualSeparator5.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Light", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(487, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 25);
            this.label4.TabIndex = 104;
            this.label4.Text = "Gerencial";
            // 
            // Caixa
            // 
            this.Caixa.BackColor = System.Drawing.Color.Transparent;
            this.Caixa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Caixa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Caixa.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Caixa.FlatAppearance.BorderSize = 0;
            this.Caixa.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Caixa.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.Caixa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Caixa.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Caixa.ForeColor = System.Drawing.Color.DimGray;
            this.Caixa.Image = ((System.Drawing.Image)(resources.GetObject("Caixa.Image")));
            this.Caixa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Caixa.Location = new System.Drawing.Point(491, 186);
            this.Caixa.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Caixa.Name = "Caixa";
            this.Caixa.Size = new System.Drawing.Size(219, 41);
            this.Caixa.TabIndex = 106;
            this.Caixa.Text = "          Caixa";
            this.Caixa.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Caixa.UseVisualStyleBackColor = false;
            // 
            // Clientes
            // 
            this.Clientes.BackColor = System.Drawing.Color.Transparent;
            this.Clientes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Clientes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Clientes.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Clientes.FlatAppearance.BorderSize = 0;
            this.Clientes.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Clientes.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.Clientes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Clientes.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Clientes.ForeColor = System.Drawing.Color.DimGray;
            this.Clientes.Image = ((System.Drawing.Image)(resources.GetObject("Clientes.Image")));
            this.Clientes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Clientes.Location = new System.Drawing.Point(22, 143);
            this.Clientes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Clientes.Name = "Clientes";
            this.Clientes.Size = new System.Drawing.Size(219, 41);
            this.Clientes.TabIndex = 107;
            this.Clientes.Text = "          Clientes";
            this.Clientes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Clientes.UseVisualStyleBackColor = false;
            // 
            // fornecedores
            // 
            this.fornecedores.BackColor = System.Drawing.Color.Transparent;
            this.fornecedores.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.fornecedores.Cursor = System.Windows.Forms.Cursors.Hand;
            this.fornecedores.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.fornecedores.FlatAppearance.BorderSize = 0;
            this.fornecedores.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.fornecedores.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.fornecedores.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fornecedores.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fornecedores.ForeColor = System.Drawing.Color.DimGray;
            this.fornecedores.Image = ((System.Drawing.Image)(resources.GetObject("fornecedores.Image")));
            this.fornecedores.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fornecedores.Location = new System.Drawing.Point(22, 186);
            this.fornecedores.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fornecedores.Name = "fornecedores";
            this.fornecedores.Size = new System.Drawing.Size(219, 41);
            this.fornecedores.TabIndex = 109;
            this.fornecedores.Text = "          Fornecedores";
            this.fornecedores.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fornecedores.UseVisualStyleBackColor = false;
            // 
            // Categorias
            // 
            this.Categorias.BackColor = System.Drawing.Color.Transparent;
            this.Categorias.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Categorias.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Categorias.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Categorias.FlatAppearance.BorderSize = 0;
            this.Categorias.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Categorias.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.Categorias.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Categorias.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Categorias.ForeColor = System.Drawing.Color.DimGray;
            this.Categorias.Image = ((System.Drawing.Image)(resources.GetObject("Categorias.Image")));
            this.Categorias.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Categorias.Location = new System.Drawing.Point(22, 94);
            this.Categorias.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Categorias.Name = "Categorias";
            this.Categorias.Size = new System.Drawing.Size(219, 41);
            this.Categorias.TabIndex = 110;
            this.Categorias.Text = "          Categorias";
            this.Categorias.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Categorias.UseVisualStyleBackColor = false;
            // 
            // FecharCaixa
            // 
            this.FecharCaixa.BackColor = System.Drawing.Color.Transparent;
            this.FecharCaixa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.FecharCaixa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FecharCaixa.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.FecharCaixa.FlatAppearance.BorderSize = 0;
            this.FecharCaixa.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.FecharCaixa.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.FecharCaixa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FecharCaixa.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FecharCaixa.ForeColor = System.Drawing.Color.DimGray;
            this.FecharCaixa.Image = ((System.Drawing.Image)(resources.GetObject("FecharCaixa.Image")));
            this.FecharCaixa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.FecharCaixa.Location = new System.Drawing.Point(256, 278);
            this.FecharCaixa.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.FecharCaixa.Name = "FecharCaixa";
            this.FecharCaixa.Size = new System.Drawing.Size(219, 41);
            this.FecharCaixa.TabIndex = 111;
            this.FecharCaixa.Text = "          Fechar Caixa";
            this.FecharCaixa.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.FecharCaixa.UseVisualStyleBackColor = false;
            // 
            // EntradaSaidaCaixa
            // 
            this.EntradaSaidaCaixa.BackColor = System.Drawing.Color.Transparent;
            this.EntradaSaidaCaixa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.EntradaSaidaCaixa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.EntradaSaidaCaixa.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.EntradaSaidaCaixa.FlatAppearance.BorderSize = 0;
            this.EntradaSaidaCaixa.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.EntradaSaidaCaixa.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.EntradaSaidaCaixa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EntradaSaidaCaixa.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EntradaSaidaCaixa.ForeColor = System.Drawing.Color.DimGray;
            this.EntradaSaidaCaixa.Image = ((System.Drawing.Image)(resources.GetObject("EntradaSaidaCaixa.Image")));
            this.EntradaSaidaCaixa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.EntradaSaidaCaixa.Location = new System.Drawing.Point(256, 232);
            this.EntradaSaidaCaixa.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.EntradaSaidaCaixa.Name = "EntradaSaidaCaixa";
            this.EntradaSaidaCaixa.Size = new System.Drawing.Size(219, 41);
            this.EntradaSaidaCaixa.TabIndex = 112;
            this.EntradaSaidaCaixa.Text = "          Entrada/Saída Caixa";
            this.EntradaSaidaCaixa.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.EntradaSaidaCaixa.UseVisualStyleBackColor = false;
            // 
            // TelaFinanceiroInicial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(733, 649);
            this.Controls.Add(this.EntradaSaidaCaixa);
            this.Controls.Add(this.FecharCaixa);
            this.Controls.Add(this.Categorias);
            this.Controls.Add(this.fornecedores);
            this.Controls.Add(this.Clientes);
            this.Controls.Add(this.Caixa);
            this.Controls.Add(this.visualSeparator5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.AbrirCaixa);
            this.Controls.Add(this.visualSeparator4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.visualSeparator6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.novoRecebimento);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.novoPagamento);
            this.Controls.Add(this.aPagar);
            this.Controls.Add(this.aReceber);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TelaFinanceiroInicial";
            this.Text = "TelaFinanceiroInicial";
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button novoPagamento;
        private System.Windows.Forms.Button aPagar;
        private System.Windows.Forms.Button aReceber;
        private System.Windows.Forms.Button novoRecebimento;
        private VisualPlus.Toolkit.Controls.Layout.VisualSeparator visualSeparator4;
        private System.Windows.Forms.Label label2;
        private VisualPlus.Toolkit.Controls.Layout.VisualSeparator visualSeparator6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button AbrirCaixa;
        private VisualPlus.Toolkit.Controls.Layout.VisualSeparator visualSeparator5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Caixa;
        private System.Windows.Forms.Button Clientes;
        private System.Windows.Forms.Button fornecedores;
        private System.Windows.Forms.Button Categorias;
        private System.Windows.Forms.Button FecharCaixa;
        private System.Windows.Forms.Button EntradaSaidaCaixa;
    }
}