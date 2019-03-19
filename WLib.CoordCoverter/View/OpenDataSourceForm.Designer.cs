using WLib.CoordCoverter.View.Control;

namespace WLib.CoordCoverter.View
{
    partial class OpenDataSourceForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpenDataSourceForm));
            this.cmbLayers = new System.Windows.Forms.ComboBox();
            this.lblLayer = new System.Windows.Forms.Label();
            this.cmbIdField = new System.Windows.Forms.ComboBox();
            this.cmbNameField = new System.Windows.Forms.ComboBox();
            this.lblNameField = new System.Windows.Forms.Label();
            this.lblIdField = new System.Windows.Forms.Label();
            this.btnRun = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOpt = new System.Windows.Forms.DataGridViewLinkColumn();
            this.colComment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colResult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colResultPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.选中SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.不选DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.全选AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.反选NToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清除选择CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.打开生成的txt文件OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开生成的txt文件位置LToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.复制名称ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.复制IDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.查找FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblVersion = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.txtTextFileDir = new PathBoxSimple();
            this.dataSourceSelector1 = new DataSourceSelector();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbLayers
            // 
            this.cmbLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbLayers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLayers.FormattingEnabled = true;
            this.cmbLayers.Location = new System.Drawing.Point(52, 3);
            this.cmbLayers.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this.cmbLayers.Name = "cmbLayers";
            this.cmbLayers.Size = new System.Drawing.Size(176, 20);
            this.cmbLayers.TabIndex = 3;
            this.cmbLayers.SelectedIndexChanged += new System.EventHandler(this.cmbLayers_SelectedIndexChanged);
            // 
            // lblLayer
            // 
            this.lblLayer.AutoSize = true;
            this.lblLayer.Location = new System.Drawing.Point(3, 6);
            this.lblLayer.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.lblLayer.Name = "lblLayer";
            this.lblLayer.Size = new System.Drawing.Size(41, 12);
            this.lblLayer.TabIndex = 1;
            this.lblLayer.Text = "图层：";
            // 
            // cmbIdField
            // 
            this.cmbIdField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbIdField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIdField.FormattingEnabled = true;
            this.cmbIdField.Location = new System.Drawing.Point(322, 3);
            this.cmbIdField.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this.cmbIdField.Name = "cmbIdField";
            this.cmbIdField.Size = new System.Drawing.Size(176, 20);
            this.cmbIdField.TabIndex = 3;
            this.cmbIdField.SelectedIndexChanged += new System.EventHandler(this.cmbIdField_SelectedIndexChanged);
            // 
            // cmbNameField
            // 
            this.cmbNameField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbNameField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNameField.Enabled = false;
            this.cmbNameField.FormattingEnabled = true;
            this.cmbNameField.Location = new System.Drawing.Point(606, 3);
            this.cmbNameField.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this.cmbNameField.Name = "cmbNameField";
            this.cmbNameField.Size = new System.Drawing.Size(176, 20);
            this.cmbNameField.TabIndex = 3;
            this.cmbNameField.SelectedIndexChanged += new System.EventHandler(this.cmbNameField_SelectedIndexChanged);
            // 
            // lblNameField
            // 
            this.lblNameField.AutoSize = true;
            this.lblNameField.Location = new System.Drawing.Point(507, 6);
            this.lblNameField.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.lblNameField.Name = "lblNameField";
            this.lblNameField.Size = new System.Drawing.Size(89, 12);
            this.lblNameField.TabIndex = 5;
            this.lblNameField.Text = "项目名称字段：";
            // 
            // lblIdField
            // 
            this.lblIdField.AutoSize = true;
            this.lblIdField.Location = new System.Drawing.Point(237, 6);
            this.lblIdField.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.lblIdField.Name = "lblIdField";
            this.lblIdField.Size = new System.Drawing.Size(77, 12);
            this.lblIdField.TabIndex = 5;
            this.lblIdField.Text = "项目ID字段：";
            // 
            // btnRun
            // 
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRun.Location = new System.Drawing.Point(670, 391);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(129, 37);
            this.btnRun.TabIndex = 8;
            this.btnRun.Text = "转换";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowDrop = true;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.colID,
            this.colName,
            this.colOpt,
            this.colComment,
            this.colResult,
            this.colResultPath});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(12, 80);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(787, 266);
            this.dataGridView1.TabIndex = 9;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // colCheck
            // 
            this.colCheck.HeaderText = "生成";
            this.colCheck.Name = "colCheck";
            this.colCheck.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colCheck.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colID
            // 
            this.colID.HeaderText = "ID";
            this.colID.Name = "colID";
            this.colID.ReadOnly = true;
            // 
            // colName
            // 
            this.colName.HeaderText = "名称";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            this.colName.Width = 200;
            // 
            // colOpt
            // 
            this.colOpt.HeaderText = "";
            this.colOpt.Name = "colOpt";
            this.colOpt.Width = 46;
            // 
            // colComment
            // 
            this.colComment.HeaderText = "备注";
            this.colComment.Name = "colComment";
            // 
            // colResult
            // 
            this.colResult.HeaderText = "生成结果";
            this.colResult.Name = "colResult";
            this.colResult.ReadOnly = true;
            this.colResult.Width = 200;
            // 
            // colResultPath
            // 
            this.colResultPath.HeaderText = "生成路径";
            this.colResultPath.Name = "colResultPath";
            this.colResultPath.Visible = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.选中SToolStripMenuItem,
            this.不选DToolStripMenuItem,
            this.toolStripSeparator2,
            this.全选AToolStripMenuItem,
            this.反选NToolStripMenuItem,
            this.清除选择CToolStripMenuItem,
            this.toolStripSeparator1,
            this.打开生成的txt文件OToolStripMenuItem,
            this.打开生成的txt文件位置LToolStripMenuItem,
            this.toolStripSeparator3,
            this.复制名称ToolStripMenuItem,
            this.复制IDToolStripMenuItem,
            this.toolStripSeparator4,
            this.查找FToolStripMenuItem,
            this.设置ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(213, 270);
            // 
            // 选中SToolStripMenuItem
            // 
            this.选中SToolStripMenuItem.Name = "选中SToolStripMenuItem";
            this.选中SToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.选中SToolStripMenuItem.Text = "选中(&S)";
            this.选中SToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // 不选DToolStripMenuItem
            // 
            this.不选DToolStripMenuItem.Name = "不选DToolStripMenuItem";
            this.不选DToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.不选DToolStripMenuItem.Text = "不选(&D)";
            this.不选DToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(209, 6);
            // 
            // 全选AToolStripMenuItem
            // 
            this.全选AToolStripMenuItem.Name = "全选AToolStripMenuItem";
            this.全选AToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.全选AToolStripMenuItem.Text = "全选(&A)";
            this.全选AToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // 反选NToolStripMenuItem
            // 
            this.反选NToolStripMenuItem.Name = "反选NToolStripMenuItem";
            this.反选NToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.反选NToolStripMenuItem.Text = "反选(&N)";
            this.反选NToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // 清除选择CToolStripMenuItem
            // 
            this.清除选择CToolStripMenuItem.Name = "清除选择CToolStripMenuItem";
            this.清除选择CToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.清除选择CToolStripMenuItem.Text = "清除选择(&C)";
            this.清除选择CToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(209, 6);
            // 
            // 打开生成的txt文件OToolStripMenuItem
            // 
            this.打开生成的txt文件OToolStripMenuItem.Name = "打开生成的txt文件OToolStripMenuItem";
            this.打开生成的txt文件OToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.打开生成的txt文件OToolStripMenuItem.Text = "打开生成的txt文件(&O)";
            this.打开生成的txt文件OToolStripMenuItem.Click += new System.EventHandler(this.打开生成的txt文件OToolStripMenuItem_Click);
            // 
            // 打开生成的txt文件位置LToolStripMenuItem
            // 
            this.打开生成的txt文件位置LToolStripMenuItem.Name = "打开生成的txt文件位置LToolStripMenuItem";
            this.打开生成的txt文件位置LToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.打开生成的txt文件位置LToolStripMenuItem.Text = "打开生成的txt文件位置(&L)";
            this.打开生成的txt文件位置LToolStripMenuItem.Click += new System.EventHandler(this.打开生成的txt文件位置LToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(209, 6);
            // 
            // 复制名称ToolStripMenuItem
            // 
            this.复制名称ToolStripMenuItem.Name = "复制名称ToolStripMenuItem";
            this.复制名称ToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.复制名称ToolStripMenuItem.Text = "复制名称(&E)";
            this.复制名称ToolStripMenuItem.Click += new System.EventHandler(this.复制名称ToolStripMenuItem_Click);
            // 
            // 复制IDToolStripMenuItem
            // 
            this.复制IDToolStripMenuItem.Name = "复制IDToolStripMenuItem";
            this.复制IDToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.复制IDToolStripMenuItem.Text = "复制ID(&R)";
            this.复制IDToolStripMenuItem.Click += new System.EventHandler(this.复制IDToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(209, 6);
            // 
            // 查找FToolStripMenuItem
            // 
            this.查找FToolStripMenuItem.Name = "查找FToolStripMenuItem";
            this.查找FToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.查找FToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.查找FToolStripMenuItem.Text = "查找(&F)";
            this.查找FToolStripMenuItem.Click += new System.EventHandler(this.查找FToolStripMenuItem_Click);
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.设置ToolStripMenuItem.Text = "设置";
            this.设置ToolStripMenuItem.Click += new System.EventHandler(this.设置ToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 99F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.lblLayer, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbLayers, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblIdField, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblNameField, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbNameField, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbIdField, 3, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(18, 47);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(788, 26);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.Location = new System.Drawing.Point(535, 391);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(129, 37);
            this.btnStop.TabIndex = 25;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Visible = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(12, 409);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(113, 12);
            this.lblVersion.TabIndex = 26;
            this.lblVersion.Text = "windr v1.0.18.0716";
            this.lblVersion.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 433);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(810, 4);
            this.progressBar1.TabIndex = 27;
            this.progressBar1.Visible = false;
            // 
            // txtTextFileDir
            // 
            this.txtTextFileDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTextFileDir.ButtonsSplitWidth = 2;
            this.txtTextFileDir.ButtonWidth = 130;
            this.txtTextFileDir.DefaultTips = "";
            this.txtTextFileDir.FileFilter = null;
            this.txtTextFileDir.Location = new System.Drawing.Point(12, 349);
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
            this.txtTextFileDir.SelectPathType = ESelectPathType.Folder;
            this.txtTextFileDir.SelectTips = null;
            this.txtTextFileDir.ShowButtonOption = EShowButtonOption.ViewSelect;
            this.txtTextFileDir.Size = new System.Drawing.Size(788, 37);
            this.txtTextFileDir.TabIndex = 24;
            // 
            // dataSourceSelector1
            // 
            this.dataSourceSelector1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataSourceSelector1.Description = "数据源：";
            this.dataSourceSelector1.Location = new System.Drawing.Point(12, 12);
            this.dataSourceSelector1.Name = "dataSourceSelector1";
            this.dataSourceSelector1.OptEnable = true;
            this.dataSourceSelector1.PathOrConnStr = "";
            this.dataSourceSelector1.Size = new System.Drawing.Size(788, 31);
            this.dataSourceSelector1.TabIndex = 10;
            this.dataSourceSelector1.WorkspaceIndex = 0;
            this.dataSourceSelector1.WorkspaceTypeFilter = "shp|gdb|mdb|sql|";
            this.dataSourceSelector1.AfterSelectPath += new System.EventHandler(this.dataSourceSelector1_AfterSelectPath);
            this.dataSourceSelector1.WorkspaceTypeChanged += new System.EventHandler(this.dataSourceSelector1_WorkspaceTypeChanged);
            // 
            // OpenDataSourceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 437);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.txtTextFileDir);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.dataSourceSelector1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnRun);
            this.KeyPreview = true;
            this.Name = "OpenDataSourceForm";
            this.Text = "指定数据源转成txt坐标文件";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OpenDataSourceForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cmbLayers;
        private System.Windows.Forms.Label lblLayer;
        private System.Windows.Forms.ComboBox cmbIdField;
        private System.Windows.Forms.ComboBox cmbNameField;
        private System.Windows.Forms.Label lblNameField;
        private System.Windows.Forms.Label lblIdField;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.DataGridView dataGridView1;
        private DataSourceSelector dataSourceSelector1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private PathBoxSimple txtTextFileDir;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 选中SToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 不选DToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 全选AToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 反选NToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清除选择CToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 打开生成的txt文件OToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开生成的txt文件位置LToolStripMenuItem;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewLinkColumn colOpt;
        private System.Windows.Forms.DataGridViewTextBoxColumn colComment;
        private System.Windows.Forms.DataGridViewTextBoxColumn colResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn colResultPath;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查找FToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 复制名称ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 复制IDToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    }
}