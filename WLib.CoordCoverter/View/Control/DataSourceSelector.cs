/*---------------------------------------------------------------- 
// auth： Windragon
// date： 
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OSGeo.OGR;
using WLib.CoordCoverter.Utility;

namespace WLib.CoordCoverter.View.Control
{
    /// <summary>
    /// GDAL数据源选择器
    /// </summary>
    [DefaultEvent("WorkspaceTypeChanged")]
    [DefaultProperty("WorkspaceTypeFilter")]
    public partial class DataSourceSelector : UserControl
    {
        #region 基础属性
        /// <summary>
        /// "shp|gdb|mdb|sde|excel|sql"
        /// </summary>
        public const string DEFAULT_WORKSPACE_TYPE = "shp|gdb|mdb|sde|excel|sql";
        /// <summary>
        /// 可选的工作空间类别
        /// </summary>
        private string _workspaceTypeFilter;
        /// <summary>
        /// 可选的工作空间类别
        /// </summary>
        public string WorkspaceTypeFilter
        {
            set
            {
                var description2 = EnumHelper.GetDescriptions<EWorkspaceType>(2);
                var splitStrArr = value.Split('|');

                this.cmbADBType.Items.Clear();
                _workspaceTypeFilter = string.Empty;
                foreach (var str in splitStrArr)
                {
                    foreach (var des2 in description2)
                    {
                        if (des2 == str)
                        {
                            _workspaceTypeFilter += str + "|";
                            this.cmbADBType.Items.Add(EnumHelper.GetDescription(EnumHelper.GetEnum<EWorkspaceType>(str, 2)));
                        }
                    }
                }
                if (_workspaceTypeFilter == string.Empty)
                {
                    _workspaceTypeFilter = DEFAULT_WORKSPACE_TYPE;
                    foreach (var str in DEFAULT_WORKSPACE_TYPE.Split('|'))
                    {
                        this.cmbADBType.Items.Add(EnumHelper.GetDescription(EnumHelper.GetEnum<EWorkspaceType>(str)));
                    }
                }
                else
                {
                    _workspaceTypeFilter.Remove(_workspaceTypeFilter.Length - 1);
                }

                this.cmbADBType.SelectedIndex = 0;
            }
            get => this._workspaceTypeFilter;
        }
        /// <summary>
        /// 获取所选的工作空间类别
        /// </summary>
        public EWorkspaceType WorkspaceType => EnumHelper.GetEnum<EWorkspaceType>(this.cmbADBType.SelectedItem.ToString());

        /// <summary>
        /// 获取或设置可选工作空间列表中选中项的索引
        /// </summary>
        public int WorkspaceIndex { get => this.cmbADBType.SelectedIndex; set => this.cmbADBType.SelectedIndex = value; }
        /// <summary>
        /// 标示除浏览按钮外，其余操作是否可用
        /// </summary>
        public bool OptEnable { get => this.SourcePathBox.OptEnable; set => this.SourcePathBox.OptEnable = this.cmbADBType.Enabled = value; }
        /// <summary>
        /// 控件左侧的提示信息（eg:工作空间）
        /// </summary>
        public string Description { get => this.lblWorkspaceDesc.Text; set => this.lblWorkspaceDesc.Text = value; }
        /// <summary>
        /// 路径或连接字符串
        /// </summary>
        public string PathOrConnStr
        {
            get => this.SourcePathBox.Path;
            set
            {
                var wsType = GetWorkspaceType(value);
                this.SourcePathBox.Path = value;
                var workspaceIndex = wsType == EWorkspaceType.Default ? 0 : this.cmbADBType.Items.IndexOf(EnumHelper.GetDescription(wsType));
                if (WorkspaceIndex != workspaceIndex)
                    WorkspaceIndex = workspaceIndex;
            }
        }
        #endregion

        #region 基础事件
        /// <summary>
        /// 选择工作空间并获取要素类名称完成事件
        /// （同时代表子控件PathBox的AfterSelectPath）
        /// </summary>
        public event EventHandler AfterSelectPath;
        /// <summary>
        /// 改变工作空间类型选项完成的事件
        /// </summary>
        public event EventHandler WorkspaceTypeChanged;
        /// <summary>
        /// 触发AfterSelectPath事件
        /// </summary>
        internal void OnAfterSelectPath()
        {
            AfterSelectPath?.Invoke(this, new EventArgs());
        }
        /// <summary>
        /// 触发AfterSelectPath事件
        /// </summary>
        internal void OnWorkspaceTypeChanged()
        {
            WorkspaceTypeChanged?.Invoke(this, new EventArgs());
        }
        #endregion

        #region GIS相关属性、方法
        /// <summary>
        /// 数据源
        /// </summary>
        public DataSource GdalDataSource { get; private set; }
        /// <summary>
        /// 获取数据源中的要素类
        /// </summary>
        public Layer[] Layers { get; private set; }
        /// <summary>
        /// 获取数据源中的要素的名称
        /// </summary>
        public string[] LayerNames
        {
            get
            {
                if (Layers == null) return null;
                List<string> lyrNames = new List<string>();
                foreach (var lyr in Layers)
                {
                    lyrNames.Add(lyr.GetName());
                }
                return lyrNames.ToArray();
            }
        }
        /// <summary>
        /// 返回指定名称或别名的要素类（未连接数据源或找不到时返回null）
        /// </summary>
        /// <param name="name">要素类名称或要素类别名</param>
        /// <returns></returns>
        public Layer GetLayerByName(string name)
        {
            if (Layers == null) return null;
            foreach (var cls in Layers)
            {
                if (cls.GetName() == name)
                    return cls;
            }
            return null;
        }
        /// <summary>
        /// 返回指定名称或别名关键字的要素类（模糊匹配）（未连接数据源或找不到时返回null）
        /// </summary>
        /// <param name="name">要素类名称或要素类别名</param>
        /// <returns></returns>
        public Layer GetLayerByKeyName(string name)
        {
            if (Layers == null) return null;
            foreach (var cls in Layers)
            {
                if (cls.GetName().Contains(name))
                    return cls;
            }
            return null;
        }
        /// <summary>
        /// 根据路径或连接字符打开对应数据源，并获取要素类或表格
        /// </summary>
        /// <param name="pathOrConStr"></param>
        public void LoadWorkspace(string pathOrConStr)
        {
            if (System.IO.File.Exists(pathOrConStr))
            {
                string extension = System.IO.Path.GetExtension(pathOrConStr);
                if (extension.Equals(".mdb"))
                    this.cmbADBType.SelectedItem = EnumHelper.GetDescription(EWorkspaceType.Access);
                else if (extension.Equals(".xls") || extension.Equals(".xlsx"))
                    this.cmbADBType.SelectedItem = EnumHelper.GetDescription(EWorkspaceType.Excel);
            }
            else if (System.IO.Directory.Exists(pathOrConStr))
            {
                this.cmbADBType.SelectedItem = EnumHelper.GetDescription(pathOrConStr.EndsWith(".gdb") ? EWorkspaceType.FileGDB : EWorkspaceType.ShapeFile);
            }
            else
            {
                this.cmbADBType.SelectedItem = EnumHelper.GetDescription(EWorkspaceType.Sde);
            }
            this.PathOrConnStr = pathOrConStr;
            this.SourcePathBox.SelectPath(pathOrConStr);
        }
        /// <summary>
        /// 获取数据源的全部图层
        /// </summary>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        public static List<Layer> GetLayers(DataSource dataSource)
        {
            var layers = new List<Layer>();
            for (int i = 0; i < dataSource.GetLayerCount(); i++)
            {
                layers.Add(dataSource.GetLayerByIndex(i));
            }

            return layers;
        }
        #endregion

        /// <summary>
        /// 数据源选择器
        /// </summary>
        public DataSourceSelector()
        {
            InitializeComponent();

            this.SourcePathBox.AfeterSelectPath += txtASourcePath_AfeterSelectPath;
            AfterSelectPath = new EventHandler((object sender, EventArgs e) => { });
            WorkspaceTypeChanged = new EventHandler((object sender, EventArgs e) => { this.SourcePathBox.Text = ""; });
            WorkspaceTypeFilter = DEFAULT_WORKSPACE_TYPE;
        }
        /// <summary>
        /// 根据路径或连接参数，判断工作空间类型，只判断shp/gdb/mdb/sde/xls(xlsx)
        /// </summary>
        /// <param name="strConnOrPath">工作空间的路径或连接参数，可以是shp/gdb文件夹路径、mdb文件路径或SDE连接参数</param>
        /// <returns>若strConnOrPath不是连接字符串，且指示的不是gdb,mdb,shp路径或路径不存在，返回null</returns>
        public static EWorkspaceType GetWorkspaceType(string strConnOrPath)
        {
            var eWorkspaceType = EWorkspaceType.Default;

            if (System.IO.File.Exists(strConnOrPath))
            {
                var extension = System.IO.Path.GetExtension(strConnOrPath);
                if (extension == ".mdb") eWorkspaceType = EWorkspaceType.Access;
                if (extension == ".xls" || extension == ".xlsx") eWorkspaceType = EWorkspaceType.Excel;
            }
            else if (System.IO.Directory.Exists(strConnOrPath))
                eWorkspaceType = strConnOrPath.EndsWith(".gdb") ? EWorkspaceType.FileGDB : EWorkspaceType.ShapeFile;
            else if (strConnOrPath.Split('=', ';').Length >= 2)
            {
                if (strConnOrPath.Contains("MSSQL"))
                    eWorkspaceType = EWorkspaceType.Sql;
                else
                    eWorkspaceType = EWorkspaceType.Sde;
            }

            return eWorkspaceType;
        }

        private void cmbWorkspaceType_SelectedIndexChanged(object sender, EventArgs e)//选择数据源类别后，改变控件状态并触发WorkspaceTypeChanged事件
        {
            var item = EnumHelper.GetEnum<EWorkspaceType>(this.cmbADBType.SelectedItem.ToString());
            GdalDataSource = null;
            Layers = null;

            switch (item)
            {
                case EWorkspaceType.ShapeFile:
                    this.SourcePathBox.ShowButtonOption = EShowButtonOption.ViewSelect;
                    this.SourcePathBox.SelectPathType = ESelectPathType.OpenFile;
                    this.SourcePathBox.FileFilter = "*.shp|*.shp";
                    this.SourcePathBox.SelectTips = "选择shp文件";
                    break;
                case EWorkspaceType.FileGDB:
                    this.SourcePathBox.ShowButtonOption = EShowButtonOption.ViewSelect;
                    this.SourcePathBox.SelectPathType = ESelectPathType.Folder;
                    this.SourcePathBox.SelectTips = "选择gdb文件夹";
                    break;
                case EWorkspaceType.Access:
                    this.SourcePathBox.ShowButtonOption = EShowButtonOption.ViewSelect;
                    this.SourcePathBox.SelectPathType = ESelectPathType.OpenFile;
                    this.SourcePathBox.FileFilter = "*.mdb|*.mdb";
                    this.SourcePathBox.SelectTips = "选择mdb数据库";
                    break;
                case EWorkspaceType.Excel:
                    this.SourcePathBox.ShowButtonOption = EShowButtonOption.ViewSelect;
                    this.SourcePathBox.SelectPathType = ESelectPathType.OpenFile;
                    this.SourcePathBox.FileFilter = "*.xls;*.xlsx|*.xls;*.xlsx";
                    this.SourcePathBox.SelectTips = "选择Excel文件";
                    break;
                case EWorkspaceType.Sde:
                case EWorkspaceType.Sql:
                    this.SourcePathBox.Text = string.Empty;
                    this.SourcePathBox.ShowButtonOption = EShowButtonOption.None;
                    break;
            }
            WorkspaceTypeChanged(this, new EventArgs());
        }

        private void txtASourcePath_AfeterSelectPath(object sender, EventArgs e)//选择数据源后，获取要素类名称，触发AfterSelectPath
        {
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();

            try
            {
                var path = this.SourcePathBox.Path;
                GdalDataSource = Ogr.Open(path, 0);
                Layers = GetLayers(GdalDataSource).ToArray();
                AfterSelectPath(this, new EventArgs());
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, this.lblWorkspaceDesc.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
            this.Cursor = Cursors.Default;
        }

        private void WorkspaceSelector_Load(object sender, EventArgs e)//加载控件时，选中第一个数据源
        {
            if (this.cmbADBType.SelectedIndex == -1) this.cmbADBType.SelectedIndex = 0;
        }
    }
}
