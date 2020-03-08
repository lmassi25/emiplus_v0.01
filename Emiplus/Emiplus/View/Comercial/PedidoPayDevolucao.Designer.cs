namespace Emiplus.View.Comercial
{
    partial class PedidoPayDevolucao
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PedidoPayDevolucao));
            this.panel2 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.Voucher = new VisualPlus.Toolkit.Controls.Editors.VisualTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.GridDevolucoes = new System.Windows.Forms.DataGridView();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVoucher = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPessoa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colExcluir = new System.Windows.Forms.DataGridViewImageColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnCancelar = new VisualPlus.Toolkit.Controls.Interactivity.VisualButton();
            this.btnSalvar = new VisualPlus.Toolkit.Controls.Interactivity.VisualButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridDevolucoes)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(41)))), ((int)(((byte)(51)))));
            this.panel2.Controls.Add(this.label11);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(493, 78);
            this.panel2.TabIndex = 37;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Segoe UI Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(14, 24);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(465, 35);
            this.label11.TabIndex = 5;
            this.label11.Text = "Trocas";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Voucher
            // 
            this.Voucher.AlphaNumeric = false;
            this.Voucher.BackColor = System.Drawing.Color.White;
            this.Voucher.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.Voucher.BackColorState.Enabled = System.Drawing.Color.White;
            this.Voucher.Border.Color = System.Drawing.Color.Gainsboro;
            this.Voucher.Border.HoverColor = System.Drawing.Color.Gainsboro;
            this.Voucher.Border.HoverVisible = true;
            this.Voucher.Border.Rounding = 8;
            this.Voucher.Border.Thickness = 1;
            this.Voucher.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.Voucher.Border.Visible = true;
            this.Voucher.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.Voucher.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.Voucher.ButtonBorder.HoverVisible = true;
            this.Voucher.ButtonBorder.Rounding = 6;
            this.Voucher.ButtonBorder.Thickness = 1;
            this.Voucher.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.Voucher.ButtonBorder.Visible = true;
            this.Voucher.ButtonColor.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Voucher.ButtonColor.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.Voucher.ButtonColor.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Voucher.ButtonColor.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Voucher.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Voucher.ButtonIndent = 3;
            this.Voucher.ButtonText = "visualButton";
            this.Voucher.ButtonVisible = false;
            this.Voucher.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Voucher.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Voucher.Image = null;
            this.Voucher.ImageSize = new System.Drawing.Size(16, 16);
            this.Voucher.ImageVisible = false;
            this.Voucher.ImageWidth = 35;
            this.Voucher.Location = new System.Drawing.Point(145, 133);
            this.Voucher.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.Voucher.Name = "Voucher";
            this.Voucher.PasswordChar = '\0';
            this.Voucher.ReadOnly = false;
            this.Voucher.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.Voucher.Size = new System.Drawing.Size(237, 34);
            this.Voucher.TabIndex = 157;
            this.Voucher.TextBoxWidth = 225;
            this.Voucher.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.Voucher.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Voucher.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Voucher.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Voucher.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.Voucher.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.Voucher.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.Voucher.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.Voucher.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Voucher.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.Voucher.Watermark.Text = "Watermark text";
            this.Voucher.Watermark.Visible = false;
            this.Voucher.WordWrap = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(145, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 17);
            this.label2.TabIndex = 156;
            this.label2.Text = "Voucher";
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel1.Controls.Add(this.GridDevolucoes);
            this.panel1.Location = new System.Drawing.Point(21, 248);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(448, 203);
            this.panel1.TabIndex = 158;
            // 
            // GridDevolucoes
            // 
            this.GridDevolucoes.AllowUserToAddRows = false;
            this.GridDevolucoes.AllowUserToDeleteRows = false;
            this.GridDevolucoes.AllowUserToResizeColumns = false;
            this.GridDevolucoes.AllowUserToResizeRows = false;
            this.GridDevolucoes.BackgroundColor = System.Drawing.Color.White;
            this.GridDevolucoes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GridDevolucoes.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.GridDevolucoes.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridDevolucoes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GridDevolucoes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridDevolucoes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colID,
            this.colVoucher,
            this.colPessoa,
            this.Column3,
            this.colExcluir});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridDevolucoes.DefaultCellStyle = dataGridViewCellStyle2;
            this.GridDevolucoes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridDevolucoes.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.GridDevolucoes.Location = new System.Drawing.Point(0, 0);
            this.GridDevolucoes.MultiSelect = false;
            this.GridDevolucoes.Name = "GridDevolucoes";
            this.GridDevolucoes.ReadOnly = true;
            this.GridDevolucoes.RowHeadersVisible = false;
            this.GridDevolucoes.RowTemplate.Height = 30;
            this.GridDevolucoes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.GridDevolucoes.Size = new System.Drawing.Size(448, 203);
            this.GridDevolucoes.TabIndex = 7;
            // 
            // colID
            // 
            this.colID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colID.HeaderText = "N°";
            this.colID.Name = "colID";
            this.colID.ReadOnly = true;
            this.colID.Width = 60;
            // 
            // colVoucher
            // 
            this.colVoucher.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colVoucher.HeaderText = "Voucher";
            this.colVoucher.Name = "colVoucher";
            this.colVoucher.ReadOnly = true;
            this.colVoucher.Width = 120;
            // 
            // colPessoa
            // 
            this.colPessoa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colPessoa.HeaderText = "Cliente";
            this.colPessoa.Name = "colPessoa";
            this.colPessoa.ReadOnly = true;
            this.colPessoa.Width = 150;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column3.HeaderText = "Valor";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 80;
            // 
            // colExcluir
            // 
            this.colExcluir.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colExcluir.HeaderText = "";
            this.colExcluir.Name = "colExcluir";
            this.colExcluir.ReadOnly = true;
            this.colExcluir.ToolTipText = "Remover";
            this.colExcluir.Width = 30;
            // 
            // panel4
            // 
            this.panel4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.label1);
            this.panel4.Location = new System.Drawing.Point(19, 209);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(448, 40);
            this.panel4.TabIndex = 159;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semilight", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(-4, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 30);
            this.label1.TabIndex = 131;
            this.label1.Text = "Lançamentos";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(246)))));
            this.panel3.Controls.Add(this.btnCancelar);
            this.panel3.Controls.Add(this.btnSalvar);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 479);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(493, 40);
            this.panel3.TabIndex = 160;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.White;
            this.btnCancelar.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCancelar.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(101)))), ((int)(((byte)(101)))));
            this.btnCancelar.BackColorState.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(62)))), ((int)(((byte)(62)))));
            this.btnCancelar.BackColorState.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(62)))), ((int)(((byte)(62)))));
            this.btnCancelar.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(101)))), ((int)(((byte)(101)))));
            this.btnCancelar.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(62)))), ((int)(((byte)(62)))));
            this.btnCancelar.Border.HoverVisible = true;
            this.btnCancelar.Border.Rounding = 6;
            this.btnCancelar.Border.Thickness = 1;
            this.btnCancelar.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.btnCancelar.Border.Visible = true;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Image = null;
            this.btnCancelar.Location = new System.Drawing.Point(371, 5);
            this.btnCancelar.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(96, 30);
            this.btnCancelar.TabIndex = 562;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCancelar.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.btnCancelar.TextStyle.Enabled = System.Drawing.Color.White;
            this.btnCancelar.TextStyle.Hover = System.Drawing.Color.White;
            this.btnCancelar.TextStyle.Pressed = System.Drawing.Color.White;
            this.btnCancelar.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btnCancelar.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.btnCancelar.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
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
            this.btnSalvar.Location = new System.Drawing.Point(244, 5);
            this.btnSalvar.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(121, 30);
            this.btnSalvar.TabIndex = 561;
            this.btnSalvar.Text = "Continuar";
            this.btnSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSalvar.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.btnSalvar.TextStyle.Enabled = System.Drawing.Color.White;
            this.btnSalvar.TextStyle.Hover = System.Drawing.Color.White;
            this.btnSalvar.TextStyle.Pressed = System.Drawing.Color.White;
            this.btnSalvar.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btnSalvar.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.btnSalvar.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(69, 115);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(68, 62);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 155;
            this.pictureBox1.TabStop = false;
            // 
            // PedidoPayDevolucao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(493, 519);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Voucher);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PedidoPayDevolucao";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridDevolucoes)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label11;
        private VisualPlus.Toolkit.Controls.Editors.VisualTextBox Voucher;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView GridDevolucoes;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVoucher;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPessoa;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewImageColumn colExcluir;
        private System.Windows.Forms.Panel panel3;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualButton btnSalvar;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualButton btnCancelar;
    }
}