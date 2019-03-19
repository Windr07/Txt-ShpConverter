/*---------------------------------------------------------------- 
// auth： Windragon
// date： 
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WLib.CoordCoverter.View
{
    /// <summary>
    /// 批量坐标文件转换窗口
    /// </summary>
    public partial class ConverterForm : Form
    {
        /// <summary>
        /// 批量坐标文件转换窗口
        /// </summary>
        public ConverterForm()
        {
            InitializeComponent();
            this.KeyPreview = true;

            try
            {
                this.cmbCreateMode.SelectedIndex = 0;
                this.txtShpDir.Text = AppDomain.CurrentDomain.BaseDirectory + @"result\shp";
                this.txtTextFileDir.Text = AppDomain.CurrentDomain.BaseDirectory + @"result\txt";
                Directory.CreateDirectory(this.txtShpDir.Text);
                Directory.CreateDirectory(this.txtTextFileDir.Text);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        /// <summary>
        /// 执行和结束转换操作时禁用或启用界面控件
        /// </summary>
        /// <param name="started"></param>
        public void ChangedView(bool started)
        {
            bool untiStarted = !started;
            this.sBtnSelectShp.Enabled = untiStarted;
            this.txtTextFileDir.OptEnable = untiStarted;
            this.btnRun.Enabled = untiStarted;
            this.移除RToolStripMenuItem.Enabled = untiStarted;
            this.清空CToolStripMenuItem.Enabled = untiStarted;

            this.cmbCreateMode.Enabled = untiStarted;
            this.sBtnSelectTxt.Enabled = untiStarted;
            this.txtShpDir.OptEnable = untiStarted;
            this.btnRun2.Enabled = untiStarted;
            this.移除toolStripMenuItem.Enabled = untiStarted;
            this.清空toolStripMenuItem.Enabled = untiStarted;

            this.progressBar1.Visible = started;
            this.progressBar1.Value = 0;
            Application.DoEvents();
        }
        /// <summary>
        /// 判断并筛选拖入的文件，获取指定扩展名的文件
        /// </summary>
        /// <param name="e">DragDrop、DragEnter或DragOver事件提供的数据</param>
        /// <param name="extension">文件扩展名，默认为.txt</param>
        /// <returns></returns>
        public static string[] SelectDragFile(DragEventArgs e, string extension = ".txt")
        {
            var arr = (Array)e.Data.GetData(DataFormats.FileDrop);
            var paths = new List<string>();
            for (int i = 0; i < arr.Length; i++)
            {
                var path = arr.GetValue(i).ToString();
                if (Path.GetExtension(path) != extension)
                    continue;
                paths.Add(path);
            }
            return paths.ToArray();
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)//改变TabControl标签页样式
        {
            var rec1 = tabControl1.GetTabRect(0);
            e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), rec1);
            e.Graphics.FillRectangle(new SolidBrush(Color.LightGreen), tabControl1.GetTabRect(1));
            for (int i = 0; i < this.tabControl1.TabPages.Count; i++)
            {
                var rec = tabControl1.GetTabRect(i);
                e.Graphics.DrawString(this.tabControl1.TabPages[i].Text, new Font("宋体", 10, FontStyle.Bold),
                    new SolidBrush(Color.Black), rec, new StringFormat { Alignment = StringAlignment.Center });
            }
        }

        private void dataGridView_DragEnter(object sender, DragEventArgs e)//拖入文件
        {
            //表明是链接类型的数据，比如文件路径
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Link : DragDropEffects.None;
        }

        private void dataGridView_DragDrop(object sender, DragEventArgs e)//放置文件
        {
            var dataGridView = (DataGridView)sender;
            var extension = dataGridView.Name == this.dataGridView1.Name ? ".shp" : ".txt";

            foreach (var path in SelectDragFile(e, extension))
            {
                dataGridView.Rows.Add("选项", "", Path.GetFileNameWithoutExtension(path),
                    (new FileInfo(path).Length / 1024.0).ToString("0.00"), path, "");
            }
        }

        private void ConverterForm_KeyDown(object sender, KeyEventArgs e)//显示版本信息等
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.F1)
                this.lblVersion.Show();
            else if (e.KeyCode == Keys.F1)
                linklblOption_LinkClicked(null, null);
            else if (e.KeyCode == Keys.F2)
                btnSelectShpPath_Click(null, null);
            else if (e.KeyCode == Keys.F3)
                选择工作空间ToolStripMenuItem_Click(null, null);
            else if (e.KeyCode == Keys.F4)
                btnSelectShpDir_Click(null, null);
        }

        private void linklblOption_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)//标准模式和扩展模式切换
        {
            bool isStandard = this.linklblOption.Text == "标准模式";
            this.sBtnSelectShp.SplitMenuStrip = isStandard ? this.sBtnSelectShp_ctxMenuStrip : null;
            this.sBtnSelectTxt.SplitMenuStrip = isStandard ? this.sBtnSelectTxt_ctxMenuStrip : null;
            this.选择工作空间ToolStripMenuItem.Visible = isStandard;
            this.btnGeo2Prj.Visible = isStandard;
            this.btnPrjToGeo.Visible = isStandard;
            this.cmbCreateMode.Visible = isStandard;
            this.linklblOption.Text = isStandard ? "扩展模式" : "标准模式";
            this.linklblOption2.Text = isStandard ? "扩展模式" : "标准模式";
        }
    }
}
