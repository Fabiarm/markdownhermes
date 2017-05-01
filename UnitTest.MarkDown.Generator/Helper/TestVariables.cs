using System.Text;

namespace UnitTest.MarkDown.Generator.Helper
{
    /// <summary>
    /// 
    /// </summary>
    public static class TestVariables
    {
        public static string TemplateFields = "@fields\n" +
                                      "* * * \n" +
                                      "__Fields__\n" +
                                      "\n" +
                                      "| Value | Name | Summary |\n" +
                                      "| --- | --- | --- |\n" +
                                      "@field| @field.type | @field.name | @field.summary |@endfield\n" +
                                      "@endfields\n" +
                                      "";

        public static string TemplateProperties = "@props\n" +
                                                  "* * * \n" +
                                                  "__Properties__\n" +
                                                  "\n" +
                                                  "| Value | Name | Summary |\n" +
                                                  "| --- | --- | --- |\n" +
                                                  "@prop| @prop.type | @prop.name | @prop.summary |@endprop\n" +
                                                  "@endprops\n" +
                                                  "";

        public static string TemplateMethods = "@methods\n" +
                                               "* * * \n" +
                                               "__Methods__\n" +
                                               "\n" +
                                               "| Value | Name | Summary |\n" +
                                               "| --- | --- | --- |\n" +
                                               "@method| @method.type | @method.name | @method.summary |@endmethod\n" +
                                               "@endmethods\n" +
                                               "";

        public static string TemplateHeader = "### @prefix\n" +
                                              "__Namespace__: @fullName\n" +
                                              "* * *\n" +
                                              " __Summary__: @summary\n";

        public static string TemplateFull
        {
            get
            {
                var sb = new StringBuilder();
                sb.AppendLine(TemplateHeader);
                sb.AppendLine(TemplateProperties);
                sb.AppendLine(TemplateFields);
                sb.AppendLine(TemplateMethods);
                return sb.ToString();
            }
        }
    }
}
