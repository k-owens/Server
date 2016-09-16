namespace Server.Core
{
    public interface IRequestHandler
    {
        Response HandleRequest(Request request);
    }
}
