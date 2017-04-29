using MarkDown.Hermes.Models;

namespace MarkDown.Hermes.Interfaces
{
    public interface IMarkDownReactor
    {
        void Load(string dllPath, string dllXmlPath);
        void Build(Options options);
    }
}