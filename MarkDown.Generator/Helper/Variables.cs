using System;
using System.Reflection;

namespace MarkDown.Generator.Helper
{
    internal static class Variables
    {
        public static BindingFlags PropertiesFlags = BindingFlags.Public |
                                                     BindingFlags.Static |
                                                     BindingFlags.Instance |
                                                     BindingFlags.DeclaredOnly;

        public static BindingFlags FieldFlags = BindingFlags.Public |
                                                BindingFlags.Static |
                                                BindingFlags.DeclaredOnly;

        public static BindingFlags MethodsFlags = BindingFlags.Public |
                                                  BindingFlags.Static |
                                                  BindingFlags.Instance |
                                                  BindingFlags.DeclaredOnly;

        public static Func<MethodInfo, bool> MethodQueries =
            (m => !m.Name.Equals("GetHashCode", StringComparison.OrdinalIgnoreCase) && !m.IsSpecialName);
    }
}