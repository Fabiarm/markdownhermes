using MarkDown.Hermes.Helper;

namespace MarkDown.Hermes
{
    class Program
    {
        static void Main(string[] args)
        {
            Executor.Run(args, Forge.GetOutputWriter());
        }
    }
}
