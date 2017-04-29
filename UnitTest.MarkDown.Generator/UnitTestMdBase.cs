using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using MarkDown.Generator;
using MarkDown.Generator.Interfaces;
using MarkDown.Generator.Models;
using NUnit.Framework;

namespace UnitTest.MarkDown.Generator
{
    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    public abstract class UnitTestMdBase
    {
        /// <summary>
        /// 
        /// </summary>
        protected UnitTestMdBase()
        {
            SetUpConfiguration();
        }

        /// <summary>
        /// 
        /// </summary>
        public List<MarkDownType> Types;
        /// <summary>
        /// 
        /// </summary>
        public XmlVsComment[] Comments;
        /// <summary>
        /// 
        /// </summary>
        public IMdGenerator MarkDownGeneratorObj;
        /// <summary>
        /// 
        /// </summary>
        public IXmlVsParser XmlVsParserObj;
        /// <summary>
        /// 
        /// </summary>
        public Assembly GetExecutingAssembly;
        /// <summary>
        /// 
        /// </summary>
        public string PathToXmlDocumentation;
        /// <summary>
        /// 
        /// </summary>
        public string PathToDll;

        /// <summary>
        /// 
        /// </summary>
        public abstract bool UseTypes { get; }
        /// <summary>
        /// 
        /// </summary>
        public abstract bool UseComments { get; }
        /// <summary>
        /// 
        /// </summary>
        public abstract bool UseMarkDownGenerator { get; }
        /// <summary>
        /// 
        /// </summary>
        public abstract bool UseXmlVsParser { get; }
        /// <summary>
        /// 
        /// </summary>
        public abstract bool UseExecutingAssembly { get; }

        /// <summary>
        /// 
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            SetUpConfiguration();
        }

        private void SetUpConfiguration()
        {
            if (UseExecutingAssembly)
            {
                GetExecutingAssembly = Assembly.GetExecutingAssembly();
                var codeBase = new Uri(GetExecutingAssembly.CodeBase);
                PathToXmlDocumentation = Path.Combine(path1: Path.GetDirectoryName(codeBase.LocalPath),
                    path2: "UnitTest.MarkDown.Generator.xml");
                PathToDll = Path.Combine(path1: Path.GetDirectoryName(codeBase.LocalPath),
                    path2: "UnitTest.MarkDown.Generator.dll");
            }
            if (UseXmlVsParser)
                XmlVsParserObj = new XmlVsParser();
            if (UseMarkDownGenerator)
                MarkDownGeneratorObj = new MdGenerator();
            if (UseTypes)
                Types = new List<MarkDownType>()
                {
                    new MarkDownType(MemberType.Type, null, false)
                    {
                        Name = "Name01",
                        FullName = "ClassName01",
                        Properties = new List<MarkDownProperty>()
                        {
                            new MarkDownProperty(MemberType.Property, null) {Name = "Prop01", FullName = "ClassName01"}
                        },
                        Fields = new List<MarkDownField>()
                        {
                            new MarkDownField(MemberType.Field, null) {Name = "Field01", FullName = "ClassName01"}
                        },
                        Methods = new List<MarkDownMethod>()
                        {
                            new MarkDownMethod(MemberType.Method, null) {Name = "Method01", FullName = "ClassName01"}
                        }
                    }
                };
            if (UseComments)
                Comments = new XmlVsComment[]
                {
                    new XmlVsComment()
                    {
                        ClassName = "ClassName01",
                        MemberName = "Name01",
                        MemberType = MemberType.Type,
                        Summary = "Summary01",
                        Remarks = "Remarks01",
                        Returns = string.Empty,
                        Parameters = null
                    },
                    new XmlVsComment()
                    {
                        ClassName = "ClassName01",
                        MemberName = "Prop01",
                        MemberType = MemberType.Property,
                        Summary = "Summary02",
                        Remarks = "Remarks02",
                        Returns = string.Empty,
                        Parameters = null
                    },
                    new XmlVsComment()
                    {
                        ClassName = "ClassName03",
                        MemberName = "Name03",
                        MemberType = MemberType.Type,
                        Summary = "Summary03",
                        Remarks = "Remarks03",
                        Returns = string.Empty,
                        Parameters = null
                    },
                    new XmlVsComment()
                    {
                        ClassName = "ClassName01",
                        MemberName = "Field01",
                        MemberType = MemberType.Field,
                        Summary = "Summary04"
                    },
                    new XmlVsComment()
                    {
                        ClassName = "ClassName01",
                        MemberName = "Method01",
                        MemberType = MemberType.Method,
                        Summary = "Summary05",
                        Remarks = "Remarks05",
                        Returns = "void"
                    }
                };
        }
    }
}
