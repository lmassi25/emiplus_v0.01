﻿namespace Emiplus.View.Comercial
{
    partial class PedidoModalItens
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PedidoModalItens));
            this.panel1 = new System.Windows.Forms.Panel();
            this.Selecionar = new VisualPlus.Toolkit.Controls.Interactivity.VisualButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.GridListaProdutos = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buscarProduto = new VisualPlus.Toolkit.Controls.Editors.VisualTextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridListaProdutos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(246)))));
            this.panel1.Controls.Add(this.Selecionar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 492);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(882, 40);
            this.panel1.TabIndex = 56;
            // 
            // Selecionar
            // 
            this.Selecionar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Selecionar.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Selecionar.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(166)))), ((int)(((byte)(155)))));
            this.Selecionar.BackColorState.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(158)))), ((int)(((byte)(147)))));
            this.Selecionar.BackColorState.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(158)))), ((int)(((byte)(147)))));
            this.Selecionar.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(166)))), ((int)(((byte)(155)))));
            this.Selecionar.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(158)))), ((int)(((byte)(147)))));
            this.Selecionar.Border.HoverVisible = true;
            this.Selecionar.Border.Rounding = 6;
            this.Selecionar.Border.Thickness = 1;
            this.Selecionar.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.Selecionar.Border.Visible = true;
            this.Selecionar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Selecionar.DialogResult = System.Windows.Forms.DialogResult.None;
            this.Selecionar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Selecionar.ForeColor = System.Drawing.Color.White;
            this.Selecionar.Image = null;
            this.Selecionar.Location = new System.Drawing.Point(729, 5);
            this.Selecionar.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.Selecionar.Name = "Selecionar";
            this.Selecionar.Size = new System.Drawing.Size(139, 30);
            this.Selecionar.TabIndex = 556;
            this.Selecionar.Text = "Selecionar (F10)";
            this.Selecionar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Selecionar.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.Selecionar.TextStyle.Enabled = System.Drawing.Color.White;
            this.Selecionar.TextStyle.Hover = System.Drawing.Color.White;
            this.Selecionar.TextStyle.Pressed = System.Drawing.Color.White;
            this.Selecionar.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.Selecionar.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.Selecionar.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(41)))), ((int)(((byte)(51)))));
            this.panel2.Controls.Add(this.label11);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(882, 73);
            this.panel2.TabIndex = 55;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Segoe UI Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(0, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(882, 30);
            this.label11.TabIndex = 5;
            this.label11.Text = "Busca avançada de Produtos / Serviços";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.GridListaProdutos);
            this.panel3.Location = new System.Drawing.Point(29, 206);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(823, 234);
            this.panel3.TabIndex = 60;
            // 
            // GridListaProdutos
            // 
            this.GridListaProdutos.AllowUserToAddRows = false;
            this.GridListaProdutos.AllowUserToDeleteRows = false;
            this.GridListaProdutos.AllowUserToResizeColumns = false;
            this.GridListaProdutos.AllowUserToResizeRows = false;
            this.GridListaProdutos.BackgroundColor = System.Drawing.Color.White;
            this.GridListaProdutos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GridListaProdutos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.GridListaProdutos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridListaProdutos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GridListaProdutos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridListaProdutos.DefaultCellStyle = dataGridViewCellStyle2;
            this.GridListaProdutos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridListaProdutos.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.GridListaProdutos.Location = new System.Drawing.Point(0, 0);
            this.GridListaProdutos.MultiSelect = false;
            this.GridListaProdutos.Name = "GridListaProdutos";
            this.GridListaProdutos.ReadOnly = true;
            this.GridListaProdutos.RowTemplate.Height = 30;
            this.GridListaProdutos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridListaProdutos.Size = new System.Drawing.Size(823, 234);
            this.GridListaProdutos.TabIndex = 9;
            this.GridListaProdutos.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(26, 185);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 17);
            this.label1.TabIndex = 59;
            this.label1.Text = "Resultados encontrados:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(79, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(197, 17);
            this.label2.TabIndex = 58;
            this.label2.Text = "Buscar produtos / serviços: (F1)";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(24, 97);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(51, 54);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 57;
            this.pictureBox1.TabStop = false;
            // 
            // buscarProduto
            // 
            this.buscarProduto.AlphaNumeric = false;
            this.buscarProduto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buscarProduto.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.buscarProduto.BackColorState.Enabled = System.Drawing.Color.White;
            this.buscarProduto.Border.Color = System.Drawing.Color.Gainsboro;
            this.buscarProduto.Border.HoverColor = System.Drawing.Color.Gainsboro;
            this.buscarProduto.Border.HoverVisible = true;
            this.buscarProduto.Border.Rounding = 8;
            this.buscarProduto.Border.Thickness = 1;
            this.buscarProduto.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.buscarProduto.Border.Visible = true;
            this.buscarProduto.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.buscarProduto.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.buscarProduto.ButtonBorder.HoverVisible = true;
            this.buscarProduto.ButtonBorder.Rounding = 6;
            this.buscarProduto.ButtonBorder.Thickness = 1;
            this.buscarProduto.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.buscarProduto.ButtonBorder.Visible = true;
            this.buscarProduto.ButtonColor.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buscarProduto.ButtonColor.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.buscarProduto.ButtonColor.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buscarProduto.ButtonColor.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buscarProduto.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buscarProduto.ButtonIndent = 3;
            this.buscarProduto.ButtonText = "visualButton";
            this.buscarProduto.ButtonVisible = false;
            this.buscarProduto.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buscarProduto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buscarProduto.Image = ((System.Drawing.Image)(resources.GetObject("buscarProduto.Image")));
            this.buscarProduto.ImageSize = new System.Drawing.Size(16, 16);
            this.buscarProduto.ImageVisible = true;
            this.buscarProduto.ImageWidth = 35;
            this.buscarProduto.Location = new System.Drawing.Point(82, 118);
            this.buscarProduto.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.buscarProduto.Name = "buscarProduto";
            this.buscarProduto.PasswordChar = '\0';
            this.buscarProduto.ReadOnly = false;
            this.buscarProduto.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.buscarProduto.Size = new System.Drawing.Size(376, 34);
            this.buscarProduto.TabIndex = 0;
            this.buscarProduto.TextBoxWidth = 225;
            this.buscarProduto.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.buscarProduto.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buscarProduto.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buscarProduto.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buscarProduto.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.buscarProduto.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.buscarProduto.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.buscarProduto.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.buscarProduto.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buscarProduto.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.buscarProduto.Watermark.Text = "Watermark text";
            this.buscarProduto.Watermark.Visible = false;
            this.buscarProduto.WordWrap = true;
            // 
            // PedidoModalItens
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(882, 532);
            this.Controls.Add(this.buscarProduto);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(898, 571);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(898, 571);
            this.Name = "PedidoModalItens";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridListaProdutos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView GridListaProdutos;
        private VisualPlus.Toolkit.Controls.Editors.VisualTextBox buscarProduto;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualButton Selecionar;
    }
}