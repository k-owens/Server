using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class NetworkClientRequestBuilder
    {
        private Request request;

        public NetworkClientRequestBuilder()
        {
            request = new Request();
        }

        public Request BuildRequestFromClientData(byte[] requestMessage)
        {
            if (requestMessage.Length == 0)
            {
                SetRequestToNoValues();
            }
            else
            {
                ParseMessageBytes(requestMessage);
            }
            return request;
        }

        private void SetRequestToNoValues()
        {
            request.Method = "";
            request.Uri = "";
            request.HttpVersion = "";
            request.Body = new byte[0];
            request.Headers = new List<string>();
        }

        private void ParseMessageBytes(byte[] requestMessage)
        {
            string[] messageLines = Encoding.UTF8.GetString(requestMessage).Split('\n');
            var firstLine = messageLines[0].Substring(0, messageLines[0].Length - 1);
            request.Headers = DivideHeaders(messageLines);
            ParseStartingLine(firstLine);
            ParseMessageBody(requestMessage);
        }

        private void ParseMessageBody(byte[] requestMessage)
        {
            var index = Encoding.UTF8.GetString(requestMessage).IndexOf("\r\n\r\n");
            if (index == -1)
                request.Body = new byte[0];
            else
                request.Body = BodyOfMessageFromClientData(index + 4, requestMessage);
        }

        private void ParseStartingLine(string firstLine)
        {
            var requestLine = firstLine.Split(' ', ' ');
            request.Method = requestLine[0];
            request.Uri = requestLine[1];
            request.HttpVersion = requestLine[2];
        }

        private List<string> DivideHeaders(string[] allLines)
        {
            if (allLines.Length < 3)
                return new List<string>();

            var headers = new List<string>();
            for (int i = 0; i < allLines.Length - 3; i++)
            {
                if (allLines[i + 1].Length > 0)
                    headers.Add(allLines[i + 1].Substring(0, allLines[i + 1].Length - 1));
            }
            return headers;
        }

        private byte[] BodyOfMessageFromClientData(int startIndex, byte[] message)
        {
            var body = new byte[message.Length - startIndex];
            for (var index = startIndex; index < message.Length; index++)
            {
                body[index - startIndex] = message[index];
            }
            return body;
        }
    }
}
