using System.Collections.Generic;
using MarkDown.Generator.Models;

namespace MarkDown.Generator.Interfaces
{
    public interface IMarkDownBuilder
    {
        List<MarkDownType> Types { get; }
        List<IMdStringEditor> Content { get; }
        string FullContent { get; }
        void Load(string dllPath, string dllXmlPath);
        void Build();
    }
}