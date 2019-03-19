using System.Windows.Forms;
using wyDay.Controls;
using WLib.CoordCoverter.View.Control;

namespace WLib.CoordCoverter.View
{
    partial class ConverterForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnRun = new System.Windows.Forms.Button();
            this.openFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDlg = new System.Windows.Forms.SaveFileDialog();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colOpt = new System.Windows.Forms.DataGridViewLinkColumn();
            this.colResult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFileSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFilePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colResultPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.打开shp文件位置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开生成的txt文件OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开生成的txt文件位置GToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.设置SToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.移除RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清空CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabShpToTxt = new System.Windows.Forms.TabPage();
            this.linklblOption = new System.Windows.Forms.LinkLabel();
            this.btnGeo2Prj = new System.Windows.Forms.Button();
            this.btnPrjToGeo = new System.Windows.Forms.Button();
            this.lblVersion = new System.Windows.Forms.Label();
            this.txtTextFileDir = new WLib.CoordCoverter.View.Control.PathBoxSimple();
            this.sBtnSelectShp = new wyDay.Controls.SplitButton();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSaveToSourceShpDir = new System.Windows.Forms.CheckBox();
            this.tabTxt2Shp = new System.Windows.Forms.TabPage();
            this.linklblOption2 = new System.Windows.Forms.LinkLabel();
            this.sBtnSelectTxt = new wyDay.Controls.SplitButton();
            this.cbSaveToSourceTxtDir = new System.Windows.Forms.CheckBox();
            this.cmbCreateMode = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.colOpt2 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.colResult2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFileName2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFileSize2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFilePath2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colResultPath2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.打开txt文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开txt文件位置toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开生成的shp文件位置toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.移除toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清空toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRun2 = new System.Windows.Forms.Button();
            this.txtShpDir = new WLib.CoordCoverter.View.Control.PathBoxSimple();
            this.sBtnSelectTxt_ctxMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.选择txt目录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sBtnSelectShp_ctxMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.选择shp目录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.选择工作空间ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabShpToTxt.SuspendLayout();
            this.tabTxt2Shp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.sBtnSelectTxt_ctxMenuStrip.SuspendLayout();
            this.sBtnSelectShp_ctxMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRun
            // 
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRun.Location = new System.Drawing.Point(719, 358);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(129, 41);
            this.btnRun.TabIndex = 1;
            this.btnRun.Text = "转成txt";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // openFileDlg
            // 
            this.openFileDlg.FileName = "openFileDialog1";
            this.openFileDlg.Multiselect = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowDrop = true;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colOpt,
            this.colResult,
            this.colFileName,
            this.colFileSize,
            this.colFilePath,
            this.colResultPath});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Location = new System.Drawing.Point(8, 45);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(839, 269);
            this.dataGridView1.TabIndex = 7;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.dataGridView_DragDrop);
            this.dataGridView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.dataGridView_DragEnter);
            // 
            // colOpt
            // 
            this.colOpt.HeaderText = "";
            this.colOpt.Name = "colOpt";
            this.colOpt.Width = 46;
            // 
            // colResult
            // 
            this.colResult.HeaderText = "生成结果";
            this.colResult.Name = "colResult";
            this.colResult.ReadOnly = true;
            this.colResult.Width = 126;
            // 
            // colFileName
            // 
            this.colFileName.HeaderText = "文件名";
            this.colFileName.Name = "colFileName";
            this.colFileName.ReadOnly = true;
            this.colFileName.Width = 160;
            // 
            // colFileSize
            // 
            this.colFileSize.HeaderText = "大小(KB)";
            this.colFileSize.Name = "colFileSize";
            this.colFileSize.ReadOnly = true;
            // 
            // colFilePath
            // 
            this.colFilePath.HeaderText = "文件路径";
            this.colFilePath.Name = "colFilePath";
            this.colFilePath.ReadOnly = true;
            this.colFilePath.Width = 360;
            // 
            // colResultPath
            // 
            this.colResultPath.HeaderText = "txt保存路径";
            this.colResultPath.Name = "colResultPath";
            this.colResultPath.Visible = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开shp文件位置ToolStripMenuItem,
            this.打开生成的txt文件OToolStripMenuItem,
            this.打开生成的txt文件位置GToolStripMenuItem,
            this.toolStripSeparator1,
            this.设置SToolStripMenuItem1,
            this.toolStripSeparator4,
            this.移除RToolStripMenuItem,
            this.清空CToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(216, 148);
            // 
            // 打开shp文件位置ToolStripMenuItem
            // 
            this.打开shp文件位置ToolStripMenuItem.Name = "打开shp文件位置ToolStripMenuItem";
            this.打开shp文件位置ToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.打开shp文件位置ToolStripMenuItem.Text = "打开shp文件位置(&L)";
            this.打开shp文件位置ToolStripMenuItem.Click += new System.EventHandler(this.打开文件位置ToolStripMenuItem_Click);
            // 
            // 打开生成的txt文件OToolStripMenuItem
            // 
            this.打开生成的txt文件OToolStripMenuItem.Name = "打开生成的txt文件OToolStripMenuItem";
            this.打开生成的txt文件OToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.打开生成的txt文件OToolStripMenuItem.Text = "打开生成的txt文件(&O)";
            this.打开生成的txt文件OToolStripMenuItem.Click += new System.EventHandler(this.打开生成的txt文件OToolStripMenuItem_Click);
            // 
            // 打开生成的txt文件位置GToolStripMenuItem
            // 
            this.打开生成的txt文件位置GToolStripMenuItem.Name = "打开生成的txt文件位置GToolStripMenuItem";
            this.打开生成的txt文件位置GToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.打开生成的txt文件位置GToolStripMenuItem.Text = "打开生成的txt文件位置(&G)";
            this.打开生成的txt文件位置GToolStripMenuItem.Click += new System.EventHandler(this.打开生成的txt文件位置GToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(212, 6);
            // 
            // 设置SToolStripMenuItem1
            // 
            this.设置SToolStripMenuItem1.Name = "设置SToolStripMenuItem1";
            this.设置SToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.设置SToolStripMenuItem1.Size = new System.Drawing.Size(215, 22);
            this.设置SToolStripMenuItem1.Text = "设置(&S)";
            this.设置SToolStripMenuItem1.Click += new System.EventHandler(this.设置SToolStripMenuItem1_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(212, 6);
            // 
            // 移除RToolStripMenuItem
            // 
            this.移除RToolStripMenuItem.Name = "移除RToolStripMenuItem";
            this.移除RToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.移除RToolStripMenuItem.Text = "移除(&R)";
            this.移除RToolStripMenuItem.Click += new System.EventHandler(this.移除RToolStripMenuItem_Click);
            // 
            // 清空CToolStripMenuItem
            // 
            this.清空CToolStripMenuItem.Name = "清空CToolStripMenuItem";
            this.清空CToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.清空CToolStripMenuItem.Text = "清空(&C)";
            this.清空CToolStripMenuItem.Click += new System.EventHandler(this.清空CToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.AllowDrop = true;
            this.tabControl1.Controls.Add(this.tabShpToTxt);
            this.tabControl1.Controls.Add(this.tabTxt2Shp);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(863, 430);
            this.tabControl1.TabIndex = 8;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);
            // 
            // tabShpToTxt
            // 
            this.tabShpToTxt.AllowDrop = true;
            this.tabShpToTxt.Controls.Add(this.linklblOption);
            this.tabShpToTxt.Controls.Add(this.btnGeo2Prj);
            this.tabShpToTxt.Controls.Add(this.btnPrjToGeo);
            this.tabShpToTxt.Controls.Add(this.lblVersion);
            this.tabShpToTxt.Controls.Add(this.txtTextFileDir);
            this.tabShpToTxt.Controls.Add(this.btnRun);
            this.tabShpToTxt.Controls.Add(this.sBtnSelectShp);
            this.tabShpToTxt.Controls.Add(this.label1);
            this.tabShpToTxt.Controls.Add(this.dataGridView1);
            this.tabShpToTxt.Controls.Add(this.cbSaveToSourceShpDir);
            this.tabShpToTxt.ImageIndex = 3;
            this.tabShpToTxt.Location = new System.Drawing.Point(4, 22);
            this.tabShpToTxt.Name = "tabShpToTxt";
            this.tabShpToTxt.Padding = new System.Windows.Forms.Padding(3);
            this.tabShpToTxt.Size = new System.Drawing.Size(855, 404);
            this.tabShpToTxt.TabIndex = 0;
            this.tabShpToTxt.Text = "ShapeFile转txt";
            this.tabShpToTxt.UseVisualStyleBackColor = true;
            // 
            // linklblOption
            // 
            this.linklblOption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linklblOption.AutoSize = true;
            this.linklblOption.Location = new System.Drawing.Point(11, 369);
            this.linklblOption.Name = "linklblOption";
            this.linklblOption.Size = new System.Drawing.Size(53, 12);
            this.linklblOption.TabIndex = 27;
            this.linklblOption.TabStop = true;
            this.linklblOption.Text = "标准模式";
            this.linklblOption.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklblOption_LinkClicked);
            // 
            // btnGeo2Prj
            // 
            this.btnGeo2Prj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGeo2Prj.Location = new System.Drawing.Point(449, 359);
            this.btnGeo2Prj.Name = "btnGeo2Prj";
            this.btnGeo2Prj.Size = new System.Drawing.Size(129, 41);
            this.btnGeo2Prj.TabIndex = 26;
            this.btnGeo2Prj.Text = "地理转投影坐标";
            this.btnGeo2Prj.UseVisualStyleBackColor = true;
            this.btnGeo2Prj.Visible = false;
            this.btnGeo2Prj.Click += new System.EventHandler(this.btnGeo2Prj_Click);
            // 
            // btnPrjToGeo
            // 
            this.btnPrjToGeo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrjToGeo.Location = new System.Drawing.Point(584, 358);
            this.btnPrjToGeo.Name = "btnPrjToGeo";
            this.btnPrjToGeo.Size = new System.Drawing.Size(129, 41);
            this.btnPrjToGeo.TabIndex = 26;
            this.btnPrjToGeo.Text = "投影转地理坐标";
            this.btnPrjToGeo.UseVisualStyleBackColor = true;
            this.btnPrjToGeo.Visible = false;
            this.btnPrjToGeo.Click += new System.EventHandler(this.btnPrjToGeo_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(70, 369);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(113, 12);
            this.lblVersion.TabIndex = 24;
            this.lblVersion.Text = "windr v1.0.18.0720";
            this.lblVersion.Visible = false;
            // 
            // txtTextFileDir
            // 
            this.txtTextFileDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTextFileDir.ButtonsSplitWidth = 2;
            this.txtTextFileDir.ButtonWidth = 130;
            this.txtTextFileDir.DefaultTips = "";
            this.txtTextFileDir.FileFilter = null;
            this.txtTextFileDir.Location = new System.Drawing.Point(206, 317);
            this.txtTextFileDir.MaximumSize = new System.Drawing.Size(13609, 136);
            this.txtTextFileDir.MinimumSize = new System.Drawing.Size(0, 37);
            this.txtTextFileDir.Name = "txtTextFileDir";
            this.txtTextFileDir.OperateButtonText = "操作";
            this.txtTextFileDir.OptEnable = true;
            this.txtTextFileDir.Path = "";
            this.txtTextFileDir.PathToButtonSplitWidth = 2;
            this.txtTextFileDir.ReadOnly = false;
            this.txtTextFileDir.SelectButtonText = "选择txt保存目录";
            this.txtTextFileDir.SelectEnable = true;
            this.txtTextFileDir.SelectPathType = WLib.CoordCoverter.View.Control.ESelectPathType.Folder;
            this.txtTextFileDir.SelectTips = null;
            this.txtTextFileDir.ShowButtonOption = WLib.CoordCoverter.View.Control.EShowButtonOption.ViewSelect;
            this.txtTextFileDir.Size = new System.Drawing.Size(642, 37);
            this.txtTextFileDir.TabIndex = 23;
            this.txtTextFileDir.Visible = false;
            // 
            // sBtnSelectShp
            // 
            this.sBtnSelectShp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sBtnSelectShp.AutoSize = true;
            this.sBtnSelectShp.Location = new System.Drawing.Point(718, 6);
            this.sBtnSelectShp.Name = "sBtnSelectShp";
            this.sBtnSelectShp.Size = new System.Drawing.Size(129, 36);
            this.sBtnSelectShp.TabIndex = 12;
            this.sBtnSelectShp.Text = "选择shp文件";
            this.sBtnSelectShp.UseVisualStyleBackColor = true;
            this.sBtnSelectShp.Click += new System.EventHandler(this.btnSelectShpPath_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "请拖动或选择shp文件到此处：";
            // 
            // cbSaveToSourceShpDir
            // 
            this.cbSaveToSourceShpDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbSaveToSourceShpDir.AutoSize = true;
            this.cbSaveToSourceShpDir.Checked = true;
            this.cbSaveToSourceShpDir.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSaveToSourceShpDir.Location = new System.Drawing.Point(8, 328);
            this.cbSaveToSourceShpDir.Name = "cbSaveToSourceShpDir";
            this.cbSaveToSourceShpDir.Size = new System.Drawing.Size(192, 16);
            this.cbSaveToSourceShpDir.TabIndex = 10;
            this.cbSaveToSourceShpDir.Text = "txt文件保存到shp文件所在位置";
            this.cbSaveToSourceShpDir.UseVisualStyleBackColor = true;
            this.cbSaveToSourceShpDir.CheckedChanged += new System.EventHandler(this.cbSaveToSourceTxtDir_CheckedChanged);
            // 
            // tabTxt2Shp
            // 
            this.tabTxt2Shp.Controls.Add(this.linklblOption2);
            this.tabTxt2Shp.Controls.Add(this.sBtnSelectTxt);
            this.tabTxt2Shp.Controls.Add(this.cbSaveToSourceTxtDir);
            this.tabTxt2Shp.Controls.Add(this.cmbCreateMode);
            this.tabTxt2Shp.Controls.Add(this.label4);
            this.tabTxt2Shp.Controls.Add(this.dataGridView2);
            this.tabTxt2Shp.Controls.Add(this.btnRun2);
            this.tabTxt2Shp.Controls.Add(this.txtShpDir);
            this.tabTxt2Shp.ImageIndex = 2;
            this.tabTxt2Shp.Location = new System.Drawing.Point(4, 22);
            this.tabTxt2Shp.Name = "tabTxt2Shp";
            this.tabTxt2Shp.Padding = new System.Windows.Forms.Padding(3);
            this.tabTxt2Shp.Size = new System.Drawing.Size(855, 404);
            this.tabTxt2Shp.TabIndex = 1;
            this.tabTxt2Shp.Text = "txt转shapeFile";
            this.tabTxt2Shp.UseVisualStyleBackColor = true;
            // 
            // linklblOption2
            // 
            this.linklblOption2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linklblOption2.AutoSize = true;
            this.linklblOption2.Location = new System.Drawing.Point(11, 369);
            this.linklblOption2.Name = "linklblOption2";
            this.linklblOption2.Size = new System.Drawing.Size(53, 12);
            this.linklblOption2.TabIndex = 28;
            this.linklblOption2.TabStop = true;
            this.linklblOption2.Text = "标准模式";
            this.linklblOption2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklblOption_LinkClicked);
            // 
            // sBtnSelectTxt
            // 
            this.sBtnSelectTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sBtnSelectTxt.AutoSize = true;
            this.sBtnSelectTxt.Location = new System.Drawing.Point(719, 6);
            this.sBtnSelectTxt.Name = "sBtnSelectTxt";
            this.sBtnSelectTxt.Size = new System.Drawing.Size(128, 36);
            this.sBtnSelectTxt.TabIndex = 21;
            this.sBtnSelectTxt.Text = "选择txt文件";
            this.sBtnSelectTxt.UseVisualStyleBackColor = true;
            this.sBtnSelectTxt.Click += new System.EventHandler(this.btnSelectTxtPath_Click);
            // 
            // cbSaveToSourceTxtDir
            // 
            this.cbSaveToSourceTxtDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbSaveToSourceTxtDir.AutoSize = true;
            this.cbSaveToSourceTxtDir.Checked = true;
            this.cbSaveToSourceTxtDir.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSaveToSourceTxtDir.Location = new System.Drawing.Point(8, 328);
            this.cbSaveToSourceTxtDir.Name = "cbSaveToSourceTxtDir";
            this.cbSaveToSourceTxtDir.Size = new System.Drawing.Size(192, 16);
            this.cbSaveToSourceTxtDir.TabIndex = 19;
            this.cbSaveToSourceTxtDir.Text = "shp文件保存到txt文件所在位置";
            this.cbSaveToSourceTxtDir.UseVisualStyleBackColor = true;
            this.cbSaveToSourceTxtDir.CheckedChanged += new System.EventHandler(this.cbSaveToSourceShpDir_CheckedChanged);
            // 
            // cmbCreateMode
            // 
            this.cmbCreateMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCreateMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCreateMode.FormattingEnabled = true;
            this.cmbCreateMode.Items.AddRange(new object[] {
            "每个坐标文件生成对应ShapeFile",
            "所有坐标文件生成到一个ShapeFile"});
            this.cmbCreateMode.Location = new System.Drawing.Point(469, 14);
            this.cmbCreateMode.Name = "cmbCreateMode";
            this.cmbCreateMode.Size = new System.Drawing.Size(244, 20);
            this.cmbCreateMode.TabIndex = 18;
            this.cmbCreateMode.Visible = false;
            this.cmbCreateMode.SelectedIndexChanged += new System.EventHandler(this.cmbCreateMode_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(167, 12);
            this.label4.TabIndex = 17;
            this.label4.Text = "请拖动或选择txt文件到此处：";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowDrop = true;
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colOpt2,
            this.colResult2,
            this.colFileName2,
            this.colFileSize2,
            this.colFilePath2,
            this.colResultPath2});
            this.dataGridView2.ContextMenuStrip = this.contextMenuStrip2;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView2.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView2.Location = new System.Drawing.Point(8, 45);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(839, 269);
            this.dataGridView2.TabIndex = 14;
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            this.dataGridView2.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellDoubleClick);
            this.dataGridView2.DragDrop += new System.Windows.Forms.DragEventHandler(this.dataGridView_DragDrop);
            this.dataGridView2.DragEnter += new System.Windows.Forms.DragEventHandler(this.dataGridView_DragEnter);
            // 
            // colOpt2
            // 
            this.colOpt2.HeaderText = "";
            this.colOpt2.Name = "colOpt2";
            this.colOpt2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colOpt2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colOpt2.Width = 46;
            // 
            // colResult2
            // 
            this.colResult2.HeaderText = "生成结果";
            this.colResult2.Name = "colResult2";
            this.colResult2.ReadOnly = true;
            this.colResult2.Width = 160;
            // 
            // colFileName2
            // 
            this.colFileName2.HeaderText = "文件名";
            this.colFileName2.Name = "colFileName2";
            this.colFileName2.ReadOnly = true;
            this.colFileName2.Width = 160;
            // 
            // colFileSize2
            // 
            this.colFileSize2.HeaderText = "大小(KB)";
            this.colFileSize2.Name = "colFileSize2";
            this.colFileSize2.ReadOnly = true;
            // 
            // colFilePath2
            // 
            this.colFilePath2.HeaderText = "文件路径";
            this.colFilePath2.Name = "colFilePath2";
            this.colFilePath2.ReadOnly = true;
            this.colFilePath2.Width = 330;
            // 
            // colResultPath2
            // 
            this.colResultPath2.HeaderText = "shp文件保存路径";
            this.colResultPath2.Name = "colResultPath2";
            this.colResultPath2.Visible = false;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开txt文件ToolStripMenuItem,
            this.打开txt文件位置toolStripMenuItem,
            this.打开生成的shp文件位置toolStripMenuItem,
            this.toolStripSeparator2,
            this.移除toolStripMenuItem,
            this.清空toolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip1";
            this.contextMenuStrip2.Size = new System.Drawing.Size(223, 120);
            // 
            // 打开txt文件ToolStripMenuItem
            // 
            this.打开txt文件ToolStripMenuItem.Name = "打开txt文件ToolStripMenuItem";
            this.打开txt文件ToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.打开txt文件ToolStripMenuItem.Text = "打开txt文件(&O)";
            this.打开txt文件ToolStripMenuItem.Click += new System.EventHandler(this.打开txt文件ToolStripMenuItem_Click);
            // 
            // 打开txt文件位置toolStripMenuItem
            // 
            this.打开txt文件位置toolStripMenuItem.Name = "打开txt文件位置toolStripMenuItem";
            this.打开txt文件位置toolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.打开txt文件位置toolStripMenuItem.Text = "打开txt文件位置(&L)";
            this.打开txt文件位置toolStripMenuItem.Click += new System.EventHandler(this.打开txt文件位置toolStripMenuItem_Click);
            // 
            // 打开生成的shp文件位置toolStripMenuItem
            // 
            this.打开生成的shp文件位置toolStripMenuItem.Name = "打开生成的shp文件位置toolStripMenuItem";
            this.打开生成的shp文件位置toolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.打开生成的shp文件位置toolStripMenuItem.Text = "打开生成的shp文件位置(&G)";
            this.打开生成的shp文件位置toolStripMenuItem.Click += new System.EventHandler(this.打开生成的shp文件位置toolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(219, 6);
            // 
            // 移除toolStripMenuItem
            // 
            this.移除toolStripMenuItem.Name = "移除toolStripMenuItem";
            this.移除toolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.移除toolStripMenuItem.Text = "移除(&R)";
            this.移除toolStripMenuItem.Click += new System.EventHandler(this.移除toolStripMenuItem_Click);
            // 
            // 清空toolStripMenuItem
            // 
            this.清空toolStripMenuItem.Name = "清空toolStripMenuItem";
            this.清空toolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.清空toolStripMenuItem.Text = "清空(&C)";
            this.清空toolStripMenuItem.Click += new System.EventHandler(this.清空toolStripMenuItem_Click);
            // 
            // btnRun2
            // 
            this.btnRun2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRun2.Location = new System.Drawing.Point(719, 358);
            this.btnRun2.Name = "btnRun2";
            this.btnRun2.Size = new System.Drawing.Size(129, 41);
            this.btnRun2.TabIndex = 13;
            this.btnRun2.Text = "转换";
            this.btnRun2.UseVisualStyleBackColor = true;
            this.btnRun2.Click += new System.EventHandler(this.btnRun2_Click);
            // 
            // txtShpDir
            // 
            this.txtShpDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtShpDir.ButtonsSplitWidth = 2;
            this.txtShpDir.ButtonWidth = 130;
            this.txtShpDir.DefaultTips = "";
            this.txtShpDir.FileFilter = null;
            this.txtShpDir.Location = new System.Drawing.Point(206, 317);
            this.txtShpDir.MaximumSize = new System.Drawing.Size(11665, 117);
            this.txtShpDir.MinimumSize = new System.Drawing.Size(0, 33);
            this.txtShpDir.Name = "txtShpDir";
            this.txtShpDir.OperateButtonText = "操作";
            this.txtShpDir.OptEnable = true;
            this.txtShpDir.Path = "";
            this.txtShpDir.PathToButtonSplitWidth = 2;
            this.txtShpDir.ReadOnly = false;
            this.txtShpDir.SelectButtonText = "选择shp保存目录";
            this.txtShpDir.SelectEnable = true;
            this.txtShpDir.SelectPathType = WLib.CoordCoverter.View.Control.ESelectPathType.Folder;
            this.txtShpDir.SelectTips = null;
            this.txtShpDir.ShowButtonOption = WLib.CoordCoverter.View.Control.EShowButtonOption.ViewSelect;
            this.txtShpDir.Size = new System.Drawing.Size(642, 37);
            this.txtShpDir.TabIndex = 22;
            this.txtShpDir.Visible = false;
            // 
            // sBtnSelectTxt_ctxMenuStrip
            // 
            this.sBtnSelectTxt_ctxMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.选择txt目录ToolStripMenuItem});
            this.sBtnSelectTxt_ctxMenuStrip.Name = "contextMenuStrip3";
            this.sBtnSelectTxt_ctxMenuStrip.Size = new System.Drawing.Size(139, 26);
            // 
            // 选择txt目录ToolStripMenuItem
            // 
            this.选择txt目录ToolStripMenuItem.Name = "选择txt目录ToolStripMenuItem";
            this.选择txt目录ToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.选择txt目录ToolStripMenuItem.Text = "选择txt目录";
            this.选择txt目录ToolStripMenuItem.Click += new System.EventHandler(this.btnSelectTxtDir_Click);
            // 
            // sBtnSelectShp_ctxMenuStrip
            // 
            this.sBtnSelectShp_ctxMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.选择shp目录ToolStripMenuItem,
            this.选择工作空间ToolStripMenuItem});
            this.sBtnSelectShp_ctxMenuStrip.Name = "contextMenuStrip3";
            this.sBtnSelectShp_ctxMenuStrip.Size = new System.Drawing.Size(167, 48);
            // 
            // 选择shp目录ToolStripMenuItem
            // 
            this.选择shp目录ToolStripMenuItem.Name = "选择shp目录ToolStripMenuItem";
            this.选择shp目录ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.选择shp目录ToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.选择shp目录ToolStripMenuItem.Text = "选择shp目录";
            this.选择shp目录ToolStripMenuItem.Click += new System.EventHandler(this.btnSelectShpDir_Click);
            // 
            // 选择工作空间ToolStripMenuItem
            // 
            this.选择工作空间ToolStripMenuItem.Name = "选择工作空间ToolStripMenuItem";
            this.选择工作空间ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.选择工作空间ToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.选择工作空间ToolStripMenuItem.Text = "选择数据源";
            this.选择工作空间ToolStripMenuItem.Visible = false;
            this.选择工作空间ToolStripMenuItem.Click += new System.EventHandler(this.选择工作空间ToolStripMenuItem_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 430);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(863, 4);
            this.progressBar1.TabIndex = 25;
            this.progressBar1.Visible = false;
            // 
            // ConverterForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 434);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.progressBar1);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(600, 300);
            this.Name = "ConverterForm";
            this.Text = "批量坐标文件转换 - 兼容国家2000和西安80坐标";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ConverterForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabShpToTxt.ResumeLayout(false);
            this.tabShpToTxt.PerformLayout();
            this.tabTxt2Shp.ResumeLayout(false);
            this.tabTxt2Shp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.sBtnSelectTxt_ctxMenuStrip.ResumeLayout(false);
            this.sBtnSelectShp_ctxMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion


        private Button btnRun;

        private OpenFileDialog openFileDlg;

        private SaveFileDialog saveFileDlg;

        private DataGridView dataGridView1;

        private TabPage tabShpToTxt;

        private Label label1;

        private TabPage tabTxt2Shp;

        private ContextMenuStrip contextMenuStrip1;

        private ToolStripMenuItem 打开shp文件位置ToolStripMenuItem;

        private ToolStripMenuItem 打开生成的txt文件OToolStripMenuItem;

        private ToolStripMenuItem 移除RToolStripMenuItem;

        private ToolStripMenuItem 清空CToolStripMenuItem;

        private Label label4;

        private DataGridView dataGridView2;

        private Button btnRun2;

        private ToolStripMenuItem 打开生成的txt文件位置GToolStripMenuItem;

        private ContextMenuStrip contextMenuStrip2;

        private ToolStripMenuItem 打开txt文件ToolStripMenuItem;

        private ToolStripMenuItem 打开txt文件位置toolStripMenuItem;

        private ToolStripMenuItem 打开生成的shp文件位置toolStripMenuItem;

        private ToolStripMenuItem 移除toolStripMenuItem;

        private ToolStripMenuItem 清空toolStripMenuItem;

        private ComboBox cmbCreateMode;

        private CheckBox cbSaveToSourceShpDir;

        private CheckBox cbSaveToSourceTxtDir;

        private TabControl tabControl1;

        private SplitButton sBtnSelectShp;

        private ContextMenuStrip sBtnSelectShp_ctxMenuStrip;

        private ToolStripMenuItem 选择shp目录ToolStripMenuItem;

        private ContextMenuStrip sBtnSelectTxt_ctxMenuStrip;

        private ToolStripMenuItem 选择txt目录ToolStripMenuItem;

        private SplitButton sBtnSelectTxt;

        private ToolStripSeparator toolStripSeparator1;

        private ToolStripSeparator toolStripSeparator2;

        private PathBoxSimple txtShpDir;

        private PathBoxSimple txtTextFileDir;

        private ToolStripMenuItem 选择工作空间ToolStripMenuItem;

        private Label lblVersion;

        private ProgressBar progressBar1;

        private Button btnPrjToGeo;

        private DataGridViewLinkColumn colOpt;

        private DataGridViewTextBoxColumn colResult;

        private DataGridViewTextBoxColumn colFileName;

        private DataGridViewTextBoxColumn colFileSize;

        private DataGridViewTextBoxColumn colFilePath;

        private DataGridViewTextBoxColumn colResultPath;

        private Button btnGeo2Prj;
        private LinkLabel linklblOption;
        private LinkLabel linklblOption2;
        private ToolStripMenuItem 设置SToolStripMenuItem1;
        private ToolStripSeparator toolStripSeparator4;
        private DataGridViewLinkColumn colOpt2;
        private DataGridViewTextBoxColumn colResult2;
        private DataGridViewTextBoxColumn colFileName2;
        private DataGridViewTextBoxColumn colFileSize2;
        private DataGridViewTextBoxColumn colFilePath2;
        private DataGridViewTextBoxColumn colResultPath2;
    }
}

