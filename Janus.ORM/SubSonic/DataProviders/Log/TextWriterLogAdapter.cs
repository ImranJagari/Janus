using System.IO;

namespace Janus.ORM.SubSonic.DataProviders.Log
{
    public class TextWriterLogAdapter : ILogAdapter
    {
        public TextWriter Writer { get; private set; }

        public TextWriterLogAdapter(TextWriter writer)
        {
            Writer = writer;
        }

        public void Log(string message)
        {
            Writer.WriteLine(message);
        }
    }
}