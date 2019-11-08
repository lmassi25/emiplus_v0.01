namespace Emiplus.View.Reports
{
    partial class Inventario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inventario));
            this.Loading = new System.Windows.Forms.PictureBox();
            this.imprimir = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.GridLista = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Categorias = new VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.Fornecedor = new VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSearch = new VisualPlus.Toolkit.Controls.Interactivity.VisualButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.barraTitulo = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.Loading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridLista)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.barraTitulo.SuspendLayout();
            this.SuspendLayout();
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
            this.imprimir.Location = new System.Drawing.Point(867, 3);
            this.imprimir.Name = "imprimir";
            this.imprimir.Size = new System.Drawing.Size(116, 90);
            this.imprimir.TabIndex = 101;
            this.imprimir.Text = "Gerar Relatório";
            this.imprimir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.imprimir.UseVisualStyleBackColor = true;
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
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(109, 11);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(20, 20);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 51;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(201, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 20);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Light", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(249, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 40);
            this.label1.TabIndex = 181;
            this.label1.Text = "Estoque";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label13.Location = new System.Drawing.Point(182, 214);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(345, 21);
            this.label13.TabIndex = 177;
            this.label13.Text = "Clique no botão pesquisar para buscar os dados.";
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
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.Loading);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.GridLista);
            this.panel2.Location = new System.Drawing.Point(255, 135);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(717, 478);
            this.panel2.TabIndex = 178;
            // 
            // Categorias
            // 
            this.Categorias.BackColor = System.Drawing.Color.White;
            this.Categorias.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.Categorias.BackColorState.Enabled = System.Drawing.Color.White;
            this.Categorias.Border.Color = System.Drawing.Color.Gainsboro;
            this.Categorias.Border.HoverColor = System.Drawing.Color.Gainsboro;
            this.Categorias.Border.HoverVisible = true;
            this.Categorias.Border.Rounding = 6;
            this.Categorias.Border.Thickness = 1;
            this.Categorias.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.Categorias.Border.Visible = true;
            this.Categorias.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(119)))), ((int)(((byte)(118)))));
            this.Categorias.ButtonImage = null;
            this.Categorias.ButtonStyle = VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox.ButtonStyles.Arrow;
            this.Categorias.ButtonWidth = 30;
            this.Categorias.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.Categorias.DropDownHeight = 100;
            this.Categorias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Categorias.DropDownWidth = 250;
            this.Categorias.FormattingEnabled = true;
            this.Categorias.ImageList = null;
            this.Categorias.ImageVisible = false;
            this.Categorias.Index = 0;
            this.Categorias.IntegralHeight = false;
            this.Categorias.ItemHeight = 23;
            this.Categorias.ItemImageVisible = true;
            this.Categorias.Location = new System.Drawing.Point(101, 207);
            this.Categorias.MenuItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Categorias.MenuItemNormal = System.Drawing.Color.White;
            this.Categorias.MenuTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Categorias.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.Categorias.Name = "Categorias";
            this.Categorias.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Categorias.Size = new System.Drawing.Size(120, 29);
            this.Categorias.State = VisualPlus.Enumerators.MouseStates.Normal;
            this.Categorias.TabIndex = 197;
            this.Categorias.TextAlignment = System.Drawing.StringAlignment.Center;
            this.Categorias.TextDisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.Categorias.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Categorias.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.Categorias.TextRendering = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.Categorias.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.Categorias.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Categorias.TextStyle.Hover = System.Drawing.Color.Empty;
            this.Categorias.TextStyle.Pressed = System.Drawing.Color.Empty;
            this.Categorias.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.Categorias.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.Categorias.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.Categorias.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.Categorias.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Categorias.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.Categorias.Watermark.Text = "Watermark text";
            this.Categorias.Watermark.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Gray;
            this.label12.Location = new System.Drawing.Point(29, 211);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(66, 17);
            this.label12.TabIndex = 196;
            this.label12.Text = "Categoria";
            // 
            // Fornecedor
            // 
            this.Fornecedor.BackColor = System.Drawing.Color.White;
            this.Fornecedor.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.Fornecedor.BackColorState.Enabled = System.Drawing.Color.White;
            this.Fornecedor.Border.Color = System.Drawing.Color.Gainsboro;
            this.Fornecedor.Border.HoverColor = System.Drawing.Color.Gainsboro;
            this.Fornecedor.Border.HoverVisible = true;
            this.Fornecedor.Border.Rounding = 6;
            this.Fornecedor.Border.Thickness = 1;
            this.Fornecedor.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.Fornecedor.Border.Visible = true;
            this.Fornecedor.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(119)))), ((int)(((byte)(118)))));
            this.Fornecedor.ButtonImage = null;
            this.Fornecedor.ButtonStyle = VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox.ButtonStyles.Arrow;
            this.Fornecedor.ButtonWidth = 30;
            this.Fornecedor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.Fornecedor.DropDownHeight = 100;
            this.Fornecedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Fornecedor.DropDownWidth = 250;
            this.Fornecedor.FormattingEnabled = true;
            this.Fornecedor.ImageList = null;
            this.Fornecedor.ImageVisible = false;
            this.Fornecedor.Index = 0;
            this.Fornecedor.IntegralHeight = false;
            this.Fornecedor.ItemHeight = 23;
            this.Fornecedor.ItemImageVisible = true;
            this.Fornecedor.Location = new System.Drawing.Point(101, 166);
            this.Fornecedor.MenuItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Fornecedor.MenuItemNormal = System.Drawing.Color.White;
            this.Fornecedor.MenuTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Fornecedor.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.Fornecedor.Name = "Fornecedor";
            this.Fornecedor.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Fornecedor.Size = new System.Drawing.Size(120, 29);
            this.Fornecedor.State = VisualPlus.Enumerators.MouseStates.Normal;
            this.Fornecedor.TabIndex = 194;
            this.Fornecedor.TextAlignment = System.Drawing.StringAlignment.Center;
            this.Fornecedor.TextDisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.Fornecedor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Fornecedor.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.Fornecedor.TextRendering = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.Fornecedor.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.Fornecedor.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Fornecedor.TextStyle.Hover = System.Drawing.Color.Empty;
            this.Fornecedor.TextStyle.Pressed = System.Drawing.Color.Empty;
            this.Fornecedor.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.Fornecedor.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.Fornecedor.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.Fornecedor.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.Fornecedor.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Fornecedor.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.Fornecedor.Watermark.Text = "Watermark text";
            this.Fornecedor.Watermark.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(18, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 17);
            this.label4.TabIndex = 193;
            this.label4.Text = "Fornecedor";
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
            this.label3.Location = new System.Drawing.Point(223, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 17);
            this.label3.TabIndex = 50;
            this.label3.Text = "Estoque";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.pictureBox4);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.pictureBox1);
            this.panel4.Location = new System.Drawing.Point(1, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1007, 40);
            this.panel4.TabIndex = 180;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label6.Location = new System.Drawing.Point(133, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 17);
            this.label6.TabIndex = 52;
            this.label6.Text = "Produtos";
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
            this.btnSearch.Location = new System.Drawing.Point(39, 258);
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(257, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(232, 15);
            this.label2.TabIndex = 182;
            this.label2.Text = "Consulte o estoque do seus produtos aqui.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Gray;
            this.label7.Location = new System.Drawing.Point(50, 134);
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
            this.panel5.Controls.Add(this.btnHelp);
            this.panel5.Location = new System.Drawing.Point(0, 632);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1008, 97);
            this.panel5.TabIndex = 179;
            // 
            // barraTitulo
            // 
            this.barraTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.barraTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.barraTitulo.Controls.Add(this.Categorias);
            this.barraTitulo.Controls.Add(this.label12);
            this.barraTitulo.Controls.Add(this.Fornecedor);
            this.barraTitulo.Controls.Add(this.label4);
            this.barraTitulo.Controls.Add(this.btnSearch);
            this.barraTitulo.Controls.Add(this.label7);
            this.barraTitulo.Controls.Add(this.panel3);
            this.barraTitulo.Controls.Add(this.btnExit);
            this.barraTitulo.Location = new System.Drawing.Point(0, 41);
            this.barraTitulo.Name = "barraTitulo";
            this.barraTitulo.Size = new System.Drawing.Size(239, 596);
            this.barraTitulo.TabIndex = 177;
            // 
            // Inventario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.barraTitulo);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Inventario";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.Loading)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridLista)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.barraTitulo.ResumeLayout(false);
            this.barraTitulo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Loading;
        private System.Windows.Forms.Button imprimir;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DataGridView GridLista;
        private System.Windows.Forms.Panel panel2;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox Categorias;
        private System.Windows.Forms.Label label12;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox Fornecedor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label6;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualButton btnSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel barraTitulo;
    }
}