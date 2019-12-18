/*---------------------------------------------------------------- 
// auth： Windragon
// date： 
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using OSGeo.GDAL;
using OSGeo.OGR;
using OSGeo.OSR;
using WLib.CoordCoverter.Config;
using WLib.CoordCoverter.Model;
using WLib.CoordCoverter.Utility;
using Driver = OSGeo.OGR.Driver;

namespace WLib.CoordCoverter
{
    /// <summary>
    /// 提供坐标红线文件的读写和转换的方法，
    /// 核心方法<see cref="ShpToTxt"/>，<see cref="TxtToShp"/>）
    /// </summary>
    public static class RedLineManager
    {
        #region Txt转TProjInfo
        /// <summary>
        /// 读取txt坐标文件，存入数据到TProjInfo实例中
        /// </summary>
        /// <param name="txtPath">txt文件路径</param>
        /// <param name="isAddProjZone">是否在Y坐标值上添加投影分带带号</param>
        /// <returns></returns>
        public static TProjInfo GetProjInfoFromTxt(string txtPath, bool isAddProjZone = false)
        {
            var txtInfo = new TProjInfo();

            var lines = File.ReadAllLines(txtPath, FileEncode.GetEncoding(txtPath));
            var txtFlag = ETxtContent.NoDefine;
            TPolygon polygon = null;
            for (int i = 0; i < lines.Length; i++)
            {
                try
                {
                    var line = lines[i];
                    if (SetStrFlag(line, ref txtFlag))
                        continue;

                    if (txtFlag == ETxtContent.Project)
                        txtInfo.AddProjectDict(line);

                    else if (txtFlag == ETxtContent.Property)
                        txtInfo.AddPropertyDict(line);

                    else if (txtFlag == ETxtContent.Coordinate)
                    {
                        string[] strArray = line.Split(',');
                        if (line.Contains("@"))
                        {
                            polygon = new TPolygon();
                            polygon.AddAttribute(strArray);
                            txtInfo.Polygons.Add(polygon);
                        }
                        else if (strArray.Length == 4)
                        {
                            var strY = isAddProjZone ? txtInfo.ProjZone + strArray[3] : strArray[3];
                            polygon.AddXY(Convert.ToInt32(strArray[1]), Convert.ToDouble(strArray[2]), Convert.ToDouble(strY));
                        }
                    }
                }
                catch (Exception ex) { throw new Exception($"读取坐标文件第{i}行发生错误：{ex.Message}"); }
            }
            CheckRepairPolygon(txtInfo);
            return txtInfo;
        }
        /// <summary>
        /// 使多边形（每个环）首尾坐标一致
        /// </summary> 
        private static void CheckRepairPolygon(TProjInfo txtInfo)
        {
            foreach (TPolygon obj in txtInfo.Polygons)
            {
                foreach (var ring in obj.Rings)
                {
                    var xyPair = ring.XyPair;
                    if (xyPair[0][0] != xyPair[xyPair.Count - 1][0] || xyPair[0][1] != xyPair[xyPair.Count - 1][1])
                        xyPair.Add(new[] { xyPair[0][0], xyPair[0][1] });
                }
            }
        }
        /// <summary>
        /// 设置读取信息的标识，返回标识是否改变
        /// </summary>
        /// <param name="lineStr"></param>
        /// <param name="txtFlag"></param>
        /// <returns></returns>
        private static bool SetStrFlag(string lineStr, ref ETxtContent txtFlag)
        {
            if (lineStr.Contains("[项目信息]"))
            {
                txtFlag = ETxtContent.Project;
                return true;
            }
            if (lineStr.Contains("[属性描述]"))
            {
                txtFlag = ETxtContent.Property;
                return true;
            }
            if (lineStr.Contains("[地块坐标]"))
            {
                txtFlag = ETxtContent.Coordinate;
                return true;
            }
            return false;
        }
        #endregion


        #region TProjInfo转TXT
        /// <summary>
        /// 将项目坐标信息转成TXT规范文本
        /// </summary>
        /// <returns></returns>
        public static string ToText(TProjInfo projInfo)
        {
            StringBuilder sb = new StringBuilder();

            if (projInfo.ProjectDict.Count > 0)//项目信息 
            {
                sb.AppendLine("[项目信息]");
                foreach (var pair in projInfo.ProjectDict) { sb.AppendLine(pair.Key + "=" + pair.Value); }
            }

            if (projInfo.PropertyDict.Count > 0) //属性描述
            {
                sb.AppendLine("[属性描述]");
                foreach (var pair in projInfo.PropertyDict) { sb.AppendLine(pair.Key + "=" + pair.Value); }
            }

            if (projInfo.Polygons.Count > 0) //地块坐标
            {
                var precision = projInfo.PropertyDict["精度"].Replace("1", "0");//eg:配置中精度显示为0.001，X和Y坐标输出格式为0.000
                sb.AppendLine("[地块坐标]");
                foreach (TPolygon polygon in projInfo.Polygons)
                {
                    for (int i = 0; i < polygon.AttributeValues.Count; i++)
                    {
                        sb.Append(polygon.AttributeValues[i] + ",");
                    }
                    sb.AppendLine("@");

                    for (int i = 0; i < polygon.Rings.Count; i++)
                    {
                        var ring = polygon.Rings[i];
                        var ringIndex = i + 1;
                        for (int j = 0; j < ring.XyPair.Count; j++)
                        {
                            var pt = ring.XyPair[j];
                            sb.AppendFormat("J{0},{1},{2},{3}{4}", j + 1, ringIndex, pt[0].ToString(precision), pt[1].ToString(precision), Environment.NewLine);
                        }
                    }
                }
                sb.Remove(sb.Length - 1, 1);//移除最后一个换行符
            }

            return sb.ToString();
        }
        /// <summary>
        /// 将项目坐标信息写到TXT文件中（编码默认GB2312）
        /// </summary>
        /// <param name="filePath">txt文件路径</param>
        /// <param name="projInfo">红线项目信息</param>
        /// <param name="encoding">txt文件编码，若值为null,则设定为GB2312</param>
        public static void ToTxtFile(string filePath, TProjInfo projInfo, Encoding encoding = null)
        {
            var content = ToText(projInfo);
            File.WriteAllText(filePath, content, encoding ?? Encoding.GetEncoding("GB2312"));
        }
        #endregion


        #region Shp转TProjInfo（Gdal接口）
        /// <summary>
        /// 读取shp文件，存入数据到TProjInfo实例中
        /// </summary>
        /// <param name="shpFilePath"></param>
        /// <returns></returns>
        public static TProjInfo GetProjInfoFromShp(string shpFilePath)
        {
            var dataSource = Ogr.Open(shpFilePath, 0);
            if (dataSource == null)
                throw new Exception("GDAL打开数据源失败，请确定GDAL已正常初始化、数据源存在且数据可读！");

            if (dataSource.GetLayerCount() == 0)
                throw new Exception("GDAL读取的数据源中，图层数量为0！");

            var layer = dataSource.GetLayerByIndex(0);
            var fieldDefns = layer.GetLayerDefn();
            var geoType = fieldDefns.GetGeomType();
            if (geoType != wkbGeometryType.wkbPolygon &&
                geoType != wkbGeometryType.wkbPolygon25D)
                throw new Exception($"无法转换“{geoType}”类型的Shp文件，请确保图层类型为面图层");

            var features = GetFeatures(layer);
            if (features.Count < 1)
                throw new Exception("图层为空！");

            var txtInfo = GetProjInfoFromFeatures(features.ToArray());

            dataSource.Dispose();
            return txtInfo;
        }
        /// <summary>
        /// 读取要素数据，存入数据到TProjInfo实例中
        /// </summary>
        /// <param name="features"></param>
        /// <returns></returns>
        public static TProjInfo GetProjInfoFromFeatures(Feature[] features)
        {
            var txtInfo = new TProjInfo();

            GetAttrDescriptPart(features, txtInfo);//获取“属性描述”

            for (int i = 0; i < features.Length; i++)//获取“地块坐标”
            {
                var feature = features[i];
                var geometry = feature.GetGeometryRef();
                if (geometry == null || geometry.IsEmpty())
                    throw new Exception("几何图形为空！");

                TPolygon tPolygon = GetBlockInfoHeader(feature, i + 1);//地块头部属性信息
                tPolygon.Rings.AddRange(GetRingInfo(geometry));//地块各个环的点信息
                if (tPolygon.AttributeValues[0] == "0")
                    tPolygon.AttributeValues[0] = tPolygon.Rings.Sum(v => v.XyPair.Count).ToString();

                txtInfo.Polygons.Add(tPolygon);
            }

            return txtInfo;
        }
        /// <summary>
        /// 读取多边形（可能是MulitPolygon）的每个环，构造TRing集合
        /// </summary>
        /// <param name="geometry"></param>
        /// <returns></returns>
        private static List<TRing> GetRingInfo(Geometry geometry)
        {
            var rings = new List<TRing>();
            for (int subIdx = 0; subIdx < geometry.GetGeometryCount(); subIdx++)
            {
                var subGeo = geometry.GetGeometryRef(subIdx);
                var subGeoType = subGeo.GetGeometryType();
                if (subGeoType == wkbGeometryType.wkbLineString || subGeoType == wkbGeometryType.wkbLineString25D)
                {
                    var tRing = new TRing();
                    for (int ptIndex = 0; ptIndex < subGeo.GetPointCount(); ptIndex++)
                    {
                        tRing.XyPair.Add(new[] { subGeo.GetY(ptIndex), subGeo.GetX(ptIndex) });
                    }
                    rings.Add(tRing);
                }
                else if (subGeoType == wkbGeometryType.wkbPolygon || subGeoType == wkbGeometryType.wkbPolygon25D)
                {
                    rings.AddRange(GetRingInfo(subGeo));
                }
            }

            return rings;
        }
        /// <summary>
        /// 获取属性描述部分
        /// </summary>
        /// <param name="features"></param>
        /// <param name="txtInfo"></param>
        private static void GetAttrDescriptPart(Feature[] features, TProjInfo txtInfo)
        {
            //获取“属性描述”
            if (features.Length == 0)
                throw new Exception("要素记录为空！");

            var feature1 = features[0];
            var fieldDefns = feature1.GetDefnRef();
            foreach (var field in CfgRedlineTxt.AttrFields)
            {
                var index = fieldDefns.GetFieldIndex(field.FieldName);
                txtInfo.PropertyDict.Add(field.AliasName, index > -1 ? feature1.GetFieldAsString(index) : field.DefaultValue);
            }

            //检验几何类型
            var geometry1 = feature1.GetGeometryRef();
            if (geometry1 == null || geometry1.IsEmpty())
                throw new Exception("几何图形为空！");

            var geoType = geometry1.GetGeometryType();
            if (geoType != wkbGeometryType.wkbMultiPolygon &&
                geoType != wkbGeometryType.wkbMultiPolygon25D &&
                geoType != wkbGeometryType.wkbPolygon &&
                geoType != wkbGeometryType.wkbPolygon25D)
                throw new Exception("图形不是多边形！");

            var subGeometry = geometry1.GetGeometryRef(0);
            if (subGeometry.GetGeometryType() == wkbGeometryType.wkbPolygon)
                subGeometry = subGeometry.GetGeometryRef(0);
            if (subGeometry.GetGeometryType() != wkbGeometryType.wkbLineString &&
                subGeometry.GetGeometryType() != wkbGeometryType.wkbLineString25D)
                throw new Exception("图形不是多边形！");

            //检查坐标系、带号
            var spatialRef = geometry1.GetSpatialReference();
            if (spatialRef == null)
                throw new Exception("无法识别坐标系，请先对数据设置正确坐标系！");

            //获取坐标系WKID（GDAL获取坐标系的功能不稳定，当获取不到WKID时通过配置获取坐标系，通过X坐标获取带号）
            var wkid = GetWkid(spatialRef);
            var geoSpatialRef = CfgRedlineTxt.GeoSpatialRefs.GetGeoSpatialRef(wkid);
            if (geoSpatialRef == null)
            {
                spatialRef.ExportToWkt(out var wkt);
                geoSpatialRef = CfgRedlineTxt.GeoSpatialRefs.GetGeoSpatialRef(wkt);
            }
            if (geoSpatialRef != null)
            {
                txtInfo.PropertyDict["坐标系"] = geoSpatialRef.AliasNames[0];
                if (wkid != -1)
                    txtInfo.PropertyDict["带号"] = geoSpatialRef.GetProjZone(wkid).ToString();
                else
                    txtInfo.PropertyDict["带号"] = subGeometry.GetX(0).ToString().Substring(0, 2);
            }
        }
        /// <summary>
        /// 获取投影坐标系WKID，获取失败返回-1
        /// </summary>
        /// <param name="prjectionSpatialRef">投影坐标系</param>
        /// <seealso cref="http://spatialreference.org/ref/"/>
        /// <returns></returns>
        public static int GetWkid(SpatialReference prjectionSpatialRef)
        {
            prjectionSpatialRef.AutoIdentifyEPSG();
            var strProjcs = prjectionSpatialRef.GetAttrValue("AUTHORITY", 1);

            if (!Int32.TryParse(strProjcs, out var prjcs))
                prjcs = -1;
            return prjcs;
        }
        /// <summary>
        /// 获取地块信息（界址点数,地块面积,地块编号,地块名称,几何类型(点、线、面),图幅号,地块用途,地类编码）
        /// </summary>
        /// <param name="feature">地块要素</param>
        /// <param name="xh">地块序号</param>
        /// <returns></returns>
        private static TPolygon GetBlockInfoHeader(Feature feature, int xh)
        {
            var tPolygon = new TPolygon();
            var polygon = feature.GetGeometryRef();
            var blockFields = CfgRedlineTxt.BlockFields;

            var ptCount = polygon.GetPointCount().ToString();
            var area = polygon.GetArea().ToString("0.00");
            tPolygon.AttributeValues.AddRange(new[] { ptCount, area, xh.ToString(), "", "面", "", "", "" });

            for (int j = 0; j < blockFields.Count; j++)
            {
                if (tPolygon.AttributeValues[j] == string.Empty)
                {
                    var index = feature.GetFieldIndex(blockFields[j].FieldName);
                    if (index > -1)
                        tPolygon.AttributeValues[j] = feature.GetFieldAsString(index);
                }
            }

            return tPolygon;
        }
        /// <summary>
        /// （根据查询条件）获取图层中的要素
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="whereClause"></param>
        /// <param name="geometry"></param>
        /// <returns></returns>
        private static List<Feature> GetFeatures(Layer layer, string whereClause = null, Geometry geometry = null)
        {
            var features = new List<Feature>();

            layer.ResetReading();
            layer.SetSpatialFilter(geometry);
            layer.SetAttributeFilter(whereClause);
            Feature oFeature;
            while ((oFeature = layer.GetNextFeature()) != null)
            {
                features.Add(oFeature);
            }
            return features;
        }
        #endregion


        #region TProjInfo转Shp（Gdal接口）
        /// <summary>
        /// 创建shp文件，将项目坐标信息写入shp文件中
        /// </summary>
        /// <param name="tProjInfo"></param>
        /// <param name="shpFilePath"></param>
        /// <returns></returns>
        public static void ToShpFile(TProjInfo tProjInfo, string shpFilePath)
        {
            var dataSource = CreateShapefileSource(shpFilePath);//创建shp数据源
            var layer = CreateRedLineLayer(dataSource, tProjInfo.Wkid, shpFilePath); //创建图层
            ToLayer(tProjInfo, layer); //数据写入图层

            dataSource.FlushCache();
            layer.SyncToDisk();
            dataSource.Dispose();
            
            Modify_Prj_To_Gauss_Kruger(shpFilePath);//将prj文件中的“墨卡托”字符替换成“高斯克吕格”
        }
        /// <summary>
        /// 在指定路径下构建shp数据源（此操作不会生成实际的shp文件）
        /// </summary>
        /// <param name="shpFilePath">shp文件路径</param>
        /// <returns></returns>
        public static DataSource CreateShapefileSource(string shpFilePath)
        {
            Driver driver = Ogr.GetDriverByName("ESRI Shapefile");
            if (driver == null)
                throw new Exception("ESRI Shapefile 驱动不可使用");

            Gdal.SetConfigOption("GDAL_FILENAME_IS_UTF8", "YES"); // 为了支持中文路径
            DataSource dataSource = driver.CreateDataSource(shpFilePath, null);
            if (dataSource == null)
                throw new Exception("创建SHP文件" + shpFilePath + "失败");

            DirectoryInfo directoryInfo = new DirectoryInfo(Path.GetDirectoryName(shpFilePath));
            if (!directoryInfo.Exists)
                directoryInfo.Create();

            driver.Dispose();

            return dataSource;
        }
        /// <summary>
        /// 创建坐标红线图层（生成shp文件）
        /// </summary>
        /// <returns></returns>
        public static Layer CreateRedLineLayer(DataSource dataSource, int wkid, string shpFilePath)
        {
            //创建图层
            var spatialRef = new SpatialReference("");
            spatialRef.ImportFromEPSG(wkid);
            Layer layer = dataSource.CreateLayer(Path.GetFileNameWithoutExtension(shpFilePath), spatialRef, wkbGeometryType.wkbPolygon, null);
            //定义字段
            foreach (TField baseField in CfgRedlineTxt.BlockFields)
            {
                FieldDefn fieldDefn = new FieldDefn(baseField.FieldName, FieldType.OFTString);
                fieldDefn.SetWidth(1000);
                layer.CreateField(fieldDefn, 1);
            }
            return layer;
        }
        /// <summary>
        /// 将项目坐标信息写入指定图层中
        /// （要求图层依次包含以下字段：界址点数,地块面积,地块编号,地块名称,几何类型,图幅号,地块用途,地类编码）
        /// </summary>
        /// <param name="tProjInfo"></param>
        /// <param name="layer"></param>
        public static void ToLayer(TProjInfo tProjInfo, Layer layer)
        {
            FeatureDefn featureDefn = layer.GetLayerDefn();

            //创建XMID字段，写入guid以区分项目
            var guid = Guid.NewGuid().ToString();
            const string strXmid = "XMID";
            if (featureDefn.GetFieldIndex(strXmid) < 0)
                layer.CreateField(new FieldDefn(strXmid, FieldType.OFTString), 1);

            //创建Feature
            foreach (TPolygon polygon in tProjInfo.Polygons)
            {
                Feature feature = new Feature(featureDefn);
                for (int i = 0; i < polygon.AttributeValues.Count; i++)
                {
                    feature.SetField(i, polygon.AttributeValues[i]);
                }
                feature.SetField(strXmid, guid);

                bool first = true;
                StringBuilder polygonWkt = new StringBuilder("POLYGON(");
                foreach (TRing ring in polygon.Rings)
                {
                    StringBuilder sbRingWkt = new StringBuilder((first ? "" : ",") + "(");
                    first = false;
                    bool ringFirst = true;
                    foreach (double[] doubles in ring.XyPair)
                    {
                        sbRingWkt.Append((ringFirst ? "" : ",") + doubles[1] + " " + doubles[0]);
                        ringFirst = false;
                    }
                    sbRingWkt.Append(")");
                    polygonWkt.Append(sbRingWkt);
                }
                polygonWkt.Append(")");

                Geometry geometry = Geometry.CreateFromWkt(polygonWkt.ToString());
                feature.SetGeometry(geometry);
                layer.CreateFeature(feature);
            }

            layer.SyncToDisk();
        }
        /// <summary>
        /// 将prj文件中的“墨卡托”字符替换成“高斯克吕格”
        /// （由于GDAL生成的shp文件使用墨卡托替换高斯克吕格投影，因此通过该方法强制将坐标系信息改回高斯克吕格）
        /// </summary>
        public static void Modify_Prj_To_Gauss_Kruger(string shpFilePath)
        {
            var dir = Path.GetDirectoryName(shpFilePath);
            var name = Path.GetFileNameWithoutExtension(shpFilePath);
            var prjFilePath = Path.Combine(dir, name + ".prj");
            var content =  File.ReadAllText(prjFilePath);
            content = content.Replace("Transverse_Mercator", "Gauss_Kruger").Replace("transverse_mercator", "Gauss_Kruger");
            File.WriteAllText(prjFilePath, content);
        }
        #endregion


        #region TXT与Shp互转（Gdal接口）
        /// <summary>
        /// Shp文件转TXT
        /// </summary>
        /// <param name="shpFilePath">shp文件绝对路径,包括文件名</param>
        /// <param name="txtFilePath">生成的txt文件的绝对路径,包括文件名</param>
        public static void ShpToTxt(string shpFilePath, string txtFilePath)
        {
            var tProjInfo = GetProjInfoFromShp(shpFilePath);
            ToTxtFile(txtFilePath, tProjInfo);
        }
        /// <summary>
        /// txt红线文件转换Shp文件
        /// </summary>
        /// <param name="txtFilePath">txt文件的绝对路径,包括文件名</param>
        /// <param name="shpFilePath">生成的shp文件绝对路径,包括文件名</param>
        public static void TxtToShp(string txtFilePath, string shpFilePath)
        {
            var tProjInfo = GetProjInfoFromTxt(txtFilePath);
            ToShpFile(tProjInfo, shpFilePath);
        }
        #endregion
    }
}
