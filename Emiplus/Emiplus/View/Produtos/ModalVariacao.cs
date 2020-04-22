using Emiplus.Data.Helpers;
using Emiplus.Data.SobreEscrever;
using Emiplus.Properties;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Produtos
{
    public partial class ModalVariacao : Form
    {
        private Model.Item _mItem = new Model.Item();

        public static int idProduto { get; set; }

         private KeyedAutoCompleteStringCollection collection = new KeyedAutoCompleteStringCollection();

        public ModalVariacao()
        {
            InitializeComponent();
            Eventos();
        }

        private List<string> GetPermutation(int numLoops, List<List<string>> lstMaster)
        {
            int[] loopIndex = new int[numLoops];
            int[] loopCnt = new int[numLoops];

            List<string> llstRes = new List<string>();

            for(int i = 0; i < numLoops; i++) loopIndex[i] = 0;
            for(int i = 0; i < numLoops; i++) loopCnt[i] = lstMaster[i].Count;

            bool finished = false;
            while(!finished)
            {
                // access current element
                string line = "";
                for(int i = 0; i < numLoops; i++)
                {
                    line += lstMaster[i][loopIndex[i]];
                }
                llstRes.Add(line);
                int n = numLoops-1;                  
                for(;;)
                {
                    // increment innermost loop
                    loopIndex[n]++;
                    // if at Cnt: reset, increment outer loop
                    if(loopIndex[n] < loopCnt[n]) break;

                    loopIndex[n] = 0;
                    n--;
                    if(n < 0)
                    { 
                        finished=true;
                        break;
                    }
                }       
            }

            return llstRes;
        }

        private void SetHeadersTable(DataGridView Table)
        {
            Table.ColumnCount = 6;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            
            Table.RowHeadersVisible = false;

            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.HeaderText = "Selecione";
            checkColumn.Name = "Selecione";
            checkColumn.FlatStyle = FlatStyle.Standard;
            checkColumn.CellTemplate = new DataGridViewCheckBoxCell();
            checkColumn.Width = 60;
            Table.Columns.Insert(0, checkColumn);

            Table.Columns[1].Name = "ID";
            Table.Columns[1].Visible = false;

            Table.Columns[2].Name = "Combinação";
            Table.Columns[2].Width = 150;
            Table.Columns[2].Visible = true;

            Table.Columns[3].Name = "Estoque";
            Table.Columns[3].Width = 70;
            Table.Columns[3].Visible = true;

            Table.Columns[4].Name = "Referencia";
            Table.Columns[4].Width = 70;
            Table.Columns[4].Visible = true;

            Table.Columns[5].Name = "Código de Barras";
            Table.Columns[5].Width = 130;
            Table.Columns[5].Visible = true;

            Table.Columns[6].Name = "IDAttr";
            Table.Columns[6].Visible = false;
        }

        // Autocomplete no campo Grupos
        private void AutoCompleteGrupos()
        {
            IEnumerable<Model.ItemGrupo> grupos = new Model.ItemGrupo().FindAll().WhereFalse("excluir").Get<Model.ItemGrupo>();
            if (grupos != null)
            {
                foreach (Model.ItemGrupo item in grupos)
                {
                    collection.Add(item.Title, item.Id);
                }
            }

            txtGrupos.AutoCompleteCustomSource = collection;
        }

        private void AddCombination()
        {
            string[] grupos = txtBuscarVariacao.Text.Split('+');
            if (grupos.Count() == 3)
            {
                Alert.Message("Opps", "É permitido apenas 3 combinações diferente.", Alert.AlertType.error);
                return;
            }

            if (!string.IsNullOrEmpty(txtBuscarVariacao.Text) && txtBuscarVariacao.Text.Contains(txtGrupos.Text))
                return;

            if (string.IsNullOrEmpty(txtBuscarVariacao.Text))
            {
                btnClearCombinacao.Visible = true;
                txtBuscarVariacao.Text = txtGrupos.Text;
                txtGrupos.Clear();
                return;
            }

            txtBuscarVariacao.AppendText($"+{txtGrupos.Text}");

            txtGrupos.Clear();
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
                case Keys.Enter:
                    if (!string.IsNullOrEmpty(txtGrupos.Text))
                        AddCombination();
                    break;
            }
        }

        private void ClearEstoque()
        {
            if (dataGridVariacao.Rows.Count > 0) {
                bool generateAlert = AlertOptions.Message("Atenção", "Você está prestes a deletar todo o estoque de combinações, continuar?", Common.AlertBig.AlertType.info, Common.AlertBig.AlertBtn.YesNo);
                if (!generateAlert)
                    return;
                
                IEnumerable<Model.ItemEstoque> checkEstoque = new Model.ItemEstoque().FindAll().WhereFalse("excluir").Where("item", idProduto).Get<Model.ItemEstoque>();
                if (checkEstoque.Count() > 0)
                {
                    foreach (Model.ItemEstoque data in checkEstoque)
                    {
                        new Model.ItemEstoque().Remove(data.Id);
                    }
                }

                dataGridVariacao.Rows.Clear();
            }
        }

        private void Eventos()
        {
            KeyDown += KeyDowns;
            txtGrupos.KeyDown += KeyDowns;
            KeyPreview = true;
            Masks.SetToUpper(this);

            Load += (s, e) =>
            {
                ToolHelp.Show("Com essa opção marcada é possível que o sistema dê uma travada para gerar\ncada código de barras diferente um do outro.", pictureBox4, ToolHelp.ToolTipIcon.Info, "Ajuda!");
            };

            Shown += (s, e) =>
            {
                Refresh();

                _mItem = _mItem.FindById(idProduto).FirstOrDefault<Model.Item>();
                AutoCompleteGrupos();
                SetHeadersTable(dataGridVariacao);
                LoadDataGrid();

                txtGrupos.Focus();
            };

            btnAddGrupo.Click += (s, e) =>
            {
                AddCombination();
            };

            btnClearCombinacao.Click += (s, e) => 
            {
                ClearEstoque();
                txtBuscarVariacao.Text = "";
                btnClearCombinacao.Visible = false;
                btnGerar.Visible = true;
            };

            btnGerar.Click += (sender, e) =>
            {
                if (string.IsNullOrEmpty(txtBuscarVariacao.Text))
                {
                    Alert.Message("Opps", "Escolhe pelo menos um grupo de variação.", Alert.AlertType.error);
                    return;
                }

                if (dataGridVariacao.Rows.Count > 0) {
                    bool generateAlert = AlertOptions.Message("Atenção", "Ao gerar novas combinações, você irá perder a atual!\n Continuar?", Common.AlertBig.AlertType.info, Common.AlertBig.AlertBtn.YesNo);
                    if (!generateAlert)
                        return;
                    
                    dataGridVariacao.Rows.Clear();
                }

                List<string> attr1Name = new List<string>();
                List<string> attr2Name = new List<string>();
                List<string> attr3Name = new List<string>();

                List<string> attr1Id = new List<string>();
                List<string> attr2Id = new List<string>();
                List<string> attr3Id = new List<string>();

                string[] grupos = txtBuscarVariacao.Text.Split('+');
                int i_attrs = 0;
                foreach (string word in grupos)
                {
                    i_attrs++;

                    Model.ItemGrupo grupo = new Model.ItemGrupo().FindAll().WhereFalse("excluir").Where("title", word).FirstOrDefault<Model.ItemGrupo>();
                    if (grupo != null)
                    {
                        IEnumerable<Model.ItemAtributos> attr = new Model.ItemAtributos().FindAll().WhereFalse("excluir").Where("grupo", grupo.Id).Get<Model.ItemAtributos>();
                        foreach (Model.ItemAtributos data in attr)
                        {
                            switch (i_attrs)
                            {
                                case 1:
                                    attr1Name.Add($"{data.Atributo}");
                                    attr1Id.Add($"{data.Id}");
                                    break;
                                case 2:
                                    attr2Name.Add($" - {data.Atributo}");
                                    attr2Id.Add($", {data.Id}");
                                    break;
                                case 3:
                                    attr3Name.Add($" - {data.Atributo}");
                                    attr3Id.Add($", {data.Id}");
                                    break;
                            }
                        }
                    }
                }

                List<List<string>> lstMaster = new List<List<string>>();
                if (attr1Id.Count > 0)
                    lstMaster.Add(attr1Id);

                if (attr2Id.Count > 0)
                    lstMaster.Add(attr2Id);

                if (attr3Id.Count > 0)
                    lstMaster.Add(attr3Id);

                List<List<string>> lstMasterNames = new List<List<string>>();
                if (attr1Name.Count > 0)
                    lstMasterNames.Add(attr1Name);

                if (attr2Name.Count > 0)
                    lstMasterNames.Add(attr2Name);

                if (attr3Name.Count > 0)
                    lstMasterNames.Add(attr3Name);

                List<string> ids = GetPermutation(lstMaster.Count, lstMaster);
                List<string> names = GetPermutation(lstMasterNames.Count, lstMasterNames);

                if (ids.Count == names.Count)
                {
                    for (int u = 0; u < ids.Count(); u++)
                    {
                        string codeBarras = "";
                        if (checkCodeBarras.Checked)
                            codeBarras = CodeBarrasRandom();

                        dataGridVariacao.Rows.Add(
                            false,
                            ids[u],
                            names[u],
                            0,
                            "",
                            codeBarras
                            );
                    }

                    dataGridVariacao.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            };

            btnSalvar.Click += (s, e) =>
            {
                if (dataGridVariacao.Rows.Count > 0)
                {
                    Model.Item item = new Model.Item().FindById(idProduto).WhereFalse("excluir").FirstOrDefault<Model.Item>();
                    if (item != null)
                    {
                        item.Atributos = txtBuscarVariacao.Text;
                        item.Save(item);
                    }

                    foreach (DataGridViewRow row in dataGridVariacao.Rows)
                    {
                        // Apaga o registro se estoque for 0 e se existir no banco
                        if (Validation.ConvertToDouble(row.Cells["Estoque"].Value) == 0)
                        {
                            if (row.Cells["IDAttr"].Value != null && !string.IsNullOrEmpty(row.Cells["IDAttr"].Value.ToString()))
                            {
                                new Model.ItemEstoque().Remove(Validation.ConvertToInt32(row.Cells["IDAttr"].Value));
                                continue;
                            }
                        }

                        // Se a coluna for diferente de 0 ou diferente de vazio, inclui a linha no banco!
                        if (Validation.ConvertToDouble(row.Cells["Estoque"].Value) > 0)
                        {
                            string codeBarras = "";
                            if (checkCodeBarras.Checked && string.IsNullOrEmpty(row.Cells["Código de Barras"].Value.ToString()))
                                codeBarras = CodeBarrasRandom();
                            else 
                                codeBarras = row.Cells["Código de Barras"].Value.ToString();

                            // Se a coluna IDAttr não estiver vazio, atualiza o registro
                            if (row.Cells["IDAttr"].Value != null && !string.IsNullOrEmpty(row.Cells["IDAttr"].Value.ToString()))
                            {
                                Model.ItemEstoque updateEstoque = new Model.ItemEstoque().FindById(Validation.ConvertToInt32(row.Cells["IDAttr"].Value)).FirstOrDefault<Model.ItemEstoque>();
                                updateEstoque.Item = idProduto;
                                updateEstoque.Referencia = row.Cells["Referencia"].Value.ToString();
                                updateEstoque.Codebarras = codeBarras;
                                updateEstoque.Atributo = row.Cells["ID"].Value.ToString();
                                updateEstoque.Estoque = Validation.ConvertToDouble(row.Cells["Estoque"].Value);
                                updateEstoque.Usuario = Settings.Default.user_id;
                                updateEstoque.Title = row.Cells["Combinação"].Value.ToString();
                                updateEstoque.Save(updateEstoque);

                                continue;
                            }

                            Model.ItemEstoque estoque = new Model.ItemEstoque();
                            estoque.Item = idProduto;
                            estoque.Referencia = row.Cells["Referencia"].Value.ToString();
                            estoque.Codebarras = codeBarras;
                            estoque.Atributo = row.Cells["ID"].Value.ToString();
                            estoque.Estoque = Validation.ConvertToDouble(row.Cells["Estoque"].Value);
                            estoque.Usuario = Settings.Default.user_id;
                            estoque.Title = row.Cells["Combinação"].Value.ToString();
                            estoque.Save(estoque);

                            row.Cells["IDAttr"].Value = estoque.GetLastId();
                        }
                    }

                    DialogResult = DialogResult.OK;
                    Close();
                }
            };
            
            btnClose.Click += (s, e) => Close();
        }

        private void LoadDataGrid()
        {
            IEnumerable<Model.ItemEstoque> checkEstoque = new Model.ItemEstoque().FindAll().WhereFalse("excluir").Where("item", idProduto).Get<Model.ItemEstoque>();
            if (checkEstoque.Count() == 0)
                return;

            btnGerar.Visible = false;
            btnClearCombinacao.Visible = true;
            txtBuscarVariacao.Text = _mItem.Atributos;

            List<string> attr1Name = new List<string>();
            List<string> attr2Name = new List<string>();
            List<string> attr3Name = new List<string>();

            List<string> attr1Id = new List<string>();
            List<string> attr2Id = new List<string>();
            List<string> attr3Id = new List<string>();

            string[] grupos = _mItem.Atributos.Split('+');
            int i_attrs = 0;
            foreach (string word in grupos)
            {
                i_attrs++;

                Model.ItemGrupo grupo = new Model.ItemGrupo().FindAll().WhereFalse("excluir").Where("title", word).FirstOrDefault<Model.ItemGrupo>();
                if (grupo != null)
                {
                    IEnumerable<Model.ItemAtributos> attr = new Model.ItemAtributos().FindAll().WhereFalse("excluir").Where("grupo", grupo.Id).Get<Model.ItemAtributos>();
                    foreach (Model.ItemAtributos data in attr)
                    {
                        switch (i_attrs)
                        {
                            case 1:
                                attr1Name.Add($"{data.Atributo}");
                                attr1Id.Add($"{data.Id}");
                                break;
                            case 2:
                                attr2Name.Add($" - {data.Atributo}");
                                attr2Id.Add($", {data.Id}");
                                break;
                            case 3:
                                attr3Name.Add($" - {data.Atributo}");
                                attr3Id.Add($", {data.Id}");
                                break;
                        }
                    }
                }
            }

            List<List<string>> lstMaster = new List<List<string>>();
            if (attr1Id.Count > 0)
                lstMaster.Add(attr1Id);

            if (attr2Id.Count > 0)
                lstMaster.Add(attr2Id);

            if (attr3Id.Count > 0)
                lstMaster.Add(attr3Id);

            List<List<string>> lstMasterNames = new List<List<string>>();
            if (attr1Name.Count > 0)
                lstMasterNames.Add(attr1Name);

            if (attr2Name.Count > 0)
                lstMasterNames.Add(attr2Name);

            if (attr3Name.Count > 0)
                lstMasterNames.Add(attr3Name);

            List<string> ids = GetPermutation(lstMaster.Count, lstMaster);
            List<string> names = GetPermutation(lstMasterNames.Count, lstMasterNames);

            if (ids.Count == names.Count)
            {
                for (int u = 0; u < ids.Count(); u++)
                {
                    string codeBarras = "";
                    if (checkCodeBarras.Checked)
                        codeBarras = CodeBarrasRandom();

                    dataGridVariacao.Rows.Add(
                        false,
                        ids[u],
                        names[u],
                        0,
                        "",
                        codeBarras
                        );
                }

                if (checkEstoque != null && checkEstoque.Count() > 0)
                {
                    // Verifica se existe algum linha
                    if (dataGridVariacao.Rows.Count > 0)
                    {
                        // Percorre todas as linhas do datagrid
                        foreach (DataGridViewRow row in dataGridVariacao.Rows)
                        {
                            // Percorre todos dados do banco, comparando as linhas do grid
                            foreach (Model.ItemEstoque item in checkEstoque)
                            {
                                Console.WriteLine(row.Cells["ID"].Value.ToString());
                                Console.WriteLine(item.Atributo);
                                if (row.Cells["ID"].Value.ToString() == item.Atributo)
                                {
                                    row.Cells["Referencia"].Value = item.Referencia;
                                    row.Cells["Código de Barras"].Value = item.Codebarras;
                                    row.Cells["Estoque"].Value = item.Estoque;
                                    row.Cells["IDAttr"].Value = item.Id;
                                }
                            }

                        }
                    }
                }

                dataGridVariacao.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private string CodeBarrasRandom()
        {
            Thread.Sleep(250);
            return Validation.CodeBarrasRandom();
        }
    }
}
