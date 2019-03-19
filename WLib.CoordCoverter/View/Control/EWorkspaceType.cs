/*---------------------------------------------------------------- 
// auth： Windragon
// date： 
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using WLib.CoordCoverter.Utility;

namespace WLib.CoordCoverter.View.Control
{
    /// <summary>
    /// ESRI ArcGIS常用工作空间（IWorkspace）类别
    /// </summary>
    public enum EWorkspaceType
    {
        /// <summary>
        /// 默认，指ShapeFile,FileGDB,Access三种工作空间之一
        /// </summary>
        [EnumDescription("shp/gdb，或mdb数据库")]
        [EnumDescription("default",2)]
        [EnumDescription("", 1)]
        Default = 0,
        /// <summary>
        /// ShapeFile文件夹
        /// </summary>
        [EnumDescription("ShapeFile文件夹")]
        [EnumDescription("shp", 2)]
        [EnumDescription("esriDataSourcesFile.ShapefileWorkspaceFactory", 1)]
        ShapeFile = 1,
        /// <summary>
        /// 文件地理数据库（*.gdb）
        /// </summary>
        [EnumDescription("文件地理数据库")]
        [EnumDescription("gdb", 2)]
        [EnumDescription("esriDataSourcesGDB.FileGDBWorkspaceFactory", 1)]
        FileGDB = 2,
        /// <summary>
        /// Access数据库
        /// </summary>
        [EnumDescription("Access数据库")]
        [EnumDescription("mdb", 2)]
        [EnumDescription("esriDataSourcesFile.ShapefileWorkspaceFactory", 1)]
        Access = 3,
        /// <summary>
        /// SDE数据库
        /// </summary>
        [EnumDescription("SDE数据库")]
        [EnumDescription("sde", 2)]
        [EnumDescription("esriDataSourcesGDB.AccessWorkspaceFactory", 1)]
        Sde = 4,
        /// <summary>
        /// Excel文件
        /// </summary>
        [EnumDescription("Excel文件")]
        [EnumDescription("excel", 2)]
        [EnumDescription("esriDataSourcesOleDB.ExcelWorkspaceFactory", 1)]
        Excel = 5,
        /// <summary>
        /// 文本文件夹
        /// </summary>
        [EnumDescription("文本文件夹")]
        [EnumDescription("txt", 2)]
        [EnumDescription("esriDataSourcesOleDB.TextFileWorkspaceFactory", 1)]
        TextFile = 6,
        /// <summary>
        /// OleDb数据库（Access/Excel/dbf/Oracle/SQLServer等）
        /// </summary>
        [EnumDescription("OleDb数据库")]
        [EnumDescription("oledb", 2)]
        [EnumDescription("esriDataSourcesOleDB.OLEDBWorkspaceFactory", 1)]
        OleDb = 7,
        /// <summary>
        /// 栅格数据
        /// </summary>
        [EnumDescription("栅格数据文件夹")]
        [EnumDescription("raster", 2)]
        [EnumDescription("esriDataSourcesRaster.RasterWorkspaceFactory", 1)]
        Raster = 8,
        /// <summary>
        /// Sql数据库
        /// </summary>
        [EnumDescription("Sql数据库")]
        [EnumDescription("sql", 2)]
        [EnumDescription("esriDataSourcesGDB.SqlWorkspaceFactory", 1)]
        Sql = 10,
        /// <summary>
        /// CAD数据文件夹
        /// </summary>
        [EnumDescription("CAD数据文件夹")]
        [EnumDescription("cad", 2)]
        [EnumDescription("esriDataSourcesFile.CadWorkspaceFactory", 1)]
        CAD = 11,


        //关于UID：https://blog.csdn.net/yulongguiziyao/article/details/16119633
        //关于WorkspaceFactoryProgID：http://edndoc.esri.com/arcobjects/9.2/ComponentHelp/esriGeodatabase/IWorkspaceName_WorkspaceFactoryProgID.htm
        //esriDataSourcesFile.ArcInfoWorkspaceFactory
        //esriDataSourcesFile.CadWorkspaceFactory
        //esriDataSourcesOleDB.OLEDBWorkspaceFactory
        //esriDataSourcesFile.PCCoverageWorkspaceFactory
        //esriDataSourcesRaster.RasterWorkspaceFactory
        //esriDataSourcesFile.TinWorkspaceFactory
        //esriDataSourcesFile.VpfWorkspaceFactory
    }
}
