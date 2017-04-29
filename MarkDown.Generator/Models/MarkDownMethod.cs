using System;
using System.Reflection;

namespace MarkDown.Generator.Models
{
    public class MarkDownMethod : MarkDownBase
    {
        public Type ReturnType { get; set; }
        public ParameterInfo ReturnParameter { get; set; }
        public MethodInfo ReturnMethodInfo { get; set; }

        public MarkDownMethod(MemberType type, Type representType) : base(type, representType)
        {
        }
    }
}