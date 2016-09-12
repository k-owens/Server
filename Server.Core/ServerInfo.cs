namespace Server.Core
{
    public class ServerInfo
    {
        public int Port { get; }
        public IResponseHandler HttpHandler { get; }
        public int Timeout { get; }

        public ServerInfo(int port, IResponseHandler httpHandler, int timeout)
        {
            HttpHandler = httpHandler;
            Port = port;
            Timeout = timeout;
        }
    }
}
