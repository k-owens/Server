namespace Server.Core
{
    public interface IResponseHandler
    {
        Reply Execute(Request request);
    }
}
