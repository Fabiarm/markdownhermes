using System;
using System.Reflection;

namespace MarkDown.Generator.Helper
{
    internal static class Variables
    {
        public static readonly BindingFlags PropertiesFlags = BindingFlags.Public |
                                                     BindingFlags.Static |
                                                     BindingFlags.Instance |
                                                     BindingFlags.DeclaredOnly;

        public static readonly BindingFlags FieldFlags = BindingFlags.Public |
                                                BindingFlags.Static |
                                                BindingFlags.DeclaredOnly;

        public static readonly BindingFlags MethodsFlags = BindingFlags.Public |
                                                  BindingFlags.Static |
                                                  BindingFlags.Instance |
                                                  BindingFlags.DeclaredOnly;

        public static readonly Func<MethodInfo, bool> MethodQueries =
            (m => !m.Name.Equals("GetHashCode", StringComparison.OrdinalIgnoreCase) && !m.IsSpecialName);

        public static readonly string TagPrefix = "@prefix";
        public static readonly string TagFullName = "@fullName";
        public static readonly string TagSummary = "@summary";
        public static readonly string TagProperty = "prop";
        public static readonly string TagField = "field";
        public static readonly string TagMethod = "method";
        public static readonly string TagChildType = ".type";
        public static readonly string TagChildName = ".name";
        public static readonly string TagChildSummary = ".summary";
    }
}