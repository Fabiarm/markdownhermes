using System.ComponentModel;

namespace MarkDown.Generator.Models
{
    public enum MemberType
    {
        [Description("")] None = 0,
        [Description("P")] Property = 'P',
        [Description("F")] Field = 'F',
        [Description("E")] Event = 'E',
        [Description("T")] Type = 'T',
        [Description("M")] Method = 'M'
    }
}
