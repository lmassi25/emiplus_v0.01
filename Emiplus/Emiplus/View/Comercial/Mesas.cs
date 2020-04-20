using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.View.Common;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VisualPlus.Toolkit.Controls.Interactivity;
using VisualPlus.Toolkit.Controls.Layout;

namespace Emiplus.View.Comercial
{
    public partial class Mesas : Form
    {
        private Model.Pedido _mPedido = new Model.Pedido();
        private Model.PedidoItem _mPedidoItem = new Model.PedidoItem();

        private List<string> checks = new List<string>();
        
        public Mesas()
        {
            InitializeComponent();
            Eventos();
        }

        private double GetSubTotal(string mesa)
        {
            Model.PedidoItem data = new Model.PedidoItem().Query().SelectRaw("SUM(TOTAL) AS TOTAL").Where("mesa", mesa).WhereFalse("excluir").Where("pedido", 0).FirstOrDefault<Model.PedidoItem>();
            if (data != null)
                return data.Total;

            return 0;
        }

        private Model.PedidoItem GetData(string mesa)
        {
            Model.PedidoItem data = new Model.PedidoItem().FindAll().Where("mesa", mesa).WhereFalse("excluir").Where("pedido", 0).FirstOrDefault<Model.PedidoItem>();
            if (data != null)
                return data;

            return null;
        }

        private Model.Usuarios GetUsuario(string mesa)
        {
            var idUser = GetData(mesa) == null ? 0 : GetData(mesa).Usuario;
            Model.Usuarios data = new Model.Usuarios().FindByUserId(idUser).FirstOrDefault<Model.Usuarios>();
            if (data != null)
                return data;

            return null;
        }

        private void LoadMesas(string searchText = "")
        {
            flowLayout.Controls.Clear();
            
            string txtSearch = $"%{searchText}%";
            if (IniFile.Read("MesasPreCadastrada", "Comercial") == "True")
            {
                IEnumerable<Model.Mesas> mesas = new Model.Mesas().FindAll().WhereFalse("excluir").Where("mesa", "like", txtSearch).Get<Model.Mesas>();
                if (mesas.Count() > 0)
                {
                    foreach (var mesa in mesas)
                    {
                        string idMesa = mesa.Mesa;

                        Model.PedidoItem pedidoItem = new Model.PedidoItem().FindAll().WhereFalse("excluir").Where("pedido", 0).Where("mesa", idMesa).FirstOrDefault<Model.PedidoItem>();

                        VisualPanel panel1 = new VisualPanel();
                        panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                        | System.Windows.Forms.AnchorStyles.Right)));
                        panel1.BackColor = System.Drawing.Color.Transparent;
                        panel1.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
                        panel1.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
                        panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
                        panel1.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
                        panel1.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
                        panel1.Border.HoverVisible = true;
                        panel1.Border.Rounding = 6;
                        panel1.Border.Thickness = 1;
                        panel1.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
                        panel1.Border.Visible = true;
                        panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                        panel1.Location = new System.Drawing.Point(3, 3);
                        panel1.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
                        panel1.Name = $"{idMesa}mesa";
                        panel1.Cursor = Cursors.Hand;
                        panel1.Padding = new System.Windows.Forms.Padding(5);
                        panel1.Size = new System.Drawing.Size(229, 140);
                        panel1.TabIndex = 40060;
                        panel1.Text = "visualPanel2";
                        panel1.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
                        panel1.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                        panel1.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                        panel1.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                        panel1.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
                        panel1.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
                        panel1.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                        #region panel2
                        VisualPanel panel2 = new VisualPanel();
                        panel2.BackColor = System.Drawing.Color.Gray;
                        panel2.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
                        panel2.BackColorState.Enabled = System.Drawing.Color.Gray;
                        panel2.Border.Color = System.Drawing.Color.Gray;
                        panel2.Border.HoverColor = System.Drawing.Color.Gray;
                        panel2.Border.HoverVisible = true;
                        panel2.Border.Rounding = 6;
                        panel2.Border.Thickness = 1;
                        panel2.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
                        panel2.Border.Visible = true;
                        panel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                        panel2.Location = new System.Drawing.Point(0, 0);
                        panel2.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
                        panel2.Name = "visualPanel1";
                        panel2.Padding = new System.Windows.Forms.Padding(5);
                        panel2.Size = new System.Drawing.Size(229, 41);
                        panel2.TabIndex = 11;
                        panel2.Text = "visualPanel1";
                        panel2.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
                        panel2.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                        panel2.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                        panel2.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                        panel2.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
                        panel2.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
                        panel2.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                        Label txtNrMesa = new Label();
                        txtNrMesa.AutoSize = true;
                        txtNrMesa.BackColor = System.Drawing.Color.Gray;
                        txtNrMesa.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        txtNrMesa.ForeColor = System.Drawing.Color.White;
                        txtNrMesa.Location = new System.Drawing.Point(33, 8);
                        txtNrMesa.Name = $"{idMesa}";
                        txtNrMesa.Size = new System.Drawing.Size(34, 25);
                        txtNrMesa.TabIndex = 6;
                        txtNrMesa.Text = mesa.Mesa;
                        txtNrMesa.Click += actionMesa;

                        VisualCheckBox check = new VisualCheckBox();
                        check.Checked = false;
                        check.Click += actionCheck;
                        check.BackColor = System.Drawing.Color.Gray;
                        check.Border.Color = System.Drawing.Color.White;
                        check.Border.HoverColor = System.Drawing.Color.White;
                        check.Border.HoverVisible = true;
                        check.Border.Rounding = 3;
                        check.Border.Thickness = 1;
                        check.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
                        check.Border.Visible = true;
                        check.Box = new System.Drawing.Size(14, 14);
                        check.BoxColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
                        check.BoxColorState.Enabled = System.Drawing.Color.White;
                        check.BoxColorState.Hover = System.Drawing.Color.White;
                        check.BoxColorState.Pressed = System.Drawing.Color.White;
                        check.BoxSpacing = 2;
                        check.CheckStyle.AutoSize = true;
                        check.CheckStyle.Bounds = new System.Drawing.Rectangle(0, 0, 125, 23);
                        check.CheckStyle.Character = '✔';
                        check.CheckStyle.CheckColor = System.Drawing.Color.Gray;
                        check.CheckStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        //check.CheckStyle.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
                        check.CheckStyle.ShapeRounding = 3;
                        check.CheckStyle.ShapeType = VisualPlus.Enumerators.ShapeTypes.Rounded;
                        check.CheckStyle.Style = VisualPlus.Structure.CheckStyle.CheckType.Checkmark;
                        check.CheckStyle.Thickness = 2F;
                        check.Cursor = System.Windows.Forms.Cursors.Hand;
                        check.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                        check.IsBoxLarger = false;
                        check.Location = new System.Drawing.Point(9, 9);
                        check.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
                        check.Name = $"{idMesa}check";
                        check.Size = new System.Drawing.Size(18, 23);
                        check.TabIndex = 9;
                        check.TextSize = new System.Drawing.Size(0, 0);
                        check.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
                        check.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                        check.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                        check.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                        check.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
                        check.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
                        check.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                        
                        if (pedidoItem == null)
                            check.Visible = false;

                        panel2.Controls.Add(check);
                        panel2.Controls.Add(txtNrMesa);
                        #endregion
                    
                        Label SubTotal = new Label();
                        SubTotal.AutoSize = true;
                        SubTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
                        SubTotal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        SubTotal.ForeColor = System.Drawing.Color.DarkGray;
                        SubTotal.Location = new System.Drawing.Point(5, 112);
                        SubTotal.Name = "label13";
                        SubTotal.Size = new System.Drawing.Size(54, 15);
                        SubTotal.TabIndex = 15;
                        SubTotal.Text = "Subtotal:";

                        Label txtSubTotal = new Label();
                        txtSubTotal.AutoSize = true;
                        txtSubTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
                        txtSubTotal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        txtSubTotal.ForeColor = System.Drawing.Color.Gray;
                        txtSubTotal.Location = new System.Drawing.Point(72, 112);
                        txtSubTotal.Name = "label12";
                        txtSubTotal.Size = new System.Drawing.Size(56, 15);
                        txtSubTotal.TabIndex = 16;
                        if (pedidoItem != null)
                            txtSubTotal.Text = $"R$ {Validation.FormatPrice(GetSubTotal(idMesa))}";
                        else 
                            txtSubTotal.Text = "R$ 00,00";

                        Label Hra = new Label();
                        Hra.AutoSize = true;
                        Hra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
                        Hra.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        Hra.ForeColor = System.Drawing.Color.DarkGray;
                        Hra.Location = new System.Drawing.Point(5, 91);
                        Hra.Name = "label3";
                        Hra.Size = new System.Drawing.Size(46, 15);
                        Hra.TabIndex = 7;
                        Hra.Text = "Tempo:";

                        Label txtHra = new Label();
                        txtHra.AutoSize = true;
                        txtHra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
                        txtHra.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        txtHra.ForeColor = System.Drawing.Color.Gray;
                        txtHra.Location = new System.Drawing.Point(72, 91);
                        txtHra.Name = "label11";
                        txtHra.Size = new System.Drawing.Size(56, 15);
                        txtHra.TabIndex = 14;

                        if (pedidoItem != null)
                        {
                            var date = DateTime.Now;
                            var hourMesa = date.AddHours(-GetData(idMesa).Criado.Hour);
                            var minMesa = date.AddMinutes(-GetData(idMesa).Criado.Minute);

                            txtHra.Text = $"{hourMesa.Hour}h {minMesa.Minute}m";
                        }
                        else
                            txtHra.Text = "00h 00m";

                        Label Status = new Label();
                        Status.AutoSize = true;
                        Status.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
                        Status.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        Status.ForeColor = System.Drawing.Color.DarkGray;
                        Status.Location = new System.Drawing.Point(5, 70);
                        Status.Name = "label7";
                        Status.Size = new System.Drawing.Size(42, 15);
                        Status.TabIndex = 10;
                        Status.Text = "Status:";

                        Label txtStatus = new Label();
                        txtStatus.AutoSize = true;
                        txtStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
                        txtStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        
                        if (pedidoItem != null)
                            txtStatus.ForeColor = System.Drawing.Color.Red;
                        else
                            txtStatus.ForeColor = System.Drawing.Color.Green;

                        txtStatus.Location = new System.Drawing.Point(72, 70);
                        txtStatus.Name = "label10";
                        txtStatus.Size = new System.Drawing.Size(56, 15);
                        txtStatus.TabIndex = 13;
                        if (pedidoItem != null)
                            txtStatus.Text = "Ocupado";
                        else
                            txtStatus.Text = "Livre";
                    
                        Label Atendente = new Label();
                        Atendente.AutoSize = true;
                        Atendente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
                        Atendente.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        Atendente.ForeColor = System.Drawing.Color.DarkGray;
                        Atendente.Location = new System.Drawing.Point(5, 49);
                        Atendente.Name = "label4";
                        Atendente.Size = new System.Drawing.Size(65, 15);
                        Atendente.TabIndex = 8;
                        Atendente.Text = "Atendente:";

                        Label txtAtendente = new Label();
                        txtAtendente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
                        txtAtendente.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        txtAtendente.ForeColor = System.Drawing.Color.Gray;
                        txtAtendente.Location = new System.Drawing.Point(72, 49);
                        txtAtendente.Name = "label9";
                        txtAtendente.Size = new System.Drawing.Size(151, 15);
                        txtAtendente.TabIndex = 12;
                        
                        if (txtStatus != null)
                        {
                            var userName = GetUsuario(idMesa) == null ? "" : GetUsuario(idMesa).Nome;
                            txtAtendente.Text = $"{Validation.FirstCharToUpper(userName)}";
                        }
                        else
                            txtAtendente.Text = "";

                        panel1.Controls.Add(txtSubTotal);
                        panel1.Controls.Add(SubTotal);
                        panel1.Controls.Add(txtHra);
                        panel1.Controls.Add(txtStatus);
                        panel1.Controls.Add(txtAtendente);
                        panel1.Controls.Add(panel2);
                        panel1.Controls.Add(Status);
                        panel1.Controls.Add(Atendente);
                        panel1.Controls.Add(Hra);

                        flowLayout.Controls.Add(panel1);
                    }
                }
            }
            else
            {
                var mesas = _mPedidoItem.FindAll(new[] { "mesa" }).Where("pedido", 0).Where("mesa", "like", txtSearch).GroupBy("mesa").Get();
                if (mesas != null)
                {
                    foreach (var mesa in mesas)
                    {
                        string idMesa = mesa.MESA.ToString();

                        VisualPanel panel1 = new VisualPanel();
                        panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                        | System.Windows.Forms.AnchorStyles.Right)));
                        panel1.BackColor = System.Drawing.Color.Transparent;
                        panel1.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
                        panel1.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
                        panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
                        panel1.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
                        panel1.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
                        panel1.Border.HoverVisible = true;
                        panel1.Border.Rounding = 6;
                        panel1.Border.Thickness = 1;
                        panel1.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
                        panel1.Border.Visible = true;
                        panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                        panel1.Location = new System.Drawing.Point(3, 3);
                        panel1.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
                        panel1.Name = $"{idMesa}mesa";
                        panel1.Cursor = Cursors.Hand;
                        panel1.Padding = new System.Windows.Forms.Padding(5);
                        panel1.Size = new System.Drawing.Size(229, 140);
                        panel1.TabIndex = 40060;
                        panel1.Text = "visualPanel2";
                        panel1.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
                        panel1.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                        panel1.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                        panel1.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                        panel1.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
                        panel1.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
                        panel1.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                        #region panel2
                        VisualPanel panel2 = new VisualPanel();
                        panel2.BackColor = System.Drawing.Color.Gray;
                        panel2.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
                        panel2.BackColorState.Enabled = System.Drawing.Color.Gray;
                        panel2.Border.Color = System.Drawing.Color.Gray;
                        panel2.Border.HoverColor = System.Drawing.Color.Gray;
                        panel2.Border.HoverVisible = true;
                        panel2.Border.Rounding = 6;
                        panel2.Border.Thickness = 1;
                        panel2.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
                        panel2.Border.Visible = true;
                        panel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                        panel2.Location = new System.Drawing.Point(0, 0);
                        panel2.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
                        panel2.Name = "visualPanel1";
                        panel2.Padding = new System.Windows.Forms.Padding(5);
                        panel2.Size = new System.Drawing.Size(229, 41);
                        panel2.TabIndex = 11;
                        panel2.Text = "visualPanel1";
                        panel2.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
                        panel2.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                        panel2.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                        panel2.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                        panel2.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
                        panel2.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
                        panel2.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                        Label txtNrMesa = new Label();
                        txtNrMesa.AutoSize = true;
                        txtNrMesa.BackColor = System.Drawing.Color.Gray;
                        txtNrMesa.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        txtNrMesa.ForeColor = System.Drawing.Color.White;
                        txtNrMesa.Location = new System.Drawing.Point(33, 8);
                        txtNrMesa.Name = $"{idMesa}";
                        txtNrMesa.Size = new System.Drawing.Size(34, 25);
                        txtNrMesa.TabIndex = 6;
                        txtNrMesa.Text = mesa.MESA.ToString();
                        txtNrMesa.Click += actionMesa;

                        VisualCheckBox check = new VisualCheckBox();
                        check.Checked = false;
                        check.Click += actionCheck;
                        check.BackColor = System.Drawing.Color.Gray;
                        check.Border.Color = System.Drawing.Color.White;
                        check.Border.HoverColor = System.Drawing.Color.White;
                        check.Border.HoverVisible = true;
                        check.Border.Rounding = 3;
                        check.Border.Thickness = 1;
                        check.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
                        check.Border.Visible = true;
                        check.Box = new System.Drawing.Size(14, 14);
                        check.BoxColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
                        check.BoxColorState.Enabled = System.Drawing.Color.White;
                        check.BoxColorState.Hover = System.Drawing.Color.White;
                        check.BoxColorState.Pressed = System.Drawing.Color.White;
                        check.BoxSpacing = 2;
                        check.CheckStyle.AutoSize = true;
                        check.CheckStyle.Bounds = new System.Drawing.Rectangle(0, 0, 125, 23);
                        check.CheckStyle.Character = '✔';
                        check.CheckStyle.CheckColor = System.Drawing.Color.Gray;
                        check.CheckStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        //check.CheckStyle.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
                        check.CheckStyle.ShapeRounding = 3;
                        check.CheckStyle.ShapeType = VisualPlus.Enumerators.ShapeTypes.Rounded;
                        check.CheckStyle.Style = VisualPlus.Structure.CheckStyle.CheckType.Checkmark;
                        check.CheckStyle.Thickness = 2F;
                        check.Cursor = System.Windows.Forms.Cursors.Hand;
                        check.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                        check.IsBoxLarger = false;
                        check.Location = new System.Drawing.Point(9, 9);
                        check.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
                        check.Name = $"{idMesa}check";
                        check.Size = new System.Drawing.Size(18, 23);
                        check.TabIndex = 9;
                        check.TextSize = new System.Drawing.Size(0, 0);
                        check.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
                        check.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                        check.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                        check.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                        check.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
                        check.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
                        check.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                        panel2.Controls.Add(check);
                        panel2.Controls.Add(txtNrMesa);
                        #endregion
                    
                        Label SubTotal = new Label();
                        SubTotal.AutoSize = true;
                        SubTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
                        SubTotal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        SubTotal.ForeColor = System.Drawing.Color.DarkGray;
                        SubTotal.Location = new System.Drawing.Point(5, 112);
                        SubTotal.Name = "label13";
                        SubTotal.Size = new System.Drawing.Size(54, 15);
                        SubTotal.TabIndex = 15;
                        SubTotal.Text = "Subtotal:";

                        Label txtSubTotal = new Label();
                        txtSubTotal.AutoSize = true;
                        txtSubTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
                        txtSubTotal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        txtSubTotal.ForeColor = System.Drawing.Color.Gray;
                        txtSubTotal.Location = new System.Drawing.Point(72, 112);
                        txtSubTotal.Name = "label12";
                        txtSubTotal.Size = new System.Drawing.Size(56, 15);
                        txtSubTotal.TabIndex = 16;
                        txtSubTotal.Text = $"R$ {Validation.FormatPrice(GetSubTotal(idMesa))}";

                        Label Hra = new Label();
                        Hra.AutoSize = true;
                        Hra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
                        Hra.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        Hra.ForeColor = System.Drawing.Color.DarkGray;
                        Hra.Location = new System.Drawing.Point(5, 91);
                        Hra.Name = "label3";
                        Hra.Size = new System.Drawing.Size(46, 15);
                        Hra.TabIndex = 7;
                        Hra.Text = "Tempo:";

                        Label txtHra = new Label();
                        txtHra.AutoSize = true;
                        txtHra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
                        txtHra.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        txtHra.ForeColor = System.Drawing.Color.Gray;
                        txtHra.Location = new System.Drawing.Point(72, 91);
                        txtHra.Name = "label11";
                        txtHra.Size = new System.Drawing.Size(56, 15);
                        txtHra.TabIndex = 14;

                        var date = DateTime.Now;
                        var hourMesa = date.AddHours(-GetData(idMesa).Criado.Hour);
                        var minMesa = date.AddMinutes(-GetData(idMesa).Criado.Minute);

                        txtHra.Text = $"{hourMesa.Hour}h {minMesa.Minute}m";

                        Label Status = new Label();
                        Status.AutoSize = true;
                        Status.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
                        Status.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        Status.ForeColor = System.Drawing.Color.DarkGray;
                        Status.Location = new System.Drawing.Point(5, 70);
                        Status.Name = "label7";
                        Status.Size = new System.Drawing.Size(42, 15);
                        Status.TabIndex = 10;
                        Status.Text = "Status:";

                        Label txtStatus = new Label();
                        txtStatus.AutoSize = true;
                        txtStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
                        txtStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        txtStatus.ForeColor = System.Drawing.Color.Red;
                        txtStatus.Location = new System.Drawing.Point(72, 70);
                        txtStatus.Name = "label10";
                        txtStatus.Size = new System.Drawing.Size(56, 15);
                        txtStatus.TabIndex = 13;
                        txtStatus.Text = "Ocupado";
                    
                        Label Atendente = new Label();
                        Atendente.AutoSize = true;
                        Atendente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
                        Atendente.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        Atendente.ForeColor = System.Drawing.Color.DarkGray;
                        Atendente.Location = new System.Drawing.Point(5, 49);
                        Atendente.Name = "label4";
                        Atendente.Size = new System.Drawing.Size(65, 15);
                        Atendente.TabIndex = 8;
                        Atendente.Text = "Atendente:";

                        Label txtAtendente = new Label();
                        txtAtendente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
                        txtAtendente.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        txtAtendente.ForeColor = System.Drawing.Color.Gray;
                        txtAtendente.Location = new System.Drawing.Point(72, 49);
                        txtAtendente.Name = "label9";
                        txtAtendente.Size = new System.Drawing.Size(151, 15);
                        txtAtendente.TabIndex = 12;
                        var userName = GetUsuario(idMesa) == null ? "" : GetUsuario(idMesa).Nome;
                        txtAtendente.Text = $"{userName}";

                        panel1.Controls.Add(txtSubTotal);
                        panel1.Controls.Add(SubTotal);
                        panel1.Controls.Add(txtHra);
                        panel1.Controls.Add(txtStatus);
                        panel1.Controls.Add(txtAtendente);
                        panel1.Controls.Add(panel2);
                        panel1.Controls.Add(Status);
                        panel1.Controls.Add(Atendente);
                        panel1.Controls.Add(Hra);

                        flowLayout.Controls.Add(panel1);
                    }
                }
            }
        }

        private  void Eventos()
        {
            Shown += (s, e) =>
            {
                Refresh();
                LoadMesas();
            };

            btnFechar.Click += (s, e) =>
            {
                if (checks.Count > 0)
                {
                    _mPedido.Id = 0;
                    _mPedido.Excluir = 0;
                    _mPedido.Tipo = "Vendas";
                    _mPedido.campof = "MESA";
                    _mPedido.Cliente = 1;
                    _mPedido.Save(_mPedido);
                    int idPedido = _mPedido.GetLastId();

                    foreach (string mesa in checks)
                    {
                        var dataMesa = _mPedidoItem.FindAll().Where("mesa", mesa).WhereFalse("excluir").Where("pedido", 0).Get();
                        if (dataMesa != null)
                        {
                            foreach (dynamic item in dataMesa)
                            {
                                int ID = item.ID;
                                Model.PedidoItem update = _mPedidoItem.FindById(ID).FirstOrDefault<Model.PedidoItem>();
                                update.Pedido = idPedido;
                                update.Save(update);
                            }
                        }
                    }

                    Home.pedidoPage = "Vendas";
                    AddPedidos.Id = idPedido;
                    AddPedidos.PDV = false;
                    AddPedidos NovoPedido = new AddPedidos();
                    NovoPedido.TopMost = true;
                    NovoPedido.ShowDialog();
                    LoadMesas();
                }
                else
                {
                    Alert.Message("Oppss", "Selecione uma mesa!", Alert.AlertType.warning);
                }
            };

            btnAdicionar.Click += (s, e) =>
            {
                AddItemMesa form = new AddItemMesa();
                if (form.ShowDialog() == DialogResult.OK)
                    LoadMesas();
            };

            search.TextChanged += (s, e) => LoadMesas(search.Text);
            btnAtualizar.Click += (s, e) => LoadMesas();

            btnExit.Click += (s, e) => Close();
        }

        private void actionMesa(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            Mesa.nrMesa = label.Name;
            OpenForm.Show<Mesa>(this);
        }

        private void actionCheck(object sender, EventArgs e)
        {
            VisualCheckBox check = (VisualCheckBox)sender;
            string idCheck = check.Name.Replace("check", "");
            if (check.Checked)
                checks.Add(idCheck);
            else
                checks.Remove(idCheck);

            if (checks.Count > 0)
                btnFechar.Visible = true;
            else
                btnFechar.Visible = false;
        }
    }
}
