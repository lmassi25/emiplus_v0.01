namespace Emiplus.View.Produtos
{
    partial class ModalNCM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModalNCM));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.search = new VisualPlus.Toolkit.Controls.Editors.VisualTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSelecionar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.GridLista = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buscar = new VisualPlus.Toolkit.Controls.Interactivity.VisualButton();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridLista)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(59)))), ((int)(((byte)(78)))));
            this.panel2.Controls.Add(this.label11);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(605, 68);
            this.panel2.TabIndex = 56;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(209, 21);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(165, 30);
            this.label11.TabIndex = 5;
            this.label11.Text = "Selecione o NCM";
            // 
            // search
            // 
            this.search.AlphaNumeric = false;
            this.search.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.search.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.search.BackColorState.Enabled = System.Drawing.Color.White;
            this.search.Border.Color = System.Drawing.Color.Gainsboro;
            this.search.Border.HoverColor = System.Drawing.Color.Gainsboro;
            this.search.Border.HoverVisible = true;
            this.search.Border.Rounding = 8;
            this.search.Border.Thickness = 1;
            this.search.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.search.Border.Visible = true;
            this.search.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.search.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.search.ButtonBorder.HoverVisible = true;
            this.search.ButtonBorder.Rounding = 6;
            this.search.ButtonBorder.Thickness = 1;
            this.search.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.search.ButtonBorder.Visible = true;
            this.search.ButtonColor.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.search.ButtonColor.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.search.ButtonColor.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.search.ButtonColor.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.search.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.search.ButtonIndent = 3;
            this.search.ButtonText = "visualButton";
            this.search.ButtonVisible = false;
            this.search.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.search.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.search.Image = ((System.Drawing.Image)(resources.GetObject("search.Image")));
            this.search.ImageSize = new System.Drawing.Size(16, 16);
            this.search.ImageVisible = true;
            this.search.ImageWidth = 35;
            this.search.Location = new System.Drawing.Point(38, 108);
            this.search.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.search.Name = "search";
            this.search.PasswordChar = '\0';
            this.search.ReadOnly = false;
            this.search.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.search.Size = new System.Drawing.Size(376, 34);
            this.search.TabIndex = 61;
            this.search.TextBoxWidth = 330;
            this.search.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.search.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.search.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.search.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.search.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.search.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.search.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.search.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.search.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.search.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.search.Watermark.Text = "Watermark text";
            this.search.Watermark.Visible = false;
            this.search.WordWrap = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.panel1.Controls.Add(this.btnSelecionar);
            this.panel1.Controls.Add(this.btnCancelar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 359);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(605, 63);
            this.panel1.TabIndex = 62;
            // 
            // btnSelecionar
            // 
            this.btnSelecionar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelecionar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSelecionar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelecionar.FlatAppearance.BorderSize = 0;
            this.btnSelecionar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSelecionar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSelecionar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelecionar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelecionar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSelecionar.Image = ((System.Drawing.Image)(resources.GetObject("btnSelecionar.Image")));
            this.btnSelecionar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSelecionar.Location = new System.Drawing.Point(354, 2);
            this.btnSelecionar.Name = "btnSelecionar";
            this.btnSelecionar.Size = new System.Drawing.Size(119, 60);
            this.btnSelecionar.TabIndex = 2;
            this.btnSelecionar.Text = "Selecionar (F10)";
            this.btnSelecionar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSelecionar.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCancelar.Location = new System.Drawing.Point(475, 2);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(118, 60);
            this.btnCancelar.TabIndex = 3;
            this.btnCancelar.Text = "Cancelar (ESC)";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.GridLista);
            this.panel3.Location = new System.Drawing.Point(38, 178);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(528, 155);
            this.panel3.TabIndex = 66;
            // 
            // GridLista
            // 
            this.GridLista.AllowUserToAddRows = false;
            this.GridLista.AllowUserToDeleteRows = false;
            this.GridLista.AllowUserToResizeColumns = false;
            this.GridLista.AllowUserToResizeRows = false;
            this.GridLista.BackgroundColor = System.Drawing.Color.White;
            this.GridLista.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GridLista.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.GridLista.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridLista.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.GridLista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
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
            this.GridLista.ReadOnly = true;
            this.GridLista.RowHeadersVisible = false;
            this.GridLista.RowTemplate.Height = 30;
            this.GridLista.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridLista.Size = new System.Drawing.Size(528, 155);
            this.GridLista.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(35, 156);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 17);
            this.label1.TabIndex = 65;
            this.label1.Text = "NCM\'s encontrados:\r\n";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(34, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 17);
            this.label2.TabIndex = 64;
            this.label2.Text = "Buscar:";
            // 
            // buscar
            // 
            this.buscar.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buscar.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(160)))), ((int)(((byte)(220)))));
            this.buscar.BackColorState.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(135)))), ((int)(((byte)(194)))));
            this.buscar.BackColorState.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(135)))), ((int)(((byte)(194)))));
            this.buscar.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(135)))), ((int)(((byte)(194)))));
            this.buscar.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(135)))), ((int)(((byte)(194)))));
            this.buscar.Border.HoverVisible = true;
            this.buscar.Border.Rounding = 6;
            this.buscar.Border.Thickness = 1;
            this.buscar.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.buscar.Border.Visible = true;
            this.buscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buscar.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buscar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buscar.ForeColor = System.Drawing.Color.White;
            this.buscar.Image = null;
            this.buscar.Location = new System.Drawing.Point(420, 108);
            this.buscar.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.buscar.Name = "buscar";
            this.buscar.Size = new System.Drawing.Size(89, 34);
            this.buscar.TabIndex = 156;
            this.buscar.Text = "Buscar";
            this.buscar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buscar.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.buscar.TextStyle.Enabled = System.Drawing.Color.White;
            this.buscar.TextStyle.Hover = System.Drawing.Color.White;
            this.buscar.TextStyle.Pressed = System.Drawing.Color.White;
            this.buscar.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.buscar.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.buscar.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semilight", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(187, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 25);
            this.label3.TabIndex = 157;
            this.label3.Text = "Carregando..";
            // 
            // ModalNCM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(605, 422);
            this.Controls.Add(this.buscar);
            this.Controls.Add(this.search);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Name = "ModalNCM";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridLista)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label11;
        private VisualPlus.Toolkit.Controls.Editors.VisualTextBox search;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSelecionar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView GridLista;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualButton buscar;
        private System.Windows.Forms.Label label3;
    }
}