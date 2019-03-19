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
    /// 投影坐标系信息
    /// </summary>
    public class TPrjSpatialRef
    {
        /// <summary>
        /// 投影坐标系名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 投影坐标系别名
        /// </summary>
        public string[] AliasName { get; set; }
        /// <summary>
        /// 投影坐标系各分带的带号和WKID
        /// </summary>
        public Dictionary<int, int> ZoneWkidDict { get; set; }

        /// <summary>
        /// 投影坐标系信息
        /// </summary>
        /// <param name="name">投影坐标系名称</param>
        /// <param name="aliasName">投影坐标系别名</param>
        public TPrjSpatialRef(string name, string[] aliasName)
        {
            Name = name;
            AliasName = aliasName;
            ZoneWkidDict = new Dictionary<int, int>();
        }
    }
}
