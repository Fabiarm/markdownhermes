using MarkDown.Generator.Attributes;

namespace UnitTest.MarkDown.Generator.TestData
{
    /// <summary>
    /// TestEnum desc
    /// </summary>
    [Documented]
    public enum TestEnum
    {
        /// <summary>
        /// Test01 desc enum
        /// </summary>
        Test01,

        /// <summary>
        /// Test02 desc enum
        /// </summary>
        [IgnoredDocs]
        Test02
    }
}
