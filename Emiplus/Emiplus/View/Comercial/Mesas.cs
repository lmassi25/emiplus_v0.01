﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.View.Common;
using SqlKata.Execution;
using VisualPlus.Enumerators;
using VisualPlus.Structure;
using VisualPlus.Toolkit.Controls.Interactivity;
using VisualPlus.Toolkit.Controls.Layout;

namespace Emiplus.View.Comercial
{
    public partial class Mesas : Form
    {
        private readonly Model.Pedido _mPedido = new Model.Pedido();
        private readonly PedidoItem _mPedidoItem = new PedidoItem();

        private readonly List<string> checks = new List<string>();

        public Mesas()
        {
            InitializeComponent();
            Eventos();
        }

        private double GetSubTotal(string mesa)
        {
            var data = new PedidoItem().Query().SelectRaw("SUM(TOTAL) AS TOTAL").Where("mesa", mesa)
                .WhereFalse("excluir").Where("pedido", 0).FirstOrDefault<PedidoItem>();
            if (data != null)
                return data.Total;

            return 0;
        }

        private PedidoItem GetData(string mesa)
        {
            var data = new PedidoItem().FindAll().Where("mesa", mesa).WhereFalse("excluir").Where("pedido", 0)
                .FirstOrDefault<PedidoItem>();
            return data;
        }

        private Usuarios GetUsuario(string mesa)
        {
            var idUser = GetData(mesa) == null ? 0 : GetData(mesa).Usuario;
            var data = new Usuarios().FindByUserId(idUser).FirstOrDefault<Usuarios>();
            return data;
        }

        private void LoadMesas(string searchText = "")
        {
            flowLayout.Controls.Clear();

            var txtSearch = $"%{searchText}%";
            if (IniFile.Read("MesasPreCadastrada", "Comercial") == "True")
            {
                var mesas = new Model.Mesas().FindAll().WhereFalse("excluir").Where("mesa", "like", txtSearch)
                    .Get<Model.Mesas>();
                if (mesas.Any())
                    foreach (var mesa in mesas)
                    {
                        var idMesa = mesa.Mesa;

                        var pedidoItem = new PedidoItem().FindAll().WhereFalse("excluir").Where("pedido", 0)
                            .Where("mesa", idMesa).FirstOrDefault<PedidoItem>();

                        var panel1 = new VisualPanel
                        {
                            Anchor = AnchorStyles.Top | AnchorStyles.Left
                                                      | AnchorStyles.Right,
                            BackColor = Color.Transparent,
                            BackColorState
                                = {Disabled = Color.FromArgb(224, 224, 224), Enabled = Color.FromArgb(249, 249, 249)},
                            BackgroundImageLayout = ImageLayout.Zoom,
                            Border =
                            {
                                Color = Color.FromArgb(224, 224, 224),
                                HoverColor = Color.FromArgb(224, 224, 224),
                                HoverVisible = true,
                                Rounding = 6,
                                Thickness = 1,
                                Type = ShapeTypes.Rounded,
                                Visible = true
                            },
                            ForeColor = Color.FromArgb(0, 0, 0),
                            Location = new Point(3, 3),
                            MouseState = MouseStates.Normal,
                            Name = $"{idMesa}mesa",
                            Cursor = Cursors.Hand,
                            Padding = new Padding(5),
                            Size = new Size(229, 140),
                            TabIndex = 40060,
                            Text = @"visualPanel2",
                            TextStyle =
                            {
                                Disabled = Color.FromArgb(131, 129, 129),
                                Enabled = Color.FromArgb(0, 0, 0),
                                Hover = Color.FromArgb(0, 0, 0),
                                Pressed = Color.FromArgb(0, 0, 0),
                                TextAlignment = StringAlignment.Center,
                                TextLineAlignment = StringAlignment.Center,
                                TextRenderingHint = TextRenderingHint.ClearTypeGridFit
                            }
                        };

                        #region panel2

                        var panel2 = new VisualPanel
                        {
                            BackColor = Color.Gray,
                            BackColorState = {Disabled = Color.FromArgb(224, 224, 224), Enabled = Color.Gray},
                            Border =
                            {
                                Color = Color.Gray,
                                HoverColor = Color.Gray,
                                HoverVisible = true,
                                Rounding = 6,
                                Thickness = 1,
                                Type = ShapeTypes.Rounded,
                                Visible = true
                            },
                            ForeColor = Color.FromArgb(0, 0, 0),
                            Location = new Point(0, 0),
                            MouseState = MouseStates.Normal,
                            Name = "visualPanel1",
                            Padding = new Padding(5),
                            Size = new Size(229, 41),
                            TabIndex = 11,
                            Text = @"visualPanel1",
                            TextStyle =
                            {
                                Disabled = Color.FromArgb(131, 129, 129),
                                Enabled = Color.FromArgb(0, 0, 0),
                                Hover = Color.FromArgb(0, 0, 0),
                                Pressed = Color.FromArgb(0, 0, 0),
                                TextAlignment = StringAlignment.Center,
                                TextLineAlignment = StringAlignment.Center,
                                TextRenderingHint = TextRenderingHint.ClearTypeGridFit
                            }
                        };

                        var txtNrMesa = new Label
                        {
                            AutoSize = true,
                            BackColor = Color.Gray,
                            Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0),
                            ForeColor = Color.White,
                            Location = new Point(33, 8),
                            Name = $"{idMesa}",
                            Size = new Size(34, 25),
                            TabIndex = 6,
                            Text = mesa.Mesa
                        };
                        txtNrMesa.Click += ActionMesa;

                        var check = new VisualCheckBox
                        {
                            BackColor = Color.Gray,
                            Border =
                            {
                                Color = Color.White,
                                HoverColor = Color.White,
                                HoverVisible = true,
                                Rounding = 3,
                                Thickness = 1,
                                Type = ShapeTypes.Rounded,
                                Visible = true
                            },
                            Box = new Size(14, 14),
                            BoxColorState =
                            {
                                Disabled = Color.FromArgb(224, 224, 224),
                                Enabled = Color.White,
                                Hover = Color.White,
                                Pressed = Color.White
                            },
                            BoxSpacing = 2,
                            CheckStyle =
                            {
                                AutoSize = true,
                                Bounds = new Rectangle(0, 0, 125, 23),
                                Character = '✔',
                                CheckColor = Color.Gray,
                                Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point,
                                    0),
                                ShapeRounding = 3,
                                ShapeType = ShapeTypes.Rounded,
                                Style = CheckStyle.CheckType.Checkmark,
                                Thickness = 2F
                            },
                            Cursor = Cursors.Hand,
                            ForeColor = Color.FromArgb(0, 0, 0),
                            IsBoxLarger = false,
                            Location = new Point(9, 9),
                            MouseState = MouseStates.Normal,
                            Name = $"{idMesa}check",
                            Size = new Size(18, 23),
                            TabIndex = 9,
                            TextSize = new Size(0, 0),
                            TextStyle =
                            {
                                Disabled = Color.FromArgb(131, 129, 129),
                                Enabled = Color.FromArgb(0, 0, 0),
                                Hover = Color.FromArgb(0, 0, 0),
                                Pressed = Color.FromArgb(0, 0, 0),
                                TextAlignment = StringAlignment.Center,
                                TextLineAlignment = StringAlignment.Center,
                                TextRenderingHint = TextRenderingHint.ClearTypeGridFit
                            },
                            Checked = false
                        };
                        check.Click += ActionCheck;

                        if (pedidoItem == null)
                            check.Visible = false;

                        panel2.Controls.Add(check);
                        panel2.Controls.Add(txtNrMesa);

                        #endregion

                        var subTotal = new Label
                        {
                            AutoSize = true,
                            BackColor = Color.FromArgb(249, 249, 249),
                            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0),
                            ForeColor = Color.DarkGray,
                            Location = new Point(5, 112),
                            Name = "label13",
                            Size = new Size(54, 15),
                            TabIndex = 15,
                            Text = @"Subtotal:"
                        };

                        var txtSubTotal = new Label
                        {
                            AutoSize = true,
                            BackColor = Color.FromArgb(249, 249, 249),
                            Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0),
                            ForeColor = Color.Gray,
                            Location = new Point(72, 112),
                            Name = "label12",
                            Size = new Size(56, 15),
                            TabIndex = 16,
                            Text = pedidoItem != null
                                ? $"R$ {Validation.FormatPrice(GetSubTotal(idMesa))}"
                                : "R$ 00,00"
                        };


                        var hra = new Label
                        {
                            AutoSize = true,
                            BackColor = Color.FromArgb(249, 249, 249),
                            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0),
                            ForeColor = Color.DarkGray,
                            Location = new Point(5, 91),
                            Name = "label3",
                            Size = new Size(46, 15),
                            TabIndex = 7,
                            Text = @"Tempo:"
                        };

                        var txtHra = new Label
                        {
                            AutoSize = true,
                            BackColor = Color.FromArgb(249, 249, 249),
                            Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0),
                            ForeColor = Color.Gray,
                            Location = new Point(72, 91),
                            Name = "label11",
                            Size = new Size(56, 15),
                            TabIndex = 14
                        };

                        if (pedidoItem != null)
                        {
                            var date = DateTime.Now;
                            var hourMesa = date.AddHours(-GetData(idMesa).Criado.Hour);
                            var minMesa = date.AddMinutes(-GetData(idMesa).Criado.Minute);

                            txtHra.Text = $@"{hourMesa.Hour}h {minMesa.Minute}m";
                        }
                        else
                        {
                            txtHra.Text = @"00h 00m";
                        }

                        var status = new Label
                        {
                            AutoSize = true,
                            BackColor = Color.FromArgb(249, 249, 249),
                            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0),
                            ForeColor = Color.DarkGray,
                            Location = new Point(5, 70),
                            Name = "label7",
                            Size = new Size(42, 15),
                            TabIndex = 10,
                            Text = @"Status:"
                        };

                        var txtStatus = new Label
                        {
                            AutoSize = true,
                            BackColor = Color.FromArgb(249, 249, 249),
                            Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0),
                            Location = new Point(72, 70),
                            Name = "label10",
                            Size = new Size(56, 15),
                            ForeColor = pedidoItem != null ? Color.Red : Color.Green,
                            Text = pedidoItem != null ? "Ocupado" : "Livre"
                        };


                        var atendente = new Label
                        {
                            AutoSize = true,
                            BackColor = Color.FromArgb(249, 249, 249),
                            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0),
                            ForeColor = Color.DarkGray,
                            Location = new Point(5, 49),
                            Name = "label4",
                            Size = new Size(65, 15),
                            TabIndex = 8,
                            Text = @"Atendente:"
                        };

                        var txtAtendente = new Label
                        {
                            BackColor = Color.FromArgb(249, 249, 249),
                            Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0),
                            ForeColor = Color.Gray,
                            Location = new Point(72, 49),
                            Name = "label9",
                            Size = new Size(151, 15),
                            TabIndex = 12
                        };

                        if (txtStatus.Text == @"Ocupado")
                        {
                            var userName = GetUsuario(idMesa) == null ? "" : GetUsuario(idMesa).Nome;
                            txtAtendente.Text = $@"{Validation.FirstCharToUpper(userName)}";
                        }
                        else
                        {
                            txtAtendente.Text = "";
                        }

                        panel1.Controls.Add(txtSubTotal);
                        panel1.Controls.Add(subTotal);
                        panel1.Controls.Add(txtHra);
                        panel1.Controls.Add(txtStatus);
                        panel1.Controls.Add(txtAtendente);
                        panel1.Controls.Add(panel2);
                        panel1.Controls.Add(status);
                        panel1.Controls.Add(atendente);
                        panel1.Controls.Add(hra);

                        flowLayout.Controls.Add(panel1);
                    }
            }
            else
            {
                var mesas = _mPedidoItem.FindAll(new[] {"mesa"}).Where("pedido", 0).Where("mesa", "like", txtSearch)
                    .GroupBy("mesa").Get();
                if (mesas != null)
                    foreach (var mesa in mesas)
                    {
                        string idMesa = mesa.MESA.ToString();

                        var panel1 = new VisualPanel
                        {
                            Anchor = AnchorStyles.Top | AnchorStyles.Left
                                                      | AnchorStyles.Right,
                            BackColor = Color.Transparent,
                            BackColorState =
                                {Disabled = Color.FromArgb(224, 224, 224), Enabled = Color.FromArgb(249, 249, 249)},
                            BackgroundImageLayout = ImageLayout.Zoom,
                            Border =
                            {
                                Color = Color.FromArgb(224, 224, 224),
                                HoverColor = Color.FromArgb(224, 224, 224),
                                HoverVisible = true,
                                Rounding = 6,
                                Thickness = 1,
                                Type = ShapeTypes.Rounded,
                                Visible = true
                            },
                            ForeColor = Color.FromArgb(0, 0, 0),
                            Location = new Point(3, 3),
                            MouseState = MouseStates.Normal,
                            Name = $"{idMesa}mesa",
                            Cursor = Cursors.Hand,
                            Padding = new Padding(5),
                            Size = new Size(229, 140),
                            TabIndex = 40060,
                            Text = @"visualPanel2",
                            TextStyle =
                            {
                                Disabled = Color.FromArgb(131, 129, 129),
                                Enabled = Color.FromArgb(0, 0, 0),
                                Hover = Color.FromArgb(0, 0, 0),
                                Pressed = Color.FromArgb(0, 0, 0),
                                TextAlignment = StringAlignment.Center,
                                TextLineAlignment = StringAlignment.Center,
                                TextRenderingHint = TextRenderingHint.ClearTypeGridFit
                            }
                        };

                        #region panel2

                        var panel2 = new VisualPanel
                        {
                            BackColor = Color.Gray,
                            BackColorState = {Disabled = Color.FromArgb(224, 224, 224), Enabled = Color.Gray},
                            Border =
                            {
                                Color = Color.Gray,
                                HoverColor = Color.Gray,
                                HoverVisible = true,
                                Rounding = 6,
                                Thickness = 1,
                                Type = ShapeTypes.Rounded,
                                Visible = true
                            },
                            ForeColor = Color.FromArgb(0, 0, 0),
                            Location = new Point(0, 0),
                            MouseState = MouseStates.Normal,
                            Name = "visualPanel1",
                            Padding = new Padding(5),
                            Size = new Size(229, 41),
                            TabIndex = 11,
                            Text = @"visualPanel1",
                            TextStyle =
                            {
                                Disabled = Color.FromArgb(131, 129, 129),
                                Enabled = Color.FromArgb(0, 0, 0),
                                Hover = Color.FromArgb(0, 0, 0),
                                Pressed = Color.FromArgb(0, 0, 0),
                                TextAlignment = StringAlignment.Center,
                                TextLineAlignment = StringAlignment.Center,
                                TextRenderingHint = TextRenderingHint.ClearTypeGridFit
                            }
                        };

                        var txtNrMesa = new Label
                        {
                            AutoSize = true,
                            BackColor = Color.Gray,
                            Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0),
                            ForeColor = Color.White,
                            Location = new Point(33, 8),
                            Name = $"{idMesa}",
                            Size = new Size(34, 25),
                            TabIndex = 6,
                            Text = mesa.MESA.ToString()
                        };
                        txtNrMesa.Click += ActionMesa;

                        var check = new VisualCheckBox
                        {
                            Checked = false,
                            BackColor = Color.Gray,
                            Border =
                            {
                                Color = Color.White,
                                HoverColor = Color.White,
                                HoverVisible = true,
                                Rounding = 3,
                                Thickness = 1,
                                Type = ShapeTypes.Rounded,
                                Visible = true
                            },
                            Box = new Size(14, 14),
                            BoxColorState =
                            {
                                Disabled = Color.FromArgb(224, 224, 224),
                                Enabled = Color.White,
                                Hover = Color.White,
                                Pressed = Color.White
                            },
                            BoxSpacing = 2,
                            CheckStyle =
                            {
                                AutoSize = true,
                                Bounds = new Rectangle(0, 0, 125, 23),
                                Character = '✔',
                                CheckColor = Color.Gray,
                                Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point,
                                    0),
                                ShapeRounding = 3,
                                ShapeType = ShapeTypes.Rounded,
                                Style = CheckStyle.CheckType.Checkmark,
                                Thickness = 2F
                            },
                            Cursor = Cursors.Hand,
                            ForeColor = Color.FromArgb(0, 0, 0),
                            IsBoxLarger = false,
                            Location = new Point(9, 9),
                            MouseState = MouseStates.Normal,
                            Name = $"{idMesa}check",
                            Size = new Size(18, 23),
                            TabIndex = 9,
                            TextSize = new Size(0, 0),
                            TextStyle =
                            {
                                Disabled = Color.FromArgb(131, 129, 129),
                                Enabled = Color.FromArgb(0, 0, 0),
                                Hover = Color.FromArgb(0, 0, 0),
                                Pressed = Color.FromArgb(0, 0, 0),
                                TextAlignment = StringAlignment.Center,
                                TextLineAlignment = StringAlignment.Center,
                                TextRenderingHint = TextRenderingHint.ClearTypeGridFit
                            }
                        };
                        check.Click += ActionCheck;

                        panel2.Controls.Add(check);
                        panel2.Controls.Add(txtNrMesa);

                        #endregion

                        var subTotal = new Label
                        {
                            AutoSize = true,
                            BackColor = Color.FromArgb(249, 249, 249),
                            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0),
                            ForeColor = Color.DarkGray,
                            Location = new Point(5, 112),
                            Name = "label13",
                            Size = new Size(54, 15),
                            TabIndex = 15,
                            Text = @"Subtotal:"
                        };

                        var txtSubTotal = new Label
                        {
                            AutoSize = true,
                            BackColor = Color.FromArgb(249, 249, 249),
                            Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0),
                            ForeColor = Color.Gray,
                            Location = new Point(72, 112),
                            Name = "label12",
                            Size = new Size(56, 15),
                            TabIndex = 16,
                            Text = $@"R$ {Validation.FormatPrice(GetSubTotal(idMesa))}"
                        };

                        var hra = new Label
                        {
                            AutoSize = true,
                            BackColor = Color.FromArgb(249, 249, 249),
                            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0),
                            ForeColor = Color.DarkGray,
                            Location = new Point(5, 91),
                            Name = "label3",
                            Size = new Size(46, 15),
                            TabIndex = 7,
                            Text = @"Tempo:"
                        };

                        var txtHra = new Label
                        {
                            AutoSize = true,
                            BackColor = Color.FromArgb(249, 249, 249),
                            Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0),
                            ForeColor = Color.Gray,
                            Location = new Point(72, 91),
                            Name = "label11",
                            Size = new Size(56, 15),
                            TabIndex = 14
                        };

                        var date = DateTime.Now;
                        var hourMesa = date.AddHours(-GetData(idMesa).Criado.Hour);
                        var minMesa = date.AddMinutes(-GetData(idMesa).Criado.Minute);

                        txtHra.Text = $@"{hourMesa.Hour}h {minMesa.Minute}m";

                        var status = new Label
                        {
                            AutoSize = true,
                            BackColor = Color.FromArgb(249, 249, 249),
                            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0),
                            ForeColor = Color.DarkGray,
                            Location = new Point(5, 70),
                            Name = "label7",
                            Size = new Size(42, 15),
                            TabIndex = 10,
                            Text = @"Status:"
                        };

                        var txtStatus = new Label
                        {
                            AutoSize = true,
                            BackColor = Color.FromArgb(249, 249, 249),
                            Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0),
                            ForeColor = Color.Red,
                            Location = new Point(72, 70),
                            Name = "label10",
                            Size = new Size(56, 15),
                            TabIndex = 13,
                            Text = @"Ocupado"
                        };

                        var atendente = new Label
                        {
                            AutoSize = true,
                            BackColor = Color.FromArgb(249, 249, 249),
                            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0),
                            ForeColor = Color.DarkGray,
                            Location = new Point(5, 49),
                            Name = "label4",
                            Size = new Size(65, 15),
                            TabIndex = 8,
                            Text = @"Atendente:"
                        };

                        var txtAtendente = new Label
                        {
                            BackColor = Color.FromArgb(249, 249, 249),
                            Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0),
                            ForeColor = Color.Gray,
                            Location = new Point(72, 49),
                            Name = "label9",
                            Size = new Size(151, 15),
                            TabIndex = 12
                        };

                        var userName = GetUsuario(idMesa) == null ? "" : GetUsuario(idMesa).Nome;
                        txtAtendente.Text = $@"{userName}";

                        panel1.Controls.Add(txtSubTotal);
                        panel1.Controls.Add(subTotal);
                        panel1.Controls.Add(txtHra);
                        panel1.Controls.Add(txtStatus);
                        panel1.Controls.Add(txtAtendente);
                        panel1.Controls.Add(panel2);
                        panel1.Controls.Add(status);
                        panel1.Controls.Add(atendente);
                        panel1.Controls.Add(hra);

                        flowLayout.Controls.Add(panel1);
                    }
            }
        }

        private void Eventos()
        {
            Shown += (s, e) =>
            {
                Refresh();
                LoadMesas();
            };

            btnFechar.Click += (s, e) =>
            {
                var result = AlertOptions.Message("Atenção!",
                    "Você está prestes a fechar uma mesa, ao continuar não será possível voltar!" +
                    Environment.NewLine + "Deseja continuar?", AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                if (!result)
                    return;

                if (checks.Count > 0)
                {
                    _mPedido.Id = 0;
                    _mPedido.Excluir = 0;
                    _mPedido.Tipo = "Vendas";
                    _mPedido.campof = "MESA";
                    _mPedido.Cliente = 1;
                    _mPedido.Save(_mPedido);
                    var idPedido = _mPedido.GetLastId();
                    
                    foreach (var mesa in checks)
                    {
                        var dataMesa = _mPedidoItem.FindAll().Where("mesa", mesa).WhereFalse("excluir")
                            .Where("pedido", 0).Get();
                        if (dataMesa != null)
                            foreach (var item in dataMesa)
                            {
                                int id = item.ID;
                                var update = _mPedidoItem.FindById(id).FirstOrDefault<PedidoItem>();
                                update.Pedido = idPedido;
                                update.Save(update);
                            }
                    }

                    Home.pedidoPage = "Vendas";
                    AddPedidos.Id = idPedido;
                    AddPedidos.PDV = false;
                    using (var novoPedido = new AddPedidos())
                    {
                        novoPedido.TopMost = true;
                        novoPedido.ShowDialog();
                    }

                    LoadMesas();
                }
                else
                {
                    Alert.Message("Oppss", "Selecione uma mesa!", Alert.AlertType.warning);
                }
            };

            btnAdicionar.Click += (s, e) =>
            {
                var form = new AddItemMesa();
                if (form.ShowDialog() == DialogResult.OK)
                    LoadMesas();
            };

            search.TextChanged += (s, e) => LoadMesas(search.Text);
            btnAtualizar.Click += (s, e) => LoadMesas();

            btnExit.Click += (s, e) => Close();
        }

        private void ActionMesa(object sender, EventArgs e)
        {
            var label = (Label) sender;
            Mesa.nrMesa = label.Name;
            OpenForm.Show<Mesa>(this);
        }

        private void ActionCheck(object sender, EventArgs e)
        {
            var check = (VisualCheckBox) sender;
            var idCheck = check.Name.Replace("check", "");
            if (check.Checked)
                checks.Add(idCheck);
            else
                checks.Remove(idCheck);

            btnFechar.Visible = checks.Count > 0;
        }
    }
}