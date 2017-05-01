using System;
using System.Linq;
using System.Text.RegularExpressions;
using MarkDown.Generator.Exceptions;
using MarkDown.Generator.Helper;
using MarkDown.Generator.Interfaces;
using MarkDown.Generator.Models;

namespace MarkDown.Generator
{
    internal class MdFluentBuilder: IMdFluentBuilder
    {
        public MdFluentBuilder(string template, string fileName, MarkDownType type)
        {
            ConditionValidator.ThrowExceptionIfNotValid<ArgumentNullException>((string.IsNullOrWhiteSpace(template)),
                nameof(template));
            ConditionValidator.ThrowExceptionIfNotValid<ArgumentNullException>((string.IsNullOrWhiteSpace(fileName)),
                nameof(fileName));
            ConditionValidator.ThrowExceptionIfNotValid<ArgumentNullException>((type == null),
                nameof(type));
            Template = template;
            FileName = fileName;
            Type = type;
        }

        public string Template { get; }
        public string FileName { get; }
        public MarkDownType Type { get; }

        public IMdStringEditor Build()
        {
            var mb = new MdStringEditor(FileName);
            CreateContent(mb);
            return mb;
        }

        private void CreateContent(IMdStringEditor mb)
        {
            var template = Template;
            template = template.Replace(Variables.TagPrefix, Type.Prefix);
            template = template.Replace(Variables.TagFullName, Type.FullName);
            template = template.Replace(Variables.TagSummary, Type.Summary);

            var properties = GetCollection(template, Variables.TagProperty);
            var fields = GetCollection(properties, Variables.TagField);
            var methods = GetCollection(fields, Variables.TagMethod);

            mb.AppendLine(methods);
        }

        private string GetCollection(string template, string tag)
        {
            var isProperties = false;
            var isFields = false;
            var isMethods = false;

            if (tag == Variables.TagProperty)
                isProperties = true;
            else if (tag == Variables.TagField)
                isFields = true;
            else if (tag == Variables.TagMethod)
                isMethods = true;

            var regMainExp = $"@{tag}s.*?@end{tag}s";
            var regCollExp = $"@{tag}.*?@end{tag}";
            var matchProperties = Regex.Matches(template, regMainExp, RegexOptions.Singleline);
            if (matchProperties.Count > 1)
                throw new Exception($"Section '@{tag}s' should be at least.");
            if (matchProperties.Count != 1) return template;

            if ((isProperties && Type.Properties.Count == 0) ||
                (isFields && Type.Fields.Count == 0) ||
                (isMethods && Type.Methods.Count == 0))
                return template.Replace(matchProperties[0].Value, string.Empty);

            var props = matchProperties[0]
                .Value.Replace($"@{tag}s", string.Empty)
                .Replace($"@end{tag}s", string.Empty);

            var matchProperty = Regex.Matches(props, regCollExp, RegexOptions.Singleline);
            if (matchProperty.Count > 1)
                throw new Exception($"Section '@{tag}' should be at least.");
            var prop = matchProperty[0].Value.Replace($"@{tag}", string.Empty).Replace($"@end{tag}", string.Empty);

            var context = string.Empty;

            if (isProperties)
                context = Type.Properties.Aggregate(context,
                    (current, property) => current + (prop.Replace(Variables.TagChildType,
                                                              HelpUtils.WritePropertyType(property))
                                                          .Replace(Variables.TagChildName,
                                                              HelpUtils.WritePropertyName(property))
                                                          .Replace(Variables.TagChildSummary, property.Summary) +
                                                      "\n"));
            if (isFields)
                context = Type.Fields.Aggregate(context,
                    (current, field) => current + (prop.Replace(Variables.TagChildType,
                                                           HelpUtils.WritePropertyType(field))
                                                       .Replace(Variables.TagChildName,
                                                           HelpUtils.WritePropertyName(field))
                                                       .Replace(Variables.TagChildSummary, field.Summary) + "\n"));
            if (isMethods)
                context = Type.Methods.Aggregate(context,
                    (current, method) => current + (prop.Replace(Variables.TagChildType,
                                                            HelpUtils.WritePropertyType(method))
                                                        .Replace(Variables.TagChildName,
                                                            HelpUtils.WritePropertyName(method))
                                                        .Replace(Variables.TagChildSummary, method.Summary) + "\n"));
            prop = context;
            var content = template.Replace(matchProperty[0].Value, prop);
            return content.Replace(matchProperties[0].Value, content)
                .Replace($"@{tag}s", string.Empty)
                .Replace($"@end{tag}s", string.Empty);
        }
    }
}
