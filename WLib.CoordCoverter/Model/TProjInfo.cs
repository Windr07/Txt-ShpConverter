/*---------------------------------------------------------------- 
// auth： Windragon
// date： 
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using WLib.CoordCoverter.Config;

namespace WLib.CoordCoverter.Model
{
    /// <summary>
    /// 代表一个坐标红线项目，存储从txt或shp坐标文件获取的全部坐标红线信息
    /// </summary>
    public class TProjInfo
    {
        /// <summary>
        /// 坐标系代码WKID
        /// </summary>
        private int _wkid = -1;
        /// <summary>
        /// 带号
        /// </summary>
        private int _projZone = -1;
        /// <summary>
        /// txt中的"[项目信息]"字典
        /// </summary>
        public Dictionary<string, string> ProjectDict;
        /// <summary>
        /// txt中的"[属性信息]"字典
        /// </summary>
        public Dictionary<string, string> PropertyDict;
        /// <summary>
        /// txt中的"[图形信息]",即多边形地块的集合
        /// </summary>
        public List<TPolygon> Polygons;
        /// <summary>
        /// 坐标系代码WKID
        /// </summary>
        public int Wkid
        {
            get
            {
                if (_wkid == -1)
                {
                    try
                    {
                        if (!PropertyDict.ContainsKey("坐标系"))
                            throw new Exception("TXT坐标文件未定义坐标系！");

                        var coorSysName = PropertyDict["坐标系"];
                        var supportCoorSysNames = CfgRedlineTxt.GeoSpatialRefs.GetGeoSpatialRefAliasNames();
                        var geoSpatialRef = CfgRedlineTxt.GeoSpatialRefs[coorSysName];

                        if (geoSpatialRef == null)
                            throw new Exception($"不支持TXT所描述的坐标系({coorSysName})\r\n当前支持的坐标系为：{supportCoorSysNames}");

                        if (!geoSpatialRef.Projections[0].ZoneWkidDict.ContainsKey(ProjZone))
                            throw new Exception("TXT坐标文件填写的带号不符合带号范围(25-45)！");

                        _wkid = geoSpatialRef.Projections[0].ZoneWkidDict[ProjZone];
                    }
                    catch (Exception ex) { throw new Exception("读取坐标文件信息失败，请检查坐标文件规范：" + ex.Message); }
                }
                return _wkid;
            }
        }
        /// <summary>
        /// 带号
        /// </summary>
        public int ProjZone
        {
            get
            {
                if (_projZone == -1)
                {
                    if (!PropertyDict.ContainsKey("带号") || string.IsNullOrEmpty(PropertyDict["带号"]))
                        throw new Exception("TXT坐标文件未定义带号！");
                    ;
                    if (!int.TryParse(PropertyDict["带号"].Trim(), out _projZone))
                        throw new Exception("TXT坐标文件填写的带号不符合要求！");
                }
                return _projZone;
            }
        }


        /// <summary>
        /// 代表一个坐标红线项目，存储从txt或shp坐标文件获取的全部坐标红线信息
        /// </summary>
        public TProjInfo()
        {
            Polygons = new List<TPolygon>();
            ProjectDict = new Dictionary<string, string>();
            PropertyDict = new Dictionary<string, string>();
        }
        /// <summary>
        /// 添加一条项目信息，如：项目名称=罗定市环城公路东段建设项目
        /// </summary>
        /// <param name="lineStr"></param>
        public void AddProjectDict(string lineStr)
        {
            string[] tempMsg = lineStr.Split('=');
            ProjectDict.Add(tempMsg[0].Trim(), tempMsg.Length == 1 ? "" : tempMsg[1].Trim());
        }
        /// <summary>
        /// 添加一条属性描述，如：坐标系=80国家大地坐标系
        /// </summary>
        /// <param name="lineStr"></param>
        public void AddPropertyDict(string lineStr)
        {
            string[] tempMsg = lineStr.Split('=');
            PropertyDict.Add(tempMsg[0].Trim(), tempMsg.Length == 1 ? "" : tempMsg[1].Trim());
        }
    }
}
