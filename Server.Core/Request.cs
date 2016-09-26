namespace Server
{
    public class Request
    {
        public string HttpVersion { get; internal set; }
        public string Uri { get; internal set; }
        public string Method { get; internal set; }
        public string[] Headers { get; internal set; }
        public byte[] Body { get; internal set; }

        public Request()
        {
            HttpVersion = "HTTP/1.1";
            Uri = "/";
            Method = "GET";
            Headers = new string[0];
            Body = new byte[0];
        }
    }
}
