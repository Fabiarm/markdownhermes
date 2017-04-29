using System;

namespace MarkDown.Generator.Attributes
{
    /// <summary>
    /// Allows to ignore artefact for documentation
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property |
                    AttributeTargets.Interface | AttributeTargets.Field | AttributeTargets.Enum | AttributeTargets.Method)]
    public class IgnoredDocs : Attribute
    {
    }
}