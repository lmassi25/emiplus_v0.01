namespace Emiplus.View.Comercial
{
    partial class Pedido
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pedido));
            this.label2 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.imprimir = new System.Windows.Forms.Button();
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Loading = new System.Windows.Forms.PictureBox();
            this.GridLista = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.barraTitulo = new System.Windows.Forms.Panel();
            this.BuscaID = new VisualPlus.Toolkit.Controls.Editors.VisualTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.filterRemovido = new System.Windows.Forms.RadioButton();
            this.Usuarios = new VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.dataFinal = new VisualPlus.Toolkit.Controls.Editors.VisualDateTimePicker();
            this.dataInicial = new VisualPlus.Toolkit.Controls.Editors.VisualDateTimePicker();
            this.btnSearch = new VisualPlus.Toolkit.Controls.Interactivity.VisualButton();
            this.BuscarPessoa = new VisualPlus.Toolkit.Controls.Editors.VisualTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.filterTodos = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Status = new VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Loading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridLista)).BeginInit();
            this.barraTitulo.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(256, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(335, 15);
            this.label2.TabIndex = 170;
            this.label2.Text = "Gerencie os pedido aqui! Adicione, edite ou delete um pedido.\r\n";
            // 
            // btnExit
            // 
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExit.Location = new System.Drawing.Point(24, 10);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(85, 90);
            this.btnExit.TabIndex = 11;
            this.btnExit.Text = "Voltar";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExit.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.panel5.Controls.Add(this.imprimir);
            this.panel5.Controls.Add(this.btnAdicionar);
            this.panel5.Controls.Add(this.btnEditar);
            this.panel5.Controls.Add(this.btnHelp);
            this.panel5.Location = new System.Drawing.Point(-1, 632);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1008, 97);
            this.panel5.TabIndex = 20;
            // 
            // imprimir
            // 
            this.imprimir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imprimir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.imprimir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imprimir.FlatAppearance.BorderSize = 0;
            this.imprimir.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.imprimir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.imprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.imprimir.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imprimir.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.imprimir.Image = ((System.Drawing.Image)(resources.GetObject("imprimir.Image")));
            this.imprimir.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.imprimir.Location = new System.Drawing.Point(889, 3);
            this.imprimir.Name = "imprimir";
            this.imprimir.Size = new System.Drawing.Size(116, 90);
            this.imprimir.TabIndex = 102;
            this.imprimir.Text = "Gerar Relatório";
            this.imprimir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.imprimir.UseVisualStyleBackColor = true;
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdicionar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAdicionar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdicionar.FlatAppearance.BorderSize = 0;
            this.btnAdicionar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAdicionar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAdicionar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdicionar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdicionar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnAdicionar.Image = ((System.Drawing.Image)(resources.GetObject("btnAdicionar.Image")));
            this.btnAdicionar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAdicionar.Location = new System.Drawing.Point(798, 3);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(85, 90);
            this.btnAdicionar.TabIndex = 2;
            this.btnAdicionar.Text = "Adicionar";
            this.btnAdicionar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAdicionar.UseVisualStyleBackColor = true;
            // 
            // btnEditar
            // 
            this.btnEditar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnEditar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditar.FlatAppearance.BorderSize = 0;
            this.btnEditar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnEditar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnEditar.Image = ((System.Drawing.Image)(resources.GetObject("btnEditar.Image")));
            this.btnEditar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnEditar.Location = new System.Drawing.Point(707, 3);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(85, 90);
            this.btnEditar.TabIndex = 3;
            this.btnEditar.Text = "Editar";
            this.btnEditar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEditar.UseVisualStyleBackColor = true;
            // 
            // btnHelp
            // 
            this.btnHelp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnHelp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHelp.FlatAppearance.BorderSize = 0;
            this.btnHelp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnHelp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHelp.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHelp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnHelp.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp.Image")));
            this.btnHelp.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnHelp.Location = new System.Drawing.Point(24, 3);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(85, 90);
            this.btnHelp.TabIndex = 100;
            this.btnHelp.Text = "Ajuda!";
            this.btnHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnHelp.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.Loading);
            this.panel2.Controls.Add(this.GridLista);
            this.panel2.Location = new System.Drawing.Point(254, 135);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(717, 478);
            this.panel2.TabIndex = 18;
            // 
            // Loading
            // 
            this.Loading.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Loading.Image = global::Emiplus.Properties.Resources.loader_page;
            this.Loading.Location = new System.Drawing.Point(3, 3);
            this.Loading.Name = "Loading";
            this.Loading.Size = new System.Drawing.Size(259, 159);
            this.Loading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Loading.TabIndex = 7;
            this.Loading.TabStop = false;
            this.Loading.Visible = false;
            // 
            // GridLista
            // 
            this.GridLista.AllowUserToAddRows = false;
            this.GridLista.AllowUserToDeleteRows = false;
            this.GridLista.AllowUserToResizeColumns = false;
            this.GridLista.AllowUserToResizeRows = false;
            this.GridLista.BackgroundColor = System.Drawing.Color.White;
            this.GridLista.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GridLista.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.GridLista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridLista.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridLista.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.GridLista.Location = new System.Drawing.Point(0, 0);
            this.GridLista.MultiSelect = false;
            this.GridLista.Name = "GridLista";
            this.GridLista.ReadOnly = true;
            this.GridLista.RowHeadersVisible = false;
            this.GridLista.RowTemplate.Height = 30;
            this.GridLista.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridLista.Size = new System.Drawing.Size(717, 478);
            this.GridLista.TabIndex = 50;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Light", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(248, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 40);
            this.label1.TabIndex = 160;
            this.label1.Text = "Pedidos:";
            // 
            // barraTitulo
            // 
            this.barraTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.barraTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.barraTitulo.Controls.Add(this.Status);
            this.barraTitulo.Controls.Add(this.label13);
            this.barraTitulo.Controls.Add(this.BuscaID);
            this.barraTitulo.Controls.Add(this.label6);
            this.barraTitulo.Controls.Add(this.filterRemovido);
            this.barraTitulo.Controls.Add(this.Usuarios);
            this.barraTitulo.Controls.Add(this.label12);
            this.barraTitulo.Controls.Add(this.dataFinal);
            this.barraTitulo.Controls.Add(this.dataInicial);
            this.barraTitulo.Controls.Add(this.btnSearch);
            this.barraTitulo.Controls.Add(this.BuscarPessoa);
            this.barraTitulo.Controls.Add(this.label11);
            this.barraTitulo.Controls.Add(this.label10);
            this.barraTitulo.Controls.Add(this.label9);
            this.barraTitulo.Controls.Add(this.label8);
            this.barraTitulo.Controls.Add(this.filterTodos);
            this.barraTitulo.Controls.Add(this.label7);
            this.barraTitulo.Controls.Add(this.panel3);
            this.barraTitulo.Controls.Add(this.btnExit);
            this.barraTitulo.Location = new System.Drawing.Point(-1, 41);
            this.barraTitulo.Name = "barraTitulo";
            this.barraTitulo.Size = new System.Drawing.Size(239, 596);
            this.barraTitulo.TabIndex = 15;
            // 
            // BuscaID
            // 
            this.BuscaID.AlphaNumeric = false;
            this.BuscaID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BuscaID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.BuscaID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.BuscaID.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.BuscaID.BackColorState.Enabled = System.Drawing.Color.White;
            this.BuscaID.Border.Color = System.Drawing.Color.Gainsboro;
            this.BuscaID.Border.HoverColor = System.Drawing.Color.Gainsboro;
            this.BuscaID.Border.HoverVisible = true;
            this.BuscaID.Border.Rounding = 8;
            this.BuscaID.Border.Thickness = 1;
            this.BuscaID.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.BuscaID.Border.Visible = true;
            this.BuscaID.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.BuscaID.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.BuscaID.ButtonBorder.HoverVisible = true;
            this.BuscaID.ButtonBorder.Rounding = 6;
            this.BuscaID.ButtonBorder.Thickness = 1;
            this.BuscaID.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.BuscaID.ButtonBorder.Visible = true;
            this.BuscaID.ButtonColor.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BuscaID.ButtonColor.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.BuscaID.ButtonColor.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BuscaID.ButtonColor.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.BuscaID.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BuscaID.ButtonIndent = 3;
            this.BuscaID.ButtonText = "visualButton";
            this.BuscaID.ButtonVisible = false;
            this.BuscaID.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BuscaID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BuscaID.Image = ((System.Drawing.Image)(resources.GetObject("BuscaID.Image")));
            this.BuscaID.ImageSize = new System.Drawing.Size(16, 16);
            this.BuscaID.ImageVisible = true;
            this.BuscaID.ImageWidth = 35;
            this.BuscaID.Location = new System.Drawing.Point(21, 491);
            this.BuscaID.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.BuscaID.Name = "BuscaID";
            this.BuscaID.PasswordChar = '\0';
            this.BuscaID.ReadOnly = false;
            this.BuscaID.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.BuscaID.Size = new System.Drawing.Size(200, 34);
            this.BuscaID.TabIndex = 202;
            this.BuscaID.TextBoxWidth = 150;
            this.BuscaID.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.BuscaID.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BuscaID.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BuscaID.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BuscaID.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.BuscaID.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.BuscaID.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.BuscaID.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.BuscaID.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BuscaID.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.BuscaID.Watermark.Text = "Watermark text";
            this.BuscaID.Watermark.Visible = false;
            this.BuscaID.WordWrap = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Gray;
            this.label6.Location = new System.Drawing.Point(18, 471);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 17);
            this.label6.TabIndex = 203;
            this.label6.Text = "Buscar por ID";
            // 
            // filterRemovido
            // 
            this.filterRemovido.AutoSize = true;
            this.filterRemovido.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.filterRemovido.Location = new System.Drawing.Point(21, 179);
            this.filterRemovido.Name = "filterRemovido";
            this.filterRemovido.Size = new System.Drawing.Size(85, 19);
            this.filterRemovido.TabIndex = 201;
            this.filterRemovido.TabStop = true;
            this.filterRemovido.Text = "Canceladas";
            this.filterRemovido.UseVisualStyleBackColor = true;
            // 
            // Usuarios
            // 
            this.Usuarios.BackColor = System.Drawing.Color.White;
            this.Usuarios.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.Usuarios.BackColorState.Enabled = System.Drawing.Color.White;
            this.Usuarios.Border.Color = System.Drawing.Color.Gainsboro;
            this.Usuarios.Border.HoverColor = System.Drawing.Color.Gainsboro;
            this.Usuarios.Border.HoverVisible = true;
            this.Usuarios.Border.Rounding = 6;
            this.Usuarios.Border.Thickness = 1;
            this.Usuarios.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.Usuarios.Border.Visible = true;
            this.Usuarios.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(119)))), ((int)(((byte)(118)))));
            this.Usuarios.ButtonImage = null;
            this.Usuarios.ButtonStyle = VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox.ButtonStyles.Arrow;
            this.Usuarios.ButtonWidth = 30;
            this.Usuarios.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.Usuarios.DropDownHeight = 100;
            this.Usuarios.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Usuarios.DropDownWidth = 250;
            this.Usuarios.FormattingEnabled = true;
            this.Usuarios.ImageList = null;
            this.Usuarios.ImageVisible = false;
            this.Usuarios.Index = 0;
            this.Usuarios.IntegralHeight = false;
            this.Usuarios.ItemHeight = 23;
            this.Usuarios.ItemImageVisible = true;
            this.Usuarios.Location = new System.Drawing.Point(21, 230);
            this.Usuarios.MenuItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Usuarios.MenuItemNormal = System.Drawing.Color.White;
            this.Usuarios.MenuTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Usuarios.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.Usuarios.Name = "Usuarios";
            this.Usuarios.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Usuarios.Size = new System.Drawing.Size(199, 29);
            this.Usuarios.State = VisualPlus.Enumerators.MouseStates.Normal;
            this.Usuarios.TabIndex = 200;
            this.Usuarios.TextAlignment = System.Drawing.StringAlignment.Center;
            this.Usuarios.TextDisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.Usuarios.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Usuarios.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.Usuarios.TextRendering = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.Usuarios.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.Usuarios.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Usuarios.TextStyle.Hover = System.Drawing.Color.Empty;
            this.Usuarios.TextStyle.Pressed = System.Drawing.Color.Empty;
            this.Usuarios.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.Usuarios.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.Usuarios.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.Usuarios.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.Usuarios.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Usuarios.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.Usuarios.Watermark.Text = "Watermark text";
            this.Usuarios.Watermark.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Gray;
            this.label12.Location = new System.Drawing.Point(20, 210);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(83, 17);
            this.label12.TabIndex = 199;
            this.label12.Text = "Colaborador";
            // 
            // dataFinal
            // 
            this.dataFinal.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(119)))), ((int)(((byte)(118)))));
            this.dataFinal.ArrowDisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(119)))), ((int)(((byte)(118)))));
            this.dataFinal.ArrowSize = new System.Drawing.Size(10, 5);
            this.dataFinal.BackColor = System.Drawing.Color.White;
            this.dataFinal.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataFinal.BackColorState.Enabled = System.Drawing.Color.White;
            this.dataFinal.Border.Color = System.Drawing.Color.Gainsboro;
            this.dataFinal.Border.HoverColor = System.Drawing.Color.Gainsboro;
            this.dataFinal.Border.HoverVisible = true;
            this.dataFinal.Border.Rounding = 6;
            this.dataFinal.Border.Thickness = 1;
            this.dataFinal.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.dataFinal.Border.Visible = true;
            this.dataFinal.DropDownImage = null;
            this.dataFinal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dataFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dataFinal.Image = null;
            this.dataFinal.ImageSize = new System.Drawing.Size(16, 16);
            this.dataFinal.Location = new System.Drawing.Point(48, 375);
            this.dataFinal.MinimumSize = new System.Drawing.Size(0, 25);
            this.dataFinal.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.dataFinal.Name = "dataFinal";
            this.dataFinal.Size = new System.Drawing.Size(173, 25);
            this.dataFinal.TabIndex = 41;
            this.dataFinal.Value = new System.DateTime(2019, 9, 24, 0, 0, 0, 0);
            // 
            // dataInicial
            // 
            this.dataInicial.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(119)))), ((int)(((byte)(118)))));
            this.dataInicial.ArrowDisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(119)))), ((int)(((byte)(118)))));
            this.dataInicial.ArrowSize = new System.Drawing.Size(10, 5);
            this.dataInicial.BackColor = System.Drawing.Color.White;
            this.dataInicial.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataInicial.BackColorState.Enabled = System.Drawing.Color.White;
            this.dataInicial.Border.Color = System.Drawing.Color.Gainsboro;
            this.dataInicial.Border.HoverColor = System.Drawing.Color.Gainsboro;
            this.dataInicial.Border.HoverVisible = true;
            this.dataInicial.Border.Rounding = 6;
            this.dataInicial.Border.Thickness = 1;
            this.dataInicial.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.dataInicial.Border.Visible = true;
            this.dataInicial.DropDownImage = null;
            this.dataInicial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dataInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dataInicial.Image = null;
            this.dataInicial.ImageSize = new System.Drawing.Size(16, 16);
            this.dataInicial.Location = new System.Drawing.Point(48, 345);
            this.dataInicial.MinimumSize = new System.Drawing.Size(0, 25);
            this.dataInicial.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.dataInicial.Name = "dataInicial";
            this.dataInicial.Size = new System.Drawing.Size(173, 25);
            this.dataInicial.TabIndex = 40;
            this.dataInicial.Value = new System.DateTime(2019, 9, 8, 0, 0, 0, 0);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSearch.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(160)))), ((int)(((byte)(220)))));
            this.btnSearch.BackColorState.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(135)))), ((int)(((byte)(194)))));
            this.btnSearch.BackColorState.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(135)))), ((int)(((byte)(194)))));
            this.btnSearch.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(160)))), ((int)(((byte)(220)))));
            this.btnSearch.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(135)))), ((int)(((byte)(194)))));
            this.btnSearch.Border.HoverVisible = true;
            this.btnSearch.Border.Rounding = 6;
            this.btnSearch.Border.Thickness = 1;
            this.btnSearch.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.btnSearch.Border.Visible = true;
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Image = null;
            this.btnSearch.Location = new System.Drawing.Point(48, 534);
            this.btnSearch.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(140, 45);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Pesquisar";
            this.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.btnSearch.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.btnSearch.TextStyle.Enabled = System.Drawing.Color.White;
            this.btnSearch.TextStyle.Hover = System.Drawing.Color.White;
            this.btnSearch.TextStyle.Pressed = System.Drawing.Color.White;
            this.btnSearch.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btnSearch.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.btnSearch.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // BuscarPessoa
            // 
            this.BuscarPessoa.AlphaNumeric = false;
            this.BuscarPessoa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BuscarPessoa.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.BuscarPessoa.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.BuscarPessoa.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.BuscarPessoa.BackColorState.Enabled = System.Drawing.Color.White;
            this.BuscarPessoa.Border.Color = System.Drawing.Color.Gainsboro;
            this.BuscarPessoa.Border.HoverColor = System.Drawing.Color.Gainsboro;
            this.BuscarPessoa.Border.HoverVisible = true;
            this.BuscarPessoa.Border.Rounding = 8;
            this.BuscarPessoa.Border.Thickness = 1;
            this.BuscarPessoa.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.BuscarPessoa.Border.Visible = true;
            this.BuscarPessoa.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.BuscarPessoa.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.BuscarPessoa.ButtonBorder.HoverVisible = true;
            this.BuscarPessoa.ButtonBorder.Rounding = 6;
            this.BuscarPessoa.ButtonBorder.Thickness = 1;
            this.BuscarPessoa.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.BuscarPessoa.ButtonBorder.Visible = true;
            this.BuscarPessoa.ButtonColor.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BuscarPessoa.ButtonColor.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.BuscarPessoa.ButtonColor.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BuscarPessoa.ButtonColor.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.BuscarPessoa.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BuscarPessoa.ButtonIndent = 3;
            this.BuscarPessoa.ButtonText = "visualButton";
            this.BuscarPessoa.ButtonVisible = false;
            this.BuscarPessoa.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BuscarPessoa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BuscarPessoa.Image = ((System.Drawing.Image)(resources.GetObject("BuscarPessoa.Image")));
            this.BuscarPessoa.ImageSize = new System.Drawing.Size(16, 16);
            this.BuscarPessoa.ImageVisible = true;
            this.BuscarPessoa.ImageWidth = 35;
            this.BuscarPessoa.Location = new System.Drawing.Point(21, 429);
            this.BuscarPessoa.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.BuscarPessoa.Name = "BuscarPessoa";
            this.BuscarPessoa.PasswordChar = '\0';
            this.BuscarPessoa.ReadOnly = false;
            this.BuscarPessoa.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.BuscarPessoa.Size = new System.Drawing.Size(200, 34);
            this.BuscarPessoa.TabIndex = 0;
            this.BuscarPessoa.TextBoxWidth = 150;
            this.BuscarPessoa.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.BuscarPessoa.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BuscarPessoa.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BuscarPessoa.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BuscarPessoa.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.BuscarPessoa.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.BuscarPessoa.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.BuscarPessoa.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.BuscarPessoa.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BuscarPessoa.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.BuscarPessoa.Watermark.Text = "Watermark text";
            this.BuscarPessoa.Watermark.Visible = false;
            this.BuscarPessoa.WordWrap = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Gray;
            this.label11.Location = new System.Drawing.Point(18, 409);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(128, 17);
            this.label11.TabIndex = 26;
            this.label11.Text = "Procurar por cliente";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Gray;
            this.label10.Location = new System.Drawing.Point(28, 378);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(18, 17);
            this.label10.TabIndex = 24;
            this.label10.Text = "à:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Gray;
            this.label9.Location = new System.Drawing.Point(20, 348);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(27, 17);
            this.label9.TabIndex = 21;
            this.label9.Text = "De:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Gray;
            this.label8.Location = new System.Drawing.Point(18, 323);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 17);
            this.label8.TabIndex = 190;
            this.label8.Text = "Período";
            // 
            // filterTodos
            // 
            this.filterTodos.AutoSize = true;
            this.filterTodos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.filterTodos.Location = new System.Drawing.Point(21, 154);
            this.filterTodos.Name = "filterTodos";
            this.filterTodos.Size = new System.Drawing.Size(56, 19);
            this.filterTodos.TabIndex = 14;
            this.filterTodos.TabStop = true;
            this.filterTodos.Text = "Todos";
            this.filterTodos.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Gray;
            this.label7.Location = new System.Drawing.Point(50, 128);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(129, 17);
            this.label7.TabIndex = 130;
            this.label7.Text = "Opções de Pesquisa";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel3.Location = new System.Drawing.Point(21, 119);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 1);
            this.panel3.TabIndex = 12;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.pictureBox2);
            this.panel4.Controls.Add(this.pictureBox1);
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1007, 40);
            this.panel4.TabIndex = 79;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(9, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "Você está em:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(227, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 50;
            this.label3.Text = "Pedidos";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(133, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 17);
            this.label4.TabIndex = 30;
            this.label4.Text = "Comercial";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(109, 11);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(20, 20);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(205, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 20);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // Status
            // 
            this.Status.BackColor = System.Drawing.Color.White;
            this.Status.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.Status.BackColorState.Enabled = System.Drawing.Color.White;
            this.Status.Border.Color = System.Drawing.Color.Gainsboro;
            this.Status.Border.HoverColor = System.Drawing.Color.Gainsboro;
            this.Status.Border.HoverVisible = true;
            this.Status.Border.Rounding = 6;
            this.Status.Border.Thickness = 1;
            this.Status.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.Status.Border.Visible = true;
            this.Status.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(119)))), ((int)(((byte)(118)))));
            this.Status.ButtonImage = null;
            this.Status.ButtonStyle = VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox.ButtonStyles.Arrow;
            this.Status.ButtonWidth = 30;
            this.Status.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.Status.DropDownHeight = 100;
            this.Status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Status.DropDownWidth = 250;
            this.Status.FormattingEnabled = true;
            this.Status.ImageList = null;
            this.Status.ImageVisible = false;
            this.Status.Index = 0;
            this.Status.IntegralHeight = false;
            this.Status.ItemHeight = 23;
            this.Status.ItemImageVisible = true;
            this.Status.Location = new System.Drawing.Point(21, 284);
            this.Status.MenuItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Status.MenuItemNormal = System.Drawing.Color.White;
            this.Status.MenuTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Status.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.Status.Name = "Status";
            this.Status.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Status.Size = new System.Drawing.Size(199, 29);
            this.Status.State = VisualPlus.Enumerators.MouseStates.Normal;
            this.Status.TabIndex = 205;
            this.Status.TextAlignment = System.Drawing.StringAlignment.Center;
            this.Status.TextDisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.Status.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Status.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.Status.TextRendering = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.Status.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.Status.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Status.TextStyle.Hover = System.Drawing.Color.Empty;
            this.Status.TextStyle.Pressed = System.Drawing.Color.Empty;
            this.Status.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.Status.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.Status.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.Status.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.Status.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Status.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.Status.Watermark.Text = "Watermark text";
            this.Status.Watermark.Visible = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Gray;
            this.label13.Location = new System.Drawing.Point(20, 264);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(46, 17);
            this.label13.TabIndex = 204;
            this.label13.Text = "Status";
            // 
            // Pedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.barraTitulo);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1024, 768);
            this.Name = "Pedido";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.panel5.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Loading)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridLista)).EndInit();
            this.barraTitulo.ResumeLayout(false);
            this.barraTitulo.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView GridLista;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel barraTitulo;
        private System.Windows.Forms.RadioButton filterTodos;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnAdicionar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private VisualPlus.Toolkit.Controls.Editors.VisualTextBox BuscarPessoa;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualButton btnSearch;
        private VisualPlus.Toolkit.Controls.Editors.VisualDateTimePicker dataFinal;
        private VisualPlus.Toolkit.Controls.Editors.VisualDateTimePicker dataInicial;
        private System.Windows.Forms.PictureBox Loading;
        private System.Windows.Forms.Button imprimir;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox Usuarios;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RadioButton filterRemovido;
        private VisualPlus.Toolkit.Controls.Editors.VisualTextBox BuscaID;
        private System.Windows.Forms.Label label6;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox Status;
        private System.Windows.Forms.Label label13;
    }
}