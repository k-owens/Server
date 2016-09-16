using System;
using System.Text;

namespace Server.Core
{
    internal class Reply
    {
        private Response _response;
        private int _bodySize;

        public Reply(Response response)
        {
            _response = response;
        }

        public byte[] MessageForClient()
        {
            var topOfMessage = CreateStartingLine() + CreateHeaders() + "\r\n";
            var body = GetBodyMessage();

            var wholeMessage = new byte[topOfMessage.Length + body.Length];
            CombineArrays(Encoding.UTF8.GetBytes(topOfMessage), wholeMessage, body);
            return wholeMessage;
        }

        private byte[] GetBodyMessage()
        {
            if (_response.Body == null)
            {
                return new byte[0];
            }
            _response.ReadyStreamForRead();
            var buffer = new byte[(int)_response.Body.Length];
            _bodySize = _response.Body.Read(buffer, 0, buffer.Length);
            return buffer;
        }

        private string CreateHeaders()
        {
            if (_bodySize == 0)
                return "";
            if (_response.ContentType == null)
                return "Content-Length: " + _bodySize + "\r\n";            
            else
                return "Content-Type: " + _response.ContentType + "\r\n" + "Content-Length: " + _bodySize + "\r\n";
        }

        private string CreateStartingLine()
        {
            switch (_response.StatusCode)
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
    }
}
