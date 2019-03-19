/*---------------------------------------------------------------- 
// auth： Windragon
// date： 
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using OSGeo.GDAL;
using OSGeo.OGR;
using OSGeo.OSR;

namespace WLib.CoordCoverter.Utility
{
    /// <summary>
    /// GDAL帮助类
    /// </summary>
    public class GdalHelper
    {
        /// <summary>
        /// 初始化Gdal，注册所有驱动并支持中文
        /// </summary>
        public static void GdalInit()
        {
            //若出现【无法加载 DLL“ogr_wrap”: 找不到指定的模块】，
            //请将附带的DLL.rar中的全部文件解压复制到生成目录bin\x86\Debug（或bin\x86\Debug\DLL），同时安装附带的VC++包（Vcredist_x86.exe）
            //Exception: Unable to load DLL "ogr_wrap": Cannot find the specified module.
            //Solution: ① Please unzip all the files in the attached DLL.rar to the build directory bin\x86\Debug(or bin\x86\Debug\DLL)
            //          ② Install VC++ package (Vcredist_x86.exe)
            Ogr.RegisterAll(); // 注册所有GDAL的驱动，Register all GDAL drivers
            Gdal.AllRegister();
            Gdal.SetConfigOption("GDAL_FILENAME_IS_UTF8", "YES"); // 为了支持中文路径
            Gdal.SetConfigOption("SHAPE_ENCODING", "CP936"); // 为了使属性表字段支持中文CP936
            Gdal.SetConfigOption("GDAL_DATA", AppDomain.CurrentDomain.BaseDirectory + "Data");//设置GDAL Data文件夹位置
        }
        /// <summary>
        /// 打开数据源，执行指定操作，关闭数据源并释放资源
        /// </summary>
        /// <param name="pathOrConnStr">数据源文件路径或连接字符串</param>
        /// <param name="action">打开数据源后进行的操作</param>
        /// <returns>返回异常信息，未发生异常则返回null</returns>
        public static string OpenSource(string pathOrConnStr, Action<DataSource> action)
        {
            DataSource dataSource = null;
            try
            {
                dataSource = Ogr.Open(pathOrConnStr, 0);
                if (dataSource == null)
                    return "无法读取数据源内容";

                action(dataSource);
                return null;
            }
            catch (Exception ex)
            {
                return "出现错误，异常信息为：" + ex.Message;
            }
            finally
            {
                if (dataSource != null)
                {
                    dataSource.FlushCache();
                    dataSource.Dispose();
                }
            }
        }
        /// <summary>
        /// 在指定路径下构建shp数据源（此操作不会生成实际的shp文件）
        /// </summary>
        /// <param name="shpFilePath">shp文件路径</param>
        /// <returns></returns>
        public static DataSource CreateShapefileSource(string shpFilePath)
        {
            OSGeo.OGR.Driver driver = Ogr.GetDriverByName("ESRI Shapefile");
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


        #region 图层、要素、值
        /// <summary>
        /// 重置图层的读取，重新设置图层的筛选条件
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="whereClause"></param>
        /// <param name="geometry"></param>
        private static void ResetLayerReading(Layer layer, string whereClause = null, Geometry geometry = null)
        {
            layer.ResetReading();
            //if (whereClause != null)
            layer.SetAttributeFilter(whereClause);
            //if (geometry != null)
            layer.SetSpatialFilter(geometry);
        }
        /// <summary>
        /// 按照名称查找图层
        /// </summary>
        /// <returns></returns>
        public static Layer GetLayerByName(DataSource dataSource, string layerName, bool notFoundException = true)
        {
            layerName = layerName.Replace("dbo.", "");

            Layer resultLayer = null;
            for (int i = 0; i < dataSource.GetLayerCount(); i++)
            {
                var layer = dataSource.GetLayerByIndex(i);
                if (layer == null)
                    throw new Exception($"按照方法GetLayerByIndex在索引{i}处无法查找图层");
                if (layer.GetName() == layerName)
                {
                    resultLayer = layer;
                    break;
                }
            }

            if (resultLayer == null && notFoundException)
                throw new Exception($"从指定GDAL数据源中找不到名为{layerName}的图层，请确认以下几项：" +
                                    "\r\n图层名是否有误/数据库是否完整/geometry_columns表是否包含相应数据");
            return resultLayer;
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
        /// <summary>
        /// （根据查询条件）获取图层中的要素
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="whereClause"></param>
        /// <param name="geometry"></param>
        /// <returns></returns>
        public static List<Feature> GetFeatures(Layer layer, string whereClause = null, Geometry geometry = null)
        {
            ResetLayerReading(layer, whereClause, geometry);

            var features = new List<Feature>();
            Feature oFeature = layer.GetNextFeature();
            while (oFeature != null)
            {
                features.Add(oFeature);
                oFeature = layer.GetNextFeature();
            }
            return features;
        }
        /// <summary>
        /// （根据查询条件）获取图层中的要素的OID
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="whereClause"></param>
        /// <param name="geometry"></param>
        /// <returns></returns>
        public static List<long> GetFeatureFids(Layer layer, string whereClause = null, Geometry geometry = null)
        {
            ResetLayerReading(layer, whereClause, geometry);

            var oids = new List<long>();
            Feature oFeature = null;
            while ((oFeature = layer.GetNextFeature()) != null)
            {
                oids.Add(oFeature.GetFID());
            }
            return oids;
        }
        /// <summary>
        /// （根据查询条件）获取从图层中找到的第一条要素
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="whereClause"></param>
        /// <param name="geometry"></param>
        /// <returns></returns>
        public static Feature GetFirstFeature(Layer layer, string whereClause = null, Geometry geometry = null)
        {
            ResetLayerReading(layer, whereClause, geometry);
            return layer.GetNextFeature();
        }
        /// <summary>
        /// （根据查询条件）获取从图层中找到的第一条要素的指定字段值
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="fieldName"></param>
        /// <param name="whereClause"></param>
        /// <param name="geometry"></param>
        /// <returns></returns>
        public static string GetFirstStrValue(Layer layer, string fieldName, string whereClause = null, Geometry geometry = null)
        {
            var feature = GetFirstFeature(layer, whereClause, geometry);
            if (feature == null)
                throw new Exception($"根据条件“{whereClause}”在{layer.GetName()}图层中找不到记录/要素");
            return feature.GetFieldAsString(fieldName);
        }
        /// <summary>
        /// （根据查询条件）获取图层中的图斑
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="whereClause"></param>
        /// <param name="geometry"></param>
        /// <returns></returns>
        public static List<Geometry> GetGeometries(Layer layer, string whereClause = null, Geometry geometry = null)
        {
            ResetLayerReading(layer, whereClause, geometry);

            var geometries = new List<Geometry>();
            Feature oFeature = null;
            while ((oFeature = layer.GetNextFeature()) != null)
            {
                geometries.Add(oFeature.GetGeometryRef());
            }
            return geometries;
        }
        /// <summary>
        /// （根据查询条件）获取图层中的要素，并指定更新的操作
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="action"></param>
        /// <param name="whereClause"></param>
        /// <param name="geometry"></param>
        public static void UpdateFeatures(Layer layer, Action<Feature> action, string whereClause = null, Geometry geometry = null)
        {
            ResetLayerReading(layer, whereClause, geometry);

            Feature oFeature = null;
            while ((oFeature = layer.GetNextFeature()) != null)
            {
                action(oFeature);
            }
        }
        /// <summary>
        /// （根据查询条件）删除图层中的要素
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="whereClause"></param>
        /// <param name="geometry"></param>
        /// <returns>删除的要素的个数</returns>
        public static int DeleteFeatures(Layer layer, string whereClause = null, Geometry geometry = null)
        {
            var oids = GetFeatureFids(layer, whereClause, geometry);
            oids.ForEach(oid => layer.DeleteFeature(oid));
            return oids.Count;
        }
        #endregion


        #region 投影和投影变换
        /// <summary>
        /// 获取投影坐标系名称
        /// </summary>
        /// <param name="prjectionSpatialRef">投影坐标系</param>
        /// <seealso cref="http://spatialreference.org/ref/"/>
        /// <returns></returns>
        public static string GetProjectionSrName(SpatialReference prjectionSpatialRef)
        {
            prjectionSpatialRef.AutoIdentifyEPSG();
            var projsc = prjectionSpatialRef.GetAttrValue("PROJCS", 0);
            return projsc;
        }
        /// <summary>
        /// 获取地理坐标系名称
        /// </summary>
        /// <param name="geographySpatialRef">地理坐标系</param>
        /// <seealso cref="http://spatialreference.org/ref/"/>
        /// <returns></returns>
        public static string GetGeographySrName(SpatialReference geographySpatialRef)
        {
            geographySpatialRef.AutoIdentifyEPSG();
            var projsc = geographySpatialRef.GetAttrValue("GEOGCS", 0);
            return projsc;
        }
        /// <summary>
        /// 获取投影坐标系WKID
        /// </summary>
        /// <param name="prjectionSpatialRef">投影坐标系</param>
        /// <seealso cref="http://spatialreference.org/ref/"/>
        /// <returns></returns>
        public static int GetWkid(SpatialReference prjectionSpatialRef)
        {
            prjectionSpatialRef.AutoIdentifyEPSG();

            var strProjcs = prjectionSpatialRef.GetAttrValue("AUTHORITY", 1);

            if (!Int32.TryParse(strProjcs, out int prjcs))
                throw new Exception("无法通过GDAL SpatialReference.GetAttrValue('AUTHORITY',1)方法从坐标系中获取WKID，请确认坐标系是否符合规范");
            return prjcs;
        }
        /// <summary>
        /// 将投影坐标转成地理坐标
        /// </summary>
        /// <param name="xyGeometries">投影坐标系图斑</param>
        /// <param name="projectionSpatialRef">投影坐标系</param>
        /// <returns></returns>
        public static Geometry[] ProjectionToGeography(Geometry[] xyGeometries, SpatialReference projectionSpatialRef)
        {
            //将投影坐标转成地理坐标
            var geographySpatialRef = projectionSpatialRef.CloneGeogCS(); //获取投影坐标对应的地理坐标系
            var coordinateTransformation =
                Osr.CreateCoordinateTransformation(projectionSpatialRef, geographySpatialRef);
            var llGeometries = new List<Geometry>(); //转换坐标系后的图斑
            for (int i = 0; i < xyGeometries.Length; i++)
            {
                var llGoemetry = xyGeometries[i].Clone();
                llGoemetry.Transform(coordinateTransformation); //转换坐标系
                llGeometries.Add(llGoemetry);
            }
            return llGeometries.ToArray();
        }
        /// <summary>
        /// 将地理坐标转成投影坐标
        /// </summary>
        /// <param name="llGeomerty"></param>
        /// <param name="geographySpatialRef"></param>
        /// <param name="projectionSpatialRef"></param>
        /// <returns></returns>
        public static Geometry GeographyToProjection(Geometry llGeomerty, SpatialReference geographySpatialRef, SpatialReference projectionSpatialRef)
        {
            llGeomerty.AssignSpatialReference(geographySpatialRef);
            llGeomerty.ExportToWkt(out string wkt);

            var coordinateTransformation =
                Osr.CreateCoordinateTransformation(geographySpatialRef, projectionSpatialRef);
            var xyGoemetry = llGeomerty.Clone();
            xyGoemetry.Transform(coordinateTransformation);
            return xyGoemetry;
        }
        /// <summary>
        /// 转换坐标系
        /// </summary>
        /// <param name="sourceShpPath"></param>
        /// <param name="resultShpPath"></param>
        /// <param name="resultSpaitalRef"></param>
        /// <param name="isPrjToGeo"></param>
        public static void ConvertSpatialRef(string sourceShpPath, string resultShpPath, SpatialReference resultSpaitalRef = null, bool isPrjToGeo = true)
        {
            //打开源shp文件
            var dataSource = Ogr.Open(sourceShpPath, 0);
            if (dataSource == null)
                throw new Exception("GDAL打开数据源失败，请确定GDAL已正常初始化、数据源存在且数据可读！");
            var sourceLayer = dataSource.GetLayerByIndex(0);
            var sourceSpatialRef = sourceLayer.GetSpatialRef();
            if (sourceSpatialRef == null)
                throw new Exception("坐标系为空，请先正确设置坐标系！");
            if (isPrjToGeo)
            {
                if (resultSpaitalRef == null)
                    resultSpaitalRef = sourceSpatialRef.CloneGeogCS();
                if (sourceSpatialRef.IsProjected() == 0)
                    throw new Exception("源shp文件坐标系不是投影坐标系！");
            }
            else
            {
                if (resultSpaitalRef == null)
                    resultSpaitalRef = TryGetProjectionSpatialRef(sourceLayer);
                if (sourceSpatialRef.IsGeographic() == 0)
                    throw new Exception("源shp文件坐标系不是地理坐标系！");
            }

            //获取坐标转换规则
            var coordinateTransformation = Osr.CreateCoordinateTransformation(sourceSpatialRef, resultSpaitalRef);

            //创建结果shp文件
            var sourceFeatureDefn = sourceLayer.GetLayerDefn();
            var resultDataSource = CreateShapefileSource(resultShpPath);
            var resultLayer = resultDataSource.CreateLayer(Path.GetFileNameWithoutExtension(resultShpPath), resultSpaitalRef, wkbGeometryType.wkbPolygon, null);
            for (int i = 0; i < sourceFeatureDefn.GetFieldCount(); i++)
            {
                var sourceFieldDefn = sourceFeatureDefn.GetFieldDefn(i);
                resultLayer.CreateField(sourceFieldDefn, 1);
            }

            //投影转地理坐标系并保存
            var sourceFeatures = GetFeatures(sourceLayer);
            var resultFeatureDefn = resultLayer.GetLayerDefn();
            foreach (var sourceFeature in sourceFeatures)
            {
                var sourceGeometry = sourceFeature.GetGeometryRef();
                var resultGeometry = sourceGeometry.Clone();
                resultGeometry.Transform(coordinateTransformation); //转换坐标系
                Feature resultFeature = new Feature(resultFeatureDefn);
                resultFeature.SetFrom(sourceFeature, 1);
                resultFeature.SetGeometry(resultGeometry);
                resultLayer.CreateFeature(resultFeature);
            }
            resultLayer.SyncToDisk();
        }
        /// <summary>
        /// 尝试根据地理坐标系图层的第一个图形的坐标点，获取其对应的高斯克吕格3度分带含带号投影坐标系
        /// （判断西安80坐标和国家2000坐标）
        /// </summary>
        /// <param name="layer">判断坐标系的图层，该图层的坐标系应为地理坐标系</param>
        /// <returns></returns>
        public static SpatialReference TryGetProjectionSpatialRef(Layer layer)
        {
            var spatialRef = layer.GetSpatialRef();
            var geoSpatialRef = spatialRef.CloneGeogCS();

            var wkt = string.Empty;
            geoSpatialRef.ExportToWkt(out wkt);
            var feature = GetFirstFeature(layer);
            var geometry = feature.GetGeometryRef();
            if (geometry.IsEmpty())
                return null;
            if (geometry.GetGeometryType() == wkbGeometryType.wkbMultiPolygon)
                geometry = geometry.GetGeometryRef(0);

            if (geometry.GetGeometryType() == wkbGeometryType.wkbPolygon)
                geometry = geometry.GetGeometryRef(0);
            var lat = geometry.GetX(0);
            if (lat < 73.5 || lat > 136.5)
                return null;

            var wkid = -1;
            if (wkt.Contains("4610") | wkt.Contains("Xian 1980"))
            {
                wkid = 2349 + (int)((lat - 73.5) / 3);
            }
            if (wkt.Contains("4490") || wkt.Contains("CGCS 2000"))
            {
                wkid = 4513 + (int)((lat - 73.5) / 3);
            }

            SpatialReference prjSpatialRef = new SpatialReference("");
            prjSpatialRef.ImportFromEPSG(wkid);
            return prjSpatialRef;
        }
        #endregion


        #region 字段操作
        /// <summary>
        /// 获取图层中的全部字段
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static List<FieldDefn> GetFields(Layer layer)
        {
            var fields = new List<FieldDefn>();
            FeatureDefn featureDefn = layer.GetLayerDefn();
            for (int i = 0; i < featureDefn.GetFieldCount(); i++)
            {
                fields.Add(featureDefn.GetFieldDefn(i));
            }

            return fields;
        }
        /// <summary>
        /// 获取图层中的全部字段名称
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static List<string> GetFieldNames(Layer layer)
        {
            var fields = GetFields(layer);
            var names = new List<string>();
            foreach (var field in fields)
            {
                names.Add(field.GetName());
            }
            return names;
        }
        /// <summary>
        /// 将GDAL字段类型转成.NET类型
        /// </summary>
        /// <param name="fieldType"></param>
        /// <returns></returns>
        public static Type ConvertFieldType(FieldType fieldType)
        {
            Type type = null;
            switch (fieldType)
            {
                case FieldType.OFTInteger:
                    type = typeof(int);
                    break;
                case FieldType.OFTIntegerList:
                    type = typeof(int[]);
                    break;
                case FieldType.OFTReal:
                    type = typeof(double);
                    break;
                case FieldType.OFTRealList:
                    type = typeof(double[]);
                    break;
                case FieldType.OFTString:
                    type = typeof(string);
                    break;
                case FieldType.OFTStringList:
                    type = typeof(string[]);
                    break;
                case FieldType.OFTDate:
                    type = typeof(DateTime);
                    break;
                case FieldType.OFTTime:
                    type = typeof(DateTime);
                    break;
                case FieldType.OFTDateTime:
                    type = typeof(DateTime);
                    break;
                case FieldType.OFTInteger64:
                    type = typeof(long);
                    break;
                case FieldType.OFTInteger64List:
                    type = typeof(long[]);
                    break;
                    //case FieldType.OFTWideString:
                    //    type = typeof(int);
                    //    break;
                    //case FieldType.OFTWideStringList:
                    //    type = typeof(int);
                    //    break;
                    //case FieldType.OFTBinary:
                    //    type = typeof(int);
                    //    break;
            }

            return type;
        }
        #endregion


        /// <summary>
        /// 将几何图形转成GeoJson
        /// </summary>
        /// <param name="geometries">几何图形</param>
        /// <param name="properties">属性值，可以为null</param>
        /// <returns></returns>
        public static string GeometriesToJson(IEnumerable<Geometry> geometries, string[] properties)
        {
            StringBuilder json = new StringBuilder("{\"type\":\"FeatureCollection\",\"features\":[");
            int index = 0;
            foreach (var geometry in geometries)
            {
                string geometryJson = geometry.ExportToJson(null);
                json.Append((index == 0 ? "" : ",") +
                            "{\"type\":\"Feature\",\"geometry\":" +
                            geometryJson +
                            (properties == null ? "" : "," + properties[index]) +
                            "}");
                index++;
            }

            json.Append("]}");
            return json.ToString();
        }
        /// <summary>
        /// 创建shp文件，将几何图斑保存到shp中
        /// </summary>
        /// <param name="shpPath">shp文件保存路径</param>
        /// <param name="spatialRef">坐标系</param>
        /// <param name="geometries">要保存的几何图斑</param>
        /// <returns></returns>
        public static DataSource SaveToShpFile(string shpPath, SpatialReference spatialRef, Geometry[] geometries)
        {
            //创建数据源
            var driver = Ogr.GetDriverByName("ESRI Shapefile");
            DataSource dataSource = driver.CreateDataSource(shpPath, null);

            //创建图层
            var name = System.IO.Path.GetFileNameWithoutExtension(shpPath);
            var geometryType = geometries[0].GetGeometryType();
            Layer layer = dataSource.CreateLayer(name, spatialRef, geometryType, null);

            //创建要素
            FeatureDefn featureDef = layer.GetLayerDefn();
            foreach (var geometry in geometries)
            {
                Feature feature = new Feature(featureDef);
                feature.SetGeometry(geometry);
                layer.CreateFeature(feature);
            }

            //同步到文件中
            layer.SyncToDisk();
            return dataSource;
        }
        /// <summary>
        /// 根据图层字段信息创建一个空的DataTable
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static DataTable LayerToEmptyDataTable(Layer layer)
        {
            var fields = GetFields(layer);
            var features = GetFeatures(layer);

            var dataTable = new DataTable(layer.GetName());
            foreach (var field in fields)
            {
                Type type = ConvertFieldType(field.GetFieldType());
                dataTable.Columns.Add(field.GetName(), type);
            }

            return dataTable;
        }
        /// <summary>
        /// 创建DataTable并将图层数据全部转为字符串写入DataTable中
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static DataTable LayerToStrDataTable(Layer layer)
        {
            var fields = GetFields(layer);
            var features = GetFeatures(layer);

            var dataTable = new DataTable(layer.GetName());
            fields.ForEach(field => dataTable.Columns.Add(field.GetName()));
            features.ForEach(feature =>
            {
                var values = new string[fields.Count];
                for (int i = 0; i < fields.Count; i++)
                {
                    values[i] = feature.GetFieldAsString(i);
                }

                dataTable.Rows.Add(values);
            });

            return dataTable;
        }
    }
}
