namespace Emiplus.View.Produtos
{
    partial class AddEstoque
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddEstoque));
            this.tituloProduto = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.novaQtd = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnSalvar = new VisualPlus.Toolkit.Controls.Interactivity.VisualButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.quantidade = new VisualPlus.Toolkit.Controls.Editors.VisualTextBox();
            this.obs = new VisualPlus.Toolkit.Controls.Editors.VisualTextBox();
            this.btnRadioRemoveItem = new VisualPlus.Toolkit.Controls.Interactivity.VisualRadioButton();
            this.btnRadioAddItem = new VisualPlus.Toolkit.Controls.Interactivity.VisualRadioButton();
            this.visualPanel1 = new VisualPlus.Toolkit.Controls.Layout.VisualPanel();
            this.estoqueAtual = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.visualPanel2 = new VisualPlus.Toolkit.Controls.Layout.VisualPanel();
            this.custoAtual = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.visualPanel1.SuspendLayout();
            this.visualPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tituloProduto
            // 
            this.tituloProduto.AutoSize = true;
            this.tituloProduto.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tituloProduto.Location = new System.Drawing.Point(67, 27);
            this.tituloProduto.Name = "tituloProduto";
            this.tituloProduto.Size = new System.Drawing.Size(96, 20);
            this.tituloProduto.TabIndex = 100;
            this.tituloProduto.Text = "Meu produto";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label8.Location = new System.Drawing.Point(37, 164);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 17);
            this.label8.TabIndex = 9;
            this.label8.Text = "Quantidade:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label10.Location = new System.Drawing.Point(268, 187);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(116, 17);
            this.label10.TabIndex = 120;
            this.label10.Text = "Nova quantidade:";
            // 
            // novaQtd
            // 
            this.novaQtd.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.novaQtd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.novaQtd.Location = new System.Drawing.Point(385, 188);
            this.novaQtd.Name = "novaQtd";
            this.novaQtd.Size = new System.Drawing.Size(73, 17);
            this.novaQtd.TabIndex = 130;
            this.novaQtd.Text = "0";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label14.Location = new System.Drawing.Point(38, 215);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(89, 17);
            this.label14.TabIndex = 18;
            this.label14.Text = "Observações:";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.panel3.Controls.Add(this.btnSalvar);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 349);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(470, 40);
            this.panel3.TabIndex = 19;
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
            this.btnSalvar.Location = new System.Drawing.Point(344, 5);
            this.btnSalvar.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(96, 30);
            this.btnSalvar.TabIndex = 554;
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
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.panel4.Controls.Add(this.pictureBox1);
            this.panel4.Controls.Add(this.tituloProduto);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(470, 68);
            this.panel4.TabIndex = 20;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(39, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 20);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(36, 162);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 15);
            this.label1.TabIndex = 21;
            // 
            // quantidade
            // 
            this.quantidade.AlphaNumeric = false;
            this.quantidade.BackColor = System.Drawing.Color.White;
            this.quantidade.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.quantidade.BackColorState.Enabled = System.Drawing.Color.White;
            this.quantidade.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(229)))), ((int)(((byte)(236)))));
            this.quantidade.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(171)))), ((int)(((byte)(255)))));
            this.quantidade.Border.HoverVisible = true;
            this.quantidade.Border.Rounding = 8;
            this.quantidade.Border.Thickness = 1;
            this.quantidade.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.quantidade.Border.Visible = true;
            this.quantidade.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.quantidade.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.quantidade.ButtonBorder.HoverVisible = true;
            this.quantidade.ButtonBorder.Rounding = 6;
            this.quantidade.ButtonBorder.Thickness = 1;
            this.quantidade.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.quantidade.ButtonBorder.Visible = true;
            this.quantidade.ButtonColor.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.quantidade.ButtonColor.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.quantidade.ButtonColor.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.quantidade.ButtonColor.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.quantidade.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quantidade.ButtonIndent = 3;
            this.quantidade.ButtonText = "visualButton";
            this.quantidade.ButtonVisible = false;
            this.quantidade.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quantidade.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.quantidade.Image = null;
            this.quantidade.ImageSize = new System.Drawing.Size(16, 16);
            this.quantidade.ImageVisible = false;
            this.quantidade.ImageWidth = 35;
            this.quantidade.Location = new System.Drawing.Point(39, 184);
            this.quantidade.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.quantidade.Name = "quantidade";
            this.quantidade.PasswordChar = '\0';
            this.quantidade.ReadOnly = false;
            this.quantidade.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.quantidade.Size = new System.Drawing.Size(223, 28);
            this.quantidade.TabIndex = 2;
            this.quantidade.TextBoxWidth = 211;
            this.quantidade.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.quantidade.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.quantidade.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.quantidade.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.quantidade.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.quantidade.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.quantidade.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.quantidade.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.quantidade.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quantidade.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.quantidade.Watermark.Text = "Watermark text";
            this.quantidade.Watermark.Visible = false;
            this.quantidade.WordWrap = true;
            // 
            // obs
            // 
            this.obs.AlphaNumeric = false;
            this.obs.BackColor = System.Drawing.Color.White;
            this.obs.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.obs.BackColorState.Enabled = System.Drawing.Color.White;
            this.obs.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(229)))), ((int)(((byte)(236)))));
            this.obs.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(171)))), ((int)(((byte)(255)))));
            this.obs.Border.HoverVisible = true;
            this.obs.Border.Rounding = 8;
            this.obs.Border.Thickness = 1;
            this.obs.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.obs.Border.Visible = true;
            this.obs.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.obs.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.obs.ButtonBorder.HoverVisible = true;
            this.obs.ButtonBorder.Rounding = 6;
            this.obs.ButtonBorder.Thickness = 1;
            this.obs.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.obs.ButtonBorder.Visible = true;
            this.obs.ButtonColor.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.obs.ButtonColor.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.obs.ButtonColor.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.obs.ButtonColor.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.obs.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.obs.ButtonIndent = 3;
            this.obs.ButtonText = "visualButton";
            this.obs.ButtonVisible = false;
            this.obs.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.obs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.obs.Image = null;
            this.obs.ImageSize = new System.Drawing.Size(16, 16);
            this.obs.ImageVisible = false;
            this.obs.ImageWidth = 35;
            this.obs.Location = new System.Drawing.Point(40, 235);
            this.obs.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.obs.MultiLine = true;
            this.obs.Name = "obs";
            this.obs.PasswordChar = '\0';
            this.obs.ReadOnly = false;
            this.obs.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.obs.Size = new System.Drawing.Size(400, 69);
            this.obs.TabIndex = 3;
            this.obs.TextBoxWidth = 388;
            this.obs.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.obs.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.obs.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.obs.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.obs.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.obs.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.obs.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.obs.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.obs.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.obs.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.obs.Watermark.Text = "Watermark text";
            this.obs.Watermark.Visible = false;
            this.obs.WordWrap = true;
            // 
            // btnRadioRemoveItem
            // 
            this.btnRadioRemoveItem.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.btnRadioRemoveItem.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.btnRadioRemoveItem.Border.HoverVisible = true;
            this.btnRadioRemoveItem.Border.Rounding = 12;
            this.btnRadioRemoveItem.Border.Thickness = 1;
            this.btnRadioRemoveItem.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.btnRadioRemoveItem.Border.Visible = true;
            this.btnRadioRemoveItem.Box = new System.Drawing.Size(14, 14);
            this.btnRadioRemoveItem.BoxColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnRadioRemoveItem.BoxColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.btnRadioRemoveItem.BoxColorState.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnRadioRemoveItem.BoxColorState.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnRadioRemoveItem.BoxSpacing = 2;
            this.btnRadioRemoveItem.CheckStyle.AutoSize = true;
            this.btnRadioRemoveItem.CheckStyle.Bounds = new System.Drawing.Rectangle(0, 0, 8, 8);
            this.btnRadioRemoveItem.CheckStyle.Character = '✔';
            this.btnRadioRemoveItem.CheckStyle.CheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(136)))), ((int)(((byte)(45)))));
            this.btnRadioRemoveItem.CheckStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRadioRemoveItem.CheckStyle.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            this.btnRadioRemoveItem.CheckStyle.ShapeRounding = 6;
            this.btnRadioRemoveItem.CheckStyle.ShapeType = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.btnRadioRemoveItem.CheckStyle.Style = VisualPlus.Structure.CheckStyle.CheckType.Shape;
            this.btnRadioRemoveItem.CheckStyle.Thickness = 2F;
            this.btnRadioRemoveItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRadioRemoveItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRadioRemoveItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnRadioRemoveItem.IsBoxLarger = false;
            this.btnRadioRemoveItem.Location = new System.Drawing.Point(244, 136);
            this.btnRadioRemoveItem.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.btnRadioRemoveItem.Name = "btnRadioRemoveItem";
            this.btnRadioRemoveItem.Size = new System.Drawing.Size(109, 23);
            this.btnRadioRemoveItem.TabIndex = 1;
            this.btnRadioRemoveItem.Text = "Remover Itens";
            this.btnRadioRemoveItem.TextSize = new System.Drawing.Size(89, 19);
            this.btnRadioRemoveItem.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.btnRadioRemoveItem.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnRadioRemoveItem.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnRadioRemoveItem.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnRadioRemoveItem.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btnRadioRemoveItem.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.btnRadioRemoveItem.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // btnRadioAddItem
            // 
            this.btnRadioAddItem.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.btnRadioAddItem.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.btnRadioAddItem.Border.HoverVisible = true;
            this.btnRadioAddItem.Border.Rounding = 12;
            this.btnRadioAddItem.Border.Thickness = 1;
            this.btnRadioAddItem.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.btnRadioAddItem.Border.Visible = true;
            this.btnRadioAddItem.Box = new System.Drawing.Size(14, 14);
            this.btnRadioAddItem.BoxColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnRadioAddItem.BoxColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.btnRadioAddItem.BoxColorState.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnRadioAddItem.BoxColorState.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnRadioAddItem.BoxSpacing = 2;
            this.btnRadioAddItem.Checked = true;
            this.btnRadioAddItem.CheckStyle.AutoSize = true;
            this.btnRadioAddItem.CheckStyle.Bounds = new System.Drawing.Rectangle(0, 0, 8, 8);
            this.btnRadioAddItem.CheckStyle.Character = '✔';
            this.btnRadioAddItem.CheckStyle.CheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(136)))), ((int)(((byte)(45)))));
            this.btnRadioAddItem.CheckStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRadioAddItem.CheckStyle.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image1")));
            this.btnRadioAddItem.CheckStyle.ShapeRounding = 6;
            this.btnRadioAddItem.CheckStyle.ShapeType = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.btnRadioAddItem.CheckStyle.Style = VisualPlus.Structure.CheckStyle.CheckType.Shape;
            this.btnRadioAddItem.CheckStyle.Thickness = 2F;
            this.btnRadioAddItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRadioAddItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRadioAddItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnRadioAddItem.IsBoxLarger = false;
            this.btnRadioAddItem.Location = new System.Drawing.Point(128, 136);
            this.btnRadioAddItem.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.btnRadioAddItem.Name = "btnRadioAddItem";
            this.btnRadioAddItem.Size = new System.Drawing.Size(109, 23);
            this.btnRadioAddItem.TabIndex = 0;
            this.btnRadioAddItem.Text = "Adicionar Itens";
            this.btnRadioAddItem.TextSize = new System.Drawing.Size(92, 19);
            this.btnRadioAddItem.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.btnRadioAddItem.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnRadioAddItem.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnRadioAddItem.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnRadioAddItem.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btnRadioAddItem.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.btnRadioAddItem.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // visualPanel1
            // 
            this.visualPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.visualPanel1.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.visualPanel1.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.visualPanel1.Border.Color = System.Drawing.Color.Gainsboro;
            this.visualPanel1.Border.HoverColor = System.Drawing.Color.Gainsboro;
            this.visualPanel1.Border.HoverVisible = true;
            this.visualPanel1.Border.Rounding = 6;
            this.visualPanel1.Border.Thickness = 1;
            this.visualPanel1.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.visualPanel1.Border.Visible = true;
            this.visualPanel1.Controls.Add(this.estoqueAtual);
            this.visualPanel1.Controls.Add(this.label9);
            this.visualPanel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualPanel1.Location = new System.Drawing.Point(38, 80);
            this.visualPanel1.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.visualPanel1.Name = "visualPanel1";
            this.visualPanel1.Padding = new System.Windows.Forms.Padding(5);
            this.visualPanel1.Size = new System.Drawing.Size(199, 40);
            this.visualPanel1.TabIndex = 26;
            this.visualPanel1.Text = "visualPanel1";
            this.visualPanel1.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.visualPanel1.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualPanel1.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualPanel1.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualPanel1.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.visualPanel1.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.visualPanel1.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // estoqueAtual
            // 
            this.estoqueAtual.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.estoqueAtual.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.estoqueAtual.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.estoqueAtual.Location = new System.Drawing.Point(128, 10);
            this.estoqueAtual.Name = "estoqueAtual";
            this.estoqueAtual.Size = new System.Drawing.Size(68, 20);
            this.estoqueAtual.TabIndex = 30;
            this.estoqueAtual.Text = "0";
            this.estoqueAtual.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label9.Location = new System.Drawing.Point(11, 11);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(118, 17);
            this.label9.TabIndex = 20;
            this.label9.Text = "Quantidade Atual:";
            // 
            // visualPanel2
            // 
            this.visualPanel2.BackColor = System.Drawing.Color.Transparent;
            this.visualPanel2.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.visualPanel2.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.visualPanel2.Border.Color = System.Drawing.Color.Gainsboro;
            this.visualPanel2.Border.HoverColor = System.Drawing.Color.Gainsboro;
            this.visualPanel2.Border.HoverVisible = true;
            this.visualPanel2.Border.Rounding = 6;
            this.visualPanel2.Border.Thickness = 1;
            this.visualPanel2.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.visualPanel2.Border.Visible = true;
            this.visualPanel2.Controls.Add(this.custoAtual);
            this.visualPanel2.Controls.Add(this.label4);
            this.visualPanel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualPanel2.Location = new System.Drawing.Point(241, 80);
            this.visualPanel2.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.visualPanel2.Name = "visualPanel2";
            this.visualPanel2.Padding = new System.Windows.Forms.Padding(5);
            this.visualPanel2.Size = new System.Drawing.Size(199, 40);
            this.visualPanel2.TabIndex = 27;
            this.visualPanel2.Text = "visualPanel2";
            this.visualPanel2.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.visualPanel2.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualPanel2.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualPanel2.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualPanel2.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.visualPanel2.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.visualPanel2.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // custoAtual
            // 
            this.custoAtual.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.custoAtual.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.custoAtual.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.custoAtual.Location = new System.Drawing.Point(133, 11);
            this.custoAtual.Name = "custoAtual";
            this.custoAtual.Size = new System.Drawing.Size(62, 20);
            this.custoAtual.TabIndex = 50;
            this.custoAtual.Text = "0";
            this.custoAtual.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(13, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 17);
            this.label4.TabIndex = 40;
            this.label4.Text = "Valor Custo Atual:";
            // 
            // AddEstoque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(470, 389);
            this.Controls.Add(this.visualPanel2);
            this.Controls.Add(this.visualPanel1);
            this.Controls.Add(this.btnRadioAddItem);
            this.Controls.Add(this.btnRadioRemoveItem);
            this.Controls.Add(this.obs);
            this.Controls.Add(this.quantidade);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.novaQtd);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(486, 428);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(486, 428);
            this.Name = "AddEstoque";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.visualPanel1.ResumeLayout(false);
            this.visualPanel1.PerformLayout();
            this.visualPanel2.ResumeLayout(false);
            this.visualPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label tituloProduto;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label novaQtd;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private VisualPlus.Toolkit.Controls.Editors.VisualTextBox quantidade;
        private VisualPlus.Toolkit.Controls.Editors.VisualTextBox obs;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualRadioButton btnRadioRemoveItem;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualRadioButton btnRadioAddItem;
        private VisualPlus.Toolkit.Controls.Layout.VisualPanel visualPanel1;
        private System.Windows.Forms.Label estoqueAtual;
        private System.Windows.Forms.Label label9;
        private VisualPlus.Toolkit.Controls.Layout.VisualPanel visualPanel2;
        private System.Windows.Forms.Label custoAtual;
        private System.Windows.Forms.Label label4;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualButton btnSalvar;
    }
}