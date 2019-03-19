# Txt-ShpConverter
txt和shp格式的坐标文件互转（Text format coordinate file and shape file convert to each other, using GDAL）

## 说明
---
本工具主要用于将txt格式的“勘测定界界址点坐标交换格式”坐标文件
与ArcGIS提供的Shapefile文件进行互转，默认支持坐标系为西安80坐标和国家2000坐标。

txt坐标文件格式规范可官方网站或百度查阅：
* [勘测定界界址点坐标交换格式 - 湖南省自然资源厅](http://zrzyt.hunan.gov.cn/bsfw/zlxz/bgxz/201112/t20111227_4509015.html)
* [勘测定界界址点坐标交换格式 - 百度文库](https://wenku.baidu.com/view/820e4f75f242336c1eb95e29.html)

## 环境
---
### 运行环境
* .NET Framework 2.0
* Microsoft Visual C++ （已附带安装包Vcredist_x86.exe）

### 开发环境
* Visual Studio 2017 或更高版本
* 调用开源GIS开发库：[GDAL（Geospatial Data Abstraction Library)](https://www.gdal.org/)
* 调用第三方控件：[SplitButton.dll](https://wyday.com/splitbutton/)
* 调用.NET官方库：System.Core.dll v3.5.0.0（用于在.net2.0中使用LINQ等）

## 运行
---
1. 下载代码，在Visual Studio打开项目，生成项目
2. DLL.rar（包含GDAL库的各类dll、SplitButton.dll、System.Core.dll）的全部文件解压到生成目录中；
3. 将Data.rar的全部文件解压缩到生成目录的Data文件夹下；
4. 启动项目


## 使用
---
`WLib.CoordCoverter.RedLineManager.ShpToTxt`方法将txt坐标文件转成shp文件；
`WLib.CoordCoverter.RedLineManager.TxtToShp`方法将shp文件转成txt坐标文件；
在使用这两个坐标转换方法之前，需要先在程序启动的`Main`方法中调用`GdalHelper.GdalInit`对GDAL环境进行初始化。
```cSharp
using System;
using OSGeo.GDAL;
using WLib.CoordCoverter;
using WLib.CoordCoverter.Utility;
void Main()
{
    GdalHelper.GdalInit();//初始化Gdal，注册所有驱动并支持中文
    Gdal.SetConfigOption("SHAPE_RESTORE_SHX", "YES"); //尝试还原/生成缺少的.shx文件
    
    RedLineManager.ShpToTxt(@"c:\source.shp", @"c:\result.txt");//txt转shp
    RedLineManager.TxtToShp(@"c:\source.txt", @"c:\result.shp");//shp转txt
    
    Console.WriteLine("Conversion completed!");
}
```
