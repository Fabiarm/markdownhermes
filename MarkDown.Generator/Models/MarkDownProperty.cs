using System;

namespace MarkDown.Generator.Models
{
    public class MarkDownProperty: MarkDownBase
    {
        public Type PropertyType { get; set; }

        public MarkDownProperty(MemberType type, Type representType) : base(type, representType)
        {
        }
    }
}