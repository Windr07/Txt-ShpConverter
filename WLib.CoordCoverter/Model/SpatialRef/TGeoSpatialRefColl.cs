/*---------------------------------------------------------------- 
// auth： Windragon
// date： 
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace WLib.CoordCoverter.Model.SpatialRef
{
    /// <summary>
    /// 地理坐标系集合
    /// </summary>
    public class TGeoSpatialRefColl : IEnumerable<TGeoSpatialRef>
    {
        /// <summary>
        /// 地理坐标系集合
        /// </summary>
        private readonly List<TGeoSpatialRef> _infos = new List<TGeoSpatialRef>();

        /// <summary>
        /// 添加地理坐标系信息
        /// </summary>
        /// <param name="info"></param>
        public void Add(TGeoSpatialRef info)
        {
            _infos.Add(info);
        }
        /// <summary>
        /// 添加多个地理坐标系信息
        /// </summary>
        /// <param name="infos"></param>
        public void AddRange(IEnumerable<TGeoSpatialRef> infos)
        {
            _infos.AddRange(infos);
        }
        /// <summary>
        /// 获取或设置指定索引处的地理坐标系信息
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public TGeoSpatialRef this[int index]
        {
            get => _infos[index];
            set => _infos[index] = value;
        }

        /// <summary>
        /// 获取或设置指定名称或别名的地理坐标系信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public TGeoSpatialRef this[string name]
        {
            get
            {
                foreach (var info in _infos)
                {
                    if (info.Name == name)
                        return info;
                    foreach (var aliasName in info.AliasNames)
                    {
                        if (aliasName == name)
                            return info;
                    }
                }
                return null;
            }
        }
        /// <summary>
        /// 判断集合中是否包含指定的地理坐标系信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool Contains(TGeoSpatialRef info)
        {
            return _infos.Contains(info);
        }
        /// <summary>
        ///  实现IEnumerable T方法，允许foreach迭代
        /// </summary>
        /// <returns></returns>
        IEnumerator<TGeoSpatialRef> IEnumerable<TGeoSpatialRef>.GetEnumerator()
        {
            foreach (var info in _infos)
            {
                yield return info;
            }
        }

        /// <summary>
        /// 实现IEnumerable方法，允许foreach迭代
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < _infos.Count; i++)
            {
                yield return _infos[i];
            }
        }
        /// <summary>
        /// 根据WKID获取地理坐标系
        /// </summary>
        /// <param name="wkid"></param>
        /// <returns></returns>
        public TGeoSpatialRef GetGeoSpatialRef(int wkid)
        {
            foreach (var geoSpatialRef in _infos)
            {
                if (geoSpatialRef.GetProjection(wkid) != null)
                    return geoSpatialRef;
            }
            return null;
        }
        /// <summary>
        /// 根据坐标系名称关键字获取地理坐标系
        /// </summary>
        /// <param name="wktName">坐标系名称关键字</param>
        /// <returns></returns>
        public TGeoSpatialRef GetGeoSpatialRef(string wktName)
        {
            foreach (var geoSpatialRef in _infos)
            {
                foreach (var aliasName in geoSpatialRef.AliasNames)
                {
                    if (aliasName.Contains(wktName) || wktName.Contains(aliasName))
                        return geoSpatialRef;
                }

                if (geoSpatialRef.GetProjection(wktName) != null)
                    return geoSpatialRef;
            }
            return null;
        }


        /// <summary>
        /// 输出使用","拼接每个坐标系的别名
        /// </summary>
        /// <returns></returns>
        public string GetGeoSpatialRefAliasNames()
        {
            var sbName = new StringBuilder();
            foreach (var geoSpatialRef in _infos)
            {
                sbName.Append(geoSpatialRef.Name + ",");
            }
            sbName.Remove(sbName.Length - 1, 1);
            return sbName.ToString();
        }
    }
}
