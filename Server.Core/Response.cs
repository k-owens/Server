using System.IO;

namespace Server.Core
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string ContentType { get; set; }
        public MemoryStream Body { get; set; }

        internal void ReadyStreamForRead()
        {
            Body.Flush();
            Body.Position = 0;
        }
    }
}
