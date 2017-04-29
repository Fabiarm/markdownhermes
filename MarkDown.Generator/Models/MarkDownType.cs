using System;
using System.Collections.Generic;

namespace MarkDown.Generator.Models
{
    public class MarkDownType: MarkDownBase
    {
        public MarkDownType(MemberType type, Type representType, bool isEnum):base(type, representType)
        {
            IsEnum = isEnum;
            Properties = new List<MarkDownProperty>();
            Fields = new List<MarkDownField>();
            Methods = new List<MarkDownMethod>();
        }
        public bool IsEnum { get; }
        public List<MarkDownProperty> Properties { get; set; }
        public List<MarkDownField> Fields { get; set; }
        public List<MarkDownMethod> Methods { get; set; }
    }
}