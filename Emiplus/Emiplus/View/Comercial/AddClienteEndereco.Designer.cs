﻿namespace Emiplus.View.Comercial
{
    partial class AddClienteEndereco
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddClienteEndereco));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.btnAddrSalvar = new System.Windows.Forms.Button();
            this.btnAddrCancelar = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.rua = new VisualPlus.Toolkit.Controls.Editors.VisualTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.bairro = new VisualPlus.Toolkit.Controls.Editors.VisualTextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.nr = new VisualPlus.Toolkit.Controls.Editors.VisualTextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.complemento = new VisualPlus.Toolkit.Controls.Editors.VisualTextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.ibge = new VisualPlus.Toolkit.Controls.Editors.VisualTextBox();
            this.buscarEndereco = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cep = new VisualPlus.Toolkit.Controls.Editors.VisualTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cidade = new VisualPlus.Toolkit.Controls.Editors.VisualTextBox();
            this.estado = new VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox();
            this.pais = new VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.panel1.Controls.Add(this.label11);
            this.panel1.Location = new System.Drawing.Point(1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(669, 68);
            this.panel1.TabIndex = 4;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI Light", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(10, 20);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(141, 25);
            this.label11.TabIndex = 5;
            this.label11.Text = "Novo endereço:";
            // 
            // btnAddrSalvar
            // 
            this.btnAddrSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddrSalvar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAddrSalvar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddrSalvar.FlatAppearance.BorderSize = 0;
            this.btnAddrSalvar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAddrSalvar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAddrSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddrSalvar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddrSalvar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnAddrSalvar.Image = ((System.Drawing.Image)(resources.GetObject("btnAddrSalvar.Image")));
            this.btnAddrSalvar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAddrSalvar.Location = new System.Drawing.Point(593, 5);
            this.btnAddrSalvar.Name = "btnAddrSalvar";
            this.btnAddrSalvar.Size = new System.Drawing.Size(65, 60);
            this.btnAddrSalvar.TabIndex = 1;
            this.btnAddrSalvar.Text = "Salvar";
            this.btnAddrSalvar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAddrSalvar.UseVisualStyleBackColor = true;
            this.btnAddrSalvar.Click += new System.EventHandler(this.BtnAddrSalvar_Click);
            // 
            // btnAddrCancelar
            // 
            this.btnAddrCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddrCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAddrCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddrCancelar.FlatAppearance.BorderSize = 0;
            this.btnAddrCancelar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAddrCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAddrCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddrCancelar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddrCancelar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnAddrCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnAddrCancelar.Image")));
            this.btnAddrCancelar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAddrCancelar.Location = new System.Drawing.Point(16, 3);
            this.btnAddrCancelar.Name = "btnAddrCancelar";
            this.btnAddrCancelar.Size = new System.Drawing.Size(69, 60);
            this.btnAddrCancelar.TabIndex = 2;
            this.btnAddrCancelar.Text = "Excluir";
            this.btnAddrCancelar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAddrCancelar.UseVisualStyleBackColor = true;
            this.btnAddrCancelar.Click += new System.EventHandler(this.BtnAddrCancelar_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.White;
            this.label10.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label10.Location = new System.Drawing.Point(13, 79);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 17);
            this.label10.TabIndex = 28;
            this.label10.Text = "CEP";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.White;
            this.label12.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label12.Location = new System.Drawing.Point(155, 78);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(31, 17);
            this.label12.TabIndex = 30;
            this.label12.Text = "Rua";
            // 
            // rua
            // 
            this.rua.AlphaNumeric = false;
            this.rua.BackColor = System.Drawing.Color.White;
            this.rua.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rua.BackColorState.Enabled = System.Drawing.Color.White;
            this.rua.Border.Color = System.Drawing.Color.Gainsboro;
            this.rua.Border.HoverColor = System.Drawing.Color.Gainsboro;
            this.rua.Border.HoverVisible = true;
            this.rua.Border.Rounding = 8;
            this.rua.Border.Thickness = 1;
            this.rua.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.rua.Border.Visible = true;
            this.rua.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.rua.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.rua.ButtonBorder.HoverVisible = true;
            this.rua.ButtonBorder.Rounding = 6;
            this.rua.ButtonBorder.Thickness = 1;
            this.rua.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.rua.ButtonBorder.Visible = true;
            this.rua.ButtonColor.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rua.ButtonColor.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.rua.ButtonColor.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rua.ButtonColor.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.rua.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rua.ButtonIndent = 3;
            this.rua.ButtonText = "visualButton";
            this.rua.ButtonVisible = false;
            this.rua.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rua.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.rua.Image = null;
            this.rua.ImageSize = new System.Drawing.Size(16, 16);
            this.rua.ImageVisible = false;
            this.rua.ImageWidth = 35;
            this.rua.Location = new System.Drawing.Point(155, 98);
            this.rua.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.rua.Name = "rua";
            this.rua.PasswordChar = '\0';
            this.rua.ReadOnly = false;
            this.rua.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.rua.Size = new System.Drawing.Size(399, 28);
            this.rua.TabIndex = 2;
            this.rua.TextBoxWidth = 387;
            this.rua.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.rua.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.rua.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.rua.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.rua.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.rua.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.rua.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.rua.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.rua.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rua.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.rua.Watermark.Text = "Watermark text";
            this.rua.Watermark.Visible = false;
            this.rua.WordWrap = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.White;
            this.label13.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label13.Location = new System.Drawing.Point(13, 129);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(44, 17);
            this.label13.TabIndex = 32;
            this.label13.Text = "Bairro";
            // 
            // bairro
            // 
            this.bairro.AlphaNumeric = false;
            this.bairro.BackColor = System.Drawing.Color.White;
            this.bairro.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.bairro.BackColorState.Enabled = System.Drawing.Color.White;
            this.bairro.Border.Color = System.Drawing.Color.Gainsboro;
            this.bairro.Border.HoverColor = System.Drawing.Color.Gainsboro;
            this.bairro.Border.HoverVisible = true;
            this.bairro.Border.Rounding = 8;
            this.bairro.Border.Thickness = 1;
            this.bairro.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.bairro.Border.Visible = true;
            this.bairro.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.bairro.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.bairro.ButtonBorder.HoverVisible = true;
            this.bairro.ButtonBorder.Rounding = 6;
            this.bairro.ButtonBorder.Thickness = 1;
            this.bairro.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.bairro.ButtonBorder.Visible = true;
            this.bairro.ButtonColor.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bairro.ButtonColor.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.bairro.ButtonColor.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bairro.ButtonColor.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.bairro.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bairro.ButtonIndent = 3;
            this.bairro.ButtonText = "visualButton";
            this.bairro.ButtonVisible = false;
            this.bairro.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bairro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.bairro.Image = null;
            this.bairro.ImageSize = new System.Drawing.Size(16, 16);
            this.bairro.ImageVisible = false;
            this.bairro.ImageWidth = 35;
            this.bairro.Location = new System.Drawing.Point(14, 148);
            this.bairro.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.bairro.Name = "bairro";
            this.bairro.PasswordChar = '\0';
            this.bairro.ReadOnly = false;
            this.bairro.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.bairro.Size = new System.Drawing.Size(175, 28);
            this.bairro.TabIndex = 4;
            this.bairro.TextBoxWidth = 163;
            this.bairro.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.bairro.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.bairro.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.bairro.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.bairro.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.bairro.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.bairro.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.bairro.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.bairro.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bairro.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.bairro.Watermark.Text = "Watermark text";
            this.bairro.Watermark.Visible = false;
            this.bairro.WordWrap = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.White;
            this.label14.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label14.Location = new System.Drawing.Point(559, 77);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(58, 17);
            this.label14.TabIndex = 34;
            this.label14.Text = "Número";
            // 
            // nr
            // 
            this.nr.AlphaNumeric = false;
            this.nr.BackColor = System.Drawing.Color.White;
            this.nr.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.nr.BackColorState.Enabled = System.Drawing.Color.White;
            this.nr.Border.Color = System.Drawing.Color.Gainsboro;
            this.nr.Border.HoverColor = System.Drawing.Color.Gainsboro;
            this.nr.Border.HoverVisible = true;
            this.nr.Border.Rounding = 8;
            this.nr.Border.Thickness = 1;
            this.nr.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.nr.Border.Visible = true;
            this.nr.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.nr.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.nr.ButtonBorder.HoverVisible = true;
            this.nr.ButtonBorder.Rounding = 6;
            this.nr.ButtonBorder.Thickness = 1;
            this.nr.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.nr.ButtonBorder.Visible = true;
            this.nr.ButtonColor.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.nr.ButtonColor.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.nr.ButtonColor.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.nr.ButtonColor.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.nr.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nr.ButtonIndent = 3;
            this.nr.ButtonText = "visualButton";
            this.nr.ButtonVisible = false;
            this.nr.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.nr.Image = null;
            this.nr.ImageSize = new System.Drawing.Size(16, 16);
            this.nr.ImageVisible = false;
            this.nr.ImageWidth = 35;
            this.nr.Location = new System.Drawing.Point(560, 96);
            this.nr.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.nr.Name = "nr";
            this.nr.PasswordChar = '\0';
            this.nr.ReadOnly = false;
            this.nr.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.nr.Size = new System.Drawing.Size(98, 28);
            this.nr.TabIndex = 3;
            this.nr.TextBoxWidth = 86;
            this.nr.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.nr.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.nr.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.nr.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.nr.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.nr.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.nr.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.nr.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.nr.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nr.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.nr.Watermark.Text = "Watermark text";
            this.nr.Watermark.Visible = false;
            this.nr.WordWrap = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.White;
            this.label15.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label15.Location = new System.Drawing.Point(194, 129);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(94, 17);
            this.label15.TabIndex = 36;
            this.label15.Text = "Complemento";
            // 
            // complemento
            // 
            this.complemento.AlphaNumeric = false;
            this.complemento.BackColor = System.Drawing.Color.White;
            this.complemento.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.complemento.BackColorState.Enabled = System.Drawing.Color.White;
            this.complemento.Border.Color = System.Drawing.Color.Gainsboro;
            this.complemento.Border.HoverColor = System.Drawing.Color.Gainsboro;
            this.complemento.Border.HoverVisible = true;
            this.complemento.Border.Rounding = 8;
            this.complemento.Border.Thickness = 1;
            this.complemento.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.complemento.Border.Visible = true;
            this.complemento.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.complemento.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.complemento.ButtonBorder.HoverVisible = true;
            this.complemento.ButtonBorder.Rounding = 6;
            this.complemento.ButtonBorder.Thickness = 1;
            this.complemento.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.complemento.ButtonBorder.Visible = true;
            this.complemento.ButtonColor.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.complemento.ButtonColor.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.complemento.ButtonColor.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.complemento.ButtonColor.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.complemento.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.complemento.ButtonIndent = 3;
            this.complemento.ButtonText = "visualButton";
            this.complemento.ButtonVisible = false;
            this.complemento.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.complemento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.complemento.Image = null;
            this.complemento.ImageSize = new System.Drawing.Size(16, 16);
            this.complemento.ImageVisible = false;
            this.complemento.ImageWidth = 35;
            this.complemento.Location = new System.Drawing.Point(195, 148);
            this.complemento.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.complemento.Name = "complemento";
            this.complemento.PasswordChar = '\0';
            this.complemento.ReadOnly = false;
            this.complemento.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.complemento.Size = new System.Drawing.Size(463, 28);
            this.complemento.TabIndex = 5;
            this.complemento.TextBoxWidth = 451;
            this.complemento.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.complemento.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.complemento.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.complemento.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.complemento.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.complemento.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.complemento.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.complemento.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.complemento.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.complemento.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.complemento.Watermark.Text = "Watermark text";
            this.complemento.Watermark.Visible = false;
            this.complemento.WordWrap = true;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.White;
            this.label16.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label16.Location = new System.Drawing.Point(339, 179);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(49, 17);
            this.label16.TabIndex = 38;
            this.label16.Text = "Estado";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.White;
            this.label17.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label17.Location = new System.Drawing.Point(405, 179);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(32, 17);
            this.label17.TabIndex = 40;
            this.label17.Text = "País";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.White;
            this.label18.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label18.Location = new System.Drawing.Point(557, 179);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(36, 17);
            this.label18.TabIndex = 42;
            this.label18.Text = "IBGE";
            // 
            // ibge
            // 
            this.ibge.AlphaNumeric = false;
            this.ibge.BackColor = System.Drawing.Color.White;
            this.ibge.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.ibge.BackColorState.Enabled = System.Drawing.Color.White;
            this.ibge.Border.Color = System.Drawing.Color.Gainsboro;
            this.ibge.Border.HoverColor = System.Drawing.Color.Gainsboro;
            this.ibge.Border.HoverVisible = true;
            this.ibge.Border.Rounding = 8;
            this.ibge.Border.Thickness = 1;
            this.ibge.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.ibge.Border.Visible = true;
            this.ibge.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.ibge.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.ibge.ButtonBorder.HoverVisible = true;
            this.ibge.ButtonBorder.Rounding = 6;
            this.ibge.ButtonBorder.Thickness = 1;
            this.ibge.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.ibge.ButtonBorder.Visible = true;
            this.ibge.ButtonColor.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ibge.ButtonColor.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.ibge.ButtonColor.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ibge.ButtonColor.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ibge.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ibge.ButtonIndent = 3;
            this.ibge.ButtonText = "visualButton";
            this.ibge.ButtonVisible = false;
            this.ibge.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ibge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ibge.Image = null;
            this.ibge.ImageSize = new System.Drawing.Size(16, 16);
            this.ibge.ImageVisible = false;
            this.ibge.ImageWidth = 35;
            this.ibge.Location = new System.Drawing.Point(560, 198);
            this.ibge.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.ibge.Name = "ibge";
            this.ibge.PasswordChar = '\0';
            this.ibge.ReadOnly = false;
            this.ibge.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.ibge.Size = new System.Drawing.Size(98, 28);
            this.ibge.TabIndex = 9;
            this.ibge.TextBoxWidth = 86;
            this.ibge.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.ibge.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ibge.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ibge.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ibge.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.ibge.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.ibge.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.ibge.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ibge.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ibge.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.ibge.Watermark.Text = "Watermark text";
            this.ibge.Watermark.Visible = false;
            this.ibge.WordWrap = true;
            // 
            // buscarEndereco
            // 
            this.buscarEndereco.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buscarEndereco.BackgroundImage")));
            this.buscarEndereco.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buscarEndereco.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buscarEndereco.FlatAppearance.BorderSize = 0;
            this.buscarEndereco.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buscarEndereco.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buscarEndereco.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buscarEndereco.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buscarEndereco.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buscarEndereco.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buscarEndereco.Location = new System.Drawing.Point(126, 99);
            this.buscarEndereco.Name = "buscarEndereco";
            this.buscarEndereco.Size = new System.Drawing.Size(25, 25);
            this.buscarEndereco.TabIndex = 1;
            this.buscarEndereco.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buscarEndereco.UseVisualStyleBackColor = true;
            this.buscarEndereco.Click += new System.EventHandler(this.BuscarEndereco_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.panel2.Controls.Add(this.btnAddrSalvar);
            this.panel2.Controls.Add(this.btnAddrCancelar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 238);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(670, 68);
            this.panel2.TabIndex = 10;
            // 
            // cep
            // 
            this.cep.AlphaNumeric = false;
            this.cep.BackColor = System.Drawing.Color.White;
            this.cep.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.cep.BackColorState.Enabled = System.Drawing.Color.White;
            this.cep.Border.Color = System.Drawing.Color.Gainsboro;
            this.cep.Border.HoverColor = System.Drawing.Color.Gainsboro;
            this.cep.Border.HoverVisible = true;
            this.cep.Border.Rounding = 8;
            this.cep.Border.Thickness = 1;
            this.cep.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.cep.Border.Visible = true;
            this.cep.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.cep.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.cep.ButtonBorder.HoverVisible = true;
            this.cep.ButtonBorder.Rounding = 6;
            this.cep.ButtonBorder.Thickness = 1;
            this.cep.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.cep.ButtonBorder.Visible = true;
            this.cep.ButtonColor.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cep.ButtonColor.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.cep.ButtonColor.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cep.ButtonColor.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.cep.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cep.ButtonIndent = 3;
            this.cep.ButtonText = "visualButton";
            this.cep.ButtonVisible = false;
            this.cep.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cep.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cep.Image = null;
            this.cep.ImageSize = new System.Drawing.Size(16, 16);
            this.cep.ImageVisible = false;
            this.cep.ImageWidth = 35;
            this.cep.Location = new System.Drawing.Point(16, 98);
            this.cep.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.cep.Name = "cep";
            this.cep.PasswordChar = '\0';
            this.cep.ReadOnly = false;
            this.cep.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.cep.Size = new System.Drawing.Size(104, 28);
            this.cep.TabIndex = 0;
            this.cep.TextBoxWidth = 92;
            this.cep.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.cep.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cep.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cep.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cep.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.cep.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.cep.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.cep.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.cep.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cep.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.cep.Watermark.Text = "Watermark text";
            this.cep.Watermark.Visible = false;
            this.cep.WordWrap = true;
            this.cep.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Cep_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(13, 179);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 17);
            this.label1.TabIndex = 44;
            this.label1.Text = "Cidade";
            // 
            // cidade
            // 
            this.cidade.AlphaNumeric = false;
            this.cidade.BackColor = System.Drawing.Color.White;
            this.cidade.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.cidade.BackColorState.Enabled = System.Drawing.Color.White;
            this.cidade.Border.Color = System.Drawing.Color.Gainsboro;
            this.cidade.Border.HoverColor = System.Drawing.Color.Gainsboro;
            this.cidade.Border.HoverVisible = true;
            this.cidade.Border.Rounding = 8;
            this.cidade.Border.Thickness = 1;
            this.cidade.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.cidade.Border.Visible = true;
            this.cidade.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.cidade.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.cidade.ButtonBorder.HoverVisible = true;
            this.cidade.ButtonBorder.Rounding = 6;
            this.cidade.ButtonBorder.Thickness = 1;
            this.cidade.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.cidade.ButtonBorder.Visible = true;
            this.cidade.ButtonColor.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cidade.ButtonColor.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.cidade.ButtonColor.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cidade.ButtonColor.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.cidade.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cidade.ButtonIndent = 3;
            this.cidade.ButtonText = "visualButton";
            this.cidade.ButtonVisible = false;
            this.cidade.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cidade.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cidade.Image = null;
            this.cidade.ImageSize = new System.Drawing.Size(16, 16);
            this.cidade.ImageVisible = false;
            this.cidade.ImageWidth = 35;
            this.cidade.Location = new System.Drawing.Point(14, 198);
            this.cidade.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.cidade.Name = "cidade";
            this.cidade.PasswordChar = '\0';
            this.cidade.ReadOnly = false;
            this.cidade.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.cidade.Size = new System.Drawing.Size(320, 28);
            this.cidade.TabIndex = 6;
            this.cidade.TextBoxWidth = 308;
            this.cidade.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.cidade.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cidade.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cidade.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cidade.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.cidade.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.cidade.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.cidade.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.cidade.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cidade.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.cidade.Watermark.Text = "Watermark text";
            this.cidade.Watermark.Visible = false;
            this.cidade.WordWrap = true;
            // 
            // estado
            // 
            this.estado.BackColor = System.Drawing.Color.White;
            this.estado.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.estado.BackColorState.Enabled = System.Drawing.Color.White;
            this.estado.Border.Color = System.Drawing.Color.Gainsboro;
            this.estado.Border.HoverColor = System.Drawing.Color.Gainsboro;
            this.estado.Border.HoverVisible = true;
            this.estado.Border.Rounding = 6;
            this.estado.Border.Thickness = 1;
            this.estado.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.estado.Border.Visible = true;
            this.estado.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(119)))), ((int)(((byte)(118)))));
            this.estado.ButtonImage = null;
            this.estado.ButtonStyle = VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox.ButtonStyles.Arrow;
            this.estado.ButtonWidth = 30;
            this.estado.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.estado.DropDownHeight = 150;
            this.estado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.estado.DropDownWidth = 500;
            this.estado.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.estado.FormattingEnabled = true;
            this.estado.ImageList = null;
            this.estado.ImageVisible = false;
            this.estado.Index = 0;
            this.estado.IntegralHeight = false;
            this.estado.ItemHeight = 22;
            this.estado.ItemImageVisible = true;
            this.estado.Location = new System.Drawing.Point(340, 198);
            this.estado.MaxDropDownItems = 100;
            this.estado.MenuItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.estado.MenuItemNormal = System.Drawing.Color.White;
            this.estado.MenuTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.estado.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.estado.Name = "estado";
            this.estado.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.estado.SeparatorVisible = false;
            this.estado.Size = new System.Drawing.Size(62, 28);
            this.estado.State = VisualPlus.Enumerators.MouseStates.Normal;
            this.estado.TabIndex = 7;
            this.estado.TextAlignment = System.Drawing.StringAlignment.Center;
            this.estado.TextDisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.estado.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.estado.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.estado.TextRendering = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.estado.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.estado.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.estado.TextStyle.Hover = System.Drawing.Color.Empty;
            this.estado.TextStyle.Pressed = System.Drawing.Color.Empty;
            this.estado.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.estado.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.estado.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.estado.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.estado.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.estado.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.estado.Watermark.Text = "Watermark text";
            this.estado.Watermark.Visible = false;
            // 
            // pais
            // 
            this.pais.BackColor = System.Drawing.Color.White;
            this.pais.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.pais.BackColorState.Enabled = System.Drawing.Color.White;
            this.pais.Border.Color = System.Drawing.Color.Gainsboro;
            this.pais.Border.HoverColor = System.Drawing.Color.Gainsboro;
            this.pais.Border.HoverVisible = true;
            this.pais.Border.Rounding = 6;
            this.pais.Border.Thickness = 1;
            this.pais.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.pais.Border.Visible = true;
            this.pais.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(119)))), ((int)(((byte)(118)))));
            this.pais.ButtonImage = null;
            this.pais.ButtonStyle = VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox.ButtonStyles.Arrow;
            this.pais.ButtonWidth = 30;
            this.pais.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.pais.DropDownHeight = 50;
            this.pais.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pais.DropDownWidth = 500;
            this.pais.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pais.FormattingEnabled = true;
            this.pais.ImageList = null;
            this.pais.ImageVisible = false;
            this.pais.Index = 0;
            this.pais.IntegralHeight = false;
            this.pais.ItemHeight = 22;
            this.pais.ItemImageVisible = true;
            this.pais.Location = new System.Drawing.Point(408, 198);
            this.pais.MaxDropDownItems = 100;
            this.pais.MenuItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pais.MenuItemNormal = System.Drawing.Color.White;
            this.pais.MenuTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pais.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.pais.Name = "pais";
            this.pais.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pais.SeparatorVisible = false;
            this.pais.Size = new System.Drawing.Size(146, 28);
            this.pais.State = VisualPlus.Enumerators.MouseStates.Normal;
            this.pais.TabIndex = 8;
            this.pais.TextAlignment = System.Drawing.StringAlignment.Center;
            this.pais.TextDisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.pais.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.pais.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.pais.TextRendering = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.pais.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.pais.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pais.TextStyle.Hover = System.Drawing.Color.Empty;
            this.pais.TextStyle.Pressed = System.Drawing.Color.Empty;
            this.pais.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.pais.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.pais.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.pais.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.pais.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pais.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.pais.Watermark.Text = "Watermark text";
            this.pais.Watermark.Visible = false;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker1_RunWorkerCompleted);
            // 
            // AddClienteEndereco
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(670, 306);
            this.Controls.Add(this.pais);
            this.Controls.Add(this.estado);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cidade);
            this.Controls.Add(this.cep);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.buscarEndereco);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.ibge);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.complemento);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.nr);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.bairro);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.rua);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(686, 345);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(686, 305);
            this.Name = "AddClienteEndereco";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddClienteEndereco_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnAddrSalvar;
        private System.Windows.Forms.Button btnAddrCancelar;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private VisualPlus.Toolkit.Controls.Editors.VisualTextBox rua;
        private System.Windows.Forms.Label label13;
        private VisualPlus.Toolkit.Controls.Editors.VisualTextBox bairro;
        private System.Windows.Forms.Label label14;
        private VisualPlus.Toolkit.Controls.Editors.VisualTextBox nr;
        private System.Windows.Forms.Label label15;
        private VisualPlus.Toolkit.Controls.Editors.VisualTextBox complemento;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private VisualPlus.Toolkit.Controls.Editors.VisualTextBox ibge;
        private System.Windows.Forms.Button buscarEndereco;
        private System.Windows.Forms.Panel panel2;
        private VisualPlus.Toolkit.Controls.Editors.VisualTextBox cep;
        private System.Windows.Forms.Label label1;
        private VisualPlus.Toolkit.Controls.Editors.VisualTextBox cidade;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox estado;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox pais;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}