using MarkDown.Generator.Attributes;

namespace MarkDown.TestLibrary
{
    /// <summary>
    /// Test comments
    /// </summary>
    [Documented]
    public class TestClass
    {
        /// <summary>
        /// My field comment
        /// </summary>
        public string MyField { get; } = "string.Empty";

        /// <summary>
        /// _myField1 desc
        /// </summary>
        public string _myField1;

        /// <summary>
        /// _myField2 desc
        /// </summary>
        [IgnoredDocs]
        static string _myField2;

        /// <summary>
        /// Test summary
        /// </summary>
        //[IgnoredDocs]
        public int Prop1 { get; set; }

        /// <summary>
        /// Test prop 2
        /// </summary>
        public int Prop2 { get; set; }

        /// <summary>
        /// CreateProp1 desc
        /// </summary>
        public void CreateProp1()
        {
        }

        /// <summary>
        /// GetProp1 desc
        /// </summary>
        /// <returns></returns>
        [IgnoredDocs]
        public int GetProp1()
        {
            return 1;
        }

        /// <summary>
        /// GetProp22 desc
        /// </summary>
        /// <returns></returns>
        private int GetProp22()
        {
            return 1;
        }

        /// <summary>
        /// Test methods1
        /// </summary>
        /// <param name="id1">identifier 1</param>
        /// <param name="id2">identifier 2</param>
        /// <returns></returns>
        public string TesStringMethod(int id1, string id2)
        {
            return string.Empty;
        }
    }
}