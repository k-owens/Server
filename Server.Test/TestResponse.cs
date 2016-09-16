using System.IO;
using Server.Core;
using System.Text;

namespace Server.Test
{
    public class TestResponse : IResponseHandler
    {
        public Response HandleResponse(Request request)
        {
            Response response = new Response();
            response.StatusCode = 200;
            var message = Encoding.UTF8.GetBytes("Hello World");
            response.Body = new MemoryStream();
            response.Body.Write(message, 0, message.Length);
            return response;
        }
    }
}
