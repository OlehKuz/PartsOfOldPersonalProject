using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Common.Helpers
{
    public static class EnumExtentions
    {
        private static TAttribute GetEnumAttribute<TAttribute>(Type type,string enumName) where TAttribute:Attribute
        {
            var memInfo = type.GetMember(enumName);
            var attribute = memInfo.First()?.GetCustomAttribute<TAttribute>();
            return attribute;
        }
        private static TAttribute GetEnumAttribute<TAttribute>(this Enum enumValue) where TAttribute:Attribute
        {
            var type = enumValue.GetType();
            return GetEnumAttribute<TAttribute>(type, Enum.GetName(type, enumValue) ?? enumValue.ToString());
        }
        
        private static IEnumerable<TAttribute> GetEnumAttribute<TAttribute,TEnum>() where TEnum:Enum where TAttribute:Attribute
        {
            List<TAttribute> attributes = new List<TAttribute>();
            var type = typeof(TEnum);
            var names = type.GetEnumNames();
            foreach (var name in names)
            {
                var attribute = GetEnumAttribute<TAttribute>(type, name);
                if (attribute!=null) attributes.Add(attribute);
            }
            return attributes; 
        }

        public static string GetEnumValueName(Enum enumValue)
        {
            return GetEnumAttribute<EnumMemberAttribute>(enumValue).Value;
        }
        
        //TODO make yield return and check what is the difference with selectmany
        public static IEnumerable<string> GetEnumValueNames<TEnum>() where TEnum : Enum
        {
            var attributes = GetEnumAttribute<EnumMemberAttribute, TEnum>();
            return attributes.Select(a => a.Value).ToArray();
        }

    }
}