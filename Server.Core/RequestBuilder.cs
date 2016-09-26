 using System;
 using System.Text;

namespace Server.Core
{
    public class RequestBuilder
    {
        private Request request;

        public RequestBuilder()
        {
            request = new Request();
        }

        public Request BuildRequest()
        {
            return request;
        }

        public Request BuildRequestFromWholeMessage(byte[] requestMessage)
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

        public void SetRequestHttpVersion(string httpVersion)
        {
            request.HttpVersion = httpVersion;
        }

        public void SetRequestUri(string uri)
        {
            request.Uri = uri;
        }

        public void SetRequestMethod(string method)
        {
            request.Method = method;
        }

        public void SetRequestHeaders(string[] headers)
        {
            request.Headers = headers;
        }

        public void SetRequestBody(byte[] body)
        {
            request.Body = body;
        }

        public void SetRequestBody(string bodyMessage)
        {
            request.Body = Encoding.UTF8.GetBytes(bodyMessage);
        }

        private byte[] GetBodyOfMessage(int startIndex, byte[] message)
        {
            var body = new byte[message.Length - startIndex];
            for (var index = startIndex; index < message.Length; index++)
            {
                body[index-startIndex] = message[index];
            }
            return body;
        }

        private string[] DivideHeaders(string[] allLines)
        {
            if(allLines.Length < 3)
                return new string[0];

            string[] headers = new string[allLines.Length-3];
            for (int i = 0; i < allLines.Length - 3; i++)
            {
                if(allLines[i+1].Length > 0)
                    headers[i] = allLines[i + 1].Substring(0, allLines[i+1].Length-1);
            }
            return headers;
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
                request.Body = GetBodyOfMessage(index + 4, requestMessage);
        }

        private void ParseStartingLine(string firstLine)
        {
            var requestLine = firstLine.Split(' ', ' ');
            request.Method = requestLine[0];
            request.Uri = requestLine[1];
            request.HttpVersion = requestLine[2];
        }

        private void SetRequestToNoValues()
        {
            request.Method = "";
            request.Uri = "";
            request.HttpVersion = "";
            request.Body = new byte[0];
            request.Headers = new string[0];
        }
    }
}
