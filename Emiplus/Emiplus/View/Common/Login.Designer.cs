namespace Emiplus.View.Common
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.BarraTitulo = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.btnFechar = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.version = new System.Windows.Forms.Label();
            this.visualPanel1 = new VisualPlus.Toolkit.Controls.Layout.VisualPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.email = new VisualPlus.Toolkit.Controls.Editors.VisualTextBox();
            this.password = new VisualPlus.Toolkit.Controls.Editors.VisualTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.linkRecover = new System.Windows.Forms.Label();
            this.btnEntrar = new VisualPlus.Toolkit.Controls.Interactivity.VisualButton();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.linkSupport = new System.Windows.Forms.Label();
            this.btnUpdate = new VisualPlus.Toolkit.Controls.Interactivity.VisualButton();
            this.remember = new VisualPlus.Toolkit.Controls.Interactivity.VisualCheckBox();
            this.panelEmpresa = new System.Windows.Forms.Panel();
            this.i = new System.Windows.Forms.Label();
            this.idEmpresa = new VisualPlus.Toolkit.Controls.Editors.VisualTextBox();
            this.btnConfirmar = new VisualPlus.Toolkit.Controls.Interactivity.VisualButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.BarraTitulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnFechar)).BeginInit();
            this.panel2.SuspendLayout();
            this.visualPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelEmpresa.SuspendLayout();
            this.SuspendLayout();
            // 
            // BarraTitulo
            // 
            this.BarraTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.BarraTitulo.Controls.Add(this.label3);
            this.BarraTitulo.Controls.Add(this.btnFechar);
            this.BarraTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.BarraTitulo.Location = new System.Drawing.Point(0, 0);
            this.BarraTitulo.Name = "BarraTitulo";
            this.BarraTitulo.Size = new System.Drawing.Size(722, 35);
            this.BarraTitulo.TabIndex = 0;
            this.BarraTitulo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BarraTitulo_MouseDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label3.Location = new System.Drawing.Point(5, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Emiplus - Login";
            // 
            // btnFechar
            // 
            this.btnFechar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFechar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnFechar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFechar.Image = ((System.Drawing.Image)(resources.GetObject("btnFechar.Image")));
            this.btnFechar.Location = new System.Drawing.Point(694, 10);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(15, 15);
            this.btnFechar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnFechar.TabIndex = 2;
            this.btnFechar.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(93)))), ((int)(((byte)(89)))));
            this.panel2.Controls.Add(this.version);
            this.panel2.Controls.Add(this.visualPanel1);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 35);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(370, 486);
            this.panel2.TabIndex = 1;
            // 
            // version
            // 
            this.version.BackColor = System.Drawing.Color.Transparent;
            this.version.Cursor = System.Windows.Forms.Cursors.Default;
            this.version.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.version.ForeColor = System.Drawing.Color.White;
            this.version.Location = new System.Drawing.Point(6, 463);
            this.version.Name = "version";
            this.version.Size = new System.Drawing.Size(359, 15);
            this.version.TabIndex = 161;
            this.version.Text = "Versão 1.0.0";
            this.version.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // visualPanel1
            // 
            this.visualPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(102)))), ((int)(((byte)(98)))));
            this.visualPanel1.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.visualPanel1.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(102)))), ((int)(((byte)(98)))));
            this.visualPanel1.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(143)))), ((int)(((byte)(132)))));
            this.visualPanel1.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(143)))), ((int)(((byte)(132)))));
            this.visualPanel1.Border.HoverVisible = true;
            this.visualPanel1.Border.Rounding = 6;
            this.visualPanel1.Border.Thickness = 1;
            this.visualPanel1.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rectangle;
            this.visualPanel1.Border.Visible = true;
            this.visualPanel1.Controls.Add(this.pictureBox1);
            this.visualPanel1.ForeColor = System.Drawing.Color.White;
            this.visualPanel1.Location = new System.Drawing.Point(35, 0);
            this.visualPanel1.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.visualPanel1.Name = "visualPanel1";
            this.visualPanel1.Padding = new System.Windows.Forms.Padding(5);
            this.visualPanel1.Size = new System.Drawing.Size(335, 129);
            this.visualPanel1.TabIndex = 60;
            this.visualPanel1.Text = "visualPanel1";
            this.visualPanel1.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.visualPanel1.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualPanel1.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualPanel1.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualPanel1.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.visualPanel1.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.visualPanel1.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(102)))), ((int)(((byte)(98)))));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(93, 34);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(127, 68);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 59;
            this.pictureBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI Light", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(33, 217);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(316, 74);
            this.label4.TabIndex = 58;
            this.label4.Text = "Uma plataforma completa \r\npara seu negócio!";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Light", 23.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(171)))), ((int)(((byte)(176)))));
            this.label1.Location = new System.Drawing.Point(427, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(281, 42);
            this.label1.TabIndex = 100;
            this.label1.Text = "Entre com sua conta";
            // 
            // email
            // 
            this.email.AlphaNumeric = false;
            this.email.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.email.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.email.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(48)))), ((int)(((byte)(67)))));
            this.email.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(80)))));
            this.email.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(93)))), ((int)(((byte)(89)))));
            this.email.Border.HoverVisible = true;
            this.email.Border.Rounding = 8;
            this.email.Border.Thickness = 1;
            this.email.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rectangle;
            this.email.Border.Visible = true;
            this.email.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(80)))));
            this.email.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(80)))));
            this.email.ButtonBorder.HoverVisible = true;
            this.email.ButtonBorder.Rounding = 6;
            this.email.ButtonBorder.Thickness = 1;
            this.email.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.email.ButtonBorder.Visible = false;
            this.email.ButtonColor.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.email.ButtonColor.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(80)))));
            this.email.ButtonColor.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(80)))));
            this.email.ButtonColor.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(80)))));
            this.email.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.email.ButtonIndent = 3;
            this.email.ButtonText = "visualButton";
            this.email.ButtonVisible = false;
            this.email.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.email.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(171)))), ((int)(((byte)(176)))));
            this.email.Image = null;
            this.email.ImageSize = new System.Drawing.Size(16, 16);
            this.email.ImageVisible = false;
            this.email.ImageWidth = 35;
            this.email.Location = new System.Drawing.Point(397, 218);
            this.email.MaxLength = 200;
            this.email.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.email.Name = "email";
            this.email.PasswordChar = '\0';
            this.email.ReadOnly = false;
            this.email.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.email.Size = new System.Drawing.Size(301, 34);
            this.email.TabIndex = 0;
            this.email.TextBoxWidth = 289;
            this.email.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.email.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.email.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.email.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.email.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.email.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.email.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.email.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.email.Watermark.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.email.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.email.Watermark.Text = "";
            this.email.Watermark.Visible = false;
            this.email.WordWrap = true;
            // 
            // password
            // 
            this.password.AlphaNumeric = false;
            this.password.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.password.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.password.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(48)))), ((int)(((byte)(67)))));
            this.password.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(80)))));
            this.password.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(93)))), ((int)(((byte)(89)))));
            this.password.Border.HoverVisible = true;
            this.password.Border.Rounding = 8;
            this.password.Border.Thickness = 1;
            this.password.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rectangle;
            this.password.Border.Visible = true;
            this.password.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(80)))));
            this.password.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(80)))));
            this.password.ButtonBorder.HoverVisible = true;
            this.password.ButtonBorder.Rounding = 6;
            this.password.ButtonBorder.Thickness = 1;
            this.password.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.password.ButtonBorder.Visible = false;
            this.password.ButtonColor.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.password.ButtonColor.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(80)))));
            this.password.ButtonColor.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(80)))));
            this.password.ButtonColor.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(80)))));
            this.password.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.password.ButtonIndent = 3;
            this.password.ButtonText = "visualButton";
            this.password.ButtonVisible = false;
            this.password.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.password.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(171)))), ((int)(((byte)(176)))));
            this.password.Image = null;
            this.password.ImageSize = new System.Drawing.Size(16, 16);
            this.password.ImageVisible = false;
            this.password.ImageWidth = 35;
            this.password.Location = new System.Drawing.Point(397, 286);
            this.password.MaxLength = 200;
            this.password.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.password.Name = "password";
            this.password.PasswordChar = '●';
            this.password.ReadOnly = false;
            this.password.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.password.Size = new System.Drawing.Size(301, 34);
            this.password.TabIndex = 1;
            this.password.TextBoxWidth = 289;
            this.password.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.password.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.password.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.password.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.password.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.password.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.password.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.password.UseSystemPasswordChar = true;
            this.password.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.password.Watermark.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.password.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.password.Watermark.Text = "Seu E-mail";
            this.password.Watermark.Visible = false;
            this.password.WordWrap = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(171)))), ((int)(((byte)(176)))));
            this.label5.Location = new System.Drawing.Point(397, 197);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 17);
            this.label5.TabIndex = 57;
            this.label5.Text = "E-mail";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(171)))), ((int)(((byte)(176)))));
            this.label6.Location = new System.Drawing.Point(396, 266);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 17);
            this.label6.TabIndex = 58;
            this.label6.Text = "Senha";
            // 
            // linkRecover
            // 
            this.linkRecover.AutoSize = true;
            this.linkRecover.BackColor = System.Drawing.Color.Transparent;
            this.linkRecover.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkRecover.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkRecover.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(171)))), ((int)(((byte)(176)))));
            this.linkRecover.Location = new System.Drawing.Point(590, 375);
            this.linkRecover.Name = "linkRecover";
            this.linkRecover.Size = new System.Drawing.Size(105, 15);
            this.linkRecover.TabIndex = 3;
            this.linkRecover.Text = "Perdeu sua Senha?";
            // 
            // btnEntrar
            // 
            this.btnEntrar.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnEntrar.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(93)))), ((int)(((byte)(89)))));
            this.btnEntrar.BackColorState.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(102)))), ((int)(((byte)(98)))));
            this.btnEntrar.BackColorState.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(102)))), ((int)(((byte)(98)))));
            this.btnEntrar.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(93)))), ((int)(((byte)(89)))));
            this.btnEntrar.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(93)))), ((int)(((byte)(89)))));
            this.btnEntrar.Border.HoverVisible = true;
            this.btnEntrar.Border.Rounding = 6;
            this.btnEntrar.Border.Thickness = 1;
            this.btnEntrar.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.btnEntrar.Border.Visible = true;
            this.btnEntrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEntrar.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnEntrar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEntrar.ForeColor = System.Drawing.Color.White;
            this.btnEntrar.Image = null;
            this.btnEntrar.Location = new System.Drawing.Point(397, 364);
            this.btnEntrar.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.btnEntrar.Name = "btnEntrar";
            this.btnEntrar.Size = new System.Drawing.Size(151, 36);
            this.btnEntrar.TabIndex = 2;
            this.btnEntrar.Text = "Entrar!";
            this.btnEntrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEntrar.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.btnEntrar.TextStyle.Enabled = System.Drawing.Color.White;
            this.btnEntrar.TextStyle.Hover = System.Drawing.Color.White;
            this.btnEntrar.TextStyle.Pressed = System.Drawing.Color.White;
            this.btnEntrar.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btnEntrar.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.btnEntrar.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(171)))), ((int)(((byte)(176)))));
            this.label7.Location = new System.Drawing.Point(396, 440);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 15);
            this.label7.TabIndex = 158;
            this.label7.Text = "Suporte";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semilight", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(171)))), ((int)(((byte)(176)))));
            this.label8.Location = new System.Drawing.Point(397, 461);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(299, 30);
            this.label8.TabIndex = 159;
            this.label8.Text = "Para obter acesso a esse aplicativo ou precisar de ajuda, \r\nacesse ";
            // 
            // linkSupport
            // 
            this.linkSupport.AutoSize = true;
            this.linkSupport.BackColor = System.Drawing.Color.Transparent;
            this.linkSupport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkSupport.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkSupport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(171)))), ((int)(((byte)(176)))));
            this.linkSupport.Location = new System.Drawing.Point(433, 476);
            this.linkSupport.Name = "linkSupport";
            this.linkSupport.Size = new System.Drawing.Size(140, 15);
            this.linkSupport.TabIndex = 160;
            this.linkSupport.Text = "nosso site, clicando aqui!";
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnUpdate.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(148)))), ((int)(((byte)(6)))));
            this.btnUpdate.BackColorState.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(106)))), ((int)(((byte)(2)))));
            this.btnUpdate.BackColorState.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(106)))), ((int)(((byte)(2)))));
            this.btnUpdate.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(148)))), ((int)(((byte)(6)))));
            this.btnUpdate.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(106)))), ((int)(((byte)(2)))));
            this.btnUpdate.Border.HoverVisible = true;
            this.btnUpdate.Border.Rounding = 6;
            this.btnUpdate.Border.Thickness = 1;
            this.btnUpdate.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.btnUpdate.Border.Visible = true;
            this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdate.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Image = null;
            this.btnUpdate.Location = new System.Drawing.Point(417, 245);
            this.btnUpdate.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(259, 50);
            this.btnUpdate.TabIndex = 161;
            this.btnUpdate.Text = "Atualizar!";
            this.btnUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUpdate.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.btnUpdate.TextStyle.Enabled = System.Drawing.Color.White;
            this.btnUpdate.TextStyle.Hover = System.Drawing.Color.White;
            this.btnUpdate.TextStyle.Pressed = System.Drawing.Color.White;
            this.btnUpdate.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btnUpdate.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.btnUpdate.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.btnUpdate.Visible = false;
            // 
            // remember
            // 
            this.remember.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.remember.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.remember.Border.HoverVisible = true;
            this.remember.Border.Rounding = 3;
            this.remember.Border.Thickness = 1;
            this.remember.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.remember.Border.Visible = true;
            this.remember.Box = new System.Drawing.Size(15, 15);
            this.remember.BoxColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.remember.BoxColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.remember.BoxColorState.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.remember.BoxColorState.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.remember.BoxSpacing = 1;
            this.remember.CheckStyle.AutoSize = true;
            this.remember.CheckStyle.Bounds = new System.Drawing.Rectangle(0, 0, 125, 23);
            this.remember.CheckStyle.Character = '✔';
            this.remember.CheckStyle.CheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.remember.CheckStyle.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remember.CheckStyle.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            this.remember.CheckStyle.ShapeRounding = 2;
            this.remember.CheckStyle.ShapeType = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.remember.CheckStyle.Style = VisualPlus.Structure.CheckStyle.CheckType.Checkmark;
            this.remember.CheckStyle.Thickness = 1F;
            this.remember.Cursor = System.Windows.Forms.Cursors.Hand;
            this.remember.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remember.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(171)))), ((int)(((byte)(176)))));
            this.remember.IsBoxLarger = false;
            this.remember.Location = new System.Drawing.Point(397, 331);
            this.remember.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.remember.Name = "remember";
            this.remember.Size = new System.Drawing.Size(125, 23);
            this.remember.TabIndex = 162;
            this.remember.Text = " Lembrar!";
            this.remember.TextSize = new System.Drawing.Size(51, 16);
            this.remember.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.remember.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.remember.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.remember.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.remember.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.remember.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.remember.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // panelEmpresa
            // 
            this.panelEmpresa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(61)))));
            this.panelEmpresa.Controls.Add(this.label9);
            this.panelEmpresa.Controls.Add(this.label2);
            this.panelEmpresa.Controls.Add(this.btnConfirmar);
            this.panelEmpresa.Controls.Add(this.i);
            this.panelEmpresa.Controls.Add(this.idEmpresa);
            this.panelEmpresa.Location = new System.Drawing.Point(376, 41);
            this.panelEmpresa.Name = "panelEmpresa";
            this.panelEmpresa.Size = new System.Drawing.Size(353, 186);
            this.panelEmpresa.TabIndex = 162;
            this.panelEmpresa.Visible = false;
            // 
            // i
            // 
            this.i.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.i.AutoSize = true;
            this.i.BackColor = System.Drawing.Color.Transparent;
            this.i.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.i.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(171)))), ((int)(((byte)(176)))));
            this.i.Location = new System.Drawing.Point(106, 2);
            this.i.Name = "i";
            this.i.Size = new System.Drawing.Size(139, 25);
            this.i.TabIndex = 164;
            this.i.Text = "ID da empresa";
            // 
            // idEmpresa
            // 
            this.idEmpresa.AlphaNumeric = false;
            this.idEmpresa.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.idEmpresa.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.idEmpresa.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(48)))), ((int)(((byte)(67)))));
            this.idEmpresa.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(80)))));
            this.idEmpresa.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(93)))), ((int)(((byte)(89)))));
            this.idEmpresa.Border.HoverVisible = true;
            this.idEmpresa.Border.Rounding = 8;
            this.idEmpresa.Border.Thickness = 1;
            this.idEmpresa.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rectangle;
            this.idEmpresa.Border.Visible = true;
            this.idEmpresa.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(80)))));
            this.idEmpresa.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(80)))));
            this.idEmpresa.ButtonBorder.HoverVisible = true;
            this.idEmpresa.ButtonBorder.Rounding = 6;
            this.idEmpresa.ButtonBorder.Thickness = 1;
            this.idEmpresa.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.idEmpresa.ButtonBorder.Visible = false;
            this.idEmpresa.ButtonColor.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.idEmpresa.ButtonColor.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(80)))));
            this.idEmpresa.ButtonColor.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(80)))));
            this.idEmpresa.ButtonColor.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(80)))));
            this.idEmpresa.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.idEmpresa.ButtonIndent = 3;
            this.idEmpresa.ButtonText = "visualButton";
            this.idEmpresa.ButtonVisible = false;
            this.idEmpresa.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.idEmpresa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(171)))), ((int)(((byte)(176)))));
            this.idEmpresa.Image = null;
            this.idEmpresa.ImageSize = new System.Drawing.Size(16, 16);
            this.idEmpresa.ImageVisible = false;
            this.idEmpresa.ImageWidth = 35;
            this.idEmpresa.Location = new System.Drawing.Point(23, 68);
            this.idEmpresa.MaxLength = 200;
            this.idEmpresa.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.idEmpresa.Name = "idEmpresa";
            this.idEmpresa.PasswordChar = '\0';
            this.idEmpresa.ReadOnly = false;
            this.idEmpresa.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.idEmpresa.Size = new System.Drawing.Size(314, 34);
            this.idEmpresa.TabIndex = 163;
            this.idEmpresa.TextBoxWidth = 302;
            this.idEmpresa.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.idEmpresa.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.idEmpresa.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.idEmpresa.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.idEmpresa.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.idEmpresa.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.idEmpresa.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.idEmpresa.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.idEmpresa.Watermark.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.idEmpresa.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.idEmpresa.Watermark.Text = "Seu E-mail";
            this.idEmpresa.Watermark.Visible = false;
            this.idEmpresa.WordWrap = true;
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnConfirmar.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnConfirmar.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(93)))), ((int)(((byte)(89)))));
            this.btnConfirmar.BackColorState.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(102)))), ((int)(((byte)(98)))));
            this.btnConfirmar.BackColorState.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(102)))), ((int)(((byte)(98)))));
            this.btnConfirmar.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(93)))), ((int)(((byte)(89)))));
            this.btnConfirmar.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(93)))), ((int)(((byte)(89)))));
            this.btnConfirmar.Border.HoverVisible = true;
            this.btnConfirmar.Border.Rounding = 6;
            this.btnConfirmar.Border.Thickness = 1;
            this.btnConfirmar.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.btnConfirmar.Border.Visible = true;
            this.btnConfirmar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmar.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnConfirmar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmar.ForeColor = System.Drawing.Color.White;
            this.btnConfirmar.Image = null;
            this.btnConfirmar.Location = new System.Drawing.Point(96, 164);
            this.btnConfirmar.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(151, 36);
            this.btnConfirmar.TabIndex = 163;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnConfirmar.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.btnConfirmar.TextStyle.Enabled = System.Drawing.Color.White;
            this.btnConfirmar.TextStyle.Hover = System.Drawing.Color.White;
            this.btnConfirmar.TextStyle.Pressed = System.Drawing.Color.White;
            this.btnConfirmar.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btnConfirmar.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.btnConfirmar.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(171)))), ((int)(((byte)(176)))));
            this.label2.Location = new System.Drawing.Point(49, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(258, 34);
            this.label2.TabIndex = 165;
            this.label2.Text = "É muito importante que você copie e cole o \r\n\'ID da empresa\' correto nesse campo!" +
    "";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(171)))), ((int)(((byte)(176)))));
            this.label9.Location = new System.Drawing.Point(34, 47);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(296, 17);
            this.label9.TabIndex = 166;
            this.label9.Text = "Exemplo: a22ae07e-0e64-4cff-8c01-f1b8690a0e75";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(61)))));
            this.ClientSize = new System.Drawing.Size(722, 521);
            this.Controls.Add(this.panelEmpresa);
            this.Controls.Add(this.remember);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.linkSupport);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnEntrar);
            this.Controls.Add(this.linkRecover);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.password);
            this.Controls.Add(this.email);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.BarraTitulo);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(722, 521);
            this.MinimumSize = new System.Drawing.Size(722, 521);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login - Emiplus";
            this.BarraTitulo.ResumeLayout(false);
            this.BarraTitulo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnFechar)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.visualPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelEmpresa.ResumeLayout(false);
            this.panelEmpresa.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel BarraTitulo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox btnFechar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private VisualPlus.Toolkit.Controls.Editors.VisualTextBox email;
        private VisualPlus.Toolkit.Controls.Layout.VisualPanel visualPanel1;
        private VisualPlus.Toolkit.Controls.Editors.VisualTextBox password;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label linkRecover;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualButton btnEntrar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label linkSupport;
        private System.Windows.Forms.Label version;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualButton btnUpdate;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualCheckBox remember;
        private System.Windows.Forms.Panel panelEmpresa;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualButton btnConfirmar;
        private System.Windows.Forms.Label i;
        private VisualPlus.Toolkit.Controls.Editors.VisualTextBox idEmpresa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
    }
}