using MarkDown.Hermes.Interfaces;

namespace MarkDown.Hermes.Helper
{
    public static class Forge
    {
        public static IOutputWriter GetOutputWriter()
        {
            return new OutputWriter();
        }

        public static IXmlContentReader GetXmlContentReader()
        {
            return new XmlContentReader();
        }
    }
}
