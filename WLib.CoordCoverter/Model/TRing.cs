/*---------------------------------------------------------------- 
// auth： Windragon
// date： 
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Collections.Generic;

namespace WLib.CoordCoverter.Model
{
    /// <summary>
    /// 表示一个闭合面的环对象
    /// </summary>
    public class TRing
    {
        /// <summary>
        /// 环号
        /// </summary>
        public int RingId;
        /// <summary>
        /// x,y坐标对,0位置为x,1位置为y（x，y值与ArcGIS中相反）
        /// </summary>
        public List<double[]> XyPair;

        /// <summary>
        /// 表示一个闭合面的环对象
        /// </summary>
        public TRing()
        {
            XyPair = new List<double[]>();
        }
        /// <summary>
        /// 表示一个闭合面的环对象
        /// </summary>
        public TRing(int ringId)
        {
            RingId = ringId;
            XyPair = new List<double[]>();
        }
    }
}
