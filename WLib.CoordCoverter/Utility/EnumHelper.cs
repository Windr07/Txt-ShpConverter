/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2017/5/23 14:04:44
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;

namespace WLib.CoordCoverter.Utility
{
    /// <summary>
    /// 枚举帮助类
    /// (使用反射机制，调用时注意效率优化)
    /// </summary> 
    public static class EnumHelper
    {
        /// <summary>
        /// 获得指定枚举值的第一个描述
        /// （即获取枚举的自定义特性EnumDescriptionAttribute的Description属性）
        /// </summary>
        /// <param name="value">枚举值</param>
        /// <param name="decriptionTag">对枚举的描述的分类标签</param>
        /// <returns></returns>
        public static string GetDescription(Enum value, int decriptionTag = 0)
        {
            if (value == null)
                throw new ArgumentException("枚举参数value为空！");

            string name = value.ToString();
            string description = name;
            var fieldInfo = value.GetType().GetField(name);
            var attributes = (EnumDescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                EnumDescriptionAttribute attribute = null;
                foreach (var att in attributes)
                {
                    if (att.DescriptionTag == decriptionTag)
                    {
                        attribute = att;
                        break;
                    }
                }
                description = attribute?.Description;
            }
            return description;
        }
        /// <summary>
        /// 根据枚举的第一个描述（即自定义特性EnumDescriptionAttribute的Description值），返回枚举值，T必须是枚举类型
        /// </summary>
        /// <param name="description">指定枚举值的描述，此值应是枚举的自定义特性EnumDescriptionAttribute的Description属性值</param>
        /// <param name="descriptionTag">对枚举的描述的分类标签</param>
        /// <returns>返回查找的枚举值</returns>
        public static T GetEnum<T>(string description, int descriptionTag = 0) where T : struct
        {
            object result = null;
            Dictionary<string, string> dict = GetNameAndDescriptionDict<T>(descriptionTag);
            string enumName = null;
            if (dict.ContainsValue(description))
            {
                foreach (var pair in dict)
                {
                    if (pair.Value == description) { enumName = pair.Key; break; }
                }
                result = Enum.Parse(typeof(T), enumName);
            }
            return (T)result;
        }
        /// <summary>
        /// 获得枚举的所有枚举值对应的第一个描述
        /// （即获取所有枚举值的自定义特性EnumDescriptionAttribute的Description属性，T必须是枚举类型）
        /// </summary>
        /// <returns></returns>
        public static string[] GetDescriptions<T>(int descriptionTag = 0) where T : struct
        {
            var dict = GetNameAndDescriptionDict<T>(descriptionTag);
            var values = new List<string>();
            foreach (var pair in dict)
            {
                values.Add(pair.Value);
            }
            return values.ToArray();
        }
        /// <summary>
        /// 获得枚举的所有枚举值的名称和第一个描述的键值对，T必须是枚举类型
        /// </summary>
        /// <param name="decriptionTag">对枚举的描述的分类标签</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetNameAndDescriptionDict<T>(int decriptionTag) where T : struct
        {
            var enumType = typeof(T);
            string[] names = Enum.GetNames(enumType);
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (var name in names)
            {
                var fieldInfo = enumType.GetField(name);
                var attributes = (EnumDescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
                if (attributes.Length > 0)
                {
                    EnumDescriptionAttribute attribute = null;
                    foreach (var att in attributes)
                    {
                        if (att.DescriptionTag == decriptionTag)
                        {
                            attribute = att;
                            break;
                        }
                    }
                    result.Add(name, attribute == null ? name : attribute.Description);
                }
                else
                {
                    result.Add(name, name);
                }
            }
            return result;
        }
    }
}
