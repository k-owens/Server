using System.IO;
using Server.Core;
using System.Text;

namespace Server.Test
{
    public class TestResponse : IRequestHandler
    {
        public Response HandleRequest(Request request)
        {
            ResponseBuilder responseBuilder = new ResponseBuilder();
            responseBuilder.SetStatusCode(200);
            var message = Encoding.UTF8.GetBytes("Hello World");
            responseBuilder.SetBody(new MemoryStream());
            responseBuilder.GetBodyStream().Write(message, 0, message.Length);
            responseBuilder.SetContentType("text/plain");
            return responseBuilder.BuildResponse();
        }
    }
}
