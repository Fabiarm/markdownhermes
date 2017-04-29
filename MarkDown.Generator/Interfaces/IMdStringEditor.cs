using System.Collections.Generic;

namespace MarkDown.Generator.Interfaces
{
    public interface IMdStringEditor
    {
        string FileName { get; }
        void Append(string text);
        void AppendLine();
        void AppendLine(string text);
        void Header(int level, string text);
        void HeaderWithCode(int level, string code);
        void Link(string text, string url);
        void Image(string altText, string imageUrl);
        void Code(string language, string code);
        void CodeQuote(string code);
        void Table(string[] headers, IEnumerable<string[]> items);
        void List(string text);
        void ListLink(string text, string url);
    }
}
