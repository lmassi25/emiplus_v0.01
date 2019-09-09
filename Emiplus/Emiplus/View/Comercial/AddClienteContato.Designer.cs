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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddClienteContato));
            this.label11 = new System.Windows.Forms.Label();
            this.btnContatoSalvar = new System.Windows.Forms.Button();
            this.btnContatoCancelar = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label23 = new System.Windows.Forms.Label();
            this.contato = new VisualPlus.Toolkit.Controls.Editors.VisualTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.telefone = new VisualPlus.Toolkit.Controls.Editors.VisualTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.celular = new VisualPlus.Toolkit.Controls.Editors.VisualTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.email = new VisualPlus.Toolkit.Controls.Editors.VisualTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI Light", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(10, 20);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(126, 25);
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
            this.btnContatoSalvar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.btnContatoCancelar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.White;
            this.label23.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label23.Location = new System.Drawing.Point(14, 78);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(57, 17);
            this.label23.TabIndex = 32;
            this.label23.Text = "Contato";
            // 
            // contato
            // 
            this.contato.AlphaNumeric = false;
            this.contato.BackColor = System.Drawing.Color.White;
            this.contato.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.contato.BackColorState.Enabled = System.Drawing.Color.White;
            this.contato.Border.Color = System.Drawing.Color.Gainsboro;
            this.contato.Border.HoverColor = System.Drawing.Color.Gainsboro;
            this.contato.Border.HoverVisible = true;
            this.contato.Border.Rounding = 8;
            this.contato.Border.Thickness = 1;
            this.contato.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.contato.Border.Visible = true;
            this.contato.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.contato.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.contato.ButtonBorder.HoverVisible = true;
            this.contato.ButtonBorder.Rounding = 6;
            this.contato.ButtonBorder.Thickness = 1;
            this.contato.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.contato.ButtonBorder.Visible = true;
            this.contato.ButtonColor.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.contato.ButtonColor.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.contato.ButtonColor.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.contato.ButtonColor.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.contato.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contato.ButtonIndent = 3;
            this.contato.ButtonText = "visualButton";
            this.contato.ButtonVisible = false;
            this.contato.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contato.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.contato.Image = null;
            this.contato.ImageSize = new System.Drawing.Size(16, 16);
            this.contato.ImageVisible = false;
            this.contato.ImageWidth = 35;
            this.contato.Location = new System.Drawing.Point(15, 97);
            this.contato.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.contato.Name = "contato";
            this.contato.PasswordChar = '\0';
            this.contato.ReadOnly = false;
            this.contato.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.contato.Size = new System.Drawing.Size(236, 28);
            this.contato.TabIndex = 31;
            this.contato.TextBoxWidth = 224;
            this.contato.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.contato.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.contato.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.contato.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.contato.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.contato.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.contato.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.contato.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.contato.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contato.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.contato.Watermark.Text = "Watermark text";
            this.contato.Watermark.Visible = false;
            this.contato.WordWrap = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(256, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 17);
            this.label5.TabIndex = 34;
            this.label5.Text = "Telefone";
            // 
            // telefone
            // 
            this.telefone.AlphaNumeric = false;
            this.telefone.BackColor = System.Drawing.Color.White;
            this.telefone.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.telefone.BackColorState.Enabled = System.Drawing.Color.White;
            this.telefone.Border.Color = System.Drawing.Color.Gainsboro;
            this.telefone.Border.HoverColor = System.Drawing.Color.Gainsboro;
            this.telefone.Border.HoverVisible = true;
            this.telefone.Border.Rounding = 8;
            this.telefone.Border.Thickness = 1;
            this.telefone.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.telefone.Border.Visible = true;
            this.telefone.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.telefone.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.telefone.ButtonBorder.HoverVisible = true;
            this.telefone.ButtonBorder.Rounding = 6;
            this.telefone.ButtonBorder.Thickness = 1;
            this.telefone.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.telefone.ButtonBorder.Visible = true;
            this.telefone.ButtonColor.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.telefone.ButtonColor.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.telefone.ButtonColor.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.telefone.ButtonColor.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.telefone.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.telefone.ButtonIndent = 3;
            this.telefone.ButtonText = "visualButton";
            this.telefone.ButtonVisible = false;
            this.telefone.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.telefone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.telefone.Image = null;
            this.telefone.ImageSize = new System.Drawing.Size(16, 16);
            this.telefone.ImageVisible = false;
            this.telefone.ImageWidth = 35;
            this.telefone.Location = new System.Drawing.Point(257, 97);
            this.telefone.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.telefone.Name = "telefone";
            this.telefone.PasswordChar = '\0';
            this.telefone.ReadOnly = false;
            this.telefone.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.telefone.Size = new System.Drawing.Size(197, 28);
            this.telefone.TabIndex = 33;
            this.telefone.TextBoxWidth = 185;
            this.telefone.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.telefone.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.telefone.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.telefone.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.telefone.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.telefone.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.telefone.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.telefone.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.telefone.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.telefone.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.telefone.Watermark.Text = "Watermark text";
            this.telefone.Watermark.Visible = false;
            this.telefone.WordWrap = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label6.Location = new System.Drawing.Point(459, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 17);
            this.label6.TabIndex = 36;
            this.label6.Text = "Celular";
            // 
            // celular
            // 
            this.celular.AlphaNumeric = false;
            this.celular.BackColor = System.Drawing.Color.White;
            this.celular.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.celular.BackColorState.Enabled = System.Drawing.Color.White;
            this.celular.Border.Color = System.Drawing.Color.Gainsboro;
            this.celular.Border.HoverColor = System.Drawing.Color.Gainsboro;
            this.celular.Border.HoverVisible = true;
            this.celular.Border.Rounding = 8;
            this.celular.Border.Thickness = 1;
            this.celular.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.celular.Border.Visible = true;
            this.celular.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.celular.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.celular.ButtonBorder.HoverVisible = true;
            this.celular.ButtonBorder.Rounding = 6;
            this.celular.ButtonBorder.Thickness = 1;
            this.celular.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.celular.ButtonBorder.Visible = true;
            this.celular.ButtonColor.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.celular.ButtonColor.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.celular.ButtonColor.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.celular.ButtonColor.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.celular.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.celular.ButtonIndent = 3;
            this.celular.ButtonText = "visualButton";
            this.celular.ButtonVisible = false;
            this.celular.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.celular.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.celular.Image = null;
            this.celular.ImageSize = new System.Drawing.Size(16, 16);
            this.celular.ImageVisible = false;
            this.celular.ImageWidth = 35;
            this.celular.Location = new System.Drawing.Point(460, 97);
            this.celular.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.celular.Name = "celular";
            this.celular.PasswordChar = '\0';
            this.celular.ReadOnly = false;
            this.celular.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.celular.Size = new System.Drawing.Size(191, 28);
            this.celular.TabIndex = 35;
            this.celular.TextBoxWidth = 179;
            this.celular.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.celular.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.celular.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.celular.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.celular.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.celular.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.celular.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.celular.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.celular.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.celular.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.celular.Watermark.Text = "Watermark text";
            this.celular.Watermark.Visible = false;
            this.celular.WordWrap = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label7.Location = new System.Drawing.Point(16, 133);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 17);
            this.label7.TabIndex = 38;
            this.label7.Text = "E-mail";
            // 
            // email
            // 
            this.email.AlphaNumeric = false;
            this.email.BackColor = System.Drawing.Color.White;
            this.email.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.email.BackColorState.Enabled = System.Drawing.Color.White;
            this.email.Border.Color = System.Drawing.Color.Gainsboro;
            this.email.Border.HoverColor = System.Drawing.Color.Gainsboro;
            this.email.Border.HoverVisible = true;
            this.email.Border.Rounding = 8;
            this.email.Border.Thickness = 1;
            this.email.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.email.Border.Visible = true;
            this.email.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.email.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.email.ButtonBorder.HoverVisible = true;
            this.email.ButtonBorder.Rounding = 6;
            this.email.ButtonBorder.Thickness = 1;
            this.email.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.email.ButtonBorder.Visible = true;
            this.email.ButtonColor.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.email.ButtonColor.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.email.ButtonColor.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.email.ButtonColor.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.email.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.email.ButtonIndent = 3;
            this.email.ButtonText = "visualButton";
            this.email.ButtonVisible = false;
            this.email.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.email.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.email.Image = null;
            this.email.ImageSize = new System.Drawing.Size(16, 16);
            this.email.ImageVisible = false;
            this.email.ImageWidth = 35;
            this.email.Location = new System.Drawing.Point(17, 152);
            this.email.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.email.Name = "email";
            this.email.PasswordChar = '\0';
            this.email.ReadOnly = false;
            this.email.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.email.Size = new System.Drawing.Size(234, 28);
            this.email.TabIndex = 37;
            this.email.TextBoxWidth = 222;
            this.email.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.email.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.email.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.email.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.email.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.email.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.email.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.email.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.email.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.email.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.email.Watermark.Text = "Watermark text";
            this.email.Watermark.Visible = false;
            this.email.WordWrap = true;
            // 
            // AddClienteContato
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(670, 200);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.email);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.celular);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.telefone);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.contato);
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
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnContatoSalvar;
        private System.Windows.Forms.Button btnContatoCancelar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label23;
        private VisualPlus.Toolkit.Controls.Editors.VisualTextBox contato;
        private System.Windows.Forms.Label label5;
        private VisualPlus.Toolkit.Controls.Editors.VisualTextBox telefone;
        private System.Windows.Forms.Label label6;
        private VisualPlus.Toolkit.Controls.Editors.VisualTextBox celular;
        private System.Windows.Forms.Label label7;
        private VisualPlus.Toolkit.Controls.Editors.VisualTextBox email;
    }
}