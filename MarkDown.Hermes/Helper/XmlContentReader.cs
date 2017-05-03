using System;
using System.IO;
using System.Xml;
using MarkDown.Hermes.Interfaces;

namespace MarkDown.Hermes.Helper
{
    internal class XmlContentReader: IXmlContentReader
    {
        public string GetContent(string pathToSettings)
        {
            if (string.IsNullOrEmpty(pathToSettings)) return string.Empty;
            try
            {
                if (File.Exists(pathToSettings))
                {
                    var doc = new XmlDocument();
                    doc.Load(pathToSettings);
                    var templateId = doc.SelectSingleNode("//root/TemplateId")?.InnerText;
                    if (!string.IsNullOrWhiteSpace(templateId) &&
                        templateId.Equals("default", StringComparison.OrdinalIgnoreCase))
                        return string.Empty;
                    if (!string.IsNullOrWhiteSpace(templateId))
                    {
                        var pattern = $"//root/Templates/Template[@Id='{templateId}']";
                        var node = doc.SelectSingleNode(pattern);
                        if (node != null)
                            return node.InnerText;
                    }
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
            return string.Empty;
        }

        public string GetTemplateId(string pathToSettings)
        {
            if (string.IsNullOrEmpty(pathToSettings)) return string.Empty;
            try
            {
                if (File.Exists(pathToSettings))
                {
                    var doc = new XmlDocument();
                    doc.Load(pathToSettings);
                    return doc.SelectSingleNode("//root/TemplateId")?.InnerText;
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
            return string.Empty;
        }
    }
}