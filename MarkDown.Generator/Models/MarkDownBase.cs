using System;
using MarkDown.Generator.Extensions;

namespace MarkDown.Generator.Models
{
    public class MarkDownBase
    {
        public MarkDownBase(MemberType type, Type representType)
        {
            MemberType = type;
            RepresentType = representType;
            Prefix = GetPrefix();
        }

        public string Summary { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public MemberType MemberType { get; }
        public Type RepresentType { get; }
        public string Prefix { get; }

        public string FullMemberName
        {
            get
            {
                if (MemberType == MemberType.Type)
                    return MemberType.ToDescription() + ":" + FullName;
                return MemberType.ToDescription() + ":" + FullName + "." + Name;
            }
        }

        private string GetPrefix()
        {
            if (RepresentType == null) return string.Empty;
            var stat = (RepresentType.IsAbstract && RepresentType.IsSealed) ? "static " : "";
            var abst = (RepresentType.IsAbstract && !RepresentType.IsInterface && !RepresentType.IsSealed)
                ? "abstract "
                : "";
            var classOrStructOrEnumOrInterface = RepresentType.IsInterface
                ? "interface"
                : RepresentType.IsEnum
                    ? "enum"
                    : RepresentType.IsValueType
                        ? "struct"
                        : "class";

            //return $"public {stat}{abst}{classOrStructOrEnumOrInterface} {BeautifyType(RepresentType, true)}";
            return $"public {stat}{abst}{classOrStructOrEnumOrInterface} {RepresentType.Name}";
        }

        //private string BeautifyType(Type type, bool isFull = false)
        //{
        //    if (type == null) return "";
        //    if (type == typeof(void)) return "void";
        //    if (!type.IsGenericType) return (isFull) ? type.FullName : type.Name;

        //    var innerFormat = string.Join(", ", type.GetGenericArguments().Select(x => BeautifyType(x)));
        //    return Regex.Replace(
        //               isFull ? type.GetGenericTypeDefinition().FullName : type.GetGenericTypeDefinition().Name,
        //               @"`.+$", "") + "<" + innerFormat + ">";
        //}
    }
}