using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using MarkDown.Generator.Exceptions;
using MarkDown.Generator.Interfaces;
using MarkDown.Generator.Models;

namespace MarkDown.Generator
{
    internal class XmlVsParser: IXmlVsParser
    {
        /// <summary>
        /// Get from repository https://github.com/neuecc/MarkdownGenerator/blob/master/VSDocParser.cs
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        private XmlVsComment[] ParseXmlComment(XDocument document)
        {
            ConditionValidator.ThrowExceptionIfNotValid<ArgumentNullException>((document == null), nameof(document));

            return document.Descendants("member")
                .Select(x =>
                {
                    var match = Regex.Match(x.Attribute("name").Value, @"(.):(.+)\.([^.()]+)?(\(.+\)|$)");
                    if (!match.Groups[1].Success) return null;

                    var memberType = (MemberType)match.Groups[1].Value[0];
                    if (memberType == MemberType.None) return null;

                    var summary = ((string)x.Element("summary")) ?? "";
                    if (summary != "")
                    {
                        summary = string.Join("  ", summary.Split(new[] { "\r", "\n", "\t" }, StringSplitOptions.RemoveEmptyEntries).Select(y => y.Trim()));
                    }

                    var returns = ((string)x.Element("returns")) ?? "";
                    var remarks = ((string)x.Element("remarks")) ?? "";
                    var parameters = x.Elements("param")
                        .Select(e => Tuple.Create(e.Attribute("name").Value, e))
                        .Distinct(new ItemEqualityComparer<string, XElement>())
                        .ToDictionary(e => e.Item1, e => e.Item2.Value);

                    var className = (memberType == MemberType.Type)
                        ? match.Groups[2].Value + "." + match.Groups[3].Value
                        : match.Groups[2].Value;

                    return new XmlVsComment
                    {
                        MemberType = memberType,
                        ClassName = className,
                        MemberName = match.Groups[3].Value,
                        Summary = summary.Trim(),
                        Remarks = remarks.Trim(),
                        Parameters = parameters,
                        Returns = returns.Trim()
                    };
                })
                .Where(x => x != null)
                .ToArray();
        }

        public XmlVsComment[] GetComments(string path)
        {
            ConditionValidator.ThrowExceptionIfNotValid<ArgumentNullException>((string.IsNullOrWhiteSpace(path)),
                nameof(path));
            return ParseXmlComment(GetLoadFile(path));
        }

        private XDocument GetLoadFile(string path)
        {
            ConditionValidator.ThrowExceptionIfNotValid<ArgumentNullException>((!File.Exists(path)),
                nameof(path), $"Path '{path}' does not exist");
            return XDocument.Load(path);
        }
    }
}
