using MarkDown.Generator.Models;

namespace MarkDown.Generator.Interfaces
{
    public interface IMdFluentBuilder
    {
        string Template { get; }
        string FileName { get; }
        MarkDownType Type { get; }
        IMdStringEditor Build();
    }
}