using System;
using System.Reflection;
using MarkDown.Generator.Models;

namespace MarkDown.Generator.Helper
{
    internal static class MarkDownEntityCreator
    {
        public static MarkDownType CreateType(Type type, bool isEnum)
        {
            var createdType = new MarkDownType(MemberType.Type, type, isEnum)
            {
                Name = type.Name,
                FullName = type.FullName,
            };
            return createdType;
        }

        public static MarkDownProperty CreateProperty(PropertyInfo property, string fullName)
        {
            var createdType = new MarkDownProperty(MemberType.Property, property.GetType())
            {
                Name = property.Name,
                FullName = fullName,
                PropertyType = property.PropertyType,
            };
            return createdType;
        }

        public static MarkDownField CreateField(FieldInfo field, string fullName)
        {
            var createdType = new MarkDownField(MemberType.Field, field.GetType())
            {
                Name = field.Name,
                FullName = fullName,
                PropertyType = field.FieldType
            };
            return createdType;
        }

        public static MarkDownMethod CreatMethod(MethodInfo method, string fullName)
        {
            var createdType = new MarkDownMethod(MemberType.Method, method.GetType())
            {
                Name = method.Name,
                FullName = fullName,
                ReturnType = method.ReturnType,
                ReturnParameter = method.ReturnParameter,
                ReturnMethodInfo = method
            };
            return createdType;
        }
    }
}
