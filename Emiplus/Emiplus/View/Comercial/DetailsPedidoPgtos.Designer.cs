namespace Emiplus.View.Comercial
{
    partial class DetailsPedidoPgtos
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.GridListaFormaPgtos = new System.Windows.Forms.DataGridView();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridListaFormaPgtos)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(59)))), ((int)(((byte)(78)))));
            this.panel2.Controls.Add(this.label11);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(445, 68);
            this.panel2.TabIndex = 122;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Segoe UI Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(3, 18);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(439, 30);
            this.label11.TabIndex = 5;
            this.label11.Text = "Pagamentos Lançados";
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // GridListaFormaPgtos
            // 
            this.GridListaFormaPgtos.AllowUserToAddRows = false;
            this.GridListaFormaPgtos.AllowUserToDeleteRows = false;
            this.GridListaFormaPgtos.AllowUserToResizeColumns = false;
            this.GridListaFormaPgtos.AllowUserToResizeRows = false;
            this.GridListaFormaPgtos.BackgroundColor = System.Drawing.Color.White;
            this.GridListaFormaPgtos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GridListaFormaPgtos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.GridListaFormaPgtos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.GridListaFormaPgtos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridListaFormaPgtos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colID,
            this.Column2,
            this.Column1,
            this.Column3});
            this.GridListaFormaPgtos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridListaFormaPgtos.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.GridListaFormaPgtos.Location = new System.Drawing.Point(0, 68);
            this.GridListaFormaPgtos.MultiSelect = false;
            this.GridListaFormaPgtos.Name = "GridListaFormaPgtos";
            this.GridListaFormaPgtos.ReadOnly = true;
            this.GridListaFormaPgtos.RowHeadersVisible = false;
            this.GridListaFormaPgtos.RowTemplate.Height = 30;
            this.GridListaFormaPgtos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.GridListaFormaPgtos.Size = new System.Drawing.Size(445, 217);
            this.GridListaFormaPgtos.TabIndex = 123;
            // 
            // colID
            // 
            this.colID.HeaderText = "Id";
            this.colID.Name = "colID";
            this.colID.ReadOnly = true;
            this.colID.Visible = false;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "Forma de pagamento";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Vencimento";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Valor";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // DetailsPedidoPgtos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(445, 285);
            this.Controls.Add(this.GridListaFormaPgtos);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(461, 324);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(461, 324);
            this.Name = "DetailsPedidoPgtos";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridListaFormaPgtos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView GridListaFormaPgtos;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}