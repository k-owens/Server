namespace Server.Core
{
    public interface IResponseHandler
    {
        Response HandleResponse(Request request);
    }
}
