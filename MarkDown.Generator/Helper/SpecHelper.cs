using MarkDown.Generator.Interfaces;

namespace MarkDown.Generator.Helper
{
    public static class SpecHelper
    {
        public static IMdGenerator CreateGenerator()
        {
            return new MdGenerator();
        }

        public static IXmlVsParser CreateXmlVsParser()
        {
            return new XmlVsParser();
        }
    }
}