namespace Server.Core
{
    public class RoutingInfo
    {
        public HttpMethod Method { get; }
        public string Uri { get; }
        public IRequestHandler HandlerToRun { get; }

        public RoutingInfo(HttpMethod method, string uri, IRequestHandler handlerToRun)
        {
            Method = method;
            Uri = uri;
            HandlerToRun = handlerToRun;
        }
    }
}
