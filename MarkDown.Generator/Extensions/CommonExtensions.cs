using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using MarkDown.Generator.Interfaces;
using MarkDown.Generator.Models;

namespace MarkDown.Generator.Extensions
{
    internal static class CommonExtensions
    {
        public static bool CheckAttribute<T>(this Type type) where T : Attribute
        {
            var entity = type.GetCustomAttribute<T>(false);
            return entity != null;
        }
        public static bool CheckAttribute<T>(this PropertyInfo type) where T : Attribute
        {
            var entity = type.GetCustomAttribute<T>(false);
            return entity != null;
        }
        public static bool CheckAttribute<T>(this FieldInfo type) where T : Attribute
        {
            var entity = type.GetCustomAttribute<T>(false);
            return entity != null;
        }
        public static bool CheckAttribute<T>(this MethodInfo type) where T : Attribute
        {
            var entity = type.GetCustomAttribute<T>(false);
            return entity != null;
        }
        private static T GetCustomAttribute<T>(this Type type, bool inherit) where T : Attribute
        {
            var atts = type.GetCustomAttributes(typeof(T), inherit);
            if (atts.Length == 0) return null;
            return atts[0] as T;
        }

        public static List<T> FillSummary<T>(this List<T> collection, XmlVsComment[] comments) where T : MarkDownBase
        {
            foreach (var item in collection)
                item.Summary = comments.FirstOrDefault(s => s.FullMemberName == item.FullMemberName)?.Summary;
            return collection;
        }

        public static string FullContent(this List<IMdStringEditor> collection)
        {
            var sb = new StringBuilder();
            foreach (var item in collection)
                sb.AppendLine(item.ToString());
            return sb.ToString();
        }
    }
}
