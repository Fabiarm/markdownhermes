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
                        !templateId.Equals("default", StringComparison.OrdinalIgnoreCase))
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
                    var result = doc.SelectSingleNode("//root/TemplateId")?.InnerText;
                    if (result != null)
                        return result;
                    throw new Exception("Could not find template id");
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