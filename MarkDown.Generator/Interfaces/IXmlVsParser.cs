using MarkDown.Generator.Models;

namespace MarkDown.Generator.Interfaces
{
    public interface IXmlVsParser
    {
        XmlVsComment[] GetComments(string path);
    }
}