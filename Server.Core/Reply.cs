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
            var startingLine = CreateStartingLine();
            var headers = CreateHeaders();
            var newLine = Encoding.UTF8.GetBytes("\r\n");
            var body = GetBodyMessage();

            var startAndHeaders = new byte[startingLine.Length + headers.Length];
            CombineArrays(startingLine, startAndHeaders, headers);

            var startHeadersNewLine = new byte[startAndHeaders.Length + newLine.Length];
            CombineArrays(startAndHeaders, startHeadersNewLine, newLine);

            var wholeMessage = new byte[startHeadersNewLine.Length + body.Length];
            CombineArrays(startHeadersNewLine, wholeMessage, body);
            return wholeMessage;
        }

        private byte[] GetBodyMessage()
        {
            if (!(_response.Body == null))
            {
                _response.ReadyStreamForRead();
                var buffer = new byte[(int)_response.Body.Length];
                _bodySize = _response.Body.Read(buffer, 0, buffer.Length);
                return buffer;
            }
            return new byte[0];
        }

        private byte[] CreateHeaders()
        {
            if (_bodySize != 0)
            {
                if (!(_response.ContentType == null))
                {
                    return Encoding.UTF8.GetBytes("Content-Type: " + _response.ContentType + "\r\n" + "Content-Length: " + _bodySize + "\r\n");
                }
                else
                {
                    return Encoding.UTF8.GetBytes("Content-Length: " + _bodySize + "\r\n");
                }
            }
            return new byte[0];
        }

        private byte[] CreateStartingLine()
        {
            switch (_response.StatusCode)
            {
                case 0:
                    return Encoding.UTF8.GetBytes("HTTP/1.1 200 OK\r\n");
                case 200:
                    return Encoding.UTF8.GetBytes("HTTP/1.1 200 OK\r\n");
                case 201:
                    return Encoding.UTF8.GetBytes("HTTP/1.1 201 Created\r\n");
                case 400:
                    return Encoding.UTF8.GetBytes("HTTP/1.1 400 Bad Request\r\n");
                case 404:
                    return Encoding.UTF8.GetBytes("HTTP/1.1 404 Not Found\r\n");
                case 409:
                    return Encoding.UTF8.GetBytes("HTTP/1.1 409 Conflict\r\n");
                case 505:
                    return Encoding.UTF8.GetBytes("HTTP/1.1 505 HTTP Version Not Supported\r\n");
                default:
                    return Encoding.UTF8.GetBytes("HTTP/1.1 501 Not Implemented\r\n");
            }
        }

        private static void CombineArrays(byte[] messageBytes, byte[] combinedMessage, byte[] bodyMessage)
        {
            Buffer.BlockCopy(messageBytes, 0, combinedMessage, 0, messageBytes.Length);
            Buffer.BlockCopy(bodyMessage, 0, combinedMessage, messageBytes.Length, bodyMessage.Length);
        }
    }
}
