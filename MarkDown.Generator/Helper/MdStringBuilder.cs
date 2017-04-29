using System.Collections.Generic;
using System.Linq;
using MarkDown.Generator.Models;

namespace MarkDown.Generator.Helper
{
    internal static class MdStringBuilder
    {
        public static void BuildTable<T>(MdStringEditor mb, MarkDownType parent, string label, List<T> array)
            where T : MarkDownBase
        {
            if (array.Count <= 0) return;
            mb.AppendLine("* * *");
            mb.AppendLine($"__{label}__");
            mb.AppendLine();
            var head = (parent.IsEnum)
                ? new[] {"Value", "Name", "Summary"}
                : new[] {"Name", "Summary"};
            IEnumerable<T> seq = array;
            if (!parent.IsEnum)
                seq = array.OrderBy(x => x.Name);
            var data = seq.Select(item => parent.IsEnum
                ? new[] {HelpUtils.WritePropertyType(item).ToLower(), HelpUtils.WritePropertyName(item), item.Summary}
                : new[] {item.Name, item.Summary});
            mb.Table(head, data);
            mb.AppendLine();
        }

        public static void CreateHeader(MdStringEditor mb, MarkDownType parent)
        {
            mb.Header(3, $"{parent.Prefix}");
            mb.AppendLine($"__Namespace__: {parent.RepresentType.Namespace}");
            mb.AppendLine("* * *");
            mb.AppendLine($"__Summary__: {parent.Summary}");
            mb.AppendLine();
        }
    }
}
