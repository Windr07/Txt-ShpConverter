/*---------------------------------------------------------------- 
// auth： Windragon
// date： 
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.CoordCoverter.Model
{
    /// <summary>
    /// shp文件信息，包括shp图层坐标系WKID、shp文件路径
    /// </summary>
    public class ShpSourceInfo
    {
        /// <summary>
        /// 坐标系Well Konwn Id
        /// </summary>
        public int Wkid { get; set; }
        /// <summary>
        /// shp文件所在路径
        /// </summary>
        public string ShpPath { get; set; }
        /// <summary>
        /// shp文件信息，包括shp图层坐标系WKID、shp文件路径
        /// </summary>
        public ShpSourceInfo() { }
    }
}
