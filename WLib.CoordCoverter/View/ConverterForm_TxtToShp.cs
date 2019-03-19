/*---------------------------------------------------------------- 
// auth： Windragon
// date： 
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using OSGeo.OGR;
using WLib.CoordCoverter.Model;

namespace WLib.CoordCoverter.View
{
    public partial class ConverterForm
    {
        private string _selectedTxtDir;
        private string _firstTxtPath;

        private void btnSelectTxtPath_Click(object sender, EventArgs e)//选择txt文件
        {
            openFileDlg.Filter = @"*.txt|*.txt";
            openFileDlg.FileName = _firstTxtPath;
            if (openFileDlg.ShowDialog() == DialogResult.OK)
            {
                _firstTxtPath = openFileDlg.FileNames[0];
                foreach (var path in openFileDlg.FileNames)
                {
                    this.dataGridView2.Rows.Add("选项", "", Path.GetFileNameWithoutExtension(path),
                        (new FileInfo(path).Length / 1024.0).ToString("0.00"), path, "");
                }
            }
        }

        private void btnSelectTxtDir_Click(object sender, EventArgs e)//选择txt目录
        {
            var folderBrowserDlg = new FolderBrowserDialog { SelectedPath = _selectedTxtDir };
            if (folderBrowserDlg.ShowDialog() == DialogResult.OK)
            {
                _selectedTxtDir = folderBrowserDlg.SelectedPath;
                var paths = Directory.GetFiles(folderBrowserDlg.SelectedPath, "*.txt", SearchOption.AllDirectories);
                foreach (var path in paths)
                {
                    this.dataGridView2.Rows.Add("选项", "", Path.GetFileNameWithoutExtension(path),
                        (new FileInfo(path).Length / 1024.0).ToString("0.00"), path, "");
                }
            }
        }

        private void cbSaveToSourceShpDir_CheckedChanged(object sender, EventArgs e)//shp文件保存到txt文件所在位置
        {
            this.txtShpDir.Visible = !this.cbSaveToSourceTxtDir.Checked;
        }

        private void btnRun2_Click(object sender, EventArgs e)//转换
        {
            var isSaveToSource = this.cbSaveToSourceTxtDir.Checked;
            var shpDir = this.txtShpDir.Text.Trim();
            if (!isSaveToSource || this.cmbCreateMode.SelectedIndex == 1)
            {
                if (!Directory.Exists(shpDir) && !File.Exists(shpDir))
                {
                    MessageBox.Show(@"shp文件保存路径不存在，请选择正确的shp文件保存路径！", this.Text, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
            }

            ChangedView(true);
            var rows = this.dataGridView2.Rows;
            this.progressBar1.Maximum = rows.Count;
            foreach (DataGridViewRow row in rows) { row.Cells["colResult2"].Value = ""; }

            if (this.cmbCreateMode.SelectedIndex == 0)
                CreateToEachShp(rows, isSaveToSource);
            else
                CreateToOneShp(rows);

            ChangedView(false);
        }

        private void CreateToEachShp(DataGridViewRowCollection rows, bool isSaveToSource)
        {
            var shpDir = this.txtShpDir.Text.Trim();
            for (int i = 0; i < rows.Count; i++)
            {
                try
                {
                    var path = rows[i].Cells["colFilePath2"].Value.ToString();
                    if (isSaveToSource)
                        shpDir = Path.GetDirectoryName(path);

                    var name = rows[i].Cells["colFileName2"].Value.ToString();
                    var resultPath = Path.Combine(shpDir, name + ".shp");
                    if (File.Exists(path))
                        RedLineManager.TxtToShp(path, resultPath);

                    rows[i].Cells["colResultPath2"].Value = resultPath;
                    rows[i].Cells["colResult2"].Value = "成功";
                }
                catch (Exception ex)
                {
                    rows[i].Cells["colResult2"].Value = ex.Message.Contains("GDAL_DATA")
                        ? "失败：要确保正常txt转shapefile，请将本工具放置在不含中文的目录中（" + ex.Message + ")，且勿删除Data文件夹"
                        : "失败：" + ex.Message;
                }
                this.progressBar1.Value++;
                Application.DoEvents();
            }
        }

        private void CreateToOneShp(DataGridViewRowCollection rows)
        {
            var shpInfos = new List<ShpSourceInfo>();
            var shpDir = this.txtShpDir.Text.Trim();

            for (int i = 0; i < rows.Count; i++)
            {
                try
                {
                    var path = rows[i].Cells["colFilePath2"].Value.ToString();
                    if (File.Exists(path))
                    {
                        var projInfo = RedLineManager.GetProjInfoFromTxt(path);
                        ShpSourceInfo shpInfo = null;
                        foreach (var info in shpInfos)
                        {
                            if (info.Wkid == projInfo.Wkid) { shpInfo = info; break; }
                        }
                        Layer layer;
                        DataSource dataSource;
                        if (shpInfo == null)
                        {
                            var shpName = $"DH{projInfo.ProjZone}_{projInfo.Wkid}_{DateTime.Now:yyyy_MM_dd_HH_mm_ss}.shp";
                            var shpPath = Path.Combine(shpDir, shpName);
                            dataSource = RedLineManager.CreateShapefileSource(shpPath);
                            layer = RedLineManager.CreateRedLineLayer(dataSource, projInfo.Wkid, shpPath);
                            shpInfo = new ShpSourceInfo { Wkid = projInfo.Wkid, ShpPath = shpPath };
                            shpInfos.Add(shpInfo);
                        }
                        else
                        {
                            var shpPath = shpInfo.ShpPath;
                            dataSource = Ogr.Open(shpPath, 1);
                            layer = dataSource.GetLayerByName(Path.GetFileNameWithoutExtension(shpPath));
                        }
                        RedLineManager.ToLayer(projInfo, layer);
                        rows[i].Cells["colResultPath2"].Value = shpInfo.ShpPath;
                        rows[i].Cells["colResult2"].Value = "成功";
                    }
                }
                catch (Exception ex)
                {
                    rows[i].Cells["colResult2"].Value = ex.Message.Contains("GDAL_DATA")
                        ? "失败：要确保正常txt转shapefile，请将本工具放置在不含中文的目录中（" + ex.Message + ")，且勿删除Data文件夹"
                        : "失败：" + ex.Message;
                }
                this.progressBar1.Value++;
                Application.DoEvents();
            }
        }

        private void cmbCreateMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cbSaveToSourceTxtDir.Visible = this.cmbCreateMode.SelectedIndex == 0;
            if (this.cmbCreateMode.SelectedIndex == 1)
                this.cbSaveToSourceTxtDir.Checked = false;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.colOpt2.Index)
                this.contextMenuStrip2.Show(MousePosition);
        }

        private void 打开txt文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView2.SelectedRows.Count <= 0) return;

            var path = this.dataGridView2.SelectedRows[0].Cells["colFilePath2"].Value.ToString();
            System.Diagnostics.Process.Start(path);
        }

        private void 打开txt文件位置toolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView2.SelectedRows.Count <= 0) return;

            var path = this.dataGridView2.SelectedRows[0].Cells["colFilePath2"].Value.ToString();
            System.Diagnostics.Process.Start("explorer.exe", "/select," + path);
        }

        private void 打开生成的shp文件位置toolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView2.SelectedRows.Count <= 0) return;

            var path = this.dataGridView2.SelectedRows[0].Cells["colResultPath2"].Value.ToString();
            if (File.Exists(path)) System.Diagnostics.Process.Start("explorer.exe", "/select," + path);
        }

        private void 移除toolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.dataGridView2.SelectedRows)
                this.dataGridView2.Rows.Remove(row);
        }

        private void 清空toolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.dataGridView2.Rows.Clear();
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            打开txt文件ToolStripMenuItem_Click(null, null);
        }
    }
}
