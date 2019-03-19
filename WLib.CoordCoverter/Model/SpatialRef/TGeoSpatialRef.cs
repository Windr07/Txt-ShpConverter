/*---------------------------------------------------------------- 
// auth： Windragon
// date： 
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Collections.Generic;

namespace WLib.CoordCoverter.Model.SpatialRef
{
    /// <summary>
    /// 地理坐标系
    /// </summary>
    public class TGeoSpatialRef
    {
        /// <summary>
        /// 地理坐标系名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 地理坐标系别名
        /// </summary>
        public string[] AliasNames { get; set; }
        /// <summary>
        /// 地理坐标系WKID
        /// </summary>
        public int Wkid { get; set; }
        /// <summary>
        /// 包含的投影坐标系
        /// </summary>
        public List<TPrjSpatialRef> Projections { get; set; }
        /// <summary>
        /// 地理坐标系
        /// </summary>
        /// <param name="name">地理坐标系名称</param>
        /// <param name="aliasName">地理坐标系别名</param>
        /// <param name="wkid">地理坐标系WKID</param>
        public TGeoSpatialRef(string name, string[] aliasName, int wkid)
        {
            Name = name;
            AliasNames = aliasName;
            Wkid = wkid;
            Projections = new List<TPrjSpatialRef>();
        }


        /// <summary>
        /// 根据WKID获取投影坐标系信息
        /// </summary>
        /// <param name="wkid"></param>
        /// <returns></returns>
        public TPrjSpatialRef GetProjection(int wkid)
        {
            foreach (var prj in Projections)
            {
                if (prj.ZoneWkidDict.ContainsValue(wkid))
                    return prj;
            }
            return null;
        }
        /// <summary>
        /// 根据坐标系名称关键字获取投影坐标系信息
        /// </summary>
        /// <param name="keyName">坐标系名称关键字</param>
        /// <returns></returns>
        public TPrjSpatialRef GetProjection(string keyName)
        {
            foreach (var prj in Projections)
            {
                if (prj.Name.Contains(keyName) || keyName.Contains(prj.Name))
                    return prj;
                foreach (var aliasName in prj.AliasName)
                {
                    if (keyName.Contains(aliasName) || aliasName.Contains(keyName))
                        return prj;
                }
            }
            return null;
        }
        /// <summary>
        /// 根据WKID获取带号，找不到则返回0
        /// </summary>
        /// <param name="wkid">坐标系Well Known ID</param>
        /// <returns></returns>
        public int GetProjZone(int wkid)
        {
            foreach (var prj in Projections)
            {
                foreach (var pair in prj.ZoneWkidDict)
                {
                    if (pair.Value == wkid)
                        return pair.Key;
                }
            }
            return 0;
        }
        /// <summary>
        /// 根据带号获取WKID，找不到则返回0
        /// </summary>
        /// <param name="projZone">带号</param>
        /// <returns></returns>
        public int GetWkid(int projZone)
        {
            foreach (var prj in Projections)
            {
                foreach (var pair in prj.ZoneWkidDict)
                {
                    if (pair.Key == projZone)
                        return pair.Value;
                }
            }
            return 0;
        }
    }
}
