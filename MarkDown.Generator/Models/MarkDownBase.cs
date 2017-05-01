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
            var modificator = RepresentType.IsPublic ? "public " : "";
            return $"{modificator}{stat}{abst}{classOrStructOrEnumOrInterface} {RepresentType.Name}";
        }
    }
}