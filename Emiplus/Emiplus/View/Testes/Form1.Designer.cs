namespace Emiplus.View.Testes
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox11 = new System.Windows.Forms.PictureBox();
            this.pictureBox15 = new System.Windows.Forms.PictureBox();
            this.label18 = new System.Windows.Forms.Label();
            this.pessoaJF = new VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.cpfCnpj = new VisualPlus.Toolkit.Controls.Editors.VisualTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.visualTextBox1 = new VisualPlus.Toolkit.Controls.Editors.VisualTextBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox11
            // 
            this.pictureBox11.BackColor = System.Drawing.Color.White;
            this.pictureBox11.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox11.Image")));
            this.pictureBox11.Location = new System.Drawing.Point(226, 11);
            this.pictureBox11.Name = "pictureBox11";
            this.pictureBox11.Size = new System.Drawing.Size(15, 15);
            this.pictureBox11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox11.TabIndex = 42;
            this.pictureBox11.TabStop = false;
            // 
            // pictureBox15
            // 
            this.pictureBox15.BackColor = System.Drawing.Color.White;
            this.pictureBox15.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox15.Image")));
            this.pictureBox15.Location = new System.Drawing.Point(66, 11);
            this.pictureBox15.Name = "pictureBox15";
            this.pictureBox15.Size = new System.Drawing.Size(15, 15);
            this.pictureBox15.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox15.TabIndex = 37;
            this.pictureBox15.TabStop = false;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.White;
            this.label18.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label18.Location = new System.Drawing.Point(12, 9);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(50, 17);
            this.label18.TabIndex = 41;
            this.label18.Text = "Pessoa";
            // 
            // pessoaJF
            // 
            this.pessoaJF.BackColor = System.Drawing.Color.White;
            this.pessoaJF.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.pessoaJF.BackColorState.Enabled = System.Drawing.Color.White;
            this.pessoaJF.Border.Color = System.Drawing.Color.Gainsboro;
            this.pessoaJF.Border.HoverColor = System.Drawing.Color.Gainsboro;
            this.pessoaJF.Border.HoverVisible = true;
            this.pessoaJF.Border.Rounding = 6;
            this.pessoaJF.Border.Thickness = 1;
            this.pessoaJF.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.pessoaJF.Border.Visible = true;
            this.pessoaJF.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(119)))), ((int)(((byte)(118)))));
            this.pessoaJF.ButtonImage = null;
            this.pessoaJF.ButtonStyle = VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox.ButtonStyles.Arrow;
            this.pessoaJF.ButtonWidth = 30;
            this.pessoaJF.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.pessoaJF.DropDownHeight = 100;
            this.pessoaJF.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pessoaJF.DropDownWidth = 250;
            this.pessoaJF.FormattingEnabled = true;
            this.pessoaJF.ImageList = null;
            this.pessoaJF.ImageVisible = false;
            this.pessoaJF.Index = 0;
            this.pessoaJF.IntegralHeight = false;
            this.pessoaJF.ItemHeight = 23;
            this.pessoaJF.ItemImageVisible = true;
            this.pessoaJF.Location = new System.Drawing.Point(9, 29);
            this.pessoaJF.MenuItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pessoaJF.MenuItemNormal = System.Drawing.Color.White;
            this.pessoaJF.MenuTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pessoaJF.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.pessoaJF.Name = "pessoaJF";
            this.pessoaJF.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pessoaJF.Size = new System.Drawing.Size(132, 29);
            this.pessoaJF.State = VisualPlus.Enumerators.MouseStates.Normal;
            this.pessoaJF.TabIndex = 40;
            this.pessoaJF.TextAlignment = System.Drawing.StringAlignment.Center;
            this.pessoaJF.TextDisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.pessoaJF.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.pessoaJF.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.pessoaJF.TextRendering = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.pessoaJF.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.pessoaJF.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pessoaJF.TextStyle.Hover = System.Drawing.Color.Empty;
            this.pessoaJF.TextStyle.Pressed = System.Drawing.Color.Empty;
            this.pessoaJF.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.pessoaJF.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.pessoaJF.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.pessoaJF.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.pessoaJF.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pessoaJF.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.pessoaJF.Watermark.Text = "Watermark text";
            this.pessoaJF.Watermark.Visible = false;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.White;
            this.label20.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label20.Location = new System.Drawing.Point(148, 10);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(74, 17);
            this.label20.TabIndex = 39;
            this.label20.Text = "CPF / CNPJ";
            // 
            // cpfCnpj
            // 
            this.cpfCnpj.AlphaNumeric = false;
            this.cpfCnpj.BackColor = System.Drawing.Color.White;
            this.cpfCnpj.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.cpfCnpj.BackColorState.Enabled = System.Drawing.Color.White;
            this.cpfCnpj.Border.Color = System.Drawing.Color.Gainsboro;
            this.cpfCnpj.Border.HoverColor = System.Drawing.Color.Gainsboro;
            this.cpfCnpj.Border.HoverVisible = true;
            this.cpfCnpj.Border.Rounding = 8;
            this.cpfCnpj.Border.Thickness = 1;
            this.cpfCnpj.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.cpfCnpj.Border.Visible = true;
            this.cpfCnpj.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.cpfCnpj.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.cpfCnpj.ButtonBorder.HoverVisible = true;
            this.cpfCnpj.ButtonBorder.Rounding = 6;
            this.cpfCnpj.ButtonBorder.Thickness = 1;
            this.cpfCnpj.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.cpfCnpj.ButtonBorder.Visible = true;
            this.cpfCnpj.ButtonColor.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cpfCnpj.ButtonColor.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.cpfCnpj.ButtonColor.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cpfCnpj.ButtonColor.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.cpfCnpj.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cpfCnpj.ButtonIndent = 3;
            this.cpfCnpj.ButtonText = "visualButton";
            this.cpfCnpj.ButtonVisible = false;
            this.cpfCnpj.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cpfCnpj.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cpfCnpj.Image = null;
            this.cpfCnpj.ImageSize = new System.Drawing.Size(16, 16);
            this.cpfCnpj.ImageVisible = false;
            this.cpfCnpj.ImageWidth = 35;
            this.cpfCnpj.Location = new System.Drawing.Point(147, 29);
            this.cpfCnpj.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.cpfCnpj.Name = "cpfCnpj";
            this.cpfCnpj.PasswordChar = '\0';
            this.cpfCnpj.ReadOnly = false;
            this.cpfCnpj.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.cpfCnpj.Size = new System.Drawing.Size(119, 28);
            this.cpfCnpj.TabIndex = 38;
            this.cpfCnpj.TextBoxWidth = 107;
            this.cpfCnpj.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.cpfCnpj.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cpfCnpj.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cpfCnpj.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cpfCnpj.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.cpfCnpj.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.cpfCnpj.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.cpfCnpj.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.cpfCnpj.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cpfCnpj.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.cpfCnpj.Watermark.Text = "Watermark text";
            this.cpfCnpj.Watermark.Visible = false;
            this.cpfCnpj.WordWrap = true;
            this.cpfCnpj.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CpfCnpj_KeyDown);
            this.cpfCnpj.TextChanged += new System.EventHandler(this.CpfCnpj_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.White;
            this.label10.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label10.Location = new System.Drawing.Point(271, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 17);
            this.label10.TabIndex = 44;
            this.label10.Text = "CEP";
            // 
            // visualTextBox1
            // 
            this.visualTextBox1.AlphaNumeric = false;
            this.visualTextBox1.BackColor = System.Drawing.Color.White;
            this.visualTextBox1.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.visualTextBox1.BackColorState.Enabled = System.Drawing.Color.White;
            this.visualTextBox1.Border.Color = System.Drawing.Color.Gainsboro;
            this.visualTextBox1.Border.HoverColor = System.Drawing.Color.Gainsboro;
            this.visualTextBox1.Border.HoverVisible = true;
            this.visualTextBox1.Border.Rounding = 8;
            this.visualTextBox1.Border.Thickness = 1;
            this.visualTextBox1.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.visualTextBox1.Border.Visible = true;
            this.visualTextBox1.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.visualTextBox1.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.visualTextBox1.ButtonBorder.HoverVisible = true;
            this.visualTextBox1.ButtonBorder.Rounding = 6;
            this.visualTextBox1.ButtonBorder.Thickness = 1;
            this.visualTextBox1.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.visualTextBox1.ButtonBorder.Visible = true;
            this.visualTextBox1.ButtonColor.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.visualTextBox1.ButtonColor.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.visualTextBox1.ButtonColor.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.visualTextBox1.ButtonColor.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.visualTextBox1.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.visualTextBox1.ButtonIndent = 3;
            this.visualTextBox1.ButtonText = "visualButton";
            this.visualTextBox1.ButtonVisible = false;
            this.visualTextBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.visualTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualTextBox1.Image = null;
            this.visualTextBox1.ImageSize = new System.Drawing.Size(16, 16);
            this.visualTextBox1.ImageVisible = false;
            this.visualTextBox1.ImageWidth = 35;
            this.visualTextBox1.Location = new System.Drawing.Point(272, 29);
            this.visualTextBox1.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.visualTextBox1.Name = "visualTextBox1";
            this.visualTextBox1.PasswordChar = '\0';
            this.visualTextBox1.ReadOnly = false;
            this.visualTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.visualTextBox1.Size = new System.Drawing.Size(135, 28);
            this.visualTextBox1.TabIndex = 43;
            this.visualTextBox1.TextBoxWidth = 123;
            this.visualTextBox1.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.visualTextBox1.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualTextBox1.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualTextBox1.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualTextBox1.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.visualTextBox1.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.visualTextBox1.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.visualTextBox1.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.visualTextBox1.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.visualTextBox1.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.visualTextBox1.Watermark.Text = "Watermark text";
            this.visualTextBox1.Watermark.Visible = false;
            this.visualTextBox1.WordWrap = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(413, 35);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 45;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.visualTextBox1);
            this.Controls.Add(this.pictureBox11);
            this.Controls.Add(this.pictureBox15);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.pessoaJF);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.cpfCnpj);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox11;
        private System.Windows.Forms.PictureBox pictureBox15;
        private System.Windows.Forms.Label label18;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox pessoaJF;
        private System.Windows.Forms.Label label20;
        private VisualPlus.Toolkit.Controls.Editors.VisualTextBox cpfCnpj;
        private System.Windows.Forms.Label label10;
        private VisualPlus.Toolkit.Controls.Editors.VisualTextBox visualTextBox1;
        private System.Windows.Forms.Button button1;
    }
}