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
            this.visualPanel1 = new VisualPlus.Toolkit.Controls.Layout.VisualPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.email = new VisualPlus.Toolkit.Controls.Editors.VisualTextBox();
            this.visualTextBox1 = new VisualPlus.Toolkit.Controls.Editors.VisualTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAlterarQtd = new VisualPlus.Toolkit.Controls.Interactivity.VisualButton();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.BarraTitulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnFechar)).BeginInit();
            this.panel2.SuspendLayout();
            this.visualPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            this.btnFechar.Click += new System.EventHandler(this.BtnFechar_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(93)))), ((int)(((byte)(89)))));
            this.panel2.Controls.Add(this.visualPanel1);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 35);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(370, 486);
            this.panel2.TabIndex = 1;
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
            this.label1.Location = new System.Drawing.Point(427, 153);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(281, 42);
            this.label1.TabIndex = 0;
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
            this.email.Location = new System.Drawing.Point(397, 240);
            this.email.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.email.Name = "email";
            this.email.PasswordChar = '\0';
            this.email.ReadOnly = false;
            this.email.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.email.Size = new System.Drawing.Size(301, 34);
            this.email.TabIndex = 55;
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
            // visualTextBox1
            // 
            this.visualTextBox1.AlphaNumeric = false;
            this.visualTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.visualTextBox1.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.visualTextBox1.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(48)))), ((int)(((byte)(67)))));
            this.visualTextBox1.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(80)))));
            this.visualTextBox1.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(93)))), ((int)(((byte)(89)))));
            this.visualTextBox1.Border.HoverVisible = true;
            this.visualTextBox1.Border.Rounding = 8;
            this.visualTextBox1.Border.Thickness = 1;
            this.visualTextBox1.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rectangle;
            this.visualTextBox1.Border.Visible = true;
            this.visualTextBox1.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(80)))));
            this.visualTextBox1.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(80)))));
            this.visualTextBox1.ButtonBorder.HoverVisible = true;
            this.visualTextBox1.ButtonBorder.Rounding = 6;
            this.visualTextBox1.ButtonBorder.Thickness = 1;
            this.visualTextBox1.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.visualTextBox1.ButtonBorder.Visible = false;
            this.visualTextBox1.ButtonColor.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.visualTextBox1.ButtonColor.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(80)))));
            this.visualTextBox1.ButtonColor.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(80)))));
            this.visualTextBox1.ButtonColor.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(80)))));
            this.visualTextBox1.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.visualTextBox1.ButtonIndent = 3;
            this.visualTextBox1.ButtonText = "visualButton";
            this.visualTextBox1.ButtonVisible = false;
            this.visualTextBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.visualTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(171)))), ((int)(((byte)(176)))));
            this.visualTextBox1.Image = null;
            this.visualTextBox1.ImageSize = new System.Drawing.Size(16, 16);
            this.visualTextBox1.ImageVisible = false;
            this.visualTextBox1.ImageWidth = 35;
            this.visualTextBox1.Location = new System.Drawing.Point(397, 308);
            this.visualTextBox1.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.visualTextBox1.Name = "visualTextBox1";
            this.visualTextBox1.PasswordChar = '\0';
            this.visualTextBox1.ReadOnly = false;
            this.visualTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.visualTextBox1.Size = new System.Drawing.Size(301, 34);
            this.visualTextBox1.TabIndex = 56;
            this.visualTextBox1.TextBoxWidth = 289;
            this.visualTextBox1.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.visualTextBox1.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualTextBox1.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualTextBox1.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualTextBox1.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.visualTextBox1.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.visualTextBox1.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.visualTextBox1.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.visualTextBox1.Watermark.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.visualTextBox1.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.visualTextBox1.Watermark.Text = "Seu E-mail";
            this.visualTextBox1.Watermark.Visible = false;
            this.visualTextBox1.WordWrap = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(171)))), ((int)(((byte)(176)))));
            this.label5.Location = new System.Drawing.Point(397, 219);
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
            this.label6.Location = new System.Drawing.Point(396, 288);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 17);
            this.label6.TabIndex = 58;
            this.label6.Text = "Senha";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(171)))), ((int)(((byte)(176)))));
            this.label2.Location = new System.Drawing.Point(590, 373);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 15);
            this.label2.TabIndex = 156;
            this.label2.Text = "Perdeu sua Senha?";
            // 
            // btnAlterarQtd
            // 
            this.btnAlterarQtd.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnAlterarQtd.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(93)))), ((int)(((byte)(89)))));
            this.btnAlterarQtd.BackColorState.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(102)))), ((int)(((byte)(98)))));
            this.btnAlterarQtd.BackColorState.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(102)))), ((int)(((byte)(98)))));
            this.btnAlterarQtd.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(93)))), ((int)(((byte)(89)))));
            this.btnAlterarQtd.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(93)))), ((int)(((byte)(89)))));
            this.btnAlterarQtd.Border.HoverVisible = true;
            this.btnAlterarQtd.Border.Rounding = 6;
            this.btnAlterarQtd.Border.Thickness = 1;
            this.btnAlterarQtd.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.btnAlterarQtd.Border.Visible = true;
            this.btnAlterarQtd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAlterarQtd.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnAlterarQtd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlterarQtd.ForeColor = System.Drawing.Color.White;
            this.btnAlterarQtd.Image = null;
            this.btnAlterarQtd.Location = new System.Drawing.Point(397, 362);
            this.btnAlterarQtd.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.btnAlterarQtd.Name = "btnAlterarQtd";
            this.btnAlterarQtd.Size = new System.Drawing.Size(151, 36);
            this.btnAlterarQtd.TabIndex = 157;
            this.btnAlterarQtd.Text = "Entrar!";
            this.btnAlterarQtd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAlterarQtd.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.btnAlterarQtd.TextStyle.Enabled = System.Drawing.Color.White;
            this.btnAlterarQtd.TextStyle.Hover = System.Drawing.Color.White;
            this.btnAlterarQtd.TextStyle.Pressed = System.Drawing.Color.White;
            this.btnAlterarQtd.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btnAlterarQtd.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.btnAlterarQtd.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
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
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(171)))), ((int)(((byte)(176)))));
            this.label9.Location = new System.Drawing.Point(433, 476);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(140, 15);
            this.label9.TabIndex = 160;
            this.label9.Text = "nosso site, clicando aqui!";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(61)))));
            this.ClientSize = new System.Drawing.Size(722, 521);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnAlterarQtd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.visualTextBox1);
            this.Controls.Add(this.email);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.BarraTitulo);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(722, 521);
            this.MinimumSize = new System.Drawing.Size(722, 521);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.BarraTitulo.ResumeLayout(false);
            this.BarraTitulo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnFechar)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.visualPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
        private VisualPlus.Toolkit.Controls.Editors.VisualTextBox visualTextBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualButton btnAlterarQtd;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
    }
}