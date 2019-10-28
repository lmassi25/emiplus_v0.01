namespace Emiplus.Data.Helpers
{
    partial class Alert
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Alert));
            this.title = new System.Windows.Forms.Label();
            this.message = new System.Windows.Forms.Label();
            this.icon = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.timeout = new System.Windows.Forms.Timer(this.components);
            this.Mostrar = new System.Windows.Forms.Timer(this.components);
            this.CloseAlert = new System.Windows.Forms.Timer(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.icon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.ForeColor = System.Drawing.Color.White;
            this.title.Location = new System.Drawing.Point(65, 9);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(89, 20);
            this.title.TabIndex = 0;
            this.title.Text = "Tudo certo!";
            // 
            // message
            // 
            this.message.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.message.ForeColor = System.Drawing.Color.White;
            this.message.Location = new System.Drawing.Point(66, 31);
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(336, 29);
            this.message.TabIndex = 1;
            this.message.Text = "Mensagem de sucesso!";
            // 
            // icon
            // 
            this.icon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.icon.Location = new System.Drawing.Point(13, 14);
            this.icon.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.icon.Name = "icon";
            this.icon.Size = new System.Drawing.Size(35, 35);
            this.icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.icon.TabIndex = 2;
            this.icon.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(379, 4);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(23, 23);
            this.btnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnClose.TabIndex = 3;
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // timeout
            // 
            this.timeout.Enabled = true;
            this.timeout.Interval = 4000;
            this.timeout.Tick += new System.EventHandler(this.timeout_Tick);
            // 
            // Mostrar
            // 
            this.Mostrar.Tick += new System.EventHandler(this.Show_Tick);
            // 
            // CloseAlert
            // 
            this.CloseAlert.Tick += new System.EventHandler(this.Close_Tick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "icon-sucesso.png");
            this.imageList1.Images.SetKeyName(1, "icon-info.png");
            this.imageList1.Images.SetKeyName(2, "icon-warning.png");
            this.imageList1.Images.SetKeyName(3, "icon-cancel.png");
            // 
            // Alert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(126)))), ((int)(((byte)(89)))));
            this.ClientSize = new System.Drawing.Size(408, 61);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.icon);
            this.Controls.Add(this.message);
            this.Controls.Add(this.title);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Alert";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Alert";
            this.Load += new System.EventHandler(this.Alert_Load);
            ((System.ComponentModel.ISupportInitialize)(this.icon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Label message;
        private System.Windows.Forms.PictureBox icon;
        private System.Windows.Forms.PictureBox btnClose;
        private System.Windows.Forms.Timer timeout;
        private System.Windows.Forms.Timer Mostrar;
        private System.Windows.Forms.Timer CloseAlert;
        private System.Windows.Forms.ImageList imageList1;
    }
}