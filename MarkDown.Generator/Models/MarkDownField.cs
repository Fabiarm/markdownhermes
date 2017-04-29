using System;

namespace MarkDown.Generator.Models
{
    public class MarkDownField : MarkDownBase
    {
        public Type PropertyType { get; set; }

        public MarkDownField(MemberType type, Type representType) : base(type, representType)
        {
        }
    }
}