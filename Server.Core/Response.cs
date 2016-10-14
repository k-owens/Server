using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Server.Core
{
    public  class Response
    {
        public int StatusCode { get; private set; }
        public string ContentType { get; private set; }
        public Stream Body { get; private set; }

        private int _bodySize;

        //public Response()
        //{
        //    StatusCode = 200;
        //    ContentType = "";
        //}

        public Response(int statusCode, string contentType, Stream body)
        {
            StatusCode = statusCode;
            ContentType = contentType;
            Body = body;
        }

        public Task<string> GetBodyAsStringAsync()
        {
            Body.Position = 0;
            var streamReader = new StreamReader(Body ?? new MemoryStream());
            return streamReader.ReadToEndAsync();
        }

        internal byte[] MessageForClient()
        {
            var body = GetBodyMessage();
            var topOfMessage = CreateStartingLine() + CreateHeaders() + "\r\n";

            var wholeMessage = new byte[topOfMessage.Length + body.Length];
            CombineArrays(Encoding.UTF8.GetBytes(topOfMessage), wholeMessage, body);
            return wholeMessage;
        }

        private byte[] GetBodyMessage()
        {
            if (Body == null)
            {
                return new byte[0];
            }
            ReadyStreamForRead();
            var buffer = new byte[(int)Body.Length];
            _bodySize = Body.Read(buffer, 0, buffer.Length);
            return buffer;
        }

        private string CreateHeaders()
        {
            if (_bodySize == 0)
                return "";
            if (ContentType == null)
                return "Content-Length: " + _bodySize + "\r\n";            
            else
                return "Content-Type: " + ContentType + "\r\n" + "Content-Length: " + _bodySize + "\r\n";
        }

        private string CreateStartingLine()
        {
            switch (StatusCode)
            {
                case 0:
                    return "HTTP/1.1 200 OK\r\n";
                case 200:
                    return "HTTP/1.1 200 OK\r\n";
                case 201:
                    return "HTTP/1.1 201 Created\r\n";
                case 400:
                    return "HTTP/1.1 400 Bad Request\r\n";
                case 404:
                    return "HTTP/1.1 404 Not Found\r\n";
                case 409:
                    return "HTTP/1.1 409 Conflict\r\n";
                case 505:
                    return "HTTP/1.1 505 HTTP Version Not Supported\r\n";
                default:
                    return "HTTP/1.1 501 Not Implemented\r\n";
            }
        }

        private static void CombineArrays(byte[] messageBytes, byte[] combinedMessage, byte[] bodyMessage)
        {
            Buffer.BlockCopy(messageBytes, 0, combinedMessage, 0, messageBytes.Length);
            Buffer.BlockCopy(bodyMessage, 0, combinedMessage, messageBytes.Length, bodyMessage.Length);
        }

        private void ReadyStreamForRead()
        {
            Body.Flush();
            Body.Position = 0;
        }
    }
}
