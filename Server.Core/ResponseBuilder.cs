using System.IO;

namespace Server.Core
{
    public class ResponseBuilder
    {
        private int _statusCode;
        private string _contentType;
        private Stream _body;

        public ResponseBuilder()
        {
            _statusCode = 200;
            _contentType = "";
        }

        public Response Build()
        {
            return new Response(_statusCode, _contentType, _body);
        }

        public ResponseBuilder SetBody(byte[] bodyMessage)
        {
            _body = new MemoryStream();
            _body.Write(bodyMessage, 0, bodyMessage.Length);
            return this;
        }

        public ResponseBuilder SetBody(Stream bodyStream)
        {
            _body = bodyStream;
            return this;
        }

        public Stream GetBodyStream()
        {
            return _body;
        }

        public ResponseBuilder SetStatusCode(int statusCode)
        {
            _statusCode = statusCode;
            return this;
        }

        public ResponseBuilder SetContentType(string contentType)
        {
            _contentType = contentType;
            return this;
        }
    }
}
