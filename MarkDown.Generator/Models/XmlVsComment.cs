using System.Collections.Generic;
using MarkDown.Generator.Extensions;

namespace MarkDown.Generator.Models
{
    public class XmlVsComment
    {
        public MemberType MemberType { get; set; }
        public string ClassName { get; set; }
        public string MemberName { get; set; }
        public string Summary { get; set; }
        public string Remarks { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
        public string Returns { get; set; }
        public string FullMemberName
        {
            get
            {
                if (MemberType == MemberType.Type)
                    return MemberType.ToDescription() + ":" + ClassName;
                return MemberType.ToDescription() + ":" + ClassName + "." + MemberName;
            }
        }
    }
}
