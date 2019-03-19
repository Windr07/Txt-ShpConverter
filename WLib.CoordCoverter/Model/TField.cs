/*---------------------------------------------------------------- 
// auth： Windragon
// date： 
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.CoordCoverter.Model
{
    /// <summary>
    /// 包含字段名、字段别名的字段对象
    /// </summary>
    public class TField
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 字段别名
        /// </summary>
        public string AliasName { get; }
        /// <summary>
        /// 字段默认值
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// 包含字段名、字段别名的字段对象
        /// </summary>
        /// <param name="name">字段名</param>
        /// <param name="aliasName">字段别名</param>
        public TField(string name, string aliasName)
        {
            FieldName = name;
            AliasName = aliasName;
        }
        /// <summary>
        /// 包含字段名、字段别名、字段默认值的字段对象
        /// </summary>
        /// <param name="name">字段名</param>
        /// <param name="aliasName">字段别名</param>
        /// <param name="defaultValue">默认值</param>
        public TField(string name, string aliasName, string defaultValue)
        {
            FieldName = name;
            AliasName = aliasName;
            DefaultValue = defaultValue;
        }
    }
}
