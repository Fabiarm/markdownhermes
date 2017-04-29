using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MarkDown.Generator.Attributes;
using MarkDown.Generator.Exceptions;
using MarkDown.Generator.Extensions;
using MarkDown.Generator.Helper;
using MarkDown.Generator.Interfaces;
using MarkDown.Generator.Models;

namespace MarkDown.Generator
{
    internal class MdGenerator : IMdGenerator
    {
        public List<MarkDownType> GetTypes(Assembly assembly)
        {
            var list = new List<MarkDownType>();
            ConditionValidator.ThrowExceptionIfNotValid<ArgumentNullException>((assembly == null),
                nameof(assembly));

            if (assembly == null) return list;

            var typeArray = assembly.GetTypes();

            foreach (var type in typeArray)
            {
                if (type.CustomAttributes!=null && type.CheckAttribute<Documented>())
                {
                    var entity = MarkDownEntityCreator.CreateType(type, !type.IsEnum);

                    entity.Properties.AddRange(ReflectValidator.GetProperties(type, entity));
                    entity.Fields.AddRange(ReflectValidator.GetFields(type, entity));
                    entity.Methods.AddRange(ReflectValidator.GetMethods(type, entity));

                    list.Add(entity);
                }
            }
            return list;
        }

        public List<MarkDownType> UpdateComments(List<MarkDownType> types, XmlVsComment[] comments)
        {
            ConditionValidator.ThrowExceptionIfNotValid<ArgumentNullException>((types == null), nameof(types));
            ConditionValidator.ThrowExceptionIfNotValid<ArgumentNullException>((comments == null), nameof(comments));
            foreach (var markDownType in types)
            {
                markDownType.Summary = comments.FirstOrDefault(s => s.FullMemberName == markDownType.FullMemberName)
                    ?.Summary;
                markDownType.Properties.FillSummary(comments);
                markDownType.Fields.FillSummary(comments);
                markDownType.Methods.FillSummary(comments);
            }
            return types;
        }
    }
}