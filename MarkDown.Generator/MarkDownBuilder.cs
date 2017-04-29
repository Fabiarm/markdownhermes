using System;
using System.Collections.Generic;
using System.Reflection;
using MarkDown.Generator.Exceptions;
using MarkDown.Generator.Extensions;
using MarkDown.Generator.Helper;
using MarkDown.Generator.Interfaces;
using MarkDown.Generator.Models;

namespace MarkDown.Generator
{
    public class MarkDownBuilder : IMarkDownBuilder
    {
        private readonly IMdGenerator _markDownGenerator;
        private readonly IXmlVsParser _xmlVsParser;
        private List<IMdStringEditor> _content;

        public MarkDownBuilder(IMdGenerator markDownGenerator, IXmlVsParser xmlVsParser)
        {
            ConditionValidator.ThrowExceptionIfNotValid<ArgumentNullException>((markDownGenerator == null),
                nameof(markDownGenerator));
            ConditionValidator.ThrowExceptionIfNotValid<ArgumentNullException>((xmlVsParser == null),
                nameof(xmlVsParser));

            _markDownGenerator = markDownGenerator;
            _xmlVsParser = xmlVsParser;
        }

        public List<MarkDownType> Types { get; private set; }
        public List<IMdStringEditor> Content => _content;
        public string FullContent => _content.FullContent();

        public void Load(string dllPath, string dllXmlPath)
        {
            ConditionValidator.ThrowExceptionIfNotValid<ArgumentNullException>((string.IsNullOrWhiteSpace(dllPath)),
                nameof(dllPath));
            ConditionValidator.ThrowExceptionIfNotValid<ArgumentNullException>((string.IsNullOrWhiteSpace(dllXmlPath)),
                nameof(dllXmlPath));

            _content = new List<IMdStringEditor>();

            var assembly = Assembly.LoadFile(dllPath);
            var types = _markDownGenerator.GetTypes(assembly);
            var comments = _xmlVsParser.GetComments(dllXmlPath);
            Types = _markDownGenerator.UpdateComments(types, comments);
        }

        public void Build()
        {
            ConditionValidator.ThrowSystemExceptionIfNotValid<OperationCanceledException>(Types == null,
                $"Should be run 'Load' method");
            _content = new List<IMdStringEditor>();
            foreach (var type in Types)
            {
                var mb = new MdStringEditor(type.Name);
                MdStringBuilder.CreateHeader(mb, type);
                MdStringBuilder.BuildTable(mb, type, "Properties", type.Properties);
                MdStringBuilder.BuildTable(mb, type, "Fields", type.Fields);
                MdStringBuilder.BuildTable(mb, type, "Methods", type.Methods);
                _content.Add(mb);
            }
        }
    }
}
