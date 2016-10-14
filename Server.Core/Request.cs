using System.Collections.Generic;

namespace Server.Core
{
    public class Request
    {
        public string HttpVersion { get; private set; }
        public string Uri { get; private set; }
        public HttpMethod Method { get; private set; }
        public List<string> Headers { get; private set; }
        public byte[] Body { get; private set; }

        public Request(string httpVersion, string uri, HttpMethod method, List<string> headers, byte[] body)
        {
            HttpVersion = httpVersion;
            Uri = uri;
            Method = method;
            Headers = headers;
            Body = body;
        }
    }
}
