using System;

namespace MarkDown.Generator.Attributes
{
    /// <summary>
    /// Allows to mark artefact for documentation
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface |
                    AttributeTargets.Enum)]
    public class Documented : Attribute
    {
    }
}