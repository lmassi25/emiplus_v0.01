namespace Emiplus.View.Produtos
{
    partial class ReajusteDeProduto
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReajusteDeProduto));
            this.panel2 = new System.Windows.Forms.Panel();
            this.GridLista = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descricao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Medida = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Categoria = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Fornecedores = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ValorVenda = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Estoque = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.Categorias = new VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.Fornecedor = new VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.BuscarProduto = new VisualPlus.Toolkit.Controls.Editors.VisualTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnSearch = new VisualPlus.Toolkit.Controls.Interactivity.VisualButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btnHelp = new VisualPlus.Toolkit.Controls.Interactivity.VisualButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExit = new VisualPlus.Toolkit.Controls.Interactivity.VisualButton();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridLista)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.GridLista);
            this.panel2.Location = new System.Drawing.Point(37, 236);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(939, 478);
            this.panel2.TabIndex = 184;
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridLista.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GridLista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridLista.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Descricao,
            this.Medida,
            this.Categoria,
            this.Fornecedores,
            this.ValorVenda,
            this.Estoque});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridLista.DefaultCellStyle = dataGridViewCellStyle4;
            this.GridLista.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridLista.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.GridLista.Location = new System.Drawing.Point(0, 0);
            this.GridLista.MultiSelect = false;
            this.GridLista.Name = "GridLista";
            this.GridLista.RowHeadersVisible = false;
            this.GridLista.RowTemplate.Height = 30;
            this.GridLista.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridLista.Size = new System.Drawing.Size(939, 478);
            this.GridLista.TabIndex = 50;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.Visible = false;
            // 
            // Descricao
            // 
            this.Descricao.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Descricao.HeaderText = "Descrição";
            this.Descricao.Name = "Descricao";
            // 
            // Medida
            // 
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Medida.DefaultCellStyle = dataGridViewCellStyle2;
            this.Medida.HeaderText = "Medida";
            this.Medida.Name = "Medida";
            this.Medida.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Medida.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Categoria
            // 
            this.Categoria.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Categoria.DefaultCellStyle = dataGridViewCellStyle3;
            this.Categoria.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.Categoria.HeaderText = "Categoria";
            this.Categoria.Name = "Categoria";
            this.Categoria.Width = 150;
            // 
            // Fornecedores
            // 
            this.Fornecedores.HeaderText = "Fornecedor";
            this.Fornecedores.Name = "Fornecedores";
            // 
            // ValorVenda
            // 
            this.ValorVenda.HeaderText = "Valor Venda";
            this.ValorVenda.Name = "ValorVenda";
            // 
            // Estoque
            // 
            this.Estoque.HeaderText = "Estoque";
            this.Estoque.Name = "Estoque";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(34, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(233, 15);
            this.label2.TabIndex = 188;
            this.label2.Text = "Faça uma edição rápida aos produtos aqui.";
            // 
            // Categorias
            // 
            this.Categorias.BackColor = System.Drawing.Color.White;
            this.Categorias.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.Categorias.BackColorState.Enabled = System.Drawing.Color.White;
            this.Categorias.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(229)))), ((int)(((byte)(236)))));
            this.Categorias.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(171)))), ((int)(((byte)(255)))));
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
            this.Categorias.Location = new System.Drawing.Point(431, 190);
            this.Categorias.MenuItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Categorias.MenuItemNormal = System.Drawing.Color.White;
            this.Categorias.MenuTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Categorias.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.Categorias.Name = "Categorias";
            this.Categorias.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Categorias.Size = new System.Drawing.Size(155, 29);
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
            this.label12.Location = new System.Drawing.Point(428, 171);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 17);
            this.label12.TabIndex = 196;
            this.label12.Text = "Categorias";
            // 
            // Fornecedor
            // 
            this.Fornecedor.BackColor = System.Drawing.Color.White;
            this.Fornecedor.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.Fornecedor.BackColorState.Enabled = System.Drawing.Color.White;
            this.Fornecedor.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(229)))), ((int)(((byte)(236)))));
            this.Fornecedor.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(171)))), ((int)(((byte)(255)))));
            this.Fornecedor.Border.HoverVisible = true;
            this.Fornecedor.Border.Rounding = 6;
            this.Fornecedor.Border.Thickness = 1;
            this.Fornecedor.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.Fornecedor.Border.Visible = true;
            this.Fornecedor.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(119)))), ((int)(((byte)(118)))));
            this.Fornecedor.ButtonImage = null;
            this.Fornecedor.ButtonStyle = VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox.ButtonStyles.Arrow;
            this.Fornecedor.ButtonWidth = 30;
            this.Fornecedor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
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
            this.Fornecedor.Location = new System.Drawing.Point(273, 190);
            this.Fornecedor.MenuItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Fornecedor.MenuItemNormal = System.Drawing.Color.White;
            this.Fornecedor.MenuTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Fornecedor.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.Fornecedor.Name = "Fornecedor";
            this.Fornecedor.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Fornecedor.Size = new System.Drawing.Size(152, 29);
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
            this.label4.Location = new System.Drawing.Point(270, 171);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 17);
            this.label4.TabIndex = 193;
            this.label4.Text = "Fornecedor";
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
            // BuscarProduto
            // 
            this.BuscarProduto.AlphaNumeric = false;
            this.BuscarProduto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.BuscarProduto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.BuscarProduto.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.BuscarProduto.BackColorState.Enabled = System.Drawing.Color.White;
            this.BuscarProduto.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(229)))), ((int)(((byte)(236)))));
            this.BuscarProduto.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(171)))), ((int)(((byte)(255)))));
            this.BuscarProduto.Border.HoverVisible = true;
            this.BuscarProduto.Border.Rounding = 8;
            this.BuscarProduto.Border.Thickness = 1;
            this.BuscarProduto.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.BuscarProduto.Border.Visible = true;
            this.BuscarProduto.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.BuscarProduto.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.BuscarProduto.ButtonBorder.HoverVisible = true;
            this.BuscarProduto.ButtonBorder.Rounding = 6;
            this.BuscarProduto.ButtonBorder.Thickness = 1;
            this.BuscarProduto.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.BuscarProduto.ButtonBorder.Visible = true;
            this.BuscarProduto.ButtonColor.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BuscarProduto.ButtonColor.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.BuscarProduto.ButtonColor.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BuscarProduto.ButtonColor.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.BuscarProduto.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BuscarProduto.ButtonIndent = 3;
            this.BuscarProduto.ButtonText = "visualButton";
            this.BuscarProduto.ButtonVisible = false;
            this.BuscarProduto.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BuscarProduto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BuscarProduto.Image = ((System.Drawing.Image)(resources.GetObject("BuscarProduto.Image")));
            this.BuscarProduto.ImageSize = new System.Drawing.Size(16, 16);
            this.BuscarProduto.ImageVisible = true;
            this.BuscarProduto.ImageWidth = 35;
            this.BuscarProduto.Location = new System.Drawing.Point(37, 191);
            this.BuscarProduto.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.BuscarProduto.Name = "BuscarProduto";
            this.BuscarProduto.PasswordChar = '\0';
            this.BuscarProduto.ReadOnly = false;
            this.BuscarProduto.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.BuscarProduto.Size = new System.Drawing.Size(230, 28);
            this.BuscarProduto.TabIndex = 0;
            this.BuscarProduto.TextBoxWidth = 150;
            this.BuscarProduto.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.BuscarProduto.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BuscarProduto.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BuscarProduto.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BuscarProduto.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.BuscarProduto.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.BuscarProduto.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.BuscarProduto.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.BuscarProduto.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BuscarProduto.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.BuscarProduto.Watermark.Text = "Watermark text";
            this.BuscarProduto.Watermark.Visible = false;
            this.BuscarProduto.WordWrap = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Gray;
            this.label11.Location = new System.Drawing.Point(34, 171);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(114, 17);
            this.label11.TabIndex = 26;
            this.label11.Text = "Procurar produto";
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
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.btnHelp);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.pictureBox4);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.pictureBox1);
            this.panel4.Location = new System.Drawing.Point(1, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1007, 40);
            this.panel4.TabIndex = 186;
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
            this.label3.Size = new System.Drawing.Size(123, 17);
            this.label3.TabIndex = 50;
            this.label3.Text = "Produtos Vendidos";
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
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Image = null;
            this.btnSearch.Location = new System.Drawing.Point(592, 191);
            this.btnSearch.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(95, 28);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Buscar";
            this.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.btnSearch.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.btnSearch.TextStyle.Enabled = System.Drawing.Color.White;
            this.btnSearch.TextStyle.Hover = System.Drawing.Color.White;
            this.btnSearch.TextStyle.Pressed = System.Drawing.Color.White;
            this.btnSearch.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btnSearch.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.btnSearch.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Light", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(30, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(264, 40);
            this.label1.TabIndex = 187;
            this.label1.Text = "Reajuste de Produto";
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelp.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnHelp.BackColorState.Enabled = System.Drawing.Color.White;
            this.btnHelp.BackColorState.Hover = System.Drawing.Color.White;
            this.btnHelp.BackColorState.Pressed = System.Drawing.Color.White;
            this.btnHelp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnHelp.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(229)))), ((int)(((byte)(236)))));
            this.btnHelp.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnHelp.Border.HoverVisible = true;
            this.btnHelp.Border.Rounding = 6;
            this.btnHelp.Border.Thickness = 1;
            this.btnHelp.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.btnHelp.Border.Visible = true;
            this.btnHelp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHelp.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnHelp.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHelp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(93)))), ((int)(((byte)(110)))));
            this.btnHelp.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp.Image")));
            this.btnHelp.Location = new System.Drawing.Point(961, 6);
            this.btnHelp.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(37, 30);
            this.btnHelp.TabIndex = 40056;
            this.btnHelp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnHelp.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.btnHelp.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(93)))), ((int)(((byte)(110)))));
            this.btnHelp.TextStyle.Hover = System.Drawing.Color.White;
            this.btnHelp.TextStyle.Pressed = System.Drawing.Color.White;
            this.btnHelp.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btnHelp.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.btnHelp.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Location = new System.Drawing.Point(1, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1007, 40);
            this.panel1.TabIndex = 198;
            // 
            // btnExit
            // 
            this.btnExit.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnExit.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.btnExit.BackColorState.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnExit.BackColorState.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.btnExit.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnExit.Border.HoverVisible = true;
            this.btnExit.Border.Rounding = 6;
            this.btnExit.Border.Thickness = 1;
            this.btnExit.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.btnExit.Border.Visible = true;
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnExit.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Image = null;
            this.btnExit.Location = new System.Drawing.Point(11, 5);
            this.btnExit.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(96, 30);
            this.btnExit.TabIndex = 40054;
            this.btnExit.Text = "Voltar";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExit.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.btnExit.TextStyle.Enabled = System.Drawing.Color.White;
            this.btnExit.TextStyle.Hover = System.Drawing.Color.White;
            this.btnExit.TextStyle.Pressed = System.Drawing.Color.White;
            this.btnExit.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btnExit.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.btnExit.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // ReajusteDeProduto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.Categorias);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Fornecedor);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BuscarProduto);
            this.Controls.Add(this.label11);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ReajusteDeProduto";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridLista)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView GridLista;
        private System.Windows.Forms.Label label2;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox Categorias;
        private System.Windows.Forms.Label label12;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox Fornecedor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private VisualPlus.Toolkit.Controls.Editors.VisualTextBox BuscarProduto;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualButton btnSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descricao;
        private System.Windows.Forms.DataGridViewComboBoxColumn Medida;
        private System.Windows.Forms.DataGridViewComboBoxColumn Categoria;
        private System.Windows.Forms.DataGridViewComboBoxColumn Fornecedores;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValorVenda;
        private System.Windows.Forms.DataGridViewTextBoxColumn Estoque;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualButton btnHelp;
        private System.Windows.Forms.Panel panel1;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualButton btnExit;
    }
}