using System;
using System.Collections.Generic;
using System.Linq;
using MarkDown.Generator.Attributes;
using MarkDown.Generator.Extensions;
using MarkDown.Generator.Models;

namespace MarkDown.Generator.Helper
{
    internal static class ReflectValidator
    {
        public static IList<MarkDownProperty> GetProperties(Type type, MarkDownType entity)
        {
            var list = new List<MarkDownProperty>();
            var properties = type.GetProperties(Variables.PropertiesFlags);
            foreach (var property in properties)
            {
                if (property.CheckAttribute<IgnoredDocs>()) continue;
                list.Add(MarkDownEntityCreator.CreateProperty(property, entity.FullName));
            }
            return list;
        }

        public static IList<MarkDownField> GetFields(Type type, MarkDownType entity)
        {
            var list = new List<MarkDownField>();
            var fields = type.GetFields(Variables.FieldFlags);
            foreach (var field in fields)
            {
                if (field.CheckAttribute<IgnoredDocs>()) continue;
                list.Add(MarkDownEntityCreator.CreateField(field, entity.FullName));
            }
            return list;
        }

        public static IList<MarkDownMethod> GetMethods(Type type, MarkDownType entity)
        {
            var list = new List<MarkDownMethod>();
            var methods = type.GetMethods(Variables.MethodsFlags)
                .Where(Variables.MethodQueries).ToArray();
            foreach (var method in methods)
            {
                if (method.CheckAttribute<IgnoredDocs>()) continue;
                list.Add(MarkDownEntityCreator.CreatMethod(method, entity.FullName));
            }
            return list;
        }
    }
}
