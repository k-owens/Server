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

        public Response Build()
        {
            return _response;
        }

        public ResponseBuilder SetBody(byte[] bodyMessage)
        {
            _response.Body = new MemoryStream();
            _response.Body.Write(bodyMessage, 0, bodyMessage.Length);
            return this;
        }

        public ResponseBuilder SetBody(Stream bodyStream)
        {
            _response.Body = bodyStream;
            return this;
        }

        public Stream GetBodyStream()
        {
            return _response.Body;
        }

        public ResponseBuilder SetStatusCode(int statusCode)
        {
            _response.StatusCode = statusCode;
            return this;
        }

        public ResponseBuilder SetContentType(string contentType)
        {
            _response.ContentType = contentType;
            return this;
        }
    }
}
