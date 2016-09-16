namespace Server.Core
{
    public class ServerInfo
    {
        public int Port { get; }
        public IRequestHandler HttpHandler { get; }
        public int Timeout { get; }

        public ServerInfo(int port, IRequestHandler httpHandler, int timeout)
        {
            HttpHandler = httpHandler;
            Port = port;
            Timeout = timeout;
        }
    }
}
