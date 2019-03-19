/*---------------------------------------------------------------- 
// auth： Windragon
// date： 
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.CoordCoverter.Model
{
    /// <summary>
    /// txt文件的读取标识枚举
    /// </summary>
    public enum ETxtContent : int
    {
        /// <summary>
        /// 项目信息标识
        /// </summary>
        Project = 0,
        /// <summary>
        /// 属性信息标识
        /// </summary>
        Property = 1,
        /// <summary>
        /// 坐标信息标识
        /// </summary>
        Coordinate = 2,
        /// <summary>
        /// 没有定义的标识
        /// </summary>
        NoDefine = -1
    }
}
