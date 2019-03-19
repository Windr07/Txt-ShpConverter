/*---------------------------------------------------------------- 
// auth： Windragon
// date： 
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Xml;
using WLib.CoordCoverter.Model;
using WLib.CoordCoverter.Model.SpatialRef;

namespace WLib.CoordCoverter.Config
{
    /// <summary>
    /// 获取红线文件的属性字段配置信息
    /// </summary>
    public class CfgRedlineTxt
    {
        /// <summary>
        /// 配置文件路径
        /// </summary>
        public static string XmlConfigPath => AppDomain.CurrentDomain.BaseDirectory + "Config\\CfgRedlineTxt.xml";

        /// <summary>
        /// XML配置文档
        /// </summary>
        private static XmlDocument _sourcexmlcfg;
        /// <summary>
        /// XML配置文档
        /// </summary>
        public static XmlDocument SourceXmlCfg
        {
            get
            {
                if (_sourcexmlcfg == null)
                {
                    _sourcexmlcfg = new XmlDocument();
                    _sourcexmlcfg.Load(XmlConfigPath);
                }
                return _sourcexmlcfg;
            }
        }

        /// <summary>
        /// 基本地块坐标的配置信息
        /// </summary>
        private static List<TField> _blockFieldList;
        /// <summary>
        /// 属性描述部分的配置信息
        /// </summary>
        private static List<TField> _attrFieldList;
        /// <summary>
        /// 支持的坐标系
        /// </summary>
        private static TGeoSpatialRefColl _geoSpatialRefs;

        /// <summary>
        /// 获取地块坐标配置信息
        /// (界址点数, 地块面积, 地块编号, 地块名称, 几何类型, 图幅号, 地块用途, 地类编码)
        /// </summary>
        /// <returns></returns>
        public static List<TField> BlockFields
        {
            get
            {
                if (_blockFieldList == null)
                    ReadFieldCfgs();
                return _blockFieldList;
            }
        }
        /// <summary>
        /// 获取属性描述配置信息
        ///  (格式版本号,数据产生单位,数据产生日期,坐标系,几度分带,投影类型,计量单位,带号,精度,转换参数)
        /// </summary>
        public static List<TField> AttrFields
        {
            get
            {
                if (_attrFieldList == null)
                    ReadFieldCfgs();
                return _attrFieldList;
            }
        }
        /// <summary>
        /// 支持的坐标系
        /// </summary>
        public static TGeoSpatialRefColl GeoSpatialRefs
        {
            get
            {
                if (_geoSpatialRefs == null)
                {
                    _geoSpatialRefs = new TGeoSpatialRefColl();

                    var geoNodes = SourceXmlCfg.SelectSingleNode("Root").SelectSingleNode("SpatialRef").SelectNodes("Geography");
                    foreach (XmlNode geoNode in geoNodes)
                    {
                        var geoInfo = new TGeoSpatialRef(
                            geoNode.Attributes["name"].Value,
                            geoNode.Attributes["aliasName"].Value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries),
                            Convert.ToInt32(geoNode.Attributes["wkid"].Value)
                        );
                        var prjNodes = geoNode.SelectNodes("Projection");
                        foreach (XmlNode prjNode in prjNodes)
                        {
                            var projInfo = new TPrjSpatialRef(
                                prjNode.Attributes["name"].Value,
                                prjNode.Attributes["aliasName"].Value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries));

                            var itemNodes = prjNode.SelectNodes("Item");
                            foreach (XmlNode item in itemNodes)
                            {
                                projInfo.ZoneWkidDict.Add(
                                    Convert.ToInt32(item.Attributes["zone"].Value),
                                    Convert.ToInt32(item.Attributes["wkid"].Value));
                            }
                            geoInfo.Projections.Add(projInfo);
                        }
                        _geoSpatialRefs.Add(geoInfo);
                    }
                }

                return _geoSpatialRefs;
            }
        }

        /// <summary>
        /// 清空缓存，将相关变量设置为null
        /// </summary>
        public static void ClearCache()
        {
            _sourcexmlcfg = null;
            _blockFieldList = null;
            _attrFieldList = null;
            _geoSpatialRefs = null;
        }
        /// <summary>
        /// 获取全部字段配置信息
        /// </summary>
        private static void ReadFieldCfgs()
        {
            try
            {
                var rootElement = SourceXmlCfg.SelectSingleNode("Root");
                var fieldsElement = rootElement.SelectSingleNode("Fields");
                _blockFieldList = ReadFieldCfg(fieldsElement, "BlockField");
                _attrFieldList = ReadFieldCfg(fieldsElement, "CoorAttField");
            }
            catch (Exception ex)
            {
                _blockFieldList = null;
                _attrFieldList = null;
                throw ex;
            }
        }
        /// <summary>
        /// 获取指定字段配置信息
        /// </summary>
        /// <param name="rootNode"></param>
        /// <param name="subNodeName"></param>
        /// <returns></returns>
        private static List<TField> ReadFieldCfg(XmlNode rootNode, string subNodeName)
        {
            var tFields = new List<TField>();
            var subNode = rootNode.SelectSingleNode(subNodeName);
            if (subNode != null)
            {
                foreach (XmlNode fieldNode in subNode.SelectNodes("Field"))
                {
                    var attrs = fieldNode.Attributes;
                    if (attrs["default"] == null)
                        tFields.Add(new TField(attrs["name"].Value, attrs["aliasName"].Value));
                    else
                        tFields.Add(new TField(attrs["name"].Value, attrs["aliasName"].Value, attrs["default"].Value));
                }
            }
            return tFields;
        }
    }
}
