using System;
using System.Collections.Generic;
using MarkDown.Generator.Exceptions;
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
            ParseTemplate(mb);
            return mb;
        }

        private void ParseTemplate(IMdStringEditor mb)
        {
            var template = Template;
            var type = Type;
            mb.AppendLine("test");
        }
    }
}
