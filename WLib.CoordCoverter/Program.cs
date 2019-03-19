using System;
using System.Windows.Forms;
using OSGeo.GDAL;
using WLib.CoordCoverter.Utility;
using WLib.CoordCoverter.View;

namespace WLib.CoordCoverter
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //若出现【无法加载 DLL“ogr_wrap”: 找不到指定的模块】，
            //请将附带的DLL.rar中的全部文件解压复制到生成目录bin\x86\Debug（或bin\x86\Debug\DLL），同时安装附带的VC++包（Vcredist_x86.exe）
            //Exception: Unable to load DLL "ogr_wrap": Cannot find the specified module.
            //Solution: ① Please unzip all the files in the attached DLL.rar to the build directory bin\x86\Debug(or bin\x86\Debug\DLL)
            //          ② Install VC++ package (Vcredist_x86.exe)
            //Exception: Unable to open EPSG support file gcs.csv.
            //Solution: Please unzip all the files in the attached Data.rar to the build directory bin\x86\Debug\Data
            GdalHelper.GdalInit();
            Gdal.SetConfigOption("SHAPE_RESTORE_SHX", "YES"); //尝试还原/生成缺少的.shx文件

            Application.Run(new ConverterForm());
        }
    }
}
