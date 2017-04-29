using System;
using System.ComponentModel;
using MarkDown.Generator.Exceptions;

namespace MarkDown.Generator.Extensions
{
    internal static class EnumExtensions
    {
        /// <summary>
        /// Get description attribute value
        /// </summary>
        /// <param name="enumerationValue"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string ToDescription<T>(this T enumerationValue)
            where T : struct
        {
            var type = enumerationValue.GetType();
            ConditionValidator.ThrowExceptionIfNotValid<ArgumentException>((!type.IsEnum),
                $"EnumerationValue must be of Enum type {enumerationValue}");
            var memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo.Length <= 0) return enumerationValue.ToString();
            var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attrs.Length > 0 ? ((DescriptionAttribute) attrs[0]).Description : enumerationValue.ToString();
        }
    }
}
