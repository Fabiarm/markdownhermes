using System.Linq;
using System.Reflection;
using MarkDown.Generator.Models;

namespace MarkDown.Generator.Helper
{
    internal static class HelpUtils
    {
        private static string GetMethodPrefix(MethodInfo methodInfo)
        {
            var isExtension = methodInfo.GetCustomAttributes<System.Runtime.CompilerServices.ExtensionAttribute>(false)
                .Any();

            var seq = methodInfo.GetParameters()
                .Select(x =>
                {
                    var suffix = x.HasDefaultValue ? (" = " + (x.DefaultValue ?? $"null")) : "";
                    return "`" + x.ParameterType.Name + "` " + x.Name + suffix;
                });

            return methodInfo.Name + "(" + (isExtension ? "this " : "") + string.Join(", ", seq) + ")";
        }
        public static string WritePropertyType(MarkDownBase item)
        {
            switch (item.MemberType)
            {
                case MemberType.Property:
                    return ((MarkDownProperty)item).PropertyType.Name;
                case MemberType.Field:
                    return ((MarkDownField)item).PropertyType.Name;
                case MemberType.Method:
                    return ((MarkDownMethod)item).ReturnType.Name;
                default:
                    return ((MarkDownType)item).MemberType.ToString();
            }
        }

        public static string WritePropertyName(MarkDownBase item)
        {
            switch (item.MemberType)
            {
                case MemberType.Method:
                    var method = ((MarkDownMethod)item).ReturnMethodInfo;
                    return method != null ? GetMethodPrefix(method) : item.Name;
                default:
                    return item.Name;
            }
        }
    }
}