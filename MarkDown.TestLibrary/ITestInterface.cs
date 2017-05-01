using MarkDown.Generator.Attributes;

namespace MarkDown.TestLibrary
{
    /// <summary>
    /// TestInterface comment
    /// </summary>
    [Documented]
    public interface ITestInterface
    {
        /// <summary>
        /// IntProp1 in interface
        /// </summary>
       // [IgnoredDocs]
        int IntProp1 { get; set; }

        /// <summary>
        /// IntProp2 in interface
        /// </summary>
        int IntProp2 { get; set; }
    }
}