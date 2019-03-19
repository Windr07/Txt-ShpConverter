/*---------------------------------------------------------------- 
// auth： Windragon
// date： 
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.IO;
using System.Windows.Forms;
using WLib.CoordCoverter.Utility;

namespace WLib.CoordCoverter.View
{
    public partial class ConverterForm
    {
        private string _tmpShpDir;
        private string _tmpShpPath;

        private void btnSelectShpPath_Click(object sender, EventArgs e)//选择shp文件
        {
            openFileDlg.Filter = @"*.shp|*.shp";
            openFileDlg.FileName = _tmpShpPath;
            if (openFileDlg.ShowDialog() == DialogResult.OK)
            {
                _tmpShpPath = openFileDlg.FileNames[0];
                foreach (var path in openFileDlg.FileNames)
                {
                    this.dataGridView1.Rows.Add("选项", "", Path.GetFileNameWithoutExtension(path),
                        (new FileInfo(path).Length / 1024.0).ToString("0.00"), path, "");
                }
            }
        }

        private void btnSelectShpDir_Click(object sender, EventArgs e)//选择shp目录
        {
            var folderBrowserDlg = new FolderBrowserDialog { SelectedPath = _tmpShpDir };
            if (folderBrowserDlg.ShowDialog() == DialogResult.OK)
            {
                _tmpShpDir = folderBrowserDlg.SelectedPath;
                var paths = Directory.GetFiles(folderBrowserDlg.SelectedPath, "*.shp", SearchOption.AllDirectories);
                foreach (var path in paths)
                {
                    this.dataGridView1.Rows.Add("选项", "", Path.GetFileNameWithoutExtension(path),
                        (new FileInfo(path).Length / 1024.0).ToString("0.00"), path, "");
                }
            }
        }

        private void 选择工作空间ToolStripMenuItem_Click(object sender, EventArgs e)//选择工作空间
        {
            var form = new OpenDataSourceForm();
            form.FormClosed += delegate { this.Show(); };
            form.Show();
            this.Hide();
        }

        private void cbSaveToSourceTxtDir_CheckedChanged(object sender, EventArgs e)//txt文件保存到shp文件所在位置
        {
            this.txtTextFileDir.Visible = !this.cbSaveToSourceShpDir.Checked;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)//弹出操作菜单
        {
            if (e.ColumnIndex == this.colOpt.Index)
                this.contextMenuStrip1.Show(MousePosition);
        }

        private void btnRun_Click(object sender, EventArgs e)//转换
        {
            bool isSaveToSource = this.cbSaveToSourceShpDir.Checked;
            string txtDir = null;
            if (!isSaveToSource)
            {
                if (!Directory.Exists(txtDir = this.txtTextFileDir.Text.Trim()))
                {
                    MessageBox.Show(@"TXT保存目录不存在，请选择正确的TXT保存目录！", @"坐标文件转换", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            ChangedView(true);
            var rows = this.dataGridView1.Rows;
            this.progressBar1.Maximum = rows.Count;

            foreach (DataGridViewRow row in rows) { row.Cells["colResult"].Value = ""; }

            for (int i = 0; i < rows.Count; i++)
            {
                try
                {
                    var path = rows[i].Cells["colFilePath"].Value.ToString();
                    var name = rows[i].Cells["colFileName"].Value.ToString();
                    if (isSaveToSource)
                        txtDir = Path.GetDirectoryName(path);
                    var resultPath = Path.Combine(txtDir, name + ".txt");
                    if (File.Exists(path))
                        RedLineManager.ShpToTxt(path, resultPath);

                    rows[i].Cells["colResultPath"].Value = resultPath;
                    rows[i].Cells["colResult"].Value = "成功";
                }
                catch (Exception ex)
                {
                    rows[i].Cells["colResult"].Value = "失败：" + ex.Message;
                }
                this.progressBar1.Value++;
                Application.DoEvents();
            }
            ChangedView(false);
        }

        private void 打开文件位置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count <= 0) return;

            var path = this.dataGridView1.SelectedRows[0].Cells["colFilePath"].Value.ToString();
            System.Diagnostics.Process.Start("explorer.exe", "/select," + path);
        }

        private void 打开生成的txt文件OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count <= 0) return;

            var path = this.dataGridView1.SelectedRows[0].Cells["colResultPath"].Value.ToString();
            if (File.Exists(path)) System.Diagnostics.Process.Start("explorer.exe", path);
        }

        private void 打开生成的txt文件位置GToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count <= 0) return;

            var path = this.dataGridView1.SelectedRows[0].Cells["colResultPath"].Value.ToString();
            if (File.Exists(path)) System.Diagnostics.Process.Start("explorer.exe", "/select," + path);
        }

        private void 移除RToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
                this.dataGridView1.Rows.Remove(row);
        }

        private void 清空CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Clear();
        }

        private void 设置SToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.Rows.Count <= 0)
                return;
            var path = this.dataGridView1.Rows[0].Cells["colFilePath"].Value.ToString();
            new ShpToTxtSettingsForm(path).ShowDialog();
        }

        private void btnPrjToGeo_Click(object sender, EventArgs e)
        {
            bool isSaveToSource = this.cbSaveToSourceShpDir.Checked;
            string txtDir = null;
            if (!isSaveToSource)
            {
                if (!Directory.Exists(txtDir = this.txtTextFileDir.Text.Trim()))
                {
                    MessageBox.Show(@"TXT保存目录不存在，请选择正确的TXT保存目录！", @"坐标文件转换", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            ChangedView(true);
            var rows = this.dataGridView1.Rows;
            this.progressBar1.Maximum = rows.Count;

            foreach (DataGridViewRow row in rows) { row.Cells["colResult"].Value = ""; }

            for (int i = 0; i < rows.Count; i++)
            {
                try
                {
                    var path = rows[i].Cells["colFilePath"].Value.ToString();
                    var name = rows[i].Cells["colFileName"].Value.ToString();
                    if (isSaveToSource)
                        txtDir = Path.GetDirectoryName(path);
                    var resultPath = Path.Combine(txtDir, name + "_Prj2Geo.shp");
                    if (File.Exists(path))
                        GdalHelper.ConvertSpatialRef(path, resultPath);

                    rows[i].Cells["colResultPath"].Value = resultPath;
                    rows[i].Cells["colResult"].Value = "成功";
                }
                catch (Exception ex)
                {
                    rows[i].Cells["colResult"].Value = "失败：" + ex.Message;
                }
                this.progressBar1.Value++;
                Application.DoEvents();
            }
            ChangedView(false);
        }

        private void btnGeo2Prj_Click(object sender, EventArgs e)
        {
            bool isSaveToSource = this.cbSaveToSourceShpDir.Checked;
            string txtDir = null;
            if (!isSaveToSource)
            {
                if (!Directory.Exists(txtDir = this.txtTextFileDir.Text.Trim()))
                {
                    MessageBox.Show(@"TXT保存目录不存在，请选择正确的TXT保存目录！", @"坐标文件转换", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            ChangedView(true);
            var rows = this.dataGridView1.Rows;
            this.progressBar1.Maximum = rows.Count;

            foreach (DataGridViewRow row in rows) { row.Cells["colResult"].Value = ""; }

            for (int i = 0; i < rows.Count; i++)
            {
                try
                {
                    var path = rows[i].Cells["colFilePath"].Value.ToString();
                    var name = rows[i].Cells["colFileName"].Value.ToString();
                    if (isSaveToSource)
                        txtDir = Path.GetDirectoryName(path);

                    var resultPath = Path.Combine(txtDir, name + "_Prj2Geo.shp");
                    if (File.Exists(path))
                        GdalHelper.ConvertSpatialRef(path, resultPath, null, false);

                    rows[i].Cells["colResultPath"].Value = resultPath;
                    rows[i].Cells["colResult"].Value = "成功";
                }
                catch (Exception ex)
                {
                    rows[i].Cells["colResult"].Value = "失败：" + ex.Message;
                }
                this.progressBar1.Value++;
                Application.DoEvents();
            }
            ChangedView(false);
        }
    }
}
