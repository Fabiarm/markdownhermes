using System;

namespace MarkDown.Hermes.Interfaces
{
    public interface IOutputWriter
    {
        void WriteError(Exception ex);
        void WriteInfo(string message);
    }
}
