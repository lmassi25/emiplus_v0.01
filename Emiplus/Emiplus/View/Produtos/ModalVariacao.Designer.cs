namespace Emiplus.View.Produtos
{
    partial class ModalVariacao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModalVariacao));
            this.panel5 = new System.Windows.Forms.Panel();
            this.dataGridVariacao = new System.Windows.Forms.DataGridView();
            this.label30 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.txtBuscarVariacao = new VisualPlus.Toolkit.Controls.Editors.VisualTextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.visualPanel2 = new VisualPlus.Toolkit.Controls.Layout.VisualPanel();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.barraTitulo = new System.Windows.Forms.Panel();
            this.btnSalvar = new VisualPlus.Toolkit.Controls.Interactivity.VisualButton();
            this.btnGerar = new VisualPlus.Toolkit.Controls.Interactivity.VisualButton();
            this.txtGrupos = new VisualPlus.Toolkit.Controls.Editors.VisualTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddGrupo = new VisualPlus.Toolkit.Controls.Interactivity.VisualButton();
            this.btnClearCombinacao = new System.Windows.Forms.Label();
            this.checkCodeBarras = new VisualPlus.Toolkit.Controls.Interactivity.VisualCheckBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.btnClose = new VisualPlus.Toolkit.Controls.Interactivity.VisualButton();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridVariacao)).BeginInit();
            this.visualPanel2.SuspendLayout();
            this.barraTitulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.Transparent;
            this.panel5.Controls.Add(this.dataGridVariacao);
            this.panel5.Location = new System.Drawing.Point(27, 203);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(725, 292);
            this.panel5.TabIndex = 15132;
            // 
            // dataGridVariacao
            // 
            this.dataGridVariacao.AllowUserToAddRows = false;
            this.dataGridVariacao.AllowUserToDeleteRows = false;
            this.dataGridVariacao.AllowUserToResizeColumns = false;
            this.dataGridVariacao.AllowUserToResizeRows = false;
            this.dataGridVariacao.BackgroundColor = System.Drawing.Color.White;
            this.dataGridVariacao.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridVariacao.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridVariacao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridVariacao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridVariacao.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridVariacao.Location = new System.Drawing.Point(0, 0);
            this.dataGridVariacao.MultiSelect = false;
            this.dataGridVariacao.Name = "dataGridVariacao";
            this.dataGridVariacao.RowHeadersVisible = false;
            this.dataGridVariacao.RowTemplate.Height = 30;
            this.dataGridVariacao.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridVariacao.Size = new System.Drawing.Size(725, 292);
            this.dataGridVariacao.TabIndex = 6;
            this.dataGridVariacao.TabStop = false;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.BackColor = System.Drawing.Color.Transparent;
            this.label30.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label30.Location = new System.Drawing.Point(24, 181);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(89, 17);
            this.label30.TabIndex = 15131;
            this.label30.Text = "Combinações";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.BackColor = System.Drawing.Color.White;
            this.label29.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label29.Location = new System.Drawing.Point(263, 147);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(297, 13);
            this.label29.TabIndex = 15128;
            this.label29.Text = "Busque os grupos de variações para combinar a esse produto.";
            // 
            // txtBuscarVariacao
            // 
            this.txtBuscarVariacao.AlphaNumeric = false;
            this.txtBuscarVariacao.BackColor = System.Drawing.Color.White;
            this.txtBuscarVariacao.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.txtBuscarVariacao.BackColorState.Enabled = System.Drawing.Color.White;
            this.txtBuscarVariacao.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(229)))), ((int)(((byte)(236)))));
            this.txtBuscarVariacao.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(171)))), ((int)(((byte)(255)))));
            this.txtBuscarVariacao.Border.HoverVisible = true;
            this.txtBuscarVariacao.Border.Rounding = 8;
            this.txtBuscarVariacao.Border.Thickness = 1;
            this.txtBuscarVariacao.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.txtBuscarVariacao.Border.Visible = true;
            this.txtBuscarVariacao.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtBuscarVariacao.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.txtBuscarVariacao.ButtonBorder.HoverVisible = true;
            this.txtBuscarVariacao.ButtonBorder.Rounding = 6;
            this.txtBuscarVariacao.ButtonBorder.Thickness = 1;
            this.txtBuscarVariacao.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.txtBuscarVariacao.ButtonBorder.Visible = true;
            this.txtBuscarVariacao.ButtonColor.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtBuscarVariacao.ButtonColor.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtBuscarVariacao.ButtonColor.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtBuscarVariacao.ButtonColor.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.txtBuscarVariacao.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscarVariacao.ButtonIndent = 3;
            this.txtBuscarVariacao.ButtonText = "visualButton";
            this.txtBuscarVariacao.ButtonVisible = false;
            this.txtBuscarVariacao.Enabled = false;
            this.txtBuscarVariacao.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscarVariacao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtBuscarVariacao.Image = null;
            this.txtBuscarVariacao.ImageSize = new System.Drawing.Size(16, 16);
            this.txtBuscarVariacao.ImageVisible = false;
            this.txtBuscarVariacao.ImageWidth = 35;
            this.txtBuscarVariacao.Location = new System.Drawing.Point(263, 115);
            this.txtBuscarVariacao.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.txtBuscarVariacao.Name = "txtBuscarVariacao";
            this.txtBuscarVariacao.PasswordChar = '\0';
            this.txtBuscarVariacao.ReadOnly = false;
            this.txtBuscarVariacao.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtBuscarVariacao.Size = new System.Drawing.Size(375, 30);
            this.txtBuscarVariacao.TabIndex = 15129;
            this.txtBuscarVariacao.TextBoxWidth = 363;
            this.txtBuscarVariacao.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.txtBuscarVariacao.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtBuscarVariacao.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtBuscarVariacao.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtBuscarVariacao.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.txtBuscarVariacao.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.txtBuscarVariacao.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.txtBuscarVariacao.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.txtBuscarVariacao.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscarVariacao.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.txtBuscarVariacao.Watermark.Text = "Watermark text";
            this.txtBuscarVariacao.Watermark.Visible = false;
            this.txtBuscarVariacao.WordWrap = true;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.BackColor = System.Drawing.Color.Transparent;
            this.label28.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label28.Location = new System.Drawing.Point(263, 95);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(64, 17);
            this.label28.TabIndex = 15130;
            this.label28.Text = "Variações";
            // 
            // visualPanel2
            // 
            this.visualPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.visualPanel2.BackColor = System.Drawing.Color.Transparent;
            this.visualPanel2.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.visualPanel2.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(161)))), ((int)(((byte)(194)))));
            this.visualPanel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.visualPanel2.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(161)))), ((int)(((byte)(194)))));
            this.visualPanel2.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(161)))), ((int)(((byte)(194)))));
            this.visualPanel2.Border.HoverVisible = true;
            this.visualPanel2.Border.Rounding = 6;
            this.visualPanel2.Border.Thickness = 1;
            this.visualPanel2.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.visualPanel2.Border.Visible = true;
            this.visualPanel2.Controls.Add(this.label26);
            this.visualPanel2.Controls.Add(this.label27);
            this.visualPanel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualPanel2.Location = new System.Drawing.Point(27, 21);
            this.visualPanel2.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.visualPanel2.Name = "visualPanel2";
            this.visualPanel2.Padding = new System.Windows.Forms.Padding(5);
            this.visualPanel2.Size = new System.Drawing.Size(725, 62);
            this.visualPanel2.TabIndex = 15127;
            this.visualPanel2.Text = "visualPanel2";
            this.visualPanel2.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.visualPanel2.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualPanel2.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualPanel2.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualPanel2.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.visualPanel2.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.visualPanel2.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(161)))), ((int)(((byte)(194)))));
            this.label26.Font = new System.Drawing.Font("Segoe UI Semilight", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.White;
            this.label26.Location = new System.Drawing.Point(11, 34);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(362, 15);
            this.label26.TabIndex = 6;
            this.label26.Text = "Adicione abaixo a combinação de variações que seu produto possui.";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(161)))), ((int)(((byte)(194)))));
            this.label27.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.White;
            this.label27.Location = new System.Drawing.Point(11, 15);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(173, 15);
            this.label27.TabIndex = 4;
            this.label27.Text = "Seu produto possui variações?";
            // 
            // barraTitulo
            // 
            this.barraTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.barraTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(246)))));
            this.barraTitulo.Controls.Add(this.btnClose);
            this.barraTitulo.Controls.Add(this.btnSalvar);
            this.barraTitulo.Location = new System.Drawing.Point(0, 519);
            this.barraTitulo.Name = "barraTitulo";
            this.barraTitulo.Size = new System.Drawing.Size(776, 40);
            this.barraTitulo.TabIndex = 15133;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalvar.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSalvar.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(166)))), ((int)(((byte)(155)))));
            this.btnSalvar.BackColorState.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(158)))), ((int)(((byte)(147)))));
            this.btnSalvar.BackColorState.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(158)))), ((int)(((byte)(147)))));
            this.btnSalvar.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(166)))), ((int)(((byte)(155)))));
            this.btnSalvar.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(158)))), ((int)(((byte)(147)))));
            this.btnSalvar.Border.HoverVisible = true;
            this.btnSalvar.Border.Rounding = 6;
            this.btnSalvar.Border.Thickness = 1;
            this.btnSalvar.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.btnSalvar.Border.Visible = true;
            this.btnSalvar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvar.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSalvar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvar.ForeColor = System.Drawing.Color.White;
            this.btnSalvar.Image = null;
            this.btnSalvar.Location = new System.Drawing.Point(665, 5);
            this.btnSalvar.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(96, 30);
            this.btnSalvar.TabIndex = 553;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSalvar.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.btnSalvar.TextStyle.Enabled = System.Drawing.Color.White;
            this.btnSalvar.TextStyle.Hover = System.Drawing.Color.White;
            this.btnSalvar.TextStyle.Pressed = System.Drawing.Color.White;
            this.btnSalvar.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btnSalvar.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.btnSalvar.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // btnGerar
            // 
            this.btnGerar.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnGerar.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(161)))), ((int)(((byte)(194)))));
            this.btnGerar.BackColorState.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(148)))), ((int)(((byte)(182)))));
            this.btnGerar.BackColorState.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(148)))), ((int)(((byte)(182)))));
            this.btnGerar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGerar.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(161)))), ((int)(((byte)(194)))));
            this.btnGerar.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(148)))), ((int)(((byte)(182)))));
            this.btnGerar.Border.HoverVisible = true;
            this.btnGerar.Border.Rounding = 6;
            this.btnGerar.Border.Thickness = 1;
            this.btnGerar.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.btnGerar.Border.Visible = true;
            this.btnGerar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGerar.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnGerar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGerar.ForeColor = System.Drawing.Color.White;
            this.btnGerar.Image = null;
            this.btnGerar.Location = new System.Drawing.Point(644, 115);
            this.btnGerar.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.btnGerar.Name = "btnGerar";
            this.btnGerar.Size = new System.Drawing.Size(108, 30);
            this.btnGerar.TabIndex = 15134;
            this.btnGerar.Text = "Gerar";
            this.btnGerar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGerar.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.btnGerar.TextStyle.Enabled = System.Drawing.Color.White;
            this.btnGerar.TextStyle.Hover = System.Drawing.Color.White;
            this.btnGerar.TextStyle.Pressed = System.Drawing.Color.White;
            this.btnGerar.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btnGerar.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.btnGerar.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // txtGrupos
            // 
            this.txtGrupos.AlphaNumeric = false;
            this.txtGrupos.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtGrupos.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtGrupos.BackColor = System.Drawing.Color.White;
            this.txtGrupos.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtGrupos.BackColorState.Enabled = System.Drawing.Color.White;
            this.txtGrupos.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(229)))), ((int)(((byte)(236)))));
            this.txtGrupos.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(171)))), ((int)(((byte)(255)))));
            this.txtGrupos.Border.HoverVisible = true;
            this.txtGrupos.Border.Rounding = 8;
            this.txtGrupos.Border.Thickness = 1;
            this.txtGrupos.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.txtGrupos.Border.Visible = true;
            this.txtGrupos.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtGrupos.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.txtGrupos.ButtonBorder.HoverVisible = true;
            this.txtGrupos.ButtonBorder.Rounding = 6;
            this.txtGrupos.ButtonBorder.Thickness = 1;
            this.txtGrupos.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.txtGrupos.ButtonBorder.Visible = true;
            this.txtGrupos.ButtonColor.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtGrupos.ButtonColor.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtGrupos.ButtonColor.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtGrupos.ButtonColor.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.txtGrupos.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGrupos.ButtonIndent = 3;
            this.txtGrupos.ButtonText = "visualButton";
            this.txtGrupos.ButtonVisible = false;
            this.txtGrupos.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGrupos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtGrupos.Image = null;
            this.txtGrupos.ImageSize = new System.Drawing.Size(16, 16);
            this.txtGrupos.ImageVisible = false;
            this.txtGrupos.ImageWidth = 35;
            this.txtGrupos.Location = new System.Drawing.Point(27, 115);
            this.txtGrupos.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.txtGrupos.Name = "txtGrupos";
            this.txtGrupos.PasswordChar = '\0';
            this.txtGrupos.ReadOnly = false;
            this.txtGrupos.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtGrupos.Size = new System.Drawing.Size(184, 30);
            this.txtGrupos.TabIndex = 15135;
            this.txtGrupos.TextBoxWidth = 172;
            this.txtGrupos.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.txtGrupos.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtGrupos.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtGrupos.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtGrupos.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.txtGrupos.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.txtGrupos.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.txtGrupos.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.txtGrupos.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGrupos.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.txtGrupos.Watermark.Text = "Watermark text";
            this.txtGrupos.Watermark.Visible = false;
            this.txtGrupos.WordWrap = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(24, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 17);
            this.label1.TabIndex = 15136;
            this.label1.Text = "Buscar Grupos";
            // 
            // btnAddGrupo
            // 
            this.btnAddGrupo.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnAddGrupo.BackColorState.Enabled = System.Drawing.Color.White;
            this.btnAddGrupo.BackColorState.Hover = System.Drawing.Color.White;
            this.btnAddGrupo.BackColorState.Pressed = System.Drawing.Color.White;
            this.btnAddGrupo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnAddGrupo.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(229)))), ((int)(((byte)(236)))));
            this.btnAddGrupo.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnAddGrupo.Border.HoverVisible = true;
            this.btnAddGrupo.Border.Rounding = 6;
            this.btnAddGrupo.Border.Thickness = 1;
            this.btnAddGrupo.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.btnAddGrupo.Border.Visible = true;
            this.btnAddGrupo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddGrupo.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnAddGrupo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddGrupo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(93)))), ((int)(((byte)(110)))));
            this.btnAddGrupo.Image = ((System.Drawing.Image)(resources.GetObject("btnAddGrupo.Image")));
            this.btnAddGrupo.Location = new System.Drawing.Point(217, 116);
            this.btnAddGrupo.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.btnAddGrupo.Name = "btnAddGrupo";
            this.btnAddGrupo.Size = new System.Drawing.Size(40, 29);
            this.btnAddGrupo.TabIndex = 15137;
            this.btnAddGrupo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAddGrupo.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.btnAddGrupo.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(93)))), ((int)(((byte)(110)))));
            this.btnAddGrupo.TextStyle.Hover = System.Drawing.Color.White;
            this.btnAddGrupo.TextStyle.Pressed = System.Drawing.Color.White;
            this.btnAddGrupo.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btnAddGrupo.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.btnAddGrupo.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // btnClearCombinacao
            // 
            this.btnClearCombinacao.AutoSize = true;
            this.btnClearCombinacao.BackColor = System.Drawing.Color.Transparent;
            this.btnClearCombinacao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClearCombinacao.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearCombinacao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnClearCombinacao.Location = new System.Drawing.Point(525, 99);
            this.btnClearCombinacao.Name = "btnClearCombinacao";
            this.btnClearCombinacao.Size = new System.Drawing.Size(113, 13);
            this.btnClearCombinacao.TabIndex = 15138;
            this.btnClearCombinacao.Text = "Limpar Combinações";
            this.btnClearCombinacao.Visible = false;
            // 
            // checkCodeBarras
            // 
            this.checkCodeBarras.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(171)))), ((int)(((byte)(255)))));
            this.checkCodeBarras.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(171)))), ((int)(((byte)(255)))));
            this.checkCodeBarras.Border.HoverVisible = true;
            this.checkCodeBarras.Border.Rounding = 3;
            this.checkCodeBarras.Border.Thickness = 1;
            this.checkCodeBarras.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.checkCodeBarras.Border.Visible = true;
            this.checkCodeBarras.Box = new System.Drawing.Size(14, 14);
            this.checkCodeBarras.BoxColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.checkCodeBarras.BoxColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.checkCodeBarras.BoxColorState.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.checkCodeBarras.BoxColorState.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.checkCodeBarras.BoxSpacing = 2;
            this.checkCodeBarras.CheckStyle.AutoSize = true;
            this.checkCodeBarras.CheckStyle.Bounds = new System.Drawing.Rectangle(0, 0, 125, 23);
            this.checkCodeBarras.CheckStyle.Character = '✔';
            this.checkCodeBarras.CheckStyle.CheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(136)))), ((int)(((byte)(45)))));
            this.checkCodeBarras.CheckStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkCodeBarras.CheckStyle.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            this.checkCodeBarras.CheckStyle.ShapeRounding = 3;
            this.checkCodeBarras.CheckStyle.ShapeType = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.checkCodeBarras.CheckStyle.Style = VisualPlus.Structure.CheckStyle.CheckType.Checkmark;
            this.checkCodeBarras.CheckStyle.Thickness = 2F;
            this.checkCodeBarras.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkCodeBarras.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.checkCodeBarras.IsBoxLarger = false;
            this.checkCodeBarras.Location = new System.Drawing.Point(602, 149);
            this.checkCodeBarras.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.checkCodeBarras.Name = "checkCodeBarras";
            this.checkCodeBarras.Size = new System.Drawing.Size(150, 23);
            this.checkCodeBarras.TabIndex = 15139;
            this.checkCodeBarras.Text = "Gerar código de barras?";
            this.checkCodeBarras.TextSize = new System.Drawing.Size(123, 16);
            this.checkCodeBarras.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.checkCodeBarras.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.checkCodeBarras.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.checkCodeBarras.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.checkCodeBarras.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.checkCodeBarras.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.checkCodeBarras.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(745, 153);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(15, 15);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 25;
            this.pictureBox4.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnClose.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(161)))), ((int)(((byte)(194)))));
            this.btnClose.BackColorState.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(148)))), ((int)(((byte)(182)))));
            this.btnClose.BackColorState.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(148)))), ((int)(((byte)(182)))));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(161)))), ((int)(((byte)(194)))));
            this.btnClose.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(148)))), ((int)(((byte)(182)))));
            this.btnClose.Border.HoverVisible = true;
            this.btnClose.Border.Rounding = 6;
            this.btnClose.Border.Thickness = 1;
            this.btnClose.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.btnClose.Border.Visible = true;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Image = null;
            this.btnClose.Location = new System.Drawing.Point(12, 5);
            this.btnClose.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(108, 30);
            this.btnClose.TabIndex = 15122;
            this.btnClose.Text = "Voltar";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.btnClose.TextStyle.Enabled = System.Drawing.Color.White;
            this.btnClose.TextStyle.Hover = System.Drawing.Color.White;
            this.btnClose.TextStyle.Pressed = System.Drawing.Color.White;
            this.btnClose.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btnClose.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.btnClose.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // ModalVariacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(776, 559);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.checkCodeBarras);
            this.Controls.Add(this.btnClearCombinacao);
            this.Controls.Add(this.btnAddGrupo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtGrupos);
            this.Controls.Add(this.btnGerar);
            this.Controls.Add(this.barraTitulo);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.txtBuscarVariacao);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.visualPanel2);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.MinimumSize = new System.Drawing.Size(792, 585);
            this.Name = "ModalVariacao";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridVariacao)).EndInit();
            this.visualPanel2.ResumeLayout(false);
            this.visualPanel2.PerformLayout();
            this.barraTitulo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.DataGridView dataGridVariacao;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label29;
        private VisualPlus.Toolkit.Controls.Editors.VisualTextBox txtBuscarVariacao;
        private System.Windows.Forms.Label label28;
        private VisualPlus.Toolkit.Controls.Layout.VisualPanel visualPanel2;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Panel barraTitulo;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualButton btnSalvar;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualButton btnGerar;
        private VisualPlus.Toolkit.Controls.Editors.VisualTextBox txtGrupos;
        private System.Windows.Forms.Label label1;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualButton btnAddGrupo;
        private System.Windows.Forms.Label btnClearCombinacao;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualCheckBox checkCodeBarras;
        private System.Windows.Forms.PictureBox pictureBox4;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualButton btnClose;
    }
}