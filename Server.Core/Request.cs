using System.Collections.Generic;

namespace Server
{
    public class Request
    {
        public string HttpVersion { get; internal set; }
        public string Uri { get; internal set; }
        public string Method { get; internal set; }
        public List<string> Headers { get; internal set; }
        public byte[] Body { get; internal set; }

        public Request()
        {
            HttpVersion = "HTTP/1.1";
            Uri = "/";
            Method = "GET";
            Headers = new List<string>();
            Body = new byte[0];
        }
    }
}
