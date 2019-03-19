/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2017/5/23 14:03:07
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;

namespace WLib.CoordCoverter.Utility
{
    /// <summary>
    /// 对枚举的实际意义进行描述的特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public sealed class EnumDescriptionAttribute : Attribute
    {
        /// <summary>
        /// 对枚举的文字描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 对枚举的文字描述的分类标签
        /// （由于一个枚举值可以拥有多个描述特性，此值一般设定为对描述特性进行分组，默认值为0）
        /// </summary>
        public int DescriptionTag { get; set; }
        /// <summary>
        /// 对枚举的实际意义进行描述的特性
        /// </summary>
        /// <param name="description">对枚举的文字描述</param>
        public EnumDescriptionAttribute(string description)
            : base()
        {
            this.Description = description;
        }
        /// <summary>
        /// 对枚举的实际意义进行描述的特性
        /// </summary>
        /// <param name="description">对枚举的文字描述</param>
        /// <param name="descriptionTag">对枚举的文字描述的分类标签，默认为0</param>
        public EnumDescriptionAttribute(string description, int descriptionTag)
            : base()
        {
            this.Description = description;
            this.DescriptionTag = descriptionTag;
        }
    }
}
