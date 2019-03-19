/*---------------------------------------------------------------- 
// auth： Windragon
// date： 
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OSGeo.OGR;
using WLib.CoordCoverter.Utility;
using WLib.CoordCoverter.View.Control;

namespace WLib.CoordCoverter.View
{
    public partial class OpenDataSourceForm : Form
    {
        private bool _stopRunning;//是否停止运行

        public OpenDataSourceForm()
        {
            InitializeComponent();

            txtTextFileDir.Text = AppDomain.CurrentDomain.BaseDirectory + @"result\txt";
        }

        private void ChangedView(bool started)
        {
            bool untiStarted = !started;
            dataSourceSelector1.OptEnable = untiStarted;
            tableLayoutPanel1.Enabled = untiStarted;
            txtTextFileDir.OptEnable = untiStarted;
            colCheck.ReadOnly = started;
            选中SToolStripMenuItem.Enabled = untiStarted;
            不选DToolStripMenuItem.Enabled = untiStarted;
            全选AToolStripMenuItem.Enabled = untiStarted;
            反选NToolStripMenuItem.Enabled = untiStarted;
            清除选择CToolStripMenuItem.Enabled = untiStarted;
            btnRun.Enabled = untiStarted;
            btnStop.Enabled = started;
            btnStop.Visible = started;
            progressBar1.Visible = started;
            progressBar1.Value = 0;
            Application.DoEvents();
        }

        private void dataSourceSelector1_WorkspaceTypeChanged(object sender, EventArgs e)
        {
            cmbLayers.Items.Clear();
            cmbIdField.Items.Clear();
            cmbNameField.Items.Clear();
            dataGridView1.Rows.Clear();
            if (dataSourceSelector1.WorkspaceType == EWorkspaceType.Sql)
                dataSourceSelector1.PathOrConnStr = "MSSQL:server=localhost;uid=;pwd=;database=";
        }

        private void dataSourceSelector1_AfterSelectPath(object sender, EventArgs e)
        {
            cmbLayers.Items.Clear();
            dataGridView1.Rows.Clear();
            if (dataSourceSelector1.GdalDataSource != null)
            {
                cmbLayers.Items.AddRange(dataSourceSelector1.LayerNames);
                if (cmbLayers.Items.Count > 0)
                    cmbLayers.SelectedIndex = 0;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == colOpt.Index)
                contextMenuStrip1.Show(MousePosition);
        }

        private void cmbLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbIdField.Items.Clear();
            cmbNameField.Items.Clear();
            if (cmbLayers.SelectedIndex > -1)
            {
                try
                {
                    cmbNameField.Items.Add("<无>");
                    var layer = dataSourceSelector1.GetLayerByName(cmbLayers.SelectedItem.ToString());
                    FeatureDefn featureDefn = layer.GetLayerDefn();
                    for (int i = 0; i < featureDefn.GetFieldCount(); i++)
                    {
                        var fieldName = featureDefn.GetFieldDefn(i).GetName();
                        cmbNameField.Items.Add(fieldName);
                        cmbIdField.Items.Add(fieldName);
                    }
                    if (cmbIdField.Items.Contains("XMID"))
                    {
                        cmbIdField.SelectedItem = "XMID";
                        if (cmbNameField.Items.Contains("XMMC"))
                            cmbNameField.SelectedItem = "XMMC";
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void cmbIdField_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            cmbNameField.Enabled = cmbIdField.SelectedIndex > -1;

            try
            {
                if (cmbIdField.SelectedIndex > -1)
                {
                    var xmidFieldName = cmbIdField.SelectedItem.ToString();
                    var layerName = cmbLayers.SelectedItem.ToString();
                    var sql = $"select distinct({xmidFieldName}) from \"{layerName}\"";
                    var layer = dataSourceSelector1.GdalDataSource.ExecuteSQL(sql, null, null);

                    var xmidList = new List<string>();
                    Feature feature = null;
                    while ((feature = layer.GetNextFeature()) != null)
                    {
                        xmidList.Add(feature.GetFieldAsString(xmidFieldName));
                    }

                    foreach (var xmid in xmidList)
                    {
                        dataGridView1.Rows.Add(true, xmid, "", "选项", "", "");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbNameField_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbNameField.SelectedIndex > -1 && cmbNameField.SelectedItem.ToString() != "<无>")
                {
                    var xmidFieldName = cmbIdField.SelectedItem.ToString();
                    var nameFieldName = cmbNameField.SelectedItem.ToString();
                    var layerName = cmbLayers.SelectedItem.ToString();
                    var sql = $"select {xmidFieldName}, {nameFieldName} from \"{layerName}\"";
                    var layer = dataSourceSelector1.GdalDataSource.ExecuteSQL(sql, null, null);

                    var dict = new Dictionary<string, string>();
                    Feature feature = null;
                    while ((feature = layer.GetNextFeature()) != null)
                    {
                        var id = feature.GetFieldAsString(xmidFieldName);
                        if (!dict.ContainsKey(id))
                            dict.Add(id, feature.GetFieldAsString(nameFieldName));
                    }

                    int i = 0;
                    foreach (var pair in dict)
                    {
                        dataGridView1.Rows[i++].Cells["colName"].Value = pair.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenDataSourceForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.F1)
                lblVersion.Show();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
                return;

            ChangedView(true);
            try
            {
                _stopRunning = false;
                var rows = dataGridView1.Rows;
                progressBar1.Maximum = rows.Count;
                foreach (DataGridViewRow row in rows) { row.Cells["colResult"].Value = string.Empty; }
                Application.DoEvents();

                var layerName = cmbLayers.SelectedItem.ToString();
                var layer = dataSourceSelector1.GetLayerByName(layerName);
                var idFieldName = cmbIdField.SelectedItem.ToString();
                var txtFileDir = txtTextFileDir.Text.Trim();
                var featureDefn = layer.GetLayerDefn();
                var idField = featureDefn.GetFieldDefn(featureDefn.GetFieldIndex(idFieldName));
                var sqlFormat = idField.GetFieldType() == FieldType.OFTString ? "{0} = '{1}'" : "{0} = {1}";
                var fileNameFormat = cmbNameField.SelectedIndex > 0 ? "{0}_{1}.txt" : "{0}.txt";

                foreach (DataGridViewRow row in rows)
                {
                    if (Convert.ToBoolean(row.Cells["colCheck"].Value) == true)
                    {
                        try
                        {
                            var id = row.Cells["colID"].Value.ToString();
                            var name = row.Cells["colName"].Value.ToString();
                            var filePath = Path.Combine(txtFileDir, string.Format(fileNameFormat, id, name));
                            var features = GdalHelper.GetFeatures(layer, string.Format(sqlFormat, idFieldName, id));
                            var txtInfo = RedLineManager.GetProjInfoFromFeatures(features.ToArray());
                            RedLineManager.ToTxtFile(filePath, txtInfo);

                            row.Cells["colResultPath"].Value = filePath;
                            row.Cells["colResult"].Value = "成功";
                        }
                        catch (Exception ex)
                        {
                            row.Cells["colResult"].Value = "失败：" + ex.Message;
                        }
                    }
                    progressBar1.Value++;
                    Application.DoEvents();
                    if (_stopRunning)
                        break;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
            ChangedView(false);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _stopRunning = true;
            btnStop.Enabled = false;
        }

        #region  右键菜单

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var text = ((ToolStripMenuItem)sender).Text;//菜单项上显示文本
            var cnText = new Regex(@"\w+[^(]").Match(text).Value;//匹配括号前的字符 //匹配中文@"[\u4e00-\u9fa5]"
            switch (cnText)
            {
                case "选中": foreach (DataGridViewRow row in dataGridView1.SelectedRows) { row.Cells["colCheck"].Value = true; } break;
                case "不选": foreach (DataGridViewRow row in dataGridView1.SelectedRows) { row.Cells["colCheck"].Value = false; } break;
                case "全选": foreach (DataGridViewRow row in dataGridView1.Rows) { row.Cells["colCheck"].Value = true; } break;
                case "反选": foreach (DataGridViewRow row in dataGridView1.Rows) { row.Cells["colCheck"].Value = !Convert.ToBoolean(row.Cells["colCheck"].Value); } break;
                case "清除选择": foreach (DataGridViewRow row in dataGridView1.Rows) { row.Cells["colCheck"].Value = false; } break;
            }
        }

        private void 打开生成的txt文件OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var rows = dataGridView1.SelectedRows;
            if (rows.Count <= 0) return;

            var objPath = rows[0].Cells["colResultPath"].Value?.ToString();
            if (File.Exists(objPath)) Process.Start(objPath);
        }

        private void 打开生成的txt文件位置LToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var rows = dataGridView1.SelectedRows;
            if (rows.Count <= 0) return;

            var objPath = rows[0].Cells["colResultPath"].Value?.ToString();
            if ( File.Exists(objPath)) Process.Start("explorer.exe", "/select," + objPath);
        }

        private void 复制名称ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var rows = dataGridView1.SelectedRows;
            if (rows.Count > 0) Clipboard.SetDataObject(rows[0].Cells["colName"].Value.ToString());
        }

        private void 复制IDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var rows = dataGridView1.SelectedRows;
            if (rows.Count > 0) Clipboard.SetDataObject(rows[0].Cells["colID"].Value.ToString());
        }

        private void 设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
                return;
            var layerName = cmbLayers.SelectedItem.ToString();
            var layer = dataSourceSelector1.GetLayerByName(layerName);
            new ShpToTxtSettingsForm(layer).ShowDialog();
        }

        private void 查找FToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new InputForm("查找", "查找并勾选", "查找项目ID或名称，多个项目以逗号“,”隔开");
            if (form.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(form.KeyWord))
            {
                foreach (DataGridViewRow row in dataGridView1.Rows) { row.Cells["colCheck"].Value = false; }//清除选择

                var keywords = form.KeyWord.Split(new[] { ',', '，' }, StringSplitOptions.RemoveEmptyEntries);
                if (keywords.Length == 0) return;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    foreach (var keyword in keywords)
                    {
                        if (row.Cells["colID"].Value.ToString().Contains(keyword) ||
                            row.Cells["colName"].Value.ToString().Contains(keyword))
                            row.Cells["colCheck"].Value = true;
                    }
                }
            }
        }
        #endregion
    }
}
