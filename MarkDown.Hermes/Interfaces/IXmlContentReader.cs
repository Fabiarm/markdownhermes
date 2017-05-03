namespace MarkDown.Hermes.Interfaces
{
    public interface IXmlContentReader
    {
        string GetContent(string pathToSettings);
        string GetTemplateId(string pathToSettings);
    }
}