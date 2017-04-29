using System.Collections.Generic;
using System.Reflection;
using MarkDown.Generator.Models;

namespace MarkDown.Generator.Interfaces
{
    public interface IMdGenerator
    {
        List<MarkDownType> GetTypes(Assembly assembly);
        List<MarkDownType> UpdateComments(List<MarkDownType> types, XmlVsComment[] comments);
    }
}