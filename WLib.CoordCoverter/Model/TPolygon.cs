/*---------------------------------------------------------------- 
// auth： Windragon
// date： 
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Collections.Generic;
using WLib.CoordCoverter.Config;

namespace WLib.CoordCoverter.Model
{
    /// <summary>
    /// 代表一个闭合多边形的信息
    /// </summary>
    public class TPolygon
    {
        /// <summary>
        /// 存放属性的值列表,顺序和位置是固定的：
        /// 0-界址点数,1-地块面积,2-地块编号,3-地块名称,4-几何类型(点、线、面),5-图幅号,6-地块用途,7-地类编码
        /// </summary>
        public List<string> AttributeValues { get; }
        /// <summary>
        /// 表示多边形的面，由一环或多环组成
        /// </summary>
        public List<TRing> Rings { get; }
        /// <summary>
        /// 代表一个闭合多边形的信息
        /// </summary>
        public TPolygon()
        {
            AttributeValues = new List<string>();
            Rings = new List<TRing>();
        }

        /// <summary>
        /// 添加坐标
        /// </summary>
        /// <param name="ringId">环号</param>
        /// <param name="x">坐标文件中的X坐标（纵坐标，对应GIS中的Y坐标）</param>
        /// <param name="y">坐标文件中的Y坐标（横坐标，一般包含带号，对应GIS中的X坐标）</param>
        public void AddXY(int ringId, double x, double y)
        {
            TRing ring = Rings.Find(p => p.RingId == ringId);
            if (ring == null)
            {
                ring = new TRing(ringId);
                Rings.Add(ring);
            }
            ring.XyPair.Add(new[] { x, y });
        }
        /// <summary>
        /// 添加属性
        /// </summary>
        /// <param name="values"></param>
        public void AddAttribute(string[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (i < CfgRedlineTxt.BlockFields.Count)
                {
                    if (!values[i].Contains("@"))
                        AttributeValues.Add(values[i]);
                }
                else
                    break;
            }
        }
    }
}
