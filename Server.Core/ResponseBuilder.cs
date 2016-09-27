using System.IO;

namespace Server.Core
{
    public class ResponseBuilder
    {
        private Response _response;

        public ResponseBuilder()
        {
            _response = new Response();
        }

        public Response BuildResponse()
        {
            return _response;
        }

        public void SetBody(byte[] bodyMessage)
        {
            _response.Body = new MemoryStream();
            _response.Body.Write(bodyMessage, 0, bodyMessage.Length);
        }

        public void SetBody(Stream bodyStream)
        {
            _response.Body = bodyStream;
        }

        public Stream GetBodyStream()
        {
            return _response.Body;
        }

        public void SetStatusCode(int statusCode)
        {
            _response.StatusCode = statusCode;
        }

        public void SetContentType(string contentType)
        {
            _response.ContentType = contentType;
        }
    }
}
