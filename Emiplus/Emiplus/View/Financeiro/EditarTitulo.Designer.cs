namespace Emiplus.View.Financeiro
{
    partial class EditarTitulo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditarTitulo));
            this.pictureBox12 = new System.Windows.Forms.PictureBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.emissao = new VisualPlus.Toolkit.Controls.Editors.VisualTextBox();
            this.visualGroupBox1 = new VisualPlus.Toolkit.Controls.Layout.VisualGroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.receita = new VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox();
            this.cliente = new VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.total = new VisualPlus.Toolkit.Controls.Editors.VisualTextBox();
            this.formaPgto = new VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.vencimento = new VisualPlus.Toolkit.Controls.Editors.VisualTextBox();
            this.barraTitulo = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnRemover = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dataRecebido = new VisualPlus.Toolkit.Controls.Editors.VisualTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.recebido = new VisualPlus.Toolkit.Controls.Editors.VisualTextBox();
            this.visualGroupBox2 = new VisualPlus.Toolkit.Controls.Layout.VisualGroupBox();
            this.btnImprimir = new VisualPlus.Toolkit.Controls.Interactivity.VisualButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).BeginInit();
            this.visualGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.barraTitulo.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.visualGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox12
            // 
            this.pictureBox12.BackColor = System.Drawing.Color.White;
            this.pictureBox12.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox12.Image")));
            this.pictureBox12.Location = new System.Drawing.Point(573, 31);
            this.pictureBox12.Name = "pictureBox12";
            this.pictureBox12.Size = new System.Drawing.Size(15, 15);
            this.pictureBox12.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox12.TabIndex = 37;
            this.pictureBox12.TabStop = false;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.White;
            this.label21.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label21.Location = new System.Drawing.Point(516, 30);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(57, 17);
            this.label21.TabIndex = 28;
            this.label21.Text = "Emissão";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.White;
            this.label23.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label23.Location = new System.Drawing.Point(16, 30);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(75, 17);
            this.label23.TabIndex = 24;
            this.label23.Text = "Receber de";
            // 
            // emissao
            // 
            this.emissao.AlphaNumeric = false;
            this.emissao.BackColor = System.Drawing.Color.White;
            this.emissao.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.emissao.BackColorState.Enabled = System.Drawing.Color.White;
            this.emissao.Border.Color = System.Drawing.Color.Gainsboro;
            this.emissao.Border.HoverColor = System.Drawing.Color.Gainsboro;
            this.emissao.Border.HoverVisible = true;
            this.emissao.Border.Rounding = 8;
            this.emissao.Border.Thickness = 1;
            this.emissao.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.emissao.Border.Visible = true;
            this.emissao.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.emissao.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.emissao.ButtonBorder.HoverVisible = true;
            this.emissao.ButtonBorder.Rounding = 6;
            this.emissao.ButtonBorder.Thickness = 1;
            this.emissao.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.emissao.ButtonBorder.Visible = true;
            this.emissao.ButtonColor.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.emissao.ButtonColor.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.emissao.ButtonColor.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.emissao.ButtonColor.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.emissao.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emissao.ButtonIndent = 3;
            this.emissao.ButtonText = "visualButton";
            this.emissao.ButtonVisible = false;
            this.emissao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emissao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.emissao.Image = null;
            this.emissao.ImageSize = new System.Drawing.Size(16, 16);
            this.emissao.ImageVisible = false;
            this.emissao.ImageWidth = 35;
            this.emissao.Location = new System.Drawing.Point(519, 49);
            this.emissao.MaxLength = 10;
            this.emissao.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.emissao.Name = "emissao";
            this.emissao.PasswordChar = '\0';
            this.emissao.ReadOnly = false;
            this.emissao.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.emissao.Size = new System.Drawing.Size(112, 28);
            this.emissao.TabIndex = 2;
            this.emissao.TextBoxWidth = 100;
            this.emissao.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.emissao.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.emissao.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.emissao.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.emissao.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.emissao.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.emissao.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.emissao.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.emissao.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emissao.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.emissao.Watermark.Text = "Watermark text";
            this.emissao.Watermark.Visible = false;
            this.emissao.WordWrap = true;
            // 
            // visualGroupBox1
            // 
            this.visualGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.visualGroupBox1.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.visualGroupBox1.BackColorState.Enabled = System.Drawing.Color.White;
            this.visualGroupBox1.Border.Color = System.Drawing.Color.Gainsboro;
            this.visualGroupBox1.Border.HoverColor = System.Drawing.Color.Gainsboro;
            this.visualGroupBox1.Border.HoverVisible = true;
            this.visualGroupBox1.Border.Rounding = 6;
            this.visualGroupBox1.Border.Thickness = 1;
            this.visualGroupBox1.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.visualGroupBox1.Border.Visible = true;
            this.visualGroupBox1.BoxStyle = VisualPlus.Toolkit.Controls.Layout.VisualGroupBox.GroupBoxStyle.Classic;
            this.visualGroupBox1.Controls.Add(this.label8);
            this.visualGroupBox1.Controls.Add(this.emissao);
            this.visualGroupBox1.Controls.Add(this.receita);
            this.visualGroupBox1.Controls.Add(this.cliente);
            this.visualGroupBox1.Controls.Add(this.label7);
            this.visualGroupBox1.Controls.Add(this.total);
            this.visualGroupBox1.Controls.Add(this.formaPgto);
            this.visualGroupBox1.Controls.Add(this.label3);
            this.visualGroupBox1.Controls.Add(this.pictureBox4);
            this.visualGroupBox1.Controls.Add(this.label2);
            this.visualGroupBox1.Controls.Add(this.vencimento);
            this.visualGroupBox1.Controls.Add(this.pictureBox12);
            this.visualGroupBox1.Controls.Add(this.label21);
            this.visualGroupBox1.Controls.Add(this.label23);
            this.visualGroupBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.visualGroupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.visualGroupBox1.Image = null;
            this.visualGroupBox1.Location = new System.Drawing.Point(40, 144);
            this.visualGroupBox1.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.visualGroupBox1.Name = "visualGroupBox1";
            this.visualGroupBox1.Padding = new System.Windows.Forms.Padding(5, 26, 5, 5);
            this.visualGroupBox1.Separator = false;
            this.visualGroupBox1.SeparatorColor = System.Drawing.Color.White;
            this.visualGroupBox1.Size = new System.Drawing.Size(652, 147);
            this.visualGroupBox1.TabIndex = 86;
            this.visualGroupBox1.Text = "Informações";
            this.visualGroupBox1.TextAlignment = System.Drawing.StringAlignment.Center;
            this.visualGroupBox1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.visualGroupBox1.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.visualGroupBox1.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.visualGroupBox1.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualGroupBox1.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualGroupBox1.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualGroupBox1.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.visualGroupBox1.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.visualGroupBox1.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.visualGroupBox1.TitleBoxHeight = 25;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label8.Location = new System.Drawing.Point(17, 86);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 17);
            this.label8.TabIndex = 98;
            this.label8.Text = "Receita";
            // 
            // receita
            // 
            this.receita.BackColor = System.Drawing.Color.White;
            this.receita.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.receita.BackColorState.Enabled = System.Drawing.Color.White;
            this.receita.Border.Color = System.Drawing.Color.Gainsboro;
            this.receita.Border.HoverColor = System.Drawing.Color.Gainsboro;
            this.receita.Border.HoverVisible = true;
            this.receita.Border.Rounding = 6;
            this.receita.Border.Thickness = 1;
            this.receita.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.receita.Border.Visible = true;
            this.receita.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(119)))), ((int)(((byte)(118)))));
            this.receita.ButtonImage = null;
            this.receita.ButtonStyle = VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox.ButtonStyles.Arrow;
            this.receita.ButtonWidth = 30;
            this.receita.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.receita.DropDownHeight = 100;
            this.receita.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.receita.DropDownWidth = 250;
            this.receita.FormattingEnabled = true;
            this.receita.ImageList = null;
            this.receita.ImageVisible = false;
            this.receita.Index = 0;
            this.receita.IntegralHeight = false;
            this.receita.ItemHeight = 23;
            this.receita.ItemImageVisible = true;
            this.receita.Location = new System.Drawing.Point(19, 105);
            this.receita.MenuItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.receita.MenuItemNormal = System.Drawing.Color.White;
            this.receita.MenuTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.receita.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.receita.Name = "receita";
            this.receita.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.receita.Size = new System.Drawing.Size(360, 29);
            this.receita.State = VisualPlus.Enumerators.MouseStates.Normal;
            this.receita.TabIndex = 3;
            this.receita.TextAlignment = System.Drawing.StringAlignment.Center;
            this.receita.TextDisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.receita.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.receita.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.receita.TextRendering = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.receita.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.receita.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.receita.TextStyle.Hover = System.Drawing.Color.Empty;
            this.receita.TextStyle.Pressed = System.Drawing.Color.Empty;
            this.receita.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.receita.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.receita.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.receita.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.receita.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.receita.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.receita.Watermark.Text = "Watermark text";
            this.receita.Watermark.Visible = false;
            // 
            // cliente
            // 
            this.cliente.BackColor = System.Drawing.Color.White;
            this.cliente.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.cliente.BackColorState.Enabled = System.Drawing.Color.White;
            this.cliente.Border.Color = System.Drawing.Color.Gainsboro;
            this.cliente.Border.HoverColor = System.Drawing.Color.Gainsboro;
            this.cliente.Border.HoverVisible = true;
            this.cliente.Border.Rounding = 6;
            this.cliente.Border.Thickness = 1;
            this.cliente.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.cliente.Border.Visible = true;
            this.cliente.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(119)))), ((int)(((byte)(118)))));
            this.cliente.ButtonImage = null;
            this.cliente.ButtonStyle = VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox.ButtonStyles.Arrow;
            this.cliente.ButtonWidth = 30;
            this.cliente.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cliente.DropDownHeight = 100;
            this.cliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cliente.DropDownWidth = 250;
            this.cliente.FormattingEnabled = true;
            this.cliente.ImageList = null;
            this.cliente.ImageVisible = false;
            this.cliente.Index = 0;
            this.cliente.IntegralHeight = false;
            this.cliente.ItemHeight = 23;
            this.cliente.ItemImageVisible = true;
            this.cliente.Location = new System.Drawing.Point(19, 49);
            this.cliente.MenuItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cliente.MenuItemNormal = System.Drawing.Color.White;
            this.cliente.MenuTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cliente.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.cliente.Name = "cliente";
            this.cliente.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cliente.Size = new System.Drawing.Size(360, 29);
            this.cliente.State = VisualPlus.Enumerators.MouseStates.Normal;
            this.cliente.TabIndex = 0;
            this.cliente.TextAlignment = System.Drawing.StringAlignment.Center;
            this.cliente.TextDisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.cliente.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cliente.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.cliente.TextRendering = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.cliente.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.cliente.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cliente.TextStyle.Hover = System.Drawing.Color.Empty;
            this.cliente.TextStyle.Pressed = System.Drawing.Color.Empty;
            this.cliente.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.cliente.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.cliente.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.cliente.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.cliente.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cliente.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.cliente.Watermark.Text = "Watermark text";
            this.cliente.Watermark.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label7.Location = new System.Drawing.Point(516, 86);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 17);
            this.label7.TabIndex = 95;
            this.label7.Text = "Total";
            // 
            // total
            // 
            this.total.AlphaNumeric = false;
            this.total.BackColor = System.Drawing.Color.White;
            this.total.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.total.BackColorState.Enabled = System.Drawing.Color.White;
            this.total.Border.Color = System.Drawing.Color.Gainsboro;
            this.total.Border.HoverColor = System.Drawing.Color.Gainsboro;
            this.total.Border.HoverVisible = true;
            this.total.Border.Rounding = 8;
            this.total.Border.Thickness = 1;
            this.total.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.total.Border.Visible = true;
            this.total.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.total.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.total.ButtonBorder.HoverVisible = true;
            this.total.ButtonBorder.Rounding = 6;
            this.total.ButtonBorder.Thickness = 1;
            this.total.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.total.ButtonBorder.Visible = true;
            this.total.ButtonColor.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.total.ButtonColor.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.total.ButtonColor.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.total.ButtonColor.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.total.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.total.ButtonIndent = 3;
            this.total.ButtonText = "visualButton";
            this.total.ButtonVisible = false;
            this.total.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.total.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.total.Image = null;
            this.total.ImageSize = new System.Drawing.Size(16, 16);
            this.total.ImageVisible = false;
            this.total.ImageWidth = 35;
            this.total.Location = new System.Drawing.Point(519, 105);
            this.total.MaxLength = 10;
            this.total.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.total.Name = "total";
            this.total.PasswordChar = '\0';
            this.total.ReadOnly = false;
            this.total.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.total.Size = new System.Drawing.Size(112, 28);
            this.total.TabIndex = 5;
            this.total.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.total.TextBoxWidth = 100;
            this.total.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.total.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.total.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.total.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.total.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.total.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.total.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.total.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.total.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.total.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.total.Watermark.Text = "Watermark text";
            this.total.Watermark.Visible = false;
            this.total.WordWrap = true;
            // 
            // formaPgto
            // 
            this.formaPgto.BackColor = System.Drawing.Color.White;
            this.formaPgto.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.formaPgto.BackColorState.Enabled = System.Drawing.Color.White;
            this.formaPgto.Border.Color = System.Drawing.Color.Gainsboro;
            this.formaPgto.Border.HoverColor = System.Drawing.Color.Gainsboro;
            this.formaPgto.Border.HoverVisible = true;
            this.formaPgto.Border.Rounding = 6;
            this.formaPgto.Border.Thickness = 1;
            this.formaPgto.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.formaPgto.Border.Visible = true;
            this.formaPgto.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(119)))), ((int)(((byte)(118)))));
            this.formaPgto.ButtonImage = null;
            this.formaPgto.ButtonStyle = VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox.ButtonStyles.Arrow;
            this.formaPgto.ButtonWidth = 30;
            this.formaPgto.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.formaPgto.DropDownHeight = 100;
            this.formaPgto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.formaPgto.DropDownWidth = 150;
            this.formaPgto.FormattingEnabled = true;
            this.formaPgto.ImageList = null;
            this.formaPgto.ImageVisible = false;
            this.formaPgto.Index = 0;
            this.formaPgto.IntegralHeight = false;
            this.formaPgto.ItemHeight = 23;
            this.formaPgto.ItemImageVisible = true;
            this.formaPgto.Location = new System.Drawing.Point(393, 105);
            this.formaPgto.MenuItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.formaPgto.MenuItemNormal = System.Drawing.Color.White;
            this.formaPgto.MenuTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.formaPgto.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.formaPgto.Name = "formaPgto";
            this.formaPgto.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.formaPgto.Size = new System.Drawing.Size(112, 29);
            this.formaPgto.State = VisualPlus.Enumerators.MouseStates.Normal;
            this.formaPgto.TabIndex = 4;
            this.formaPgto.TextAlignment = System.Drawing.StringAlignment.Center;
            this.formaPgto.TextDisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.formaPgto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.formaPgto.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.formaPgto.TextRendering = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.formaPgto.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.formaPgto.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.formaPgto.TextStyle.Hover = System.Drawing.Color.Empty;
            this.formaPgto.TextStyle.Pressed = System.Drawing.Color.Empty;
            this.formaPgto.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.formaPgto.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.formaPgto.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.formaPgto.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.formaPgto.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.formaPgto.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.formaPgto.Watermark.Text = "Watermark text";
            this.formaPgto.Watermark.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(390, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 17);
            this.label3.TabIndex = 92;
            this.label3.Text = "Forma Receber";
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.White;
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(470, 31);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(15, 15);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 90;
            this.pictureBox4.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(390, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 17);
            this.label2.TabIndex = 89;
            this.label2.Text = "Vencimento";
            // 
            // vencimento
            // 
            this.vencimento.AlphaNumeric = false;
            this.vencimento.BackColor = System.Drawing.Color.White;
            this.vencimento.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.vencimento.BackColorState.Enabled = System.Drawing.Color.White;
            this.vencimento.Border.Color = System.Drawing.Color.Gainsboro;
            this.vencimento.Border.HoverColor = System.Drawing.Color.Gainsboro;
            this.vencimento.Border.HoverVisible = true;
            this.vencimento.Border.Rounding = 8;
            this.vencimento.Border.Thickness = 1;
            this.vencimento.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.vencimento.Border.Visible = true;
            this.vencimento.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.vencimento.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.vencimento.ButtonBorder.HoverVisible = true;
            this.vencimento.ButtonBorder.Rounding = 6;
            this.vencimento.ButtonBorder.Thickness = 1;
            this.vencimento.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.vencimento.ButtonBorder.Visible = true;
            this.vencimento.ButtonColor.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.vencimento.ButtonColor.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.vencimento.ButtonColor.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.vencimento.ButtonColor.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.vencimento.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vencimento.ButtonIndent = 3;
            this.vencimento.ButtonText = "visualButton";
            this.vencimento.ButtonVisible = false;
            this.vencimento.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vencimento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.vencimento.Image = null;
            this.vencimento.ImageSize = new System.Drawing.Size(16, 16);
            this.vencimento.ImageVisible = false;
            this.vencimento.ImageWidth = 35;
            this.vencimento.Location = new System.Drawing.Point(393, 49);
            this.vencimento.MaxLength = 10;
            this.vencimento.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.vencimento.Name = "vencimento";
            this.vencimento.PasswordChar = '\0';
            this.vencimento.ReadOnly = false;
            this.vencimento.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.vencimento.Size = new System.Drawing.Size(112, 28);
            this.vencimento.TabIndex = 1;
            this.vencimento.TextBoxWidth = 100;
            this.vencimento.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.vencimento.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.vencimento.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.vencimento.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.vencimento.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.vencimento.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.vencimento.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.vencimento.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.vencimento.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vencimento.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.vencimento.Watermark.Text = "Watermark text";
            this.vencimento.Watermark.Visible = false;
            this.vencimento.WordWrap = true;
            // 
            // barraTitulo
            // 
            this.barraTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.barraTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.barraTitulo.Controls.Add(this.button1);
            this.barraTitulo.Controls.Add(this.btnSalvar);
            this.barraTitulo.Controls.Add(this.btnRemover);
            this.barraTitulo.Location = new System.Drawing.Point(0, 552);
            this.barraTitulo.Name = "barraTitulo";
            this.barraTitulo.Size = new System.Drawing.Size(731, 97);
            this.barraTitulo.TabIndex = 81;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.Location = new System.Drawing.Point(529, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 90);
            this.button1.TabIndex = 5;
            this.button1.Text = "Imprimir";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalvar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSalvar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvar.FlatAppearance.BorderSize = 0;
            this.btnSalvar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSalvar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSalvar.Image = ((System.Drawing.Image)(resources.GetObject("btnSalvar.Image")));
            this.btnSalvar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSalvar.Location = new System.Drawing.Point(631, 3);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(85, 90);
            this.btnSalvar.TabIndex = 4;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSalvar.UseVisualStyleBackColor = true;
            // 
            // btnRemover
            // 
            this.btnRemover.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemover.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnRemover.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRemover.FlatAppearance.BorderSize = 0;
            this.btnRemover.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnRemover.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnRemover.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemover.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemover.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnRemover.Image = ((System.Drawing.Image)(resources.GetObject("btnRemover.Image")));
            this.btnRemover.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRemover.Location = new System.Drawing.Point(28, 3);
            this.btnRemover.Name = "btnRemover";
            this.btnRemover.Size = new System.Drawing.Size(85, 90);
            this.btnRemover.TabIndex = 3;
            this.btnRemover.Text = "Apagar";
            this.btnRemover.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRemover.UseVisualStyleBackColor = true;
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
            this.btnExit.Location = new System.Drawing.Point(28, 4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(85, 90);
            this.btnExit.TabIndex = 11;
            this.btnExit.Text = "Voltar";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExit.UseVisualStyleBackColor = true;
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnHelp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHelp.FlatAppearance.BorderSize = 0;
            this.btnHelp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnHelp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHelp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnHelp.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp.Image")));
            this.btnHelp.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnHelp.Location = new System.Drawing.Point(631, 3);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(85, 90);
            this.btnHelp.TabIndex = 13;
            this.btnHelp.Text = "Ajuda";
            this.btnHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnHelp.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.panel5.Controls.Add(this.btnExit);
            this.panel5.Controls.Add(this.btnHelp);
            this.panel5.Location = new System.Drawing.Point(0, 41);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(731, 97);
            this.panel5.TabIndex = 82;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.pictureBox1);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.pictureBox2);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.pictureBox3);
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(733, 40);
            this.panel4.TabIndex = 87;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(346, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "Editar";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(324, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 20);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(10, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "Você está em:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(134, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "Financeiro";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(110, 11);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(20, 20);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label6.Location = new System.Drawing.Point(227, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 17);
            this.label6.TabIndex = 5;
            this.label6.Text = "Recebimentos";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(205, 11);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(20, 20);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 4;
            this.pictureBox3.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.White;
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label9.Location = new System.Drawing.Point(17, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 17);
            this.label9.TabIndex = 100;
            this.label9.Text = "Data Recebido";
            // 
            // dataRecebido
            // 
            this.dataRecebido.AlphaNumeric = false;
            this.dataRecebido.BackColor = System.Drawing.Color.White;
            this.dataRecebido.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.dataRecebido.BackColorState.Enabled = System.Drawing.Color.White;
            this.dataRecebido.Border.Color = System.Drawing.Color.Gainsboro;
            this.dataRecebido.Border.HoverColor = System.Drawing.Color.Gainsboro;
            this.dataRecebido.Border.HoverVisible = true;
            this.dataRecebido.Border.Rounding = 8;
            this.dataRecebido.Border.Thickness = 1;
            this.dataRecebido.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.dataRecebido.Border.Visible = true;
            this.dataRecebido.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.dataRecebido.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.dataRecebido.ButtonBorder.HoverVisible = true;
            this.dataRecebido.ButtonBorder.Rounding = 6;
            this.dataRecebido.ButtonBorder.Thickness = 1;
            this.dataRecebido.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.dataRecebido.ButtonBorder.Visible = true;
            this.dataRecebido.ButtonColor.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataRecebido.ButtonColor.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.dataRecebido.ButtonColor.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataRecebido.ButtonColor.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dataRecebido.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataRecebido.ButtonIndent = 3;
            this.dataRecebido.ButtonText = "visualButton";
            this.dataRecebido.ButtonVisible = false;
            this.dataRecebido.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataRecebido.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.dataRecebido.Image = null;
            this.dataRecebido.ImageSize = new System.Drawing.Size(16, 16);
            this.dataRecebido.ImageVisible = false;
            this.dataRecebido.ImageWidth = 35;
            this.dataRecebido.Location = new System.Drawing.Point(20, 50);
            this.dataRecebido.MaxLength = 10;
            this.dataRecebido.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.dataRecebido.Name = "dataRecebido";
            this.dataRecebido.PasswordChar = '\0';
            this.dataRecebido.ReadOnly = false;
            this.dataRecebido.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataRecebido.Size = new System.Drawing.Size(182, 28);
            this.dataRecebido.TabIndex = 0;
            this.dataRecebido.TextBoxWidth = 170;
            this.dataRecebido.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.dataRecebido.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.dataRecebido.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.dataRecebido.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.dataRecebido.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.dataRecebido.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.dataRecebido.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.dataRecebido.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataRecebido.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataRecebido.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.dataRecebido.Watermark.Text = "Watermark text";
            this.dataRecebido.Watermark.Visible = false;
            this.dataRecebido.WordWrap = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.White;
            this.label10.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label10.Location = new System.Drawing.Point(205, 30);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(97, 17);
            this.label10.TabIndex = 103;
            this.label10.Text = "Valor Recebido";
            // 
            // recebido
            // 
            this.recebido.AlphaNumeric = false;
            this.recebido.BackColor = System.Drawing.Color.White;
            this.recebido.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.recebido.BackColorState.Enabled = System.Drawing.Color.White;
            this.recebido.Border.Color = System.Drawing.Color.Gainsboro;
            this.recebido.Border.HoverColor = System.Drawing.Color.Gainsboro;
            this.recebido.Border.HoverVisible = true;
            this.recebido.Border.Rounding = 8;
            this.recebido.Border.Thickness = 1;
            this.recebido.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.recebido.Border.Visible = true;
            this.recebido.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.recebido.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.recebido.ButtonBorder.HoverVisible = true;
            this.recebido.ButtonBorder.Rounding = 6;
            this.recebido.ButtonBorder.Thickness = 1;
            this.recebido.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.recebido.ButtonBorder.Visible = true;
            this.recebido.ButtonColor.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.recebido.ButtonColor.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.recebido.ButtonColor.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.recebido.ButtonColor.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.recebido.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.recebido.ButtonIndent = 3;
            this.recebido.ButtonText = "visualButton";
            this.recebido.ButtonVisible = false;
            this.recebido.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.recebido.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.recebido.Image = null;
            this.recebido.ImageSize = new System.Drawing.Size(16, 16);
            this.recebido.ImageVisible = false;
            this.recebido.ImageWidth = 35;
            this.recebido.Location = new System.Drawing.Point(208, 50);
            this.recebido.MaxLength = 10;
            this.recebido.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.recebido.Name = "recebido";
            this.recebido.PasswordChar = '\0';
            this.recebido.ReadOnly = false;
            this.recebido.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.recebido.Size = new System.Drawing.Size(171, 28);
            this.recebido.TabIndex = 1;
            this.recebido.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.recebido.TextBoxWidth = 159;
            this.recebido.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.recebido.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.recebido.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.recebido.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.recebido.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.recebido.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.recebido.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.recebido.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.recebido.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.recebido.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.recebido.Watermark.Text = "Watermark text";
            this.recebido.Watermark.Visible = false;
            this.recebido.WordWrap = true;
            // 
            // visualGroupBox2
            // 
            this.visualGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.visualGroupBox2.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.visualGroupBox2.BackColorState.Enabled = System.Drawing.Color.White;
            this.visualGroupBox2.Border.Color = System.Drawing.Color.Gainsboro;
            this.visualGroupBox2.Border.HoverColor = System.Drawing.Color.Gainsboro;
            this.visualGroupBox2.Border.HoverVisible = true;
            this.visualGroupBox2.Border.Rounding = 6;
            this.visualGroupBox2.Border.Thickness = 1;
            this.visualGroupBox2.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.visualGroupBox2.Border.Visible = true;
            this.visualGroupBox2.BoxStyle = VisualPlus.Toolkit.Controls.Layout.VisualGroupBox.GroupBoxStyle.Classic;
            this.visualGroupBox2.Controls.Add(this.btnImprimir);
            this.visualGroupBox2.Controls.Add(this.label10);
            this.visualGroupBox2.Controls.Add(this.label9);
            this.visualGroupBox2.Controls.Add(this.dataRecebido);
            this.visualGroupBox2.Controls.Add(this.recebido);
            this.visualGroupBox2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.visualGroupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.visualGroupBox2.Image = null;
            this.visualGroupBox2.Location = new System.Drawing.Point(40, 298);
            this.visualGroupBox2.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.visualGroupBox2.Name = "visualGroupBox2";
            this.visualGroupBox2.Padding = new System.Windows.Forms.Padding(5, 26, 5, 5);
            this.visualGroupBox2.Separator = false;
            this.visualGroupBox2.SeparatorColor = System.Drawing.Color.White;
            this.visualGroupBox2.Size = new System.Drawing.Size(652, 101);
            this.visualGroupBox2.TabIndex = 6;
            this.visualGroupBox2.Text = "Recebimento";
            this.visualGroupBox2.TextAlignment = System.Drawing.StringAlignment.Center;
            this.visualGroupBox2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.visualGroupBox2.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.visualGroupBox2.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.visualGroupBox2.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualGroupBox2.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualGroupBox2.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualGroupBox2.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.visualGroupBox2.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.visualGroupBox2.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.visualGroupBox2.TitleBoxHeight = 25;
            // 
            // btnImprimir
            // 
            this.btnImprimir.BackColor = System.Drawing.Color.White;
            this.btnImprimir.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnImprimir.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(160)))), ((int)(((byte)(220)))));
            this.btnImprimir.BackColorState.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(135)))), ((int)(((byte)(194)))));
            this.btnImprimir.BackColorState.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(135)))), ((int)(((byte)(194)))));
            this.btnImprimir.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(135)))), ((int)(((byte)(194)))));
            this.btnImprimir.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(135)))), ((int)(((byte)(194)))));
            this.btnImprimir.Border.HoverVisible = true;
            this.btnImprimir.Border.Rounding = 6;
            this.btnImprimir.Border.Thickness = 1;
            this.btnImprimir.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.btnImprimir.Border.Visible = true;
            this.btnImprimir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimir.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnImprimir.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.ForeColor = System.Drawing.Color.White;
            this.btnImprimir.Image = null;
            this.btnImprimir.Location = new System.Drawing.Point(387, 49);
            this.btnImprimir.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(155, 28);
            this.btnImprimir.TabIndex = 2;
            this.btnImprimir.Text = "Imprimir Comprovante";
            this.btnImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnImprimir.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.btnImprimir.TextStyle.Enabled = System.Drawing.Color.White;
            this.btnImprimir.TextStyle.Hover = System.Drawing.Color.White;
            this.btnImprimir.TextStyle.Pressed = System.Drawing.Color.White;
            this.btnImprimir.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btnImprimir.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.btnImprimir.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.btnImprimir.Visible = false;
            // 
            // EditarTitulo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(733, 649);
            this.Controls.Add(this.visualGroupBox2);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.visualGroupBox1);
            this.Controls.Add(this.barraTitulo);
            this.Controls.Add(this.panel5);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "EditarTitulo";
            this.Text = "EditarTitulo";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).EndInit();
            this.visualGroupBox1.ResumeLayout(false);
            this.visualGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.barraTitulo.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.visualGroupBox2.ResumeLayout(false);
            this.visualGroupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox12;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label23;
        private VisualPlus.Toolkit.Controls.Editors.VisualTextBox emissao;
        private VisualPlus.Toolkit.Controls.Layout.VisualGroupBox visualGroupBox1;
        private System.Windows.Forms.Panel barraTitulo;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnRemover;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label2;
        private VisualPlus.Toolkit.Controls.Editors.VisualTextBox vencimento;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox formaPgto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private VisualPlus.Toolkit.Controls.Editors.VisualTextBox total;
        private System.Windows.Forms.Label label8;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox receita;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox cliente;
        private System.Windows.Forms.Label label10;
        private VisualPlus.Toolkit.Controls.Editors.VisualTextBox recebido;
        private System.Windows.Forms.Label label9;
        private VisualPlus.Toolkit.Controls.Editors.VisualTextBox dataRecebido;
        private VisualPlus.Toolkit.Controls.Layout.VisualGroupBox visualGroupBox2;
        private System.Windows.Forms.Button button1;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualButton btnImprimir;
    }
}