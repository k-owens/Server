using System.IO;

namespace Server.Core
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string ContentType { get; set; }
        public Stream Body { get; set; }

        public void SetBody(byte[] bodyMessage)
        {
            Body = new MemoryStream();
            Body.Write(bodyMessage, 0, bodyMessage.Length);
        }

        internal void ReadyStreamForRead()
        {
            Body.Flush();
            Body.Position = 0;
        }
    }
}
